namespace EstadoTaller
{
    partial class UCGraficoBarras
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

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar 
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pHorasNoRealizables = new System.Windows.Forms.Panel();
            this.lblHorasNoRealizadas = new System.Windows.Forms.Label();
            this.pHorasRealizables = new System.Windows.Forms.Panel();
            this.lblHorasRealizables = new System.Windows.Forms.Label();
            this.lblPuesto = new System.Windows.Forms.Label();
            this.lblTotalHoras = new System.Windows.Forms.Label();
            this.pHorasInciertas = new System.Windows.Forms.Panel();
            this.lblHorasInciertas = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.pHorasNoRealizables.SuspendLayout();
            this.pHorasRealizables.SuspendLayout();
            this.pHorasInciertas.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pHorasNoRealizables, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.pHorasRealizables, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblPuesto, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblTotalHoras, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.pHorasInciertas, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.99813F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(220, 363);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Click += new System.EventHandler(this.UCGraficoBarras_Click);
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            //
            // JMRM Horas Taller 17/01/2020 
            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;
            // 
            // pHorasNoRealizables
            // 
            this.pHorasNoRealizables.BackColor = System.Drawing.Color.Red;
            this.pHorasNoRealizables.Controls.Add(this.lblHorasNoRealizadas);
            this.pHorasNoRealizables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pHorasNoRealizables.Location = new System.Drawing.Point(3, 108);
            this.pHorasNoRealizables.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.pHorasNoRealizables.Name = "pHorasNoRealizables";
            this.pHorasNoRealizables.Size = new System.Drawing.Size(214, 73);
            this.pHorasNoRealizables.TabIndex = 0;
            this.pHorasNoRealizables.Click += new System.EventHandler(this.UCGraficoBarras_Click);
            // 
            // lblHorasNoRealizadas
            // 
            this.lblHorasNoRealizadas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHorasNoRealizadas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.lblHorasNoRealizadas.Location = new System.Drawing.Point(0, 0);
            this.lblHorasNoRealizadas.Margin = new System.Windows.Forms.Padding(3);
            this.lblHorasNoRealizadas.Name = "lblHorasNoRealizadas";
            this.lblHorasNoRealizadas.Size = new System.Drawing.Size(214, 73);
            this.lblHorasNoRealizadas.TabIndex = 3;
            this.lblHorasNoRealizadas.Text = "No Realizables\r\n{0}h";
            this.lblHorasNoRealizadas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblHorasNoRealizadas.Click += new System.EventHandler(this.UCGraficoBarras_Click);
            // 
            // pHorasRealizables
            // 
            this.pHorasRealizables.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pHorasRealizables.Controls.Add(this.lblHorasRealizables);
            this.pHorasRealizables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pHorasRealizables.Location = new System.Drawing.Point(3, 254);
            this.pHorasRealizables.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.pHorasRealizables.Name = "pHorasRealizables";
            this.pHorasRealizables.Size = new System.Drawing.Size(214, 73);
            this.pHorasRealizables.TabIndex = 1;
            this.pHorasRealizables.Click += new System.EventHandler(this.UCGraficoBarras_Click);
            // 
            // lblHorasRealizables
            // 
            this.lblHorasRealizables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHorasRealizables.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.lblHorasRealizables.Location = new System.Drawing.Point(0, 0);
            this.lblHorasRealizables.Margin = new System.Windows.Forms.Padding(3);
            this.lblHorasRealizables.Name = "lblHorasRealizables";
            this.lblHorasRealizables.Size = new System.Drawing.Size(214, 73);
            this.lblHorasRealizables.TabIndex = 3;
            this.lblHorasRealizables.Text = "Realizables\r\n{0}h\r\n";
            this.lblHorasRealizables.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblHorasRealizables.Click += new System.EventHandler(this.UCGraficoBarras_Click);
            // 
            // lblPuesto
            // 
            this.lblPuesto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblPuesto.AutoSize = true;
            this.lblPuesto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.lblPuesto.Location = new System.Drawing.Point(70, 0);
            this.lblPuesto.Name = "lblPuesto";
            this.lblPuesto.Size = new System.Drawing.Size(80, 35);
            this.lblPuesto.TabIndex = 3;
            this.lblPuesto.Text = "Maquina {0}";
            this.lblPuesto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPuesto.Click += new System.EventHandler(this.UCGraficoBarras_Click);
            // 
            // lblTotalHoras
            // 
            this.lblTotalHoras.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalHoras.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.lblTotalHoras.Location = new System.Drawing.Point(3, 330);
            this.lblTotalHoras.Margin = new System.Windows.Forms.Padding(3);
            this.lblTotalHoras.Name = "lblTotalHoras";
            this.lblTotalHoras.Size = new System.Drawing.Size(214, 30);
            this.lblTotalHoras.TabIndex = 2;
            this.lblTotalHoras.Text = "Total {0}h\r\n";
            this.lblTotalHoras.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.lblTotalHoras.Click += new System.EventHandler(this.UCGraficoBarras_Click);
            // 
            // pHorasInciertas
            // 
            this.pHorasInciertas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.pHorasInciertas.Controls.Add(this.lblHorasInciertas);
            this.pHorasInciertas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pHorasInciertas.Location = new System.Drawing.Point(3, 181);
            this.pHorasInciertas.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.pHorasInciertas.Name = "pHorasInciertas";
            this.pHorasInciertas.Size = new System.Drawing.Size(214, 73);
            this.pHorasInciertas.TabIndex = 4;
            this.pHorasInciertas.Click += new System.EventHandler(this.UCGraficoBarras_Click);
            // 
            // lblHorasInciertas
            // 
            this.lblHorasInciertas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHorasInciertas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.lblHorasInciertas.Location = new System.Drawing.Point(0, 0);
            this.lblHorasInciertas.Margin = new System.Windows.Forms.Padding(3);
            this.lblHorasInciertas.Name = "lblHorasInciertas";
            this.lblHorasInciertas.Size = new System.Drawing.Size(214, 73);
            this.lblHorasInciertas.TabIndex = 4;
            this.lblHorasInciertas.Text = "Inciertas \r\n{0}h";
            this.lblHorasInciertas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblHorasInciertas.Click += new System.EventHandler(this.UCGraficoBarras_Click);
            // 
            // UCGraficoBarras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UCGraficoBarras";
            this.Size = new System.Drawing.Size(220, 363);
            this.Click += new System.EventHandler(this.UCGraficoBarras_Click);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.pHorasNoRealizables.ResumeLayout(false);
            this.pHorasRealizables.ResumeLayout(false);
            this.pHorasInciertas.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pHorasNoRealizables;
        private System.Windows.Forms.Panel pHorasRealizables;
        private System.Windows.Forms.Label lblHorasNoRealizadas;
        private System.Windows.Forms.Label lblHorasRealizables;
        private System.Windows.Forms.Label lblTotalHoras;
        private System.Windows.Forms.Label lblPuesto;
        private System.Windows.Forms.Panel pHorasInciertas;
        private System.Windows.Forms.Label lblHorasInciertas;

    }
}
