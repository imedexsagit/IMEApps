using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Intranet
{
    public partial class ImportarCodigo : Form
    {
        private String codigoElegido;


        public ImportarCodigo(DataSet ds)
        {           
            InitializeComponent();

            this.codigoElegido = "";

            this.cmbCodigo.DataSource = ds.Tables[0];
            this.cmbCodigo.ValueMember = "CODIGO";
            this.cmbCodigo.DisplayMember = "DENOMINACION";

            this.textBox1.Text = "";
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.codigoElegido = cmbCodigo.Text;
            this.textBox1.Text = this.cmbCodigo.SelectedValue.ToString();
        }


        public String getResultado()
        {
            return this.codigoElegido;
        }

        private void btnAcept_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.codigoElegido = "";

            this.Close();
        }

    }
}
