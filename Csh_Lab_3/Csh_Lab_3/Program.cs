using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing;

namespace Csh_Lab_3
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Font font = new Font("Consolas", 12, FontStyle.Regular);
            Size startWinSize = new Size { Width = 500, Height = 450 };
            string formTitle = "Lab 3";
           
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(font, formTitle, startWinSize));
        }
    }
}
