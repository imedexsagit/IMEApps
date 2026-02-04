using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace InspeccionPinturaTablet
{
    public partial class Form5paquete : Form
    {
        string certificado;
        int w;
        inspeccionesPinturaBD ipDB = new inspeccionesPinturaBD();
        public string paqmin, paqmed, paqmax;

        public Form5paquete(string certificado, int w)
        {
            InitializeComponent();
            this.certificado = certificado;
            this.w = w;
            DataTable table = null;
            table = ipDB.datos5EspesorPaquetes(certificado,w);
            dataGridView1.DataSource = table;
            dataGridView1.Columns[0].Visible = false; // NO visulizamos EL CODEMP

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // PRIMERO RECORREMOS EL  DATESET Y GUARDAMOS LAS FILAS 
            Cursor.Current = Cursors.WaitCursor;

            SqlConnection conexion = null;
            SqlCommand comando = null;
            string strsql;

            int totalmin = 0, totalmed = 0, totalmax = 0;
            int dividendomin = 0, dividendomed = 0, dividendomax = 0;

            string connString = "Data Source=srvsql02;Initial Catalog=gg;User ID=gg;Password=ostia";

            DataTable table = new DataTable();

            conexion = new SqlConnection(connString);
            conexion.Open();
            SqlDataAdapter adapter;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                try
                {
                   

                        strsql = "UPDATE  TC_INSP_PINTURA_5ESP_PAQUETES SET ";
                        strsql = strsql + " capmin=" + row.Cells[4].Value.ToString();
                        strsql = strsql + ", capmed=" + row.Cells[5].Value.ToString();
                        strsql = strsql + ", capmax=" + row.Cells[6].Value.ToString();
                        strsql = strsql + " WHERE certificado = '" + certificado + "' and paquete = " + row.Cells[3].Value.ToString() + " and w="+w;


                        comando = new SqlCommand(strsql, conexion);
                        adapter = new SqlDataAdapter(comando);
                        table = new DataTable();
                        adapter.Fill(table);

                }
                catch (ArgumentOutOfRangeException outOfRange)
                {
                    Console.WriteLine("Error: {0}", outOfRange.Message);
                }

                if (Int32.Parse(row.Cells[4].Value.ToString()) != 0)
                { //min

                    totalmin = totalmin + Int32.Parse(row.Cells[4].Value.ToString());
                    dividendomin = dividendomin + 1;
                }

                if (Int32.Parse(row.Cells[5].Value.ToString()) != 0)
                { //med

                    totalmed = totalmed + Int32.Parse(row.Cells[5].Value.ToString());
                    dividendomed = dividendomed + 1;
                }

                if (Int32.Parse(row.Cells[6].Value.ToString()) != 0)
                { //max

                    totalmax = totalmax + Int32.Parse(row.Cells[6].Value.ToString());
                    dividendomax = dividendomax + 1;
                }
            }

            conexion.Close();
            Cursor.Current = Cursors.Default;
            MessageBox.Show("CAMBIOS GUARDADOS CORRECTAMENTE");


            if (dividendomin == 0) paqmin = "0";
            else paqmin = Convert.ToString(totalmin / dividendomin);

            if (dividendomed == 0) paqmed = "0";
            else paqmed = Convert.ToString(totalmed / dividendomed);

            if (dividendomax == 0) paqmax = "0";
            else paqmax = Convert.ToString(totalmax / dividendomax);
        }
    }
}
