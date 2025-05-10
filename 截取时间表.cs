using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace FFX265_Batch_Converter {
    public class 截取时间表 {

        Regex regexTime = new Regex(@"(?:(?<HH>0\d|1\d|2[01234]|[1-9])[\D]+(?<MM>0\d|[1-5]\d|60|[1-9])[\W]+(?<SS>0\d|[1-5]\d|60|[1-9])(?:[\W]+(?<MS>\d+)[\D]*)?)|(?:(?<MM>0\d|[1-5]\d|60|[1-9])[\D]+(?<SS>0\d|[1-5]\d|60|[1-9])(?:[\W]+(?<MS>\d+)[\D]*)?)", RegexOptions.RightToLeft | RegexOptions.Compiled);

        Dictionary<string, DateTime> pairs_截取时间表_修改时刻 = new Dictionary<string, DateTime>( ), pairs路径_时刻 = new Dictionary<string, DateTime>( );

        public Dictionary<string, 截取时间> pairs_大写文件名_截取时刻 = new Dictionary<string, 截取时间>( );

        public 截取时间 hasTime(FileInfo file) {
            string vName = file.Name.ToUpper( );
            string name = vName.Substring(0, vName.LastIndexOf('.'));

            if (!pairs_大写文件名_截取时刻.TryGetValue(name, out 截取时间 one)) {
                if (!pairs_大写文件名_截取时刻.TryGetValue(vName, out one)) {
                    one = new 截取时间( );//截取时刻写在-i(加载文件之前)，硬字幕不同步。
                }
            }

            if (!pairs_大写文件名_截取时刻.ContainsKey(file.Name))
                pairs_大写文件名_截取时刻.Add(file.Name, one);

            return one;
        }


        TimeSpan tryGetTimeSpan(object obj, out bool hasTime) {
            string str = obj.ToString( ).Trim( );
            hasTime = false;
            TimeSpan timeSpan = TimeSpan.Zero;
            if (!string.IsNullOrEmpty(str)) {
                if (double.TryParse(str, out double d1)) {
                    hasTime = true;
                    timeSpan = TimeSpan.FromSeconds(d1);
                }

                if (!hasTime) {
                    {
                        hasTime = TimeSpan.TryParse(str, out timeSpan);
                        if (!hasTime) {
                            str += " ";
                            Match m = regexTime.Match(str);
                            if (m.Success) {
                                string temp = string.Format("{0}:{1}:{2}.{3}", m.Groups["HH"].Value.PadLeft(2, '0'), m.Groups["MM"].Value.PadLeft(2, '0'), m.Groups["SS"].Value.PadLeft(2, '0'), m.Groups["MS"].Value.PadRight(3, '0'));
                                hasTime = TimeSpan.TryParse(temp, out timeSpan);
                            }
                        }
                    }
                }
            }
            return timeSpan;
        }

        bool b有修改(FileInfo fi) {
            string upFullName = fi.FullName;
            if (pairs_截取时间表_修改时刻.ContainsKey(upFullName)) {
                if (pairs_截取时间表_修改时刻[upFullName] == fi.LastWriteTimeUtc)
                    return false;
                else
                    pairs_截取时间表_修改时刻[upFullName] = fi.LastWriteTimeUtc;
            } else
                pairs_截取时间表_修改时刻.Add(upFullName, fi.LastWriteTimeUtc);

            return true;
        }

        public void addDataFromCSV(FileInfo fi) {
            if (b有修改(fi)) {
                string[] Rows = null;
                try { Rows = File.ReadAllLines(fi.FullName, getEncoding(fi)); } catch { return; }

                for (int r = 0; r < Rows.Length; r++) {
                    string[] Column = Rows[r].Split(',');
                    if (Column.Length > 1) {
                        string s2 = Column.Length > 2 ? Column[2] : string.Empty;
                        TimeSpan t1 = tryGetTimeSpan(Column[1].Trim('"') + " ", out bool b1);
                        TimeSpan t2 = tryGetTimeSpan(s2.Trim('"') + " ", out bool b2);
                        if (b1 || b2) {
                            string name = Column[0].Trim( ).Trim('"').ToUpper( );
                            if (pairs_大写文件名_截取时刻.ContainsKey(name)) {
                                pairs_大写文件名_截取时刻[name] = new 截取时间(t1, t2);
                            } else {
                                pairs_大写文件名_截取时刻.Add(name, new 截取时间(t1, t2));
                            }
                        }
                    }
                }
            }
        }
        public void addDataFromExcelByConn(FileInfo fi) {
            if (!b有修改(fi)) return;
            List<string> tableName = new List<string>( );
            string strCon = string.Empty;
            string vertion = fi.Extension.ToLower( ) == ".xlsx" ? "12" : "8";
            strCon = $"Provider=Microsoft.ACE.OLEDB.16.0;Data Source={fi.FullName};;Extended Properties=\"Excel {vertion}.0;HDR=YES;IMEX=1\"";
            try {
                using (OleDbConnection objConn = new OleDbConnection(strCon)) {
                    objConn.Open( );
                    DataTable dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    foreach (DataRow row in dt.Rows) {
                        string strSheetTableName = row["TABLE_NAME"].ToString( );
                        if (strSheetTableName.Contains("$") && strSheetTableName.Replace("'", "").EndsWith("$")) {  //过滤无效SheetName
                            tableName.Add(strSheetTableName.Substring(0, strSheetTableName.Length - 1));
                        }
                    }
                }
            } catch {
                if (vertion == "8") {
                    strCon = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};;Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"", fi.FullName);
                    try {
                        using (OleDbConnection objConn = new OleDbConnection(strCon)) {
                            objConn.Open( );
                            DataTable dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            foreach (DataRow row in dt.Rows) {
                                string strSheetTableName = row["TABLE_NAME"].ToString( );
                                if (strSheetTableName.Contains("$") && strSheetTableName.Replace("'", "").EndsWith("$")) {  //过滤无效SheetName
                                    tableName.Add(strSheetTableName.Substring(0, strSheetTableName.Length - 1));
                                }
                            }
                        }
                    } catch { return; }
                }
            }
            if (tableName.Count < 1) return;
            using (DataSet ds = new DataSet( )) {
                for (int i = 0; i < tableName.Count; i++) {
                    try {
                        string strCom = $" SELECT * FROM [{tableName[i]}$]";
                        using (OleDbConnection myConn = new OleDbConnection(strCon))
                        using (OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, myConn)) {
                            myConn.Open( );
                            myCommand.Fill(ds);
                        }
                    } catch { } finally {
                        if (ds != null || ds.Tables.Count > 0)
                            for (int t = 0; t < ds.Tables.Count; t++) {
                                for (int r = 0; r < ds.Tables[t].Rows.Count; r++) {
                                    //string s1 = ds.Tables[t].Rows[r][1].ToString();
                                    //string s2 = ds.Tables[t].Rows[r][2].ToString();
                                    TimeSpan t1 = tryGetTimeSpan(ds.Tables[t].Rows[r][1], out bool b1);
                                    TimeSpan t2 = tryGetTimeSpan(ds.Tables[t].Rows[r][2], out bool b2);
                                    if (b1 || b2) {
                                        string name = ds.Tables[t].Rows[r][0].ToString( ).Trim( ).ToUpper( );

                                        if (pairs_大写文件名_截取时刻.ContainsKey(name)) {
                                            pairs_大写文件名_截取时刻[name] = new 截取时间(t1, t2);
                                        } else {
                                            pairs_大写文件名_截取时刻.Add(name, new 截取时间(t1, t2));
                                        }
                                    }
                                }

                            }

                    }
                }
            }
        }

        public void addFromDirectory(List<string> arrDir) {
            for (int i = 0; i < arrDir.Count; i++) {
                addFromDirectory(arrDir[i], false);
            }
        }
        public void addFromDirectory(string dir, bool interval) {
            DateTime time = DateTime.Now;
            if (pairs路径_时刻.ContainsKey(dir)) {
                if (time.Subtract(pairs路径_时刻[dir]).TotalSeconds < 30) {
                    if (interval)
                        return;
                } else
                    pairs路径_时刻[dir] = time;
            } else
                pairs路径_时刻.Add(dir, time);

            List<FileInfo> fileInfos = new List<FileInfo>( );

            string[] arr_xlsxFile;
            try { arr_xlsxFile = Directory.GetFiles(dir, "*截取时间表*.*"); } catch { return; }

            int x = 0;
            for (; x < arr_xlsxFile.Length; x++) {
                FileInfo fi = new FileInfo(arr_xlsxFile[x]);
                if (fi.Length < 1048576) {
                    string ext = fi.Extension.ToLower( );
                    if (ext == ".csv" || ext == ".xls" || ext == ".xlsx") {//64位程序要用64位数据库链接插件，64位系统默认安装32位，不支持2010
                        fileInfos.Add(fi);
                        x++;
                        break;
                    }
                }
            }
            for (; x < arr_xlsxFile.Length; x++) {
                FileInfo fi = new FileInfo(arr_xlsxFile[x]);
                if (fi.Length < 1048576) {
                    string ext = fi.Extension.ToLower( );
                    if (ext == ".csv" || ext == ".xls" || ext == ".xlsx") {
                        bool bInsert = false;
                        for (int i = 0; i < fileInfos.Count; i++) {
                            if (fi.LastWriteTimeUtc < fileInfos[i].LastWriteTimeUtc) {
                                fileInfos.Insert(i, fi);
                                bInsert = true;
                                break;
                            }
                        }
                        if (!bInsert) fileInfos.Add(fi);

                    }
                }
            }

            for (x = 0; x < fileInfos.Count; x++) {
                string ext = fileInfos[x].Extension.ToLower( );
                if (ext == ".csv") {
                    addDataFromCSV(fileInfos[x]);
                } else if (ext == ".xls" || ext == ".xlsx") {
                    addDataFromExcelByConn(fileInfos[x]);
                }
            }
        }

        Encoding getEncoding(FileInfo fi) {
            byte[] fileBytes = new byte[3]; // 创建一个缓冲区
            try {
                using (FileStream fileStream = new FileStream(fi.FullName, FileMode.Open, FileAccess.Read)) {
                    //读取文件的一部分数据
                    fileStream.Read(fileBytes, 0, fileBytes.Length);
                }
            } catch { }

            Encoding encoding = Encoding.Default;

            //UTF - 8
            if (fileBytes.Length == 3) {
                if ((fileBytes[0] == 34 && fileBytes[1] == 232 && fileBytes[2] == 167) ||
                    (fileBytes[0] == 239 && fileBytes[1] == 187 && fileBytes[2] == 191)) {
                    encoding = Encoding.UTF8;
                } else if (fileBytes[0] == 34 && fileBytes[1] == 202 && fileBytes[2] == 211) {
                    encoding = Encoding.GetEncoding("GB2312");
                } else if ((fileBytes[0] == 255 && fileBytes[1] == 254 && fileBytes[2] == 34) ||
                        (fileBytes[0] == 254 && fileBytes[1] == 255 && fileBytes[2] == 0)) {
                    encoding = Encoding.UTF32;
                }
            }
            return encoding;

        }

    }

    public class 截取时间 {
        public bool b剪裁片头 = false, b剪裁片尾 = false;

        public TimeSpan time设置开始 = TimeSpan.Zero, time设置结束 = TimeSpan.Zero;
        public TimeSpan span视频 = TimeSpan.Zero, span编码 = TimeSpan.Zero, time编码开始 = TimeSpan.Zero, time编码结束 = TimeSpan.Zero;

        public 截取时间( ) {

        }

        public 截取时间(TimeSpan time开始, TimeSpan time结束) {
            if (time开始.Ticks > 0 && time结束.Ticks > 0) {
                if (time开始 == time结束) {
                    time设置结束 = time结束;
                } else {
                    if (time开始 > time结束) {
                        time设置开始 = time结束;
                        time设置结束 = time开始;
                    } else {
                        time设置开始 = time开始;
                        time设置结束 = time结束;
                    }
                }
            } else if (time开始.Ticks > 0) {
                time设置开始 = time开始;
            } else if (time结束.Ticks > 0) {
                time设置结束 = time结束;
            }
        }

        public TimeSpan get编码时长(TimeSpan span视频时长) {
            this.span视频 = span视频时长;

            if (time设置开始 > TimeSpan.Zero && time设置开始 < span视频时长) {
                time编码开始 = time设置开始;
                b剪裁片头 = true;
            } else {
                b剪裁片头 = false;
            }

            if (time设置结束 > TimeSpan.Zero && time设置结束 < span视频时长) {
                time编码结束 = time设置结束;
                b剪裁片尾 = true;
            } else {
                time编码结束 = span视频时长;
                b剪裁片尾 = false;
            }

            if (b剪裁片头 || b剪裁片尾)
                span编码 = time编码结束.Subtract(time编码开始);
            else
                span编码 = span视频时长;

            return span编码;
        }

        public string str剪裁Title {
            get {
                if (b剪裁片头 && b剪裁片尾) return "Trim.";
                else if (b剪裁片头) return "TrimStart.";
                else if (b剪裁片尾) return "TrimEnd.";
                else return string.Empty;
            }
        }
        public string getCMD {
            get {
                string ss, to;
                if (b剪裁片头) ss = string.Format(" -ss {0:F3}", time编码开始.TotalSeconds);
                else ss = string.Empty;

                if (b剪裁片尾) to = string.Format(" -to {0:F3}", time编码结束.TotalSeconds);
                else to = string.Empty;

                return ss + to;
            }
        }
        public string getSS {
            get {
                string ss;
                if (b剪裁片头) ss = string.Format(" -ss {0:F3}", time设置开始.TotalSeconds);
                else ss = string.Empty;

                return ss;
            }
        }
        public string getTo {
            get {
                string to;
                if (b剪裁片尾) to = string.Format(" -to {0:F3}", time编码结束.TotalSeconds);
                else to = string.Empty;

                return to;
            }
        }

    }



}
