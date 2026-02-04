namespace ProyectosOnLine
{
    partial class Form5
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.bONODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pUESTODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tCBONOLBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bonosOF = new ProyectosOnLine.BonosOF();
            this.tC_BONOLTableAdapter = new ProyectosOnLine.BonosOFTableAdapters.TC_BONOLTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tCBONOLBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bonosOF)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.bONODataGridViewTextBoxColumn,
            this.pUESTODataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.tCBONOLBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(250, 221);
            this.dataGridView1.TabIndex = 0;
            // 
            // bONODataGridViewTextBoxColumn
            // 
            this.bONODataGridViewTextBoxColumn.DataPropertyName = "BONO";
            this.bONODataGridViewTextBoxColumn.FillWeight = 75F;
            this.bONODataGridViewTextBoxColumn.HeaderText = "BONO";
            this.bONODataGridViewTextBoxColumn.Name = "bONODataGridViewTextBoxColumn";
            this.bONODataGridViewTextBoxColumn.ReadOnly = true;
            this.bONODataGridViewTextBoxColumn.Width = 75;
            // 
            // pUESTODataGridViewTextBoxColumn
            // 
            this.pUESTODataGridViewTextBoxColumn.DataPropertyName = "PUESTO";
            this.pUESTODataGridViewTextBoxColumn.FillWeight = 125F;
            this.pUESTODataGridViewTextBoxColumn.HeaderText = "PUESTO";
            this.pUESTODataGridViewTextBoxColumn.Name = "pUESTODataGridViewTextBoxColumn";
            this.pUESTODataGridViewTextBoxColumn.ReadOnly = true;
            this.pUESTODataGridViewTextBoxColumn.Width = 125;
            // 
            // tCBONOLBindingSource
            // 
            this.tCBONOLBindingSource.DataMember = "TC_BONOL";
            this.tCBONOLBindingSource.DataSource = this.bonosOF;
            // 
            // bonosOF
            // 
            this.bonosOF.DataSetName = "BonosOF";
            this.bonosOF.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tC_BONOLTableAdapter
            // 
            this.tC_BONOLTableAdapter.ClearBeforeFill = true;
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.dataGridView1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form5";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bonos de la OF: ";
            this.Load += new System.EventHandler(this.Form5_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tCBONOLBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bonosOF)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private BonosOF bonosOF;
        private System.Windows.Forms.BindingSource tCBONOLBindingSource;
        private ProyectosOnLine.BonosOFTableAdapters.TC_BONOLTableAdapter tC_BONOLTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn bONODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pUESTODataGridViewTextBoxColumn;
    }
}