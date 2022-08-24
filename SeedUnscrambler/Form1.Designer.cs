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
            this.SuspendLayout();
            // 
            // textBox_Seed
            // 
            this.textBox_Seed.Location = new System.Drawing.Point(12, 11);
            this.textBox_Seed.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_Seed.Name = "textBox_Seed";
            this.textBox_Seed.Size = new System.Drawing.Size(667, 25);
            this.textBox_Seed.TabIndex = 0;
            this.textBox_Seed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_Seed.TextChanged += new System.EventHandler(this.textBox_Seed_TextChanged);
            // 
            // textBox_Eth_PK
            // 
            this.textBox_Eth_PK.Location = new System.Drawing.Point(12, 46);
            this.textBox_Eth_PK.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_Eth_PK.Name = "textBox_Eth_PK";
            this.textBox_Eth_PK.Size = new System.Drawing.Size(689, 25);
            this.textBox_Eth_PK.TabIndex = 1;
            this.textBox_Eth_PK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_Eth_Address
            // 
            this.textBox_Eth_Address.Location = new System.Drawing.Point(12, 75);
            this.textBox_Eth_Address.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_Eth_Address.Name = "textBox_Eth_Address";
            this.textBox_Eth_Address.Size = new System.Drawing.Size(637, 25);
            this.textBox_Eth_Address.TabIndex = 2;
            this.textBox_Eth_Address.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_Sol_Address
            // 
            this.textBox_Sol_Address.Location = new System.Drawing.Point(12, 203);
            this.textBox_Sol_Address.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_Sol_Address.Name = "textBox_Sol_Address";
            this.textBox_Sol_Address.Size = new System.Drawing.Size(637, 25);
            this.textBox_Sol_Address.TabIndex = 4;
            this.textBox_Sol_Address.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_Sol_PK
            // 
            this.textBox_Sol_PK.Location = new System.Drawing.Point(12, 174);
            this.textBox_Sol_PK.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_Sol_PK.Name = "textBox_Sol_PK";
            this.textBox_Sol_PK.Size = new System.Drawing.Size(689, 25);
            this.textBox_Sol_PK.TabIndex = 3;
            this.textBox_Sol_PK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_Tron_Address
            // 
            this.textBox_Tron_Address.Location = new System.Drawing.Point(12, 139);
            this.textBox_Tron_Address.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_Tron_Address.Name = "textBox_Tron_Address";
            this.textBox_Tron_Address.Size = new System.Drawing.Size(637, 25);
            this.textBox_Tron_Address.TabIndex = 6;
            this.textBox_Tron_Address.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_Tron_PK
            // 
            this.textBox_Tron_PK.Location = new System.Drawing.Point(12, 110);
            this.textBox_Tron_PK.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_Tron_PK.Name = "textBox_Tron_PK";
            this.textBox_Tron_PK.Size = new System.Drawing.Size(689, 25);
            this.textBox_Tron_PK.TabIndex = 5;
            this.textBox_Tron_PK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button_ETH_Go
            // 
            this.button_ETH_Go.Location = new System.Drawing.Point(655, 74);
            this.button_ETH_Go.Name = "button_ETH_Go";
            this.button_ETH_Go.Size = new System.Drawing.Size(46, 27);
            this.button_ETH_Go.TabIndex = 7;
            this.button_ETH_Go.Text = "Go";
            this.button_ETH_Go.UseVisualStyleBackColor = true;
            this.button_ETH_Go.Click += new System.EventHandler(this.button_ETH_Go_Click);
            // 
            // button_Tron_Go
            // 
            this.button_Tron_Go.Location = new System.Drawing.Point(655, 138);
            this.button_Tron_Go.Name = "button_Tron_Go";
            this.button_Tron_Go.Size = new System.Drawing.Size(46, 27);
            this.button_Tron_Go.TabIndex = 8;
            this.button_Tron_Go.Text = "Go";
            this.button_Tron_Go.UseVisualStyleBackColor = true;
            this.button_Tron_Go.Click += new System.EventHandler(this.button_Tron_Go_Click);
            // 
            // button_SOL_Go
            // 
            this.button_SOL_Go.Location = new System.Drawing.Point(655, 202);
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
            this.checkBox_TopMost.Location = new System.Drawing.Point(686, 9);
            this.checkBox_TopMost.Name = "checkBox_TopMost";
            this.checkBox_TopMost.Size = new System.Drawing.Size(15, 14);
            this.checkBox_TopMost.TabIndex = 10;
            this.checkBox_TopMost.UseVisualStyleBackColor = true;
            this.checkBox_TopMost.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox_HideTaskbar
            // 
            this.checkBox_HideTaskbar.AutoSize = true;
            this.checkBox_HideTaskbar.Location = new System.Drawing.Point(686, 24);
            this.checkBox_HideTaskbar.Name = "checkBox_HideTaskbar";
            this.checkBox_HideTaskbar.Size = new System.Drawing.Size(15, 14);
            this.checkBox_HideTaskbar.TabIndex = 11;
            this.checkBox_HideTaskbar.UseVisualStyleBackColor = true;
            this.checkBox_HideTaskbar.CheckedChanged += new System.EventHandler(this.checkBox_HideTaskbar_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(713, 240);
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
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Seed";
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
    }
}