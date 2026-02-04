namespace LanzamientoProyectos
{
    partial class LanzaProyectosForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LanzaProyectosForm));
            this.cbPedido = new System.Windows.Forms.ComboBox();
            this.lbPedido = new System.Windows.Forms.Label();
            this.btnObtenerPedidos = new System.Windows.Forms.Button();
            this.tbCodigoProyecto = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbProyecto = new System.Windows.Forms.Label();
            this.cbProyecto = new System.Windows.Forms.ComboBox();
            this.btnNuevoProyecto = new System.Windows.Forms.Button();
            this.btnGuardarProyecto = new System.Windows.Forms.Button();
            this.btnCalcularNecesidades = new System.Windows.Forms.Button();
            this.btnValidar = new System.Windows.Forms.Button();
            this.dgvLineasProyecto = new System.Windows.Forms.DataGridView();
            this.tcProyecto = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.nUDCantidadLinea = new System.Windows.Forms.NumericUpDown();
            this.cbCantidad = new System.Windows.Forms.GroupBox();
            this.nudCantidad = new System.Windows.Forms.NumericUpDown();
            this.gbCodigo = new System.Windows.Forms.GroupBox();
            this.tbCodigoManual = new System.Windows.Forms.TextBox();
            this.btnAddCodigoManual = new System.Windows.Forms.Button();
            this.tgvArbolPedidos = new AdvancedDataGridView.TreeGridView();
            this.Nodo = new AdvancedDataGridView.TreeGridColumn();
            this.Desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumPedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LineaPedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Necesarias = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lanzadas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Proyecto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoReg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LblCantidad = new System.Windows.Forms.Label();
            this.btnAddLineaProyecto = new System.Windows.Forms.Button();
            this.btnRemoveLineaProyecto = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnStop = new System.Windows.Forms.Button();
            this.dgvNecesidadesProyecto = new System.Windows.Forms.DataGridView();
            this.btnBorrarLanzamiento = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbFiltros = new System.Windows.Forms.GroupBox();
            this.btnNuevoProyectoExpedicion = new System.Windows.Forms.Button();
            this.checkBox_desglose_lineas = new System.Windows.Forms.CheckBox();
            this.lblValidado = new System.Windows.Forms.Label();
            this.lbTipoPedido = new System.Windows.Forms.Label();
            this.cbTipoPedido = new System.Windows.Forms.ComboBox();
            this.btnLimpiarFiltros = new System.Windows.Forms.Button();
            this.btnBuscarProyectos = new System.Windows.Forms.Button();
            this.dtpHorizonte = new System.Windows.Forms.DateTimePicker();
            this.lbHorizonte = new System.Windows.Forms.Label();
            this.lbFamilia = new System.Windows.Forms.Label();
            this.cbFamilia = new System.Windows.Forms.ComboBox();
            this.btnUltimo = new System.Windows.Forms.Button();
            this.btnSiguiente = new System.Windows.Forms.Button();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.btnPrimero = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLineasProyecto)).BeginInit();
            this.tcProyecto.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDCantidadLinea)).BeginInit();
            this.cbCantidad.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).BeginInit();
            this.gbCodigo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tgvArbolPedidos)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNecesidadesProyecto)).BeginInit();
            this.gbFiltros.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbPedido
            // 
            this.cbPedido.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbPedido.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbPedido.FormattingEnabled = true;
            this.cbPedido.ItemHeight = 13;
            this.cbPedido.Location = new System.Drawing.Point(195, 25);
            this.cbPedido.Name = "cbPedido";
            this.cbPedido.Size = new System.Drawing.Size(119, 21);
            this.cbPedido.TabIndex = 1;
            // 
            // lbPedido
            // 
            this.lbPedido.AutoSize = true;
            this.lbPedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPedido.Location = new System.Drawing.Point(143, 29);
            this.lbPedido.Name = "lbPedido";
            this.lbPedido.Size = new System.Drawing.Size(43, 13);
            this.lbPedido.TabIndex = 6;
            this.lbPedido.Text = "Pedido:";
            // 
            // btnObtenerPedidos
            // 
            this.btnObtenerPedidos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnObtenerPedidos.Location = new System.Drawing.Point(10, 67);
            this.btnObtenerPedidos.Name = "btnObtenerPedidos";
            this.btnObtenerPedidos.Size = new System.Drawing.Size(99, 23);
            this.btnObtenerPedidos.TabIndex = 8;
            this.btnObtenerPedidos.Text = "Obtener Pedidos";
            this.btnObtenerPedidos.UseVisualStyleBackColor = true;
            this.btnObtenerPedidos.Click += new System.EventHandler(this.btnObtenerArbol_Click);
            // 
            // tbCodigoProyecto
            // 
            this.tbCodigoProyecto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCodigoProyecto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCodigoProyecto.Location = new System.Drawing.Point(872, 24);
            this.tbCodigoProyecto.MaxLength = 20;
            this.tbCodigoProyecto.Name = "tbCodigoProyecto";
            this.tbCodigoProyecto.Size = new System.Drawing.Size(269, 20);
            this.tbCodigoProyecto.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(761, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Código del Proyecto";
            // 
            // lbProyecto
            // 
            this.lbProyecto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbProyecto.AutoSize = true;
            this.lbProyecto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProyecto.Location = new System.Drawing.Point(609, 28);
            this.lbProyecto.Name = "lbProyecto";
            this.lbProyecto.Size = new System.Drawing.Size(52, 13);
            this.lbProyecto.TabIndex = 13;
            this.lbProyecto.Text = "Proyecto:";
            // 
            // cbProyecto
            // 
            this.cbProyecto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbProyecto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbProyecto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbProyecto.FormattingEnabled = true;
            this.cbProyecto.ItemHeight = 13;
            this.cbProyecto.Location = new System.Drawing.Point(667, 24);
            this.cbProyecto.Name = "cbProyecto";
            this.cbProyecto.Size = new System.Drawing.Size(82, 21);
            this.cbProyecto.TabIndex = 5;
            // 
            // btnNuevoProyecto
            // 
            this.btnNuevoProyecto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNuevoProyecto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevoProyecto.Location = new System.Drawing.Point(835, 67);
            this.btnNuevoProyecto.Name = "btnNuevoProyecto";
            this.btnNuevoProyecto.Size = new System.Drawing.Size(99, 23);
            this.btnNuevoProyecto.TabIndex = 10;
            this.btnNuevoProyecto.Text = "Nuevo Proyecto";
            this.btnNuevoProyecto.UseVisualStyleBackColor = true;
            this.btnNuevoProyecto.Click += new System.EventHandler(this.btnNuevoProyecto_Click);
            // 
            // btnGuardarProyecto
            // 
            this.btnGuardarProyecto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardarProyecto.Location = new System.Drawing.Point(948, 67);
            this.btnGuardarProyecto.Name = "btnGuardarProyecto";
            this.btnGuardarProyecto.Size = new System.Drawing.Size(99, 23);
            this.btnGuardarProyecto.TabIndex = 10;
            this.btnGuardarProyecto.Text = "Guardar Proyecto";
            this.btnGuardarProyecto.UseVisualStyleBackColor = true;
            this.btnGuardarProyecto.Click += new System.EventHandler(this.btnGuardarProyecto_Click);
            // 
            // btnCalcularNecesidades
            // 
            this.btnCalcularNecesidades.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCalcularNecesidades.Location = new System.Drawing.Point(16, 485);
            this.btnCalcularNecesidades.Name = "btnCalcularNecesidades";
            this.btnCalcularNecesidades.Size = new System.Drawing.Size(58, 25);
            this.btnCalcularNecesidades.TabIndex = 11;
            this.btnCalcularNecesidades.Text = "Calcular";
            this.btnCalcularNecesidades.UseVisualStyleBackColor = true;
            this.btnCalcularNecesidades.Click += new System.EventHandler(this.btnCalcularNecesidades_Click);
            // 
            // btnValidar
            // 
            this.btnValidar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnValidar.Enabled = false;
            this.btnValidar.Location = new System.Drawing.Point(164, 485);
            this.btnValidar.Name = "btnValidar";
            this.btnValidar.Size = new System.Drawing.Size(58, 25);
            this.btnValidar.TabIndex = 13;
            this.btnValidar.Text = "Validar";
            this.btnValidar.UseVisualStyleBackColor = true;
            this.btnValidar.Click += new System.EventHandler(this.btnLanzar_Click);
            // 
            // dgvLineasProyecto
            // 
            this.dgvLineasProyecto.AllowUserToAddRows = false;
            this.dgvLineasProyecto.AllowUserToDeleteRows = false;
            this.dgvLineasProyecto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLineasProyecto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLineasProyecto.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvLineasProyecto.Location = new System.Drawing.Point(751, 3);
            this.dgvLineasProyecto.Name = "dgvLineasProyecto";
            this.dgvLineasProyecto.Size = new System.Drawing.Size(491, 454);
            this.dgvLineasProyecto.TabIndex = 20;
            // 
            // tcProyecto
            // 
            this.tcProyecto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcProyecto.Controls.Add(this.tabPage1);
            this.tcProyecto.Controls.Add(this.tabPage2);
            this.tcProyecto.Location = new System.Drawing.Point(8, 111);
            this.tcProyecto.Name = "tcProyecto";
            this.tcProyecto.SelectedIndex = 0;
            this.tcProyecto.Size = new System.Drawing.Size(1253, 550);
            this.tcProyecto.TabIndex = 21;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.nUDCantidadLinea);
            this.tabPage1.Controls.Add(this.cbCantidad);
            this.tabPage1.Controls.Add(this.gbCodigo);
            this.tabPage1.Controls.Add(this.dgvLineasProyecto);
            this.tabPage1.Controls.Add(this.btnAddCodigoManual);
            this.tabPage1.Controls.Add(this.tgvArbolPedidos);
            this.tabPage1.Controls.Add(this.LblCantidad);
            this.tabPage1.Controls.Add(this.btnAddLineaProyecto);
            this.tabPage1.Controls.Add(this.btnRemoveLineaProyecto);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1245, 524);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Líneas del Proyecto";
            // 
            // nUDCantidadLinea
            // 
            this.nUDCantidadLinea.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.nUDCantidadLinea.Location = new System.Drawing.Point(695, 131);
            this.nUDCantidadLinea.Name = "nUDCantidadLinea";
            this.nUDCantidadLinea.Size = new System.Drawing.Size(54, 20);
            this.nUDCantidadLinea.TabIndex = 27;
            // 
            // cbCantidad
            // 
            this.cbCantidad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCantidad.Controls.Add(this.nudCantidad);
            this.cbCantidad.Location = new System.Drawing.Point(1031, 463);
            this.cbCantidad.Name = "cbCantidad";
            this.cbCantidad.Size = new System.Drawing.Size(139, 58);
            this.cbCantidad.TabIndex = 5;
            this.cbCantidad.TabStop = false;
            this.cbCantidad.Text = "Cantidad";
            // 
            // nudCantidad
            // 
            this.nudCantidad.Location = new System.Drawing.Point(6, 20);
            this.nudCantidad.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.nudCantidad.Minimum = new decimal(new int[] {
            50000,
            0,
            0,
            -2147483648});
            this.nudCantidad.Name = "nudCantidad";
            this.nudCantidad.Size = new System.Drawing.Size(120, 20);
            this.nudCantidad.TabIndex = 0;
            this.nudCantidad.Enter += new System.EventHandler(this.nudCantidad_Enter);
            this.nudCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nudCantidad_KeyPress);
            // 
            // gbCodigo
            // 
            this.gbCodigo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCodigo.Controls.Add(this.tbCodigoManual);
            this.gbCodigo.Location = new System.Drawing.Point(751, 463);
            this.gbCodigo.Name = "gbCodigo";
            this.gbCodigo.Size = new System.Drawing.Size(256, 58);
            this.gbCodigo.TabIndex = 4;
            this.gbCodigo.TabStop = false;
            this.gbCodigo.Text = "Código";
            // 
            // tbCodigoManual
            // 
            this.tbCodigoManual.Location = new System.Drawing.Point(37, 21);
            this.tbCodigoManual.Name = "tbCodigoManual";
            this.tbCodigoManual.Size = new System.Drawing.Size(178, 20);
            this.tbCodigoManual.TabIndex = 0;
            this.tbCodigoManual.Enter += new System.EventHandler(this.tbCodigoManual_Enter);
            this.tbCodigoManual.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCodigoManual_KeyPress);
            // 
            // btnAddCodigoManual
            // 
            this.btnAddCodigoManual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddCodigoManual.Image = ((System.Drawing.Image)(resources.GetObject("btnAddCodigoManual.Image")));
            this.btnAddCodigoManual.Location = new System.Drawing.Point(1176, 463);
            this.btnAddCodigoManual.Name = "btnAddCodigoManual";
            this.btnAddCodigoManual.Size = new System.Drawing.Size(63, 58);
            this.btnAddCodigoManual.TabIndex = 6;
            this.btnAddCodigoManual.UseVisualStyleBackColor = true;
            this.btnAddCodigoManual.Click += new System.EventHandler(this.btnAddCodigoManual_Click);
            // 
            // tgvArbolPedidos
            // 
            this.tgvArbolPedidos.AllowUserToAddRows = false;
            this.tgvArbolPedidos.AllowUserToDeleteRows = false;
            this.tgvArbolPedidos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tgvArbolPedidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tgvArbolPedidos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nodo,
            this.Desc,
            this.NumPedido,
            this.LineaPedido,
            this.Necesarias,
            this.Lanzadas,
            this.Proyecto,
            this.TipoReg});
            this.tgvArbolPedidos.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.tgvArbolPedidos.ImageList = null;
            this.tgvArbolPedidos.Location = new System.Drawing.Point(3, 3);
            this.tgvArbolPedidos.Name = "tgvArbolPedidos";
            this.tgvArbolPedidos.RowHeadersVisible = false;
            this.tgvArbolPedidos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tgvArbolPedidos.Size = new System.Drawing.Size(688, 518);
            this.tgvArbolPedidos.TabIndex = 23;
            this.tgvArbolPedidos.SelectionChanged += new System.EventHandler(this.tgvArbolPedidos_SelectionChanged);
            // 
            // Nodo
            // 
            this.Nodo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Nodo.DefaultNodeImage = null;
            this.Nodo.HeaderText = "Código";
            this.Nodo.Name = "Nodo";
            this.Nodo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Nodo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Desc
            // 
            this.Desc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Desc.HeaderText = "Descripción";
            this.Desc.Name = "Desc";
            this.Desc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // NumPedido
            // 
            this.NumPedido.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NumPedido.HeaderText = "Nº Pedido";
            this.NumPedido.Name = "NumPedido";
            this.NumPedido.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.NumPedido.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // LineaPedido
            // 
            this.LineaPedido.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.LineaPedido.HeaderText = "LineaPedido";
            this.LineaPedido.Name = "LineaPedido";
            this.LineaPedido.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Necesarias
            // 
            this.Necesarias.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Necesarias.HeaderText = "Necesarias";
            this.Necesarias.Name = "Necesarias";
            this.Necesarias.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Lanzadas
            // 
            this.Lanzadas.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Lanzadas.HeaderText = "Lanzadas";
            this.Lanzadas.Name = "Lanzadas";
            this.Lanzadas.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Proyecto
            // 
            this.Proyecto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Proyecto.HeaderText = "Proyecto";
            this.Proyecto.Name = "Proyecto";
            this.Proyecto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TipoReg
            // 
            this.TipoReg.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TipoReg.HeaderText = "T. Pedido";
            this.TipoReg.Name = "TipoReg";
            this.TipoReg.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TipoReg.Visible = false;
            // 
            // LblCantidad
            // 
            this.LblCantidad.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.LblCantidad.AutoSize = true;
            this.LblCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCantidad.Location = new System.Drawing.Point(692, 115);
            this.LblCantidad.Name = "LblCantidad";
            this.LblCantidad.Size = new System.Drawing.Size(57, 13);
            this.LblCantidad.TabIndex = 26;
            this.LblCantidad.Text = "Cantidad";
            // 
            // btnAddLineaProyecto
            // 
            this.btnAddLineaProyecto.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnAddLineaProyecto.Image = ((System.Drawing.Image)(resources.GetObject("btnAddLineaProyecto.Image")));
            this.btnAddLineaProyecto.Location = new System.Drawing.Point(697, 157);
            this.btnAddLineaProyecto.Name = "btnAddLineaProyecto";
            this.btnAddLineaProyecto.Size = new System.Drawing.Size(48, 48);
            this.btnAddLineaProyecto.TabIndex = 7;
            this.btnAddLineaProyecto.UseVisualStyleBackColor = true;
            this.btnAddLineaProyecto.Click += new System.EventHandler(this.btnAddLineaProyecto_Click);
            // 
            // btnRemoveLineaProyecto
            // 
            this.btnRemoveLineaProyecto.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnRemoveLineaProyecto.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveLineaProyecto.Image")));
            this.btnRemoveLineaProyecto.Location = new System.Drawing.Point(697, 220);
            this.btnRemoveLineaProyecto.Name = "btnRemoveLineaProyecto";
            this.btnRemoveLineaProyecto.Size = new System.Drawing.Size(48, 48);
            this.btnRemoveLineaProyecto.TabIndex = 8;
            this.btnRemoveLineaProyecto.UseVisualStyleBackColor = true;
            this.btnRemoveLineaProyecto.Click += new System.EventHandler(this.btnRemoveLineaProyecto_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.btnStop);
            this.tabPage2.Controls.Add(this.dgvNecesidadesProyecto);
            this.tabPage2.Controls.Add(this.btnCalcularNecesidades);
            this.tabPage2.Controls.Add(this.btnValidar);
            this.tabPage2.Controls.Add(this.btnBorrarLanzamiento);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1245, 524);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Necesidades del Proyecto";
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(90, 485);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(58, 25);
            this.btnStop.TabIndex = 14;
            this.btnStop.Text = "STOP";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Visible = false;
            // 
            // dgvNecesidadesProyecto
            // 
            this.dgvNecesidadesProyecto.AllowUserToAddRows = false;
            this.dgvNecesidadesProyecto.AllowUserToDeleteRows = false;
            this.dgvNecesidadesProyecto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvNecesidadesProyecto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNecesidadesProyecto.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dgvNecesidadesProyecto.Location = new System.Drawing.Point(3, 3);
            this.dgvNecesidadesProyecto.Name = "dgvNecesidadesProyecto";
            this.dgvNecesidadesProyecto.RowHeadersVisible = false;
            this.dgvNecesidadesProyecto.Size = new System.Drawing.Size(1239, 476);
            this.dgvNecesidadesProyecto.TabIndex = 0;
            this.dgvNecesidadesProyecto.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvNecesidadesProyecto_CellBeginEdit);
            this.dgvNecesidadesProyecto.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNecesidadesProyecto_CellEndEdit);
            // 
            // btnBorrarLanzamiento
            // 
            this.btnBorrarLanzamiento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBorrarLanzamiento.Enabled = false;
            this.btnBorrarLanzamiento.Location = new System.Drawing.Point(238, 485);
            this.btnBorrarLanzamiento.Name = "btnBorrarLanzamiento";
            this.btnBorrarLanzamiento.Size = new System.Drawing.Size(58, 25);
            this.btnBorrarLanzamiento.TabIndex = 12;
            this.btnBorrarLanzamiento.Text = "Invalidar";
            this.btnBorrarLanzamiento.UseVisualStyleBackColor = true;
            this.btnBorrarLanzamiento.Click += new System.EventHandler(this.btnBorrarLanzamiento_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Línea";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Descripción";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Código";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // gbFiltros
            // 
            this.gbFiltros.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFiltros.Controls.Add(this.btnNuevoProyectoExpedicion);
            this.gbFiltros.Controls.Add(this.checkBox_desglose_lineas);
            this.gbFiltros.Controls.Add(this.lblValidado);
            this.gbFiltros.Controls.Add(this.lbTipoPedido);
            this.gbFiltros.Controls.Add(this.cbTipoPedido);
            this.gbFiltros.Controls.Add(this.btnLimpiarFiltros);
            this.gbFiltros.Controls.Add(this.btnBuscarProyectos);
            this.gbFiltros.Controls.Add(this.dtpHorizonte);
            this.gbFiltros.Controls.Add(this.lbHorizonte);
            this.gbFiltros.Controls.Add(this.lbFamilia);
            this.gbFiltros.Controls.Add(this.cbFamilia);
            this.gbFiltros.Controls.Add(this.lbPedido);
            this.gbFiltros.Controls.Add(this.cbPedido);
            this.gbFiltros.Controls.Add(this.btnObtenerPedidos);
            this.gbFiltros.Controls.Add(this.lbProyecto);
            this.gbFiltros.Controls.Add(this.btnGuardarProyecto);
            this.gbFiltros.Controls.Add(this.cbProyecto);
            this.gbFiltros.Controls.Add(this.btnNuevoProyecto);
            this.gbFiltros.Controls.Add(this.label1);
            this.gbFiltros.Controls.Add(this.tbCodigoProyecto);
            this.gbFiltros.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFiltros.Location = new System.Drawing.Point(8, 7);
            this.gbFiltros.Name = "gbFiltros";
            this.gbFiltros.Size = new System.Drawing.Size(1249, 98);
            this.gbFiltros.TabIndex = 0;
            this.gbFiltros.TabStop = false;
            this.gbFiltros.Text = "Filtros";
            // 
            // btnNuevoProyectoExpedicion
            // 
            this.btnNuevoProyectoExpedicion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNuevoProyectoExpedicion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevoProyectoExpedicion.Location = new System.Drawing.Point(1144, 54);
            this.btnNuevoProyectoExpedicion.Name = "btnNuevoProyectoExpedicion";
            this.btnNuevoProyectoExpedicion.Size = new System.Drawing.Size(99, 38);
            this.btnNuevoProyectoExpedicion.TabIndex = 16;
            this.btnNuevoProyectoExpedicion.Text = "Nuevo Proyecto EXPEDICION";
            this.btnNuevoProyectoExpedicion.UseVisualStyleBackColor = true;
            this.btnNuevoProyectoExpedicion.Click += new System.EventHandler(this.btnNuevoProyectoExpedicion_Click);
            // 
            // checkBox_desglose_lineas
            // 
            this.checkBox_desglose_lineas.AutoSize = true;
            this.checkBox_desglose_lineas.Location = new System.Drawing.Point(128, 75);
            this.checkBox_desglose_lineas.Name = "checkBox_desglose_lineas";
            this.checkBox_desglose_lineas.Size = new System.Drawing.Size(105, 17);
            this.checkBox_desglose_lineas.TabIndex = 15;
            this.checkBox_desglose_lineas.Text = "Desglosar líneas";
            this.checkBox_desglose_lineas.UseVisualStyleBackColor = true;
            // 
            // lblValidado
            // 
            this.lblValidado.AutoSize = true;
            this.lblValidado.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValidado.ForeColor = System.Drawing.Color.Red;
            this.lblValidado.Location = new System.Drawing.Point(276, 54);
            this.lblValidado.Name = "lblValidado";
            this.lblValidado.Size = new System.Drawing.Size(188, 37);
            this.lblValidado.TabIndex = 14;
            this.lblValidado.Text = "VALIDADO";
            this.lblValidado.Visible = false;
            // 
            // lbTipoPedido
            // 
            this.lbTipoPedido.AutoSize = true;
            this.lbTipoPedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTipoPedido.Location = new System.Drawing.Point(7, 29);
            this.lbTipoPedido.Name = "lbTipoPedido";
            this.lbTipoPedido.Size = new System.Drawing.Size(56, 13);
            this.lbTipoPedido.TabIndex = 8;
            this.lbTipoPedido.Text = "T. Pedido:";
            // 
            // cbTipoPedido
            // 
            this.cbTipoPedido.FormattingEnabled = true;
            this.cbTipoPedido.ItemHeight = 13;
            this.cbTipoPedido.Items.AddRange(new object[] {
            "",
            "CF",
            "CS"});
            this.cbTipoPedido.Location = new System.Drawing.Point(72, 25);
            this.cbTipoPedido.Name = "cbTipoPedido";
            this.cbTipoPedido.Size = new System.Drawing.Size(59, 21);
            this.cbTipoPedido.TabIndex = 3;
            // 
            // btnLimpiarFiltros
            // 
            this.btnLimpiarFiltros.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLimpiarFiltros.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiarFiltros.Location = new System.Drawing.Point(1146, 22);
            this.btnLimpiarFiltros.Name = "btnLimpiarFiltros";
            this.btnLimpiarFiltros.Size = new System.Drawing.Size(88, 23);
            this.btnLimpiarFiltros.TabIndex = 7;
            this.btnLimpiarFiltros.Text = "Limpiar Filtros";
            this.btnLimpiarFiltros.UseVisualStyleBackColor = true;
            this.btnLimpiarFiltros.Click += new System.EventHandler(this.btnLimpiarFiltros_Click);
            // 
            // btnBuscarProyectos
            // 
            this.btnBuscarProyectos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscarProyectos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarProyectos.Location = new System.Drawing.Point(722, 67);
            this.btnBuscarProyectos.Name = "btnBuscarProyectos";
            this.btnBuscarProyectos.Size = new System.Drawing.Size(99, 23);
            this.btnBuscarProyectos.TabIndex = 9;
            this.btnBuscarProyectos.Text = "Buscar Proyectos";
            this.btnBuscarProyectos.UseVisualStyleBackColor = true;
            this.btnBuscarProyectos.Click += new System.EventHandler(this.btnBuscarProyectos_Click);
            // 
            // dtpHorizonte
            // 
            this.dtpHorizonte.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHorizonte.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHorizonte.Location = new System.Drawing.Point(503, 24);
            this.dtpHorizonte.Name = "dtpHorizonte";
            this.dtpHorizonte.Size = new System.Drawing.Size(99, 20);
            this.dtpHorizonte.TabIndex = 4;
            // 
            // lbHorizonte
            // 
            this.lbHorizonte.AutoSize = true;
            this.lbHorizonte.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHorizonte.Location = new System.Drawing.Point(439, 28);
            this.lbHorizonte.Name = "lbHorizonte";
            this.lbHorizonte.Size = new System.Drawing.Size(55, 13);
            this.lbHorizonte.TabIndex = 7;
            this.lbHorizonte.Text = "Horizonte:";
            // 
            // lbFamilia
            // 
            this.lbFamilia.AutoSize = true;
            this.lbFamilia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFamilia.Location = new System.Drawing.Point(323, 29);
            this.lbFamilia.Name = "lbFamilia";
            this.lbFamilia.Size = new System.Drawing.Size(42, 13);
            this.lbFamilia.TabIndex = 4;
            this.lbFamilia.Text = "Familia:";
            // 
            // cbFamilia
            // 
            this.cbFamilia.FormattingEnabled = true;
            this.cbFamilia.ItemHeight = 13;
            this.cbFamilia.Location = new System.Drawing.Point(374, 25);
            this.cbFamilia.Name = "cbFamilia";
            this.cbFamilia.Size = new System.Drawing.Size(59, 21);
            this.cbFamilia.TabIndex = 2;
            // 
            // btnUltimo
            // 
            this.btnUltimo.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnUltimo.Image = ((System.Drawing.Image)(resources.GetObject("btnUltimo.Image")));
            this.btnUltimo.Location = new System.Drawing.Point(1048, 667);
            this.btnUltimo.Name = "btnUltimo";
            this.btnUltimo.Size = new System.Drawing.Size(32, 32);
            this.btnUltimo.TabIndex = 33;
            this.btnUltimo.UseVisualStyleBackColor = true;
            this.btnUltimo.Click += new System.EventHandler(this.btnUltimo_Click);
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSiguiente.Image = ((System.Drawing.Image)(resources.GetObject("btnSiguiente.Image")));
            this.btnSiguiente.Location = new System.Drawing.Point(1009, 667);
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(32, 32);
            this.btnSiguiente.TabIndex = 32;
            this.btnSiguiente.UseVisualStyleBackColor = true;
            this.btnSiguiente.Click += new System.EventHandler(this.btnSiguiente_Click);
            // 
            // btnAnterior
            // 
            this.btnAnterior.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAnterior.Image = ((System.Drawing.Image)(resources.GetObject("btnAnterior.Image")));
            this.btnAnterior.Location = new System.Drawing.Point(970, 667);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(32, 32);
            this.btnAnterior.TabIndex = 31;
            this.btnAnterior.UseVisualStyleBackColor = true;
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
            // 
            // btnPrimero
            // 
            this.btnPrimero.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnPrimero.Image = ((System.Drawing.Image)(resources.GetObject("btnPrimero.Image")));
            this.btnPrimero.Location = new System.Drawing.Point(931, 667);
            this.btnPrimero.Name = "btnPrimero";
            this.btnPrimero.Size = new System.Drawing.Size(32, 32);
            this.btnPrimero.TabIndex = 30;
            this.btnPrimero.UseVisualStyleBackColor = true;
            this.btnPrimero.Click += new System.EventHandler(this.btnPrimero_Click);
            // 
            // LanzaProyectosForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1277, 711);
            this.Controls.Add(this.btnUltimo);
            this.Controls.Add(this.btnSiguiente);
            this.Controls.Add(this.btnAnterior);
            this.Controls.Add(this.btnPrimero);
            this.Controls.Add(this.gbFiltros);
            this.Controls.Add(this.tcProyecto);
            this.MinimumSize = new System.Drawing.Size(1293, 510);
            this.Name = "LanzaProyectosForm";
            this.ShowIcon = false;
            this.Text = "Lanzador";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LanzaProyectosForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLineasProyecto)).EndInit();
            this.tcProyecto.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDCantidadLinea)).EndInit();
            this.cbCantidad.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).EndInit();
            this.gbCodigo.ResumeLayout(false);
            this.gbCodigo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tgvArbolPedidos)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNecesidadesProyecto)).EndInit();
            this.gbFiltros.ResumeLayout(false);
            this.gbFiltros.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbPedido;
        private System.Windows.Forms.Label lbPedido;
        private System.Windows.Forms.Button btnObtenerPedidos;
        private System.Windows.Forms.Button btnAddLineaProyecto;
        private System.Windows.Forms.TextBox tbCodigoProyecto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbProyecto;
        private System.Windows.Forms.ComboBox cbProyecto;
        private System.Windows.Forms.Button btnNuevoProyecto;
        private System.Windows.Forms.Button btnGuardarProyecto;
        private System.Windows.Forms.Button btnCalcularNecesidades;
        private System.Windows.Forms.Button btnValidar;
        private System.Windows.Forms.DataGridView dgvLineasProyecto;
        private System.Windows.Forms.TabControl tcProyecto;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnRemoveLineaProyecto;
        private AdvancedDataGridView.TreeGridView tgvArbolPedidos;
        private System.Windows.Forms.DataGridView dgvNecesidadesProyecto;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.Button btnBorrarLanzamiento;
        private System.Windows.Forms.Label LblCantidad;
        private System.Windows.Forms.Button btnAddCodigoManual;
        private System.Windows.Forms.GroupBox gbFiltros;
        private System.Windows.Forms.Label lbFamilia;
        private System.Windows.Forms.ComboBox cbFamilia;
        private System.Windows.Forms.DateTimePicker dtpHorizonte;
        private System.Windows.Forms.Label lbHorizonte;
        private System.Windows.Forms.Button btnLimpiarFiltros;
        private System.Windows.Forms.Button btnBuscarProyectos;
        private System.Windows.Forms.Label lbTipoPedido;
        private System.Windows.Forms.ComboBox cbTipoPedido;
        private System.Windows.Forms.Button btnPrimero;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.Button btnSiguiente;
        private System.Windows.Forms.Button btnUltimo;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.GroupBox cbCantidad;
        private System.Windows.Forms.GroupBox gbCodigo;
        private System.Windows.Forms.NumericUpDown nudCantidad;
        private System.Windows.Forms.Label lblValidado;
        private System.Windows.Forms.TextBox tbCodigoManual;
        private AdvancedDataGridView.TreeGridColumn Nodo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumPedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn LineaPedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn Necesarias;
        private System.Windows.Forms.DataGridViewTextBoxColumn Lanzadas;
        private System.Windows.Forms.DataGridViewTextBoxColumn Proyecto;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoReg;
        private System.Windows.Forms.NumericUpDown nUDCantidadLinea;
        private System.Windows.Forms.CheckBox checkBox_desglose_lineas;
        private System.Windows.Forms.Button btnNuevoProyectoExpedicion;
        

    }
}

