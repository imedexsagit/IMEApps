namespace TareasFaltanDatos
{
    partial class Form1
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
            this.tgFaltanDatos = new AdvancedDataGridView.TreeGridView();
            this.Pedido = new AdvancedDataGridView.TreeGridColumn();
            this.Cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tarea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaFin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Linea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Serie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaNecesaria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaPrevista = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Euros = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EuroKilos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kilos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha = new System.Windows.Forms.DateTimePicker();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tgFaltanDatos)).BeginInit();
            this.SuspendLayout();
            // 
            // tgFaltanDatos
            // 
            this.tgFaltanDatos.AllowUserToAddRows = false;
            this.tgFaltanDatos.AllowUserToDeleteRows = false;
            this.tgFaltanDatos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.tgFaltanDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tgFaltanDatos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Pedido,
            this.Cliente,
            this.Tarea,
            this.FechaFin,
            this.Linea,
            this.Descripcion,
            this.Serie,
            this.FechaNecesaria,
            this.FechaPrevista,
            this.Euros,
            this.EuroKilos,
            this.Kilos});
            this.tgFaltanDatos.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.tgFaltanDatos.ImageList = null;
            this.tgFaltanDatos.Location = new System.Drawing.Point(12, 62);
            this.tgFaltanDatos.Name = "tgFaltanDatos";
            this.tgFaltanDatos.Size = new System.Drawing.Size(1632, 725);
            this.tgFaltanDatos.TabIndex = 108;
            this.tgFaltanDatos.NodeExpanded += new AdvancedDataGridView.ExpandedEventHandler(this.tgFaltanDatos_NodeExpanded);
            this.tgFaltanDatos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tgFaltanDatos_CellClick);
            this.tgFaltanDatos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tgFaltanDatos_CellDoubleClick);
            // 
            // Pedido
            // 
            this.Pedido.DefaultNodeImage = null;
            this.Pedido.HeaderText = "Pedido";
            this.Pedido.Name = "Pedido";
            this.Pedido.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Pedido.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Pedido.Width = 153;
            // 
            // Cliente
            // 
            this.Cliente.HeaderText = "Cliente";
            this.Cliente.Name = "Cliente";
            this.Cliente.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Cliente.Width = 152;
            // 
            // Tarea
            // 
            this.Tarea.HeaderText = "Tarea";
            this.Tarea.Name = "Tarea";
            this.Tarea.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Tarea.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Tarea.Width = 153;
            // 
            // FechaFin
            // 
            this.FechaFin.HeaderText = "Fecha Fin";
            this.FechaFin.Name = "FechaFin";
            this.FechaFin.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FechaFin.Width = 152;
            // 
            // Linea
            // 
            this.Linea.HeaderText = "Línea";
            this.Linea.Name = "Linea";
            this.Linea.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Linea.Width = 154;
            // 
            // Descripcion
            // 
            this.Descripcion.HeaderText = "Descripción";
            this.Descripcion.Name = "Descripcion";
            this.Descripcion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Descripcion.Width = 154;
            // 
            // Serie
            // 
            this.Serie.HeaderText = "Serie";
            this.Serie.Name = "Serie";
            this.Serie.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Serie.Width = 153;
            // 
            // FechaNecesaria
            // 
            this.FechaNecesaria.HeaderText = "Fecha Necesaria";
            this.FechaNecesaria.Name = "FechaNecesaria";
            this.FechaNecesaria.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FechaPrevista
            // 
            this.FechaPrevista.HeaderText = "Fecha Prevista";
            this.FechaPrevista.Name = "FechaPrevista";
            this.FechaPrevista.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Euros
            // 
            this.Euros.HeaderText = "€";
            this.Euros.Name = "Euros";
            this.Euros.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Euros.Width = 154;
            // 
            // EuroKilos
            // 
            this.EuroKilos.HeaderText = "€/Kg";
            this.EuroKilos.Name = "EuroKilos";
            this.EuroKilos.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.EuroKilos.Width = 153;
            // 
            // Kilos
            // 
            this.Kilos.HeaderText = "Kg";
            this.Kilos.Name = "Kilos";
            this.Kilos.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Kilos.Width = 154;
            // 
            // fecha
            // 
            this.fecha.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.fecha.Location = new System.Drawing.Point(93, 27);
            this.fecha.Name = "fecha";
            this.fecha.Size = new System.Drawing.Size(129, 20);
            this.fecha.TabIndex = 110;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Location = new System.Drawing.Point(1467, 793);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(177, 51);
            this.btnGuardar.TabIndex = 111;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefrescar.Location = new System.Drawing.Point(1493, 19);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(151, 37);
            this.btnRefrescar.TabIndex = 112;
            this.btnRefrescar.Text = "⟳ Refrescar";
            this.btnRefrescar.UseVisualStyleBackColor = true;
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 20);
            this.label1.TabIndex = 113;
            this.label1.Text = "Fecha :";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1656, 847);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRefrescar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.fecha);
            this.Controls.Add(this.tgFaltanDatos);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tareas con falta de datos";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tgFaltanDatos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AdvancedDataGridView.TreeGridView tgFaltanDatos;
        private System.Windows.Forms.DateTimePicker fecha;
        private AdvancedDataGridView.TreeGridColumn Pedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tarea;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaFin;
        private System.Windows.Forms.DataGridViewTextBoxColumn Linea;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Serie;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaNecesaria;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaPrevista;
        private System.Windows.Forms.DataGridViewTextBoxColumn Euros;
        private System.Windows.Forms.DataGridViewTextBoxColumn EuroKilos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kilos;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.Label label1;

    }
}

