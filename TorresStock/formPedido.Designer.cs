namespace TorresStock
{
    partial class formPedido
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
            this.dgLineasAux = new System.Windows.Forms.DataGridView();
            this.btnAñadirLineas = new System.Windows.Forms.Button();
            this.btnDesmarcar = new System.Windows.Forms.Button();
            this.btnMarcarTodas = new System.Windows.Forms.Button();
            this.CodigoAux = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CantidadAux = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PedidoAux = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LineaAux = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AñadirAux = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgLineasAux)).BeginInit();
            this.SuspendLayout();
            // 
            // dgLineasAux
            // 
            this.dgLineasAux.AllowUserToAddRows = false;
            this.dgLineasAux.AllowUserToDeleteRows = false;
            this.dgLineasAux.AllowUserToResizeColumns = false;
            this.dgLineasAux.AllowUserToResizeRows = false;
            this.dgLineasAux.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgLineasAux.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgLineasAux.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CodigoAux,
            this.CantidadAux,
            this.PedidoAux,
            this.LineaAux,
            this.AñadirAux});
            this.dgLineasAux.Location = new System.Drawing.Point(12, 52);
            this.dgLineasAux.Name = "dgLineasAux";
            this.dgLineasAux.Size = new System.Drawing.Size(716, 458);
            this.dgLineasAux.TabIndex = 0;
            // 
            // btnAñadirLineas
            // 
            this.btnAñadirLineas.Location = new System.Drawing.Point(580, 516);
            this.btnAñadirLineas.Name = "btnAñadirLineas";
            this.btnAñadirLineas.Size = new System.Drawing.Size(148, 25);
            this.btnAñadirLineas.TabIndex = 1;
            this.btnAñadirLineas.Text = "Añadir Lineas";
            this.btnAñadirLineas.UseVisualStyleBackColor = true;
            this.btnAñadirLineas.Click += new System.EventHandler(this.btnAñadirLineas_Click);
            // 
            // btnDesmarcar
            // 
            this.btnDesmarcar.Location = new System.Drawing.Point(406, 21);
            this.btnDesmarcar.Name = "btnDesmarcar";
            this.btnDesmarcar.Size = new System.Drawing.Size(148, 25);
            this.btnDesmarcar.TabIndex = 2;
            this.btnDesmarcar.Text = "Desmarcar Todas";
            this.btnDesmarcar.UseVisualStyleBackColor = true;
            this.btnDesmarcar.Click += new System.EventHandler(this.btnDesmarcar_Click);
            // 
            // btnMarcarTodas
            // 
            this.btnMarcarTodas.Location = new System.Drawing.Point(580, 21);
            this.btnMarcarTodas.Name = "btnMarcarTodas";
            this.btnMarcarTodas.Size = new System.Drawing.Size(148, 25);
            this.btnMarcarTodas.TabIndex = 3;
            this.btnMarcarTodas.Text = "Marcar Todas";
            this.btnMarcarTodas.UseVisualStyleBackColor = true;
            this.btnMarcarTodas.Click += new System.EventHandler(this.btnMarcarTodas_Click);
            // 
            // CodigoAux
            // 
            this.CodigoAux.HeaderText = "Codigo";
            this.CodigoAux.Name = "CodigoAux";
            // 
            // CantidadAux
            // 
            this.CantidadAux.HeaderText = "Cantidad";
            this.CantidadAux.Name = "CantidadAux";
            // 
            // PedidoAux
            // 
            this.PedidoAux.HeaderText = "Pedido";
            this.PedidoAux.Name = "PedidoAux";
            // 
            // LineaAux
            // 
            this.LineaAux.HeaderText = "Linea";
            this.LineaAux.Name = "LineaAux";
            // 
            // AñadirAux
            // 
            this.AñadirAux.HeaderText = "Añadir";
            this.AñadirAux.Name = "AñadirAux";
            // 
            // formPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 553);
            this.Controls.Add(this.btnMarcarTodas);
            this.Controls.Add(this.btnDesmarcar);
            this.Controls.Add(this.btnAñadirLineas);
            this.Controls.Add(this.dgLineasAux);
            this.Name = "formPedido";
            this.Text = "Pedido: ";
            ((System.ComponentModel.ISupportInitialize)(this.dgLineasAux)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgLineasAux;
        private System.Windows.Forms.Button btnAñadirLineas;
        private System.Windows.Forms.Button btnDesmarcar;
        private System.Windows.Forms.Button btnMarcarTodas;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoAux;
        private System.Windows.Forms.DataGridViewTextBoxColumn CantidadAux;
        private System.Windows.Forms.DataGridViewTextBoxColumn PedidoAux;
        private System.Windows.Forms.DataGridViewTextBoxColumn LineaAux;
        private System.Windows.Forms.DataGridViewCheckBoxColumn AñadirAux;
    }
}