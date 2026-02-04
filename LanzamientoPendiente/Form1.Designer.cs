namespace LanzamientoPendiente
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbPedido = new System.Windows.Forms.TextBox();
            this.lbPedido = new System.Windows.Forms.Label();
            this.btnComprobarPedido = new System.Windows.Forms.Button();
            this.dgLanzamientos = new System.Windows.Forms.DataGridView();
            this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Denominacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lineas = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.CmabiarDenominacion = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Lanzado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAnadirFila = new System.Windows.Forms.Button();
            this.btnLanzar = new System.Windows.Forms.Button();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.btnVaciar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLanzamientos)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnVaciar);
            this.groupBox1.Controls.Add(this.tbPedido);
            this.groupBox1.Controls.Add(this.lbPedido);
            this.groupBox1.Controls.Add(this.btnComprobarPedido);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(8, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1359, 104);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PEDIDO";
            // 
            // tbPedido
            // 
            this.tbPedido.Location = new System.Drawing.Point(6, 28);
            this.tbPedido.Name = "tbPedido";
            this.tbPedido.Size = new System.Drawing.Size(200, 29);
            this.tbPedido.TabIndex = 2;
            // 
            // lbPedido
            // 
            this.lbPedido.AutoSize = true;
            this.lbPedido.ForeColor = System.Drawing.Color.Firebrick;
            this.lbPedido.Location = new System.Drawing.Point(489, 49);
            this.lbPedido.Name = "lbPedido";
            this.lbPedido.Size = new System.Drawing.Size(0, 24);
            this.lbPedido.TabIndex = 1;
            // 
            // btnComprobarPedido
            // 
            this.btnComprobarPedido.Location = new System.Drawing.Point(239, 28);
            this.btnComprobarPedido.Name = "btnComprobarPedido";
            this.btnComprobarPedido.Size = new System.Drawing.Size(180, 58);
            this.btnComprobarPedido.TabIndex = 0;
            this.btnComprobarPedido.Text = "COMPROBAR";
            this.btnComprobarPedido.UseVisualStyleBackColor = true;
            this.btnComprobarPedido.Click += new System.EventHandler(this.btnComprobarPedido_Click);
            // 
            // dgLanzamientos
            // 
            this.dgLanzamientos.AllowUserToAddRows = false;
            this.dgLanzamientos.AllowUserToDeleteRows = false;
            this.dgLanzamientos.AllowUserToResizeColumns = false;
            this.dgLanzamientos.AllowUserToResizeRows = false;
            this.dgLanzamientos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgLanzamientos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Codigo,
            this.Denominacion,
            this.Lineas,
            this.CmabiarDenominacion,
            this.Lanzado});
            this.dgLanzamientos.Location = new System.Drawing.Point(8, 155);
            this.dgLanzamientos.Name = "dgLanzamientos";
            this.dgLanzamientos.Size = new System.Drawing.Size(1359, 551);
            this.dgLanzamientos.TabIndex = 1;
            this.dgLanzamientos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgLanzamientos_CellClick);
            this.dgLanzamientos.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgLanzamientos_CellLeave);
            // 
            // Codigo
            // 
            this.Codigo.HeaderText = "Código";
            this.Codigo.Name = "Codigo";
            // 
            // Denominacion
            // 
            this.Denominacion.HeaderText = "Denominación";
            this.Denominacion.Name = "Denominacion";
            // 
            // Lineas
            // 
            this.Lineas.HeaderText = "Líneas Desarrollo";
            this.Lineas.Name = "Lineas";
            // 
            // CmabiarDenominacion
            // 
            this.CmabiarDenominacion.HeaderText = "Cambiar Denominación";
            this.CmabiarDenominacion.Name = "CmabiarDenominacion";
            // 
            // Lanzado
            // 
            this.Lanzado.HeaderText = "Lanzados";
            this.Lanzado.Name = "Lanzado";
            this.Lanzado.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Lanzado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // btnAnadirFila
            // 
            this.btnAnadirFila.Enabled = false;
            this.btnAnadirFila.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnadirFila.Location = new System.Drawing.Point(8, 117);
            this.btnAnadirFila.Name = "btnAnadirFila";
            this.btnAnadirFila.Size = new System.Drawing.Size(206, 32);
            this.btnAnadirFila.TabIndex = 2;
            this.btnAnadirFila.Text = "Añadir Fila";
            this.btnAnadirFila.UseVisualStyleBackColor = true;
            this.btnAnadirFila.Click += new System.EventHandler(this.btnAñadirFila_Click);
            // 
            // btnLanzar
            // 
            this.btnLanzar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLanzar.Location = new System.Drawing.Point(1098, 713);
            this.btnLanzar.Name = "btnLanzar";
            this.btnLanzar.Size = new System.Drawing.Size(269, 40);
            this.btnLanzar.TabIndex = 3;
            this.btnLanzar.Text = "LANZAR CÓDIGOS";
            this.btnLanzar.UseVisualStyleBackColor = true;
            this.btnLanzar.Click += new System.EventHandler(this.btnLanzar_Click);
            // 
            // btnQuitar
            // 
            this.btnQuitar.Enabled = false;
            this.btnQuitar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuitar.Location = new System.Drawing.Point(1098, 117);
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Size = new System.Drawing.Size(269, 32);
            this.btnQuitar.TabIndex = 4;
            this.btnQuitar.Text = "Quitar Filas Seleccionadas";
            this.btnQuitar.UseVisualStyleBackColor = true;
            this.btnQuitar.Click += new System.EventHandler(this.btnQuitar_Click);
            // 
            // btnVaciar
            // 
            this.btnVaciar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVaciar.Location = new System.Drawing.Point(6, 63);
            this.btnVaciar.Name = "btnVaciar";
            this.btnVaciar.Size = new System.Drawing.Size(200, 23);
            this.btnVaciar.TabIndex = 3;
            this.btnVaciar.Text = "Limpiar cuadro de pedido";
            this.btnVaciar.UseVisualStyleBackColor = true;
            this.btnVaciar.Click += new System.EventHandler(this.btnVaciar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1379, 757);
            this.Controls.Add(this.btnQuitar);
            this.Controls.Add(this.btnLanzar);
            this.Controls.Add(this.btnAnadirFila);
            this.Controls.Add(this.dgLanzamientos);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lanzamiento";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLanzamientos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgLanzamientos;
        private System.Windows.Forms.TextBox tbPedido;
        private System.Windows.Forms.Label lbPedido;
        private System.Windows.Forms.Button btnComprobarPedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Denominacion;
        private System.Windows.Forms.DataGridViewComboBoxColumn Lineas;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CmabiarDenominacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Lanzado;
        private System.Windows.Forms.Button btnAnadirFila;
        private System.Windows.Forms.Button btnLanzar;
        private System.Windows.Forms.Button btnQuitar;
        private System.Windows.Forms.Button btnVaciar;
    }
}

