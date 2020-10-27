using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScintillaNET;

namespace ChrysaEditor.Pages
{
    public abstract class Page
    {
        public ScintillaNET.Scintilla TextArea;
        public String FileName;
        public bool Dirty = false;
		public const int NUMBER_MARGIN = 1;
		public const int BOOKMARK_MARGIN = 2;
		public const int BOOKMARK_MARKER = 2;
		public const int FOLDING_MARGIN = 3;
		public const bool CODEFOLDING_CIRCULAR = true;
        public const int BACK_COLOR = 0;
        public const int FORE_COLOR = 0xFFFFFF;
        public TabPage hostpage;

        public static Color IntToColor(int rgb)
        {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }

		

	}
}
