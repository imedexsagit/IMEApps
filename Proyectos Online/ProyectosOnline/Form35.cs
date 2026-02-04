using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using empresaGlobalProj;
using System.Data.SqlClient;
using System.Configuration;


namespace ProyectosOnLine
{
    public partial class Form35 : Form
    {
        private Int32 of;
        private List<int> listaOF = new List<int>();
        private String operacion;
        private String cadenaOf = ""; 


        public Form35(List<int> ordfab, String operacion)
        {
            InitializeComponent();
            this.listaOF = ordfab;
            this.operacion = operacion;

            for (int i = 0; i < listaOF.Count; i++) {
                cadenaOf +=  listaOF[i] + " , ";
            }

            cadenaOf = cadenaOf.Remove(cadenaOf.Length - 3);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void Form35_Load(object sender, EventArgs e)
        {
            string strSql = "";
            SqlCommand sqlCmd;
            DataTable dt = new DataTable();
            string server = "srvsql02";
            string connetionString = @"Data Source=" + server + ";Initial Catalog=gg;User ID=gg;Password=ostia";

            SqlConnection conexion = new SqlConnection(connetionString);


            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                strSql = "select ofab.ORDFAB as 'OrdFab',ofab.CODIGO as 'Codigo',ctdlan as 'Cantidad'from T_ORDFAB ofab join";
                strSql += "   T_ORDFABO ofabo on ofabo.ORDFAB = ofab.ORDFAB";
                strSql += "   where  (ofab.CODEMP = '" + empresaGlobal.empresaID + "')  AND (ofab.TIPOREG = 'F') AND (ofab.ORDFAB in (" + this.cadenaOf + "))  AND (OPERAC = '" + this.operacion + "')";

                sqlCmd = new SqlCommand(strSql, conexion);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                da.Fill(dt);


               

            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }

            var bindingSource = new System.Windows.Forms.BindingSource();
            bindingSource.DataSource = dt;
            dataGridView1.DataSource = bindingSource;

            //Carlos Sánchez 28/11/2022
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells[0].Value = true;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int contador = 0;

            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ggConnectionString"].ToString());
            String strSql = "";
            SqlCommand sqlCmd;

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[0].Value) == true)    //Si está seleccionado el checkbox
                    {
                        contador++;

                        sqlCmd = new SqlCommand("gg.[dbo].[Recuperar_Operaciones_Exterior]", conexion);
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.Parameters.Add("@OrdFab", SqlDbType.Int);
                        sqlCmd.Parameters["@Ordfab"].Value = Convert.ToInt32(row.Cells[1].Value);

                        sqlCmd.ExecuteNonQuery();

                    }

                }

                if (contador > 0)
                {
                    MessageBox.Show(Form.ActiveForm, "Cambios realizados correctamente");
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, "Ocurrio un error al guardar los cambios: " + ex.Message.ToString());
                this.DialogResult = DialogResult.Cancel;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();

                this.Close();
            } 

        }

        private void button3_Click(object sender, EventArgs e) //Carlos Sánchez 28/11/2022
        {
            if (button3.Text == "Quitar seleccionados")
            {

                button3.Text = "Seleccionar todo";
                
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells[0].Value = false;
                }

            }
            else
            {
                button3.Text = "Quitar seleccionados";
                
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells[0].Value = true;
                }

            }
        }



    }
}
