namespace PaquetesReprocesado
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            this.checkNegro = new System.Windows.Forms.CheckBox();
            this.checkBlanco = new System.Windows.Forms.CheckBox();
            this.labelTitulo = new System.Windows.Forms.Label();
            this.dgvPaquetes = new System.Windows.Forms.DataGridView();
            this.tbFiltro = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnPaqNuevo = new System.Windows.Forms.Button();
            this.btnEliminarPaquete = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnConfirmarPaquete = new System.Windows.Forms.Button();
            this.panelCantidad = new System.Windows.Forms.Panel();
            this.btnAñad = new System.Windows.Forms.Button();
            this.labelMarcaPanel = new System.Windows.Forms.Label();
            this.nupCantidad = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.btnCerrarCantidad = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.cbSede = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.labelId = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnBuscarGalva = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btnAddMarca = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvContenido = new System.Windows.Forms.DataGridView();
            this.dgvBusqueda = new System.Windows.Forms.DataGridView();
            this.radioBlanco = new System.Windows.Forms.RadioButton();
            this.radioNegro = new System.Windows.Forms.RadioButton();
            this.cbExpeds = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.cbFiltroSede = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tbCamion = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaquetes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panelCantidad.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContenido)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBusqueda)).BeginInit();
            this.SuspendLayout();
            // 
            // checkNegro
            // 
            this.checkNegro.AutoSize = true;
            this.checkNegro.Checked = true;
            this.checkNegro.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkNegro.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkNegro.Location = new System.Drawing.Point(355, 75);
            this.checkNegro.Name = "checkNegro";
            this.checkNegro.Size = new System.Drawing.Size(107, 29);
            this.checkNegro.TabIndex = 0;
            this.checkNegro.Text = "NEGRO";
            this.checkNegro.UseVisualStyleBackColor = true;
            this.checkNegro.CheckedChanged += new System.EventHandler(this.checkNegro_CheckedChanged);
            // 
            // checkBlanco
            // 
            this.checkBlanco.AutoSize = true;
            this.checkBlanco.Checked = true;
            this.checkBlanco.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBlanco.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBlanco.Location = new System.Drawing.Point(355, 110);
            this.checkBlanco.Name = "checkBlanco";
            this.checkBlanco.Size = new System.Drawing.Size(120, 29);
            this.checkBlanco.TabIndex = 1;
            this.checkBlanco.Text = "BLANCO";
            this.checkBlanco.UseVisualStyleBackColor = true;
            this.checkBlanco.CheckedChanged += new System.EventHandler(this.checkBlanco_CheckedChanged);
            // 
            // labelTitulo
            // 
            this.labelTitulo.AutoSize = true;
            this.labelTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitulo.Location = new System.Drawing.Point(12, 9);
            this.labelTitulo.Name = "labelTitulo";
            this.labelTitulo.Size = new System.Drawing.Size(521, 31);
            this.labelTitulo.TabIndex = 3;
            this.labelTitulo.Text = "TRAZABILIDAD DE REPROCESADOS";
            // 
            // dgvPaquetes
            // 
            this.dgvPaquetes.AllowUserToAddRows = false;
            this.dgvPaquetes.AllowUserToDeleteRows = false;
            this.dgvPaquetes.AllowUserToOrderColumns = true;
            this.dgvPaquetes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvPaquetes.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPaquetes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle19;
            this.dgvPaquetes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPaquetes.DefaultCellStyle = dataGridViewCellStyle20;
            this.dgvPaquetes.Location = new System.Drawing.Point(18, 145);
            this.dgvPaquetes.MultiSelect = false;
            this.dgvPaquetes.Name = "dgvPaquetes";
            this.dgvPaquetes.ReadOnly = true;
            this.dgvPaquetes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPaquetes.Size = new System.Drawing.Size(1314, 422);
            this.dgvPaquetes.TabIndex = 12;
            this.dgvPaquetes.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPaquetes_CellContentDoubleClick);
            this.dgvPaquetes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPaquetes_CellDoubleClick);
            // 
            // tbFiltro
            // 
            this.tbFiltro.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbFiltro.Location = new System.Drawing.Point(56, 95);
            this.tbFiltro.Name = "tbFiltro";
            this.tbFiltro.Size = new System.Drawing.Size(264, 38);
            this.tbFiltro.TabIndex = 13;
            this.tbFiltro.TextChanged += new System.EventHandler(this.tbFiltro_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(18, 99);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 31);
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // btnPaqNuevo
            // 
            this.btnPaqNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnPaqNuevo.Image")));
            this.btnPaqNuevo.Location = new System.Drawing.Point(1169, 23);
            this.btnPaqNuevo.Name = "btnPaqNuevo";
            this.btnPaqNuevo.Size = new System.Drawing.Size(55, 55);
            this.btnPaqNuevo.TabIndex = 15;
            this.btnPaqNuevo.UseVisualStyleBackColor = true;
            this.btnPaqNuevo.Click += new System.EventHandler(this.btnPaqNuevo_Click);
            // 
            // btnEliminarPaquete
            // 
            this.btnEliminarPaquete.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminarPaquete.Image")));
            this.btnEliminarPaquete.Location = new System.Drawing.Point(1258, 23);
            this.btnEliminarPaquete.Name = "btnEliminarPaquete";
            this.btnEliminarPaquete.Size = new System.Drawing.Size(55, 55);
            this.btnEliminarPaquete.TabIndex = 16;
            this.btnEliminarPaquete.UseVisualStyleBackColor = true;
            this.btnEliminarPaquete.Click += new System.EventHandler(this.btnEliminarPaquete_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1165, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 17);
            this.label1.TabIndex = 17;
            this.label1.Text = "NUEVO";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1244, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 17);
            this.label2.TabIndex = 18;
            this.label2.Text = "ELIMINAR";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.tbCamion);
            this.panel1.Controls.Add(this.btnConfirmarPaquete);
            this.panel1.Controls.Add(this.panelCantidad);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.btnEliminar);
            this.panel1.Controls.Add(this.cbSede);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.labelId);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btnBuscarGalva);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.btnAddMarca);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.dgvContenido);
            this.panel1.Controls.Add(this.dgvBusqueda);
            this.panel1.Controls.Add(this.radioBlanco);
            this.panel1.Controls.Add(this.radioNegro);
            this.panel1.Controls.Add(this.cbExpeds);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(18, 69);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1314, 614);
            this.panel1.TabIndex = 19;
            this.panel1.Visible = false;
            // 
            // btnConfirmarPaquete
            // 
            this.btnConfirmarPaquete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnConfirmarPaquete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnConfirmarPaquete.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmarPaquete.Location = new System.Drawing.Point(1130, 516);
            this.btnConfirmarPaquete.Name = "btnConfirmarPaquete";
            this.btnConfirmarPaquete.Size = new System.Drawing.Size(181, 90);
            this.btnConfirmarPaquete.TabIndex = 25;
            this.btnConfirmarPaquete.Text = "CONFIRMAR PAQUETE";
            this.btnConfirmarPaquete.UseVisualStyleBackColor = false;
            this.btnConfirmarPaquete.Click += new System.EventHandler(this.btnConfirmarPaquete_Click);
            // 
            // panelCantidad
            // 
            this.panelCantidad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCantidad.Controls.Add(this.btnAñad);
            this.panelCantidad.Controls.Add(this.labelMarcaPanel);
            this.panelCantidad.Controls.Add(this.nupCantidad);
            this.panelCantidad.Controls.Add(this.label9);
            this.panelCantidad.Controls.Add(this.btnCerrarCantidad);
            this.panelCantidad.Location = new System.Drawing.Point(917, 222);
            this.panelCantidad.Name = "panelCantidad";
            this.panelCantidad.Size = new System.Drawing.Size(269, 210);
            this.panelCantidad.TabIndex = 24;
            this.panelCantidad.Visible = false;
            // 
            // btnAñad
            // 
            this.btnAñad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnAñad.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAñad.ForeColor = System.Drawing.Color.Black;
            this.btnAñad.Location = new System.Drawing.Point(8, 151);
            this.btnAñad.Name = "btnAñad";
            this.btnAñad.Size = new System.Drawing.Size(141, 50);
            this.btnAñad.TabIndex = 26;
            this.btnAñad.Text = "AÑADIR";
            this.btnAñad.UseVisualStyleBackColor = false;
            this.btnAñad.Click += new System.EventHandler(this.btnAñad_Click);
            // 
            // labelMarcaPanel
            // 
            this.labelMarcaPanel.AutoSize = true;
            this.labelMarcaPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMarcaPanel.ForeColor = System.Drawing.Color.Blue;
            this.labelMarcaPanel.Location = new System.Drawing.Point(3, 55);
            this.labelMarcaPanel.Name = "labelMarcaPanel";
            this.labelMarcaPanel.Size = new System.Drawing.Size(123, 25);
            this.labelMarcaPanel.TabIndex = 25;
            this.labelMarcaPanel.Text = "CANTIDAD";
            // 
            // nupCantidad
            // 
            this.nupCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nupCantidad.Location = new System.Drawing.Point(8, 83);
            this.nupCantidad.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nupCantidad.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupCantidad.Name = "nupCantidad";
            this.nupCantidad.Size = new System.Drawing.Size(120, 38);
            this.nupCantidad.TabIndex = 24;
            this.nupCantidad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(3, 8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(123, 25);
            this.label9.TabIndex = 23;
            this.label9.Text = "CANTIDAD";
            // 
            // btnCerrarCantidad
            // 
            this.btnCerrarCantidad.BackColor = System.Drawing.Color.Red;
            this.btnCerrarCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrarCantidad.ForeColor = System.Drawing.Color.White;
            this.btnCerrarCantidad.Location = new System.Drawing.Point(208, 8);
            this.btnCerrarCantidad.Name = "btnCerrarCantidad";
            this.btnCerrarCantidad.Size = new System.Drawing.Size(50, 50);
            this.btnCerrarCantidad.TabIndex = 21;
            this.btnCerrarCantidad.Text = "X";
            this.btnCerrarCantidad.UseVisualStyleBackColor = false;
            this.btnCerrarCantidad.Click += new System.EventHandler(this.btnCerrarCantidad_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(760, 589);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 17);
            this.label8.TabIndex = 23;
            this.label8.Text = "ELIMINAR";
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnEliminar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEliminar.BackgroundImage")));
            this.btnEliminar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnEliminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminar.Location = new System.Drawing.Point(762, 516);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(74, 74);
            this.btnEliminar.TabIndex = 22;
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // cbSede
            // 
            this.cbSede.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbSede.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbSede.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSede.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSede.FormattingEnabled = true;
            this.cbSede.Items.AddRange(new object[] {
            "CASAR DE CÁCERES",
            "SANTIAGO DEL CAMPO",
            "MEDINA DEL CAMPO"});
            this.cbSede.Location = new System.Drawing.Point(1046, 104);
            this.cbSede.Name = "cbSede";
            this.cbSede.Size = new System.Drawing.Size(259, 33);
            this.cbSede.TabIndex = 20;
            this.cbSede.SelectedIndexChanged += new System.EventHandler(this.cbSede_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(1041, 81);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 25);
            this.label7.TabIndex = 19;
            this.label7.Text = "SEDE";
            // 
            // labelId
            // 
            this.labelId.AutoSize = true;
            this.labelId.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelId.ForeColor = System.Drawing.Color.Blue;
            this.labelId.Location = new System.Drawing.Point(911, 4);
            this.labelId.Name = "labelId";
            this.labelId.Size = new System.Drawing.Size(23, 31);
            this.labelId.TabIndex = 17;
            this.labelId.Text = "-";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(756, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(151, 31);
            this.label6.TabIndex = 16;
            this.label6.Text = "PAQUETE";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(224, 25);
            this.label5.TabIndex = 15;
            this.label5.Text = "PAQ. GALVANIZADO";
            // 
            // btnBuscarGalva
            // 
            this.btnBuscarGalva.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnBuscarGalva.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarGalva.Image")));
            this.btnBuscarGalva.Location = new System.Drawing.Point(470, 108);
            this.btnBuscarGalva.Name = "btnBuscarGalva";
            this.btnBuscarGalva.Size = new System.Drawing.Size(108, 42);
            this.btnBuscarGalva.TabIndex = 14;
            this.btnBuscarGalva.UseVisualStyleBackColor = false;
            this.btnBuscarGalva.Click += new System.EventHandler(this.btnBuscarGalva_Click);
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(3, 38);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(261, 30);
            this.textBox2.TabIndex = 13;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            this.textBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyDown);
            // 
            // btnAddMarca
            // 
            this.btnAddMarca.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddMarca.BackgroundImage")));
            this.btnAddMarca.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddMarca.Location = new System.Drawing.Point(615, 208);
            this.btnAddMarca.Name = "btnAddMarca";
            this.btnAddMarca.Size = new System.Drawing.Size(113, 248);
            this.btnAddMarca.TabIndex = 12;
            this.btnAddMarca.UseVisualStyleBackColor = true;
            this.btnAddMarca.Click += new System.EventHandler(this.btnAddMarca_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(757, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 25);
            this.label4.TabIndex = 11;
            this.label4.Text = "CONTENIDO";
            // 
            // dgvContenido
            // 
            this.dgvContenido.AllowUserToAddRows = false;
            this.dgvContenido.AllowUserToDeleteRows = false;
            this.dgvContenido.AllowUserToOrderColumns = true;
            this.dgvContenido.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvContenido.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle21.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvContenido.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle21;
            this.dgvContenido.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvContenido.DefaultCellStyle = dataGridViewCellStyle22;
            this.dgvContenido.Location = new System.Drawing.Point(762, 152);
            this.dgvContenido.Name = "dgvContenido";
            this.dgvContenido.ReadOnly = true;
            this.dgvContenido.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvContenido.Size = new System.Drawing.Size(549, 358);
            this.dgvContenido.TabIndex = 10;
            // 
            // dgvBusqueda
            // 
            this.dgvBusqueda.AllowUserToAddRows = false;
            this.dgvBusqueda.AllowUserToDeleteRows = false;
            this.dgvBusqueda.AllowUserToOrderColumns = true;
            this.dgvBusqueda.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvBusqueda.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle23.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle23.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle23.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBusqueda.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle23;
            this.dgvBusqueda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle24.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle24.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle24.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle24.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle24.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBusqueda.DefaultCellStyle = dataGridViewCellStyle24;
            this.dgvBusqueda.Location = new System.Drawing.Point(3, 152);
            this.dgvBusqueda.Name = "dgvBusqueda";
            this.dgvBusqueda.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBusqueda.Size = new System.Drawing.Size(575, 358);
            this.dgvBusqueda.TabIndex = 9;
            // 
            // radioBlanco
            // 
            this.radioBlanco.AutoSize = true;
            this.radioBlanco.Checked = true;
            this.radioBlanco.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioBlanco.Location = new System.Drawing.Point(762, 44);
            this.radioBlanco.Name = "radioBlanco";
            this.radioBlanco.Size = new System.Drawing.Size(113, 29);
            this.radioBlanco.TabIndex = 8;
            this.radioBlanco.TabStop = true;
            this.radioBlanco.Text = "BLANCO";
            this.radioBlanco.UseVisualStyleBackColor = true;
            this.radioBlanco.CheckedChanged += new System.EventHandler(this.radioBlanco_CheckedChanged);
            // 
            // radioNegro
            // 
            this.radioNegro.AutoSize = true;
            this.radioNegro.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioNegro.Location = new System.Drawing.Point(762, 79);
            this.radioNegro.Name = "radioNegro";
            this.radioNegro.Size = new System.Drawing.Size(101, 29);
            this.radioNegro.TabIndex = 7;
            this.radioNegro.Text = "NEGRO";
            this.radioNegro.UseVisualStyleBackColor = true;
            // 
            // cbExpeds
            // 
            this.cbExpeds.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbExpeds.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbExpeds.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbExpeds.FormattingEnabled = true;
            this.cbExpeds.Location = new System.Drawing.Point(1046, 35);
            this.cbExpeds.Name = "cbExpeds";
            this.cbExpeds.Size = new System.Drawing.Size(259, 33);
            this.cbExpeds.TabIndex = 5;
            this.cbExpeds.SelectedIndexChanged += new System.EventHandler(this.cbExpeds_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1041, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(264, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "EXPEDICIÓN ASOCIADA";
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.Red;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(1148, 3);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(184, 60);
            this.btnCancelar.TabIndex = 20;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Visible = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // cbFiltroSede
            // 
            this.cbFiltroSede.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbFiltroSede.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbFiltroSede.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFiltroSede.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFiltroSede.FormattingEnabled = true;
            this.cbFiltroSede.Items.AddRange(new object[] {
            "TODAS",
            "CASAR DE CÁCERES",
            "SANTIAGO DEL CAMPO",
            "MEDINA DEL CAMPO"});
            this.cbFiltroSede.Location = new System.Drawing.Point(545, 95);
            this.cbFiltroSede.Name = "cbFiltroSede";
            this.cbFiltroSede.Size = new System.Drawing.Size(259, 33);
            this.cbFiltroSede.TabIndex = 22;
            this.cbFiltroSede.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(540, 72);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 25);
            this.label10.TabIndex = 21;
            this.label10.Text = "SEDE";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(3, 76);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(99, 25);
            this.label11.TabIndex = 27;
            this.label11.Text = "CAMIÓN";
            // 
            // tbCamion
            // 
            this.tbCamion.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCamion.Location = new System.Drawing.Point(3, 104);
            this.tbCamion.Name = "tbCamion";
            this.tbCamion.Size = new System.Drawing.Size(261, 30);
            this.tbCamion.TabIndex = 26;
            this.tbCamion.TextChanged += new System.EventHandler(this.tbCamion_TextChanged);
            this.tbCamion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbCamion_KeyDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 695);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnEliminarPaquete);
            this.Controls.Add(this.btnPaqNuevo);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.tbFiltro);
            this.Controls.Add(this.dgvPaquetes);
            this.Controls.Add(this.labelTitulo);
            this.Controls.Add(this.checkBlanco);
            this.Controls.Add(this.checkNegro);
            this.Controls.Add(this.cbFiltroSede);
            this.Controls.Add(this.label10);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TRAZABILIDAD REPROCESADOS";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaquetes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelCantidad.ResumeLayout(false);
            this.panelCantidad.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupCantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContenido)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBusqueda)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkNegro;
        private System.Windows.Forms.CheckBox checkBlanco;
        private System.Windows.Forms.Label labelTitulo;
        private System.Windows.Forms.DataGridView dgvPaquetes;
        private System.Windows.Forms.TextBox tbFiltro;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnPaqNuevo;
        private System.Windows.Forms.Button btnEliminarPaquete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnBuscarGalva;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btnAddMarca;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvContenido;
        private System.Windows.Forms.DataGridView dgvBusqueda;
        private System.Windows.Forms.RadioButton radioBlanco;
        private System.Windows.Forms.RadioButton radioNegro;
        private System.Windows.Forms.ComboBox cbExpeds;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ComboBox cbSede;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Panel panelCantidad;
        private System.Windows.Forms.Button btnAñad;
        private System.Windows.Forms.Label labelMarcaPanel;
        private System.Windows.Forms.NumericUpDown nupCantidad;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnCerrarCantidad;
        private System.Windows.Forms.ComboBox cbFiltroSede;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnConfirmarPaquete;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbCamion;
    }
}

