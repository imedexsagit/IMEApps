namespace ImeApps
{

    using System;
    using System.ComponentModel;
    using System.Drawing;
    //using System.Net;
    using System.Configuration;
    using System.Windows.Forms;

    partial class FormElegirEmpresas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormElegirEmpresas));
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.button_Ime = new System.Windows.Forms.Button();
            this.button_Made = new System.Windows.Forms.Button();
            this.button_oldMade = new System.Windows.Forms.Button();
            this.SuspendLayout();
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
            // button_Ime
            // 
            this.button_Ime.Image = ((System.Drawing.Image)(resources.GetObject("button_Ime.Image")));
            this.button_Ime.Location = new System.Drawing.Point(15, 38);
            this.button_Ime.Name = "button_Ime";
            this.button_Ime.Size = new System.Drawing.Size(192, 210);
            this.button_Ime.TabIndex = 3;
            this.button_Ime.UseVisualStyleBackColor = true;
            this.button_Ime.Click += new System.EventHandler(this.button_Ime_Click);
            // 
            // button_Made
            // 
            this.button_Made.Image = ((System.Drawing.Image)(resources.GetObject("button_Made.Image")));
            this.button_Made.Location = new System.Drawing.Point(230, 38);
            this.button_Made.Name = "button_Made";
            this.button_Made.Size = new System.Drawing.Size(192, 210);
            this.button_Made.TabIndex = 4;
            this.button_Made.UseVisualStyleBackColor = true;
            this.button_Made.Click += new System.EventHandler(this.button_Made_Click);
            // 
            // button_oldMade
            // 
            this.button_oldMade.Image = ((System.Drawing.Image)(resources.GetObject("button_oldMade.Image")));
            this.button_oldMade.Location = new System.Drawing.Point(446, 38);
            this.button_oldMade.Name = "button_oldMade";
            this.button_oldMade.Size = new System.Drawing.Size(192, 210);
            this.button_oldMade.TabIndex = 5;
            this.button_oldMade.UseVisualStyleBackColor = true;
            this.button_oldMade.Click += new System.EventHandler(this.button_oldMade_Click);
            // 
            // FormElegirEmpresas
            // 
            this.ClientSize = new System.Drawing.Size(654, 263);
            this.Controls.Add(this.button_oldMade);
            this.Controls.Add(this.button_Made);
            this.Controls.Add(this.button_Ime);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormElegirEmpresas";
            this.Text = "Empresas";
            this.Load += new System.EventHandler(this.FormElegirEmpresas_Load);
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
        private Label label1;
        private ContextMenuStrip contextMenuStrip1;
        private Button button_Ime;
        private Button button_Made;
        private Button button_oldMade;

    }
}