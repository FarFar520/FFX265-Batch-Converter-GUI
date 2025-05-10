using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using static FFX265_Batch_Converter.JsonVideoStreams;

namespace FFX265_Batch_Converter {
    internal class RunFFprobe {
        public static JavaScriptSerializer js = new JavaScriptSerializer( );
        FileInfo _fi输入文件;
        string _str输出信息 = string.Empty;

        public static readonly string str逐行 = "interlaced_frame=0", str交错 = "interlaced_frame=1";

        public static string _ffprobe = "ffprobe";

        public RunFFprobe(FileInfo fi输入文件) {
            _fi输入文件 = fi输入文件;
        }
        public bool fx读取流JSON信息(out string g) {
            g = string.Empty;
            using (Process process = new Process( )) {
                process.StartInfo.FileName = _ffprobe;
                process.StartInfo.Arguments = string.Format("\"{0}\" -v quiet -print_format json -show_format -show_streams", _fi输入文件);
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                process.StartInfo.WorkingDirectory = _fi输入文件.Directory.FullName;

                string json = string.Empty;
                try {
                    process.Start( );//启动程序
                } catch (Exception err) {
                    _str输出信息 = err.Message;
                }
                json = process.StandardOutput.ReadToEnd( ).Trim( );

                if (json[0] == '{' && json[json.Length - 1] == '}') {
                    JsonVideoStreams rootobject = js.Deserialize<JsonVideoStreams>(json);
                    foreach (VStream v in rootobject.streams) {
                        if (v.codec_type == "video" && !string.IsNullOrEmpty(v.field_order)) {
                            string[] r_frame_rate = v.r_frame_rate.Split('/');

                            break;
                        }
                    }
                } else return false;
            }
            return true;
        }
        public bool fx查60帧判断交错信息( ) {
            int scan_frame = 60;
            using (Process process = new Process( )) {
                process.StartInfo.FileName = _ffprobe;
                process.StartInfo.Arguments = $" \"{_fi输入文件.Name}\" -select_streams v -read_intervals \"%+#{scan_frame}\" -show_entries \"frame=interlaced_frame\"";
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                process.StartInfo.WorkingDirectory = _fi输入文件.Directory.FullName;

                try {
                    process.Start( );//启动程序
                } catch (Exception err) {
                    _str输出信息 = err.Message;
                }

                int sum交错 = 0;
                while (!process.StandardOutput.EndOfStream) {
                    string line = process.StandardOutput.ReadLine( );
                    if (line == str逐行)
                        sum交错++;

                }

                return scan_frame - sum交错 < sum交错;//超过一半的帧是交错符合交错视频
            }
        }
    }
}
