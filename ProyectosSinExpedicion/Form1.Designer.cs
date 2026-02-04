namespace ProyectosSinExpedicion
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lb_titulo = new System.Windows.Forms.Label();
            this.tboxProyecto = new System.Windows.Forms.TextBox();
            this.btn_Consultar_Modificar = new System.Windows.Forms.Button();
            this.dgProyectosSinExpedicion = new System.Windows.Forms.DataGridView();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgProyectosSinExpedicion)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lb_titulo);
            this.groupBox2.Controls.Add(this.tboxProyecto);
            this.groupBox2.Controls.Add(this.btn_Consultar_Modificar);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1488, 93);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filtros";
            // 
            // lb_titulo
            // 
            this.lb_titulo.AutoSize = true;
            this.lb_titulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_titulo.Location = new System.Drawing.Point(8, 32);
            this.lb_titulo.Name = "lb_titulo";
            this.lb_titulo.Size = new System.Drawing.Size(65, 16);
            this.lb_titulo.TabIndex = 0;
            this.lb_titulo.Text = "Proyecto:";
            // 
            // tboxProyecto
            // 
            this.tboxProyecto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tboxProyecto.Location = new System.Drawing.Point(11, 51);
            this.tboxProyecto.Name = "tboxProyecto";
            this.tboxProyecto.Size = new System.Drawing.Size(325, 22);
            this.tboxProyecto.TabIndex = 1;
            // 
            // btn_Consultar_Modificar
            // 
            this.btn_Consultar_Modificar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Consultar_Modificar.Location = new System.Drawing.Point(387, 33);
            this.btn_Consultar_Modificar.Name = "btn_Consultar_Modificar";
            this.btn_Consultar_Modificar.Size = new System.Drawing.Size(218, 40);
            this.btn_Consultar_Modificar.TabIndex = 2;
            this.btn_Consultar_Modificar.Text = "Consultar";
            this.btn_Consultar_Modificar.UseVisualStyleBackColor = true;
            this.btn_Consultar_Modificar.Click += new System.EventHandler(this.btn_Consultar_Modificar_Click);
            // 
            // dgProyectosSinExpedicion
            // 
            this.dgProyectosSinExpedicion.AllowUserToAddRows = false;
            this.dgProyectosSinExpedicion.AllowUserToDeleteRows = false;
            this.dgProyectosSinExpedicion.AllowUserToResizeColumns = false;
            this.dgProyectosSinExpedicion.AllowUserToResizeRows = false;
            this.dgProyectosSinExpedicion.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgProyectosSinExpedicion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgProyectosSinExpedicion.EnableHeadersVisualStyles = false;
            this.dgProyectosSinExpedicion.Location = new System.Drawing.Point(14, 117);
            this.dgProyectosSinExpedicion.Name = "dgProyectosSinExpedicion";
            this.dgProyectosSinExpedicion.Size = new System.Drawing.Size(1486, 533);
            this.dgProyectosSinExpedicion.TabIndex = 23;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1516, 662);
            this.Controls.Add(this.dgProyectosSinExpedicion);
            this.Controls.Add(this.groupBox2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Proyectos sin expedición";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgProyectosSinExpedicion)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lb_titulo;
        private System.Windows.Forms.TextBox tboxProyecto;
        private System.Windows.Forms.Button btn_Consultar_Modificar;
        private System.Windows.Forms.DataGridView dgProyectosSinExpedicion;
    }
}

