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
            textBox_Seed = new TextBox();
            textBox_Eth_PK = new TextBox();
            textBox_Eth_Address = new TextBox();
            textBox_Sol_Address = new TextBox();
            textBox_Sol_PK = new TextBox();
            textBox_Tron_Address = new TextBox();
            textBox_Tron_PK = new TextBox();
            button_ETH_Go = new Button();
            button_Tron_Go = new Button();
            button_SOL_Go = new Button();
            checkBox_TopMost = new CheckBox();
            checkBox_HideTaskbar = new CheckBox();
            button_Random12Words = new Button();
            numericUpDown1 = new NumericUpDown();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label_Btc = new Label();
            button_Btc_1 = new Button();
            textBox_Btc_1 = new TextBox();
            textBox_Btc_PK = new TextBox();
            textBox_Btc_Wif = new TextBox();
            button_Btc_3 = new Button();
            textBox_Btc_3 = new TextBox();
            button_Btc_q = new Button();
            textBox_Btc_q = new TextBox();
            button_Btc_p = new Button();
            textBox_Btc_p = new TextBox();
            radioButton_Btc_44 = new RadioButton();
            radioButton_Btc_84 = new RadioButton();
            button_Clear = new Button();
            checkBox_HideShot = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // textBox_Seed
            // 
            textBox_Seed.Location = new Point(12, 44);
            textBox_Seed.Margin = new Padding(3, 2, 3, 2);
            textBox_Seed.Name = "textBox_Seed";
            textBox_Seed.Size = new Size(689, 25);
            textBox_Seed.TabIndex = 0;
            textBox_Seed.TextAlign = HorizontalAlignment.Center;
            textBox_Seed.TextChanged += textBox_Seed_TextChanged;
            // 
            // textBox_Eth_PK
            // 
            textBox_Eth_PK.Location = new Point(12, 315);
            textBox_Eth_PK.Margin = new Padding(3, 2, 3, 2);
            textBox_Eth_PK.Name = "textBox_Eth_PK";
            textBox_Eth_PK.Size = new Size(689, 25);
            textBox_Eth_PK.TabIndex = 1;
            textBox_Eth_PK.TextAlign = HorizontalAlignment.Center;
            textBox_Eth_PK.TextChanged += textBox_Eth_PK_TextChanged;
            // 
            // textBox_Eth_Address
            // 
            textBox_Eth_Address.Location = new Point(12, 344);
            textBox_Eth_Address.Margin = new Padding(3, 2, 3, 2);
            textBox_Eth_Address.Name = "textBox_Eth_Address";
            textBox_Eth_Address.ReadOnly = true;
            textBox_Eth_Address.Size = new Size(630, 25);
            textBox_Eth_Address.TabIndex = 2;
            textBox_Eth_Address.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox_Sol_Address
            // 
            textBox_Sol_Address.Location = new Point(12, 522);
            textBox_Sol_Address.Margin = new Padding(3, 2, 3, 2);
            textBox_Sol_Address.Name = "textBox_Sol_Address";
            textBox_Sol_Address.ReadOnly = true;
            textBox_Sol_Address.Size = new Size(630, 25);
            textBox_Sol_Address.TabIndex = 4;
            textBox_Sol_Address.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox_Sol_PK
            // 
            textBox_Sol_PK.Location = new Point(12, 493);
            textBox_Sol_PK.Margin = new Padding(3, 2, 3, 2);
            textBox_Sol_PK.Name = "textBox_Sol_PK";
            textBox_Sol_PK.Size = new Size(689, 25);
            textBox_Sol_PK.TabIndex = 3;
            textBox_Sol_PK.TextAlign = HorizontalAlignment.Center;
            textBox_Sol_PK.TextChanged += textBox_Sol_PK_TextChanged;
            // 
            // textBox_Tron_Address
            // 
            textBox_Tron_Address.Location = new Point(12, 435);
            textBox_Tron_Address.Margin = new Padding(3, 2, 3, 2);
            textBox_Tron_Address.Name = "textBox_Tron_Address";
            textBox_Tron_Address.ReadOnly = true;
            textBox_Tron_Address.Size = new Size(630, 25);
            textBox_Tron_Address.TabIndex = 6;
            textBox_Tron_Address.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox_Tron_PK
            // 
            textBox_Tron_PK.Location = new Point(12, 406);
            textBox_Tron_PK.Margin = new Padding(3, 2, 3, 2);
            textBox_Tron_PK.Name = "textBox_Tron_PK";
            textBox_Tron_PK.Size = new Size(689, 25);
            textBox_Tron_PK.TabIndex = 5;
            textBox_Tron_PK.TextAlign = HorizontalAlignment.Center;
            textBox_Tron_PK.TextChanged += textBox_Tron_PK_TextChanged;
            // 
            // button_ETH_Go
            // 
            button_ETH_Go.Location = new Point(647, 343);
            button_ETH_Go.Name = "button_ETH_Go";
            button_ETH_Go.Size = new Size(54, 27);
            button_ETH_Go.TabIndex = 7;
            button_ETH_Go.Text = "Go";
            button_ETH_Go.UseVisualStyleBackColor = true;
            button_ETH_Go.Click += button_ETH_Go_Click;
            // 
            // button_Tron_Go
            // 
            button_Tron_Go.Location = new Point(647, 434);
            button_Tron_Go.Name = "button_Tron_Go";
            button_Tron_Go.Size = new Size(54, 27);
            button_Tron_Go.TabIndex = 8;
            button_Tron_Go.Text = "Go";
            button_Tron_Go.UseVisualStyleBackColor = true;
            button_Tron_Go.Click += button_Tron_Go_Click;
            // 
            // button_SOL_Go
            // 
            button_SOL_Go.Location = new Point(647, 521);
            button_SOL_Go.Name = "button_SOL_Go";
            button_SOL_Go.Size = new Size(54, 27);
            button_SOL_Go.TabIndex = 9;
            button_SOL_Go.Text = "Go";
            button_SOL_Go.UseVisualStyleBackColor = true;
            button_SOL_Go.Click += button_SOL_Go_Click;
            // 
            // checkBox_TopMost
            // 
            checkBox_TopMost.AutoSize = true;
            checkBox_TopMost.Font = new Font("Calibri", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            checkBox_TopMost.ForeColor = Color.White;
            checkBox_TopMost.Location = new Point(252, 12);
            checkBox_TopMost.Name = "checkBox_TopMost";
            checkBox_TopMost.Size = new Size(76, 19);
            checkBox_TopMost.TabIndex = 10;
            checkBox_TopMost.Text = "Top Most";
            checkBox_TopMost.UseVisualStyleBackColor = true;
            checkBox_TopMost.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // checkBox_HideTaskbar
            // 
            checkBox_HideTaskbar.AutoSize = true;
            checkBox_HideTaskbar.Font = new Font("Calibri", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            checkBox_HideTaskbar.ForeColor = Color.White;
            checkBox_HideTaskbar.Location = new Point(344, 12);
            checkBox_HideTaskbar.Name = "checkBox_HideTaskbar";
            checkBox_HideTaskbar.Size = new Size(113, 19);
            checkBox_HideTaskbar.TabIndex = 11;
            checkBox_HideTaskbar.Text = "Hide on taskbar";
            checkBox_HideTaskbar.UseVisualStyleBackColor = true;
            checkBox_HideTaskbar.CheckedChanged += checkBox_HideTaskbar_CheckedChanged;
            // 
            // button_Random12Words
            // 
            button_Random12Words.Location = new Point(12, 12);
            button_Random12Words.Name = "button_Random12Words";
            button_Random12Words.Size = new Size(137, 27);
            button_Random12Words.TabIndex = 12;
            button_Random12Words.Text = "Random 12 words";
            button_Random12Words.UseVisualStyleBackColor = true;
            button_Random12Words.Click += button_Random12Words_Click;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            numericUpDown1.Location = new Point(655, 12);
            numericUpDown1.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(46, 23);
            numericUpDown1.TabIndex = 13;
            numericUpDown1.Value = new decimal(new int[] { 100, 0, 0, 0 });
            numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Calibri", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.White;
            label1.Location = new Point(594, 14);
            label1.Name = "label1";
            label1.Size = new Size(55, 18);
            label1.TabIndex = 14;
            label1.Text = "Opacity";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.White;
            label2.Location = new Point(12, 295);
            label2.Name = "label2";
            label2.Size = new Size(76, 19);
            label2.TabIndex = 15;
            label2.Text = "ETH + BSC";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = Color.White;
            label3.Location = new Point(12, 385);
            label3.Name = "label3";
            label3.Size = new Size(34, 19);
            label3.TabIndex = 16;
            label3.Text = "TRX";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = Color.White;
            label4.Location = new Point(12, 472);
            label4.Name = "label4";
            label4.Size = new Size(34, 19);
            label4.TabIndex = 17;
            label4.Text = "SOL";
            // 
            // label_Btc
            // 
            label_Btc.AutoSize = true;
            label_Btc.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label_Btc.ForeColor = Color.White;
            label_Btc.Location = new Point(12, 83);
            label_Btc.Name = "label_Btc";
            label_Btc.Size = new Size(35, 19);
            label_Btc.TabIndex = 21;
            label_Btc.Text = "BTC";
            // 
            // button_Btc_1
            // 
            button_Btc_1.Location = new Point(647, 164);
            button_Btc_1.Name = "button_Btc_1";
            button_Btc_1.Size = new Size(54, 27);
            button_Btc_1.TabIndex = 20;
            button_Btc_1.Text = "Go";
            button_Btc_1.UseVisualStyleBackColor = true;
            button_Btc_1.Click += button_Btc_1_Click;
            // 
            // textBox_Btc_1
            // 
            textBox_Btc_1.Location = new Point(12, 165);
            textBox_Btc_1.Margin = new Padding(3, 2, 3, 2);
            textBox_Btc_1.Name = "textBox_Btc_1";
            textBox_Btc_1.ReadOnly = true;
            textBox_Btc_1.Size = new Size(630, 25);
            textBox_Btc_1.TabIndex = 19;
            textBox_Btc_1.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox_Btc_PK
            // 
            textBox_Btc_PK.Location = new Point(12, 104);
            textBox_Btc_PK.Margin = new Padding(3, 2, 3, 2);
            textBox_Btc_PK.Name = "textBox_Btc_PK";
            textBox_Btc_PK.Size = new Size(689, 25);
            textBox_Btc_PK.TabIndex = 18;
            textBox_Btc_PK.TextAlign = HorizontalAlignment.Center;
            textBox_Btc_PK.TextChanged += textBox_Btc_PK_TextChanged;
            // 
            // textBox_Btc_Wif
            // 
            textBox_Btc_Wif.Location = new Point(12, 134);
            textBox_Btc_Wif.Margin = new Padding(3, 2, 3, 2);
            textBox_Btc_Wif.Name = "textBox_Btc_Wif";
            textBox_Btc_Wif.Size = new Size(689, 25);
            textBox_Btc_Wif.TabIndex = 22;
            textBox_Btc_Wif.TextAlign = HorizontalAlignment.Center;
            textBox_Btc_Wif.TextChanged += textBox_Btc_Wif_TextChanged;
            // 
            // button_Btc_3
            // 
            button_Btc_3.Location = new Point(647, 194);
            button_Btc_3.Name = "button_Btc_3";
            button_Btc_3.Size = new Size(54, 27);
            button_Btc_3.TabIndex = 24;
            button_Btc_3.Text = "Go";
            button_Btc_3.UseVisualStyleBackColor = true;
            button_Btc_3.Click += button_Btc_3_Click;
            // 
            // textBox_Btc_3
            // 
            textBox_Btc_3.Location = new Point(12, 195);
            textBox_Btc_3.Margin = new Padding(3, 2, 3, 2);
            textBox_Btc_3.Name = "textBox_Btc_3";
            textBox_Btc_3.ReadOnly = true;
            textBox_Btc_3.Size = new Size(630, 25);
            textBox_Btc_3.TabIndex = 23;
            textBox_Btc_3.TextAlign = HorizontalAlignment.Center;
            // 
            // button_Btc_q
            // 
            button_Btc_q.Location = new Point(647, 224);
            button_Btc_q.Name = "button_Btc_q";
            button_Btc_q.Size = new Size(54, 27);
            button_Btc_q.TabIndex = 26;
            button_Btc_q.Text = "Go";
            button_Btc_q.UseVisualStyleBackColor = true;
            button_Btc_q.Click += button_Btc_q_Click;
            // 
            // textBox_Btc_q
            // 
            textBox_Btc_q.Location = new Point(12, 225);
            textBox_Btc_q.Margin = new Padding(3, 2, 3, 2);
            textBox_Btc_q.Name = "textBox_Btc_q";
            textBox_Btc_q.ReadOnly = true;
            textBox_Btc_q.Size = new Size(630, 25);
            textBox_Btc_q.TabIndex = 25;
            textBox_Btc_q.TextAlign = HorizontalAlignment.Center;
            // 
            // button_Btc_p
            // 
            button_Btc_p.Location = new Point(647, 254);
            button_Btc_p.Name = "button_Btc_p";
            button_Btc_p.Size = new Size(54, 27);
            button_Btc_p.TabIndex = 28;
            button_Btc_p.Text = "Go";
            button_Btc_p.UseVisualStyleBackColor = true;
            button_Btc_p.Click += button_Btc_p_Click;
            // 
            // textBox_Btc_p
            // 
            textBox_Btc_p.Location = new Point(12, 255);
            textBox_Btc_p.Margin = new Padding(3, 2, 3, 2);
            textBox_Btc_p.Name = "textBox_Btc_p";
            textBox_Btc_p.ReadOnly = true;
            textBox_Btc_p.Size = new Size(630, 25);
            textBox_Btc_p.TabIndex = 27;
            textBox_Btc_p.TextAlign = HorizontalAlignment.Center;
            // 
            // radioButton_Btc_44
            // 
            radioButton_Btc_44.AutoSize = true;
            radioButton_Btc_44.ForeColor = Color.White;
            radioButton_Btc_44.Location = new Point(607, 77);
            radioButton_Btc_44.Name = "radioButton_Btc_44";
            radioButton_Btc_44.Size = new Size(42, 22);
            radioButton_Btc_44.TabIndex = 29;
            radioButton_Btc_44.Text = "44";
            radioButton_Btc_44.UseVisualStyleBackColor = true;
            radioButton_Btc_44.CheckedChanged += radioButton_Btc_44_CheckedChanged;
            // 
            // radioButton_Btc_84
            // 
            radioButton_Btc_84.AutoSize = true;
            radioButton_Btc_84.Checked = true;
            radioButton_Btc_84.ForeColor = Color.White;
            radioButton_Btc_84.Location = new Point(659, 77);
            radioButton_Btc_84.Name = "radioButton_Btc_84";
            radioButton_Btc_84.Size = new Size(42, 22);
            radioButton_Btc_84.TabIndex = 30;
            radioButton_Btc_84.TabStop = true;
            radioButton_Btc_84.Text = "84";
            radioButton_Btc_84.UseVisualStyleBackColor = true;
            radioButton_Btc_84.CheckedChanged += radioButton_Btc_84_CheckedChanged;
            // 
            // button_Clear
            // 
            button_Clear.Location = new Point(155, 12);
            button_Clear.Name = "button_Clear";
            button_Clear.Size = new Size(58, 27);
            button_Clear.TabIndex = 31;
            button_Clear.Text = "Clear";
            button_Clear.UseVisualStyleBackColor = true;
            button_Clear.Click += button_Clear_Click;
            // 
            // checkBox_HideShot
            // 
            checkBox_HideShot.AutoSize = true;
            checkBox_HideShot.Checked = true;
            checkBox_HideShot.CheckState = CheckState.Checked;
            checkBox_HideShot.Font = new Font("Calibri", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            checkBox_HideShot.ForeColor = Color.White;
            checkBox_HideShot.Location = new Point(473, 12);
            checkBox_HideShot.Name = "checkBox_HideShot";
            checkBox_HideShot.Size = new Size(97, 19);
            checkBox_HideShot.TabIndex = 32;
            checkBox_HideShot.Text = "Hide window";
            checkBox_HideShot.UseVisualStyleBackColor = true;
            checkBox_HideShot.CheckedChanged += checkBox_HideShot_CheckedChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(713, 559);
            Controls.Add(checkBox_HideShot);
            Controls.Add(button_Clear);
            Controls.Add(radioButton_Btc_84);
            Controls.Add(radioButton_Btc_44);
            Controls.Add(button_Btc_p);
            Controls.Add(textBox_Btc_p);
            Controls.Add(button_Btc_q);
            Controls.Add(textBox_Btc_q);
            Controls.Add(button_Btc_3);
            Controls.Add(textBox_Btc_3);
            Controls.Add(textBox_Btc_Wif);
            Controls.Add(label_Btc);
            Controls.Add(button_Btc_1);
            Controls.Add(textBox_Btc_1);
            Controls.Add(textBox_Btc_PK);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(numericUpDown1);
            Controls.Add(button_Random12Words);
            Controls.Add(checkBox_HideTaskbar);
            Controls.Add(checkBox_TopMost);
            Controls.Add(button_SOL_Go);
            Controls.Add(button_Tron_Go);
            Controls.Add(button_ETH_Go);
            Controls.Add(textBox_Tron_Address);
            Controls.Add(textBox_Tron_PK);
            Controls.Add(textBox_Sol_Address);
            Controls.Add(textBox_Sol_PK);
            Controls.Add(textBox_Eth_Address);
            Controls.Add(textBox_Eth_PK);
            Controls.Add(textBox_Seed);
            Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "Form1";
            Text = "Seed";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
        private Button button_Random12Words;
        private NumericUpDown numericUpDown1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label_Btc;
        private Button button_Btc_1;
        private TextBox textBox_Btc_1;
        private TextBox textBox_Btc_PK;
        private TextBox textBox_Btc_Wif;
        private Button button_Btc_3;
        private TextBox textBox_Btc_3;
        private Button button_Btc_q;
        private TextBox textBox_Btc_q;
        private Button button_Btc_p;
        private TextBox textBox_Btc_p;
        private RadioButton radioButton_Btc_44;
        private RadioButton radioButton_Btc_84;
        private Button button_Clear;
        private CheckBox checkBox_HideShot;
    }
}