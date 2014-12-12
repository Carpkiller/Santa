namespace Santa
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxPocetElfov = new System.Windows.Forms.TextBox();
            this.textBoxIndexHracky = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelSimulacnyCas = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxPocetCakajucich = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxParameter2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxParameter1 = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(726, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 40);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxPocetElfov
            // 
            this.textBoxPocetElfov.Location = new System.Drawing.Point(196, 9);
            this.textBoxPocetElfov.Name = "textBoxPocetElfov";
            this.textBoxPocetElfov.Size = new System.Drawing.Size(100, 20);
            this.textBoxPocetElfov.TabIndex = 1;
            // 
            // textBoxIndexHracky
            // 
            this.textBoxIndexHracky.Location = new System.Drawing.Point(238, 43);
            this.textBoxIndexHracky.Name = "textBoxIndexHracky";
            this.textBoxIndexHracky.Size = new System.Drawing.Size(100, 20);
            this.textBoxIndexHracky.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Pocet volnych elfov";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Aktualny index hracky";
            // 
            // labelSimulacnyCas
            // 
            this.labelSimulacnyCas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSimulacnyCas.AutoSize = true;
            this.labelSimulacnyCas.Location = new System.Drawing.Point(12, 250);
            this.labelSimulacnyCas.Name = "labelSimulacnyCas";
            this.labelSimulacnyCas.Size = new System.Drawing.Size(123, 13);
            this.labelSimulacnyCas.TabIndex = 5;
            this.labelSimulacnyCas.Text = "Aktualny simulacny cas :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(204, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Pocet hraciek cakajucich na spracovanie";
            // 
            // textBoxPocetCakajucich
            // 
            this.textBoxPocetCakajucich.Location = new System.Drawing.Point(238, 78);
            this.textBoxPocetCakajucich.Name = "textBoxPocetCakajucich";
            this.textBoxPocetCakajucich.Size = new System.Drawing.Size(100, 20);
            this.textBoxPocetCakajucich.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(494, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Parameter 2 :";
            // 
            // textBoxParameter2
            // 
            this.textBoxParameter2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxParameter2.Location = new System.Drawing.Point(720, 152);
            this.textBoxParameter2.Name = "textBoxParameter2";
            this.textBoxParameter2.Size = new System.Drawing.Size(100, 20);
            this.textBoxParameter2.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(494, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Parameter 1 :";
            // 
            // textBoxParameter1
            // 
            this.textBoxParameter1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxParameter1.Location = new System.Drawing.Point(720, 117);
            this.textBoxParameter1.Name = "textBoxParameter1";
            this.textBoxParameter1.Size = new System.Drawing.Size(100, 20);
            this.textBoxParameter1.TabIndex = 8;
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(497, 81);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(88, 17);
            this.checkBox1.TabIndex = 12;
            this.checkBox1.Text = "Optimalizacia";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOutput.Location = new System.Drawing.Point(15, 117);
            this.textBoxOutput.Multiline = true;
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxOutput.Size = new System.Drawing.Size(473, 121);
            this.textBoxOutput.TabIndex = 13;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(631, 21);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "Test";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 272);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBoxOutput);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxParameter2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxParameter1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxPocetCakajucich);
            this.Controls.Add(this.labelSimulacnyCas);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxIndexHracky);
            this.Controls.Add(this.textBoxPocetElfov);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxPocetElfov;
        private System.Windows.Forms.TextBox textBoxIndexHracky;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelSimulacnyCas;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxPocetCakajucich;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxParameter2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxParameter1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBoxOutput;
        private System.Windows.Forms.Button button2;
    }
}

