namespace TorresStock
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.gbBusquedaTorre = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbPedido = new System.Windows.Forms.Label();
            this.tbPedido = new System.Windows.Forms.TextBox();
            this.btnAddPedido = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbTorre = new System.Windows.Forms.Label();
            this.tbCantidad = new System.Windows.Forms.TextBox();
            this.tbCodigo = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.gbResultado = new System.Windows.Forms.GroupBox();
            this.rbStock = new System.Windows.Forms.RadioButton();
            this.rbTodos = new System.Windows.Forms.RadioButton();
            this.dgCodigoCantidad = new System.Windows.Forms.DataGridView();
            this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PEDIDO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LINEA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgComprobacionStock = new System.Windows.Forms.DataGridView();
            this.btnComprobarStock = new System.Windows.Forms.Button();
            this.lbComprobar = new System.Windows.Forms.Label();
            this.gbPrincipal = new System.Windows.Forms.GroupBox();
            this.btnEliminarFila = new System.Windows.Forms.Button();
            this.btnLimpiarTabla = new System.Windows.Forms.Button();
            this.gbBusquedaTorre.SuspendLayout();
            this.gbResultado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgCodigoCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgComprobacionStock)).BeginInit();
            this.gbPrincipal.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbBusquedaTorre
            // 
            this.gbBusquedaTorre.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gbBusquedaTorre.Controls.Add(this.label2);
            this.gbBusquedaTorre.Controls.Add(this.lbPedido);
            this.gbBusquedaTorre.Controls.Add(this.tbPedido);
            this.gbBusquedaTorre.Controls.Add(this.btnAddPedido);
            this.gbBusquedaTorre.Controls.Add(this.label1);
            this.gbBusquedaTorre.Controls.Add(this.lbTorre);
            this.gbBusquedaTorre.Controls.Add(this.tbCantidad);
            this.gbBusquedaTorre.Controls.Add(this.tbCodigo);
            this.gbBusquedaTorre.Controls.Add(this.btnAdd);
            this.gbBusquedaTorre.Location = new System.Drawing.Point(6, 19);
            this.gbBusquedaTorre.Name = "gbBusquedaTorre";
            this.gbBusquedaTorre.Size = new System.Drawing.Size(599, 221);
            this.gbBusquedaTorre.TabIndex = 0;
            this.gbBusquedaTorre.TabStop = false;
            this.gbBusquedaTorre.Text = "Busqueda :";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(695, 2);
            this.label2.TabIndex = 78;
            // 
            // lbPedido
            // 
            this.lbPedido.AutoSize = true;
            this.lbPedido.Location = new System.Drawing.Point(6, 152);
            this.lbPedido.Name = "lbPedido";
            this.lbPedido.Size = new System.Drawing.Size(40, 13);
            this.lbPedido.TabIndex = 77;
            this.lbPedido.Text = "Pedido";
            // 
            // tbPedido
            // 
            this.tbPedido.Location = new System.Drawing.Point(9, 168);
            this.tbPedido.Name = "tbPedido";
            this.tbPedido.Size = new System.Drawing.Size(193, 20);
            this.tbPedido.TabIndex = 76;
            // 
            // btnAddPedido
            // 
            this.btnAddPedido.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnAddPedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddPedido.Location = new System.Drawing.Point(262, 155);
            this.btnAddPedido.Name = "btnAddPedido";
            this.btnAddPedido.Size = new System.Drawing.Size(253, 40);
            this.btnAddPedido.TabIndex = 75;
            this.btnAddPedido.Text = "AÑADIR PEDIDO";
            this.btnAddPedido.UseVisualStyleBackColor = false;
            this.btnAddPedido.Click += new System.EventHandler(this.btnAddPedido_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Cantidad: ";
            // 
            // lbTorre
            // 
            this.lbTorre.AutoSize = true;
            this.lbTorre.Location = new System.Drawing.Point(6, 23);
            this.lbTorre.Name = "lbTorre";
            this.lbTorre.Size = new System.Drawing.Size(94, 13);
            this.lbTorre.TabIndex = 3;
            this.lbTorre.Text = "Código Estructura:";
            // 
            // tbCantidad
            // 
            this.tbCantidad.Location = new System.Drawing.Point(9, 87);
            this.tbCantidad.Name = "tbCantidad";
            this.tbCantidad.Size = new System.Drawing.Size(193, 20);
            this.tbCantidad.TabIndex = 2;
            // 
            // tbCodigo
            // 
            this.tbCodigo.Location = new System.Drawing.Point(9, 39);
            this.tbCodigo.Name = "tbCodigo";
            this.tbCodigo.Size = new System.Drawing.Size(193, 20);
            this.tbCodigo.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(262, 51);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(253, 40);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "AÑADIR CÓDIGO";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // gbResultado
            // 
            this.gbResultado.Controls.Add(this.rbStock);
            this.gbResultado.Controls.Add(this.rbTodos);
            this.gbResultado.Location = new System.Drawing.Point(713, 19);
            this.gbResultado.Name = "gbResultado";
            this.gbResultado.Size = new System.Drawing.Size(747, 59);
            this.gbResultado.TabIndex = 79;
            this.gbResultado.TabStop = false;
            this.gbResultado.Text = "Resultados a mostrar:";
            // 
            // rbStock
            // 
            this.rbStock.AutoSize = true;
            this.rbStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbStock.Location = new System.Drawing.Point(390, 23);
            this.rbStock.Name = "rbStock";
            this.rbStock.Size = new System.Drawing.Size(290, 20);
            this.rbStock.TabIndex = 1;
            this.rbStock.TabStop = true;
            this.rbStock.Text = "CÓDIGOS SIN EL STOCK NECESARIO";
            this.rbStock.UseVisualStyleBackColor = true;
            // 
            // rbTodos
            // 
            this.rbTodos.AutoSize = true;
            this.rbTodos.Checked = true;
            this.rbTodos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbTodos.Location = new System.Drawing.Point(40, 23);
            this.rbTodos.Name = "rbTodos";
            this.rbTodos.Size = new System.Drawing.Size(184, 20);
            this.rbTodos.TabIndex = 0;
            this.rbTodos.TabStop = true;
            this.rbTodos.Text = "TODOS LOS CÓDIGOS";
            this.rbTodos.UseVisualStyleBackColor = true;
            // 
            // dgCodigoCantidad
            // 
            this.dgCodigoCantidad.AllowUserToAddRows = false;
            this.dgCodigoCantidad.AllowUserToDeleteRows = false;
            this.dgCodigoCantidad.AllowUserToOrderColumns = true;
            this.dgCodigoCantidad.AllowUserToResizeColumns = false;
            this.dgCodigoCantidad.AllowUserToResizeRows = false;
            this.dgCodigoCantidad.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgCodigoCantidad.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCodigoCantidad.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Codigo,
            this.Cantidad,
            this.PEDIDO,
            this.LINEA});
            this.dgCodigoCantidad.Location = new System.Drawing.Point(6, 246);
            this.dgCodigoCantidad.Name = "dgCodigoCantidad";
            this.dgCodigoCantidad.Size = new System.Drawing.Size(599, 513);
            this.dgCodigoCantidad.TabIndex = 1;
            // 
            // Codigo
            // 
            this.Codigo.HeaderText = "CÓDIGO ESTRUCTURA";
            this.Codigo.Name = "Codigo";
            // 
            // Cantidad
            // 
            this.Cantidad.HeaderText = "CANTIDAD";
            this.Cantidad.Name = "Cantidad";
            // 
            // PEDIDO
            // 
            this.PEDIDO.HeaderText = "PEDIDO";
            this.PEDIDO.Name = "PEDIDO";
            // 
            // LINEA
            // 
            this.LINEA.HeaderText = "LINEA";
            this.LINEA.Name = "LINEA";
            // 
            // dgComprobacionStock
            // 
            this.dgComprobacionStock.AllowUserToAddRows = false;
            this.dgComprobacionStock.AllowUserToDeleteRows = false;
            this.dgComprobacionStock.AllowUserToOrderColumns = true;
            this.dgComprobacionStock.AllowUserToResizeColumns = false;
            this.dgComprobacionStock.AllowUserToResizeRows = false;
            this.dgComprobacionStock.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgComprobacionStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgComprobacionStock.Location = new System.Drawing.Point(713, 84);
            this.dgComprobacionStock.Name = "dgComprobacionStock";
            this.dgComprobacionStock.Size = new System.Drawing.Size(747, 708);
            this.dgComprobacionStock.TabIndex = 4;
            // 
            // btnComprobarStock
            // 
            this.btnComprobarStock.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnComprobarStock.BackgroundImage")));
            this.btnComprobarStock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnComprobarStock.Location = new System.Drawing.Point(611, 495);
            this.btnComprobarStock.Name = "btnComprobarStock";
            this.btnComprobarStock.Size = new System.Drawing.Size(96, 53);
            this.btnComprobarStock.TabIndex = 8;
            this.btnComprobarStock.UseVisualStyleBackColor = true;
            this.btnComprobarStock.Click += new System.EventHandler(this.btnComprobarStock_Click);
            // 
            // lbComprobar
            // 
            this.lbComprobar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbComprobar.Location = new System.Drawing.Point(611, 454);
            this.lbComprobar.Name = "lbComprobar";
            this.lbComprobar.Size = new System.Drawing.Size(87, 38);
            this.lbComprobar.TabIndex = 9;
            this.lbComprobar.Text = "Comprobar Stock";
            this.lbComprobar.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // gbPrincipal
            // 
            this.gbPrincipal.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gbPrincipal.Controls.Add(this.gbResultado);
            this.gbPrincipal.Controls.Add(this.btnEliminarFila);
            this.gbPrincipal.Controls.Add(this.btnLimpiarTabla);
            this.gbPrincipal.Controls.Add(this.gbBusquedaTorre);
            this.gbPrincipal.Controls.Add(this.lbComprobar);
            this.gbPrincipal.Controls.Add(this.dgComprobacionStock);
            this.gbPrincipal.Controls.Add(this.btnComprobarStock);
            this.gbPrincipal.Controls.Add(this.dgCodigoCantidad);
            this.gbPrincipal.Location = new System.Drawing.Point(12, 12);
            this.gbPrincipal.Name = "gbPrincipal";
            this.gbPrincipal.Size = new System.Drawing.Size(1466, 801);
            this.gbPrincipal.TabIndex = 10;
            this.gbPrincipal.TabStop = false;
            // 
            // btnEliminarFila
            // 
            this.btnEliminarFila.Location = new System.Drawing.Point(391, 765);
            this.btnEliminarFila.Name = "btnEliminarFila";
            this.btnEliminarFila.Size = new System.Drawing.Size(214, 27);
            this.btnEliminarFila.TabIndex = 11;
            this.btnEliminarFila.Text = "Eliminar filas seleccionadas";
            this.btnEliminarFila.UseVisualStyleBackColor = true;
            this.btnEliminarFila.Click += new System.EventHandler(this.btnEliminarFila_Click);
            // 
            // btnLimpiarTabla
            // 
            this.btnLimpiarTabla.Location = new System.Drawing.Point(6, 765);
            this.btnLimpiarTabla.Name = "btnLimpiarTabla";
            this.btnLimpiarTabla.Size = new System.Drawing.Size(162, 27);
            this.btnLimpiarTabla.TabIndex = 10;
            this.btnLimpiarTabla.Text = "Limpiar Tabla";
            this.btnLimpiarTabla.UseVisualStyleBackColor = true;
            this.btnLimpiarTabla.Click += new System.EventHandler(this.btnLimpiarTabla_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1490, 817);
            this.Controls.Add(this.gbPrincipal);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Comprobación de Stock";
            this.gbBusquedaTorre.ResumeLayout(false);
            this.gbBusquedaTorre.PerformLayout();
            this.gbResultado.ResumeLayout(false);
            this.gbResultado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgCodigoCantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgComprobacionStock)).EndInit();
            this.gbPrincipal.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbComprobar;
        private System.Windows.Forms.Button btnComprobarStock;
        private System.Windows.Forms.DataGridView dgComprobacionStock;
        public System.Windows.Forms.DataGridView dgCodigoCantidad;
        private System.Windows.Forms.GroupBox gbBusquedaTorre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbTorre;
        private System.Windows.Forms.TextBox tbCantidad;
        private System.Windows.Forms.TextBox tbCodigo;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn PEDIDO;
        private System.Windows.Forms.DataGridViewTextBoxColumn LINEA;
        private System.Windows.Forms.GroupBox gbPrincipal;
        private System.Windows.Forms.Button btnLimpiarTabla;
        private System.Windows.Forms.Button btnEliminarFila;
        private System.Windows.Forms.Label lbPedido;
        private System.Windows.Forms.TextBox tbPedido;
        private System.Windows.Forms.Button btnAddPedido;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbResultado;
        private System.Windows.Forms.RadioButton rbStock;
        private System.Windows.Forms.RadioButton rbTodos;
    }
}

