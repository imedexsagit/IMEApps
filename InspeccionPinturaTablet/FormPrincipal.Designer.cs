namespace InspeccionPinturaTablet
{
    partial class FormPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrincipal));
            this.comboBoxInsPintura = new System.Windows.Forms.ComboBox();
            this.crearInspeccion = new System.Windows.Forms.Button();
            this.modificarInspecciones = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxCliente = new System.Windows.Forms.TextBox();
            this.textBoxPedido = new System.Windows.Forms.TextBox();
            this.textBoxContenedor = new System.Windows.Forms.TextBox();
            this.textBoxPlanCalidad = new System.Windows.Forms.TextBox();
            this.textBoxCertificado = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxProyectos = new System.Windows.Forms.TextBox();
            this.textBoxPackingList = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxTorre = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxInspector = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxLugar = new System.Windows.Forms.TextBox();
            this.boton1 = new System.Windows.Forms.Button();
            this.boton2 = new System.Windows.Forms.Button();
            this.boton3 = new System.Windows.Forms.Button();
            this.boton5 = new System.Windows.Forms.Button();
            this.boton4 = new System.Windows.Forms.Button();
            this.boton6 = new System.Windows.Forms.Button();
            this.boton7 = new System.Windows.Forms.Button();
            this.eliminarInspeccion = new System.Windows.Forms.Button();
            this.generarDocumento = new System.Windows.Forms.Button();
            this.boton8 = new System.Windows.Forms.Button();
            this.Finalizada = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // comboBoxInsPintura
            // 
            this.comboBoxInsPintura.FormattingEnabled = true;
            this.comboBoxInsPintura.Location = new System.Drawing.Point(12, 48);
            this.comboBoxInsPintura.Name = "comboBoxInsPintura";
            this.comboBoxInsPintura.Size = new System.Drawing.Size(791, 26);
            this.comboBoxInsPintura.TabIndex = 1;
            this.comboBoxInsPintura.SelectedIndexChanged += new System.EventHandler(this.comboBoxInsPintura_SelectedIndexChanged);
            // 
            // crearInspeccion
            // 
            this.crearInspeccion.BackColor = System.Drawing.Color.Blue;
            this.crearInspeccion.ForeColor = System.Drawing.SystemColors.Window;
            this.crearInspeccion.Location = new System.Drawing.Point(12, 12);
            this.crearInspeccion.Name = "crearInspeccion";
            this.crearInspeccion.Size = new System.Drawing.Size(128, 30);
            this.crearInspeccion.TabIndex = 2;
            this.crearInspeccion.Text = "Crear Inspección";
            this.crearInspeccion.UseVisualStyleBackColor = false;
            this.crearInspeccion.Click += new System.EventHandler(this.crearInspeccion_Click);
            // 
            // modificarInspecciones
            // 
            this.modificarInspecciones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.modificarInspecciones.ForeColor = System.Drawing.Color.White;
            this.modificarInspecciones.Location = new System.Drawing.Point(636, 12);
            this.modificarInspecciones.Name = "modificarInspecciones";
            this.modificarInspecciones.Size = new System.Drawing.Size(167, 30);
            this.modificarInspecciones.TabIndex = 4;
            this.modificarInspecciones.Text = "GUARDAR CAMBIOS";
            this.modificarInspecciones.UseVisualStyleBackColor = false;
            this.modificarInspecciones.Click += new System.EventHandler(this.guardarcambios_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(65, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 18);
            this.label1.TabIndex = 5;
            this.label1.Text = "Cliente:";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 18);
            this.label2.TabIndex = 6;
            this.label2.Text = "Contenedor:";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(65, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 18);
            this.label3.TabIndex = 7;
            this.label3.Text = "Pedido:";
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 183);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 18);
            this.label4.TabIndex = 8;
            this.label4.Text = "Plan  de Calidad:";
            this.label4.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(43, 215);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 18);
            this.label5.TabIndex = 9;
            this.label5.Text = "Certificado:";
            this.label5.Visible = false;
            // 
            // textBoxCliente
            // 
            this.textBoxCliente.Location = new System.Drawing.Point(129, 84);
            this.textBoxCliente.Name = "textBoxCliente";
            this.textBoxCliente.Size = new System.Drawing.Size(256, 26);
            this.textBoxCliente.TabIndex = 10;
            this.textBoxCliente.Visible = false;
            // 
            // textBoxPedido
            // 
            this.textBoxPedido.Location = new System.Drawing.Point(128, 116);
            this.textBoxPedido.Name = "textBoxPedido";
            this.textBoxPedido.Size = new System.Drawing.Size(257, 26);
            this.textBoxPedido.TabIndex = 11;
            this.textBoxPedido.Visible = false;
            // 
            // textBoxContenedor
            // 
            this.textBoxContenedor.Location = new System.Drawing.Point(128, 148);
            this.textBoxContenedor.Name = "textBoxContenedor";
            this.textBoxContenedor.Size = new System.Drawing.Size(257, 26);
            this.textBoxContenedor.TabIndex = 12;
            this.textBoxContenedor.Visible = false;
            // 
            // textBoxPlanCalidad
            // 
            this.textBoxPlanCalidad.Location = new System.Drawing.Point(128, 180);
            this.textBoxPlanCalidad.Name = "textBoxPlanCalidad";
            this.textBoxPlanCalidad.Size = new System.Drawing.Size(257, 26);
            this.textBoxPlanCalidad.TabIndex = 13;
            this.textBoxPlanCalidad.Visible = false;
            // 
            // textBoxCertificado
            // 
            this.textBoxCertificado.Location = new System.Drawing.Point(129, 212);
            this.textBoxCertificado.Name = "textBoxCertificado";
            this.textBoxCertificado.Size = new System.Drawing.Size(256, 26);
            this.textBoxCertificado.TabIndex = 14;
            this.textBoxCertificado.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(451, 124);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 18);
            this.label6.TabIndex = 15;
            this.label6.Text = "Packing list:";
            this.label6.Visible = false;
            // 
            // textBoxProyectos
            // 
            this.textBoxProyectos.Location = new System.Drawing.Point(547, 84);
            this.textBoxProyectos.Name = "textBoxProyectos";
            this.textBoxProyectos.Size = new System.Drawing.Size(256, 26);
            this.textBoxProyectos.TabIndex = 17;
            this.textBoxProyectos.Visible = false;
            // 
            // textBoxPackingList
            // 
            this.textBoxPackingList.Location = new System.Drawing.Point(547, 116);
            this.textBoxPackingList.Name = "textBoxPackingList";
            this.textBoxPackingList.Size = new System.Drawing.Size(256, 26);
            this.textBoxPackingList.TabIndex = 18;
            this.textBoxPackingList.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(465, 92);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 18);
            this.label7.TabIndex = 19;
            this.label7.Text = "Proyecto:";
            this.label7.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(488, 159);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 18);
            this.label8.TabIndex = 20;
            this.label8.Text = "Torre:";
            this.label8.Visible = false;
            // 
            // textBoxTorre
            // 
            this.textBoxTorre.Location = new System.Drawing.Point(547, 151);
            this.textBoxTorre.Name = "textBoxTorre";
            this.textBoxTorre.Size = new System.Drawing.Size(256, 26);
            this.textBoxTorre.TabIndex = 21;
            this.textBoxTorre.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(488, 190);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 18);
            this.label9.TabIndex = 23;
            this.label9.Text = "Fecha:";
            this.label9.Visible = false;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            this.dateTimePicker1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Location = new System.Drawing.Point(547, 186);
            this.dateTimePicker1.MaxDate = new System.DateTime(9998, 12, 17, 0, 0, 0, 0);
            this.dateTimePicker1.MinDate = new System.DateTime(1753, 1, 17, 0, 0, 0, 0);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(256, 22);
            this.dateTimePicker1.TabIndex = 24;
            this.dateTimePicker1.TabStop = false;
            this.dateTimePicker1.Value = new System.DateTime(2022, 2, 16, 0, 0, 0, 0);
            this.dateTimePicker1.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 399);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 18);
            this.label10.TabIndex = 25;
            this.label10.Text = "Inspector:";
            this.label10.Visible = false;
            // 
            // textBoxInspector
            // 
            this.textBoxInspector.Location = new System.Drawing.Point(88, 391);
            this.textBoxInspector.Name = "textBoxInspector";
            this.textBoxInspector.Size = new System.Drawing.Size(715, 26);
            this.textBoxInspector.TabIndex = 26;
            this.textBoxInspector.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(37, 440);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 18);
            this.label11.TabIndex = 27;
            this.label11.Text = "Lugar:";
            this.label11.Visible = false;
            // 
            // textBoxLugar
            // 
            this.textBoxLugar.Location = new System.Drawing.Point(88, 432);
            this.textBoxLugar.Name = "textBoxLugar";
            this.textBoxLugar.Size = new System.Drawing.Size(715, 26);
            this.textBoxLugar.TabIndex = 28;
            this.textBoxLugar.Visible = false;
            // 
            // boton1
            // 
            this.boton1.BackColor = System.Drawing.Color.Blue;
            this.boton1.ForeColor = System.Drawing.SystemColors.Window;
            this.boton1.Location = new System.Drawing.Point(3, 244);
            this.boton1.Name = "boton1";
            this.boton1.Size = new System.Drawing.Size(400, 30);
            this.boton1.TabIndex = 29;
            this.boton1.Text = "1. Inspección de paquetes";
            this.boton1.UseVisualStyleBackColor = false;
            this.boton1.Visible = false;
            this.boton1.Click += new System.EventHandler(this.boton1_Click);
            // 
            // boton2
            // 
            this.boton2.BackColor = System.Drawing.Color.Blue;
            this.boton2.ForeColor = System.Drawing.SystemColors.Window;
            this.boton2.Location = new System.Drawing.Point(409, 244);
            this.boton2.Name = "boton2";
            this.boton2.Size = new System.Drawing.Size(400, 30);
            this.boton2.TabIndex = 31;
            this.boton2.Text = "2. Inspección granallado";
            this.boton2.UseVisualStyleBackColor = false;
            this.boton2.Visible = false;
            this.boton2.Click += new System.EventHandler(this.boton2_Click);
            // 
            // boton3
            // 
            this.boton3.BackColor = System.Drawing.Color.Blue;
            this.boton3.ForeColor = System.Drawing.SystemColors.Window;
            this.boton3.Location = new System.Drawing.Point(3, 280);
            this.boton3.Name = "boton3";
            this.boton3.Size = new System.Drawing.Size(400, 30);
            this.boton3.TabIndex = 32;
            this.boton3.Text = "3. Ensayos de evaluación de rugosidad y limpieza superficial";
            this.boton3.UseVisualStyleBackColor = false;
            this.boton3.Visible = false;
            this.boton3.Click += new System.EventHandler(this.boton3_Click);
            // 
            // boton5
            // 
            this.boton5.BackColor = System.Drawing.Color.Blue;
            this.boton5.ForeColor = System.Drawing.SystemColors.Window;
            this.boton5.Location = new System.Drawing.Point(3, 316);
            this.boton5.Name = "boton5";
            this.boton5.Size = new System.Drawing.Size(400, 30);
            this.boton5.TabIndex = 33;
            this.boton5.Text = "5. Espesor";
            this.boton5.UseVisualStyleBackColor = false;
            this.boton5.Visible = false;
            this.boton5.Click += new System.EventHandler(this.boton5_Click);
            // 
            // boton4
            // 
            this.boton4.BackColor = System.Drawing.Color.Blue;
            this.boton4.ForeColor = System.Drawing.SystemColors.Window;
            this.boton4.Location = new System.Drawing.Point(409, 280);
            this.boton4.Name = "boton4";
            this.boton4.Size = new System.Drawing.Size(400, 30);
            this.boton4.TabIndex = 34;
            this.boton4.Text = "4. Inspección de la aplicación de la pintura de recubrimiento";
            this.boton4.UseVisualStyleBackColor = false;
            this.boton4.Visible = false;
            this.boton4.Click += new System.EventHandler(this.boton4_Click);
            // 
            // boton6
            // 
            this.boton6.BackColor = System.Drawing.Color.Blue;
            this.boton6.ForeColor = System.Drawing.SystemColors.Window;
            this.boton6.Location = new System.Drawing.Point(409, 316);
            this.boton6.Name = "boton6";
            this.boton6.Size = new System.Drawing.Size(400, 30);
            this.boton6.TabIndex = 35;
            this.boton6.Text = "6. Inspección final de la pintura de recubrimiento";
            this.boton6.UseVisualStyleBackColor = false;
            this.boton6.Visible = false;
            this.boton6.Click += new System.EventHandler(this.boton6_Click);
            // 
            // boton7
            // 
            this.boton7.BackColor = System.Drawing.Color.Blue;
            this.boton7.ForeColor = System.Drawing.SystemColors.Window;
            this.boton7.Location = new System.Drawing.Point(3, 352);
            this.boton7.Name = "boton7";
            this.boton7.Size = new System.Drawing.Size(400, 30);
            this.boton7.TabIndex = 36;
            this.boton7.Text = "7. Comentarios";
            this.boton7.UseVisualStyleBackColor = false;
            this.boton7.Visible = false;
            this.boton7.Click += new System.EventHandler(this.boton7_Click);
            // 
            // eliminarInspeccion
            // 
            this.eliminarInspeccion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.eliminarInspeccion.ForeColor = System.Drawing.SystemColors.Window;
            this.eliminarInspeccion.Location = new System.Drawing.Point(146, 12);
            this.eliminarInspeccion.Name = "eliminarInspeccion";
            this.eliminarInspeccion.Size = new System.Drawing.Size(153, 30);
            this.eliminarInspeccion.TabIndex = 37;
            this.eliminarInspeccion.Text = "Eliminar Inspección";
            this.eliminarInspeccion.UseVisualStyleBackColor = false;
            this.eliminarInspeccion.Click += new System.EventHandler(this.eliminarInspeccion_Click);
            // 
            // generarDocumento
            // 
            this.generarDocumento.BackColor = System.Drawing.Color.DimGray;
            this.generarDocumento.ForeColor = System.Drawing.SystemColors.Window;
            this.generarDocumento.Location = new System.Drawing.Point(334, 464);
            this.generarDocumento.Name = "generarDocumento";
            this.generarDocumento.Size = new System.Drawing.Size(153, 30);
            this.generarDocumento.TabIndex = 38;
            this.generarDocumento.Text = "Generar Documento";
            this.generarDocumento.UseVisualStyleBackColor = false;
            this.generarDocumento.Click += new System.EventHandler(this.generarDocumento_Click2);
            // 
            // boton8
            // 
            this.boton8.BackColor = System.Drawing.Color.Blue;
            this.boton8.ForeColor = System.Drawing.SystemColors.Window;
            this.boton8.Location = new System.Drawing.Point(409, 352);
            this.boton8.Name = "boton8";
            this.boton8.Size = new System.Drawing.Size(400, 30);
            this.boton8.TabIndex = 39;
            this.boton8.Text = "8. Inspección de carga";
            this.boton8.UseVisualStyleBackColor = false;
            this.boton8.Visible = false;
            this.boton8.Click += new System.EventHandler(this.boton8_Click);
            // 
            // Finalizada
            // 
            this.Finalizada.AutoSize = true;
            this.Finalizada.Location = new System.Drawing.Point(547, 215);
            this.Finalizada.Name = "Finalizada";
            this.Finalizada.Size = new System.Drawing.Size(89, 22);
            this.Finalizada.TabIndex = 40;
            this.Finalizada.Text = "Finalizada";
            this.Finalizada.UseVisualStyleBackColor = true;
            this.Finalizada.Visible = false;
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(815, 509);
            this.Controls.Add(this.Finalizada);
            this.Controls.Add(this.boton8);
            this.Controls.Add(this.generarDocumento);
            this.Controls.Add(this.eliminarInspeccion);
            this.Controls.Add(this.boton7);
            this.Controls.Add(this.boton6);
            this.Controls.Add(this.boton4);
            this.Controls.Add(this.boton5);
            this.Controls.Add(this.boton3);
            this.Controls.Add(this.boton2);
            this.Controls.Add(this.boton1);
            this.Controls.Add(this.textBoxLugar);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBoxInspector);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBoxTorre);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxPackingList);
            this.Controls.Add(this.textBoxProyectos);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxCertificado);
            this.Controls.Add(this.textBoxPlanCalidad);
            this.Controls.Add(this.textBoxContenedor);
            this.Controls.Add(this.textBoxPedido);
            this.Controls.Add(this.textBoxCliente);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.modificarInspecciones);
            this.Controls.Add(this.crearInspeccion);
            this.Controls.Add(this.comboBoxInsPintura);
            this.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registro de inspecciones de pintura";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxInsPintura;
        private System.Windows.Forms.Button crearInspeccion;
        private System.Windows.Forms.Button modificarInspecciones;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxCliente;
        private System.Windows.Forms.TextBox textBoxPedido;
        private System.Windows.Forms.TextBox textBoxContenedor;
        private System.Windows.Forms.TextBox textBoxPlanCalidad;
        private System.Windows.Forms.TextBox textBoxCertificado;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxProyectos;
        private System.Windows.Forms.TextBox textBoxPackingList;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxTorre;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxInspector;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxLugar;
        private System.Windows.Forms.Button boton1;
        private System.Windows.Forms.Button boton2;
        private System.Windows.Forms.Button boton3;
        private System.Windows.Forms.Button boton5;
        private System.Windows.Forms.Button boton4;
        private System.Windows.Forms.Button boton6;
        private System.Windows.Forms.Button boton7;
        private System.Windows.Forms.Button eliminarInspeccion;
        private System.Windows.Forms.Button generarDocumento;
        private System.Windows.Forms.Button boton8;
        private System.Windows.Forms.CheckBox Finalizada;
    }
}

