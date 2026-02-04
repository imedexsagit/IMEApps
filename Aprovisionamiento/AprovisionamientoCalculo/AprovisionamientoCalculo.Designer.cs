namespace AprovisionamientoCalculo
{
    partial class AprovisionamientoCalculo
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
            this.label_aviso = new System.Windows.Forms.Label();
            this.button_calcular = new System.Windows.Forms.Button();
            this.barra_progreso = new System.Windows.Forms.ProgressBar();
            this.label_progreso = new System.Windows.Forms.Label();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.button_mp_completo = new System.Windows.Forms.Button();
            this.button_mp_roturas = new System.Windows.Forms.Button();
            this.label_informes = new System.Windows.Forms.Label();
            this.label_mp = new System.Windows.Forms.Label();
            this.label_to = new System.Windows.Forms.Label();
            this.button_to_completo = new System.Windows.Forms.Button();
            this.button_to_roturas = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_aviso
            // 
            this.label_aviso.AutoSize = true;
            this.label_aviso.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_aviso.ForeColor = System.Drawing.Color.Red;
            this.label_aviso.Location = new System.Drawing.Point(84, 28);
            this.label_aviso.Name = "label_aviso";
            this.label_aviso.Size = new System.Drawing.Size(0, 25);
            this.label_aviso.TabIndex = 0;
            this.label_aviso.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_calcular
            // 
            this.button_calcular.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_calcular.Location = new System.Drawing.Point(385, 272);
            this.button_calcular.Name = "button_calcular";
            this.button_calcular.Size = new System.Drawing.Size(187, 102);
            this.button_calcular.TabIndex = 1;
            this.button_calcular.Text = "CALCULAR";
            this.button_calcular.UseVisualStyleBackColor = true;
            this.button_calcular.Click += new System.EventHandler(this.button_calcular_Click);
            // 
            // barra_progreso
            // 
            this.barra_progreso.Location = new System.Drawing.Point(12, 657);
            this.barra_progreso.Name = "barra_progreso";
            this.barra_progreso.Size = new System.Drawing.Size(960, 64);
            this.barra_progreso.TabIndex = 2;
            // 
            // label_progreso
            // 
            this.label_progreso.AutoSize = true;
            this.label_progreso.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_progreso.ForeColor = System.Drawing.Color.Black;
            this.label_progreso.Location = new System.Drawing.Point(15, 615);
            this.label_progreso.Name = "label_progreso";
            this.label_progreso.Size = new System.Drawing.Size(0, 31);
            this.label_progreso.TabIndex = 3;
            this.label_progreso.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // button_mp_completo
            // 
            this.button_mp_completo.Location = new System.Drawing.Point(9, 54);
            this.button_mp_completo.Name = "button_mp_completo";
            this.button_mp_completo.Size = new System.Drawing.Size(75, 23);
            this.button_mp_completo.TabIndex = 4;
            this.button_mp_completo.Text = "Completo";
            this.button_mp_completo.UseVisualStyleBackColor = true;
            this.button_mp_completo.Click += new System.EventHandler(this.button_mp_completo_Click);
            // 
            // button_mp_roturas
            // 
            this.button_mp_roturas.Location = new System.Drawing.Point(90, 54);
            this.button_mp_roturas.Name = "button_mp_roturas";
            this.button_mp_roturas.Size = new System.Drawing.Size(75, 23);
            this.button_mp_roturas.TabIndex = 5;
            this.button_mp_roturas.Text = "Roturas";
            this.button_mp_roturas.UseVisualStyleBackColor = true;
            this.button_mp_roturas.Click += new System.EventHandler(this.button_mp_roturas_Click);
            // 
            // label_informes
            // 
            this.label_informes.AutoSize = true;
            this.label_informes.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_informes.Location = new System.Drawing.Point(35, 5);
            this.label_informes.Name = "label_informes";
            this.label_informes.Size = new System.Drawing.Size(109, 24);
            this.label_informes.TabIndex = 6;
            this.label_informes.Text = "INFORMES";
            // 
            // label_mp
            // 
            this.label_mp.AutoSize = true;
            this.label_mp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_mp.Location = new System.Drawing.Point(71, 33);
            this.label_mp.Name = "label_mp";
            this.label_mp.Size = new System.Drawing.Size(32, 20);
            this.label_mp.TabIndex = 7;
            this.label_mp.Text = "MP";
            // 
            // label_to
            // 
            this.label_to.AutoSize = true;
            this.label_to.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_to.Location = new System.Drawing.Point(71, 86);
            this.label_to.Name = "label_to";
            this.label_to.Size = new System.Drawing.Size(30, 20);
            this.label_to.TabIndex = 8;
            this.label_to.Text = "TO";
            // 
            // button_to_completo
            // 
            this.button_to_completo.Location = new System.Drawing.Point(9, 108);
            this.button_to_completo.Name = "button_to_completo";
            this.button_to_completo.Size = new System.Drawing.Size(75, 23);
            this.button_to_completo.TabIndex = 9;
            this.button_to_completo.Text = "Completo";
            this.button_to_completo.UseVisualStyleBackColor = true;
            this.button_to_completo.Click += new System.EventHandler(this.button_to_completo_Click);
            // 
            // button_to_roturas
            // 
            this.button_to_roturas.Location = new System.Drawing.Point(90, 109);
            this.button_to_roturas.Name = "button_to_roturas";
            this.button_to_roturas.Size = new System.Drawing.Size(75, 23);
            this.button_to_roturas.TabIndex = 10;
            this.button_to_roturas.Text = "Roturas";
            this.button_to_roturas.UseVisualStyleBackColor = true;
            this.button_to_roturas.Click += new System.EventHandler(this.button_to_roturas_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.button_mp_roturas);
            this.panel1.Controls.Add(this.button_to_roturas);
            this.panel1.Controls.Add(this.button_mp_completo);
            this.panel1.Controls.Add(this.button_to_completo);
            this.panel1.Controls.Add(this.label_informes);
            this.panel1.Controls.Add(this.label_to);
            this.panel1.Controls.Add(this.label_mp);
            this.panel1.Location = new System.Drawing.Point(799, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(173, 141);
            this.panel1.TabIndex = 11;
            // 
            // AprovisionamientoCalculo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(984, 761);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label_progreso);
            this.Controls.Add(this.barra_progreso);
            this.Controls.Add(this.button_calcular);
            this.Controls.Add(this.label_aviso);
            this.MaximizeBox = false;
            this.Name = "AprovisionamientoCalculo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calcular Aprovisionamiento";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AprovisionamientoCalculo_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_aviso;
        private System.Windows.Forms.Button button_calcular;
        private System.Windows.Forms.ProgressBar barra_progreso;
        private System.Windows.Forms.Label label_progreso;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.Button button_mp_completo;
        private System.Windows.Forms.Button button_mp_roturas;
        private System.Windows.Forms.Label label_informes;
        private System.Windows.Forms.Label label_mp;
        private System.Windows.Forms.Label label_to;
        private System.Windows.Forms.Button button_to_completo;
        private System.Windows.Forms.Button button_to_roturas;
        private System.Windows.Forms.Panel panel1;
    }
}

