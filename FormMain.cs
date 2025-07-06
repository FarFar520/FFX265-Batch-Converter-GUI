using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace FFX265_Batch_Converter {
    public partial class FormMain: Form {
        public FormMain( ) {
            InitializeComponent( );

            thScan = new Thread(bgScan);
            thScan.IsBackground = true;
            thScan.Start( );
            EventScan.Set( );

            thUpdate = new Thread(bgUpdate);
            thUpdate.IsBackground = true;
            thUpdate.Priority = ThreadPriority.BelowNormal;

            thEncoding = new Thread(bgEncoding);
            thEncoding.IsBackground = true;
        }

        public static AutoResetEvent EventScan = new AutoResetEvent(false), EventEncoding = new AutoResetEvent(false), EventShowLogs = new AutoResetEvent(false);

        Regex regexOutFile = new Regex("丨.*?\\d{2}(\\.\\d{2}){5}", RegexOptions.Compiled);

        X265Params userX265Params = new X265Params( );
        FFmpegParams ffmpegParams = new FFmpegParams( );

        int NumberOfProcessors = 0, NumberOfCores = 0, NumberOfLogicalProcessors = 0, Min_MultiThread_X265 = 1, Max_MultiThread_X265 = 3, SingleThread_X265 = 8;

        List<string> _listScan = new List<string>( );

        HashSet<string> _hashEncoding = new HashSet<string>( );

        HashSet<string> _hash_upOneByOne = new HashSet<string>( );

        Dictionary<string, string>
            _dicUpPath_Name = new Dictionary<string, string>( ),
            _dicUpPath_Origin = new Dictionary<string, string>( ),
            _dicFolders = new Dictionary<string, string>( );

        Dictionary<string, FileSystemWatcher> _dicWatcher = new Dictionary<string, FileSystemWatcher>( );

        Thread thScan, thUpdate, thEncoding;

        object rwlistFiles = new object( );

        string upRunPath = string.Empty;


        HashSet<string> mapVideoExt = new HashSet<string>( ) { ".264", ".265", ".3g2", ".3gp", ".3gp2", ".3gpp", ".amv", ".asf", ".avi", ".cpk", ".dat", ".dirac", ".div", ".divx", ".dpg", ".dv", ".dvr-ms", ".evo", ".f4v", ".flc", ".fli", ".flv", ".h264", ".h265", ".ifo", ".k3g", ".lavf", ".m1v", ".m2t", ".m2ts", ".m2v", ".m4b", ".m4p", ".m4v", ".mkv", ".mod", ".mov", ".mp2v", ".mp4", ".mpe", ".mpeg", ".mpg", ".mpv2", ".mts", ".mxf", ".nrg", ".nsr", ".nsv", ".ogm", ".ogv", ".qt", ".ram", ".rm", ".rmvb", ".rpm", ".skm", ".tp", ".tpr", ".trp", ".ts", ".vob", ".webm", ".wm", ".wmp", ".wmv", ".wtv", ".x264", ".x265" };


        private void OnCreated(object sender, FileSystemEventArgs e) {
            if (addFile(e.FullPath, OneByOne: false)) {
                if (thEncoding.ThreadState == (System.Threading.ThreadState.Background | System.Threading.ThreadState.WaitSleepJoin)) {
                    listBoxFiles.BeginInvoke(new Action(( ) => {
                        listBoxFiles.DataSource = null;
                        listBoxFiles.DataSource = _dicUpPath_Name.Values.ToArray( );
                    }));
                }
            }
        }

        private void OnDeleted(object sender, FileSystemEventArgs e) {
            lock (rwlistFiles) _dicUpPath_Name.Remove(e.FullPath.ToUpper( ));

            listBoxFiles.BeginInvoke(new Action(( ) => {
                listBoxFiles.DataSource = null;
                listBoxFiles.DataSource = _dicUpPath_Name.Values.ToArray( );
            }));
        }
        private void OnRenamed(object sender, RenamedEventArgs e) {
            FileInfo fileInfo = new FileInfo(e.FullPath);
            string ext = fileInfo.Extension.ToLower( );
            string up = e.FullPath.ToUpper( );

            lock (rwlistFiles) _dicUpPath_Name.Remove(e.OldFullPath.ToUpper( ));

            if (mapVideoExt.Contains(ext)) {
                if (!_dicUpPath_Origin.ContainsKey(up))
                    _dicUpPath_Origin.Add(up, e.FullPath);
                else _dicUpPath_Origin[up] = e.FullPath;

                lock (rwlistFiles) { _dicUpPath_Name.Add(up, e.Name); }

            }
            listBoxFiles.BeginInvoke(new Action(( ) => {
                listBoxFiles.DataSource = null;
                listBoxFiles.DataSource = _dicUpPath_Name.Values.ToArray( );
            }));
        }

        bool addFile(string filePath, bool OneByOne) {
            FileInfo fileInfo = new FileInfo(filePath);
            string ext = fileInfo.Extension.ToLower( );
            if (mapVideoExt.Contains(ext)) {
                string up = fileInfo.FullName.ToUpper( );
                if (_hashEncoding.Contains(up)) {
                    return false;
                } else if (regexOutFile.IsMatch(fileInfo.Name)) {
                    _hashEncoding.Add(up);
                    return false;
                }
                if (OneByOne) {
                    _hash_upOneByOne.Add(up);
                }
                bool bNewSrc = false;
                lock (rwlistFiles) {
                    if (!_dicUpPath_Name.ContainsKey(up)) {
                        _dicUpPath_Name.Add(up, fileInfo.Name);
                        bNewSrc = true;

                        if (!_dicUpPath_Origin.ContainsKey(up))
                            _dicUpPath_Origin.Add(up, filePath);
                        else
                            _dicUpPath_Origin[up] = filePath;
                    }
                }
                if (bNewSrc && !OneByOne) 编码队列.Event编码信号.Set( );//OneByOne相当于拖拽添加的文件，拖拽事件收尾有触发一次继续编码信号。
                return true;
            }
            return false;
        }

        bool addFolder(string selectedFolder, bool updateUI) {
            if (addWatcher(selectedFolder, out string upPath, out DirectoryInfo directoryInfo)) {
                if (upPath == upRunPath) return false;

                if (!_dicFolders.ContainsKey(upPath)) {
                    lock (rwlistFiles) {
                        _listScan.Add(selectedFolder);
                        _dicFolders.Add(upPath, directoryInfo.FullName);
                    }
                    if (updateUI) {
                        int height = _dicFolders.Count * 17 + 4;
                        if (height < 296) listBoxFolder.Height = height;
                        else listBoxFolder.Height = 296;

                        listBoxFolder.DataSource = null;
                        listBoxFolder.DataSource = _dicFolders.Values.ToArray( );
                        listBoxFolder.Visible = true;
                        EventScan.Set( );
                    }
                    return true;
                }
            }
            return false;
        }

        bool addWatcher(string path, out string upPath, out DirectoryInfo di) {//分成两个函数，程序目录监听不可移除，手工添加监听目录可以移除。
            di = new DirectoryInfo(path);
            upPath = di.FullName.TrimEnd('\\').ToUpper( );
            if (di.Root.Name != di.FullName && !_dicWatcher.ContainsKey(upPath) && di.Exists) {//不监听 （根目录、已有监听、不存在的目录）

                string name = "test_" + DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss.ffff") + ".txt";
                try { File.WriteAllText($"{di.FullName}\\{name}", "test"); } catch { return false; }
                try { File.Delete($"{di.FullName}\\{name}"); } catch { return false; }

                FileSystemWatcher watcher = new FileSystemWatcher(di.FullName);
                watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite;

                watcher.Created += OnCreated;
                watcher.Deleted += OnDeleted;
                watcher.Renamed += OnRenamed;

                watcher.EnableRaisingEvents = true;

                lock (rwlistFiles) _dicWatcher.Add(upPath, watcher);

                return true;
            }
            return false;
        }
        void removeWatcher(string upPath) {
            if (_dicWatcher.ContainsKey(upPath)) {
                _dicWatcher[upPath].Created -= OnCreated;
                _dicWatcher[upPath].Deleted -= OnDeleted;
                _dicWatcher[upPath].Renamed -= OnRenamed;
                _dicWatcher[upPath].EnableRaisingEvents = false;
                _dicWatcher[upPath].Dispose( );
                lock (rwlistFiles) _dicWatcher.Remove(upPath);
            }
        }

        void bgScan( ) {
            addWatcher(".", out upRunPath, out DirectoryInfo di);
            _listScan.Add(di.FullName);//程序目录

            ExtractFFmpeg.resources_to_exe( );
            编码队列.find现有ffmpeg进程( );
            thUpdate.Start( );

            while (true) {
                if (_listScan.Count < 1) EventScan.WaitOne( );

                string[] arrPath;
                lock (rwlistFiles) {
                    arrPath = _listScan.ToArray( );
                    _listScan.Clear( );
                }
                bool bAdd = false;
                for (int i = 0; i < arrPath.Length; i++) {
                    string[] files = Directory.GetFiles(arrPath[i]);
                    for (int f = 0; f < files.Length; f++) {
                        bAdd |= addFile(files[f], OneByOne: false);
                    }
                }
                if (bAdd) {
                    upDateVideoFiles( );
                }
                //for (int i = 0; i < arrPath.Length; i++) {//读取excel速度有些慢，先刷新UI，再后台读取。
                //    编码队列.x截取时间表.addFromDirectory(arrPath[i], interval: false);
                //}//进入文件处理时读取
            }
        }

        void bgUpdate( ) {
            while (true) {
                while (info_MouseEnter) EventShowLogs.WaitOne(3333);//焦点时不刷新日志，鼠标移出3秒也不刷新
                while (编码队列.i剩余ffmpeg > 0) {
                    string str = 编码队列.str全部输出信息( );
                    this.BeginInvoke(new Action(( ) => {
                        this.Text = string.Format("Σfps={0:F3} ({1}/{2}) FFX265_Batch_Converter Ver.{3}"
                            , 编码队列.sumFps, 编码队列.i剩余ffmpeg, 编码队列.i剩余ffmpeg + _dicUpPath_Name.Count, Application.ProductVersion);
                        textBoxInfo.Text = str;
                    }));
                    EventShowLogs.WaitOne(updateMS);
                }
                this.BeginInvoke(new Action(( ) => {
                    this.Text = "FFX265_Batch_Converter Ver." + Application.ProductVersion;
                    textBoxInfo.Text = "视频文件放入程序同目录转码";
                }));
                EventShowLogs.WaitOne( );
            }
        }
        void bgEncoding( ) {
            DateTime date上次找ffmpeg文件 = DateTime.Now.AddDays(1);
            do {
                if (_dicUpPath_Name.Count > 0) checkBox_lockedSet.Invoke(new Action(( ) => checkBox_lockedSet.Visible = checkBox_lockedSet.Checked = true));

                while (_dicUpPath_Name.Count > 0) {
                    Dictionary<string, string> dicUpPath_Name_Locked = new Dictionary<string, string>( );
                    List<string> listLocked = new List<string>( );
                    DateTime lockStart = DateTime.Now;
                    do {
                        while (编码队列.b还有空位 && _dicUpPath_Name.Count > 0) {
                            KeyValuePair<string, string> upOne;
                            lock (rwlistFiles) {
                                upOne = _dicUpPath_Name.First( );
                                _dicUpPath_Name.Remove(upOne.Key);
                            }
                            if (File.Exists(upOne.Key)) {
                                if (isFileLocked(upOne.Key)) {
                                    _hashEncoding.Add(upOne.Key);
                                    dicUpPath_Name_Locked.Add(upOne.Key, upOne.Value);
                                    listLocked.Add($"{DateTime.Now} 文件被占用：{upOne.Value}");
                                } else {
                                    if (_dicUpPath_Origin.TryGetValue(upOne.Key, out string origin)) {
                                        if (_hashEncoding.Add(upOne.Key)) {
                                            FileInfo fileInfo = new FileInfo(origin);
                                            RunFFmpeg ffmpeg = new RunFFmpeg(fileInfo, ffmpegParams);
                                            if (ffmpeg.find最新版ffmpeg(ref date上次找ffmpeg文件)) {
                                                if (ffmpegParams.iPreset != 5) {
                                                    ffmpeg.X265转码(userX265Params);
                                                } else {
                                                    ffmpeg.复制数据流(b保存日志: false);
                                                }
                                                编码队列.ffmpeg等待入队(ffmpeg); //此函数任务队列超过并行上限时，会处于无限等待中。
                                            } else {
                                                MessageBox.Show("得重新下载 ffmpeg.exe 放在程序旁边\r\nhttps://github.com/BtbN/FFmpeg-Builds/releases/download/latest/ffmpeg-master-latest-win64-gpl.zip");
                                            }
                                        }
                                    }
                                }
                            }
                        }//先把队列填满，再刷新一次UI。
                        List<string> listUI = new List<string>( );
                        lock (rwlistFiles) {
                            listUI.AddRange(_dicUpPath_Name.Values);
                        }
                        if (listLocked.Count > 0) listUI.AddRange(listLocked);

                        listBoxFiles.Invoke(new Action(( ) => {
                            listBoxFiles.DataSource = null;
                            listBoxFiles.DataSource = listUI;
                        }));

                        if (_dicUpPath_Name.Count > 0) 编码队列.Event编码信号.WaitOne( );
                    } while (_dicUpPath_Name.Count > 0);//跳过占用的文件设计，先转安全文件。

                    if (dicUpPath_Name_Locked.Count > 0) {
                        double useMS = DateTime.Now.Subtract(lockStart).TotalMilliseconds;
                        if (useMS > 0 && useMS < 10000) 编码队列.Event编码信号.WaitOne((10000 - (int)useMS));//等待10秒，再把被占用文件加入队列，继续尝试转码)
                        foreach (var item in dicUpPath_Name_Locked) {
                            _hashEncoding.Remove(item.Key);
                            if (!_dicUpPath_Name.ContainsKey(item.Key)) {//当等待过程中，用户手工拖入
                                lock (rwlistFiles) {
                                    _dicUpPath_Name.Add(item.Key, item.Value);
                                }
                            }
                        }
                    }//加入转码任务后继续尝试，循环到两个字典都空，则跳出。
                }

                编码队列.Event编码信号.WaitOne( );//移除队列触发一次

                if (编码队列.i剩余ffmpeg == 0) checkBox_lockedSet.Invoke(new Action(( ) => checkBox_lockedSet.Visible = checkBox_lockedSet.Checked = false));
            } while (true);
        }

        void upDateVideoFiles( ) {
            if (thEncoding.ThreadState == (System.Threading.ThreadState.Background | System.Threading.ThreadState.WaitSleepJoin) && !编码队列.b正在等待入队) {
                编码队列.Event编码信号.Set( );
            } else {
                string listBoxFiles_Tip = _dicUpPath_Name.Count == 0 ? "双击添加自动监听文件夹" : "拖入文件夹增加自动监听";
                if (listBoxFiles.InvokeRequired) {
                    listBoxFiles.Invoke(new Action(( ) => {
                        listBoxFiles.DataSource = null;
                        listBoxFiles.DataSource = _dicUpPath_Name.Values.ToArray( );
                        toolTipList.SetToolTip(listBoxFiles, listBoxFiles_Tip);
                    }));
                } else {
                    listBoxFiles.DataSource = null;
                    listBoxFiles.DataSource = _dicUpPath_Name.Values.ToArray( );
                    toolTipList.SetToolTip(listBoxFiles, listBoxFiles_Tip);
                }
            }
        }

        Regex regexWriteProcess = new Regex("System|explorer|FastCopy", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        bool isFileLocked(string filePath) {
            try {
                using (FileStream stream = File.OpenRead(filePath)) {
                    return false;
                }
            } catch {
                if (FileUtil.GetProcessesLockingFile(filePath, out List<Process> list)) {
                    for (int i = 0; i < list.Count; i++) {
                        if (list[i].ProcessName.Contains("ffmpeg")) {
                            return false;
                        }
                    }
                    for (int i = 0; i < list.Count; i++) {
                        if (regexWriteProcess.IsMatch(list[i].ProcessName)) {
                            return true;
                        }
                    }

                } else return true;

                return false;
            }
        }

        void CPUNum( ) {
            foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get( )) {
                NumberOfProcessors += int.Parse(item["NumberOfProcessors"].ToString( ));
            }
            //Console.WriteLine("处理器数: {0} ", NumberOfProcessors);

            foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_Processor").Get( )) {
                NumberOfCores += int.Parse(item["NumberOfCores"].ToString( ));
            }
            //Console.WriteLine("核心数: {0}", NumberOfCores);

            foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get( )) {
                NumberOfLogicalProcessors += int.Parse(item["NumberOfLogicalProcessors"].ToString( ));
            }
            //Console.WriteLine("逻辑核心数: {0}", NumberOfLogicalProcessors);


            SingleThread_X265 = NumberOfCores + (int)((NumberOfLogicalProcessors - NumberOfCores) * 0.35f) + 1;
            //核心数 + 超线程*0.35 + 1

            Min_MultiThread_X265 = NumberOfLogicalProcessors / 16;
            Max_MultiThread_X265 = NumberOfLogicalProcessors / 6;

            numericUpDown_NumProcess.Maximum = Max_MultiThread_X265 + 1;
            numericUpDown_NumProcess.Value = Max_MultiThread_X265;

            if (Min_MultiThread_X265 < 2) Min_MultiThread_X265 = 2;

        }

        void refresh_Settings( ) {
            ffmpegParams.audio_Set(label_acodec.Text);

            ffmpegParams.map0a = checkBox_map0a.Checked;
            ffmpegParams.map0s = checkBox_map0s.Checked;

            ffmpegParams.vfr = checkBox_vfr.Checked;
            ffmpegParams.crf = (float)numericUpDownCRF.Value;


            ffmpegParams.gop = (int)numericUpDown_gop.Value;
            ffmpegParams.gop_sec = (float)numericUpDown_gop.Value;

            ffmpegParams.b_gop_sec = checkBox_keyintMax.CheckState == CheckState.Indeterminate;

            ffmpegParams.b_add_lavfi = checkBox_add_lavfi.Checked;
            ffmpegParams.str_add_lavfi = textBox_add_lavfi.Text;

            Setting.b校正为DAR比例 = checkBox_useDAR.Checked;
            Setting.b自动剪裁黑边 = checkBox_autoCrop.Checked;
            Setting.b跳过更高阶编码 = checkBox_Skip_NewEncodec.Checked;

            userX265Params.is_aq_mode = checkBox_aq_mode.Checked;
            userX265Params.hist_scenecut = checkBox_hist_scenecut.Checked;
            userX265Params.fades = checkBox_fades.Checked;

            userX265Params.mcstf = checkBox_mcstf.Checked;

            userX265Params.is_nr_intra = checkBox_nr_intra.Checked;
            userX265Params.is_nr_inter = checkBox_nr_inter.Checked;

            userX265Params.frame_dup = checkBox_frame_dup.Checked;

            userX265Params.keyintMax = checkBox_keyintMax.CheckState == CheckState.Checked;
            userX265Params.keyintSet = checkBox_keyintMax.CheckState == CheckState.Unchecked ;

            userX265Params.analyze_src_pics = checkBox_analyze_src_pics.Checked;

            userX265Params.umh = checkBox_umh.Checked;

            userX265Params.rc_lookahead_halfkeyint = checkBox_rc_lookahead_halfkeyint.Checked;

            userX265Params.hrd = checkBox_hrd.Checked;
            userX265Params.vbv = checkBox_vbv.Checked;

            userX265Params.oneThread = set_oneThread;
            ffmpegParams.oneThread = set_oneThread;

            CheckState state = checkBox_single_sei.CheckState;

            userX265Params.nr_intra = (int)numericUpDown_nr_intra.Value;

            userX265Params.nr_inter = (int)numericUpDown_nr_inter.Value;

            userX265Params.bframes_thirdfps = checkBox_bframes_thirdfps.Checked;

            int _iPreset = comboBoxPresets.SelectedIndex;
            userX265Params.setPreset(_iPreset);
            ffmpegParams.iPreset = _iPreset;


            userX265Params.aq_mode = (int)numericUpDown_aq_mode.Value;

            userX265Params.qp_range((float)numericUpDown_qpmin.Value, (float)numericUpDown_qpmax.Value);

            userX265Params.qp_min_max = checkBox_qp_min_max.Checked;

            Setting.b自动剪裁黑边 = checkBox_autoCrop.Checked;

            编码队列.i多进程数量 = (int)numericUpDown_NumProcess.Value;

            ffmpegParams.scaleto = label_scale.Text;

            if (state == CheckState.Checked) {
                userX265Params.no_info = true;
                userX265Params.single_sei = false;
            } else if (state == CheckState.Indeterminate) {
                userX265Params.no_info = false;
                userX265Params.single_sei = true;
            } else {
                userX265Params.no_info = false;
                userX265Params.single_sei = false;
            }
        }

        private void buttonEncoding_Click(object sender, EventArgs e) {
            refresh_Settings( );
            buttonEncoding.ForeColor = Color.Black;

            if (thEncoding.IsAlive) {
                编码队列.Event编码信号.Set( );
                EventShowLogs.Set( );
            } else {
                numericUpDown_NumProcess.Minimum = 0;
                if (numericUpDown_NumProcess.Value < 1) numericUpDown_NumProcess.Value = 1;
                thEncoding.Start( );
                new UpdateFFmpeg( );
                buttonEncoding.Text = "刷新(&R)";
                if (_dicUpPath_Name.Count == 0 && _dicFolders.Count == 0) {
                    textBoxInfo.Text = "双击右侧列表框，增加文件夹";
                }
            }
        }
        private void numericUpDown_NumProcess_ValueChanged(object sender, EventArgs e) {
            if (thEncoding.IsAlive) {
                int num = (int)numericUpDown_NumProcess.Value;
                if (num == 0) {
                    labeloneThread.Visible = false;
                    label_0_NumProcess.Visible = true;
                } else {
                    if (num <= 编码队列.i剩余ffmpeg) 编码队列.i多进程数量 = num;//当1个任务转暂停，自动生效
                    else if (num < 编码队列.i多进程数量) 编码队列.i多进程数量 = num;//当设置数量小于队列容量，自动生效。
                    else if (num > 编码队列.i多进程数量) buttonEncoding.ForeColor = Color.Goldenrod;//增加任务则需要点刷新。

                    labeloneThread.Visible = true;
                    label_0_NumProcess.Visible = false;
                }
            }
        }
        private void comboBoxPresets_SelectedIndexChanged(object sender, EventArgs e) {
            int index = comboBoxPresets.SelectedIndex;
            if (index == 0) {
                checkBox_OneKey.Visible = true;
            } else {
                checkBox_OneKey.Visible = false;
                checkBox_OneKey.Checked = false;
            }

            if (thEncoding.IsAlive && ffmpegParams.iPreset != comboBoxPresets.SelectedIndex) {
                buttonEncoding.ForeColor = Color.Goldenrod;//修改设置需要点刷新。
            }
        }
        private void textBoxInfo_MouseDoubleClick(object sender, MouseEventArgs e) {
            checkBox_lockedSet.Visible = true;
            panel_Params.Visible = !panel_Params.Visible;
        }

        private void checkBox_single_sei_MouseClick(object sender, MouseEventArgs e) {
            CheckState state = checkBox_single_sei.CheckState;
            if (state == CheckState.Checked) {
                checkBox_single_sei.Text = "隐藏编码信息";
                toolTipList.SetToolTip(checkBox_single_sei, "--no-info");
            } else if (state == CheckState.Indeterminate) {
                checkBox_single_sei.Text = "单信息";
                toolTipList.SetToolTip(checkBox_single_sei, "--single-sei");
            } else {
                checkBox_single_sei.Text = "显示信息";
                toolTipList.SetToolTip(checkBox_single_sei, "--info");
            }
        }

        private void checkBox_nr_intra_CheckedChanged(object sender, EventArgs e) {
            bool show = checkBox_nr_intra.Checked;

            numericUpDown_nr_intra.Visible = show;
            if (show) {
                numericUpDown_nr_intra.Value = 64;
            } else {
                numericUpDown_nr_intra.Value = 0;
            }
        }

        private void checkBox_nr_inter_CheckedChanged(object sender, EventArgs e) {
            bool show = checkBox_nr_inter.Checked;
            numericUpDown_nr_inter.Visible = checkBox_nr_inter.Checked;
            if (show) {
                numericUpDown_nr_inter.Value = 64;
            } else {
                numericUpDown_nr_inter.Value = 0;
            }
        }

        private void checkBox_rc_lookahead_halfkeyint_CheckedChanged(object sender, EventArgs e) {
            if (checkBox_rc_lookahead_halfkeyint.Checked) {
                checkBox_rc_lookahead_halfkeyint.ForeColor = Color.Purple;
                if (numericUpDown_NumProcess.Value > 3)
                    checkBox_rc_lookahead_halfkeyint.Text = "加范围搜索帧类型（大幅增加内存消耗）";
            } else {
                checkBox_rc_lookahead_halfkeyint.ForeColor = Color.Black;
                checkBox_rc_lookahead_halfkeyint.Text = "加范围搜索帧类型";
            }
        }
        private void checkBox_keyintMax_CheckedChanged(object sender, EventArgs e) {

        }

        private void numericUpDown_nr_intra_ValueChanged(object sender, EventArgs e) {
            toolTipList.SetToolTip(numericUpDown_nr_intra, "--nr-intra=" + numericUpDown_nr_intra.Value);
        }

        private void numericUpDown_nr_inter_ValueChanged(object sender, EventArgs e) {
            toolTipList.SetToolTip(numericUpDown_nr_inter, "--nr-inter=" + numericUpDown_nr_inter.Value);
        }

        //bool _last_hrd = false, _last_vbv = false;
        private void checkBox_frame_dup_CheckedChanged(object sender, EventArgs e) {
            //if (checkBox_frame_dup.Checked) {
            //    _last_hrd = checkBox_hrd.Checked;
            //    _last_vbv = checkBox_vbv.Checked;
            //    checkBox_hrd.Checked = true;
            //    checkBox_vbv.Checked = true;
            //} else {
            //    checkBox_hrd.Checked = _last_hrd;
            //    checkBox_vbv.Checked = _last_vbv;
            //}
        }

        bool c_last_qp_range = false, c_last_vfr = false, c_last_params_visible = false;
        decimal d_last_qpmin = 0, d_last_qpmax = 69;
        private void checkBox_oneKey_CheckedChanged(object sender, EventArgs e) {
            bool show = checkBox_OneKey.Checked;
            if (show) {//先记录设置，再赋予新值

                c_last_params_visible = panel_Params.Visible;

                checkBox_autoCrop.Checked = true;

                if (checkBox_qp_min_max.Checked) {
                    c_last_qp_range = true;
                    d_last_qpmin = numericUpDown_qpmin.Value;
                    d_last_qpmax = numericUpDown_qpmax.Value;
                } else {
                    c_last_qp_range = false;
                    checkBox_qp_min_max.Checked = true;
                }
                numericUpDown_qpmax.Value = 69;
                numericUpDown_qpmin.Value = qp_min;

                c_last_vfr = checkBox_vfr.Checked;

                checkBox_vfr.Checked = true;

                panel_Params.Visible = true;

                checkBox_mcstf.Checked = true;


                numericUpDown_aq_mode.Value = 4;
            } else {

                if (!c_last_mcstf) checkBox_mcstf.Checked = false;

                checkBox_vfr.Checked = c_last_vfr;

                checkBox_qp_min_max.Checked = c_last_qp_range;

                if (c_last_qp_range) {
                    numericUpDown_qpmin.Value = d_last_qpmin;
                    numericUpDown_qpmax.Value = d_last_qpmax;
                } else {
                    numericUpDown_qpmin.Value = 0;
                    numericUpDown_qpmax.Value = 69;
                }

                if (!c_last_params_visible)
                    panel_Params.Visible = false;
            }

            checkBox_aq_mode.Checked = show;
            checkBox_analyze_src_pics.Checked = show;
            checkBox_umh.Checked = show;

            checkBox_bframes_thirdfps.Checked = show;
            checkBox_hist_scenecut.Checked = show;
            checkBox_fades.Checked = show;

            checkBox_single_sei.Checked = show;

            //checkBox_rc_lookahead_halfkeyint.Checked = show;内存占用过大
            //checkBox_keyintMax.Checked = show;电影适用，视频片段无法快进

            checkBox_nr_intra.Checked = show;
            checkBox_nr_inter.Checked = show;

            checkBox_map0a.Checked = !show;
        }

        decimal qp_max {
            get {
                if (d_crf > 63) return 69;
                else return d_crf + 6;
            }
        }
        decimal qp_min {
            get {
                if (d_crf < 9)
                    return 0;
                else return d_crf - 9;
            }
        }
        decimal d_crf = 22;

        private void numericUpDown_qp_range_ValueChanged(object sender, EventArgs e) {//两个控件分开写
            string qprange = string.Format("--qpmin={0} --qpmax={1}", numericUpDown_qpmin.Value, numericUpDown_qpmax.Value);
            toolTipList.SetToolTip(checkBox_qp_min_max, qprange);
        }

        bool set_oneThread = false, last_oneThread = false, c_last_mcstf = false;

        private void checkBox_mcstf_Click(object sender, EventArgs e) {
            c_last_mcstf = checkBox_mcstf.Checked;
        }

        private void checkBox_mcstf_CheckedChanged(object sender, EventArgs e) {
            int iProcess = (int)numericUpDown_NumProcess.Value;

            if (checkBox_mcstf.Checked) {
                labeloneThread.Text = "单线多开";
                numericUpDown_NumProcess.Maximum = NumberOfLogicalProcessors + 1;
                iProcess = SingleThread_X265;
                set_oneThread = true;
            } else {
                if (set_oneThread = last_oneThread) {
                    labeloneThread.Text = "单线多开";

                    numericUpDown_NumProcess.Maximum = NumberOfLogicalProcessors + 1;
                    iProcess = SingleThread_X265;
                } else {
                    labeloneThread.Text = "多开";

                    numericUpDown_NumProcess.Maximum = Max_MultiThread_X265 + 1;

                    if (iProcess == SingleThread_X265) {
                        iProcess = Max_MultiThread_X265;
                    } else if (iProcess > Max_MultiThread_X265) {
                        iProcess = Max_MultiThread_X265;
                    }
                }
            }

            if (numericUpDown_NumProcess.Maximum < iProcess)
                numericUpDown_NumProcess.Maximum = iProcess + 1;

            numericUpDown_NumProcess.Value = iProcess;
        }

        private void checkBox_mcstf_MouseClick(object sender, MouseEventArgs e) {
            c_last_mcstf = checkBox_mcstf.Checked;
        }
        int updateMS = 9999;
        private void checkBox_oneThread_MouseClick(object sender, MouseEventArgs e) {

        }

        private void checkBox_qp_min_max_CheckedChanged(object sender, EventArgs e) {
            d_crf = numericUpDownCRF.Value;
            bool show = checkBox_qp_min_max.Checked;

            numericUpDown_qpmin.Visible = label_qp_min_max.Visible = numericUpDown_qpmax.Visible = show;//左贴合控件，不可修改显示顺序

            if (show) {
                numericUpDown_qpmin.Value = qp_min;
                numericUpDown_qpmax.Value = qp_max;
            } else {
                numericUpDown_qpmin.Value = 0;
                numericUpDown_qpmax.Value = 69;
            }
        }

        private void checkBox_aq_mode_CheckedChanged(object sender, EventArgs e) {
            bool show = checkBox_aq_mode.Checked;
            numericUpDown_aq_mode.Visible = show;
            if (show) {
                numericUpDown_aq_mode.Value = 4;
            }
        }

        private void numericUpDown_aq_mode_ValueChanged(object sender, EventArgs e) {
            /*
             1是均匀AQ

2是方差AQ，2的效果比1好但更慢

3是码率向暗场偏移的方差AQ，也就是在2的基础上，让暗场的码率更高，因为x265的算法会导致暗场的码率不足，所以这个模式是必要的

4是通过边缘检测自动调整方差，所以编码速率比3慢很多，但压缩率也会高很多，非常划算 作者：op200_Reek https://www.bilibili.com/read/cv34735585/ 出处：bilibili
             */
            decimal aq_mode = numericUpDown_aq_mode.Value;
            string tip = string.Empty;
            if (aq_mode == 4)
                tip = " 边缘检测自动调整方差，提高压缩率";
            else if (aq_mode == 3)
                tip = " 码率向暗场偏移的方差，提高暗场码率";

            toolTipList.SetToolTip(numericUpDown_aq_mode, "--aq-mode=" + aq_mode + tip);
        }

        private void labeloneThread_Click(object sender, EventArgs e) {
            int iProcess = (int)numericUpDown_NumProcess.Value;

            if (set_oneThread) {
                last_oneThread = set_oneThread = false;

                updateMS = 2222;
                labeloneThread.Text = "多开";

                if (checkBox_mcstf.Checked)
                    checkBox_mcstf.Checked = false;

                numericUpDown_NumProcess.Maximum = Max_MultiThread_X265 + 1;

                if (iProcess == SingleThread_X265) {
                    iProcess = Max_MultiThread_X265;
                } else if (iProcess > Max_MultiThread_X265) {
                    iProcess = Max_MultiThread_X265;
                }
            } else {
                last_oneThread = set_oneThread = true;

                updateMS = 9999;
                labeloneThread.Text = "单线多开";

                numericUpDown_NumProcess.Maximum = NumberOfLogicalProcessors + 1;

                if (iProcess == Min_MultiThread_X265 || iProcess == Max_MultiThread_X265)
                    iProcess = SingleThread_X265;

                if (c_last_mcstf) {
                    checkBox_mcstf.Checked = true;
                }
            }

            if (numericUpDown_NumProcess.Maximum < iProcess) numericUpDown_NumProcess.Maximum = iProcess + 1;

            numericUpDown_NumProcess.Value = iProcess;

            if (thEncoding.IsAlive)
                buttonEncoding.ForeColor = Color.Goldenrod;
        }


        int i_scan_interlaced = 0;
        private void label_scan_interlaced_Click(object sender, EventArgs e) {
            i_scan_interlaced++;
            i_scan_interlaced %= 3;
            if (i_scan_interlaced == 0) {
                Setting.b自动反交错 = true;
                Setting.b以帧识别隔行扫 = true;
                label_scan_interlaced.Text = "反交错：扫描60帧识别(稍慢)";
            } else if (i_scan_interlaced == 1) {
                Setting.b自动反交错 = true;
                Setting.b以帧识别隔行扫 = false;
                label_scan_interlaced.Text = "反交错：编码信息识别(快)";
            } else if (i_scan_interlaced == 2) {
                Setting.b自动反交错 = false;
                Setting.b以帧识别隔行扫 = false;
                label_scan_interlaced.Text = "保持隔行扫描";
            }
        }

        private void checkBox_vfr_CheckedChanged(object sender, EventArgs e) {
            bool vfr = checkBox_vfr.Checked;
            if (vfr) {
                toolTipList.SetToolTip(checkBox_vfr, "-vf mpdecimate -vsync vfr");
            } else
                toolTipList.SetToolTip(checkBox_vfr, "识别重复画面并删除，压制为可变帧率视频");

            if (thEncoding.IsAlive && ffmpegParams.vfr == vfr) {
                buttonEncoding.ForeColor = Color.Goldenrod;
            }
        }
        private void CheckBox_autoCrop_CheckedChanged(object sender, EventArgs e) {
            if (Setting.b自动剪裁黑边 = checkBox_autoCrop.Checked) {//裁黑边选项自动生效
                checkBox_autoCrop.ForeColor = Color.DodgerBlue;
                toolTipList.SetToolTip(checkBox_autoCrop, "-vf crop=x:y");
            } else {
                checkBox_autoCrop.ForeColor = Color.Black;
                toolTipList.SetToolTip(checkBox_autoCrop, "扫描黑边需要额外耗费一些时间");
            }
        }
        private void checkBox_useDAR_CheckedChanged(object sender, EventArgs e) {
            if (thEncoding.IsAlive && Setting.b校正为DAR比例 != checkBox_useDAR.Checked) {
                buttonEncoding.ForeColor = Color.Goldenrod;
            }
        }

        int iscale = 0;
        private void label_scale_Click(object sender, EventArgs e) {
            iscale++;
            iscale %= 11;
            int num = 0;
            if (iscale == num++) {
                label_scale.Text = "100%";
                toolTipList.SetToolTip(label_scale, "原始大小");
            } else if (iscale == num++) {
                label_scale.Text = "↓50%";
                toolTipList.SetToolTip(label_scale, "画面缩小为四分之一");
            } else if (iscale == num++) {
                label_scale.Text = "↓25%";
                toolTipList.SetToolTip(label_scale, "画面缩小为八分之一");
            } else if (iscale == num++) {
                label_scale.Text = "↓1280×720p";
                toolTipList.SetToolTip(label_scale, "720p(只缩小，不放大)");
            } else if (iscale == num++) {
                label_scale.Text = "↓1920×1080p";
                toolTipList.SetToolTip(label_scale, "1080p(只缩小，不放大)");
            } else if (iscale == num++) {
                label_scale.Text = "↓2560×1440p";
                toolTipList.SetToolTip(label_scale, "1440p(只缩小，不放大)");
            } else if (iscale == num++) {
                label_scale.Text = "↓3840×2160p";
                toolTipList.SetToolTip(label_scale, "2160p(只缩小，不放大)");
            } else if (iscale == num++) {
                label_scale.Text = "↓长边1920px";
                toolTipList.SetToolTip(label_scale, "2K（只缩小，不放大）");
            } else if (iscale == num++) {
                label_scale.Text = "↓长边2560px";
                toolTipList.SetToolTip(label_scale, "2.5K（只缩小，不放大）");
            } else if (iscale == num++) {
                label_scale.Text = "↓长边3840px";
                toolTipList.SetToolTip(label_scale, "4K（只缩小，不放大）");
            } else if (iscale == num++) {
                label_scale.Text = "↓长边1280px";
                toolTipList.SetToolTip(label_scale, "1K（只缩小，不放大）");
            }

            if (thEncoding.IsAlive && ffmpegParams.scaleto != label_scale.Text) {
                buttonEncoding.ForeColor = Color.Goldenrod;
            }
        }

        private void contextMenuStrip_Scale_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {
            if (int.TryParse(Setting.regexD.Match(e.ClickedItem.Name).Value, out int s)) {
                iscale = s - 2;
                label_scale_Click(null, null);
            }
        }

        private void checkBox_Skip_NewEncodec_CheckedChanged(object sender, EventArgs e) {
            Setting.b跳过更高阶编码 = checkBox_Skip_NewEncodec.Checked;//跳过编码自动生效
        }
        int iAudio_set = 0;


        private void toolStripTextBox长边像素_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == 13) {
                if (int.TryParse(Setting.regexD.Match(toolStripTextBox长边像素.Text).Value, out int pix)) {
                    if (pix >= X265Params.MIN_PIX) {
                        label_scale.Text = $"↓长边{pix}px";
                        toolTipList.SetToolTip(label_scale, $"长边像素{pix}（Max(w,h)={pix})");
                    }
                }
            } else if (e.KeyChar == 32) {
                toolStripTextBox长边像素.Text = "1024";
                label_scale.Text = $"↓长边{1024}px";
                toolTipList.SetToolTip(label_scale, $"长边像素{1024}（Max(w,h)={1024})");
            }
        }

        private void label_acodec_Click(object sender, EventArgs e) {
            iAudio_set++;
            iAudio_set %= 3;
            if (iAudio_set == 0) {
                label_acodec.Text = "复制音轨";
                toolTipList.SetToolTip(label_acodec, "-acodec copy");
            } else if (iAudio_set == 1) {
                label_acodec.Text = "转AAC立体声";
                toolTipList.SetToolTip(label_acodec, "-acodec aac -ac 2");
            } else if (iAudio_set == 2) {
                label_acodec.Text = "转AAC单声道";
                toolTipList.SetToolTip(label_acodec, "-acodec aac -ac 1");
            }
        }

        private void checkBox_map0a_CheckedChanged(object sender, EventArgs e) {
            ffmpegParams.map0a = checkBox_map0a.Checked;
        }

        private void checkBox_map0s_CheckedChanged(object sender, EventArgs e) {
            ffmpegParams.map0s = checkBox_map0s.Checked;
        }

        private void checkBox_FinishDelSour_CheckedChanged(object sender, EventArgs e) {
            Setting.b完成后删除源视频 = checkBox_FinishDelSour.Checked;
            checkBox_FinishDelSour.BackColor = Setting.b完成后删除源视频 ? Color.Red : Color.White;
        }

        private void numericUpDown_gop_ValueChanged(object sender, EventArgs e) {
            if (checkBox_keyintMax.CheckState == CheckState.Indeterminate)
                toolTipList.SetToolTip(numericUpDown_gop, "-g= fps × " + numericUpDown_gop.Value);
            else if (checkBox_keyintMax.CheckState == CheckState.Checked) {
                toolTipList.SetToolTip(numericUpDown_gop, "-g=" + numericUpDown_gop.Value);
            }
        }

        private void numericUpDownCRF_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                //if (numericUpDownCRF.Value== numericUpDownCRF)
            }
        }

        private void numericUpDown_NumProcess_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.PageUp || e.KeyCode == Keys.VolumeUp) {
                if (numericUpDown_NumProcess.Value == numericUpDown_NumProcess.Maximum && numericUpDown_NumProcess.Maximum < 10000) {//做了4位长度
                    numericUpDown_NumProcess.Maximum++;
                    numericUpDown_NumProcess.Value++;
                }
            }
        }

        bool info_MouseEnter = false;
        //int test = 0;
        private void textBoxInfo_MouseEnter(object sender, EventArgs e) {
            info_MouseEnter = true;
            //textBoxInfo.Text = test++.ToString();
        }
        private void textBoxInfo_MouseLeave(object sender, EventArgs e) {
            info_MouseEnter = false;
        }

        private void checkBox_add_lavfi_CheckedChanged(object sender, EventArgs e) {
            textBox_add_lavfi.ForeColor = checkBox_add_lavfi.Checked ? Color.DeepSkyBlue : Color.Black;
        }

        private void checkBox_keyintMax_CheckStateChanged(object sender, EventArgs e) {
            bool show = checkBox_keyintMax.Checked;
            if (show) {
                if (checkBox_keyintMax.CheckState == CheckState.Indeterminate) {
                    numericUpDown_gop.Value = 5;
                    numericUpDown_gop.Increment = 1;
                    numericUpDown_gop.DecimalPlaces = 2;
                    numericUpDown_gop.Minimum = (decimal)0.01;
                    numericUpDown_gop.Maximum = (decimal)999.99;

                    checkBox_keyintMax.Text = "快进间隔(秒)";
                    checkBox_keyintMax.ForeColor = Color.Black;
                    toolTipList.SetToolTip(checkBox_keyintMax, "--gop=帧");

                    numericUpDown_gop.Visible = true;
                } else {
                    checkBox_keyintMax.ForeColor = Color.Red;
                    checkBox_keyintMax.Text = "最大关键帧 ≈ 播放难以拖动进度条";
                    toolTipList.SetToolTip(checkBox_keyintMax, "--keyint=-1");
                    numericUpDown_gop.Visible = false;
                }
            } else {
                numericUpDown_gop.Value = 120;
                numericUpDown_gop.Minimum = 1;
                numericUpDown_gop.Increment = 24;
                numericUpDown_gop.Maximum = 9999999;
                numericUpDown_gop.DecimalPlaces = 0;
                checkBox_keyintMax.ForeColor = Color.BlueViolet;
                checkBox_keyintMax.Text = "快进间隔(帧)";
                toolTipList.SetToolTip(checkBox_keyintMax, "--keyint=帧");
                numericUpDown_gop.Visible = true;
            }

        }

        private void checkBox_LockedSet_CheckedChanged(object sender, EventArgs e) {
            bool unlock = !checkBox_lockedSet.Checked;
            panel_Params.Enabled = unlock;
            panel_Stream.Enabled = unlock;
            panel_Screen.Enabled = unlock;
            panel_Lavfi.Enabled = unlock;
            panel_Quality.Enabled = unlock;
            checkBox_OneKey.Enabled = unlock;
        }

        private void textBoxInfo_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.F5) {
                EventShowLogs.Set( );
            }
        }

        private void listBoxFiles_DoubleClick(object sender, EventArgs e) {
            if (_dicUpPath_Name.Count < 1) {
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog( );
                folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
                folderBrowserDialog.Description = "添加整个文件夹下视频";
                folderBrowserDialog.ShowNewFolderButton = false;

                if (Directory.Exists(@"E:")) {
                    try {
                        string[] arr = Directory.GetDirectories("E:\\");
                        folderBrowserDialog.SelectedPath = arr[0];
                    } catch { folderBrowserDialog.SelectedPath = "E:\\"; }
                } else if (Directory.Exists(@"D:")) {
                    try {
                        string[] arr = Directory.GetDirectories("D:\\");
                        folderBrowserDialog.SelectedPath = arr[0];
                    } catch { folderBrowserDialog.SelectedPath = "D:\\"; }
                }


                DialogResult dr = folderBrowserDialog.ShowDialog( );

                if (dr == DialogResult.OK) {
                    addFolder(folderBrowserDialog.SelectedPath, updateUI: true);
                }
            }
        }
        private void listBoxFolder_DoubleClick(object sender, EventArgs e) {
            string folder = listBoxFolder.SelectedItem.ToString( );
            DirectoryInfo directoryInfo = new DirectoryInfo(folder);
            string upPath = directoryInfo.FullName.ToUpper( );
            if (Directory.Exists(folder)) {
                FileInfo[] files = directoryInfo.GetFiles("*.*");
                for (int i = 0; i < files.Length; i++) {
                    string upFile = files[i].FullName.ToUpper( );
                    lock (rwlistFiles)
                        if (!_hash_upOneByOne.Contains(upFile))
                            _dicUpPath_Name.Remove(upFile);
                }
                listBoxFiles.DataSource = null;
                listBoxFiles.DataSource = _dicUpPath_Name.Values.ToArray( );
            }
            lock (rwlistFiles) _dicFolders.Remove(upPath);

            removeWatcher(upPath);

            int height = _dicFolders.Count * 17 + 4;
            if (height < 296) listBoxFolder.Height = height;
            else listBoxFolder.Height = 296;

            listBoxFolder.DataSource = null;
            listBoxFolder.DataSource = _dicFolders.Values.ToArray( );
            listBoxFolder.Visible = _dicFolders.Count > 0;
        }

        Thread th休眠倒计时;
        定时休眠 定时休眠 = new 定时休眠( );
        void f休眠倒计时( ) {
            int s = 11;
            while (--s > 0) {
                checkBoxAM8Sleep.Invoke(new Action(( ) => {
                    checkBoxAM8Sleep.Text = s + "秒后休眠……";
                }));
                Thread.Sleep(999);
            }
            if (s < 1) {
                checkBoxAM8Sleep.Invoke(new Action(( ) => {
                    checkBoxAM8Sleep.Text = "省钱模式：(8:00~21:45)峰电休眠，需要配合定时唤醒";
                    定时休眠.Run( );
                }));

            }
        }
        private void checkBoxDaySleep_CheckedChanged(object sender, EventArgs e) {
            if (checkBoxAM8Sleep.Checked) {
                checkBoxAM8Sleep.ForeColor = Color.Red;
                int hour = DateTime.Now.Hour;
                int minute = DateTime.Now.Minute;
                if (hour > 7 && hour < 21 || (hour == 21 && minute < 46)) {
                    try {
                        if (th休眠倒计时 != null && th休眠倒计时.IsAlive) th休眠倒计时.Abort( );
                    } catch { }
                    try {
                        th休眠倒计时 = new Thread(f休眠倒计时);
                        th休眠倒计时.Start( );
                    } catch { }
                } else
                    定时休眠.Run( );
                //定时休眠.Run(7, 59);
            } else {
                //优化编码会引发高CPU占用BUG，不明原因
                checkBoxAM8Sleep.ForeColor = Color.Black;
                checkBoxAM8Sleep.Text = "省钱模式：(8:00~21:45)峰电休眠，需要配合定时唤醒";

                定时休眠.Stop( );
                try {
                    if (th休眠倒计时 != null && th休眠倒计时.IsAlive) th休眠倒计时.Abort( );
                } catch { }
            }
        }

        private void FormMain_Load(object sender, EventArgs e) {
            CPUNum( );
            this.Text = "FFX265_Batch_Converter Ver." + Application.ProductVersion;
        }

        private void FormMain_Activated(object sender, EventArgs e) {
            updateMS = 6666;
            EventShowLogs.Set( );
        }
        private void FormMain_Deactivate(object sender, EventArgs e) {
            updateMS = 9999;
        }
        bool minimized = false;
        private void FormMain_Resize(object sender, EventArgs e) {
            if (WindowState == FormWindowState.Minimized) {
                updateMS = int.MaxValue;
                minimized = true;
            } else {
                if (minimized) {
                    updateMS = 6666;
                    minimized = false;
                    EventShowLogs.Set( );
                }
            }

        }

        private void FormMain_DragEnter(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                e.Effect = DragDropEffects.All;
            } else
                e.Effect = DragDropEffects.None;
        }

        private void FormMain_DragDrop(object sender, DragEventArgs e) {
            string[] arr = e.Data.GetData(DataFormats.FileDrop) as string[];
            bool hasFiles = false;
            List<string> list_Dirs = new List<string>( );
            for (int i = 0; i < arr.Length; i++) {
                if (Directory.Exists(arr[i])) {
                    list_Dirs.Add(arr[i]);
                } else if (File.Exists(arr[i])) {
                    hasFiles |= addFile(arr[i], OneByOne: true);
                }
            }
            if (hasFiles) {
                upDateVideoFiles( );
            }

            if (list_Dirs.Count > 0) {
                for (int i = 0; i < list_Dirs.Count - 1; i++) addFolder(list_Dirs[i], false);//列表区拖入文件处理为文件夹监听
                addFolder(list_Dirs[list_Dirs.Count - 1], updateUI: true);//列表区拖入文件处理为文件夹监听
            }
        }

        private void textBoxInfo_DragDrop(object sender, DragEventArgs e) {
            string[] arr = e.Data.GetData(DataFormats.FileDrop) as string[];
            bool hasFiles = false;
            List<string> subFiles = new List<string>( );//文本框拖入文件处理为单个添加，并添加子目录下所有文件。
            for (int i = 0; i < arr.Length; i++) {
                if (Directory.Exists(arr[i])) {
                    DirectoryInfo di = new DirectoryInfo(arr[i]);
                    if (di.Root.FullName != di.FullName) {
                        FileInfo[] fis = di.GetFiles("*.*", SearchOption.AllDirectories);
                        foreach (FileInfo fi in fis) {
                            if (fi.Directory.Name == "源视频" || fi.Directory.Name.Contains("转码完成")) {//跳过已经处理的文件
                            } else {
                                subFiles.Add(fi.FullName);
                            }
                        }
                    }
                } else if (File.Exists(arr[i])) {
                    subFiles.Add(arr[i]);
                }
            }
            for (int i = 0; i < subFiles.Count; i++) {
                hasFiles |= addFile(subFiles[i], OneByOne: true);
            }

            if (hasFiles) {
                upDateVideoFiles( );
            }
        }


        private void FormMain_FormClosing(object sender, FormClosingEventArgs e) {
            if (编码队列.b还有本次任务( )) {
                e.Cancel = true;
                DialogResult result = MessageBox.Show("是否退出？", "X265正在编码！", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes) {
                    e.Cancel = false;
                }
            }
        }
    }
}
