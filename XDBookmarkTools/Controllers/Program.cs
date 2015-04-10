using System;
using System.Windows.Forms;

namespace XDBookmarkTools.Controllers
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var mc = new MainController();
            mc.Init();
            Application.Run(mc.MainView);
        }
    }
}