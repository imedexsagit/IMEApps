namespace TennetPintura
{
    partial class tennetPintura
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(tennetPintura));
            this.dataGridViewTennetPaint = new System.Windows.Forms.DataGridView();
            this.columnCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnSuperficie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnError = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonAccept = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.mnu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox_Buscador = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button_BuscarMarca = new System.Windows.Forms.Button();
            this.textBox_marca = new System.Windows.Forms.TextBox();
            this.label_insertado = new System.Windows.Forms.Label();
            this.label_m2 = new System.Windows.Forms.Label();
            this.pictureBox_checked = new System.Windows.Forms.PictureBox();
            this.pictureBox_not = new System.Windows.Forms.PictureBox();
            this.textBox_marcaEdit = new System.Windows.Forms.TextBox();
            this.button_cambiar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTennetPaint)).BeginInit();
            this.mnu.SuspendLayout();
            this.groupBox_Buscador.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_checked)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_not)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewTennetPaint
            // 
            this.dataGridViewTennetPaint.AccessibleRole = System.Windows.Forms.AccessibleRole.Cell;
            this.dataGridViewTennetPaint.AllowUserToOrderColumns = true;
            this.dataGridViewTennetPaint.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewTennetPaint.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTennetPaint.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTennetPaint.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnCodigo,
            this.columnSuperficie,
            this.columnError});
            this.dataGridViewTennetPaint.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewTennetPaint.Name = "dataGridViewTennetPaint";
            this.dataGridViewTennetPaint.Size = new System.Drawing.Size(867, 472);
            this.dataGridViewTennetPaint.TabIndex = 0;
            this.dataGridViewTennetPaint.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dataGridViewTennetPaint_KeyUp);
            this.dataGridViewTennetPaint.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridViewTennetPaint_MouseClick);
            // 
            // columnCodigo
            // 
            this.columnCodigo.HeaderText = "Codigo";
            this.columnCodigo.Name = "columnCodigo";
            // 
            // columnSuperficie
            // 
            this.columnSuperficie.HeaderText = "Superficie";
            this.columnSuperficie.Name = "columnSuperficie";
            // 
            // columnError
            // 
            this.columnError.HeaderText = "Error";
            this.columnError.Name = "columnError";
            this.columnError.ReadOnly = true;
            // 
            // buttonAccept
            // 
            this.buttonAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAccept.Location = new System.Drawing.Point(804, 714);
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(75, 23);
            this.buttonAccept.TabIndex = 1;
            this.buttonAccept.Text = "Aceptar";
            this.buttonAccept.UseVisualStyleBackColor = true;
            this.buttonAccept.Click += new System.EventHandler(this.buttonAccept_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(713, 713);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancelar";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // mnu
            // 
            this.mnu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCopy,
            this.mnuPaste});
            this.mnu.Name = "contextMenuStripMnu";
            this.mnu.Size = new System.Drawing.Size(110, 48);
            // 
            // mnuCopy
            // 
            this.mnuCopy.Name = "mnuCopy";
            this.mnuCopy.Size = new System.Drawing.Size(109, 22);
            this.mnuCopy.Text = "Copiar";
            this.mnuCopy.Click += new System.EventHandler(this.copiarToolStripMenuItem_Click);
            // 
            // mnuPaste
            // 
            this.mnuPaste.Name = "mnuPaste";
            this.mnuPaste.Size = new System.Drawing.Size(109, 22);
            this.mnuPaste.Text = "Pegar";
            this.mnuPaste.Click += new System.EventHandler(this.pegarToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(13, 487);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(866, 14);
            this.label1.TabIndex = 3;
            // 
            // groupBox_Buscador
            // 
            this.groupBox_Buscador.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_Buscador.AutoSize = true;
            this.groupBox_Buscador.Controls.Add(this.label2);
            this.groupBox_Buscador.Controls.Add(this.button_BuscarMarca);
            this.groupBox_Buscador.Controls.Add(this.textBox_marca);
            this.groupBox_Buscador.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.groupBox_Buscador.Location = new System.Drawing.Point(13, 504);
            this.groupBox_Buscador.Name = "groupBox_Buscador";
            this.groupBox_Buscador.Size = new System.Drawing.Size(468, 203);
            this.groupBox_Buscador.TabIndex = 5;
            this.groupBox_Buscador.TabStop = false;
            this.groupBox_Buscador.Text = "Buscador Marcas m2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(462, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 25);
            this.label2.TabIndex = 5;
            // 
            // button_BuscarMarca
            // 
            this.button_BuscarMarca.AutoSize = true;
            this.button_BuscarMarca.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button_BuscarMarca.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_BuscarMarca.Location = new System.Drawing.Point(64, 92);
            this.button_BuscarMarca.Name = "button_BuscarMarca";
            this.button_BuscarMarca.Size = new System.Drawing.Size(132, 53);
            this.button_BuscarMarca.TabIndex = 3;
            this.button_BuscarMarca.Text = "Buscar";
            this.button_BuscarMarca.UseVisualStyleBackColor = false;
            this.button_BuscarMarca.Click += new System.EventHandler(this.button_BuscarMarca_Click);
            // 
            // textBox_marca
            // 
            this.textBox_marca.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_marca.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.textBox_marca.Location = new System.Drawing.Point(6, 40);
            this.textBox_marca.Name = "textBox_marca";
            this.textBox_marca.Size = new System.Drawing.Size(254, 45);
            this.textBox_marca.TabIndex = 0;
            // 
            // label_insertado
            // 
            this.label_insertado.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_insertado.AutoSize = true;
            this.label_insertado.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label_insertado.Location = new System.Drawing.Point(96, 30);
            this.label_insertado.Name = "label_insertado";
            this.label_insertado.Size = new System.Drawing.Size(93, 25);
            this.label_insertado.TabIndex = 1;
            this.label_insertado.Text = "Insertado";
            // 
            // label_m2
            // 
            this.label_m2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_m2.AutoSize = true;
            this.label_m2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label_m2.Location = new System.Drawing.Point(321, 30);
            this.label_m2.Name = "label_m2";
            this.label_m2.Size = new System.Drawing.Size(39, 25);
            this.label_m2.TabIndex = 2;
            this.label_m2.Text = "m2";
            // 
            // pictureBox_checked
            // 
            this.pictureBox_checked.ErrorImage = null;
            this.pictureBox_checked.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_checked.Image")));
            this.pictureBox_checked.Location = new System.Drawing.Point(112, 63);
            this.pictureBox_checked.Name = "pictureBox_checked";
            this.pictureBox_checked.Size = new System.Drawing.Size(71, 67);
            this.pictureBox_checked.TabIndex = 4;
            this.pictureBox_checked.TabStop = false;
            this.pictureBox_checked.Visible = false;
            // 
            // pictureBox_not
            // 
            this.pictureBox_not.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_not.Image")));
            this.pictureBox_not.Location = new System.Drawing.Point(112, 63);
            this.pictureBox_not.Name = "pictureBox_not";
            this.pictureBox_not.Size = new System.Drawing.Size(71, 67);
            this.pictureBox_not.TabIndex = 6;
            this.pictureBox_not.TabStop = false;
            this.pictureBox_not.Visible = false;
            // 
            // textBox_marcaEdit
            // 
            this.textBox_marcaEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_marcaEdit.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_marcaEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_marcaEdit.Location = new System.Drawing.Point(284, 68);
            this.textBox_marcaEdit.Name = "textBox_marcaEdit";
            this.textBox_marcaEdit.Size = new System.Drawing.Size(122, 62);
            this.textBox_marcaEdit.TabIndex = 7;
            this.textBox_marcaEdit.Visible = false;
            // 
            // button_cambiar
            // 
            this.button_cambiar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_cambiar.AutoSize = true;
            this.button_cambiar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button_cambiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button_cambiar.Location = new System.Drawing.Point(412, 97);
            this.button_cambiar.Name = "button_cambiar";
            this.button_cambiar.Size = new System.Drawing.Size(75, 33);
            this.button_cambiar.TabIndex = 8;
            this.button_cambiar.Text = "Cambiar";
            this.button_cambiar.UseVisualStyleBackColor = false;
            this.button_cambiar.Visible = false;
            this.button_cambiar.Click += new System.EventHandler(this.button_cambiar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.pictureBox_not);
            this.groupBox1.Controls.Add(this.pictureBox_checked);
            this.groupBox1.Controls.Add(this.button_cambiar);
            this.groupBox1.Controls.Add(this.textBox_marcaEdit);
            this.groupBox1.Controls.Add(this.label_insertado);
            this.groupBox1.Controls.Add(this.label_m2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.groupBox1.Location = new System.Drawing.Point(335, 504);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(544, 203);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Resultados";
            // 
            // tennetPintura
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(891, 749);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox_Buscador);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonAccept);
            this.Controls.Add(this.dataGridViewTennetPaint);
            this.Name = "tennetPintura";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tennet Pintura";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.tennetPintura_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.tennetPintura_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTennetPaint)).EndInit();
            this.mnu.ResumeLayout(false);
            this.groupBox_Buscador.ResumeLayout(false);
            this.groupBox_Buscador.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_checked)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_not)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        //dataGridViewTennetPaint.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridViewTennetPaint);

        private System.Windows.Forms.DataGridView dataGridViewTennetPaint;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSuperficie;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnError;
        private System.Windows.Forms.Button buttonAccept;
        private System.Windows.Forms.Button buttonCancel;

        //Copy Paste
        private System.Windows.Forms.ContextMenuStrip mnu = new System.Windows.Forms.ContextMenuStrip();
        private System.Windows.Forms.ToolStripMenuItem mnuPaste = new System.Windows.Forms.ToolStripMenuItem("Paste");
        
        //private System.Windows.Forms.ToolStripMenuItem mnuCut = new System.Windows.Forms.ToolStripMenuItem("Cut");
        private System.Windows.Forms.ToolStripMenuItem mnuCopy = new System.Windows.Forms.ToolStripMenuItem("Copy");
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox_Buscador;
        private System.Windows.Forms.Button button_BuscarMarca;
        private System.Windows.Forms.Label label_m2;
        private System.Windows.Forms.Label label_insertado;
        private System.Windows.Forms.TextBox textBox_marca;
        private System.Windows.Forms.PictureBox pictureBox_checked;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox_not;
        private System.Windows.Forms.TextBox textBox_marcaEdit;
        private System.Windows.Forms.Button button_cambiar;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}