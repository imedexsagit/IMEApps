namespace Intranet
{
    partial class CrearCodigo
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
            this.btnCrear = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.txtDenominacion = new System.Windows.Forms.TextBox();
            this.cmbFamilia = new System.Windows.Forms.ComboBox();
            this.tFAMILIASBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsFamilias = new dsFamilias();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDenominacion_en = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDenominacion_fr = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDenominacion_de = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.t_FAMILIASTableAdapter = new dsFamiliasTableAdapters.T_FAMILIASTableAdapter();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tCATEGORIASBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsCategoriasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsCategorias = new dsCategorias();
            this.t_CATEGORIASTableAdapter = new dsCategoriasTableAdapters.T_CATEGORIASTableAdapter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnTraducir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tFAMILIASBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsFamilias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tCATEGORIASBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCategoriasBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCategorias)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCrear
            // 
            this.btnCrear.Location = new System.Drawing.Point(171, 527);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(86, 36);
            this.btnCrear.TabIndex = 5;
            this.btnCrear.Text = "Crear";
            this.btnCrear.UseVisualStyleBackColor = true;
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(407, 527);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(86, 36);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(86, 21);
            this.txtCodigo.MaxLength = 20;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.ReadOnly = true;
            this.txtCodigo.Size = new System.Drawing.Size(138, 20);
            this.txtCodigo.TabIndex = 1;
            // 
            // txtDenominacion
            // 
            this.txtDenominacion.Location = new System.Drawing.Point(87, 47);
            this.txtDenominacion.MaxLength = 255;
            this.txtDenominacion.Multiline = true;
            this.txtDenominacion.Name = "txtDenominacion";
            this.txtDenominacion.Size = new System.Drawing.Size(591, 59);
            this.txtDenominacion.TabIndex = 2;
            // 
            // cmbFamilia
            // 
            this.cmbFamilia.DataSource = this.tFAMILIASBindingSource;
            this.cmbFamilia.DisplayMember = "DENOMI";
            this.cmbFamilia.FormattingEnabled = true;
            this.cmbFamilia.Location = new System.Drawing.Point(90, 12);
            this.cmbFamilia.Name = "cmbFamilia";
            this.cmbFamilia.Size = new System.Drawing.Size(121, 21);
            this.cmbFamilia.TabIndex = 3;
            this.cmbFamilia.ValueMember = "FAMILIA";
            // 
            // tFAMILIASBindingSource
            // 
            this.tFAMILIASBindingSource.DataMember = "T_FAMILIAS";
            this.tFAMILIASBindingSource.DataSource = this.dsFamilias;
            // 
            // dsFamilias
            // 
            this.dsFamilias.DataSetName = "dsFamilias";
            this.dsFamilias.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Código";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Denominación";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Familia";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Categoría";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 169);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Deno. Inglés";
            // 
            // txtDenominacion_en
            // 
            this.txtDenominacion_en.Location = new System.Drawing.Point(88, 149);
            this.txtDenominacion_en.MaxLength = 255;
            this.txtDenominacion_en.Multiline = true;
            this.txtDenominacion_en.Name = "txtDenominacion_en";
            this.txtDenominacion_en.Size = new System.Drawing.Size(590, 55);
            this.txtDenominacion_en.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 239);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Deno. Francés";
            // 
            // txtDenominacion_fr
            // 
            this.txtDenominacion_fr.Location = new System.Drawing.Point(88, 217);
            this.txtDenominacion_fr.MaxLength = 255;
            this.txtDenominacion_fr.Multiline = true;
            this.txtDenominacion_fr.Name = "txtDenominacion_fr";
            this.txtDenominacion_fr.Size = new System.Drawing.Size(590, 57);
            this.txtDenominacion_fr.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 306);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Deno. Alemán";
            // 
            // txtDenominacion_de
            // 
            this.txtDenominacion_de.Location = new System.Drawing.Point(88, 287);
            this.txtDenominacion_de.MaxLength = 255;
            this.txtDenominacion_de.Multiline = true;
            this.txtDenominacion_de.Name = "txtDenominacion_de";
            this.txtDenominacion_de.Size = new System.Drawing.Size(590, 54);
            this.txtDenominacion_de.TabIndex = 16;
            // 
            // textBox4
            // 
            this.textBox4.Enabled = false;
            this.textBox4.Location = new System.Drawing.Point(90, 65);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(122, 20);
            this.textBox4.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 65);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Peso";
            // 
            // textBox5
            // 
            this.textBox5.Enabled = false;
            this.textBox5.Location = new System.Drawing.Point(90, 91);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(122, 20);
            this.textBox5.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 91);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Longitud";
            // 
            // textBox6
            // 
            this.textBox6.Enabled = false;
            this.textBox6.Location = new System.Drawing.Point(90, 117);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(122, 20);
            this.textBox6.TabIndex = 23;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 117);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Area";
            // 
            // t_FAMILIASTableAdapter
            // 
            this.t_FAMILIASTableAdapter.ClearBeforeFill = true;
            // 
            // comboBox1
            // 
            this.comboBox1.DataSource = this.tCATEGORIASBindingSource;
            this.comboBox1.DisplayMember = "DENOMI";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(90, 38);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 24;
            this.comboBox1.ValueMember = "CATEGO";
            // 
            // tCATEGORIASBindingSource
            // 
            this.tCATEGORIASBindingSource.DataMember = "T_CATEGORIAS";
            this.tCATEGORIASBindingSource.DataSource = this.dsCategoriasBindingSource;
            // 
            // dsCategoriasBindingSource
            // 
            this.dsCategoriasBindingSource.DataSource = this.dsCategorias;
            this.dsCategoriasBindingSource.Position = 0;
            // 
            // dsCategorias
            // 
            this.dsCategorias.DataSetName = "dsCategorias";
            this.dsCategorias.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // t_CATEGORIASTableAdapter
            // 
            this.t_CATEGORIASTableAdapter.ClearBeforeFill = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cmbFamilia);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.textBox6);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.textBox4);
            this.panel1.Controls.Add(this.textBox5);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Location = new System.Drawing.Point(445, 169);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(233, 151);
            this.panel1.TabIndex = 26;
            this.panel1.Visible = false;
            // 
            // btnTraducir
            // 
            this.btnTraducir.Location = new System.Drawing.Point(232, 120);
            this.btnTraducir.Name = "btnTraducir";
            this.btnTraducir.Size = new System.Drawing.Size(219, 23);
            this.btnTraducir.TabIndex = 27;
            this.btnTraducir.Text = "Traducir Automáticamente";
            this.btnTraducir.UseVisualStyleBackColor = true;
            this.btnTraducir.Click += new System.EventHandler(this.btnTraducir_Click);
            // 
            // CrearCodigo
            // 
            this.AcceptButton = this.btnCrear;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(690, 583);
            this.ControlBox = false;
            this.Controls.Add(this.btnTraducir);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtDenominacion_de);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtDenominacion_fr);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtDenominacion_en);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDenominacion);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnCrear);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CrearCodigo";
            this.Text = "Crear Código";
            this.Load += new System.EventHandler(this.CrearCodigo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tFAMILIASBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsFamilias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tCATEGORIASBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCategoriasBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCategorias)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.TextBox txtDenominacion;
        private System.Windows.Forms.ComboBox cmbFamilia;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDenominacion_en;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDenominacion_fr;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDenominacion_de;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label10;
        private dsFamilias dsFamilias;
        private System.Windows.Forms.BindingSource tFAMILIASBindingSource;
        private dsFamiliasTableAdapters.T_FAMILIASTableAdapter t_FAMILIASTableAdapter;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.BindingSource dsCategoriasBindingSource;
        private dsCategorias dsCategorias;
        private System.Windows.Forms.BindingSource tCATEGORIASBindingSource;
        private dsCategoriasTableAdapters.T_CATEGORIASTableAdapter t_CATEGORIASTableAdapter;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnTraducir;
    }
}