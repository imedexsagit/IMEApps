using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using empresaGlobalProj;

namespace ImeApps
{
    public partial class FormElegirEmpresas : Form
    {
        //variable que indica la empresa elegida
        //string empresaID = "3";

        public FormElegirEmpresas()
        {
            InitializeComponent();
        }

        private void FormElegirEmpresas_Load(object sender, EventArgs e)
        {
            //EmpresasDataGridView.DataSource = GetEmpresasList();
        }

        private void button_Ime_Click(object sender, EventArgs e)
        {
            empresaGlobal.empresaID = "3";
            empresaGlobal.nombreEmpresa = "IMEDEXSA";
            //Principal.cambiarFondoEmpresa(empresaGlobal.empresaID);
            base.Close();
        }

        private void button_Made_Click(object sender, EventArgs e)
        {
            empresaGlobal.empresaID = "60";
            empresaGlobal.nombreEmpresa = "MADETOWER";
            base.Close();
        }

        private void button_oldMade_Click(object sender, EventArgs e)
        {
            empresaGlobal.empresaID = "61";
            empresaGlobal.nombreEmpresa = "OLD_MADE";
            base.Close();
        }

    }
}