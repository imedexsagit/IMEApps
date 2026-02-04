
namespace PackingNuevo
{
    partial class Prin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Prin));
            this.lblPDA = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbOperario1 = new System.Windows.Forms.ComboBox();
            this.btnMontaje = new System.Windows.Forms.Button();
            this.btnCarga = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbPacking = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbOperario2 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label_empresa = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDcha = new System.Windows.Forms.Button();
            this.btnIzda = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPDA
            // 
            this.lblPDA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPDA.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.lblPDA.Location = new System.Drawing.Point(80, 521);
            this.lblPDA.Name = "lblPDA";
            this.lblPDA.Size = new System.Drawing.Size(93, 31);
            this.lblPDA.TabIndex = 7;
            this.lblPDA.Text = "PDASC";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.label2.Location = new System.Drawing.Point(19, 521);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 31);
            this.label2.TabIndex = 8;
            this.label2.Text = "DISP.:";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 35F);
            this.label8.Location = new System.Drawing.Point(12, 126);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(271, 85);
            this.label8.TabIndex = 6;
            this.label8.Text = "Operario 1:";
            // 
            // cmbOperario1
            // 
            this.cmbOperario1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbOperario1.DropDownHeight = 500;
            this.cmbOperario1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOperario1.Font = new System.Drawing.Font("Segoe UI", 30F);
            this.cmbOperario1.IntegralHeight = false;
            this.cmbOperario1.Location = new System.Drawing.Point(289, 131);
            this.cmbOperario1.Name = "cmbOperario1";
            this.cmbOperario1.Size = new System.Drawing.Size(592, 62);
            this.cmbOperario1.TabIndex = 2;
            // 
            // btnMontaje
            // 
            this.btnMontaje.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMontaje.Font = new System.Drawing.Font("Tahoma", 35F, System.Drawing.FontStyle.Bold);
            this.btnMontaje.Location = new System.Drawing.Point(24, 368);
            this.btnMontaje.Name = "btnMontaje";
            this.btnMontaje.Size = new System.Drawing.Size(286, 127);
            this.btnMontaje.TabIndex = 4;
            this.btnMontaje.Text = "MONTAJE";
            this.btnMontaje.Click += new System.EventHandler(this.btnMontaje_Click);
            // 
            // btnCarga
            // 
            this.btnCarga.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCarga.Font = new System.Drawing.Font("Tahoma", 40F, System.Drawing.FontStyle.Bold);
            this.btnCarga.Location = new System.Drawing.Point(370, 368);
            this.btnCarga.Name = "btnCarga";
            this.btnCarga.Size = new System.Drawing.Size(279, 127);
            this.btnCarga.TabIndex = 5;
            this.btnCarga.Text = "CARGA";
            this.btnCarga.Click += new System.EventHandler(this.btnCarga_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 35F);
            this.label1.Location = new System.Drawing.Point(24, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(225, 72);
            this.label1.TabIndex = 4;
            this.label1.Text = "PACKING:";
            // 
            // cmbPacking
            // 
            this.cmbPacking.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPacking.DropDownHeight = 500;
            this.cmbPacking.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPacking.Font = new System.Drawing.Font("Segoe UI", 30F);
            this.cmbPacking.IntegralHeight = false;
            this.cmbPacking.Location = new System.Drawing.Point(255, 27);
            this.cmbPacking.Name = "cmbPacking";
            this.cmbPacking.Size = new System.Drawing.Size(638, 62);
            this.cmbPacking.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 35F);
            this.label4.Location = new System.Drawing.Point(12, 239);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(271, 81);
            this.label4.TabIndex = 2;
            this.label4.Text = "Operario 2:";
            // 
            // cmbOperario2
            // 
            this.cmbOperario2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbOperario2.DropDownHeight = 500;
            this.cmbOperario2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOperario2.Font = new System.Drawing.Font("Segoe UI", 30F);
            this.cmbOperario2.IntegralHeight = false;
            this.cmbOperario2.Location = new System.Drawing.Point(289, 258);
            this.cmbOperario2.Name = "cmbOperario2";
            this.cmbOperario2.Size = new System.Drawing.Size(592, 62);
            this.cmbOperario2.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.label3.Location = new System.Drawing.Point(608, 523);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 32);
            this.label3.TabIndex = 1;
            this.label3.Text = "EMPRESA:";
            // 
            // label_empresa
            // 
            this.label_empresa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label_empresa.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.label_empresa.Location = new System.Drawing.Point(717, 523);
            this.label_empresa.Name = "label_empresa";
            this.label_empresa.Size = new System.Drawing.Size(107, 29);
            this.label_empresa.TabIndex = 0;
            this.label_empresa.Text = "-";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.button1.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(730, 418);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(197, 77);
            this.button1.TabIndex = 9;
            this.button1.Text = "CONSULTAR CAMIONES";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnDown);
            this.panel1.Controls.Add(this.btnUp);
            this.panel1.Controls.Add(this.btnDcha);
            this.panel1.Controls.Add(this.btnIzda);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btnCerrar);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(24, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(903, 506);
            this.panel1.TabIndex = 12;
            this.panel1.Visible = false;
            // 
            // btnDown
            // 
            this.btnDown.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDown.BackgroundImage")));
            this.btnDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDown.Location = new System.Drawing.Point(805, 258);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(82, 157);
            this.btnDown.TabIndex = 6;
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUp.BackgroundImage")));
            this.btnUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnUp.Location = new System.Drawing.Point(805, 82);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(82, 157);
            this.btnUp.TabIndex = 5;
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDcha
            // 
            this.btnDcha.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDcha.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDcha.BackgroundImage")));
            this.btnDcha.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDcha.Location = new System.Drawing.Point(466, 423);
            this.btnDcha.Name = "btnDcha";
            this.btnDcha.Size = new System.Drawing.Size(421, 75);
            this.btnDcha.TabIndex = 4;
            this.btnDcha.UseVisualStyleBackColor = true;
            this.btnDcha.Click += new System.EventHandler(this.btnDcha_Click);
            // 
            // btnIzda
            // 
            this.btnIzda.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnIzda.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnIzda.BackgroundImage")));
            this.btnIzda.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnIzda.Location = new System.Drawing.Point(17, 423);
            this.btnIzda.Name = "btnIzda";
            this.btnIzda.Size = new System.Drawing.Size(421, 75);
            this.btnIzda.TabIndex = 3;
            this.btnIzda.UseVisualStyleBackColor = true;
            this.btnIzda.Click += new System.EventHandler(this.btnIzda_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 25F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(10, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(738, 46);
            this.label5.TabIndex = 2;
            this.label5.Text = "FICHAJES MONTAJE Y CARGA DE CAMIONES";
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.BackColor = System.Drawing.Color.Red;
            this.btnCerrar.Font = new System.Drawing.Font("Segoe UI", 25F, System.Drawing.FontStyle.Bold);
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(805, 9);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(82, 54);
            this.btnCerrar.TabIndex = 1;
            this.btnCerrar.Text = "X";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click_1);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(18, 82);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(781, 333);
            this.dataGridView1.TabIndex = 0;
            // 
            // Prin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(939, 565);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label_empresa);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbOperario2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbPacking);
            this.Controls.Add(this.btnCarga);
            this.Controls.Add(this.btnMontaje);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmbOperario1);
            this.Controls.Add(this.lblPDA);
            this.Controls.Add(this.label2);
            this.MinimizeBox = false;
            this.Name = "Prin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Packing";
            this.Load += new System.EventHandler(this.Prin_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblPDA;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbOperario1;
        private System.Windows.Forms.Button btnMontaje;
        private System.Windows.Forms.Button btnCarga;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbPacking;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbOperario2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_empresa;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnDcha;
        private System.Windows.Forms.Button btnIzda;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
    }
}

