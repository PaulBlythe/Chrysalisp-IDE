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
    public partial class SearchString : Form
    {
        public String Result;

        public SearchString(String s)
        {
            InitializeComponent();
            if (s!=null)
            {
                textBox1.Text = s;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Result = textBox1.Text;
            this.Close();
        }
    }
}
