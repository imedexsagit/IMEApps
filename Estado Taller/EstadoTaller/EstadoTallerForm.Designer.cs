namespace EstadoTaller
{
    partial class EstadoTallerForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EstadoTallerForm));
            this.sCPrincipal = new System.Windows.Forms.SplitContainer();
            this.sCOpciones = new System.Windows.Forms.SplitContainer();
            this.panelOpciones = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExportarExcel = new System.Windows.Forms.Button();
            this.comboEMpresaMaq = new System.Windows.Forms.ComboBox();
            this.rbAcabado = new System.Windows.Forms.RadioButton();
            this.cBAux = new System.Windows.Forms.ComboBox();
            this.rBAux = new System.Windows.Forms.RadioButton();
            this.rBSoldadura = new System.Windows.Forms.RadioButton();
            this.rBChapa = new System.Windows.Forms.RadioButton();
            this.rBAngular = new System.Windows.Forms.RadioButton();
            this.tlpGraficasMaquinas = new System.Windows.Forms.TableLayoutPanel();
            this.sCGrids = new System.Windows.Forms.SplitContainer();
            this.dGVRealizables = new System.Windows.Forms.DataGridView();
            this.dGvNoRealizables = new System.Windows.Forms.DataGridView();
            this.cmsGraficas = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.recargarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.sCPrincipal)).BeginInit();
            this.sCPrincipal.Panel1.SuspendLayout();
            this.sCPrincipal.Panel2.SuspendLayout();
            this.sCPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sCOpciones)).BeginInit();
            this.sCOpciones.Panel1.SuspendLayout();
            this.sCOpciones.Panel2.SuspendLayout();
            this.sCOpciones.SuspendLayout();
            this.panelOpciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sCGrids)).BeginInit();
            this.sCGrids.Panel1.SuspendLayout();
            this.sCGrids.Panel2.SuspendLayout();
            this.sCGrids.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGVRealizables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGvNoRealizables)).BeginInit();
            this.cmsGraficas.SuspendLayout();
            this.SuspendLayout();
            // 
            // sCPrincipal
            // 
            this.sCPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sCPrincipal.Location = new System.Drawing.Point(0, 0);
            this.sCPrincipal.Name = "sCPrincipal";
            this.sCPrincipal.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // sCPrincipal.Panel1
            // 
            this.sCPrincipal.Panel1.Controls.Add(this.sCOpciones);
            // 
            // sCPrincipal.Panel2
            // 
            this.sCPrincipal.Panel2.Controls.Add(this.sCGrids);
            this.sCPrincipal.Panel2Collapsed = true;
            this.sCPrincipal.Size = new System.Drawing.Size(1264, 862);
            this.sCPrincipal.SplitterDistance = 672;
            this.sCPrincipal.SplitterWidth = 5;
            this.sCPrincipal.TabIndex = 0;
            // 
            // sCOpciones
            // 
            this.sCOpciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sCOpciones.Location = new System.Drawing.Point(0, 0);
            this.sCOpciones.Margin = new System.Windows.Forms.Padding(2);
            this.sCOpciones.Name = "sCOpciones";
            this.sCOpciones.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // sCOpciones.Panel1
            // 
            this.sCOpciones.Panel1.Controls.Add(this.panelOpciones);
            // 
            // sCOpciones.Panel2
            // 
            this.sCOpciones.Panel2.Controls.Add(this.tlpGraficasMaquinas);
            this.sCOpciones.Size = new System.Drawing.Size(1264, 862);
            this.sCOpciones.SplitterDistance = 28;
            this.sCOpciones.SplitterWidth = 3;
            this.sCOpciones.TabIndex = 3;
            // 
            // panelOpciones
            // 
            this.panelOpciones.Controls.Add(this.label1);
            this.panelOpciones.Controls.Add(this.btnExportarExcel);
            this.panelOpciones.Controls.Add(this.comboEMpresaMaq);
            this.panelOpciones.Controls.Add(this.rbAcabado);
            this.panelOpciones.Controls.Add(this.cBAux);
            this.panelOpciones.Controls.Add(this.rBAux);
            this.panelOpciones.Controls.Add(this.rBSoldadura);
            this.panelOpciones.Controls.Add(this.rBChapa);
            this.panelOpciones.Controls.Add(this.rBAngular);
            this.panelOpciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOpciones.Location = new System.Drawing.Point(0, 0);
            this.panelOpciones.Margin = new System.Windows.Forms.Padding(2);
            this.panelOpciones.Name = "panelOpciones";
            this.panelOpciones.Size = new System.Drawing.Size(1264, 28);
            this.panelOpciones.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(864, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Máquinas: ";
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.Location = new System.Drawing.Point(1116, 3);
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(131, 23);
            this.btnExportarExcel.TabIndex = 7;
            this.btnExportarExcel.Text = "Exportar a Excel";
            this.btnExportarExcel.UseVisualStyleBackColor = true;
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // comboEMpresaMaq
            // 
            this.comboEMpresaMaq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboEMpresaMaq.FormattingEnabled = true;
            this.comboEMpresaMaq.Items.AddRange(new object[] {
            "Todas",
            "Imedexsa",
            "Made"});
            this.comboEMpresaMaq.Location = new System.Drawing.Point(929, 3);
            this.comboEMpresaMaq.Name = "comboEMpresaMaq";
            this.comboEMpresaMaq.Size = new System.Drawing.Size(162, 21);
            this.comboEMpresaMaq.TabIndex = 6;
            // 
            // rbAcabado
            // 
            this.rbAcabado.AutoSize = true;
            this.rbAcabado.Location = new System.Drawing.Point(500, 8);
            this.rbAcabado.Margin = new System.Windows.Forms.Padding(2);
            this.rbAcabado.Name = "rbAcabado";
            this.rbAcabado.Size = new System.Drawing.Size(120, 17);
            this.rbAcabado.TabIndex = 5;
            this.rbAcabado.Text = "Acabado Superficial";
            this.rbAcabado.UseVisualStyleBackColor = true;
            this.rbAcabado.CheckedChanged += new System.EventHandler(this.rbAcabado_CheckedChanged);
            // 
            // cBAux
            // 
            this.cBAux.FormattingEnabled = true;
            this.cBAux.Items.AddRange(new object[] {
            "Angulares",
            "Chapas",
            "Ambos"});
            this.cBAux.Location = new System.Drawing.Point(660, 5);
            this.cBAux.Margin = new System.Windows.Forms.Padding(2);
            this.cBAux.Name = "cBAux";
            this.cBAux.Size = new System.Drawing.Size(134, 21);
            this.cBAux.TabIndex = 4;
            this.cBAux.Visible = false;
            this.cBAux.SelectedIndexChanged += new System.EventHandler(this.cBAux_SelectedIndexChanged);
            // 
            // rBAux
            // 
            this.rBAux.AutoSize = true;
            this.rBAux.Location = new System.Drawing.Point(352, 8);
            this.rBAux.Margin = new System.Windows.Forms.Padding(2);
            this.rBAux.Name = "rBAux";
            this.rBAux.Size = new System.Drawing.Size(132, 17);
            this.rBAux.TabIndex = 3;
            this.rBAux.Text = "Operaciones Auxiliares";
            this.rBAux.UseVisualStyleBackColor = true;
            this.rBAux.CheckedChanged += new System.EventHandler(this.rBAux_CheckedChanged);
            // 
            // rBSoldadura
            // 
            this.rBSoldadura.AutoSize = true;
            this.rBSoldadura.Location = new System.Drawing.Point(262, 8);
            this.rBSoldadura.Margin = new System.Windows.Forms.Padding(2);
            this.rBSoldadura.Name = "rBSoldadura";
            this.rBSoldadura.Size = new System.Drawing.Size(73, 17);
            this.rBSoldadura.TabIndex = 2;
            this.rBSoldadura.Text = "Soldadura";
            this.rBSoldadura.UseVisualStyleBackColor = true;
            this.rBSoldadura.CheckedChanged += new System.EventHandler(this.rBSoldadura_CheckedChanged);
            // 
            // rBChapa
            // 
            this.rBChapa.AutoSize = true;
            this.rBChapa.Location = new System.Drawing.Point(154, 8);
            this.rBChapa.Margin = new System.Windows.Forms.Padding(2);
            this.rBChapa.Name = "rBChapa";
            this.rBChapa.Size = new System.Drawing.Size(99, 17);
            this.rBChapa.TabIndex = 1;
            this.rBChapa.Text = "Chapa / Pletina";
            this.rBChapa.UseVisualStyleBackColor = true;
            this.rBChapa.CheckedChanged += new System.EventHandler(this.rBChapa_CheckedChanged);
            // 
            // rBAngular
            // 
            this.rBAngular.AutoSize = true;
            this.rBAngular.Checked = true;
            this.rBAngular.Location = new System.Drawing.Point(28, 8);
            this.rBAngular.Margin = new System.Windows.Forms.Padding(2);
            this.rBAngular.Name = "rBAngular";
            this.rBAngular.Size = new System.Drawing.Size(116, 17);
            this.rBAngular.TabIndex = 0;
            this.rBAngular.TabStop = true;
            this.rBAngular.Text = "Angular/Estructural";
            this.rBAngular.UseVisualStyleBackColor = true;
            this.rBAngular.CheckedChanged += new System.EventHandler(this.rBAngular_CheckedChanged);
            // 
            // tlpGraficasMaquinas
            // 
            this.tlpGraficasMaquinas.AutoSize = true;
            this.tlpGraficasMaquinas.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tlpGraficasMaquinas.ColumnCount = 1;
            this.tlpGraficasMaquinas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpGraficasMaquinas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpGraficasMaquinas.Location = new System.Drawing.Point(0, 0);
            this.tlpGraficasMaquinas.Name = "tlpGraficasMaquinas";
            this.tlpGraficasMaquinas.RowCount = 1;
            this.tlpGraficasMaquinas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpGraficasMaquinas.Size = new System.Drawing.Size(1264, 831);
            this.tlpGraficasMaquinas.TabIndex = 2;
            // 
            // sCGrids
            // 
            this.sCGrids.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sCGrids.Location = new System.Drawing.Point(0, 0);
            this.sCGrids.Name = "sCGrids";
            // 
            // sCGrids.Panel1
            // 
            this.sCGrids.Panel1.Controls.Add(this.dGVRealizables);
            // 
            // sCGrids.Panel2
            // 
            this.sCGrids.Panel2.Controls.Add(this.dGvNoRealizables);
            this.sCGrids.Size = new System.Drawing.Size(150, 46);
            this.sCGrids.SplitterDistance = 75;
            this.sCGrids.TabIndex = 1;
            // 
            // dGVRealizables
            // 
            this.dGVRealizables.AllowUserToAddRows = false;
            this.dGVRealizables.AllowUserToDeleteRows = false;
            this.dGVRealizables.AllowUserToResizeRows = false;
            this.dGVRealizables.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.CornflowerBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dGVRealizables.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dGVRealizables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVRealizables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dGVRealizables.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dGVRealizables.EnableHeadersVisualStyles = false;
            this.dGVRealizables.Location = new System.Drawing.Point(0, 0);
            this.dGVRealizables.Name = "dGVRealizables";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.CornflowerBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dGVRealizables.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dGVRealizables.RowHeadersVisible = false;
            this.dGVRealizables.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dGVRealizables.Size = new System.Drawing.Size(75, 46);
            this.dGVRealizables.TabIndex = 0;
            this.dGVRealizables.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dGVRealizables_CellFormatting);
            // 
            // dGvNoRealizables
            // 
            this.dGvNoRealizables.AllowUserToAddRows = false;
            this.dGvNoRealizables.AllowUserToDeleteRows = false;
            this.dGvNoRealizables.AllowUserToResizeRows = false;
            this.dGvNoRealizables.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dGvNoRealizables.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dGvNoRealizables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGvNoRealizables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dGvNoRealizables.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dGvNoRealizables.EnableHeadersVisualStyles = false;
            this.dGvNoRealizables.Location = new System.Drawing.Point(0, 0);
            this.dGvNoRealizables.Name = "dGvNoRealizables";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dGvNoRealizables.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dGvNoRealizables.RowHeadersVisible = false;
            this.dGvNoRealizables.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dGvNoRealizables.Size = new System.Drawing.Size(71, 46);
            this.dGvNoRealizables.TabIndex = 1;
            // 
            // cmsGraficas
            // 
            this.cmsGraficas.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.recargarToolStripMenuItem});
            this.cmsGraficas.Name = "cmsGraficas";
            this.cmsGraficas.Size = new System.Drawing.Size(121, 26);
            // 
            // recargarToolStripMenuItem
            // 
            this.recargarToolStripMenuItem.Name = "recargarToolStripMenuItem";
            this.recargarToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.recargarToolStripMenuItem.Text = "Recargar";
            this.recargarToolStripMenuItem.Click += new System.EventHandler(this.recargarToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 50000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // EstadoTallerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 862);
            this.Controls.Add(this.sCPrincipal);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 599);
            this.Name = "EstadoTallerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Estado del Taller ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EstadoTallerForm_FormClosing);
            this.sCPrincipal.Panel1.ResumeLayout(false);
            this.sCPrincipal.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sCPrincipal)).EndInit();
            this.sCPrincipal.ResumeLayout(false);
            this.sCOpciones.Panel1.ResumeLayout(false);
            this.sCOpciones.Panel2.ResumeLayout(false);
            this.sCOpciones.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sCOpciones)).EndInit();
            this.sCOpciones.ResumeLayout(false);
            this.panelOpciones.ResumeLayout(false);
            this.panelOpciones.PerformLayout();
            this.sCGrids.Panel1.ResumeLayout(false);
            this.sCGrids.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sCGrids)).EndInit();
            this.sCGrids.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dGVRealizables)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGvNoRealizables)).EndInit();
            this.cmsGraficas.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer sCPrincipal;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridView dGVRealizables;
        private System.Windows.Forms.ContextMenuStrip cmsGraficas;
        private System.Windows.Forms.ToolStripMenuItem recargarToolStripMenuItem;
        private System.Windows.Forms.SplitContainer sCGrids;
        private System.Windows.Forms.DataGridView dGvNoRealizables;
        private System.Windows.Forms.SplitContainer sCOpciones;
        private System.Windows.Forms.Panel panelOpciones;
        private System.Windows.Forms.TableLayoutPanel tlpGraficasMaquinas;
        private System.Windows.Forms.RadioButton rBAux;
        private System.Windows.Forms.RadioButton rBSoldadura;
        private System.Windows.Forms.RadioButton rBChapa;
        private System.Windows.Forms.RadioButton rBAngular;
        private System.Windows.Forms.ComboBox cBAux;
        private System.Windows.Forms.RadioButton rbAcabado;
        private System.Windows.Forms.ComboBox comboEMpresaMaq;
        private System.Windows.Forms.Button btnExportarExcel;
        private System.Windows.Forms.Label label1;


    }
}