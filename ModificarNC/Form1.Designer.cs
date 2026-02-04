namespace ModificarNC
{
    partial class ModificarNCyCAM
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
            this.label14 = new System.Windows.Forms.Label();
            this.txtPathNC = new System.Windows.Forms.TextBox();
            this.btnSetPathNC = new System.Windows.Forms.Button();
            this.prefijo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGenerarFicherosNC = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtOFProyecto = new System.Windows.Forms.TextBox();
            this.txtOFSerie = new System.Windows.Forms.TextBox();
            this.txtOFDibujo = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtOFDescripcion = new System.Windows.Forms.TextBox();
            this.txtOFDiseñador = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.AM = new System.Windows.Forms.CheckBox();
            this.DB = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(13, 26);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(61, 20);
            this.label14.TabIndex = 20;
            this.label14.Text = "Path NC:";
            // 
            // txtPathNC
            // 
            this.txtPathNC.Location = new System.Drawing.Point(82, 23);
            this.txtPathNC.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPathNC.Name = "txtPathNC";
            this.txtPathNC.Size = new System.Drawing.Size(436, 26);
            this.txtPathNC.TabIndex = 21;
            // 
            // btnSetPathNC
            // 
            this.btnSetPathNC.Location = new System.Drawing.Point(526, 22);
            this.btnSetPathNC.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSetPathNC.Name = "btnSetPathNC";
            this.btnSetPathNC.Size = new System.Drawing.Size(31, 29);
            this.btnSetPathNC.TabIndex = 23;
            this.btnSetPathNC.Text = "...";
            this.btnSetPathNC.UseVisualStyleBackColor = true;
            this.btnSetPathNC.Click += new System.EventHandler(this.btnSetPathNC_Click);
            // 
            // prefijo
            // 
            this.prefijo.Location = new System.Drawing.Point(129, 57);
            this.prefijo.Name = "prefijo";
            this.prefijo.Size = new System.Drawing.Size(271, 26);
            this.prefijo.TabIndex = 40;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(71, 60);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 41;
            this.label1.Text = "Prefijo:";
            // 
            // btnGenerarFicherosNC
            // 
            this.btnGenerarFicherosNC.BackColor = System.Drawing.Color.Chartreuse;
            this.btnGenerarFicherosNC.Location = new System.Drawing.Point(597, 26);
            this.btnGenerarFicherosNC.Name = "btnGenerarFicherosNC";
            this.btnGenerarFicherosNC.Size = new System.Drawing.Size(183, 80);
            this.btnGenerarFicherosNC.TabIndex = 42;
            this.btnGenerarFicherosNC.Text = "CREAR NUEVOS FICHEROS NC";
            this.btnGenerarFicherosNC.UseVisualStyleBackColor = false;
            this.btnGenerarFicherosNC.Click += new System.EventHandler(this.modificarCN);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 188);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 20);
            this.label3.TabIndex = 46;
            this.label3.Text = "Proyecto:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(78, 220);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 20);
            this.label5.TabIndex = 47;
            this.label5.Text = "Serie:";
            // 
            // txtOFProyecto
            // 
            this.txtOFProyecto.Location = new System.Drawing.Point(129, 185);
            this.txtOFProyecto.Name = "txtOFProyecto";
            this.txtOFProyecto.Size = new System.Drawing.Size(271, 26);
            this.txtOFProyecto.TabIndex = 48;
            // 
            // txtOFSerie
            // 
            this.txtOFSerie.Location = new System.Drawing.Point(129, 217);
            this.txtOFSerie.Name = "txtOFSerie";
            this.txtOFSerie.Size = new System.Drawing.Size(271, 26);
            this.txtOFSerie.TabIndex = 49;
            // 
            // txtOFDibujo
            // 
            this.txtOFDibujo.Location = new System.Drawing.Point(129, 249);
            this.txtOFDibujo.Name = "txtOFDibujo";
            this.txtOFDibujo.Size = new System.Drawing.Size(271, 26);
            this.txtOFDibujo.TabIndex = 50;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(72, 252);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 20);
            this.label11.TabIndex = 51;
            this.label11.Text = "Dibujo:";
            // 
            // txtOFDescripcion
            // 
            this.txtOFDescripcion.Location = new System.Drawing.Point(129, 281);
            this.txtOFDescripcion.Name = "txtOFDescripcion";
            this.txtOFDescripcion.Size = new System.Drawing.Size(271, 26);
            this.txtOFDescripcion.TabIndex = 52;
            // 
            // txtOFDiseñador
            // 
            this.txtOFDiseñador.Location = new System.Drawing.Point(129, 313);
            this.txtOFDiseñador.Name = "txtOFDiseñador";
            this.txtOFDiseñador.Size = new System.Drawing.Size(271, 26);
            this.txtOFDiseñador.TabIndex = 53;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(39, 284);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(84, 20);
            this.label12.TabIndex = 54;
            this.label12.Text = "Descripción:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(49, 316);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(74, 20);
            this.label13.TabIndex = 55;
            this.label13.Text = "Diseñador:";
            // 
            // AM
            // 
            this.AM.AutoSize = true;
            this.AM.Location = new System.Drawing.Point(469, 185);
            this.AM.Name = "AM";
            this.AM.Size = new System.Drawing.Size(48, 24);
            this.AM.TabIndex = 57;
            this.AM.Text = "AM";
            this.AM.UseVisualStyleBackColor = true;
            this.AM.CheckedChanged += new System.EventHandler(this.AM_CheckedChanged);
            // 
            // DB
            // 
            this.DB.AutoSize = true;
            this.DB.Location = new System.Drawing.Point(469, 215);
            this.DB.Name = "DB";
            this.DB.Size = new System.Drawing.Size(46, 24);
            this.DB.TabIndex = 58;
            this.DB.Text = "DB";
            this.DB.UseVisualStyleBackColor = true;
            this.DB.CheckedChanged += new System.EventHandler(this.DB_CheckedChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Cyan;
            this.button1.Location = new System.Drawing.Point(597, 185);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(183, 80);
            this.button1.TabIndex = 59;
            this.button1.Text = "CREAR NUEVOS FICHEROS CAM";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 150);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 20);
            this.label2.TabIndex = 60;
            this.label2.Text = "Path NC:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(82, 144);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(436, 26);
            this.textBox1.TabIndex = 61;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(526, 141);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(31, 29);
            this.button2.TabIndex = 62;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ModificarNCyCAM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 385);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.DB);
            this.Controls.Add(this.AM);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtOFDiseñador);
            this.Controls.Add(this.txtOFDescripcion);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtOFDibujo);
            this.Controls.Add(this.txtOFSerie);
            this.Controls.Add(this.txtOFProyecto);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnGenerarFicherosNC);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.prefijo);
            this.Controls.Add(this.btnSetPathNC);
            this.Controls.Add(this.txtPathNC);
            this.Controls.Add(this.label14);
            this.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ModificarNCyCAM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modificar NC y CAM";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtPathNC;
        private System.Windows.Forms.Button btnSetPathNC;
        private System.Windows.Forms.TextBox prefijo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGenerarFicherosNC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtOFProyecto;
        private System.Windows.Forms.TextBox txtOFSerie;
        private System.Windows.Forms.TextBox txtOFDibujo;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtOFDescripcion;
        private System.Windows.Forms.TextBox txtOFDiseñador;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox AM;
        private System.Windows.Forms.CheckBox DB;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
    }
}

