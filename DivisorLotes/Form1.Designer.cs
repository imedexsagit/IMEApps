namespace DivisorLotes
{
    partial class DivisorLotes
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
            this.BTN_DIVIDIR = new System.Windows.Forms.Button();
            this.combo_almacen = new System.Windows.Forms.ComboBox();
            this.combo_destino = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxLote = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BTN_DIVIDIR
            // 
            this.BTN_DIVIDIR.Location = new System.Drawing.Point(87, 226);
            this.BTN_DIVIDIR.Name = "BTN_DIVIDIR";
            this.BTN_DIVIDIR.Size = new System.Drawing.Size(111, 23);
            this.BTN_DIVIDIR.TabIndex = 0;
            this.BTN_DIVIDIR.Text = "DIVIDIR LOTE";
            this.BTN_DIVIDIR.UseVisualStyleBackColor = true;
            this.BTN_DIVIDIR.Click += new System.EventHandler(this.BTN_DIVIDIR_Click);
            // 
            // combo_almacen
            // 
            this.combo_almacen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_almacen.FormattingEnabled = true;
            this.combo_almacen.Location = new System.Drawing.Point(34, 137);
            this.combo_almacen.Name = "combo_almacen";
            this.combo_almacen.Size = new System.Drawing.Size(218, 21);
            this.combo_almacen.TabIndex = 1;
            this.combo_almacen.SelectedIndexChanged += new System.EventHandler(this.combo_almacen_SelectedIndexChanged);
            // 
            // combo_destino
            // 
            this.combo_destino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_destino.FormattingEnabled = true;
            this.combo_destino.Location = new System.Drawing.Point(34, 177);
            this.combo_destino.Name = "combo_destino";
            this.combo_destino.Size = new System.Drawing.Size(218, 21);
            this.combo_destino.TabIndex = 2;
            this.combo_destino.SelectedIndexChanged += new System.EventHandler(this.combo_destino_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "ALMACÉN ORIGEN";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 161);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "ALMACÉN DESTINO";
            // 
            // textBoxLote
            // 
            this.textBoxLote.Location = new System.Drawing.Point(34, 32);
            this.textBoxLote.Name = "textBoxLote";
            this.textBoxLote.Size = new System.Drawing.Size(100, 20);
            this.textBoxLote.TabIndex = 5;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(34, 77);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "LOTE";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "PESO";
            // 
            // DivisorLotes
            // 
            this.AccessibleName = "";
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBoxLote);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.combo_destino);
            this.Controls.Add(this.combo_almacen);
            this.Controls.Add(this.BTN_DIVIDIR);
            this.Name = "DivisorLotes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DIVISOR LOTES";
            this.Load += new System.EventHandler(this.DivisorLotes_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BTN_DIVIDIR;
        private System.Windows.Forms.ComboBox combo_almacen;
        private System.Windows.Forms.ComboBox combo_destino;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxLote;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;

        

    }
}

