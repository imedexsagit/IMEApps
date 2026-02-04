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
    public partial class FormEmpresas : Form
    {
        //variable que indica la empresa elegida
        //string empresaID = "3";

        public FormEmpresas()
        {
            InitializeComponent();
            this.EmpresasDataGridView.ReadOnly = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (EmpresasDataGridView.CurrentRow != null)
            {
                
                string nempresa = EmpresasDataGridView.CurrentRow.Cells["CODEMP"].Value.ToString();
                string empresaSeleccionada = EmpresasDataGridView.CurrentRow.Cells["DENOMI"].Value.ToString();
                string abrevempresa = EmpresasDataGridView.CurrentRow.Cells["ABREV"].Value.ToString();
                // Almacenarlo en la key "Empresa" de app.config -> appSettings
                //empresaGlobal.empresaID = "1";

                System.Diagnostics.Debug.WriteLine(" La empresaID inicialmente en Click es: " + empresaGlobal.empresaID + " y el nombre de la empresa es: " + empresaGlobal.nombreEmpresa);
                empresaGlobalProj.empresaGlobal.empresaID = nempresa;
                empresaGlobal.nombreEmpresa = abrevempresa;
                System.Diagnostics.Debug.WriteLine(" La empresaID se actualiza en Click y ahora es: " + empresaGlobal.empresaID + " y el nombre de la empresa es: " + empresaGlobal.nombreEmpresa);

                //Cojo el valor una vez cambiado en app.config para asegurar el funcionamiento
                string configvalue1 = empresaGlobal.empresaID;

                //MessageBox.Show("La empresa seleccionada es: " + empresaSeleccionada + " y el código de empresa modificado es: " + configvalue1);
                base.Close();
            }

        }

        private void FormEmpresas_Load(object sender, EventArgs e)
        {
            EmpresasDataGridView.DataSource = GetEmpresasList();
        }

        private DataTable GetEmpresasList()
        {
            DataTable dtEmpresas = new DataTable();

            string connString = @"Data Source=db-imedexsa;Initial Catalog=gg;User ID=gg;Password=ostia"; //ConfigurationManager.ConnectionStrings["Interface_Usuario.Properties.Settings.CS_CPP_0713ConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("select T_EMPRESA.CODEMP, T_EMPRESA.DENOMI, T_EMPRESA.ABREV from T_EMPRESA WHERE T_EMPRESA.CODEMP = '3' OR T_EMPRESA.CODEMP = '60' OR T_EMPRESA.CODEMP = '61'", con))
                {
                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    dtEmpresas.Load(reader);
                }
            }

            return dtEmpresas;
        }

        public void seleccionarEmpresa()
        {
            //if (this.lvEmpresas.SelectedItems.Count == 1)
            //{
            //EmpresasDS.empresasRow tag = (EmpresasDS.empresasRow)this.lvEmpresas.SelectedItems[0].Tag;
            //Estado.DREmpresa = tag;
            //base.ParentForm.Close();
            //}

        }

        private void EmpresasDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //button3_Click(sender, e);
        }

        private void EmpresasDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string nempresa = EmpresasDataGridView.Rows[e.RowIndex].Cells["CODEMP"].Value.ToString();
            string empresaSeleccionada = EmpresasDataGridView.Rows[e.RowIndex].Cells["DENOMI"].Value.ToString();
            string abrevempresa = EmpresasDataGridView.Rows[e.RowIndex].Cells["ABREV"].Value.ToString();

            // Almacenarlo en la key "Empresa" de app.config -> appSettings
            System.Diagnostics.Debug.WriteLine(" La empresaID inicialmente en DoubleClick es: " + empresaGlobal.empresaID + " y el nombre de la empresa es: " + empresaGlobal.nombreEmpresa);
            empresaGlobal.empresaID = nempresa;
            empresaGlobal.nombreEmpresa = abrevempresa;
            System.Diagnostics.Debug.WriteLine(" La empresaID se actualiza en DoubleClick y ahora es: " + empresaGlobal.empresaID + " y el nombre de la empresa es: " + empresaGlobal.nombreEmpresa);

            //se coge el valor una vez cambiado en empresaGlobal para comprobar el funcionamiento
            string configvalue1 = empresaGlobal.empresaID;


            //MessageBox.Show("La empresa seleccionada es: " + empresaSeleccionada + " y el código de empresa modificado es: " + configvalue1);
            base.Close();
        }

    }
}