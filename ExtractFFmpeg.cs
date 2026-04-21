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
            if (!File.Exists("ffmpeg.exe")) {
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
                    process.StartInfo.Arguments = $"x ffmpeg.7z -y -aos";
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
            FileInfo fileInfo = new FileInfo(filePath);
            if (fileInfo.Exists) {
                if (fileInfo.Extension == ".7z" || fileInfo.Extension == ".7Z") {
                    using (MemoryStream resourceStream = new MemoryStream(Resources._7z)) {
                        using (var archive = new ZipArchive(resourceStream, ZipArchiveMode.Read)) {
                            foreach (var entry in archive.Entries) {
                                if (File.Exists(entry.Name)) try { File.Delete(entry.Name); } catch { }
                                if (!File.Exists(entry.Name)) {
                                    try { entry.ExtractToFile(entry.Name); } catch { }
                                }
                            }
                        }
                    }
                    DirectoryInfo di_Sub = new DirectoryInfo(fileInfo.FullName.Substring(0, fileInfo.FullName.Length - 3));
                    try { di_Sub.Create( ); } catch { return; }

                    int exitCode = -1;
                    using (Process process = new Process( )) {
                        process.StartInfo.FileName = "7z";
                        process.StartInfo.UseShellExecute = false;
                        process.StartInfo.CreateNoWindow = true;
                        process.StartInfo.Arguments = $"x {fileInfo.FullName} -o{di_Sub.FullName} *.exe -r -y";
                        try {
                            process.Start( );
                            process.WaitForExit( );
                            exitCode = process.ExitCode;
                        } catch { }
                    }
                    try { File.Delete("ffmpeg.7z"); } catch { }
                    try { File.Delete("7z.exe"); } catch { }
                    try { File.Delete("7z.dll"); } catch { }
                    try { File.Delete("7-zip.dll"); } catch { }

                    if (exitCode == 0) {
                        FileInfo[] arr_EXE = di_Sub.GetFiles("*.exe", SearchOption.AllDirectories);
                        for (int i = 0; i < arr_EXE.Length; i++) {
                            if (arr_EXE[i].Length > 190000000 && arr_EXE[i].Name == "ffmpeg.exe" || arr_EXE[i].Name == "ffprobe.exe") {
                                string name = string.Format("{0}\\{1}{2:yyyy.MM.dd.HHmm}.exe",
                                    fileInfo.Directory.FullName, arr_EXE[i].Name.Substring(0, arr_EXE[i].Name.Length - 4), arr_EXE[i].LastWriteTime);
                                try { arr_EXE[i].MoveTo(name); } catch { }
                            } else {
                                try { arr_EXE[i].Delete( ); } catch { }
                            }
                        }
                    }
                    try { di_Sub.Delete(true); } catch { }
                }


            } else {

                bool bError = false;
                try {
                    using (var archive = ZipFile.OpenRead(filePath)) {
                        foreach (var entry in archive.Entries) {
                            if (entry.Name.EndsWith(".exe", StringComparison.OrdinalIgnoreCase) && (entry.Name.Contains("ffmpeg") || entry.Name.Contains("ffprobe"))) {
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



