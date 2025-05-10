using System;
using System.Diagnostics;
using System.Threading;

namespace FFX265_Batch_Converter {
    internal class 定时休眠 {

        public Thread thread;
        int hour = 8, minute = 0;

        public void Run( ) {
            if (thread != null && thread.IsAlive)
                try { thread.Abort( ); } catch { }

            thread = new Thread(峰电时段);
            thread.IsBackground = true;
            thread.Start( );
        }
        public void Run(int hour, int minute) {
            this.hour = hour % 24;
            this.minute = minute & 60;
            thread = new Thread(打表);
            thread.IsBackground = true;
            thread.Start( );
        }
        public void Stop( ) {
            if (thread != null && thread.IsAlive)
                try { thread.Abort( ); } catch { }
        }

        void 峰电时段( ) {
            do {
                try {
                    int h = DateTime.Now.Hour;
                    if (h > 7 && h < 21) {
                        进入休眠( );
                    } else if (h == 21) {
                        if (DateTime.Now.Minute < 45) {
                            进入休眠( );
                        }
                    }
                    Thread.Sleep(300000);
                } catch { }
            } while (true);
        }
        void 打表( ) {
            do {
                try {
                    int h = DateTime.Now.Hour - hour;
                    if (h == 0) {
                        if (DateTime.Now.Minute - minute >= 0) {
                            进入休眠( );
                        }
                    } else if (h == 1) {
                        if (minute - DateTime.Now.Minute > 0) {
                            进入休眠( );
                        }
                    }
                    Thread.Sleep(300000);
                } catch { }
            } while (true);

        }
        void 进入休眠( ) {
            try {
                using (Process process = new Process( )) {
                    process.StartInfo.FileName = "powercfg";
                    process.StartInfo.Arguments = "-h on";
                    process.StartInfo.Verb = "runas";
                    process.Start( );
                }
            } catch { }

            try {
                using (Process process = new Process( )) {
                    process.StartInfo.FileName = "rundll32";
                    process.StartInfo.Arguments = "powrprof.dll,SetSuspendState 0,1,0";
                    process.StartInfo.Verb = "runas";
                    process.Start( );
                }
            } catch { }

        }

    }
}
