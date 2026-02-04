namespace ImeApps
{

    using System;
    using System.ComponentModel;
    using System.Drawing;
    //using System.Net;
    using System.Configuration;
    using System.Windows.Forms;

    partial class FormEmpresas
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
        /// 

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.cerrarVentana();
        }

        public void cerrarVentana()
        {
            base.Close();
        }

        /*private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.listadoEmpresasUC1.seleccionarEmpresa();
        }*/


        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button3 = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.EmpresasDataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.EmpresasDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(607, 246);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 0;
            this.button3.Text = "Aceptar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Location = new System.Drawing.Point(526, 246);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(237, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Seleccione la empresa con la que desea trabajar";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // EmpresasDataGridView
            // 
            this.EmpresasDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EmpresasDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.EmpresasDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.EmpresasDataGridView.Location = new System.Drawing.Point(15, 29);
            this.EmpresasDataGridView.Name = "EmpresasDataGridView";
            this.EmpresasDataGridView.Size = new System.Drawing.Size(667, 211);
            this.EmpresasDataGridView.TabIndex = 4;
            this.EmpresasDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.EmpresasDataGridView_CellContentClick);
            this.EmpresasDataGridView.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.EmpresasDataGridView_CellContentDoubleClick);
            this.EmpresasDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.EmpresasDataGridView_CellContentDoubleClick);
            // 
            // FormEmpresas
            // 
            this.ClientSize = new System.Drawing.Size(694, 281);
            this.Controls.Add(this.EmpresasDataGridView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.button3);
            this.Name = "FormEmpresas";
            this.Text = "Empresas";
            this.Load += new System.EventHandler(this.FormEmpresas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.EmpresasDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        // 
        // btnCancelar
        // 
        //this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        /* this.btnCancelar.Location = new System.Drawing.Point(103, 59);
         //this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         //this.btnCancelar.Location = new System.Drawing.Point(394, 264);
         this.btnCancelar.Name = "btnCancelar";
         this.btnCancelar.Size = new System.Drawing.Size(75, 23);
         this.btnCancelar.TabIndex = 0;
         this.btnCancelar.Text = "&Cancelar";
         this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);*/
        // 
        // btnAceptar
        // 
        /*this.btnAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.btnAceptar.Location = new System.Drawing.Point(294, 264);
        this.btnAceptar.Name = "btnAceptar";
        this.btnAceptar.Size = new System.Drawing.Size(75, 23);
        this.btnAceptar.TabIndex = 1;
        this.btnAceptar.Text = "&Aceptar";
        this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);*/


        #endregion

        //private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnCancelar;
        private Label label1;
        private ContextMenuStrip contextMenuStrip1;
        private DataGridView EmpresasDataGridView;

    }
}