using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Csh_Lab_4
{
    class PrintForm: Form
    {
        public PrintForm(WordsDictionary words)
        {
            Font font = new Font("Consolas", 12, FontStyle.Regular);
            
            this.Text = "Dictionary";
            this.Font = font;
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowOnly;

            Label label = new Label()
            {
                Parent = this,
                Text = words.ToString(),
                Font = font,
                AutoSize = true,
            };
        }
    }
}