namespace EstadoTaller
{
    partial class PantallaCarga
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
            this.pBCargar = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pBCargar)).BeginInit();
            this.SuspendLayout();
            // 
            // pBCargar
            // 
            this.pBCargar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pBCargar.Image = global::EstadoTaller.Properties.Resources.cargar;
            this.pBCargar.Location = new System.Drawing.Point(0, 0);
            this.pBCargar.Name = "pBCargar";
            this.pBCargar.Size = new System.Drawing.Size(200, 200);
            this.pBCargar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pBCargar.TabIndex = 0;
            this.pBCargar.TabStop = false;
            // 
            // PantallaCarga
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(200, 200);
            this.Controls.Add(this.pBCargar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PantallaCarga";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PantallaCarga";
            ((System.ComponentModel.ISupportInitialize)(this.pBCargar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pBCargar;

    }
}