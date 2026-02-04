namespace GestorPesoClientes
{
    partial class GestorPesoClientes
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
            this.buscador = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_marcados = new System.Windows.Forms.Button();
            this.btn_desmarcados = new System.Windows.Forms.Button();
            this.btn_todos = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.mostrando = new System.Windows.Forms.Label();
            this.btn_guardar = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // buscador
            // 
            this.buscador.Location = new System.Drawing.Point(696, 34);
            this.buscador.Name = "buscador";
            this.buscador.Size = new System.Drawing.Size(100, 20);
            this.buscador.TabIndex = 1;
            this.buscador.TextChanged += new System.EventHandler(this.buscador_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(636, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "BUSCAR:";
            // 
            // btn_marcados
            // 
            this.btn_marcados.Location = new System.Drawing.Point(12, 579);
            this.btn_marcados.Name = "btn_marcados";
            this.btn_marcados.Size = new System.Drawing.Size(157, 45);
            this.btn_marcados.TabIndex = 3;
            this.btn_marcados.Text = "CON PESO";
            this.btn_marcados.UseVisualStyleBackColor = true;
            this.btn_marcados.Click += new System.EventHandler(this.btn_marcados_Click);
            // 
            // btn_desmarcados
            // 
            this.btn_desmarcados.Location = new System.Drawing.Point(175, 579);
            this.btn_desmarcados.Name = "btn_desmarcados";
            this.btn_desmarcados.Size = new System.Drawing.Size(157, 45);
            this.btn_desmarcados.TabIndex = 4;
            this.btn_desmarcados.Text = "SIN PESO";
            this.btn_desmarcados.UseVisualStyleBackColor = true;
            this.btn_desmarcados.Click += new System.EventHandler(this.btn_desmarcados_Click);
            // 
            // btn_todos
            // 
            this.btn_todos.Location = new System.Drawing.Point(639, 579);
            this.btn_todos.Name = "btn_todos";
            this.btn_todos.Size = new System.Drawing.Size(157, 45);
            this.btn_todos.TabIndex = 5;
            this.btn_todos.Text = "MOSTRAR TODOS";
            this.btn_todos.UseVisualStyleBackColor = true;
            this.btn_todos.Click += new System.EventHandler(this.btn_todos_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(276, 31);
            this.label2.TabIndex = 6;
            this.label2.Text = "LISTA DE CLIENTES";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(302, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "MOSTRANDO:";
            // 
            // mostrando
            // 
            this.mostrando.AutoSize = true;
            this.mostrando.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mostrando.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.mostrando.Location = new System.Drawing.Point(411, 21);
            this.mostrando.Name = "mostrando";
            this.mostrando.Size = new System.Drawing.Size(63, 17);
            this.mostrando.TabIndex = 8;
            this.mostrando.Text = "TODOS";
            // 
            // btn_guardar
            // 
            this.btn_guardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btn_guardar.Location = new System.Drawing.Point(639, 649);
            this.btn_guardar.Name = "btn_guardar";
            this.btn_guardar.Size = new System.Drawing.Size(157, 45);
            this.btn_guardar.TabIndex = 10;
            this.btn_guardar.Text = "GUARDAR";
            this.btn_guardar.UseVisualStyleBackColor = false;
            this.btn_guardar.Click += new System.EventHandler(this.btn_guardar_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 60);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(784, 513);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // GestorPesoClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 706);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_guardar);
            this.Controls.Add(this.mostrando);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_todos);
            this.Controls.Add(this.btn_desmarcados);
            this.Controls.Add(this.btn_marcados);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buscador);
            this.Name = "GestorPesoClientes";
            this.Text = "GESTOR PESO CLIENTES PACKING";
            this.Load += new System.EventHandler(this.GestorPesoClientes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox buscador;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_marcados;
        private System.Windows.Forms.Button btn_desmarcados;
        private System.Windows.Forms.Button btn_todos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label mostrando;
        private System.Windows.Forms.Button btn_guardar;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

