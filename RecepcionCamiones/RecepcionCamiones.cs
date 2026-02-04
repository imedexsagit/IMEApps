using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using empresaGlobalProj;

namespace RecepcionCamiones
{
    public partial class RecepcionCamiones : Form
    {
        string empresa;
        SqlConnection conexion;
        DataTable listaEtiquetas;

        public RecepcionCamiones()
        {
            InitializeComponent();
        }

        private void RecepcionCamiones_Load(object sender, EventArgs e)
        {
            this.comboBox1.SelectedIndex = 0;
            this.comboBox2.SelectedIndex = 0;
            empresa = empresaGlobal.empresaID; //ConfigurationManager.AppSettings["EMPRESA"].ToString();                        
            conexion = new SqlConnection(Utils.CD.getConexion());
            cargarTabla();
        }

        private void escribir(string letra)
        {
            textBox1.Text = textBox1.Text + letra.ToLower();
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.SelectionLength = 0;
            textBox1.Select();
        }

        private void btnNum1_Click(object sender, EventArgs e)
        {
            escribir("1");
        }
        
        private void btnNum2_Click(object sender, EventArgs e)
        {
            escribir("2");
        }

        private void btnNum3_Click(object sender, EventArgs e)
        {
            escribir("3");
        }

        private void btnNum4_Click(object sender, EventArgs e)
        {
            escribir("4");
        }

        private void btnNum5_Click(object sender, EventArgs e)
        {
            escribir("5");
        }

        private void btnNum6_Click(object sender, EventArgs e)
        {
            escribir("6");
        }

        private void btnNum7_Click(object sender, EventArgs e)
        {
            escribir("7");
        }

        private void btnNum8_Click(object sender, EventArgs e)
        {
            escribir("8");
        }

        private void btnNum9_Click(object sender, EventArgs e)
        {
            escribir("9");
        }

        private void btnGuion_Click(object sender, EventArgs e)
        {
            escribir("-");
        }

        private void btnNum0_Click(object sender, EventArgs e)
        {
            escribir("0");
        }

        private void btnNumAC_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                this.textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.SelectionLength = 0;
                textBox1.Select();
            }
        }

        private void cargarTabla()
        {
            string strsql = "";



            SqlCommand comando;
            SqlDataAdapter adapter;

            try
            {

                conexion.Open();

                strsql += "select ETIQUETA, PROYECTO, SEDE, UBICACION, F_CRE AS FECHA from TC_RECEPCION_ETIQUETAS order by F_CRE DESC";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                listaEtiquetas = new DataTable();
                adapter.Fill(listaEtiquetas);

                /*if (listaEtiquetas.Rows.Count <= 0)
                {
                    return;
                }*/

                conexion.Close();

                this.dataGridView1.DataSource = listaEtiquetas;
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable table = null;

            string strsql = "";



            SqlCommand comando;
            SqlDataAdapter adapter;

            try
            {

                conexion.Open();

                strsql += "select * from TC_LANZA_ETIQUETA where Etiqueta = " + this.textBox1.Text;

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                if (table.Rows.Count <= 0)
                {
                    MessageBox.Show("La etiqueta que se ha introducido es incorrecta o no existe en el sistema", "ERROR");
                    conexion.Close();
                    return;
                }
                else
                {
                    strsql =  "INSERT INTO [dbo].[TC_RECEPCION_ETIQUETAS] ([ETIQUETA] ,[SEDE] ,[F_CRE] ,[USUCRE] ,[UBICACION], [PROYECTO]) ";
                    strsql += "VALUES ('" + this.textBox1.Text + "' ,'" + this.comboBox2.Text + "' , GETDATE(), '" + Environment.UserName + "' , '" + this.comboBox1.Text + "', (select top (1) ofab.PROYECTO from TC_LANZA_ETIQUETA le inner join T_ORDFAB ofab on le.OrdFab = ofab.ORDFAB where Etiqueta = '" + this.textBox1.Text + "'))";

                    comando = new SqlCommand(strsql, conexion);

                    comando.ExecuteNonQuery();

                    conexion.Close();

                    cargarTabla();
                }

            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
