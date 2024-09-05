using log4net.Config;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;


namespace ACS.Monitor
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Process[] procs = Process.GetProcessesByName("ACS.Monitor");

                if (procs.Length > 1)
                {
                    MessageBox.Show("프로그램을 하나 이상 실행할 수 없습니다.");
                    Application.Exit();
                    return;
                }
                else
                {
                    XmlConfigurator.ConfigureAndWatch(new FileInfo("Config/log4net.config"));

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new MainForm());
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
