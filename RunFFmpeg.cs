using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace FFX265_Batch_Converter {
    internal class RunFFmpeg {
        static DateTime time上次查找ffprobe = DateTime.Now.AddDays(1);

        public static string _ffmpeg = "ffmpeg", _ffprobe = "ffprobe";

        public Thread th后台线程;

        public static readonly string[] sub带特效字幕 = { "ass", "ssa", "webvtt" }, sub文字字幕 = { "srt" };

        Regex regexTBR = new Regex(@"(?<tbr>\d+(\.\d+)?) tbr", RegexOptions.IgnoreCase | RegexOptions.Compiled);//平均帧率
        Regex regexFPS = new Regex(@"(?<fps>\d+(\.\d+)?) fps", RegexOptions.IgnoreCase | RegexOptions.Compiled);//播放帧率

        Regex regex时长 = new Regex(@"Duration:\s*((?:\d{2}:){2,}\d{2}(?:\.\d+)?)", RegexOptions.IgnoreCase | RegexOptions.Compiled);//视频时长
        //Regex regexWH = new Regex(@", (?<w>[1-9]\d+)x(?<h>[1-9]\d+)\W+", RegexOptions.IgnoreCase | RegexOptions.Compiled);//视频分辨率
        Regex regexWH = new Regex(@", (?<w>[1-9]\d+)x(?<h>[1-9]\d+)(?: \[SAR \d+:\d+ DAR (?<darw>\d+):(?<darh>\d+)\])?\W+", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        Regex regexCrop = new Regex(@"crop=(?<w>\d+):(?<h>\d+):(?<x>\d+):(?<y>\d+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);//剪裁坐标
        Regex regexTime = new Regex(@"time=(?<t>(?:\d{2}:)?\d{2}:\d{2}:\d{2}\.\d{2})", RegexOptions.Compiled);//编码进度
        Regex regex隔行扫描 = new Regex(@"(top|bottom)\s+first", RegexOptions.IgnoreCase | RegexOptions.Compiled);//交错视频

        Regex regex视轨 = new Regex("Stream #0.+?Video: (?<vcode>[^,]+),(.+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        Regex regex音轨 = new Regex("Stream #0.+?Audio:(.+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        Regex regex字幕 = new Regex("Stream #0.+?Subtitle:(.+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        Regex regexP = new Regex(@"(?<=^|[\W_]\b)(\d{2,4}[pi]|\d{1,2}(?:\.\d)?K)(?=[\W_]|$\b)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        Regex regexFFmpeg命令行中载入文件 = new Regex(@"(?:\s-i\s+""(?<file>[^""]+?)""\s+-[a-z]+\s+)|(?:\s-i\s+(?<file>[\S]+)\s+-[a-z]+\s+)", RegexOptions.IgnoreCase);

        FileInfo _fi输入文件;
        public FileInfo fi输入文件 => _fi输入文件;

        public static readonly string str输出格式 = ".mkv";
        public static readonly string name工作文件夹 = "正在转码……", name完成文件夹 = "转码完成", name源文件夹 = "源视频", name跳过文件夹 = "跳过转码";

        StringBuilder builder日志 = new StringBuilder( );


        List<string> list_音轨 = new List<string>( );

        TimeSpan span进度 = TimeSpan.Zero;
        Stopwatch stopwatch = new Stopwatch( );

        float outWidth = 0, outHeight = 0, cropWidth = 0, cropHeight = 0;//剪裁后的长宽用浮点数据类型，方便计算比例
        float tbr_in = 0, tbr_out = -1;

        int darW = 0, darH = 0, inWidth = 0, inHeight = 0, keyint = 0;

        bool b单线程 = false;

        bool b可变帧率 = false, b发生缩放 = false, b畸变 = false, b发生自动剪裁 = false, b时间剪裁 = false, b跳过编码 = false, b保存日志 = true, b出新帧 = false, b隔行扫描 = false, b有音轨 = false, b有字幕 = false, b有视轨 = false, b硬字幕 = false;

        string str线程, str剪裁参数, str缩放参数, str字幕 = string.Empty, str输出文件 = string.Empty, str日志类型 = string.Empty, str编码摘要 = string.Empty, str开工日志 = string.Empty;

        string str路径_转码输出;
        string vcode = string.Empty;

        string str信息输出 = string.Empty, str编码输出 = string.Empty;

        string get输出Progressive {
            get {
                float f输出长边 = outWidth > outHeight ? outWidth : outHeight;
                float K = f输出长边 / 1000;
                string progressive = string.Empty;
                if (K > 4.32) {
                    progressive = $"{f输出长边 / 960:F1}K";
                } else {
                    if (f输出长边 >= 3840) progressive = "2160p";
                    else if (f输出长边 >= 2560) progressive = "1440p";
                    else if (f输出长边 >= 1920) progressive = "1080p";
                    else if (f输出长边 >= 1440) progressive = "960p";
                    else if (f输出长边 >= 1280) progressive = "720p";
                    else if (f输出长边 >= 960) progressive = "640p";
                    else if (f输出长边 >= 640) progressive = "480p";
                    else if (f输出长边 >= 480) progressive = "320p";
                    else if (f输出长边 >= 320) progressive = "240p";
                    else if (f输出长边 >= 240) progressive = "180p";
                    else if (f输出长边 >= 180) progressive = "135p";
                    else if (f输出长边 >= 120) progressive = "90p";
                    else if (f输出长边 >= 64) progressive = "48p";
                    else progressive = $"{f输出长边 * 9 / 16}p";
                }
                return progressive;
            }
        }

        string str预计剩余(TimeSpan ts剩余) {
            if (ts剩余.Ticks > 0) {
                if (ts剩余.TotalDays > 1)
                    return $"（⏱{ts剩余.Days}天{ts剩余.Hours}小时）";
                else if (ts剩余.TotalHours > 1)
                    return $"（⏱{ts剩余.TotalHours:F1}小时）";
                else
                    return $"（⏱{ts剩余.Minutes}分{ts剩余.Seconds}秒）";
            } else return string.Empty;
        }

        string _str进度 = string.Empty;
        public string str信息 {
            get {
                if (!b等待文件归档) return str信息输出;
                else if (!b出新帧) {
                    if (_str进度 != string.Empty)
                        return _str进度;
                    else
                        return fi输入文件.Name + "\r\n" + str信息输出;
                } else b出新帧 = false;

                Match mt = regexTime.Match(str编码输出);
                if (mt.Success) {//可变帧率视频只能用时间来计算进度。
                    span进度 = TimeSpan.Parse(mt.Groups["t"].Value); //进度是已编码时长，剪裁片头不影响进度时间。
                } else if (f编码帧 > 0) {
                    span进度 = TimeSpan.FromSeconds(f编码帧 / tbr_out);//转vfr时，会丢弃部分帧，算进度会偏少一丢丢。
                }

                if (span进度 <= TimeSpan.Zero) return $"{fi输入文件.Name}\r\n{str编码输出}";

                float pct = (float)span进度.Ticks / vTime.span编码.Ticks;
                if (stopwatch.ElapsedMilliseconds > 0) {
                    TimeSpan ts预计 = TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds / span进度.TotalMilliseconds * vTime.span编码.Subtract(span进度).TotalMilliseconds);
                    //ElapsedMilliseconds 长整数有丢失精度、溢出问题，需要使用双精度小数计算毫秒。

                    return str信息输出 = $"{pct:P2}{str预计剩余(ts预计)} {fi输入文件.Name}\r\n{str编码输出}";

                } else {
                    return str信息输出 = $"{pct:P2} {fi输入文件.Name}\r\n{str编码输出}";
                }
            }
        }

        float f编码帧 = 0;
        int index_frame = -1;
        public double getFPS {
            get {
                if (str编码输出.Length < 36) return 0;
                float sec = stopwatch.ElapsedMilliseconds / 1000;
                if (sec <= 0) { return 0; }
                byte i = (byte)index_frame;
                int frame = 0;
                for (; i < str编码输出.Length; i++) { //开头可能有空格，先找到数字开头。
                    if (str编码输出[i] > '0' && str编码输出[i] <= '9') {
                        frame = str编码输出[i] - 48;
                        break;
                    }
                }
                for (i++; i < str编码输出.Length; i++) {
                    if (str编码输出[i] >= '0' && str编码输出[i] <= '9') {
                        frame = frame * 10 + str编码输出[i] - 48;
                    } else
                        break;//非数字结尾退出
                }
                if (frame > 0) {
                    f编码帧 = frame;
                    return frame / sec;
                }
                return 0;
            }
        }


        bool _b已结束 = false;
        public bool b已结束 => _b已结束;

        public bool b等待文件归档 = false;

        截取时间 vTime;
        X265Params userX265Params;
        FFmpegParams ffmpegParams;

        public RunFFmpeg( ) {
        }

        public RunFFmpeg(FileInfo fi输入文件, FFmpegParams ffmpegParams) {
            stopwatch.Start( );
            b等待文件归档 = true;

            _fi输入文件 = fi输入文件;
            b可变帧率 = ffmpegParams.vfr;
            this.ffmpegParams = ffmpegParams;
            b单线程 = ffmpegParams.oneThread;

            str线程 = b单线程 ? "-threads 1 -filter_threads 1 -filter_complex_threads 1 " : "";
        }
        public void fn后台等待(Process process) {
            str信息输出 = process.StartInfo.Arguments;
            string wmiQuery = string.Format("select CommandLine from Win32_Process where ProcessID={0}", process.Id);
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wmiQuery);
            ManagementObjectCollection retObjectCollection = searcher.Get( );
            foreach (ManagementObject retObject in retObjectCollection)
                str信息输出 += retObject["CommandLine"];

            //Match matchInput = regexFFmpeg命令行中载入文件.Match(str信息输出);
            //if (matchInput.Success) {                str信息输出 = $"现有ffmpeg {matchInput.Groups["file"].Value}";            }
            th后台线程 = new Thread(new ParameterizedThreadStart(fx后台等待));
            th后台线程.IsBackground = true;
            th后台线程.Start(process);
        }
        public void X265转码(X265Params userX265Params) {
            str日志类型 = "x265转码";
            this.userX265Params = userX265Params;

            编码队列.x截取时间表.addFromDirectory(fi输入文件.DirectoryName, true);
            vTime = 编码队列.x截取时间表.hasTime(fi输入文件);

            th后台线程 = new Thread(fnX265编码走起);
            th后台线程.Name = str日志类型 + fi输入文件.Name;
            th后台线程.IsBackground = true;
        }
        public void 复制数据流(bool b保存日志) {
            this.b保存日志 = b保存日志;
            str日志类型 = "混流";
            th后台线程 = new Thread(fn后台混流);
            th后台线程.IsBackground = true;
        }
        public bool find最新版ffmpeg(ref DateTime date上次查找) {
            if (Math.Abs(DateTime.Now.Subtract(date上次查找).TotalSeconds) > 60) {
                date上次查找 = DateTime.Now;
                string[] exeFiles = null;
                try {
                    exeFiles = Directory.GetFiles(".\\", "*ffmpeg*.exe", SearchOption.TopDirectoryOnly);
                } catch { }
                if (exeFiles != null && exeFiles.Length > 0) {
                    FileInfo fi时间最近 = null;
                    int i = 0;
                    for (; i < exeFiles.Length; i++) {
                        FileInfo fi = new FileInfo(exeFiles[i]);
                        if (fi.Length > 52428800) {
                            fi时间最近 = fi;
                            break;
                        }
                    }
                    for (; i < exeFiles.Length; i++) {
                        FileInfo fi = new FileInfo(exeFiles[i]);
                        if (fi.Length > 52428800 && fi.LastWriteTime > fi时间最近.LastWriteTime) {
                            fi时间最近 = fi;
                        }
                    }
                    string temp = fi时间最近.Name.Substring(0, fi时间最近.Name.Length - 4);
                    if (try程序可运行(temp, "ffmpeg")) {
                        _ffmpeg = temp;
                        return true;
                    } else {
                        _ffmpeg = fi时间最近.Name;
                        return try程序可运行(_ffmpeg, "ffmpeg");
                    }
                } else {
                    if (try程序可运行("ffmpeg", "ffmpeg")) {
                        _ffmpeg = "ffmpeg";
                        return true;
                    }
                }
            } else
                return true;
            return false;
        }
        public string find可执行ffprobe( ) {
            if (Math.Abs(DateTime.Now.Subtract(time上次查找ffprobe).TotalSeconds) < 60) {
                if (_ffprobe == "ffprobe" && File.Exists("ffprobe.exe")) return "ffprobe";
                else if (File.Exists(_ffprobe)) return _ffprobe;
            }

            time上次查找ffprobe = DateTime.Now;
            string[] exeFiles = null;

            if (try程序可运行("ffprobe", "ffprobe")) {
                _ffprobe = "ffprobe";
                return _ffprobe;
            }
            try {
                exeFiles = Directory.GetFiles(".\\", "*ffprobe*.exe", SearchOption.TopDirectoryOnly);
            } catch { }

            for (int i = 0; i < exeFiles.Length; i++) {
                FileInfo fi = new FileInfo(exeFiles[i]);
                if (fi.Length > 52428800) {
                    if (try程序可运行(fi.Name, "ffprobe")) {
                        _ffprobe = fi.Name;
                        return _ffprobe;
                    }
                }
            }

            return string.Empty;
        }

        static bool try程序可运行(string exe, string 开头) {
            using (Process process = new Process( )) {
                process.StartInfo.FileName = exe;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardError = true;
                try { process.Start( ); } catch { return false; }
                string err = process.StandardError.ReadToEnd( );
                if (err.StartsWith(开头) && err.Contains("libavcodec")) {
                    return true;
                }
            }
            return false;
        }

        void fn后台混流( ) {
            str信息输出 = "混流中……";
            str输出文件 = $"{fi输入文件.Name}丨混流.{DateTime.Now:yy.MM.dd.HH.mm.ss}{str输出格式}";
            string str完整命令行 = $"-i \"{fi输入文件.Name}\" -c copy \"{name工作文件夹}\\{str输出文件}\"";
            bool b运行结束 = fx后台编码(str完整命令行, out int i退出代码);
            if (b运行结束) fx文件归档(i退出代码, b判断时长: false);

            b等待文件归档 = false;
            _b已结束 = true;
        }
        void fnX265编码走起( ) {
            str信息输出 = "Running……";

            fx信息匹配( );
            fx字幕匹配( );

            if (Setting.b自动剪裁黑边) {
                b发生自动剪裁 = fx扫描黑边(out str剪裁参数);
            }

            if (b发生自动剪裁 || ffmpegParams.set_Scale) {//先进入缩放函数，获取 outW、outH结果
                b发生缩放 = ffmpegParams.scale(inWidth, inHeight, darW, darH, ref outWidth, ref outHeight, out str缩放参数);
            }
            if (cropWidth > 0 && cropHeight > 0)
                b畸变 = (1 - Math.Abs(outWidth / outHeight - cropWidth / cropHeight) < FFmpegParams.f看不出畸变);
            else
                b畸变 = (1 - Math.Abs(outWidth / outHeight - 1.0f * inWidth / inHeight) < FFmpegParams.f看不出畸变);

            if (Setting.b跳过更高阶编码) {
                if (!ffmpegParams.b_add_lavfi_set && ffmpegParams.crf > 0 && !userX265Params.keyintSet && !b隔行扫描 && !b发生自动剪裁 && !b发生缩放 && !b时间剪裁 && !b硬字幕 && !(b可变帧率 && !fi输入文件.Name.Contains("vfr"))) {
                    //挂了外部滤镜、固定了关键帧间距，转无损、恒定帧率转可变帧率 再次编码
                    b跳过编码 = (vcode.Contains("265") || vcode.Contains("hevc") || vcode.Contains("hev1") || vcode.Contains("hvc1") || vcode.Contains("av1") || vcode.Contains("vvc1") || vcode.Contains("vvc") || vcode.Contains("avs3"));
                }
            } else if (!b有视轨) {
                b跳过编码 = true;
            }

            if (b跳过编码) {
                str信息输出 = "Skiping……";
                fx跳过HEVC转码文件移动( );
            } else {
                string str完整命令行 = fx参数拼接( );
                str信息输出 = vTime.b剪裁片头 ? "Seeking……" : "Encoding……";
                fx后台编码(str完整命令行, out int i退出代码);
                str信息输出 = "Finishing……";
                fx文件归档(i退出代码, b判断时长: true);
            }

            b等待文件归档 = false;
            _b已结束 = true;
        }



        void fx信息匹配( ) {
            bool b_ffprobe;
            string exe, commamd;
            int scan_frame = 60;
            exe = find可执行ffprobe( );

            if (Setting.b以帧识别隔行扫 && exe != string.Empty) {//“ffprobe.exe”不是长时间运行的文件，每次运行检查一下；
                str信息输出 = "扫描交错帧……";
                b_ffprobe = true;
                exe = _ffprobe;
                commamd = $"\"{fi输入文件.Name}\" -select_streams v -read_intervals \"%+#{scan_frame}\" -show_entries \"frame=interlaced_frame\"";
            } else {
                str信息输出 = "读取编码信息……";
                b_ffprobe = false;
                exe = _ffmpeg;
                commamd = string.Format("-i \"{0}\"", fi输入文件.Name);
            }
            FormMain.EventShowLogs.Set( );
            using (Process process = new Process( )) {
                process.StartInfo.FileName = exe;
                process.StartInfo.Arguments = commamd;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.StandardErrorEncoding = Encoding.UTF8;
                process.StartInfo.WorkingDirectory = fi输入文件.DirectoryName;

                try {
                    process.Start( );//启动程序
                } catch (Exception err) {
                    str信息输出 = err.Message;
                    return;
                }
                if (b_ffprobe) {
                    process.OutputDataReceived += new DataReceivedEventHandler(ffprobe_OutInfo);
                    process.BeginOutputReadLine( );
                }

                List<string> list轨道 = new List<string>( );
                string str时长 = string.Empty;

                while (!process.StandardError.EndOfStream) {
                    str信息输出 = process.StandardError.ReadLine( ).Trim( );
                    if (str信息输出.StartsWith("Stream #0", StringComparison.OrdinalIgnoreCase)) {
                        list轨道.Add(str信息输出);
                    } else if (str信息输出.StartsWith("Duration: ", StringComparison.OrdinalIgnoreCase)) {
                        str时长 = str信息输出;
                    }
                }

                if (b_ffprobe) {
                    process.WaitForExit( );
                    b隔行扫描 = scan_frame - sum_interlaced_frame <= sum_interlaced_frame;
                }

                for (int i = 0; i < list轨道.Count; i++) {
                    if (list轨道[i].Contains("Video:")) {
                        b有视轨 = true;
                        Match matchV = regex视轨.Match(list轨道[i]);
                        string vInfo = matchV.Groups[1].Value;
                        if (string.IsNullOrEmpty(vInfo)) vInfo = list轨道[i];

                        vcode = matchV.Groups["vcode"].Value;

                        if (regexTBR.IsMatch(vInfo)) {
                            if (float.TryParse(regexTBR.Match(vInfo).Groups["tbr"].Value, out float tbr)) {
                                if (tbr > tbr_in) tbr_in = tbr;
                            }
                        } else if (regexFPS.IsMatch(vInfo)) {
                            if (float.TryParse(regexFPS.Match(vInfo).Groups["fps"].Value, out float fps)) {
                                if (fps > tbr_in) tbr_in = fps;
                            }
                        }
                        Match matchWH = regexWH.Match(vInfo);
                        if (int.TryParse(matchWH.Groups["w"].Value, out int w)) {
                            if (w > inWidth) { inWidth = w; outWidth = w; }
                        }
                        if (int.TryParse(matchWH.Groups["h"].Value, out int h)) {
                            if (h > inHeight) { inHeight = h; outHeight = h; }
                        }
                        if (int.TryParse(matchWH.Groups["darw"].Value, out int dw)
                            && int.TryParse(matchWH.Groups["darh"].Value, out int dh)) {//用于自动校正显示宽高
                            darW = dw;
                            darH = dh;
                        }
                        if (regex隔行扫描.IsMatch(vInfo)) {//交错视频优先使用帧上的信息，其次参考封包信息
                            b隔行扫描 = true;
                        }
                    } else if (list轨道[i].Contains("Audio:")) {
                        b有音轨 = true;
                        list_音轨.Add(regex音轨.Match(list轨道[i]).Groups[1].Value);
                    } else if (!b有字幕 && regex字幕.IsMatch(list轨道[i])) {
                        b有字幕 = true;
                    }
                }

                if (tbr_in < 1) tbr_in = 24000 / 1001.0f;

                if (b隔行扫描) tbr_out = tbr_in * 2;
                else tbr_out = tbr_in;

                if (userX265Params.keyintSet)
                    keyint = ffmpegParams.gop;
                else if (userX265Params.keyintMax)
                    keyint = int.MaxValue;  //keyint = -1;// ffmpeg -g 参数无法传入-1；
                else
                    keyint = (int)Math.Ceiling(tbr_out * ffmpegParams.gop_sec);

                Match match时长 = regex时长.Match(str时长);
                if (match时长.Success) {
                    vTime.get编码时长(TimeSpan.Parse(match时长.Groups[1].Value));
                }
                b时间剪裁 = vTime.b剪裁片头 || vTime.b剪裁片尾;
            }
        }
        bool fx时长匹配(FileInfo fi, out TimeSpan span时长) {
            span时长 = TimeSpan.Zero;
            string exe = _ffprobe == string.Empty ? _ffmpeg : _ffprobe;
            if (exe == string.Empty) exe = _ffmpeg;
            using (Process process = new Process( )) {
                process.StartInfo.FileName = exe;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.Arguments = $"-i \"{fi.Name}\"";
                process.StartInfo.WorkingDirectory = fi.DirectoryName;
                process.StartInfo.StandardErrorEncoding = Encoding.UTF8;
                try {
                    process.Start( );
                } catch {
                    return false;
                }
                while (!process.StandardError.EndOfStream) {////Error opening input file
                    string line = process.StandardError.ReadLine( ).TrimStart( );

                    if (regex时长.IsMatch(line)) {
                        span时长 = TimeSpan.Parse(regex时长.Match(line).Groups[1].Value);
                        return true;
                    }
                }
            }
            return false;
        }

        void fx字幕匹配( ) {
            string str无后缀文件名 = fi输入文件.Name.Substring(0, fi输入文件.Name.LastIndexOf('.'));
            for (int i = 0; i < sub带特效字幕.Length; i++) {
                string subTitle = string.Format("{0}\\{1}.{2}", fi输入文件.DirectoryName, str无后缀文件名, sub带特效字幕[i]);
                if (File.Exists(subTitle)) {
                    str字幕 = $"subtitles='{str无后缀文件名}.{sub带特效字幕[i]}'";
                    str信息输出 = "渲染高级字幕：" + str字幕;
                    builder日志.AppendLine(str信息输出);
                    b硬字幕 = true;
                    break;
                }
            }
            if (!b硬字幕) {
                string str字幕渲染效果;
                //str字幕渲染效果 = "force_style=FontName=Microsoft YaHei UI Bold,Outline=0.2,Shadow=0.25,Spacing=0.5";
                str字幕渲染效果 = "force_style=FontName=阿里巴巴普惠体 3 75 SemiBold,FontSize=20,Outline=0.2,Shadow=0.25,Spacing=0.5";
                for (int i = 0; i < sub文字字幕.Length; i++) {
                    string subTitle = string.Format("{0}\\{1}.{2}", fi输入文件.DirectoryName, str无后缀文件名, sub文字字幕[i]);
                    if (File.Exists(subTitle)) {
                        str字幕 = $"subtitles='{str无后缀文件名}.{sub文字字幕[i]}:{str字幕渲染效果}'";
                        str信息输出 = "渲染文字字幕：" + str字幕;
                        builder日志.AppendLine(str信息输出);
                        b硬字幕 = true;
                        break;
                    }
                }
            }
        }

        int sum_interlaced_frame = 0;
        void ffprobe_OutInfo(object sendProcess, DataReceivedEventArgs output) {//标准输出、错误输出似乎共用缓冲区，只读其中一个，输出缓冲区可能会满，卡死
            if (output.Data != null) {
                if (output.Data == "interlaced_frame=1") {
                    sum_interlaced_frame++;
                }
            }
        }

        bool fx扫描黑边(out string str剪裁黑边) {
            str剪裁黑边 = string.Empty;

            if (!Setting.b自动剪裁黑边) return false;

            Dictionary<string, int> crops = new Dictionary<string, int>( );
            List<string> list = new List<string>( );

            str信息输出 = "扫描黑边中……";
            FormMain.EventShowLogs.Set( );

            int iStarSec = (int)vTime.time编码开始.TotalSeconds;
            int endSec = (int)vTime.time编码结束.TotalSeconds;

            if (vTime.span编码.TotalSeconds > 1000) {
                if (!vTime.b剪裁片头) iStarSec += 250;
                if (!vTime.b剪裁片尾) endSec -= 250;
                for (float ss = iStarSec; ss < endSec; ss += 250) {
                    str信息输出 = string.Format("扫描黑边 {0:F2}%", 100 * ss / endSec);
                    if (Setting.b自动剪裁黑边) seekCropdetect(ss, 5, ref list);
                    else return false;//中途改变设置的话，立刻跳出。
                }
            } else if (vTime.span编码.TotalSeconds > 60) {
                int step = (int)(vTime.span编码.TotalSeconds / 9);
                if (!vTime.b剪裁片头) iStarSec += 30;
                if (!vTime.b剪裁片尾) endSec -= 5;
                for (float ss = iStarSec; ss < endSec; ss += step) {
                    str信息输出 = string.Format("扫描黑边 {0:F2}%", 100 * ss / endSec);
                    if (Setting.b自动剪裁黑边) seekCropdetect(ss, 5, ref list);
                    else return false;//中途改变设置的话，立刻跳出。
                }
            } else {
                seekCropdetect((int)vTime.span编码.TotalSeconds / 3, endSec * 2 / 3, ref list);
            }
            for (int i = 0; i < list.Count; i++) {
                Match mCrop = regexCrop.Match(list[i]);
                if (mCrop.Success) {
                    string crop = mCrop.Value;
                    if (crops.ContainsKey(crop))
                        crops[crop]++;
                    else
                        crops.Add(crop, 1);

                }
            }
            KeyValuePair<string, int> mostCrop = new KeyValuePair<string, int>( );
            KeyValuePair<string, int> secondCrop = new KeyValuePair<string, int>( );

            if (crops.Count > 0) secondCrop = crops.Last( );

            foreach (KeyValuePair<string, int> kvp in crops)
                if (mostCrop.Value < kvp.Value) {
                    secondCrop = mostCrop;
                    mostCrop = kvp;
                } else if (secondCrop.Value < kvp.Value) {
                    secondCrop = kvp;
                }

            if (mostCrop.Key != null) {
                Match matchCrop = regexCrop.Match(mostCrop.Key);
                //int crop_y = int.Parse(matchCrop.Groups["y"].Value);
                //int crop_x = int.Parse(matchCrop.Groups["x"].Value);
                float crop_height = float.Parse(matchCrop.Groups["h"].Value);
                float crop_width = float.Parse(matchCrop.Groups["w"].Value);

                if (secondCrop.Key != null && secondCrop.Value >= 5 * tbr_in) {//备选裁剪尺寸，当备选剪裁尺寸大于首选，启用备选尺寸。
                    Match matchCrop_2 = regexCrop.Match(secondCrop.Key);
                    float crop_2_Height = float.Parse(matchCrop_2.Groups["h"].Value);
                    float crop_2_width = float.Parse(matchCrop_2.Groups["w"].Value);
                    if (crop_2_width >= crop_width && crop_2_Height >= crop_height) {
                        mostCrop = secondCrop;
                        crop_width = crop_2_width;
                        crop_height = crop_2_Height;
                    }
                }
                if ((crop_width >= 64 && crop_width != inWidth) || (crop_height >= 64 && crop_height != inHeight)) {
                    str剪裁黑边 = mostCrop.Key;
                    outHeight = crop_height;
                    outWidth = crop_width;
                    cropWidth = crop_width;
                    cropHeight = crop_height;
                    return true;
                }
            }

            return false;

        }

        void seekCropdetect(float ss, int t, ref List<string> lines) {
            using (Process process = new Process( )) {
                process.StartInfo.FileName = _ffmpeg;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.StandardErrorEncoding = Encoding.UTF8;
                process.StartInfo.WorkingDirectory = fi输入文件.DirectoryName;

                process.StartInfo.Arguments = $"{str线程}-ss {ss} -i \"{fi输入文件.Name}\" -t {t} -vf cropdetect=round={userX265Params.round} -f null -an /dev/null";

                try {
                    process.Start( );//启动程序
                    process.PriorityClass = ProcessPriorityClass.BelowNormal;
                } catch (Exception err) {
                    str信息输出 = err.Message;
                    return;
                }
                while (!process.StandardError.EndOfStream) {
                    string line = process.StandardError.ReadLine( );
                    if (line.StartsWith("[Parsed_cropdetect"))
                        lines.Add(line);
                }
            }
        }


        string fx参数拼接( ) {
            string g = (userX265Params.keyintMax || keyint == userX265Params.def_keyint || keyint < 1) ? string.Empty : string.Format(" -g {0}", ffmpegParams.crf == 0 ? 1 : keyint);
            //关键帧间隔 g 0传入黑屏，最大关键字g -1无法传入，
            fx滤镜拼接(out string arg);

            arg += string.Format(" -c:v libx265{0} -crf {1} -pix_fmt yuv420p10le{2}{3}",
                ffmpegParams.x26n_preset,
                ffmpegParams.crf,
                userX265Params.getParams(keyint, ffmpegParams.crf, Math.Ceiling(tbr_out)),
                g
                );

            if (b有音轨) {
                if (ffmpegParams.acopy) {
                    arg += string.Format(" -map 0:a{0} -acodec copy", ffmpegParams.map0a ? "" : ":0");
                } else if (ffmpegParams.map0a) {
                    arg += " -map 0:a";
                    bool b音轨格式相同 = true, b音轨声道相同 = true;
                    for (int a = 0; a < list_音轨.Count; a++) {
                        if (!list_音轨[a].Contains(ffmpegParams.acodec)) {
                            b音轨格式相同 = false;
                        }
                        if (!list_音轨[a].Contains(ffmpegParams.channel)) {
                            b音轨声道相同 = false;
                        }
                    }
                    if (b音轨格式相同 && b音轨声道相同) {
                        arg += " -acodec copy";//源音轨和输出相同属性，直接复制。
                    } else {
                        arg += " -acodec " + ffmpegParams.acodec;

                        if (!b音轨声道相同)
                            arg += ffmpegParams.ac;
                    }
                } else {
                    arg += " -map 0:a:0";
                    if (list_音轨.Count > 0 && list_音轨[0].Contains(ffmpegParams.acodec) && list_音轨[0].Contains(ffmpegParams.channel)) {
                        arg += " -acodec copy";//源音轨和输出相同属性，直接复制。
                    } else {
                        arg += " -acodec " + ffmpegParams.acodec;

                        if (!list_音轨[0].Contains(ffmpegParams.channel))
                            arg += ffmpegParams.ac;
                    }
                }
            }

            if (ffmpegParams.map0s) {
                if (b有字幕) {
                    arg += " -map 0:s -c:s copy";
                }
            } else {
                arg += " -sn";
            }

            str编码摘要 = $"{vTime.str剪裁Title}x265.crf{ffmpegParams.crf}.{userX265Params.preset}";
            if (ffmpegParams.vfr) str编码摘要 += ".vfr";
            if (b硬字幕) str编码摘要 += ".硬字幕";


            string outputName = fi输入文件.Name;
            if (b发生缩放) {
                outputName = regexP.Replace(outputName, get输出Progressive);
            }
            str输出文件 = $"{outputName}丨{str编码摘要}.{DateTime.Now:yy.MM.dd.HH.mm.ss}{str输出格式}";


            string str载入 = $"-i \"{fi输入文件.Name}\"";
            string str参数 = $"{vTime.getCMD}{arg}";

            string str完整命令行 = $"{str线程}{str载入} {str参数} \"正在转码……\\{str输出文件}\"";

            // -ss 写在 -i之前ffpmeg用关键帧跳转，启动速度较快，结果开头会停滞几帧。存在关键帧时间戳到输入时间戳的空白。
            //-ss写在-i之后，会逐帧解码到输入时间戳，精确到毫秒分割。

            builder日志.AppendLine(DateTime.Now.ToString( )).AppendLine(fi输入文件.FullName).AppendLine(str参数).AppendLine( );

            return str完整命令行;
        }

        bool fx滤镜拼接(out string vf) {
            vf = string.Empty;
            List<string> listFilter = new List<string>( );

            if (Setting.b自动反交错 && b隔行扫描) {
                listFilter.Add("bwdif=1:-1:1");//交错视频，逐帧反交错。
                //listFilter.Add("bwdif=0:-1:0");//交错视频，每场混合为一帧。
                str信息输出 = "反交错帧：bwdif=1:-1:1";
                builder日志.AppendLine(str信息输出);
            }

            if (b发生自动剪裁) {
                str信息输出 = "自动剪裁：" + str剪裁参数;
                builder日志.AppendLine(str信息输出);
                listFilter.Add(str剪裁参数);
            }



            if (b畸变) {
                if (b发生缩放) {
                    str信息输出 = "缩放参数：" + str缩放参数;
                    builder日志.AppendLine(str信息输出);
                    listFilter.Add(str缩放参数);
                }
                if (b硬字幕) {//如果是修正畸变缩放，先渲染字幕会被拉变形。
                    listFilter.Add(str字幕);
                }
            } else {
                if (b硬字幕) {//如果视频比例无调整，先渲染硬字幕，后续触发缩小，可以获得更平滑字幕边缘。
                    listFilter.Add(str字幕);
                }
                if (b发生缩放) {
                    str信息输出 = "缩放参数：" + str缩放参数;
                    builder日志.AppendLine(str信息输出);
                    listFilter.Add(str缩放参数);
                }
            }

            if (b可变帧率) {
                listFilter.Add("mpdecimate");//把检查重复帧功能放到最后，比较最终画面
            }//可能存在长时间固定相同帧，只有时间码的空帧无法渲染硬字幕。

            //listFilter.Add("unsharp=5:5:-0.01:5:5:0.0");//反锐化滤镜

            if (ffmpegParams.b_add_lavfi && !string.IsNullOrEmpty(ffmpegParams.str_add_lavfi)) {
                listFilter.Add(ffmpegParams.str_add_lavfi);
            }

            if (listFilter.Count > 0) {
                vf = " -lavfi \"" + listFilter[0];
                for (int v = 1; v < listFilter.Count; v++)
                    vf += "," + listFilter[v];
                vf += "\"";

                if (b可变帧率) vf += " -fps_mode vfr";//有了可变帧率滤镜，listFilter.Count > 0 成立

            } else
                vf = " -map 0:v:0";

            if (!b可变帧率 && !b隔行扫描) {
                vf += " -fps_mode passthrough";//复制时间码，切片合并缩小帧量误差。
                //                passthrough(0)
                //Each frame is passed with its timestamp from the demuxer to the muxer.

                //cfr(1)
                //Frames will be duplicated and dropped to achieve exactly the requested constant frame rate.

                //vfr(2)
                //Frames are passed through with their timestamp or dropped so as to prevent 2 frames from having the same timestamp.

                //auto(-1)//有些源中缺失vfr、cfr信息，切片编码会多帧、少帧。
                //Chooses between cfr and vfr depending on muxer capabilities.This is the default method.
            }
            return listFilter.Count > 0;
        }

        bool fx后台编码(string str完整命令行, out int i退出代码) {
            i退出代码 = -1;
            using (Process process = new Process( )) {
                str路径_转码输出 = $"{fi输入文件.DirectoryName}\\{name工作文件夹}";
                if (!Directory.Exists(str路径_转码输出)) Directory.CreateDirectory(str路径_转码输出);

                process.StartInfo.FileName = _ffmpeg;
                process.StartInfo.Arguments = str完整命令行;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.StandardErrorEncoding = Encoding.UTF8;
                process.StartInfo.WorkingDirectory = fi输入文件.DirectoryName;

                try {
                    process.Start( );//启动程序
                    //process.BeginErrorReadLine();//System.InvalidOperationException:“不能在进程流中混合同步操作和异步操作。”
                    process.PriorityClass = ProcessPriorityClass.Idle;
                } catch (Exception err) {
                    builder日志.AppendLine(err.Message);
                    编码队列.ffmpeg主动移除结束(this);
                    _b已结束 = true;
                    return false;
                }
                while (!process.StandardError.EndOfStream) {
                    string line = process.StandardError.ReadLine( );//载入信息、头信息不显示在界面上。
                    if (!string.IsNullOrEmpty(line)) {
                        int iframe = line.IndexOf("frame=") + 6;
                        if (iframe >= 6) {
                            index_frame = iframe;
                            str编码输出 = line;
                            b出新帧 = true;
                            FormMain.EventShowLogs.Set( );//开始出帧则刷新显示表示在运转。
                            break;
                        } else {
                            builder日志.AppendLine(line);
                        }
                    }
                }
                if (!process.HasExited) {
                    str开工日志 = $"{fi输入文件.DirectoryName}\\{name工作文件夹}\\{str输出文件}.log";
                    fx工作日志( );
                }
                while (!process.StandardError.EndOfStream) {
                    string line = process.StandardError.ReadLine( );//编码过程中的信息可以刷新到程序显示。
                    if (line != null) {
                        int iframe = line.IndexOf("frame=") + 6;
                        if (iframe >= 6) {
                            index_frame = iframe;
                            str信息输出 = line;
                            str编码输出 = line;
                            b出新帧 = true;
                        } else {
                            builder日志.AppendLine(line);
                        }
                    }
                }
                编码队列.ffmpeg主动移除结束(this);

                process.WaitForExit( );

                double fps = getFPS;//附带计算编码帧的函数
                builder日志.AppendFormat("\r\n编码完成：{0:F0} （ {1}帧 ÷ {2:F0}秒 = {3:F3}帧/秒 ） \r\n\r\n"
                    , takeUpTime, f编码帧, stopwatch.Elapsed.TotalSeconds, fps);

                i退出代码 = process.ExitCode;
            }
            return true;
        }

        void fx后台等待(object obj) {
            try {
                Process process = (Process)obj;
                process.WaitForExit( );
                process.Dispose( );
            } catch { }
            _b已结束 = true;
            编码队列.ffmpeg主动移除结束(this);
        }

        void fx工作日志( ) {
            if (b保存日志) {
                try {
                    File.WriteAllText(str开工日志, builder日志.ToString( ));
                } catch { }
            }
        }

        void fx文件归档(int i退出代码, bool b判断时长) {
            try { File.Delete(str开工日志); } catch { }

            if (i退出代码 == 0) {
                FileInfo fi编码后视频文件 = new FileInfo($"{str路径_转码输出}\\{str输出文件}");

                bool b时长不变 = false;

                if (b判断时长) {
                    Match mt = regexTime.Match(str编码输出);
                    if (mt.Success) {//可变帧率视频只能用时间来计算进度。
                        span进度 = TimeSpan.Parse(mt.Groups["t"].Value); //进度是已编码时长，剪裁片头不影响进度时间。
                        if (span进度.TotalSeconds + 10 >= vTime.span编码.TotalSeconds) b时长不变 = true;
                    }
                    if (!b时长不变) {
                        if (fi编码后视频文件.Exists && fx时长匹配(fi编码后视频文件, out TimeSpan span结果)) {
                            if (Math.Abs(vTime.span编码.TotalSeconds - span结果.TotalSeconds) < 10) {
                                b时长不变 = true;
                            }
                        }
                    }
                    builder日志.Append("源视频体积：").Append(fi输入文件.Length / 1024 / 1024).AppendLine("MB");
                    builder日志.Append("编码后体积：").Append(fi编码后视频文件.Length / 1024 / 1024).AppendLine("MB");
                    builder日志.Append("整体压缩率：").AppendFormat("{0:P2}\r\n\r\n", 1.0d * fi编码后视频文件.Length / fi输入文件.Length);
                }

                if (!b判断时长 || b时长不变) {
                    string str日志路径;
                    if (Setting.b完成后删除源视频) {
                        str日志路径 = $"{fi输入文件.DirectoryName}";
                        try {
                            fi编码后视频文件.MoveTo($"{fi输入文件.DirectoryName}\\{str输出文件}");
                        } catch (Exception e) {
                            builder日志.AppendLine("移动完成文件失败： ").Append(e.Message);
                        }
                        try { fi输入文件.Delete( ); } catch (Exception e) { builder日志.AppendLine("删除源视频失败： ").Append(e.Message); }
                    } else {//
                        str日志路径 = $"{fi输入文件.DirectoryName}\\{name完成文件夹}";
                        if (!Directory.Exists(str日志路径)) try { Directory.CreateDirectory(str日志路径); } catch { }

                        try {
                            File.Move($"{str路径_转码输出}\\{str输出文件}", $"{str日志路径}\\{str输出文件}");
                        } catch (Exception e) {
                            builder日志.AppendLine("移动完成文件失败： ").Append(e.Message);
                        }


                        string str源 = $"{fi输入文件.DirectoryName}\\{name源文件夹}";
                        if (!Directory.Exists(str源)) Directory.CreateDirectory(str源);
                        try { fi输入文件.MoveTo($"{str源}\\{fi输入文件.Name}"); } catch { }
                    }

                    if (b保存日志) {
                        string str完成日志 = $"{str日志路径}\\{str输出文件}.log";
                        try { File.WriteAllText(str完成日志, builder日志.ToString( )); } catch { }
                    }

                } else {//时长校对有差异
                    DirectoryInfo di时长变短 = new DirectoryInfo($"{fi输入文件.DirectoryName}\\时长变短");
                    if (!di时长变短.Exists) try { di时长变短.Create( ); } catch { }
                    try { File.Move($"{str路径_转码输出}\\{str输出文件}", $"{di时长变短.FullName}\\{str输出文件}"); } catch { }
                    try { fi输入文件.MoveTo($"{di时长变短.FullName}\\{fi输入文件.Name}"); } catch { }

                    builder日志.AppendLine("结果时长与源视有差异： ");
                    string logPath = $"{fi输入文件.DirectoryName}\\{str日志类型}【时长变短】.{str输出文件}.log";
                    try { File.WriteAllText(logPath, builder日志.ToString( )); } catch { }
                }

                if (Directory.GetFiles(str路径_转码输出).Length == 0 && Directory.GetDirectories(str路径_转码输出).Length == 0) {
                    try { Directory.Delete(str路径_转码输出); } catch { }
                }
            } else {//意外退出
                builder日志.AppendLine("异常退出！退出代码：").Append(i退出代码);
                string logPath = $"{fi输入文件.DirectoryName}\\{str日志类型}【中断】.{str输出文件}.log";
                try { File.WriteAllText(logPath, builder日志.ToString( )); } catch { }
            }
        }

        void fx跳过HEVC转码文件移动( ) {
            编码队列.ffmpeg主动移除结束(this);
            if (!Setting.b完成后删除源视频) {//删除源 = 同文件夹自动处理功能。
                string str跳过转码 = $"{fi输入文件.DirectoryName}\\{name跳过文件夹}";
                string str跳过源 = str跳过转码 + "\\" + fi输入文件.Name;
                try {
                    if (!Directory.Exists(str跳过转码))
                        Directory.CreateDirectory(str跳过转码);
                    _fi输入文件.MoveTo(str跳过源);
                } catch { }
            }
        }

        public string takeUpTime {
            get {
                if (stopwatch.Elapsed > TimeSpan.Zero) {
                    StringBuilder stringBuilder = new StringBuilder("用时");
                    if (stopwatch.Elapsed.Days > 0) stringBuilder.AppendFormat("{0}天", stopwatch.Elapsed.Days);
                    if (stopwatch.Elapsed.Hours > 0) stringBuilder.AppendFormat("{0}小时", stopwatch.Elapsed.Hours);
                    if (stopwatch.Elapsed.Minutes > 0) stringBuilder.AppendFormat("{0}分", stopwatch.Elapsed.Minutes);
                    if (stopwatch.Elapsed.Seconds > 0) stringBuilder.AppendFormat("{0}秒", stopwatch.Elapsed.Seconds);
                    return stringBuilder.Append(" ").ToString( );
                } else
                    return string.Empty;
            }
        }
    }
}
