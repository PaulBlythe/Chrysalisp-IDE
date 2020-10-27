
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScintillaNET;

namespace ChrysaEditor.Pages
{
    public class CodePage:Page
    {
        public ContextMenuStrip menu;
        public CodePage(string name, Size clientSize)
        {
            FileName = name;
            hostpage = new TabPage();
            String nn = Path.GetFileName(name);
            hostpage.Text = nn + "   x";
            hostpage.Size = clientSize;

            TextArea = new Scintilla();
            TextArea.Dock = DockStyle.Fill;
            TextArea.TextChanged += (this.OnTextChanged);
            hostpage.Tag = this;

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

            TextArea.SetKeywords(3, Settings.Settings.LispKeywords);
            TextArea.SetKeywords(1, Settings.Settings.LispFunctions);
            TextArea.SetKeywords(2, Settings.Settings.Symbols);
            TextArea.SetKeywords(0, Settings.Settings.LispMacros);
            

            String path = Path.Combine(Settings.Settings.HostPath, name);
            string text = System.IO.File.ReadAllText(path);
            TextArea.Text = text;
            TextArea.CaretForeColor = IntToColor(0xffffff);

            InitNumberMargin();
            InitBookmarkMargin();
            InitCodeFolding();

            hostpage.Controls.Add(TextArea);

            menu = new ContextMenuStrip();
            menu.Items.Add(name);
        }


        private void OnTextChanged(object sender, EventArgs e)
        {
            Dirty = true;
        }

        public void GoTo(int line)
        {
            TextArea.Lines[line].Goto();
            TextArea.FirstVisibleLine = line;
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

        private void InitCodeFolding()
        {

            TextArea.SetFoldMarginColor(true, IntToColor(BACK_COLOR));
            TextArea.SetFoldMarginHighlightColor(true, IntToColor(BACK_COLOR));

            // Enable code folding
            TextArea.SetProperty("fold", "1");
            TextArea.SetProperty("fold.compact", "1");

            // Configure a margin to display folding symbols
            TextArea.Margins[FOLDING_MARGIN].Type = MarginType.Symbol;
            TextArea.Margins[FOLDING_MARGIN].Mask = Marker.MaskFolders;
            TextArea.Margins[FOLDING_MARGIN].Sensitive = true;
            TextArea.Margins[FOLDING_MARGIN].Width = 20;

            // Set colors for all folding markers
            for (int i = 25; i <= 31; i++)
            {
                TextArea.Markers[i].SetForeColor(IntToColor(BACK_COLOR)); // styles for [+] and [-]
                TextArea.Markers[i].SetBackColor(IntToColor(FORE_COLOR)); // styles for [+] and [-]
            }

            // Configure folding markers with respective symbols
            TextArea.Markers[Marker.Folder].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlus : MarkerSymbol.BoxPlus;
            TextArea.Markers[Marker.FolderOpen].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinus : MarkerSymbol.BoxMinus;
            TextArea.Markers[Marker.FolderEnd].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlusConnected : MarkerSymbol.BoxPlusConnected;
            TextArea.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            TextArea.Markers[Marker.FolderOpenMid].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinusConnected : MarkerSymbol.BoxMinusConnected;
            TextArea.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            TextArea.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            // Enable automatic folding
            TextArea.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);

        }
    }
}
