using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathOperationLibrary;

namespace tk3
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void bt_do_Click(object sender, EventArgs e)
        {
            HammingCode hc = new HammingCode(4);
            string s = hc.GetCodeWord("1100");
            
            string r = hc.Decode("1100000");
        }
    }
}
