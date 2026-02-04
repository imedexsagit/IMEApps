namespace LocalizacionPaquetes
{
    partial class LocalizacionPaquetes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocalizacionPaquetes));
            this.comboAlmacen = new System.Windows.Forms.ComboBox();
            this.comboNave = new System.Windows.Forms.ComboBox();
            this.comboFila = new System.Windows.Forms.ComboBox();
            this.comboCol = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textoParcela = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.usuario = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnCerrarPanel = new System.Windows.Forms.Button();
            this.panelHistorial = new System.Windows.Forms.Panel();
            this.parcelaPanel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnBuscarHistorial = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelPopup = new System.Windows.Forms.Panel();
            this.etiquetaPopup = new System.Windows.Forms.Label();
            this.btnPopup = new System.Windows.Forms.Button();
            this.panelCorrecta = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panelHistorial.SuspendLayout();
            this.panelPopup.SuspendLayout();
            this.panelCorrecta.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboAlmacen
            // 
            this.comboAlmacen.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboAlmacen.DropDownHeight = 200;
            this.comboAlmacen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAlmacen.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboAlmacen.FormattingEnabled = true;
            this.comboAlmacen.IntegralHeight = false;
            this.comboAlmacen.Location = new System.Drawing.Point(12, 99);
            this.comboAlmacen.Name = "comboAlmacen";
            this.comboAlmacen.Size = new System.Drawing.Size(1051, 69);
            this.comboAlmacen.TabIndex = 0;
            this.comboAlmacen.SelectedIndexChanged += new System.EventHandler(this.comboAlmacen_SelectedIndexChanged);
            // 
            // comboNave
            // 
            this.comboNave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboNave.DropDownHeight = 200;
            this.comboNave.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboNave.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboNave.FormattingEnabled = true;
            this.comboNave.IntegralHeight = false;
            this.comboNave.Location = new System.Drawing.Point(12, 237);
            this.comboNave.Name = "comboNave";
            this.comboNave.Size = new System.Drawing.Size(250, 69);
            this.comboNave.TabIndex = 1;
            this.comboNave.SelectedIndexChanged += new System.EventHandler(this.comboNave_SelectedIndexChanged);
            // 
            // comboFila
            // 
            this.comboFila.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboFila.DropDownHeight = 200;
            this.comboFila.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboFila.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboFila.FormattingEnabled = true;
            this.comboFila.IntegralHeight = false;
            this.comboFila.Location = new System.Drawing.Point(289, 237);
            this.comboFila.Name = "comboFila";
            this.comboFila.Size = new System.Drawing.Size(250, 69);
            this.comboFila.TabIndex = 2;
            this.comboFila.SelectedIndexChanged += new System.EventHandler(this.comboFila_SelectedIndexChanged);
            // 
            // comboCol
            // 
            this.comboCol.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboCol.DropDownHeight = 200;
            this.comboCol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboCol.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboCol.FormattingEnabled = true;
            this.comboCol.IntegralHeight = false;
            this.comboCol.Location = new System.Drawing.Point(565, 237);
            this.comboCol.Name = "comboCol";
            this.comboCol.Size = new System.Drawing.Size(250, 69);
            this.comboCol.TabIndex = 3;
            this.comboCol.SelectedIndexChanged += new System.EventHandler(this.comboCol_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 39);
            this.label1.TabIndex = 4;
            this.label1.Text = "ALMACÉN";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 195);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 39);
            this.label2.TabIndex = 5;
            this.label2.Text = "NAVE";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(282, 195);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(181, 39);
            this.label3.TabIndex = 6;
            this.label3.Text = "SECCIÓN";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(558, 195);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(175, 39);
            this.label4.TabIndex = 7;
            this.label4.Text = "NÚMERO";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(13, 370);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(148, 31);
            this.label5.TabIndex = 8;
            this.label5.Text = "PARCELA";
            // 
            // textoParcela
            // 
            this.textoParcela.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textoParcela.AutoSize = true;
            this.textoParcela.BackColor = System.Drawing.Color.Transparent;
            this.textoParcela.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textoParcela.ForeColor = System.Drawing.Color.Red;
            this.textoParcela.Location = new System.Drawing.Point(32, 431);
            this.textoParcela.Name = "textoParcela";
            this.textoParcela.Size = new System.Drawing.Size(141, 63);
            this.textoParcela.TabIndex = 9;
            this.textoParcela.Text = "ABC";
            // 
            // textBox2
            // 
            this.textBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(289, 428);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(439, 68);
            this.textBox2.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(282, 386);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(188, 39);
            this.label6.TabIndex = 13;
            this.label6.Text = "PAQUETE";
            // 
            // usuario
            // 
            this.usuario.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.usuario.AutoSize = true;
            this.usuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usuario.Location = new System.Drawing.Point(3, 580);
            this.usuario.Name = "usuario";
            this.usuario.Size = new System.Drawing.Size(14, 17);
            this.usuario.TabIndex = 15;
            this.usuario.Text = "-";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnGuardar.BackColor = System.Drawing.Color.Lime;
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Location = new System.Drawing.Point(823, 404);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(240, 124);
            this.btnGuardar.TabIndex = 16;
            this.btnGuardar.Text = "GUARDAR";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(22, 125);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 35;
            this.dataGridView1.Size = new System.Drawing.Size(847, 391);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnCerrarPanel
            // 
            this.btnCerrarPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCerrarPanel.BackColor = System.Drawing.Color.Red;
            this.btnCerrarPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrarPanel.ForeColor = System.Drawing.Color.White;
            this.btnCerrarPanel.Location = new System.Drawing.Point(721, 12);
            this.btnCerrarPanel.Name = "btnCerrarPanel";
            this.btnCerrarPanel.Size = new System.Drawing.Size(160, 53);
            this.btnCerrarPanel.TabIndex = 1;
            this.btnCerrarPanel.Text = "CERRAR";
            this.btnCerrarPanel.UseVisualStyleBackColor = false;
            this.btnCerrarPanel.Click += new System.EventHandler(this.btnCerrarPanel_Click);
            // 
            // panelHistorial
            // 
            this.panelHistorial.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelHistorial.BackColor = System.Drawing.Color.Silver;
            this.panelHistorial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHistorial.Controls.Add(this.parcelaPanel);
            this.panelHistorial.Controls.Add(this.panel1);
            this.panelHistorial.Controls.Add(this.button1);
            this.panelHistorial.Controls.Add(this.btnBuscarHistorial);
            this.panelHistorial.Controls.Add(this.textBox1);
            this.panelHistorial.Controls.Add(this.label9);
            this.panelHistorial.Controls.Add(this.label7);
            this.panelHistorial.Controls.Add(this.btnCerrarPanel);
            this.panelHistorial.Controls.Add(this.dataGridView1);
            this.panelHistorial.Location = new System.Drawing.Point(96, 33);
            this.panelHistorial.Name = "panelHistorial";
            this.panelHistorial.Size = new System.Drawing.Size(896, 548);
            this.panelHistorial.TabIndex = 17;
            // 
            // parcelaPanel
            // 
            this.parcelaPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.parcelaPanel.AutoSize = true;
            this.parcelaPanel.BackColor = System.Drawing.Color.Transparent;
            this.parcelaPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.parcelaPanel.ForeColor = System.Drawing.Color.Red;
            this.parcelaPanel.Location = new System.Drawing.Point(306, 80);
            this.parcelaPanel.Name = "parcelaPanel";
            this.parcelaPanel.Size = new System.Drawing.Size(60, 31);
            this.parcelaPanel.TabIndex = 18;
            this.parcelaPanel.Text = "- - -";
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.BackColor = System.Drawing.Color.Red;
            this.panel1.Location = new System.Drawing.Point(292, 71);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(91, 48);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(564, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 53);
            this.button1.TabIndex = 23;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnBuscarHistorial
            // 
            this.btnBuscarHistorial.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnBuscarHistorial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnBuscarHistorial.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarHistorial.ForeColor = System.Drawing.Color.White;
            this.btnBuscarHistorial.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarHistorial.Image")));
            this.btnBuscarHistorial.Location = new System.Drawing.Point(443, 12);
            this.btnBuscarHistorial.Name = "btnBuscarHistorial";
            this.btnBuscarHistorial.Size = new System.Drawing.Size(115, 53);
            this.btnBuscarHistorial.TabIndex = 22;
            this.btnBuscarHistorial.UseVisualStyleBackColor = false;
            this.btnBuscarHistorial.Click += new System.EventHandler(this.btnBuscarHistorial_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(173, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(264, 53);
            this.textBox1.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(16, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(151, 31);
            this.label9.TabIndex = 20;
            this.label9.Text = "PAQUETE";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(16, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(270, 31);
            this.label7.TabIndex = 9;
            this.label7.Text = "PARCELA ACTUAL";
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(289, 502);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(312, 92);
            this.button2.TabIndex = 19;
            this.button2.Text = "HISTORIAL";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnLimpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.Image = ((System.Drawing.Image)(resources.GetObject("btnLimpiar.Image")));
            this.btnLimpiar.Location = new System.Drawing.Point(734, 428);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(70, 68);
            this.btnLimpiar.TabIndex = 20;
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel2.BackColor = System.Drawing.Color.Red;
            this.panel2.Location = new System.Drawing.Point(19, 417);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(183, 93);
            this.panel2.TabIndex = 1;
            // 
            // panelPopup
            // 
            this.panelPopup.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelPopup.BackColor = System.Drawing.Color.Silver;
            this.panelPopup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPopup.Controls.Add(this.etiquetaPopup);
            this.panelPopup.Controls.Add(this.btnPopup);
            this.panelPopup.Location = new System.Drawing.Point(270, 148);
            this.panelPopup.Name = "panelPopup";
            this.panelPopup.Size = new System.Drawing.Size(534, 311);
            this.panelPopup.TabIndex = 22;
            // 
            // etiquetaPopup
            // 
            this.etiquetaPopup.AutoSize = true;
            this.etiquetaPopup.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.etiquetaPopup.ForeColor = System.Drawing.Color.Black;
            this.etiquetaPopup.Location = new System.Drawing.Point(24, 42);
            this.etiquetaPopup.Name = "etiquetaPopup";
            this.etiquetaPopup.Size = new System.Drawing.Size(114, 31);
            this.etiquetaPopup.TabIndex = 25;
            this.etiquetaPopup.Text = "POPUP";
            // 
            // btnPopup
            // 
            this.btnPopup.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPopup.BackColor = System.Drawing.Color.Red;
            this.btnPopup.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPopup.ForeColor = System.Drawing.Color.White;
            this.btnPopup.Location = new System.Drawing.Point(153, 206);
            this.btnPopup.Name = "btnPopup";
            this.btnPopup.Size = new System.Drawing.Size(214, 80);
            this.btnPopup.TabIndex = 24;
            this.btnPopup.Text = "CERRAR";
            this.btnPopup.UseVisualStyleBackColor = false;
            this.btnPopup.Click += new System.EventHandler(this.btnPopup_Click);
            // 
            // panelCorrecta
            // 
            this.panelCorrecta.BackColor = System.Drawing.Color.Green;
            this.panelCorrecta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCorrecta.Controls.Add(this.label8);
            this.panelCorrecta.Location = new System.Drawing.Point(216, 226);
            this.panelCorrecta.Name = "panelCorrecta";
            this.panelCorrecta.Size = new System.Drawing.Size(643, 154);
            this.panelCorrecta.TabIndex = 27;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(22, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(596, 63);
            this.label8.TabIndex = 0;
            this.label8.Text = "LECTURA CORRECTA";
            // 
            // LocalizacionPaquetes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 606);
            this.Controls.Add(this.panelCorrecta);
            this.Controls.Add(this.panelPopup);
            this.Controls.Add(this.panelHistorial);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.usuario);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboCol);
            this.Controls.Add(this.comboFila);
            this.Controls.Add(this.comboNave);
            this.Controls.Add(this.comboAlmacen);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.textoParcela);
            this.Controls.Add(this.panel2);
            this.Name = "LocalizacionPaquetes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LOCALIZACIÓN DE PAQUETES";
            this.Load += new System.EventHandler(this.LocalizacionPaquetes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panelHistorial.ResumeLayout(false);
            this.panelHistorial.PerformLayout();
            this.panelPopup.ResumeLayout(false);
            this.panelPopup.PerformLayout();
            this.panelCorrecta.ResumeLayout(false);
            this.panelCorrecta.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboAlmacen;
        private System.Windows.Forms.ComboBox comboNave;
        private System.Windows.Forms.ComboBox comboFila;
        private System.Windows.Forms.ComboBox comboCol;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label textoParcela;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label usuario;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnCerrarPanel;
        private System.Windows.Forms.Panel panelHistorial;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label parcelaPanel;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnBuscarHistorial;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelPopup;
        private System.Windows.Forms.Button btnPopup;
        private System.Windows.Forms.Label etiquetaPopup;
        private System.Windows.Forms.Panel panelCorrecta;
        private System.Windows.Forms.Label label8;
    }
}

