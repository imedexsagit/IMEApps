namespace GestionSoldadura
{
    partial class Form4
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
            this.tC_InspeccionEtiquetasDataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.tC_InspeccionEtiquetasDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // tC_InspeccionEtiquetasDataGridView
            // 
            this.tC_InspeccionEtiquetasDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tC_InspeccionEtiquetasDataGridView.Location = new System.Drawing.Point(12, 12);
            this.tC_InspeccionEtiquetasDataGridView.Name = "tC_InspeccionEtiquetasDataGridView";
            this.tC_InspeccionEtiquetasDataGridView.Size = new System.Drawing.Size(1472, 794);
            this.tC_InspeccionEtiquetasDataGridView.TabIndex = 0;
            this.tC_InspeccionEtiquetasDataGridView.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.tC_InspeccionEtiquetasDataGridView_RowPrePaint);
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1496, 818);
            this.Controls.Add(this.tC_InspeccionEtiquetasDataGridView);
            this.Name = "Form4";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "INSPECCION DE ETIQUETAS";
            ((System.ComponentModel.ISupportInitialize)(this.tC_InspeccionEtiquetasDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView tC_InspeccionEtiquetasDataGridView;

    }
}