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
        ICode icode;
        public FormMain()
        {
            InitializeComponent();
        }

        private void bt_do_Click(object sender, EventArgs e)
        {
            string message = tb_message.Text;
            if (rb_encode.Checked)
            {
                switch(cb_chooseAlgorithm.SelectedItem.ToString())
                {
                    case "Hamming code":
                        icode = new HammingCode(message.Length);
                        break;
                    case "Triangular code":
                        icode = new TriangilarCodeAlgorithm(message.Length);
                        break;
                    case "Rectangular code":
                        icode = new RectangularCode(message.Length);
                        break;
                }
                tb_result.Text = icode.GetCodeWord(message);
            }
            else
            {
                if (icode != null)
                    tb_result.Text = icode.Decode(message);
            }
        }

        private void bt_exchange_Click(object sender, EventArgs e)
        {
            tb_message.Text = tb_result.Text;
            tb_result.Text = "";
        }
    }
}
