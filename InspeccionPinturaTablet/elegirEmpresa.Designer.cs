namespace InspeccionPinturaTablet
{
    partial class elegirEmpresa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(elegirEmpresa));
            this.button_Made = new System.Windows.Forms.Button();
            this.radioES = new System.Windows.Forms.RadioButton();
            this.radioEN = new System.Windows.Forms.RadioButton();
            this.radioFR = new System.Windows.Forms.RadioButton();
            this.radioDE = new System.Windows.Forms.RadioButton();
            this.button_Ime = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_Made
            // 
            this.button_Made.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_Made.Image = ((System.Drawing.Image)(resources.GetObject("button_Made.Image")));
            this.button_Made.Location = new System.Drawing.Point(226, 23);
            this.button_Made.Name = "button_Made";
            this.button_Made.Size = new System.Drawing.Size(189, 156);
            this.button_Made.TabIndex = 5;
            this.button_Made.UseVisualStyleBackColor = true;
            this.button_Made.Visible = false;
            // 
            // radioES
            // 
            this.radioES.AutoSize = true;
            this.radioES.Location = new System.Drawing.Point(21, 202);
            this.radioES.Name = "radioES";
            this.radioES.Size = new System.Drawing.Size(75, 17);
            this.radioES.TabIndex = 6;
            this.radioES.Text = "ESPAÑOL";
            this.radioES.UseVisualStyleBackColor = true;
            this.radioES.Visible = false;
            this.radioES.CheckedChanged += new System.EventHandler(this.radioES_CheckedChanged);
            // 
            // radioEN
            // 
            this.radioEN.AutoSize = true;
            this.radioEN.Location = new System.Drawing.Point(329, 202);
            this.radioEN.Name = "radioEN";
            this.radioEN.Size = new System.Drawing.Size(64, 17);
            this.radioEN.TabIndex = 7;
            this.radioEN.Text = "INGLÉS";
            this.radioEN.UseVisualStyleBackColor = true;
            this.radioEN.Visible = false;
            this.radioEN.CheckedChanged += new System.EventHandler(this.radioEN_CheckedChanged);
            // 
            // radioFR
            // 
            this.radioFR.AutoSize = true;
            this.radioFR.Location = new System.Drawing.Point(226, 202);
            this.radioFR.Name = "radioFR";
            this.radioFR.Size = new System.Drawing.Size(75, 17);
            this.radioFR.TabIndex = 8;
            this.radioFR.Text = "FRANCÉS";
            this.radioFR.UseVisualStyleBackColor = true;
            this.radioFR.CheckedChanged += new System.EventHandler(this.radioFR_CheckedChanged);
            // 
            // radioDE
            // 
            this.radioDE.AutoSize = true;
            this.radioDE.Checked = true;
            this.radioDE.Location = new System.Drawing.Point(132, 202);
            this.radioDE.Name = "radioDE";
            this.radioDE.Size = new System.Drawing.Size(69, 17);
            this.radioDE.TabIndex = 9;
            this.radioDE.TabStop = true;
            this.radioDE.Text = "ALEMÁN";
            this.radioDE.UseVisualStyleBackColor = true;
            this.radioDE.CheckedChanged += new System.EventHandler(this.radioDE_CheckedChanged);
            // 
            // button_Ime
            // 
            this.button_Ime.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_Ime.Image = ((System.Drawing.Image)(resources.GetObject("button_Ime.Image")));
            this.button_Ime.Location = new System.Drawing.Point(121, 24);
            this.button_Ime.Name = "button_Ime";
            this.button_Ime.Size = new System.Drawing.Size(189, 155);
            this.button_Ime.TabIndex = 4;
            this.button_Ime.UseVisualStyleBackColor = true;
            this.button_Ime.Click += new System.EventHandler(this.button_Ime_Click);
            // 
            // elegirEmpresa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 231);
            this.Controls.Add(this.radioDE);
            this.Controls.Add(this.radioFR);
            this.Controls.Add(this.radioEN);
            this.Controls.Add(this.radioES);
            this.Controls.Add(this.button_Made);
            this.Controls.Add(this.button_Ime);
            this.Name = "elegirEmpresa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ELEGIR EMPRESA";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Made;
        private System.Windows.Forms.RadioButton radioES;
        private System.Windows.Forms.RadioButton radioEN;
        private System.Windows.Forms.RadioButton radioFR;
        private System.Windows.Forms.RadioButton radioDE;
        private System.Windows.Forms.Button button_Ime;
    }
}