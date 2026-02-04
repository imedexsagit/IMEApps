namespace Imprimacion
{
    partial class Imprimacion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridImprimacion = new System.Windows.Forms.DataGridView();
            this.columnCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnImprimacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnError = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonAccept = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.mnu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_limpiar = new System.Windows.Forms.Button();
            this.tbox_marca = new System.Windows.Forms.TextBox();
            this.lb_busq = new System.Windows.Forms.Label();
            this.gb_busq = new System.Windows.Forms.GroupBox();
            this.checkBoxA3 = new System.Windows.Forms.CheckBox();
            this.checkBoxA2 = new System.Windows.Forms.CheckBox();
            this.checkBoxA1 = new System.Windows.Forms.CheckBox();
            this.checkBoxA0 = new System.Windows.Forms.CheckBox();
            this.labelFilter = new System.Windows.Forms.Label();
            this.btn_busq = new System.Windows.Forms.Button();
            this.dgResultados = new System.Windows.Forms.DataGridView();
            this.gb_resultado = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tPrincipal = new System.Windows.Forms.TabPage();
            this.tModificacion = new System.Windows.Forms.TabPage();
            this.gBoxResul = new System.Windows.Forms.GroupBox();
            this.lbCodigo = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.comboOpciones = new System.Windows.Forms.ComboBox();
            this.lbEstado = new System.Windows.Forms.Label();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.tboxCodModificar = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbTTT = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbCodigoTTT = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnBuscarTT = new System.Windows.Forms.Button();
            this.tbPedidoTTT = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgResultadosTTT = new System.Windows.Forms.DataGridView();
            this.btnLimpiarCodigo = new System.Windows.Forms.Button();
            this.gbResultadoTTT = new System.Windows.Forms.GroupBox();
            this.lbResultadoTT = new System.Windows.Forms.Label();
            this.btnQuitarTTT = new System.Windows.Forms.Button();
            this.btnAsignarTTT = new System.Windows.Forms.Button();
            this.btnConsultarTTT = new System.Windows.Forms.Button();
            this.tbCodigoTT = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridImprimacion)).BeginInit();
            this.mnu.SuspendLayout();
            this.gb_busq.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgResultados)).BeginInit();
            this.gb_resultado.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tPrincipal.SuspendLayout();
            this.tModificacion.SuspendLayout();
            this.gBoxResul.SuspendLayout();
            this.tbTTT.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgResultadosTTT)).BeginInit();
            this.gbResultadoTTT.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridImprimacion
            // 
            this.dataGridImprimacion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridImprimacion.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnCodigo,
            this.columnImprimacion,
            this.columnError});
            this.dataGridImprimacion.Location = new System.Drawing.Point(6, 6);
            this.dataGridImprimacion.Name = "dataGridImprimacion";
            this.dataGridImprimacion.Size = new System.Drawing.Size(958, 480);
            this.dataGridImprimacion.TabIndex = 0;
            this.dataGridImprimacion.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dataGridImprimacion_KeyUp);
            this.dataGridImprimacion.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridImprimacion_MouseClick);
            // 
            // columnCodigo
            // 
            this.columnCodigo.HeaderText = "Codigo";
            this.columnCodigo.Name = "columnCodigo";
            this.columnCodigo.Width = 305;
            // 
            // columnImprimacion
            // 
            this.columnImprimacion.HeaderText = "Imprimacion";
            this.columnImprimacion.Name = "columnImprimacion";
            this.columnImprimacion.Width = 305;
            // 
            // columnError
            // 
            this.columnError.HeaderText = "Error";
            this.columnError.Name = "columnError";
            this.columnError.Width = 305;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(6, 499);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(958, 14);
            this.label1.TabIndex = 4;
            // 
            // buttonAccept
            // 
            this.buttonAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAccept.Location = new System.Drawing.Point(889, 736);
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(75, 23);
            this.buttonAccept.TabIndex = 5;
            this.buttonAccept.Text = "Aceptar";
            this.buttonAccept.UseVisualStyleBackColor = true;
            this.buttonAccept.Click += new System.EventHandler(this.buttonAccept_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(779, 736);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancelar";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // mnu
            // 
            this.mnu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCopy,
            this.mnuPaste});
            this.mnu.Name = "contextMenuStripMnu";
            this.mnu.Size = new System.Drawing.Size(110, 48);
            // 
            // mnuCopy
            // 
            this.mnuCopy.Name = "mnuCopy";
            this.mnuCopy.Size = new System.Drawing.Size(109, 22);
            this.mnuCopy.Text = "Copiar";
            this.mnuCopy.Click += new System.EventHandler(this.copiarToolStripMenuItem_Click);
            // 
            // mnuPaste
            // 
            this.mnuPaste.Name = "mnuPaste";
            this.mnuPaste.Size = new System.Drawing.Size(109, 22);
            this.mnuPaste.Text = "Pegar";
            this.mnuPaste.Click += new System.EventHandler(this.pegarToolStripMenuItem_Click);
            // 
            // btn_limpiar
            // 
            this.btn_limpiar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_limpiar.Location = new System.Drawing.Point(141, 754);
            this.btn_limpiar.Name = "btn_limpiar";
            this.btn_limpiar.Size = new System.Drawing.Size(105, 23);
            this.btn_limpiar.TabIndex = 8;
            this.btn_limpiar.Text = "Limpiar Tabla";
            this.btn_limpiar.UseVisualStyleBackColor = true;
            this.btn_limpiar.Click += new System.EventHandler(this.btn_limpiar_Click);
            // 
            // tbox_marca
            // 
            this.tbox_marca.Location = new System.Drawing.Point(10, 58);
            this.tbox_marca.Name = "tbox_marca";
            this.tbox_marca.Size = new System.Drawing.Size(197, 26);
            this.tbox_marca.TabIndex = 10;
            // 
            // lb_busq
            // 
            this.lb_busq.AutoSize = true;
            this.lb_busq.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_busq.Location = new System.Drawing.Point(6, 35);
            this.lb_busq.Name = "lb_busq";
            this.lb_busq.Size = new System.Drawing.Size(201, 20);
            this.lb_busq.TabIndex = 11;
            this.lb_busq.Text = "Introducir código del padre:";
            // 
            // gb_busq
            // 
            this.gb_busq.Controls.Add(this.checkBoxA3);
            this.gb_busq.Controls.Add(this.checkBoxA2);
            this.gb_busq.Controls.Add(this.checkBoxA1);
            this.gb_busq.Controls.Add(this.checkBoxA0);
            this.gb_busq.Controls.Add(this.labelFilter);
            this.gb_busq.Controls.Add(this.btn_busq);
            this.gb_busq.Controls.Add(this.tbox_marca);
            this.gb_busq.Controls.Add(this.lb_busq);
            this.gb_busq.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_busq.Location = new System.Drawing.Point(6, 516);
            this.gb_busq.Name = "gb_busq";
            this.gb_busq.Size = new System.Drawing.Size(240, 227);
            this.gb_busq.TabIndex = 13;
            this.gb_busq.TabStop = false;
            this.gb_busq.Text = "Buscador";
            // 
            // checkBoxA3
            // 
            this.checkBoxA3.AutoSize = true;
            this.checkBoxA3.Location = new System.Drawing.Point(88, 188);
            this.checkBoxA3.Name = "checkBoxA3";
            this.checkBoxA3.Size = new System.Drawing.Size(48, 24);
            this.checkBoxA3.TabIndex = 18;
            this.checkBoxA3.Text = "A3";
            this.checkBoxA3.UseVisualStyleBackColor = true;
            this.checkBoxA3.CheckedChanged += new System.EventHandler(this.checkBoxA3_CheckedChanged);
            // 
            // checkBoxA2
            // 
            this.checkBoxA2.AutoSize = true;
            this.checkBoxA2.Location = new System.Drawing.Point(10, 188);
            this.checkBoxA2.Name = "checkBoxA2";
            this.checkBoxA2.Size = new System.Drawing.Size(48, 24);
            this.checkBoxA2.TabIndex = 17;
            this.checkBoxA2.Text = "A2";
            this.checkBoxA2.UseVisualStyleBackColor = true;
            this.checkBoxA2.CheckedChanged += new System.EventHandler(this.checkBoxA2_CheckedChanged);
            // 
            // checkBoxA1
            // 
            this.checkBoxA1.AutoSize = true;
            this.checkBoxA1.Location = new System.Drawing.Point(88, 158);
            this.checkBoxA1.Name = "checkBoxA1";
            this.checkBoxA1.Size = new System.Drawing.Size(48, 24);
            this.checkBoxA1.TabIndex = 16;
            this.checkBoxA1.Text = "A1";
            this.checkBoxA1.UseVisualStyleBackColor = true;
            this.checkBoxA1.CheckedChanged += new System.EventHandler(this.checkBoxA1_CheckedChanged);
            // 
            // checkBoxA0
            // 
            this.checkBoxA0.AutoSize = true;
            this.checkBoxA0.Location = new System.Drawing.Point(10, 158);
            this.checkBoxA0.Name = "checkBoxA0";
            this.checkBoxA0.Size = new System.Drawing.Size(48, 24);
            this.checkBoxA0.TabIndex = 14;
            this.checkBoxA0.Text = "A0";
            this.checkBoxA0.UseVisualStyleBackColor = true;
            this.checkBoxA0.CheckedChanged += new System.EventHandler(this.checkBoxA0_CheckedChanged);
            // 
            // labelFilter
            // 
            this.labelFilter.AutoSize = true;
            this.labelFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFilter.Location = new System.Drawing.Point(6, 135);
            this.labelFilter.Name = "labelFilter";
            this.labelFilter.Size = new System.Drawing.Size(150, 20);
            this.labelFilter.TabIndex = 15;
            this.labelFilter.Text = "Seleccione un valor:";
            // 
            // btn_busq
            // 
            this.btn_busq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_busq.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.btn_busq.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_busq.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.btn_busq.Location = new System.Drawing.Point(88, 90);
            this.btn_busq.Name = "btn_busq";
            this.btn_busq.Size = new System.Drawing.Size(128, 31);
            this.btn_busq.TabIndex = 14;
            this.btn_busq.Text = "Buscar";
            this.btn_busq.UseVisualStyleBackColor = false;
            this.btn_busq.Click += new System.EventHandler(this.btn_busq_Click);
            // 
            // dgResultados
            // 
            this.dgResultados.AllowUserToAddRows = false;
            this.dgResultados.AllowUserToDeleteRows = false;
            this.dgResultados.AllowUserToResizeColumns = false;
            this.dgResultados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgResultados.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgResultados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgResultados.Location = new System.Drawing.Point(6, 25);
            this.dgResultados.Name = "dgResultados";
            this.dgResultados.Size = new System.Drawing.Size(706, 174);
            this.dgResultados.TabIndex = 0;
            // 
            // gb_resultado
            // 
            this.gb_resultado.Controls.Add(this.dgResultados);
            this.gb_resultado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_resultado.Location = new System.Drawing.Point(252, 516);
            this.gb_resultado.Name = "gb_resultado";
            this.gb_resultado.Size = new System.Drawing.Size(718, 205);
            this.gb_resultado.TabIndex = 12;
            this.gb_resultado.TabStop = false;
            this.gb_resultado.Text = "Resultados";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tPrincipal);
            this.tabControl1.Controls.Add(this.tModificacion);
            this.tabControl1.Controls.Add(this.tbTTT);
            this.tabControl1.Location = new System.Drawing.Point(7, 6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(988, 811);
            this.tabControl1.TabIndex = 14;
            // 
            // tPrincipal
            // 
            this.tPrincipal.Controls.Add(this.dataGridImprimacion);
            this.tPrincipal.Controls.Add(this.label1);
            this.tPrincipal.Controls.Add(this.buttonAccept);
            this.tPrincipal.Controls.Add(this.buttonCancel);
            this.tPrincipal.Controls.Add(this.btn_limpiar);
            this.tPrincipal.Controls.Add(this.gb_resultado);
            this.tPrincipal.Controls.Add(this.gb_busq);
            this.tPrincipal.Location = new System.Drawing.Point(4, 22);
            this.tPrincipal.Name = "tPrincipal";
            this.tPrincipal.Padding = new System.Windows.Forms.Padding(3);
            this.tPrincipal.Size = new System.Drawing.Size(980, 785);
            this.tPrincipal.TabIndex = 0;
            this.tPrincipal.Text = "Principal";
            this.tPrincipal.UseVisualStyleBackColor = true;
            // 
            // tModificacion
            // 
            this.tModificacion.Controls.Add(this.gBoxResul);
            this.tModificacion.Controls.Add(this.btnConsultar);
            this.tModificacion.Controls.Add(this.tboxCodModificar);
            this.tModificacion.Controls.Add(this.label2);
            this.tModificacion.Location = new System.Drawing.Point(4, 22);
            this.tModificacion.Name = "tModificacion";
            this.tModificacion.Padding = new System.Windows.Forms.Padding(3);
            this.tModificacion.Size = new System.Drawing.Size(980, 785);
            this.tModificacion.TabIndex = 1;
            this.tModificacion.Text = "Modificación Individual";
            this.tModificacion.UseVisualStyleBackColor = true;
            // 
            // gBoxResul
            // 
            this.gBoxResul.Controls.Add(this.lbCodigo);
            this.gBoxResul.Controls.Add(this.button1);
            this.gBoxResul.Controls.Add(this.comboOpciones);
            this.gBoxResul.Controls.Add(this.lbEstado);
            this.gBoxResul.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gBoxResul.Location = new System.Drawing.Point(22, 211);
            this.gBoxResul.Name = "gBoxResul";
            this.gBoxResul.Size = new System.Drawing.Size(940, 473);
            this.gBoxResul.TabIndex = 3;
            this.gBoxResul.TabStop = false;
            this.gBoxResul.Text = "Resultado";
            // 
            // lbCodigo
            // 
            this.lbCodigo.AutoSize = true;
            this.lbCodigo.Location = new System.Drawing.Point(9, 68);
            this.lbCodigo.Name = "lbCodigo";
            this.lbCodigo.Size = new System.Drawing.Size(15, 24);
            this.lbCodigo.TabIndex = 5;
            this.lbCodigo.Text = " ";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(423, 194);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(196, 53);
            this.button1.TabIndex = 4;
            this.button1.Text = "Modificar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboOpciones
            // 
            this.comboOpciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboOpciones.FormattingEnabled = true;
            this.comboOpciones.Items.AddRange(new object[] {
            "A0",
            "A1",
            "A2",
            "A3",
            "Sin Imprimación"});
            this.comboOpciones.Location = new System.Drawing.Point(13, 194);
            this.comboOpciones.Name = "comboOpciones";
            this.comboOpciones.Size = new System.Drawing.Size(318, 32);
            this.comboOpciones.TabIndex = 1;
            // 
            // lbEstado
            // 
            this.lbEstado.AutoSize = true;
            this.lbEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEstado.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lbEstado.Location = new System.Drawing.Point(6, 133);
            this.lbEstado.Name = "lbEstado";
            this.lbEstado.Size = new System.Drawing.Size(292, 42);
            this.lbEstado.TabIndex = 0;
            this.lbEstado.Text = "Sin Imprimación.";
            // 
            // btnConsultar
            // 
            this.btnConsultar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsultar.Location = new System.Drawing.Point(664, 96);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(196, 53);
            this.btnConsultar.TabIndex = 2;
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // tboxCodModificar
            // 
            this.tboxCodModificar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tboxCodModificar.Location = new System.Drawing.Point(162, 107);
            this.tboxCodModificar.Name = "tboxCodModificar";
            this.tboxCodModificar.Size = new System.Drawing.Size(365, 29);
            this.tboxCodModificar.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(158, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(177, 24);
            this.label2.TabIndex = 0;
            this.label2.Text = "Introduce el código.";
            // 
            // tbTTT
            // 
            this.tbTTT.Controls.Add(this.groupBox1);
            this.tbTTT.Controls.Add(this.groupBox2);
            this.tbTTT.Controls.Add(this.btnLimpiarCodigo);
            this.tbTTT.Controls.Add(this.gbResultadoTTT);
            this.tbTTT.Controls.Add(this.btnConsultarTTT);
            this.tbTTT.Controls.Add(this.tbCodigoTT);
            this.tbTTT.Controls.Add(this.label3);
            this.tbTTT.Location = new System.Drawing.Point(4, 22);
            this.tbTTT.Name = "tbTTT";
            this.tbTTT.Size = new System.Drawing.Size(980, 785);
            this.tbTTT.TabIndex = 2;
            this.tbTTT.Text = "Insertar TT";
            this.tbTTT.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbCodigoTTT);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnBuscarTT);
            this.groupBox1.Controls.Add(this.tbPedidoTTT);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(18, 487);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(240, 251);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Buscador TTT";
            // 
            // tbCodigoTTT
            // 
            this.tbCodigoTTT.Location = new System.Drawing.Point(10, 144);
            this.tbCodigoTTT.Name = "tbCodigoTTT";
            this.tbCodigoTTT.Size = new System.Drawing.Size(197, 26);
            this.tbCodigoTTT.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(201, 20);
            this.label5.TabIndex = 16;
            this.label5.Text = "Introducir código del padre:";
            // 
            // btnBuscarTT
            // 
            this.btnBuscarTT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscarTT.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.btnBuscarTT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarTT.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.btnBuscarTT.Location = new System.Drawing.Point(89, 205);
            this.btnBuscarTT.Name = "btnBuscarTT";
            this.btnBuscarTT.Size = new System.Drawing.Size(128, 31);
            this.btnBuscarTT.TabIndex = 14;
            this.btnBuscarTT.Text = "Buscar";
            this.btnBuscarTT.UseVisualStyleBackColor = false;
            this.btnBuscarTT.Click += new System.EventHandler(this.btnBuscarTT_Click);
            // 
            // tbPedidoTTT
            // 
            this.tbPedidoTTT.Location = new System.Drawing.Point(10, 69);
            this.tbPedidoTTT.Name = "tbPedidoTTT";
            this.tbPedidoTTT.Size = new System.Drawing.Size(197, 26);
            this.tbPedidoTTT.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Introducir Pedido:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgResultadosTTT);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(264, 487);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(712, 251);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Resultados";
            // 
            // dgResultadosTTT
            // 
            this.dgResultadosTTT.AllowUserToAddRows = false;
            this.dgResultadosTTT.AllowUserToDeleteRows = false;
            this.dgResultadosTTT.AllowUserToResizeColumns = false;
            this.dgResultadosTTT.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgResultadosTTT.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgResultadosTTT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgResultadosTTT.Location = new System.Drawing.Point(6, 25);
            this.dgResultadosTTT.Name = "dgResultadosTTT";
            this.dgResultadosTTT.Size = new System.Drawing.Size(700, 211);
            this.dgResultadosTTT.TabIndex = 0;
            // 
            // btnLimpiarCodigo
            // 
            this.btnLimpiarCodigo.Location = new System.Drawing.Point(18, 131);
            this.btnLimpiarCodigo.Name = "btnLimpiarCodigo";
            this.btnLimpiarCodigo.Size = new System.Drawing.Size(111, 23);
            this.btnLimpiarCodigo.TabIndex = 9;
            this.btnLimpiarCodigo.Text = "Limpiar Código";
            this.btnLimpiarCodigo.UseVisualStyleBackColor = true;
            this.btnLimpiarCodigo.Visible = false;
            this.btnLimpiarCodigo.Click += new System.EventHandler(this.btnLimpiarCodigo_Click);
            // 
            // gbResultadoTTT
            // 
            this.gbResultadoTTT.Controls.Add(this.lbResultadoTT);
            this.gbResultadoTTT.Controls.Add(this.btnQuitarTTT);
            this.gbResultadoTTT.Controls.Add(this.btnAsignarTTT);
            this.gbResultadoTTT.Location = new System.Drawing.Point(21, 177);
            this.gbResultadoTTT.Name = "gbResultadoTTT";
            this.gbResultadoTTT.Size = new System.Drawing.Size(944, 289);
            this.gbResultadoTTT.TabIndex = 8;
            this.gbResultadoTTT.TabStop = false;
            // 
            // lbResultadoTT
            // 
            this.lbResultadoTT.AutoSize = true;
            this.lbResultadoTT.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbResultadoTT.Location = new System.Drawing.Point(80, 45);
            this.lbResultadoTT.Name = "lbResultadoTT";
            this.lbResultadoTT.Size = new System.Drawing.Size(28, 31);
            this.lbResultadoTT.TabIndex = 7;
            this.lbResultadoTT.Text = "  ";
            // 
            // btnQuitarTTT
            // 
            this.btnQuitarTTT.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuitarTTT.Location = new System.Drawing.Point(587, 174);
            this.btnQuitarTTT.Name = "btnQuitarTTT";
            this.btnQuitarTTT.Size = new System.Drawing.Size(196, 53);
            this.btnQuitarTTT.TabIndex = 6;
            this.btnQuitarTTT.Text = "Quitar TTT";
            this.btnQuitarTTT.UseVisualStyleBackColor = true;
            this.btnQuitarTTT.Click += new System.EventHandler(this.btnQuitarTTT_Click);
            // 
            // btnAsignarTTT
            // 
            this.btnAsignarTTT.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAsignarTTT.Location = new System.Drawing.Point(86, 174);
            this.btnAsignarTTT.Name = "btnAsignarTTT";
            this.btnAsignarTTT.Size = new System.Drawing.Size(196, 53);
            this.btnAsignarTTT.TabIndex = 5;
            this.btnAsignarTTT.Text = "Asignar TTT";
            this.btnAsignarTTT.UseVisualStyleBackColor = true;
            this.btnAsignarTTT.Click += new System.EventHandler(this.btnAsignarTTT_Click);
            // 
            // btnConsultarTTT
            // 
            this.btnConsultarTTT.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsultarTTT.Location = new System.Drawing.Point(506, 60);
            this.btnConsultarTTT.Name = "btnConsultarTTT";
            this.btnConsultarTTT.Size = new System.Drawing.Size(196, 53);
            this.btnConsultarTTT.TabIndex = 7;
            this.btnConsultarTTT.Text = "Consultar";
            this.btnConsultarTTT.UseVisualStyleBackColor = true;
            this.btnConsultarTTT.Click += new System.EventHandler(this.btnConsultarTTT_Click);
            // 
            // tbCodigoTT
            // 
            this.tbCodigoTT.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCodigoTT.Location = new System.Drawing.Point(18, 84);
            this.tbCodigoTT.Name = "tbCodigoTT";
            this.tbCodigoTT.Size = new System.Drawing.Size(365, 29);
            this.tbCodigoTT.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(177, 24);
            this.label3.TabIndex = 3;
            this.label3.Text = "Introduce el código.";
            // 
            // Imprimacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 817);
            this.Controls.Add(this.tabControl1);
            this.Name = "Imprimacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Imprimacion";
            this.Load += new System.EventHandler(this.Imprimacion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridImprimacion)).EndInit();
            this.mnu.ResumeLayout(false);
            this.gb_busq.ResumeLayout(false);
            this.gb_busq.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgResultados)).EndInit();
            this.gb_resultado.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tPrincipal.ResumeLayout(false);
            this.tModificacion.ResumeLayout(false);
            this.tModificacion.PerformLayout();
            this.gBoxResul.ResumeLayout(false);
            this.gBoxResul.PerformLayout();
            this.tbTTT.ResumeLayout(false);
            this.tbTTT.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgResultadosTTT)).EndInit();
            this.gbResultadoTTT.ResumeLayout(false);
            this.gbResultadoTTT.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridImprimacion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonAccept;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ContextMenuStrip mnu;
        private System.Windows.Forms.ToolStripMenuItem mnuCopy;
        private System.Windows.Forms.ToolStripMenuItem mnuPaste;
        private System.Windows.Forms.Button btn_limpiar;
        private System.Windows.Forms.TextBox tbox_marca;
        private System.Windows.Forms.Label lb_busq;
        private System.Windows.Forms.GroupBox gb_busq;
        private System.Windows.Forms.Button btn_busq;
        private System.Windows.Forms.DataGridView dgResultados;
        private System.Windows.Forms.GroupBox gb_resultado;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnImprimacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnError;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tPrincipal;
        private System.Windows.Forms.TabPage tModificacion;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.TextBox tboxCodModificar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gBoxResul;
        private System.Windows.Forms.Label lbEstado;
        private System.Windows.Forms.ComboBox comboOpciones;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbCodigo;
        private System.Windows.Forms.TabPage tbTTT;
        private System.Windows.Forms.GroupBox gbResultadoTTT;
        private System.Windows.Forms.Button btnQuitarTTT;
        private System.Windows.Forms.Button btnAsignarTTT;
        private System.Windows.Forms.Button btnConsultarTTT;
        private System.Windows.Forms.TextBox tbCodigoTT;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbResultadoTT;
        private System.Windows.Forms.Button btnLimpiarCodigo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbCodigoTTT;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnBuscarTT;
        private System.Windows.Forms.TextBox tbPedidoTTT;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgResultadosTTT;
        private System.Windows.Forms.Label labelFilter;
        private System.Windows.Forms.CheckBox checkBoxA3;
        private System.Windows.Forms.CheckBox checkBoxA2;
        private System.Windows.Forms.CheckBox checkBoxA1;
        private System.Windows.Forms.CheckBox checkBoxA0;
      
    }
}