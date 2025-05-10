using System;
using System.IO;
using System.Net;
using System.Threading;

namespace FFX265_Batch_Converter {
    internal class UpdateFFmpeg {
        public static string url = "https://github.com/BtbN/FFmpeg-Builds/releases/download/latest/ffmpeg-master-latest-win64-gpl.zip";
        public UpdateFFmpeg( ) {
            Thread thread = new Thread(background);
            thread.IsBackground = true;
            thread.Start( );
        }

        void background( ) {
            while (true) {
                //while (DateTime.UtcNow.Hour < 6 || DateTime.UtcNow.Hour > 11) Thread.Sleep(3600 * 1000);//UTC+8时区14点开始更新。（服务器自动编译约UTC+8，13：00）
                while (DateTime.Now.Hour < 3 || DateTime.Now.Hour > 8) Thread.Sleep(3600 * 1000);//本地时间3~8点更新
                string day = DateTime.Now.ToString("yyyy-MM-dd");
                string zipFile = $"ffmpeg-master-win64-gpl-{day}.zip";

                while (!File.Exists(zipFile)) {
                    using (WebClient client = new WebClient( )) {
                        string time = DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss.fff");
                        string tempFile = $"ffmpeg-master-win64-gpl-{time}.zip.下载中";
                        try {
                            client.DownloadFile(url, tempFile);
                            day = DateTime.Now.ToString("yyyy-MM-dd");
                            zipFile = $"ffmpeg-master-win64-gpl-{day}.zip";
                            if (!File.Exists(zipFile)) File.Move(tempFile, zipFile);

                            ExtractFFmpeg.file_to_exe(zipFile);
                            break;
                        } catch {
                            try { File.Delete(tempFile); } catch { }

                            string[] arr下载中 = Directory.GetFiles(".\\", "ffmpeg-master-win64-gpl-????-??-?? ??.??.??.???.zip.下载中");
                            foreach (string file in arr下载中) {
                                try { File.Delete(file); } catch { }
                            }

                            string[] arrZip = Directory.GetFiles(".\\", "ffmpeg-master-win64-gpl-????-??-??.zip");
                            foreach (string file in arrZip) {
                                FileInfo fileInfo = new FileInfo(file);
                                if (DateTime.Now.Subtract(fileInfo.LastWriteTime).TotalDays > 7) {
                                    try { File.Delete(file); } catch { }
                                }
                            }
                            Thread.Sleep(3600 * 1000);//下载不成功的话每小时尝试一次。
                        }
                    }
                }

                Thread.Sleep(3 * 24 * 60 * 60 * 1000);//每3~4天更新一次。
            }
        }
    }
}
