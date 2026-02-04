namespace BuscadorNC
{
    partial class buscadorNC_MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(buscadorNC_MainForm));
            this.bono_tb = new System.Windows.Forms.TextBox();
            this.consBono_button = new System.Windows.Forms.Button();
            this.progressBar_copyFiles = new System.Windows.Forms.ProgressBar();
            this.label_copiando = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dataGridView_marcasCopiadas = new System.Windows.Forms.DataGridView();
            this.marcas_copiadas = new System.Windows.Forms.Label();
            this.radioButtonIme = new System.Windows.Forms.RadioButton();
            this.radioButtonMade = new System.Windows.Forms.RadioButton();
            this.groupBoxDest = new System.Windows.Forms.GroupBox();
            this.labelBono = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxProy = new System.Windows.Forms.TextBox();
            this.chkPrePaquetes = new System.Windows.Forms.CheckBox();
            this.gbCJSChapas = new System.Windows.Forms.GroupBox();
            this.radioSI = new System.Windows.Forms.RadioButton();
            this.radioNO = new System.Windows.Forms.RadioButton();
            this.radioNA = new System.Windows.Forms.RadioButton();
            this.chkChapas = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_marcasCopiadas)).BeginInit();
            this.groupBoxDest.SuspendLayout();
            this.gbCJSChapas.SuspendLayout();
            this.SuspendLayout();
            // 
            // bono_tb
            // 
            this.bono_tb.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.bono_tb.Location = new System.Drawing.Point(112, 52);
            this.bono_tb.Name = "bono_tb";
            this.bono_tb.Size = new System.Drawing.Size(218, 45);
            this.bono_tb.TabIndex = 0;
            this.bono_tb.TextChanged += new System.EventHandler(this.bono_tb_TextChanged);
            // 
            // consBono_button
            // 
            this.consBono_button.BackColor = System.Drawing.Color.LightBlue;
            this.consBono_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.consBono_button.ForeColor = System.Drawing.Color.Black;
            this.consBono_button.Location = new System.Drawing.Point(153, 229);
            this.consBono_button.Name = "consBono_button";
            this.consBono_button.Size = new System.Drawing.Size(131, 72);
            this.consBono_button.TabIndex = 1;
            this.consBono_button.Text = "Copiar Archivos Bono";
            this.consBono_button.UseVisualStyleBackColor = false;
            this.consBono_button.Click += new System.EventHandler(this.consBono_button_Click);
            // 
            // progressBar_copyFiles
            // 
            this.progressBar_copyFiles.Location = new System.Drawing.Point(91, 349);
            this.progressBar_copyFiles.Name = "progressBar_copyFiles";
            this.progressBar_copyFiles.Size = new System.Drawing.Size(591, 23);
            this.progressBar_copyFiles.TabIndex = 2;
            this.progressBar_copyFiles.Visible = false;
            // 
            // label_copiando
            // 
            this.label_copiando.AutoSize = true;
            this.label_copiando.Location = new System.Drawing.Point(88, 333);
            this.label_copiando.Name = "label_copiando";
            this.label_copiando.Size = new System.Drawing.Size(108, 13);
            this.label_copiando.TabIndex = 3;
            this.label_copiando.Text = "Copiando Archivos ...";
            this.label_copiando.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(675, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(103, 50);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // dataGridView_marcasCopiadas
            // 
            this.dataGridView_marcasCopiadas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_marcasCopiadas.Location = new System.Drawing.Point(264, 427);
            this.dataGridView_marcasCopiadas.Name = "dataGridView_marcasCopiadas";
            this.dataGridView_marcasCopiadas.Size = new System.Drawing.Size(218, 216);
            this.dataGridView_marcasCopiadas.TabIndex = 6;
            this.dataGridView_marcasCopiadas.Visible = false;
            this.dataGridView_marcasCopiadas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_marcasCopiadas_CellClick);
            this.dataGridView_marcasCopiadas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_marcasCopiadas_CellContentClick);
            // 
            // marcas_copiadas
            // 
            this.marcas_copiadas.AutoSize = true;
            this.marcas_copiadas.Location = new System.Drawing.Point(240, 400);
            this.marcas_copiadas.Name = "marcas_copiadas";
            this.marcas_copiadas.Size = new System.Drawing.Size(295, 13);
            this.marcas_copiadas.TabIndex = 7;
            this.marcas_copiadas.Text = "*Marcas Copiadas, pinche sobre ellas para editar su cantidad";
            this.marcas_copiadas.Visible = false;
            // 
            // radioButtonIme
            // 
            this.radioButtonIme.AutoSize = true;
            this.radioButtonIme.Location = new System.Drawing.Point(18, 65);
            this.radioButtonIme.Name = "radioButtonIme";
            this.radioButtonIme.Size = new System.Drawing.Size(80, 17);
            this.radioButtonIme.TabIndex = 8;
            this.radioButtonIme.TabStop = true;
            this.radioButtonIme.Text = "IMEDEXSA";
            this.radioButtonIme.UseVisualStyleBackColor = true;
            this.radioButtonIme.CheckedChanged += new System.EventHandler(this.radioButtonIme_CheckedChanged);
            // 
            // radioButtonMade
            // 
            this.radioButtonMade.AutoSize = true;
            this.radioButtonMade.Checked = true;
            this.radioButtonMade.Location = new System.Drawing.Point(18, 29);
            this.radioButtonMade.Name = "radioButtonMade";
            this.radioButtonMade.Size = new System.Drawing.Size(56, 17);
            this.radioButtonMade.TabIndex = 9;
            this.radioButtonMade.TabStop = true;
            this.radioButtonMade.Text = "MADE";
            this.radioButtonMade.UseVisualStyleBackColor = true;
            this.radioButtonMade.CheckedChanged += new System.EventHandler(this.radioButtonMade_CheckedChanged);
            // 
            // groupBoxDest
            // 
            this.groupBoxDest.Controls.Add(this.radioButtonMade);
            this.groupBoxDest.Controls.Add(this.radioButtonIme);
            this.groupBoxDest.Location = new System.Drawing.Point(282, 114);
            this.groupBoxDest.Name = "groupBoxDest";
            this.groupBoxDest.Size = new System.Drawing.Size(200, 100);
            this.groupBoxDest.TabIndex = 10;
            this.groupBoxDest.TabStop = false;
            this.groupBoxDest.Text = "Destino";
            // 
            // labelBono
            // 
            this.labelBono.AutoSize = true;
            this.labelBono.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBono.Location = new System.Drawing.Point(106, 18);
            this.labelBono.Name = "labelBono";
            this.labelBono.Size = new System.Drawing.Size(77, 31);
            this.labelBono.TabIndex = 11;
            this.labelBono.Text = "Bono";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(431, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 31);
            this.label1.TabIndex = 17;
            this.label1.Text = "Proyecto";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightBlue;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(478, 229);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(131, 72);
            this.button1.TabIndex = 13;
            this.button1.Text = "Copiar Archivos Proyecto";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxProy
            // 
            this.textBoxProy.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.textBoxProy.Location = new System.Drawing.Point(437, 52);
            this.textBoxProy.Name = "textBoxProy";
            this.textBoxProy.Size = new System.Drawing.Size(218, 45);
            this.textBoxProy.TabIndex = 12;
            // 
            // chkPrePaquetes
            // 
            this.chkPrePaquetes.AutoSize = true;
            this.chkPrePaquetes.Location = new System.Drawing.Point(189, 29);
            this.chkPrePaquetes.Name = "chkPrePaquetes";
            this.chkPrePaquetes.Size = new System.Drawing.Size(143, 17);
            this.chkPrePaquetes.TabIndex = 18;
            this.chkPrePaquetes.Text = "Agrupar por prepaquetes";
            this.chkPrePaquetes.UseVisualStyleBackColor = true;
            // 
            // gbCJSChapas
            // 
            this.gbCJSChapas.Controls.Add(this.radioNA);
            this.gbCJSChapas.Controls.Add(this.radioSI);
            this.gbCJSChapas.Controls.Add(this.radioNO);
            this.gbCJSChapas.Location = new System.Drawing.Point(562, 114);
            this.gbCJSChapas.Name = "gbCJSChapas";
            this.gbCJSChapas.Size = new System.Drawing.Size(158, 86);
            this.gbCJSChapas.TabIndex = 11;
            this.gbCJSChapas.TabStop = false;
            this.gbCJSChapas.Text = "¿Pertenece a CJS?";
            this.gbCJSChapas.Visible = false;
            // 
            // radioSI
            // 
            this.radioSI.AutoSize = true;
            this.radioSI.Location = new System.Drawing.Point(18, 23);
            this.radioSI.Name = "radioSI";
            this.radioSI.Size = new System.Drawing.Size(35, 17);
            this.radioSI.TabIndex = 9;
            this.radioSI.Text = "SÍ";
            this.radioSI.UseVisualStyleBackColor = true;
            // 
            // radioNO
            // 
            this.radioNO.AutoSize = true;
            this.radioNO.Location = new System.Drawing.Point(18, 51);
            this.radioNO.Name = "radioNO";
            this.radioNO.Size = new System.Drawing.Size(41, 17);
            this.radioNO.TabIndex = 8;
            this.radioNO.Text = "NO";
            this.radioNO.UseVisualStyleBackColor = true;
            // 
            // radioNA
            // 
            this.radioNA.AutoSize = true;
            this.radioNA.Checked = true;
            this.radioNA.Location = new System.Drawing.Point(92, 35);
            this.radioNA.Name = "radioNA";
            this.radioNA.Size = new System.Drawing.Size(45, 17);
            this.radioNA.TabIndex = 9;
            this.radioNA.TabStop = true;
            this.radioNA.Text = "N/A";
            this.radioNA.UseVisualStyleBackColor = true;
            // 
            // chkChapas
            // 
            this.chkChapas.AutoSize = true;
            this.chkChapas.Location = new System.Drawing.Point(675, 80);
            this.chkChapas.Name = "chkChapas";
            this.chkChapas.Size = new System.Drawing.Size(69, 17);
            this.chkChapas.TabIndex = 19;
            this.chkChapas.Text = "CHAPAS";
            this.chkChapas.UseVisualStyleBackColor = true;
            this.chkChapas.CheckedChanged += new System.EventHandler(this.chkChapas_CheckedChanged);
            // 
            // buscadorNC_MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 678);
            this.Controls.Add(this.chkChapas);
            this.Controls.Add(this.gbCJSChapas);
            this.Controls.Add(this.chkPrePaquetes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxProy);
            this.Controls.Add(this.labelBono);
            this.Controls.Add(this.groupBoxDest);
            this.Controls.Add(this.marcas_copiadas);
            this.Controls.Add(this.dataGridView_marcasCopiadas);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label_copiando);
            this.Controls.Add(this.progressBar_copyFiles);
            this.Controls.Add(this.consBono_button);
            this.Controls.Add(this.bono_tb);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "buscadorNC_MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BuscadorNC";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_marcasCopiadas)).EndInit();
            this.groupBoxDest.ResumeLayout(false);
            this.groupBoxDest.PerformLayout();
            this.gbCJSChapas.ResumeLayout(false);
            this.gbCJSChapas.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox bono_tb;
        private System.Windows.Forms.Button consBono_button;
        private System.Windows.Forms.ProgressBar progressBar_copyFiles;
        private System.Windows.Forms.Label label_copiando;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView dataGridView_marcasCopiadas;
        private System.Windows.Forms.Label marcas_copiadas;
        private System.Windows.Forms.RadioButton radioButtonIme;
        private System.Windows.Forms.RadioButton radioButtonMade;
        private System.Windows.Forms.GroupBox groupBoxDest;
        private System.Windows.Forms.Label labelBono;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxProy;
        private System.Windows.Forms.CheckBox chkPrePaquetes;
        private System.Windows.Forms.GroupBox gbCJSChapas;
        private System.Windows.Forms.RadioButton radioNA;
        private System.Windows.Forms.RadioButton radioSI;
        private System.Windows.Forms.RadioButton radioNO;
        private System.Windows.Forms.CheckBox chkChapas;
    }
}