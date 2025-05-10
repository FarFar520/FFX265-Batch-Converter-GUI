using FFX265_Batch_Converter.Properties;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading;

namespace FFX265_Batch_Converter {
    internal static class ExtractFFmpeg {
        public static void resources_to_exe( ) {
            Thread thread = new Thread(background);
            thread.Priority = ThreadPriority.Highest;
            thread.Start( );
        }

        static void background( ) {
            if (!File.Exists("ffmpeg.exe") && !File.Exists("ffprobe.exe")) {
                using (MemoryStream resourceStream = new MemoryStream(Resources._7z)) {
                    using (var archive = new ZipArchive(resourceStream, ZipArchiveMode.Read)) {
                        foreach (var entry in archive.Entries) {
                            if (File.Exists(entry.Name)) try { File.Delete(entry.Name); } catch { }
                            if (!File.Exists(entry.Name)) {
                                try { entry.ExtractToFile(entry.Name); } catch (Exception err) {
                                    try { File.AppendAllText("无法释放资源.log", "\r\n" + err.Message); } catch { }
                                }
                            }
                        }
                    }
                }
                if (!File.Exists("ffmpeg.7z")) File.WriteAllBytes("ffmpeg.7z", Resources.ffmpeg);
                using (Process process = new Process( )) {
                    process.StartInfo.FileName = "7z";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.Arguments = $"x ffmpeg.7z -y -aou";
                    try {
                        process.Start( );
                        process.WaitForExit( );
                    } catch { }
                }

                try { File.Delete("ffmpeg.7z"); } catch { }

                try { File.Delete("7z.exe"); } catch { }
                try { File.Delete("7z.dll"); } catch { }
                try { File.Delete("7-zip.dll"); } catch { }
            }
        }

        public static void file_to_exe(string filePath) {
            if (File.Exists(filePath)) {
                bool bError = false;
                try {
                    using (var archive = ZipFile.OpenRead(filePath)) {
                        foreach (var entry in archive.Entries) {
                            if (entry.Name.EndsWith(".exe", StringComparison.OrdinalIgnoreCase) && entry.Name.Contains("ffmpeg")) {
                                string exeFile = $"{entry.Name.Substring(0, entry.Name.Length - 4)}.{entry.LastWriteTime.ToString("yyyy.MM.dd.HHmm")}.exe";
                                if (!File.Exists(exeFile)) {
                                    try {
                                        entry.ExtractToFile(exeFile);
                                    } catch { bError = true; }
                                }
                            }
                        }
                    }
                } catch { }

                if (bError) try { File.Delete(filePath); } catch { }
            }
        }

    }


}
