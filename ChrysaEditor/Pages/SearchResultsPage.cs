using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

using ChrysaEditor.Types;

namespace ChrysaEditor.Pages
{
    public class SearchResultsPage: Page
    {
        public TabPage hostpage;
        SearchResultItem[] Results;

        public SearchResultsPage(String name, List<SearchResultItem> SearchResults)
        {
            Results = SearchResults.ToArray();

            hostpage = new TabPage("Search '"+name+"'  x");
            ListBox rlist = new ListBox();
            rlist.Font = new Font("Consolas", 10, FontStyle.Regular);
            rlist.Dock = DockStyle.Fill;
            rlist.DoubleClick += Rlist_DoubleClick;
            hostpage.Controls.Add(rlist);

            foreach (SearchResultItem sri in SearchResults)
            {
                rlist.Items.Add(sri.Result());
            }
        }

        private void Rlist_DoubleClick(object sender, EventArgs e)
        {
            ListBox l = (ListBox)sender;
            int index = l.SelectedIndex;
            CodePage cp = new CodePage(Results[index].File, hostpage.ClientSize);
            cp.GoTo(Results[index].Line);

            Form1.Instance.AddPage(cp);

            Application.DoEvents();
        }
    }
}
