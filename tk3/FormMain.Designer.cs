namespace tk3
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bt_do = new System.Windows.Forms.Button();
            this.tb_message = new System.Windows.Forms.TextBox();
            this.tb_result = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rb_encode = new System.Windows.Forms.RadioButton();
            this.rb_decode = new System.Windows.Forms.RadioButton();
            this.bt_exchange = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bt_do
            // 
            this.bt_do.Location = new System.Drawing.Point(159, 62);
            this.bt_do.Name = "bt_do";
            this.bt_do.Size = new System.Drawing.Size(75, 23);
            this.bt_do.TabIndex = 0;
            this.bt_do.Text = "Do";
            this.bt_do.UseVisualStyleBackColor = true;
            this.bt_do.Click += new System.EventHandler(this.bt_do_Click);
            // 
            // tb_message
            // 
            this.tb_message.Location = new System.Drawing.Point(46, 12);
            this.tb_message.Name = "tb_message";
            this.tb_message.Size = new System.Drawing.Size(188, 20);
            this.tb_message.TabIndex = 1;
            // 
            // tb_result
            // 
            this.tb_result.Location = new System.Drawing.Point(46, 108);
            this.tb_result.Name = "tb_result";
            this.tb_result.Size = new System.Drawing.Size(188, 20);
            this.tb_result.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rb_decode);
            this.groupBox1.Controls.Add(this.rb_encode);
            this.groupBox1.Location = new System.Drawing.Point(46, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(107, 57);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // rb_encode
            // 
            this.rb_encode.AutoSize = true;
            this.rb_encode.Checked = true;
            this.rb_encode.Location = new System.Drawing.Point(7, 10);
            this.rb_encode.Name = "rb_encode";
            this.rb_encode.Size = new System.Drawing.Size(62, 17);
            this.rb_encode.TabIndex = 0;
            this.rb_encode.TabStop = true;
            this.rb_encode.Text = "Encode";
            this.rb_encode.UseVisualStyleBackColor = true;
            // 
            // rb_decode
            // 
            this.rb_decode.AutoSize = true;
            this.rb_decode.Location = new System.Drawing.Point(7, 33);
            this.rb_decode.Name = "rb_decode";
            this.rb_decode.Size = new System.Drawing.Size(63, 17);
            this.rb_decode.TabIndex = 1;
            this.rb_decode.Text = "Decode";
            this.rb_decode.UseVisualStyleBackColor = true;
            // 
            // bt_exchange
            // 
            this.bt_exchange.Location = new System.Drawing.Point(12, 30);
            this.bt_exchange.Name = "bt_exchange";
            this.bt_exchange.Size = new System.Drawing.Size(27, 86);
            this.bt_exchange.TabIndex = 4;
            this.bt_exchange.Text = "^  |";
            this.bt_exchange.UseVisualStyleBackColor = true;
            this.bt_exchange.Click += new System.EventHandler(this.bt_exchange_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 183);
            this.Controls.Add(this.bt_exchange);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tb_result);
            this.Controls.Add(this.tb_message);
            this.Controls.Add(this.bt_do);
            this.Name = "FormMain";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_do;
        private System.Windows.Forms.TextBox tb_message;
        private System.Windows.Forms.TextBox tb_result;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rb_decode;
        private System.Windows.Forms.RadioButton rb_encode;
        private System.Windows.Forms.Button bt_exchange;
    }
}

