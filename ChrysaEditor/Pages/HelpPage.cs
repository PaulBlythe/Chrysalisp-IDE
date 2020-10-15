using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScintillaNET;

using ChrysaEditor.Settings;

namespace ChrysaEditor.Pages
{
    public class HelpPage:Page
    {
        public TabPage hostpage;

        public HelpPage(string name, Size clientSize)
        {
            hostpage = new TabPage();
            hostpage.Tag = this;
            hostpage.Text = Path.GetFileName(name) + "   x";
            hostpage.Size = clientSize;

            TextArea = new Scintilla();
            TextArea.Dock = DockStyle.Fill;
            TextArea.TextChanged += (this.OnTextChanged);

            // Configure the default style
            TextArea.StyleResetDefault();
            TextArea.Styles[Style.Default].Font = "Consolas";
            TextArea.Styles[Style.Default].Size = 12;
            TextArea.Styles[Style.Default].BackColor = IntToColor(0x212121);
            TextArea.Styles[Style.Default].ForeColor = IntToColor(0xFFFFFF);
            TextArea.StyleClearAll();

            TextArea.Styles[Style.Lisp.Identifier].ForeColor = IntToColor(0xD0DAE2);
            TextArea.Styles[Style.Lisp.Comment].ForeColor = IntToColor(0xBD758B);
            TextArea.Styles[Style.Lisp.Keyword].ForeColor = IntToColor(0x40BF57);
            TextArea.Styles[Style.Lisp.KeywordKw].ForeColor = IntToColor(0x2FAE35);
            TextArea.Styles[Style.Lisp.Number].ForeColor = IntToColor(0x00FF00);
            TextArea.Styles[Style.Lisp.String].ForeColor = IntToColor(0xFFFF00);
            TextArea.Styles[Style.Lisp.MultiComment].ForeColor = IntToColor(0xE95454);
            TextArea.Styles[Style.Lisp.Operator].ForeColor = IntToColor(0xE0E0E0);
            TextArea.Styles[Style.Lisp.Special].ForeColor = IntToColor(0xff00ff);
            TextArea.Styles[Style.Lisp.StringEol].ForeColor = IntToColor(0x77A7DB);
            TextArea.Styles[Style.Lisp.Symbol].ForeColor = IntToColor(0x48A8EE);

            TextArea.IndentationGuides = IndentView.LookBoth;

            TextArea.Lexer = Lexer.Lisp;
            TextArea.CaretForeColor = IntToColor(0xffffff);

            FileName = Path.Combine(Settings.Settings.HostPath, name);
            string text = System.IO.File.ReadAllText(FileName);
            TextArea.Text = text;

            

            InitNumberMargin();
            InitBookmarkMargin();

            hostpage.Controls.Add(TextArea);
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            Dirty = true;
        }

        private void InitNumberMargin()
        {

            TextArea.Styles[Style.LineNumber].BackColor = IntToColor(BACK_COLOR);
            TextArea.Styles[Style.LineNumber].ForeColor = IntToColor(FORE_COLOR);
            TextArea.Styles[Style.IndentGuide].ForeColor = IntToColor(FORE_COLOR);
            TextArea.Styles[Style.IndentGuide].BackColor = IntToColor(BACK_COLOR);

            var nums = TextArea.Margins[NUMBER_MARGIN];
            nums.Width = 30;
            nums.Type = MarginType.Number;
            nums.Sensitive = true;
            nums.Mask = 0;

            //TextArea.MarginClick += TextArea_MarginClick;
        }

        private void InitBookmarkMargin()
        {

            var margin = TextArea.Margins[BOOKMARK_MARGIN];
            margin.Width = 20;
            margin.Sensitive = true;
            margin.Type = MarginType.Symbol;
            margin.Mask = (1 << BOOKMARK_MARKER);

            var marker = TextArea.Markers[BOOKMARK_MARKER];
            marker.Symbol = MarkerSymbol.Circle;
            marker.SetBackColor(IntToColor(0xFF003B));
            marker.SetForeColor(IntToColor(0x000000));
            marker.SetAlpha(100);

        }
    }
}
