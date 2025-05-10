using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace FFX265_Batch_Converter {
    class 编码队列 {

        public static int i多进程数量 = 8;

        static List<RunFFmpeg> _ffmpegs = new List<RunFFmpeg>( );

        public static int i剩余ffmpeg => _ffmpegs.Count;

        public static List<RunFFmpeg> ffmpegs {
            get { return _ffmpegs; }
        }

        static object obj增删排锁 = new object( );


        public static AutoResetEvent Event编码信号 = new AutoResetEvent(false);

        public static 截取时间表 x截取时间表 = new 截取时间表( );
        public static bool b还有空位 => _ffmpegs.Count < i多进程数量;

        static bool _b正在等待入队 = false;
        public static bool b正在等待入队 => _b正在等待入队;

        public static void find现有ffmpeg进程( ) {
            Process[] processes = Process.GetProcesses( );
            List<Process> list_ffmpeg = new List<Process>( );
            for (int i = 0; i < processes.Length; i++) {
                if (processes[i].WorkingSet64 > 536870912 && processes[i].ProcessName.ToLower( ).Contains("ffmpeg")) {
                    RunFFmpeg runFFmpeg = new RunFFmpeg( );
                    runFFmpeg.fn后台等待(processes[i]);
                    lock (obj增删排锁) {
                        _ffmpegs.Add(runFFmpeg);
                    }
                }
            }
        }

        public static void ffmpeg等待入队(RunFFmpeg ffmpeg) {
            _b正在等待入队 = true;
            while (_ffmpegs.Count >= i多进程数量) {
                Event编码信号.WaitOne( );//i多进程数量=0，持续挂起入队

                if (_ffmpegs.Count > 0) {
                    for (int i = 0; i < _ffmpegs.Count;) {
                        if (_ffmpegs[i].b已结束) {
                            lock (obj增删排锁) {
                                _ffmpegs.RemoveAt(i);
                            }
                        } else
                            i++;
                    }
                }
            }
            lock (obj增删排锁) {
                _ffmpegs.Add(ffmpeg);
            }
            ffmpeg.th后台线程.Start( );
            _b正在等待入队 = false;
        }

        public static bool ffmpeg主动移除结束(RunFFmpeg ffmpeg) {
            bool success = false;
            lock (obj增删排锁) {
                success = _ffmpegs.Remove(ffmpeg);
            }
            if (!Event编码信号.Set( )) {
                FormMain.EventEncoding.Set( );
            }
            return success;
        }

        public static double sumFps = 0;
        public static string str全部输出信息( ) {
            if (_ffmpegs.Count > 1) {
                sumFps = 0;
                StringBuilder sb = new StringBuilder(_ffmpegs[0].str信息);

                for (int i = 1; i < _ffmpegs.Count; i++) {
                    sumFps += _ffmpegs[i].getFPS;
                    sb.AppendLine( ).AppendLine( ).Append(_ffmpegs[i].str信息);
                }
                if (sumFps > 0) sb.AppendFormat("\r\n\r\n∑fps={0:F3}", sumFps);

                return sb.ToString( );
            } else if (_ffmpegs.Count == 1) {
                sumFps = _ffmpegs[0].getFPS;
                return _ffmpegs[0].str信息;
            } else {
                return string.Empty;
            }
        }


        public static bool b还有本次任务( ) {
            if (_ffmpegs.Count > 0) {
                for (int i = 0; i < _ffmpegs.Count; i++) {
                    if (_ffmpegs[i].b等待文件归档)
                        return true;
                }
            }

            return false;
        }
    }
}
