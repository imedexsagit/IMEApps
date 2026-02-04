namespace PlanosPedidos
{
    partial class PlanosPedidos
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPedido = new System.Windows.Forms.TextBox();
            this.buttonCargarLineas = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxCliente = new System.Windows.Forms.TextBox();
            this.dataGridViewLineas = new System.Windows.Forms.DataGridView();
            this.checkBoxDesarrollo = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonSeleccionarTodo = new System.Windows.Forms.Button();
            this.buttonSeleccionarTodoExceptoDesarrollo = new System.Windows.Forms.Button();
            this.buttonSeleccionarNada = new System.Windows.Forms.Button();
            this.buttonGenerarDocumentacion = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxNombreCarpeta = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxCabecera = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLineas)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Pedido:";
            // 
            // textBoxPedido
            // 
            this.textBoxPedido.Location = new System.Drawing.Point(75, 15);
            this.textBoxPedido.Name = "textBoxPedido";
            this.textBoxPedido.Size = new System.Drawing.Size(80, 20);
            this.textBoxPedido.TabIndex = 2;
            // 
            // buttonCargarLineas
            // 
            this.buttonCargarLineas.Location = new System.Drawing.Point(183, 13);
            this.buttonCargarLineas.Name = "buttonCargarLineas";
            this.buttonCargarLineas.Size = new System.Drawing.Size(89, 23);
            this.buttonCargarLineas.TabIndex = 3;
            this.buttonCargarLineas.Text = "Cargar Líneas";
            this.buttonCargarLineas.UseVisualStyleBackColor = true;
            this.buttonCargarLineas.Click += new System.EventHandler(this.buttonCargarLineas_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Cliente:";
            // 
            // textBoxCliente
            // 
            this.textBoxCliente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.textBoxCliente.Location = new System.Drawing.Point(75, 43);
            this.textBoxCliente.Name = "textBoxCliente";
            this.textBoxCliente.ReadOnly = true;
            this.textBoxCliente.Size = new System.Drawing.Size(818, 20);
            this.textBoxCliente.TabIndex = 5;
            // 
            // dataGridViewLineas
            // 
            this.dataGridViewLineas.AllowUserToAddRows = false;
            this.dataGridViewLineas.AllowUserToDeleteRows = false;
            this.dataGridViewLineas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLineas.Location = new System.Drawing.Point(15, 174);
            this.dataGridViewLineas.Name = "dataGridViewLineas";
            this.dataGridViewLineas.ReadOnly = true;
            this.dataGridViewLineas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewLineas.Size = new System.Drawing.Size(878, 571);
            this.dataGridViewLineas.TabIndex = 6;
            this.dataGridViewLineas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewLineas_CellClick);
            this.dataGridViewLineas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewLineas_CellContentClick);
            // 
            // checkBoxDesarrollo
            // 
            this.checkBoxDesarrollo.AutoSize = true;
            this.checkBoxDesarrollo.Checked = true;
            this.checkBoxDesarrollo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDesarrollo.Location = new System.Drawing.Point(295, 17);
            this.checkBoxDesarrollo.Name = "checkBoxDesarrollo";
            this.checkBoxDesarrollo.Size = new System.Drawing.Size(184, 17);
            this.checkBoxDesarrollo.TabIndex = 7;
            this.checkBoxDesarrollo.Text = "Mostrar líneas en DESARROLLO";
            this.checkBoxDesarrollo.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Seleccionar:";
            // 
            // buttonSeleccionarTodo
            // 
            this.buttonSeleccionarTodo.Location = new System.Drawing.Point(94, 109);
            this.buttonSeleccionarTodo.Name = "buttonSeleccionarTodo";
            this.buttonSeleccionarTodo.Size = new System.Drawing.Size(89, 23);
            this.buttonSeleccionarTodo.TabIndex = 9;
            this.buttonSeleccionarTodo.Text = "Todo";
            this.buttonSeleccionarTodo.UseVisualStyleBackColor = true;
            this.buttonSeleccionarTodo.Click += new System.EventHandler(this.buttonSeleccionarTodo_Click);
            // 
            // buttonSeleccionarTodoExceptoDesarrollo
            // 
            this.buttonSeleccionarTodoExceptoDesarrollo.Location = new System.Drawing.Point(210, 109);
            this.buttonSeleccionarTodoExceptoDesarrollo.Name = "buttonSeleccionarTodoExceptoDesarrollo";
            this.buttonSeleccionarTodoExceptoDesarrollo.Size = new System.Drawing.Size(174, 23);
            this.buttonSeleccionarTodoExceptoDesarrollo.TabIndex = 10;
            this.buttonSeleccionarTodoExceptoDesarrollo.Text = "Todo excepto DESARROLLO";
            this.buttonSeleccionarTodoExceptoDesarrollo.UseVisualStyleBackColor = true;
            this.buttonSeleccionarTodoExceptoDesarrollo.Click += new System.EventHandler(this.buttonSeleccionarTodoExceptoDesarrollo_Click);
            // 
            // buttonSeleccionarNada
            // 
            this.buttonSeleccionarNada.Location = new System.Drawing.Point(409, 109);
            this.buttonSeleccionarNada.Name = "buttonSeleccionarNada";
            this.buttonSeleccionarNada.Size = new System.Drawing.Size(89, 23);
            this.buttonSeleccionarNada.TabIndex = 11;
            this.buttonSeleccionarNada.Text = "Nada";
            this.buttonSeleccionarNada.UseVisualStyleBackColor = true;
            this.buttonSeleccionarNada.Click += new System.EventHandler(this.buttonSeleccionarNada_Click);
            // 
            // buttonGenerarDocumentacion
            // 
            this.buttonGenerarDocumentacion.Location = new System.Drawing.Point(752, 750);
            this.buttonGenerarDocumentacion.Name = "buttonGenerarDocumentacion";
            this.buttonGenerarDocumentacion.Size = new System.Drawing.Size(141, 41);
            this.buttonGenerarDocumentacion.TabIndex = 12;
            this.buttonGenerarDocumentacion.Text = "GENERAR DOCUMENTACIÓN";
            this.buttonGenerarDocumentacion.UseVisualStyleBackColor = true;
            this.buttonGenerarDocumentacion.Click += new System.EventHandler(this.buttonGenerarDocumentacion_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 764);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Nombre carpeta:";
            // 
            // textBoxNombreCarpeta
            // 
            this.textBoxNombreCarpeta.Location = new System.Drawing.Point(104, 759);
            this.textBoxNombreCarpeta.Name = "textBoxNombreCarpeta";
            this.textBoxNombreCarpeta.Size = new System.Drawing.Size(604, 20);
            this.textBoxNombreCarpeta.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Cabecera:";
            // 
            // textBoxCabecera
            // 
            this.textBoxCabecera.BackColor = System.Drawing.Color.White;
            this.textBoxCabecera.Location = new System.Drawing.Point(75, 71);
            this.textBoxCabecera.Name = "textBoxCabecera";
            this.textBoxCabecera.Size = new System.Drawing.Size(818, 20);
            this.textBoxCabecera.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 145);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Buscar:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "LINEA",
            "CODIGO",
            "CATEGORIA",
            "DENOMINACION"});
            this.comboBox1.Location = new System.Drawing.Point(395, 138);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(145, 21);
            this.comboBox1.TabIndex = 19;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(363, 143);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Por:";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(75, 140);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(282, 20);
            this.textBox1.TabIndex = 21;
            // 
            // PlanosPedidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(905, 797);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxCabecera);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxNombreCarpeta);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonGenerarDocumentacion);
            this.Controls.Add(this.buttonSeleccionarNada);
            this.Controls.Add(this.buttonSeleccionarTodoExceptoDesarrollo);
            this.Controls.Add(this.buttonSeleccionarTodo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBoxDesarrollo);
            this.Controls.Add(this.dataGridViewLineas);
            this.Controls.Add(this.textBoxCliente);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonCargarLineas);
            this.Controls.Add(this.textBoxPedido);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "PlanosPedidos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "7";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PlanosPedidos_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLineas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPedido;
        private System.Windows.Forms.Button buttonCargarLineas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxCliente;
        private System.Windows.Forms.DataGridView dataGridViewLineas;
        private System.Windows.Forms.CheckBox checkBoxDesarrollo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonSeleccionarTodo;
        private System.Windows.Forms.Button buttonSeleccionarTodoExceptoDesarrollo;
        private System.Windows.Forms.Button buttonSeleccionarNada;
        private System.Windows.Forms.Button buttonGenerarDocumentacion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxNombreCarpeta;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxCabecera;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox1;
    }
}

