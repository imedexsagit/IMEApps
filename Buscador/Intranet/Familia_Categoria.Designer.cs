namespace Intranet
{
    partial class Familia_Categoria
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
            this.checkedListBox_familia_categoria = new System.Windows.Forms.CheckedListBox();
            this.button_cancelar = new System.Windows.Forms.Button();
            this.button_aceptar = new System.Windows.Forms.Button();
            this.t_FAMILIASTableAdapter = new dsFamiliasTableAdapters.T_FAMILIASTableAdapter();
            this.t_CATEGORIASTableAdapter = new dsCategoriasTableAdapters.T_CATEGORIASTableAdapter();
            this.dsFamilias = new dsFamilias();
            this.dsCategorias = new dsCategorias();
            this.dsLugar1 = new dsLugar();
            this.t_ExpedicionesTableAdapter1 = new dsLugarTableAdapters.T_ExpedicionesTableAdapter();
            this.dsRepres1 = new dsRepres();
            this.t_REPRESTableAdapter1 = new dsRepresTableAdapters.T_REPRESTableAdapter();
            this.t_CLIENTESTableAdapter1 = new dsClientesTableAdapters.T_CLIENTESTableAdapter();
            this.dsClientes1 = new dsClientes();
            this.textFiltroLista = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dsFamilias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCategorias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsLugar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsRepres1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsClientes1)).BeginInit();
            this.SuspendLayout();
            // 
            // checkedListBox_familia_categoria
            // 
            this.checkedListBox_familia_categoria.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListBox_familia_categoria.FormattingEnabled = true;
            this.checkedListBox_familia_categoria.HorizontalScrollbar = true;
            this.checkedListBox_familia_categoria.Location = new System.Drawing.Point(12, 37);
            this.checkedListBox_familia_categoria.Name = "checkedListBox_familia_categoria";
            this.checkedListBox_familia_categoria.Size = new System.Drawing.Size(399, 429);
            this.checkedListBox_familia_categoria.TabIndex = 0;
            this.checkedListBox_familia_categoria.SelectedIndexChanged += new System.EventHandler(this.checkedListBox_familia_categoria_SelectedIndexChanged);
            // 
            // button_cancelar
            // 
            this.button_cancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_cancelar.Location = new System.Drawing.Point(12, 481);
            this.button_cancelar.Name = "button_cancelar";
            this.button_cancelar.Size = new System.Drawing.Size(82, 34);
            this.button_cancelar.TabIndex = 1;
            this.button_cancelar.Text = "CANCELAR";
            this.button_cancelar.UseVisualStyleBackColor = true;
            this.button_cancelar.Click += new System.EventHandler(this.button_cancelar_Click);
            // 
            // button_aceptar
            // 
            this.button_aceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_aceptar.Location = new System.Drawing.Point(329, 481);
            this.button_aceptar.Name = "button_aceptar";
            this.button_aceptar.Size = new System.Drawing.Size(82, 34);
            this.button_aceptar.TabIndex = 2;
            this.button_aceptar.Text = "ACEPTAR";
            this.button_aceptar.UseVisualStyleBackColor = true;
            this.button_aceptar.Click += new System.EventHandler(this.button_aceptar_Click);
            // 
            // t_FAMILIASTableAdapter
            // 
            this.t_FAMILIASTableAdapter.ClearBeforeFill = true;
            // 
            // t_CATEGORIASTableAdapter
            // 
            this.t_CATEGORIASTableAdapter.ClearBeforeFill = true;
            // 
            // dsFamilias
            // 
            this.dsFamilias.DataSetName = "dsFamilias";
            this.dsFamilias.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dsCategorias
            // 
            this.dsCategorias.DataSetName = "dsCategorias";
            this.dsCategorias.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dsLugar1
            // 
            this.dsLugar1.DataSetName = "dsLugar";
            this.dsLugar1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // t_ExpedicionesTableAdapter1
            // 
            this.t_ExpedicionesTableAdapter1.ClearBeforeFill = true;
            // 
            // dsRepres1
            // 
            this.dsRepres1.DataSetName = "dsRepres";
            this.dsRepres1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // t_REPRESTableAdapter1
            // 
            this.t_REPRESTableAdapter1.ClearBeforeFill = true;
            // 
            // t_CLIENTESTableAdapter1
            // 
            this.t_CLIENTESTableAdapter1.ClearBeforeFill = true;
            // 
            // dsClientes1
            // 
            this.dsClientes1.DataSetName = "dsClientes";
            this.dsClientes1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // textFiltroLista
            // 
            this.textFiltroLista.Location = new System.Drawing.Point(12, 7);
            this.textFiltroLista.Name = "textFiltroLista";
            this.textFiltroLista.Size = new System.Drawing.Size(399, 20);
            this.textFiltroLista.TabIndex = 3;
            this.textFiltroLista.TextChanged += new System.EventHandler(this.textFiltroLista_TextChanged);
            // 
            // Familia_Categoria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 523);
            this.Controls.Add(this.textFiltroLista);
            this.Controls.Add(this.button_aceptar);
            this.Controls.Add(this.button_cancelar);
            this.Controls.Add(this.checkedListBox_familia_categoria);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Familia_Categoria";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Familia_Categoria";
            ((System.ComponentModel.ISupportInitialize)(this.dsFamilias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCategorias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsLugar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsRepres1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsClientes1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox_familia_categoria;
        private System.Windows.Forms.Button button_cancelar;
        private System.Windows.Forms.Button button_aceptar;
        private dsFamiliasTableAdapters.T_FAMILIASTableAdapter t_FAMILIASTableAdapter;
        private dsCategoriasTableAdapters.T_CATEGORIASTableAdapter t_CATEGORIASTableAdapter;
        private dsFamilias dsFamilias;
        private dsCategorias dsCategorias;
        private dsRepres dsRepres1;
        private dsRepresTableAdapters.T_REPRESTableAdapter t_REPRESTableAdapter1;
        private dsLugar dsLugar1;
        private dsLugarTableAdapters.T_ExpedicionesTableAdapter t_ExpedicionesTableAdapter1;
        private dsClientesTableAdapters.T_CLIENTESTableAdapter t_CLIENTESTableAdapter1;
        private dsClientes dsClientes1;
        private System.Windows.Forms.TextBox textFiltroLista;
    }
}