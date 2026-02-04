namespace ProyectosOnLine
{
    partial class Form11informe
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnExportarGeneral = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.chkRapido = new System.Windows.Forms.CheckBox();
            this.btnExpFormato = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 65);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(1265, 693);
            this.dataGridView1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Lime;
            this.button1.Location = new System.Drawing.Point(927, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(157, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "EXPORTAR A EXCEL";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Lime;
            this.button2.Location = new System.Drawing.Point(764, 9);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(157, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "EXPORTAR A EXCEL";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Lime;
            this.button3.Location = new System.Drawing.Point(1090, 10);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(157, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "EXPORTAR A EXCEL";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnExportarGeneral
            // 
            this.btnExportarGeneral.BackColor = System.Drawing.Color.Lime;
            this.btnExportarGeneral.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportarGeneral.Location = new System.Drawing.Point(12, 25);
            this.btnExportarGeneral.Name = "btnExportarGeneral";
            this.btnExportarGeneral.Size = new System.Drawing.Size(157, 34);
            this.btnExportarGeneral.TabIndex = 4;
            this.btnExportarGeneral.Text = "EXPORTACIÓN RÁPIDA";
            this.btnExportarGeneral.UseVisualStyleBackColor = false;
            this.btnExportarGeneral.Click += new System.EventHandler(this.btnExportarGeneral_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Location = new System.Drawing.Point(412, 308);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(529, 120);
            this.panel1.TabIndex = 5;
            this.panel1.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(141, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 39);
            this.label1.TabIndex = 1;
            this.label1.Text = "EXPORTANDO";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(38, 58);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(462, 40);
            this.progressBar1.TabIndex = 0;
            // 
            // chkRapido
            // 
            this.chkRapido.AutoSize = true;
            this.chkRapido.Checked = true;
            this.chkRapido.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRapido.Location = new System.Drawing.Point(1101, 42);
            this.chkRapido.Name = "chkRapido";
            this.chkRapido.Size = new System.Drawing.Size(146, 17);
            this.chkRapido.TabIndex = 6;
            this.chkRapido.Text = "EXPORTACIÓN RÁPIDA";
            this.chkRapido.UseVisualStyleBackColor = true;
            this.chkRapido.Visible = false;
            // 
            // btnExpFormato
            // 
            this.btnExpFormato.BackColor = System.Drawing.Color.Cyan;
            this.btnExpFormato.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExpFormato.Location = new System.Drawing.Point(175, 25);
            this.btnExpFormato.Name = "btnExpFormato";
            this.btnExpFormato.Size = new System.Drawing.Size(177, 34);
            this.btnExpFormato.TabIndex = 4;
            this.btnExpFormato.Text = "EXPORTACIÓN CON FORMATO";
            this.btnExpFormato.UseVisualStyleBackColor = false;
            this.btnExpFormato.Click += new System.EventHandler(this.btnExpFormato_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(187, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "EXPORTACIÓN A EXCEL";
            // 
            // Form11informe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1289, 770);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkRapido);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnExpFormato);
            this.Controls.Add(this.btnExportarGeneral);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form11informe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnExportarGeneral;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkRapido;
        private System.Windows.Forms.Button btnExpFormato;
        private System.Windows.Forms.Label label2;
    }
}