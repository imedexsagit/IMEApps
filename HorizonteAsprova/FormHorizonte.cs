using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace HorizonteAsprova
{
    public partial class formHorizonte : Form
    {

        DateTime fechaActual;

        // CARLOS 20/01/2026 - Al cargar el formulario recupero la fecha de la base de datos para almacenarla como fecha actual y poder compararla con la fecha del DateTimePicker
        public formHorizonte()
        {
            InitializeComponent();
            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ggConnectionString"].ToString());
            conexion.Open();

            String strSql = "";
            SqlCommand sqlCmd;

            strSql = "SELECT FechaHorizonte ";
            strSql += "FROM ASPROVA.dbo.ASPROVA_08_Horizonte_Table";

            sqlCmd = new SqlCommand(strSql, conexion);
            fechaActual = (DateTime)sqlCmd.ExecuteScalar();


            this.fechaHorizonte.Value = fechaActual;
            conexion.Close(); 
        }


        // CARLOS 20/01/2026 - Comparo la fecha elegida al cambiar el DateTimePicker de valor con el valor que está almacenado en la variable de fecha actual
        private void fechaHorizonte_ValueChanged(object sender, EventArgs e)
        {
            if (this.fechaHorizonte.Value != fechaActual)
            {
                this.btnGuardar.Visible = true;
            }
            else
            {
                this.btnGuardar.Visible = false;
            }
        }

        // CARLOS 20/01/2026 - Al pulsar sobre el botón de guardar almaceno el valor de la fecha elegida en una variable, oculto el botón de guardar y actualizo la fecha en la tabla de la base de datos. 
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ggConnectionString"].ToString());
            conexion.Open();

            DateTime fechaHoy = DateTime.Today; 

            DateTime fechaNueva = this.fechaHorizonte.Value;

            if (fechaNueva > fechaHoy)
            {
                string fechaSQL = fechaNueva.ToString("yyyy/MM/dd");

                fechaActual = fechaNueva;
                if (this.fechaHorizonte.Value != fechaActual)
                {
                    this.btnGuardar.Visible = true;
                }
                else
                {
                    this.btnGuardar.Visible = false;
                }
                String strSql = "";
                SqlCommand sqlCmd;

                strSql = "UPDATE ASPROVA.dbo.ASPROVA_08_Horizonte_Table ";
                strSql += "SET FechaHorizonte=@fechaSQL";

                using (sqlCmd = new SqlCommand(strSql, conexion))
                {
                    sqlCmd.Parameters.AddWithValue("@fechaSQL", fechaSQL);
                    int filasAfectadas = sqlCmd.ExecuteNonQuery();
                    System.Diagnostics.Debug.WriteLine("Filas afectadas: " + filasAfectadas);
                }

            }
            else
            {
                // CARLOS 20/01/2026 - Se muestra este formulario para indicar el error de poner una fecha incorrecta
                ErrorFecha f = new ErrorFecha();
                f.Show(); 
            }

            
            conexion.Close(); 
        }


    }
}
