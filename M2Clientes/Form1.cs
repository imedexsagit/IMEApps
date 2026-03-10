using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;
using empresaGlobalProj;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Win32;

namespace M2Clientes
{
    public partial class Form1 : Form
    {
        SqlConnection conexion;
        public string empresa;  
         private DataGridView.HitTestInfo hitTestInfo;
         Int32 fila;

        public Form1()
        {
            InitializeComponent();
            conexion = new SqlConnection(Utils.CD.getConexion());
            empresa = empresaGlobal.empresaID; //ConfigurationManager.AppSettings["EMPRESA"].ToString();  
            dataGridView1.AllowUserToAddRows = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Elimino primero si hay algun dato en la fila.
            Cursor.Current = Cursors.WaitCursor;
            int i = 0;

            dataGridView1.Rows.Clear();

            string ruta = textBox1.Text;
            string prefijo = textBox2.Text;
            int columnaMarca =  string.IsNullOrEmpty(textBox3.Text) ? 0 : Convert.ToInt32(textBox3.Text);
            int columnaM2 = string.IsNullOrEmpty(textBox4.Text) ? 0 : Convert.ToInt32(textBox4.Text);
            int columncantidad = string.IsNullOrEmpty(textBox5.Text) ? 0 : Convert.ToInt32(textBox5.Text);

            //Obtenemos los excels que estan en la ruta.

            /*string[] files = Directory.GetFiles(ruta);

            foreach (string f in files)
            {
                string extension = Path.GetExtension(f);

                if (extension == ".xlsx")
                {
                    procesarExcel(f, prefijo, columnaMarca, columnaM2, columncantidad);
                }
            }*/


            procesarExcel(ruta, prefijo, columnaMarca, columnaM2, columncantidad);
            Cursor.Current = Cursors.Default;
            MessageBox.Show("Excels procesados correctamente.");

        }

        private void btnSetPathNC_Click(object sender, EventArgs e)
        {
          /*  textBox1.Text = "C:\\";
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.SelectedPath = textBox1.Text;
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }*/

            OpenFileDialog openfile1 = new OpenFileDialog();
           // openfile1.Filter = "Excel Files | *.xlsx";
            openfile1.Filter = "XML Files (*.xml; *.xls; *.xlsx; *.xlsm; *.xlsb) |*.xml; *.xls; *.xlsx; *.xlsm; *.xlsb";
            openfile1.Title = "Seleccione el archivo Excel";

            if (openfile1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (openfile1.FileName.Equals("") == false)
                {
                    textBox1.Text = openfile1.FileName;
                }
            }
        }


        public void procesarExcel(string f, string prefijo, int colummarca, int columm2, int columcantidad)
        {

            

            //Abrimos el archivo excel
            var exl = new Excel.Application();

            Excel.Workbook libro = exl.Workbooks.Open(f);

            //Recorremos los hojas y las procesamos.
            foreach (Excel.Worksheet hoja in libro.Sheets)
            {
                procesarHoja(hoja.Name, f, prefijo, colummarca, columm2, columcantidad);
            }
        }

        public void procesarHoja(string nombreHoja, string f, string prefijo, int colummarca, int columm2, int columcantidad)
        {
            //Lo primero es abrir el excel y leer sus datos.
            OleDbConnection conn;
            OleDbDataAdapter myDataAdap;
            DataTable dt;

            conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;data source=" + f + ";Extended Properties='Excel 12.0 Xml;HDR=Yes'");
            myDataAdap = new OleDbDataAdapter("Select * from [" + nombreHoja + "$]", conn);
            dt = new DataTable();
            myDataAdap.Fill(dt);

            int a = dt.Columns.Count;

            int contador = 0; 

            foreach (DataRow row in dt.Rows)
            {
                string premarca = "0";
                string m2 = "0";
                string cantidad = "0";
                string perfil = "0";

                contador++; 

                if ((a > columm2 ) && (a > colummarca) && (a > columcantidad))
                {
                    premarca = Convert.ToString(row[colummarca - 1]);
                    perfil = Convert.ToString(row[columcantidad]);
                    m2 = Convert.ToString(row[columm2 - 1]);
                    cantidad = Convert.ToString(row[columcantidad - 1]);
                }

            
                string marca = prefijo + premarca;
                if (!esNumerico(perfil))
                {
                    if (existeMarca(marca))
                    {
                        // Comprobamos si los metros cuadrados son correctos.
                        if (esNumerico(m2))
                        {
                            //Insertamos el dato en la db y en caso correcto, lo mostramos en la tabla.
                            if (esNumerico(cantidad))
                            {
                                decimal m2pieza = Convert.ToDecimal(m2) / Convert.ToDecimal(cantidad);
                                insertarMetros(marca, Convert.ToString(m2pieza));
                                dataGridView1.Rows.Add(marca, Convert.ToString(m2pieza), "M2 CORRECTOS");
                            }
                            else
                            {  //Añadimos el error de que no exite la marca a la tabla.
                               //dataGridView1.Rows.Add(marca, m2, "ERROR. La CANTIDAD no es correcta.");


                            }

                        }
                        else
                        {
                            //Añadimos el error de que no exite la marca a la tabla.
                            //dataGridView1.Rows.Add(marca, m2, "ERROR. Los M2 no son correctos.");
                        }
                    }
                    else
                    {
                        //Añadimos el error de que no exite la marca a la tabla.
                        //dataGridView1.Rows.Add(marca, m2, "ERROR. La Marca no exite en el sistema.");
                    }

                }

            }
        }
        public void insertarMetros(string marca, string m2)
        {
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;

            if (esConjuntoSoldado(marca))
            {
                marca = marca + ".";
            }

            try
            {
                if (exiteM2Marca(marca))
                {
                    //Actulizado
                    conexion.Open();

                    strsql = "";
                    strsql = strsql + " update T_ARTCAR set VALOR='" + m2 + "',FECHAM=getdate(), USUMOD='" + Environment.UserName + "' ";
                    strsql = strsql + " WHERE   (CODIGO = '" + marca + "') AND (CodEMP = '" + empresa + "') AND (CARACT='20') ";
    
                    comando = new SqlCommand(strsql, conexion);
                    adapter = new SqlDataAdapter(comando);
                    table = new DataTable();
                    adapter.Fill(table);
                }
                else
                {
                    //Inserto

                    conexion.Open();

                    strsql = "";
                    strsql = strsql + " insert into T_ARTCAR (CodEMP,codigo,CATEGO,CARACT,VALOR,FECHAC,USUCRE) ";
                    strsql = strsql + " VALUES ('" + empresa + "','" + marca + "',";
                    strsql = strsql + " (SELECT CATEGORIA FROM      T_ARTICULOS WHERE   (CODIGO = '" + marca + "') AND (CodEMP = '" + empresa + "')), '20', ";
                    strsql = strsql + " '"+m2+"',getdate(), '"+Environment.UserName+"') ";

                    comando = new SqlCommand(strsql, conexion);
                    adapter = new SqlDataAdapter(comando);
                    table = new DataTable();
                    adapter.Fill(table);
                }

            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR AL INSERTAR LOS M2. POR FAVOR, CONSULTE CON EL DEPARTEMENTO DE INFORMATICA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool exiteM2Marca(string marca)
        {
            bool exite = false;
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;

            strsql = "";
            strsql = strsql + " SELECT   COUNT(*) as 'EXISTE'  ";
            strsql = strsql + " FROM      T_ARTCAR ";
            strsql = strsql + " WHERE   (CARACT = '20') AND (CodEMP = '3') AND (CODIGO = '"+marca+"') ";

            comando = new SqlCommand(strsql, conexion);
            adapter = new SqlDataAdapter(comando);
            table = new DataTable();
            adapter.Fill(table);

            conexion.Close();


            foreach (DataRow row in table.Rows)
            {
                exite = Convert.ToBoolean(row["EXISTE"]);
            }

            return exite;
        }

        public bool esConjuntoSoldado(string marca)
        {
            bool exite = false;
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;

            strsql = "";
            strsql = strsql + " SELECT   CATEGORIA as 'CATEGORIA'  ";
            strsql = strsql + " FROM      T_ARTICULOS ";
            strsql = strsql + " WHERE (CodEMP = '3') AND (CODIGO = '" + marca + "') ";

            comando = new SqlCommand(strsql, conexion);
            adapter = new SqlDataAdapter(comando);
            table = new DataTable();
            adapter.Fill(table);

            conexion.Close();

            string categoria = "";

            foreach (DataRow row in table.Rows)
            {
                categoria = Convert.ToString(row["CATEGORIA"]);
            }


            if (categoria == "92") exite = true;

            return exite;
        }
        public bool existeMarca(string marca)
        {

            bool exite = false;
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;

            strsql = "";
            strsql = strsql + " SELECT   COUNT(CODIGO) as 'EXISTE'  ";
            strsql = strsql + " FROM      T_ARTICULOS ";
            strsql = strsql + " WHERE    (CodEMP = '3') AND (CODIGO = '" + marca + "') ";

            comando = new SqlCommand(strsql, conexion);
            adapter = new SqlDataAdapter(comando);
            table = new DataTable();
            adapter.Fill(table);

            conexion.Close();


            foreach (DataRow row in table.Rows)
            {
                exite = Convert.ToBoolean(row["EXISTE"]);
            }


            return exite;
        }

        public bool esNumerico(string m2)
        {
            float n;
            bool isNumeric = float.TryParse(m2, out n);
            return isNumeric;

        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            // Activo el menu.
            if (e.Button == MouseButtons.Right)
            {
                this.hitTestInfo = this.dataGridView1.HitTest(e.X, e.Y);
                fila = hitTestInfo.RowIndex;
                Int32 columna = hitTestInfo.ColumnIndex;
                if (columna < 0)
                    return;

                contextMenuStrip1.Items[0].Enabled = true;



                contextMenuStrip1.Show(this.dataGridView1, new Point(e.X, e.Y));

            }


        }

        private void eLIMINARM2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Elimino los metros cuadrados.

            Cursor.Current = Cursors.WaitCursor;
            string texto = Convert.ToString(dataGridView1.Rows[fila].Cells[2].Value);

            string error = texto.Substring(0,5);

            if (error == "ERROR")
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Datos erroneos. No hay registros que eliminar.");
            }
            else
            {
                // Busco el dato anterior, y lo inserto.

                string marca = Convert.ToString(dataGridView1.Rows[fila].Cells[0].Value);

                string valor = ObtenerValorAnterior(marca);

                insertarMetros(marca, valor);

                MessageBox.Show("Datos eliminados correctamente.");

                dataGridView1.Rows.RemoveAt(fila);
           
            }

            
        }



        public string ObtenerValorAnterior(string marca)
        {
            string valor="";

            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;

            strsql = "";
            strsql = strsql + " SELECT   top(1) VALOR FROM      gg_log.dbo.T_ARTCAR_LOG  ";
            strsql = strsql + " WHERE   (CODIGO ='" + marca + "') AND (CARACT = '20') AND (Borrado_insertado = 'Antes') ORDER BY FechaM_Log DESC ";

            comando = new SqlCommand(strsql, conexion);
            adapter = new SqlDataAdapter(comando);
            table = new DataTable();
            adapter.Fill(table);

            conexion.Close();


            foreach (DataRow row in table.Rows)
            {
                valor = Convert.ToString(row["VALOR"]);
            }

            return valor;

        }

        private void vALIDARM2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string marca = Convert.ToString(dataGridView1.Rows[fila].Cells[0].Value);

            string valor =  Convert.ToString(dataGridView1.Rows[fila].Cells[1].Value);;

            insertarMetros(marca, valor);

            MessageBox.Show("Marca validada correctamente.");

            //dataGridView1.Rows.RemoveAt(fila);
            string esMarca = "";
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                esMarca = Convert.ToString(dataGridView1.Rows[i].Cells[0].Value);

                if (marca.Equals(esMarca) && fila !=i)
                {
                    dataGridView1.Rows.RemoveAt(i);
                }
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Cargo el form2.

            Form2 form2 = new Form2();
            form2.Show();
        }
    }
}
