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
        HammingCode hc;
        TriangilarCodeAlgorithm tca;
        public FormMain()
        {
            InitializeComponent();
        }

        private void bt_do_Click(object sender, EventArgs e)
        {
            string message = tb_message.Text;
            if (rb_encode.Checked)
            {
                if (cb_chooseAlgorithm.SelectedText == "Hamming code")
                {
                    hc = new HammingCode(message.Length);
                    tb_result.Text = hc.GetCodeWord(message);
                }
                else
                {
                    tca = new TriangilarCodeAlgorithm(message.Length);
                    tb_result.Text = tca.GetCodeWord(message);
                }
            }
            else
            {
                if (cb_chooseAlgorithm.SelectedText == "Hamming code" && hc != null)
                    tb_result.Text = hc.Decode(message);
                else
                {
                    if (tca != null)
                        tb_result.Text = tca.Decode(message);
                }
            }
        }

        private void bt_exchange_Click(object sender, EventArgs e)
        {
            tb_message.Text = tb_result.Text;
            tb_result.Text = "";
        }
    }
}
