using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChrysaEditor.Types
{
    public class SearchResultItem
    {
        public int Line;
        public String Instance;
        public String File;

        public String Result()
        {
            return String.Format("File {0} Line {1}   {2}", File, Line.ToString(), Instance);
        }
    }
}
