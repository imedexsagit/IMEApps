namespace ProyectosOnLine
{
    partial class Form3
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.bONODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.etiquetaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.bONODataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.etiquetaDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctd_a_fichar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tCVBONOETIQUETABindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bonoEtiquetas = new ProyectosOnLine.BonoEtiquetas();
            this.tC_V_BONO_ETIQUETATableAdapter = new ProyectosOnLine.BonoEtiquetasTableAdapters.TC_V_BONO_ETIQUETATableAdapter();
            this.button2 = new System.Windows.Forms.Button();
            this.btn_todo = new System.Windows.Forms.Button();
            this.btn_nada = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tCVBONOETIQUETABindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bonoEtiquetas)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(22, 264);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 37);
            this.button1.TabIndex = 0;
            this.button1.Text = "Añadir";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(68, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(42, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "-3";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(207, 12);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(85, 20);
            this.dateTimePicker1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Operario";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(169, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Fecha";
            // 
            // bONODataGridViewTextBoxColumn
            // 
            this.bONODataGridViewTextBoxColumn.Name = "bONODataGridViewTextBoxColumn";
            // 
            // etiquetaDataGridViewTextBoxColumn
            // 
            this.etiquetaDataGridViewTextBoxColumn.Name = "etiquetaDataGridViewTextBoxColumn";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.bONODataGridViewTextBoxColumn1,
            this.etiquetaDataGridViewTextBoxColumn1,
            this.ctd_a_fichar});
            this.dataGridView1.DataSource = this.tCVBONOETIQUETABindingSource;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridView1.Location = new System.Drawing.Point(22, 89);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridView1.Size = new System.Drawing.Size(270, 169);
            this.dataGridView1.TabIndex = 11;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            this.Column1.Width = 30;
            // 
            // bONODataGridViewTextBoxColumn1
            // 
            this.bONODataGridViewTextBoxColumn1.DataPropertyName = "BONO";
            this.bONODataGridViewTextBoxColumn1.HeaderText = "BONO";
            this.bONODataGridViewTextBoxColumn1.Name = "bONODataGridViewTextBoxColumn1";
            this.bONODataGridViewTextBoxColumn1.Width = 50;
            // 
            // etiquetaDataGridViewTextBoxColumn1
            // 
            this.etiquetaDataGridViewTextBoxColumn1.DataPropertyName = "Etiqueta";
            this.etiquetaDataGridViewTextBoxColumn1.HeaderText = "Etiqueta";
            this.etiquetaDataGridViewTextBoxColumn1.Name = "etiquetaDataGridViewTextBoxColumn1";
            this.etiquetaDataGridViewTextBoxColumn1.ReadOnly = true;
            this.etiquetaDataGridViewTextBoxColumn1.Width = 60;
            // 
            // ctd_a_fichar
            // 
            this.ctd_a_fichar.DataPropertyName = "ctd_a_fichar";
            this.ctd_a_fichar.HeaderText = "ctd_a_fichar";
            this.ctd_a_fichar.Name = "ctd_a_fichar";
            this.ctd_a_fichar.Width = 70;
            // 
            // tCVBONOETIQUETABindingSource
            // 
            this.tCVBONOETIQUETABindingSource.DataMember = "TC_V_BONO_ETIQUETA";
            this.tCVBONOETIQUETABindingSource.DataSource = this.bonoEtiquetas;
            // 
            // bonoEtiquetas
            // 
            this.bonoEtiquetas.DataSetName = "BonoEtiquetas";
            this.bonoEtiquetas.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tC_V_BONO_ETIQUETATableAdapter
            // 
            this.tC_V_BONO_ETIQUETATableAdapter.ClearBeforeFill = true;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.button2.Location = new System.Drawing.Point(207, 264);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(85, 37);
            this.button2.TabIndex = 12;
            this.button2.Text = "Cancelar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_todo
            // 
            this.btn_todo.Location = new System.Drawing.Point(22, 52);
            this.btn_todo.Name = "btn_todo";
            this.btn_todo.Size = new System.Drawing.Size(109, 21);
            this.btn_todo.TabIndex = 13;
            this.btn_todo.Text = "Seleccionar Todos";
            this.btn_todo.UseVisualStyleBackColor = true;
            this.btn_todo.Click += new System.EventHandler(this.btn_todo_Click);
            // 
            // btn_nada
            // 
            this.btn_nada.Location = new System.Drawing.Point(183, 52);
            this.btn_nada.Name = "btn_nada";
            this.btn_nada.Size = new System.Drawing.Size(109, 21);
            this.btn_nada.TabIndex = 14;
            this.btn_nada.Text = "Desmarcar Todos";
            this.btn_nada.UseVisualStyleBackColor = true;
            this.btn_nada.Click += new System.EventHandler(this.btn_nada_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 326);
            this.Controls.Add(this.btn_nada);
            this.Controls.Add(this.btn_todo);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fichar Operaciones a OF: ";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.Shown += new System.EventHandler(this.Form3_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tCVBONOETIQUETABindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bonoEtiquetas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        
        //private System.Windows.Forms.DataGridViewTextBoxColumn cODEMPDataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn tIPOREGDataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn oRDFABDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bONODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn etiquetaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private BonoEtiquetas bonoEtiquetas;
        private System.Windows.Forms.BindingSource tCVBONOETIQUETABindingSource;
        private ProyectosOnLine.BonoEtiquetasTableAdapters.TC_V_BONO_ETIQUETATableAdapter tC_V_BONO_ETIQUETATableAdapter;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        //private System.Windows.Forms.DataGridViewTextBoxColumn oPERACDataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn bONODataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn etiquetaDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ctd_a_fichar;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btn_todo;
        private System.Windows.Forms.Button btn_nada;
    }
}