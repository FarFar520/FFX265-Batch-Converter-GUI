using System;
using System.Windows.Forms;

namespace FFX265_Batch_Converter {
    internal static class Program {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main( ) {
            Application.EnableVisualStyles( );
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain( ));
        }
    }
}
