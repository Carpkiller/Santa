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
            this.SuspendLayout();
            // 
            // button1
            // 
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 272);
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
    }
}

