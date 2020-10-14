using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChrysaEditor.Forms
{
    public partial class Searching : Form
    {
        public Searching()
        {
            InitializeComponent();
        }

        
        public void InitPass(int max, String s)
        {
            textBox1.Text = s;
            textBox2.Text = matches.ToString();
            progressBar1.Value = 0;
            progressBar1.Maximum = max;
        }
        private int matches = 0;

        public void AddMatch()
        {
            matches++;
            textBox2.Text = matches.ToString();
        }

        public void Advance()
        {
            progressBar1.Value++;
        }
    }
}
