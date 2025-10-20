using System;
using System.Windows.Forms;

namespace BezpieczenstwoDanych
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1()); // uruchamia formularz WinForms
        }
    }
}
