namespace ProyectosOnLine
{
    partial class Form2
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.identificador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cODOPEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mAQUINADataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operacionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bonoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.etiquetaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PiezasFichadas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tiempo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ficeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plasmDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cizaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.punzDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soldDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PlegCH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prenDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fresDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FresCH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BiselCH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gekaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plegDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Contor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Avella = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Retala = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.manuDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.exteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.recepDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vistaTCIMPORTBONOSBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.imeDataSet = new ProyectosOnLine.imeDataSet();
            this.dividirFichajeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cargarPiezasToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.quitarPiezasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.vista_TC_IMPORTBONOSTableAdapter = new ProyectosOnLine.imeDataSetTableAdapters.Vista_TC_IMPORTBONOSTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vistaTCIMPORTBONOSBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imeDataSet)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.identificador,
            this.cODOPEDataGridViewTextBoxColumn,
            this.Nombre,
            this.mAQUINADataGridViewTextBoxColumn,
            this.operacionDataGridViewTextBoxColumn,
            this.fechaDataGridViewTextBoxColumn,
            this.bonoDataGridViewTextBoxColumn,
            this.etiquetaDataGridViewTextBoxColumn,
            this.cantidad,
            this.PiezasFichadas,
            this.Tiempo,
            this.ficeDataGridViewTextBoxColumn,
            this.serrDataGridViewTextBoxColumn,
            this.plasmDataGridViewTextBoxColumn,
            this.cizaDataGridViewTextBoxColumn,
            this.punzDataGridViewTextBoxColumn,
            this.soldDataGridViewTextBoxColumn,
            this.PlegCH,
            this.prenDataGridViewTextBoxColumn,
            this.fresDataGridViewTextBoxColumn,
            this.FresCH,
            this.BiselCH,
            this.gekaDataGridViewTextBoxColumn,
            this.plegDataGridViewTextBoxColumn,
            this.Contor,
            this.Avella,
            this.Retala,
            this.manuDataGridViewTextBoxColumn,
            this.exteDataGridViewTextBoxColumn,
            this.expeDataGridViewTextBoxColumn,
            this.recepDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.vistaTCIMPORTBONOSBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(1304, 434);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseUp);
            // 
            // identificador
            // 
            this.identificador.DataPropertyName = "identificador";
            this.identificador.HeaderText = "identificador";
            this.identificador.Name = "identificador";
            this.identificador.ReadOnly = true;
            this.identificador.Visible = false;
            // 
            // cODOPEDataGridViewTextBoxColumn
            // 
            this.cODOPEDataGridViewTextBoxColumn.DataPropertyName = "CODOPE";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cODOPEDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.cODOPEDataGridViewTextBoxColumn.HeaderText = "Operario";
            this.cODOPEDataGridViewTextBoxColumn.Name = "cODOPEDataGridViewTextBoxColumn";
            this.cODOPEDataGridViewTextBoxColumn.ReadOnly = true;
            this.cODOPEDataGridViewTextBoxColumn.Width = 50;
            // 
            // Nombre
            // 
            this.Nombre.DataPropertyName = "Nombre";
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.Name = "Nombre";
            this.Nombre.ReadOnly = true;
            this.Nombre.Width = 150;
            // 
            // mAQUINADataGridViewTextBoxColumn
            // 
            this.mAQUINADataGridViewTextBoxColumn.DataPropertyName = "MAQUINA";
            this.mAQUINADataGridViewTextBoxColumn.HeaderText = "Máquina";
            this.mAQUINADataGridViewTextBoxColumn.Name = "mAQUINADataGridViewTextBoxColumn";
            this.mAQUINADataGridViewTextBoxColumn.ReadOnly = true;
            this.mAQUINADataGridViewTextBoxColumn.Width = 50;
            // 
            // operacionDataGridViewTextBoxColumn
            // 
            this.operacionDataGridViewTextBoxColumn.DataPropertyName = "Operacion";
            this.operacionDataGridViewTextBoxColumn.HeaderText = "Operacion";
            this.operacionDataGridViewTextBoxColumn.Name = "operacionDataGridViewTextBoxColumn";
            this.operacionDataGridViewTextBoxColumn.ReadOnly = true;
            this.operacionDataGridViewTextBoxColumn.Width = 60;
            // 
            // fechaDataGridViewTextBoxColumn
            // 
            this.fechaDataGridViewTextBoxColumn.DataPropertyName = "Fecha";
            this.fechaDataGridViewTextBoxColumn.HeaderText = "Fecha";
            this.fechaDataGridViewTextBoxColumn.Name = "fechaDataGridViewTextBoxColumn";
            this.fechaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bonoDataGridViewTextBoxColumn
            // 
            this.bonoDataGridViewTextBoxColumn.DataPropertyName = "BONO";
            this.bonoDataGridViewTextBoxColumn.HeaderText = "Bono";
            this.bonoDataGridViewTextBoxColumn.Name = "bonoDataGridViewTextBoxColumn";
            this.bonoDataGridViewTextBoxColumn.ReadOnly = true;
            this.bonoDataGridViewTextBoxColumn.Width = 60;
            // 
            // etiquetaDataGridViewTextBoxColumn
            // 
            this.etiquetaDataGridViewTextBoxColumn.DataPropertyName = "Etiqueta";
            this.etiquetaDataGridViewTextBoxColumn.HeaderText = "Etiqueta";
            this.etiquetaDataGridViewTextBoxColumn.Name = "etiquetaDataGridViewTextBoxColumn";
            this.etiquetaDataGridViewTextBoxColumn.ReadOnly = true;
            this.etiquetaDataGridViewTextBoxColumn.Width = 50;
            // 
            // cantidad
            // 
            this.cantidad.DataPropertyName = "cantidad";
            this.cantidad.HeaderText = "CTD";
            this.cantidad.Name = "cantidad";
            this.cantidad.ReadOnly = true;
            this.cantidad.Width = 40;
            // 
            // PiezasFichadas
            // 
            this.PiezasFichadas.DataPropertyName = "PiezasFichadas";
            this.PiezasFichadas.HeaderText = "PiezasFichadas";
            this.PiezasFichadas.Name = "PiezasFichadas";
            this.PiezasFichadas.ReadOnly = true;
            this.PiezasFichadas.Width = 90;
            // 
            // Tiempo
            // 
            this.Tiempo.DataPropertyName = "Tiempo";
            this.Tiempo.HeaderText = "Tiempo";
            this.Tiempo.Name = "Tiempo";
            this.Tiempo.ReadOnly = true;
            this.Tiempo.Width = 50;
            // 
            // ficeDataGridViewTextBoxColumn
            // 
            this.ficeDataGridViewTextBoxColumn.DataPropertyName = "Fice";
            this.ficeDataGridViewTextBoxColumn.HeaderText = "Fice";
            this.ficeDataGridViewTextBoxColumn.Name = "ficeDataGridViewTextBoxColumn";
            this.ficeDataGridViewTextBoxColumn.ReadOnly = true;
            this.ficeDataGridViewTextBoxColumn.ToolTipText = "100001";
            this.ficeDataGridViewTextBoxColumn.Width = 40;
            // 
            // serrDataGridViewTextBoxColumn
            // 
            this.serrDataGridViewTextBoxColumn.DataPropertyName = "Serr";
            this.serrDataGridViewTextBoxColumn.HeaderText = "Serr";
            this.serrDataGridViewTextBoxColumn.Name = "serrDataGridViewTextBoxColumn";
            this.serrDataGridViewTextBoxColumn.ReadOnly = true;
            this.serrDataGridViewTextBoxColumn.ToolTipText = "120000";
            this.serrDataGridViewTextBoxColumn.Width = 40;
            // 
            // plasmDataGridViewTextBoxColumn
            // 
            this.plasmDataGridViewTextBoxColumn.DataPropertyName = "Plasm";
            this.plasmDataGridViewTextBoxColumn.HeaderText = "Plasm";
            this.plasmDataGridViewTextBoxColumn.Name = "plasmDataGridViewTextBoxColumn";
            this.plasmDataGridViewTextBoxColumn.ReadOnly = true;
            this.plasmDataGridViewTextBoxColumn.ToolTipText = "500001";
            this.plasmDataGridViewTextBoxColumn.Width = 40;
            // 
            // cizaDataGridViewTextBoxColumn
            // 
            this.cizaDataGridViewTextBoxColumn.DataPropertyName = "Ciza";
            this.cizaDataGridViewTextBoxColumn.HeaderText = "Ciza";
            this.cizaDataGridViewTextBoxColumn.Name = "cizaDataGridViewTextBoxColumn";
            this.cizaDataGridViewTextBoxColumn.ReadOnly = true;
            this.cizaDataGridViewTextBoxColumn.ToolTipText = "400001";
            this.cizaDataGridViewTextBoxColumn.Width = 40;
            // 
            // punzDataGridViewTextBoxColumn
            // 
            this.punzDataGridViewTextBoxColumn.DataPropertyName = "Punz";
            this.punzDataGridViewTextBoxColumn.HeaderText = "Punz";
            this.punzDataGridViewTextBoxColumn.Name = "punzDataGridViewTextBoxColumn";
            this.punzDataGridViewTextBoxColumn.ReadOnly = true;
            this.punzDataGridViewTextBoxColumn.ToolTipText = "100002";
            this.punzDataGridViewTextBoxColumn.Width = 40;
            // 
            // soldDataGridViewTextBoxColumn
            // 
            this.soldDataGridViewTextBoxColumn.DataPropertyName = "Sold";
            this.soldDataGridViewTextBoxColumn.HeaderText = "Sold";
            this.soldDataGridViewTextBoxColumn.Name = "soldDataGridViewTextBoxColumn";
            this.soldDataGridViewTextBoxColumn.ReadOnly = true;
            this.soldDataGridViewTextBoxColumn.ToolTipText = "950001";
            this.soldDataGridViewTextBoxColumn.Width = 40;
            // 
            // PlegCH
            // 
            this.PlegCH.DataPropertyName = "PlegCH";
            this.PlegCH.HeaderText = "PlegCH";
            this.PlegCH.Name = "PlegCH";
            this.PlegCH.ReadOnly = true;
            this.PlegCH.ToolTipText = "300002";
            this.PlegCH.Width = 45;
            // 
            // prenDataGridViewTextBoxColumn
            // 
            this.prenDataGridViewTextBoxColumn.DataPropertyName = "Gran";
            this.prenDataGridViewTextBoxColumn.HeaderText = "Gran";
            this.prenDataGridViewTextBoxColumn.Name = "prenDataGridViewTextBoxColumn";
            this.prenDataGridViewTextBoxColumn.ReadOnly = true;
            this.prenDataGridViewTextBoxColumn.ToolTipText = "550001";
            this.prenDataGridViewTextBoxColumn.Width = 40;
            // 
            // fresDataGridViewTextBoxColumn
            // 
            this.fresDataGridViewTextBoxColumn.DataPropertyName = "Fres";
            this.fresDataGridViewTextBoxColumn.HeaderText = "Fres";
            this.fresDataGridViewTextBoxColumn.Name = "fresDataGridViewTextBoxColumn";
            this.fresDataGridViewTextBoxColumn.ReadOnly = true;
            this.fresDataGridViewTextBoxColumn.ToolTipText = "600001";
            this.fresDataGridViewTextBoxColumn.Width = 40;
            // 
            // FresCH
            // 
            this.FresCH.DataPropertyName = "FresCH";
            this.FresCH.HeaderText = "FresCH";
            this.FresCH.Name = "FresCH";
            this.FresCH.ReadOnly = true;
            this.FresCH.ToolTipText = "600002";
            this.FresCH.Width = 45;
            // 
            // BiselCH
            // 
            this.BiselCH.DataPropertyName = "BiselCH";
            this.BiselCH.HeaderText = "BiselCH";
            this.BiselCH.Name = "BiselCH";
            this.BiselCH.ReadOnly = true;
            this.BiselCH.ToolTipText = "600005";
            this.BiselCH.Width = 45;
            // 
            // gekaDataGridViewTextBoxColumn
            // 
            this.gekaDataGridViewTextBoxColumn.DataPropertyName = "Geka";
            this.gekaDataGridViewTextBoxColumn.HeaderText = "Geka";
            this.gekaDataGridViewTextBoxColumn.Name = "gekaDataGridViewTextBoxColumn";
            this.gekaDataGridViewTextBoxColumn.ReadOnly = true;
            this.gekaDataGridViewTextBoxColumn.ToolTipText = "200001";
            this.gekaDataGridViewTextBoxColumn.Width = 40;
            // 
            // plegDataGridViewTextBoxColumn
            // 
            this.plegDataGridViewTextBoxColumn.DataPropertyName = "Pleg";
            this.plegDataGridViewTextBoxColumn.HeaderText = "Pleg";
            this.plegDataGridViewTextBoxColumn.Name = "plegDataGridViewTextBoxColumn";
            this.plegDataGridViewTextBoxColumn.ReadOnly = true;
            this.plegDataGridViewTextBoxColumn.ToolTipText = "300001";
            this.plegDataGridViewTextBoxColumn.Width = 40;
            // 
            // Contor
            // 
            this.Contor.DataPropertyName = "Contor";
            this.Contor.HeaderText = "Contor";
            this.Contor.Name = "Contor";
            this.Contor.ReadOnly = true;
            this.Contor.ToolTipText = "620000";
            this.Contor.Width = 40;
            // 
            // Avella
            // 
            this.Avella.DataPropertyName = "Avella";
            this.Avella.HeaderText = "Avella";
            this.Avella.Name = "Avella";
            this.Avella.ReadOnly = true;
            this.Avella.ToolTipText = "630000";
            this.Avella.Width = 40;
            // 
            // Retala
            // 
            this.Retala.DataPropertyName = "Retala";
            this.Retala.HeaderText = "Retala";
            this.Retala.Name = "Retala";
            this.Retala.ReadOnly = true;
            this.Retala.ToolTipText = "640000";
            this.Retala.Width = 40;
            // 
            // manuDataGridViewTextBoxColumn
            // 
            this.manuDataGridViewTextBoxColumn.DataPropertyName = "Manu";
            this.manuDataGridViewTextBoxColumn.HeaderText = "Manu";
            this.manuDataGridViewTextBoxColumn.Name = "manuDataGridViewTextBoxColumn";
            this.manuDataGridViewTextBoxColumn.ReadOnly = true;
            this.manuDataGridViewTextBoxColumn.ToolTipText = "990001";
            this.manuDataGridViewTextBoxColumn.Width = 40;
            // 
            // exteDataGridViewTextBoxColumn
            // 
            this.exteDataGridViewTextBoxColumn.DataPropertyName = "Exte";
            this.exteDataGridViewTextBoxColumn.HeaderText = "Exte";
            this.exteDataGridViewTextBoxColumn.Name = "exteDataGridViewTextBoxColumn";
            this.exteDataGridViewTextBoxColumn.ReadOnly = true;
            this.exteDataGridViewTextBoxColumn.ToolTipText = "000009";
            this.exteDataGridViewTextBoxColumn.Width = 40;
            // 
            // expeDataGridViewTextBoxColumn
            // 
            this.expeDataGridViewTextBoxColumn.DataPropertyName = "Expe";
            this.expeDataGridViewTextBoxColumn.HeaderText = "Expe";
            this.expeDataGridViewTextBoxColumn.Name = "expeDataGridViewTextBoxColumn";
            this.expeDataGridViewTextBoxColumn.ReadOnly = true;
            this.expeDataGridViewTextBoxColumn.Width = 40;
            // 
            // recepDataGridViewTextBoxColumn
            // 
            this.recepDataGridViewTextBoxColumn.DataPropertyName = "Recep";
            this.recepDataGridViewTextBoxColumn.HeaderText = "Recep";
            this.recepDataGridViewTextBoxColumn.Name = "recepDataGridViewTextBoxColumn";
            this.recepDataGridViewTextBoxColumn.ReadOnly = true;
            this.recepDataGridViewTextBoxColumn.ToolTipText = "800001";
            this.recepDataGridViewTextBoxColumn.Width = 40;
            // 
            // vistaTCIMPORTBONOSBindingSource
            // 
            this.vistaTCIMPORTBONOSBindingSource.DataMember = "Vista_TC_IMPORTBONOS";
            this.vistaTCIMPORTBONOSBindingSource.DataSource = this.imeDataSet;
            // 
            // imeDataSet
            // 
            this.imeDataSet.DataSetName = "imeDataSet";
            this.imeDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dividirFichajeToolStripMenuItem
            // 
            this.dividirFichajeToolStripMenuItem.Name = "dividirFichajeToolStripMenuItem";
            this.dividirFichajeToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.dividirFichajeToolStripMenuItem.Text = "Dividir Fichaje";
            this.dividirFichajeToolStripMenuItem.Click += new System.EventHandler(this.dividirFichajeToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cargarPiezasToolStripMenuItem1,
            this.dividirFichajeToolStripMenuItem,
            this.quitarPiezasToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 70);
            // 
            // cargarPiezasToolStripMenuItem1
            // 
            this.cargarPiezasToolStripMenuItem1.Name = "cargarPiezasToolStripMenuItem1";
            this.cargarPiezasToolStripMenuItem1.Size = new System.Drawing.Size(148, 22);
            this.cargarPiezasToolStripMenuItem1.Text = "Cargar Piezas";
            this.cargarPiezasToolStripMenuItem1.Click += new System.EventHandler(this.cargarPiezasToolStripMenuItem1_Click);
            // 
            // quitarPiezasToolStripMenuItem
            // 
            this.quitarPiezasToolStripMenuItem.Name = "quitarPiezasToolStripMenuItem";
            this.quitarPiezasToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.quitarPiezasToolStripMenuItem.Text = "Quitar Piezas";
            this.quitarPiezasToolStripMenuItem.Click += new System.EventHandler(this.quitarPiezasToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(22, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(197, 35);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mostrar";
            this.groupBox1.Visible = false;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(125, 12);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(65, 17);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Tiempos";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.Visible = false;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(65, 11);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(56, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Piezas";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.Visible = false;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 12);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(55, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Todos";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.Visible = false;
            // 
            // vista_TC_IMPORTBONOSTableAdapter
            // 
            this.vista_TC_IMPORTBONOSTableAdapter.ClearBeforeFill = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1304, 434);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fichajes de la OF: ";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vistaTCIMPORTBONOSBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imeDataSet)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private imeDataSet imeDataSet;
        private System.Windows.Forms.BindingSource vistaTCIMPORTBONOSBindingSource;
        private ProyectosOnLine.imeDataSetTableAdapters.Vista_TC_IMPORTBONOSTableAdapter vista_TC_IMPORTBONOSTableAdapter;
        private System.Windows.Forms.ToolStripMenuItem dividirFichajeToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cargarPiezasToolStripMenuItem1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.ToolStripMenuItem quitarPiezasToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn identificador;
        private System.Windows.Forms.DataGridViewTextBoxColumn cODOPEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn mAQUINADataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn operacionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bonoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn etiquetaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn PiezasFichadas;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tiempo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ficeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn serrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn plasmDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cizaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn punzDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn soldDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlegCH;
        private System.Windows.Forms.DataGridViewTextBoxColumn prenDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fresDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FresCH;
        private System.Windows.Forms.DataGridViewTextBoxColumn BiselCH;
        private System.Windows.Forms.DataGridViewTextBoxColumn gekaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn plegDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Contor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Avella;
        private System.Windows.Forms.DataGridViewTextBoxColumn Retala;
        private System.Windows.Forms.DataGridViewTextBoxColumn manuDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn exteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn expeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn recepDataGridViewTextBoxColumn;
    }
}