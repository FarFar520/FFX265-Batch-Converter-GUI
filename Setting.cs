using System.Text.RegularExpressions;

namespace FFX265_Batch_Converter {
    internal static class Setting {

        public static Regex regexD = new Regex(@"\d+", RegexOptions.Compiled);

        public static bool b完成后删除源视频 = false;

        public static bool
            b自动反交错 = true,
            b自动剪裁黑边 = true,
            b校正为DAR比例 = false,
            b以帧识别隔行扫 = true,
            b跳过更高阶编码 = true
            ;
    }
}
