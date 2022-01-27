using System;
using System.Windows.Forms;
using ApplicationRun.Forms;

namespace ApplicationRun
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new test());
        }
    }
}