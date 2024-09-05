using log4net.Config;
using System;
using System.IO;
using System.Windows.Forms;

namespace ACS.Monitor.MapUpload
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            XmlConfigurator.ConfigureAndWatch(new FileInfo("Config/log4net.config"));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
