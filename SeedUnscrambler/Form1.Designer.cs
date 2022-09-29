namespace SeedUnscrambler
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBox_Seed = new System.Windows.Forms.TextBox();
            this.textBox_Eth_PK = new System.Windows.Forms.TextBox();
            this.textBox_Eth_Address = new System.Windows.Forms.TextBox();
            this.textBox_Sol_Address = new System.Windows.Forms.TextBox();
            this.textBox_Sol_PK = new System.Windows.Forms.TextBox();
            this.textBox_Tron_Address = new System.Windows.Forms.TextBox();
            this.textBox_Tron_PK = new System.Windows.Forms.TextBox();
            this.button_ETH_Go = new System.Windows.Forms.Button();
            this.button_Tron_Go = new System.Windows.Forms.Button();
            this.button_SOL_Go = new System.Windows.Forms.Button();
            this.checkBox_TopMost = new System.Windows.Forms.CheckBox();
            this.checkBox_HideTaskbar = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox_Seed
            // 
            this.textBox_Seed.Location = new System.Drawing.Point(12, 44);
            this.textBox_Seed.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_Seed.Name = "textBox_Seed";
            this.textBox_Seed.Size = new System.Drawing.Size(689, 25);
            this.textBox_Seed.TabIndex = 0;
            this.textBox_Seed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_Seed.TextChanged += new System.EventHandler(this.textBox_Seed_TextChanged);
            // 
            // textBox_Eth_PK
            // 
            this.textBox_Eth_PK.Location = new System.Drawing.Point(12, 101);
            this.textBox_Eth_PK.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_Eth_PK.Name = "textBox_Eth_PK";
            this.textBox_Eth_PK.Size = new System.Drawing.Size(689, 25);
            this.textBox_Eth_PK.TabIndex = 1;
            this.textBox_Eth_PK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_Eth_PK.TextChanged += new System.EventHandler(this.textBox_Eth_PK_TextChanged);
            // 
            // textBox_Eth_Address
            // 
            this.textBox_Eth_Address.Location = new System.Drawing.Point(12, 130);
            this.textBox_Eth_Address.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_Eth_Address.Name = "textBox_Eth_Address";
            this.textBox_Eth_Address.ReadOnly = true;
            this.textBox_Eth_Address.Size = new System.Drawing.Size(637, 25);
            this.textBox_Eth_Address.TabIndex = 2;
            this.textBox_Eth_Address.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_Sol_Address
            // 
            this.textBox_Sol_Address.Location = new System.Drawing.Point(12, 311);
            this.textBox_Sol_Address.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_Sol_Address.Name = "textBox_Sol_Address";
            this.textBox_Sol_Address.ReadOnly = true;
            this.textBox_Sol_Address.Size = new System.Drawing.Size(637, 25);
            this.textBox_Sol_Address.TabIndex = 4;
            this.textBox_Sol_Address.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_Sol_PK
            // 
            this.textBox_Sol_PK.Location = new System.Drawing.Point(12, 282);
            this.textBox_Sol_PK.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_Sol_PK.Name = "textBox_Sol_PK";
            this.textBox_Sol_PK.Size = new System.Drawing.Size(689, 25);
            this.textBox_Sol_PK.TabIndex = 3;
            this.textBox_Sol_PK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_Sol_PK.TextChanged += new System.EventHandler(this.textBox_Sol_PK_TextChanged);
            // 
            // textBox_Tron_Address
            // 
            this.textBox_Tron_Address.Location = new System.Drawing.Point(12, 221);
            this.textBox_Tron_Address.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_Tron_Address.Name = "textBox_Tron_Address";
            this.textBox_Tron_Address.ReadOnly = true;
            this.textBox_Tron_Address.Size = new System.Drawing.Size(637, 25);
            this.textBox_Tron_Address.TabIndex = 6;
            this.textBox_Tron_Address.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_Tron_PK
            // 
            this.textBox_Tron_PK.Location = new System.Drawing.Point(12, 192);
            this.textBox_Tron_PK.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_Tron_PK.Name = "textBox_Tron_PK";
            this.textBox_Tron_PK.Size = new System.Drawing.Size(689, 25);
            this.textBox_Tron_PK.TabIndex = 5;
            this.textBox_Tron_PK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_Tron_PK.TextChanged += new System.EventHandler(this.textBox_Tron_PK_TextChanged);
            // 
            // button_ETH_Go
            // 
            this.button_ETH_Go.Location = new System.Drawing.Point(655, 129);
            this.button_ETH_Go.Name = "button_ETH_Go";
            this.button_ETH_Go.Size = new System.Drawing.Size(46, 27);
            this.button_ETH_Go.TabIndex = 7;
            this.button_ETH_Go.Text = "Go";
            this.button_ETH_Go.UseVisualStyleBackColor = true;
            this.button_ETH_Go.Click += new System.EventHandler(this.button_ETH_Go_Click);
            // 
            // button_Tron_Go
            // 
            this.button_Tron_Go.Location = new System.Drawing.Point(655, 220);
            this.button_Tron_Go.Name = "button_Tron_Go";
            this.button_Tron_Go.Size = new System.Drawing.Size(46, 27);
            this.button_Tron_Go.TabIndex = 8;
            this.button_Tron_Go.Text = "Go";
            this.button_Tron_Go.UseVisualStyleBackColor = true;
            this.button_Tron_Go.Click += new System.EventHandler(this.button_Tron_Go_Click);
            // 
            // button_SOL_Go
            // 
            this.button_SOL_Go.Location = new System.Drawing.Point(655, 310);
            this.button_SOL_Go.Name = "button_SOL_Go";
            this.button_SOL_Go.Size = new System.Drawing.Size(46, 27);
            this.button_SOL_Go.TabIndex = 9;
            this.button_SOL_Go.Text = "Go";
            this.button_SOL_Go.UseVisualStyleBackColor = true;
            this.button_SOL_Go.Click += new System.EventHandler(this.button_SOL_Go_Click);
            // 
            // checkBox_TopMost
            // 
            this.checkBox_TopMost.AutoSize = true;
            this.checkBox_TopMost.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBox_TopMost.ForeColor = System.Drawing.Color.White;
            this.checkBox_TopMost.Location = new System.Drawing.Point(334, 12);
            this.checkBox_TopMost.Name = "checkBox_TopMost";
            this.checkBox_TopMost.Size = new System.Drawing.Size(76, 19);
            this.checkBox_TopMost.TabIndex = 10;
            this.checkBox_TopMost.Text = "Top Most";
            this.checkBox_TopMost.UseVisualStyleBackColor = true;
            this.checkBox_TopMost.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox_HideTaskbar
            // 
            this.checkBox_HideTaskbar.AutoSize = true;
            this.checkBox_HideTaskbar.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBox_HideTaskbar.ForeColor = System.Drawing.Color.White;
            this.checkBox_HideTaskbar.Location = new System.Drawing.Point(444, 12);
            this.checkBox_HideTaskbar.Name = "checkBox_HideTaskbar";
            this.checkBox_HideTaskbar.Size = new System.Drawing.Size(113, 19);
            this.checkBox_HideTaskbar.TabIndex = 11;
            this.checkBox_HideTaskbar.Text = "Hide on taskbar";
            this.checkBox_HideTaskbar.UseVisualStyleBackColor = true;
            this.checkBox_HideTaskbar.CheckedChanged += new System.EventHandler(this.checkBox_HideTaskbar_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(229, 27);
            this.button1.TabIndex = 12;
            this.button1.Text = "Generate Random 12 words";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericUpDown1.Location = new System.Drawing.Point(655, 12);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(46, 23);
            this.numericUpDown1.TabIndex = 13;
            this.numericUpDown1.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(594, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 18);
            this.label1.TabIndex = 14;
            this.label1.Text = "Opacity";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 19);
            this.label2.TabIndex = 15;
            this.label2.Text = "Metamask";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(12, 171);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 19);
            this.label3.TabIndex = 16;
            this.label3.Text = "Tronlink";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(12, 261);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 19);
            this.label4.TabIndex = 17;
            this.label4.Text = "Phantom";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(713, 347);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBox_HideTaskbar);
            this.Controls.Add(this.checkBox_TopMost);
            this.Controls.Add(this.button_SOL_Go);
            this.Controls.Add(this.button_Tron_Go);
            this.Controls.Add(this.button_ETH_Go);
            this.Controls.Add(this.textBox_Tron_Address);
            this.Controls.Add(this.textBox_Tron_PK);
            this.Controls.Add(this.textBox_Sol_Address);
            this.Controls.Add(this.textBox_Sol_PK);
            this.Controls.Add(this.textBox_Eth_Address);
            this.Controls.Add(this.textBox_Eth_PK);
            this.Controls.Add(this.textBox_Seed);
            this.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Seed";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox textBox_Seed;
        private TextBox textBox_Eth_PK;
        private TextBox textBox_Eth_Address;
        private TextBox textBox_Sol_Address;
        private TextBox textBox_Sol_PK;
        private TextBox textBox_Tron_Address;
        private TextBox textBox_Tron_PK;
        private Button button_ETH_Go;
        private Button button_Tron_Go;
        private Button button_SOL_Go;
        private CheckBox checkBox_TopMost;
        private CheckBox checkBox_HideTaskbar;
        private Button button1;
        private NumericUpDown numericUpDown1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}