using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScintillaNET;
using MarkdownDeep;
using ChrysaEditor.Settings;

namespace ChrysaEditor.Pages
{
    public class HelpPage : Page
    {
       

        public HelpPage(string name, Size clientSize)
        {
            hostpage = new TabPage();
            hostpage.Tag = this;
            hostpage.Text = Path.GetFileName(name) + "   x";
            hostpage.Size = clientSize;

            FileName = Path.Combine(Settings.Settings.HostPath, name);
            string text = System.IO.File.ReadAllText(FileName);

            var md = new MarkdownDeep.Markdown();
            string output = md.Transform(text);

            WebBrowser wb = new WebBrowser();
            wb.DocumentText = output;
            wb.Dock = DockStyle.Fill;
            hostpage.Controls.Add(wb);

        }

    }
}
