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

namespace ReclamacionesSinProyecto
{
    public partial class ReclamacionesSinProyecto : Form
    {

        DataTable recsConProy = new DataTable();
        DataTable sinProy = new DataTable();

        DataTable recsConProySTR = new DataTable();
        DataTable sinProySTR = new DataTable();

        string empresa = empresaGlobal.empresaID; //ConfigurationManager.AppSettings["EMPRESA"].ToString();                        
        SqlConnection conexion = new SqlConnection(Utils.CD.getConexion());

        public ReclamacionesSinProyecto()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            DateTime now = DateTime.Now;

            DateTime firstDayOfMonth = new DateTime(now.Year, now.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            this.dateTimePicker1.Value = firstDayOfMonth;
            this.dateTimePicker2.Value = lastDayOfMonth;

            cargar();

            this.dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.dataGridView2.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView2.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            DataRow dr = null;
            int index = 0;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                dr = sinProy.NewRow();

                index = row.Index;
                dr["NUMERO"] = row.Cells[0].Value.ToString();
                dr["FECHA"] = row.Cells[1].Value.ToString();


                sinProy.Rows.Add(dr);
                recsConProy.Rows.RemoveAt(getIDRow(row.Cells[0].Value.ToString(), recsConProy));
            }

            guardar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataRow dr = null;
            int index = 0;
            foreach (DataGridViewRow row in this.dataGridView2.SelectedRows)
            {
                dr = recsConProy.NewRow();

                index = row.Index;

                dr["NUMERO"] = row.Cells[0].Value.ToString();
                dr["FECHA"] = row.Cells[1].Value.ToString();

                recsConProy.Rows.Add(dr);
                sinProy.Rows.RemoveAt(getIDRow(row.Cells[0].Value.ToString(), sinProy));
            }

            guardar();
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            recsConProy.DefaultView.RowFilter = "Convert([NUMERO], System.String) LIKE '%" + this.textBox1.Text + "%'";

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            sinProy.DefaultView.RowFilter = "Convert([NUMERO], System.String) LIKE '%" + this.textBox2.Text + "%'";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            guardar();
        }

        private void guardar()
        {
            string strsql = "";

            DataTable sinproyActual = new DataTable();

            List<int> list1 = new List<int>();

            foreach (DataRow row in sinProy.Rows)
            {
                list1.Add(Convert.ToInt32(row["NUMERO"].ToString()));
            }

            SqlCommand comando;
            SqlDataAdapter adapter;

            try
            {

                conexion.Open();


                strsql = "SELECT NUMERO_REC, cast (FECHA_REC as date) as FECHA FROM gg.dbo.TC_RECLAM_SIN_PROYECTO WHERE FECHA_REC >= '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' AND FECHA_REC <'" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                adapter.Fill(sinproyActual);

                List<int> list2 = new List<int>();

                foreach (DataRow row in sinproyActual.Rows)
                {
                    list2.Add(Convert.ToInt32(row["NUMERO_REC"].ToString()));
                }

                foreach (DataRow row in sinproyActual.Rows)
                {
                    if (!list1.Contains(Convert.ToInt32(row["NUMERO_REC"].ToString())))
                    {
                        strsql = "DELETE FROM gg.dbo.TC_RECLAM_SIN_PROYECTO WHERE NUMERO_REC = '" + row["NUMERO_REC"].ToString() + "'";
                        comando = new SqlCommand(strsql, conexion);
                        comando.ExecuteNonQuery();
                    }
                }

                foreach (DataRow row in sinProy.Rows)
                {
                    if (!list2.Contains(Convert.ToInt32(row["NUMERO"].ToString())))
                    {
                        string fecha = row["FECHA"].ToString();

                        DateTime myDate = DateTime.ParseExact(row["FECHA"].ToString(), "dd/MM/yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);

                        strsql = "INSERT INTO gg.dbo.TC_RECLAM_SIN_PROYECTO (CODEMP, NUMERO_REC, FECHA_REC, FECHAC, USUCRE) VALUES ('" + empresa + "', '" + row["NUMERO"].ToString() + "', '" + myDate.ToString("yyyy/MM/dd") + "', GETDATE(), '" + Environment.UserName + "')";
                        comando = new SqlCommand(strsql, conexion);
                        comando.ExecuteNonQuery();
                    }
                }

                conexion.Close();

                sinProy.AcceptChanges();

                this.dataGridView1.Sort(this.dataGridView1.Columns["NUMERO"], ListSortDirection.Ascending);
                this.dataGridView2.Sort(this.dataGridView2.Columns["NUMERO"], ListSortDirection.Ascending);

                //MessageBox.Show("Cambios guardados correctamente", "ATENCIÓN");
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (((DataTable)dataGridView2.DataSource).GetChanges() != null)
            {
                DialogResult dialogResult = MessageBox.Show("Hay cambios sin guardar, ¿Salir de todas formas?", "SALIR", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    //guardarCambios();
                }
                else if (dialogResult == DialogResult.No)
                {
                    e.Cancel = true;
                }

            }
        }

        private void cargar()
        {
            recsConProy.Clear();
            sinProy.Clear();

            Cursor.Current = Cursors.WaitCursor;

            SqlCommand comando;
            SqlDataAdapter adapter;
            string strsql;

            try
            {

                conexion.Open();

                strsql = "select	numero2 AS NUMERO, format (cast (fechadeentrada as date), 'd', 'es-es') AS FECHA  ";
                strsql += "from	( ";
                strsql += "		select	fechadeentrada, ";
                strsql += "				case CHARINDEX('_A',numero) ";
                strsql += "					when 0 then LEFT(numero,len(numero) - 3) ";
                strsql += "					else LEFT(numero,CHARINDEX('_A',numero) - 1) ";
                strsql += "				end as numero2 ";
                strsql += "		from	calidad.dbo.ObtenerDatosReclamaciones('http://sistemacalidad','usuario_sharepoint','usuario_sharepoint@imedexsa','IMEDEXSA') ";
                strsql += "		where	aceptada = 'Si' and ";
                strsql += "				fechadeentrada >= '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and ";
                strsql += "				fechadeentrada <= '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' and ";
                strsql += "				CAST(perfiles as float) > 0 ";
                strsql += "		) as t1 ";
                strsql += "where	( ";
                strsql += "		select	COUNT(*) ";
                strsql += "		from	TC_LANZA ";
                strsql += "		where	 ";
                strsql += "				CODEMP = 3 and ";
                strsql += "				VALIDADO = 'S' and ";
                strsql += "				FECHAC >= fechadeentrada and ";
                strsql += "				FECHAC <= '12/31/' + cast(year(GETDATE()) as varchar) and ";
                strsql += "				(replace(replace(PROYECTO,'Ó','O'),'RECLAMACION ','') = t1.numero2 or ";
                strsql += "				 replace(replace(PROYECTO,'Ó','O'),'RECLAMACION ','') = '0'+ t1.numero2 or ";
                strsql += "				 replace(replace(PROYECTO,'Ó','O'),'RECLAMACION ','') like '%/'+ t1.numero2 or ";
                strsql += "				 replace(replace(PROYECTO,'Ó','O'),'RECLAMACION ','') like t1.numero2 + '/%' or ";
                strsql += "				 replace(replace(PROYECTO,'Ó','O'),'RECLAMACION ','') like '%/'+ t1.numero2 + '/%' or ";
                strsql += "				 replace(replace(PROYECTO,'Ó','O'),'RECLAMACION ','') like '%-'+ t1.numero2 or ";
                strsql += "				 replace(replace(PROYECTO,'Ó','O'),'RECLAMACION ','') like t1.numero2 + '-%' or ";
                strsql += "				 replace(replace(PROYECTO,'Ó','O'),'RECLAMACION ','') like '%-'+ t1.numero2 + '-%' or ";
                strsql += " ";
                strsql += "				 replace(replace(PROYECTO,'Ó','O'),'RECLAMACION Nº ','') = t1.numero2 or ";
                strsql += "				 replace(replace(PROYECTO,'Ó','O'),'RECLAMACION Nº ','') = '0'+ t1.numero2 or ";
                strsql += "				 replace(replace(PROYECTO,'Ó','O'),'RECLAMACION Nº ','') like '%/'+ t1.numero2 or ";
                strsql += "				 replace(replace(PROYECTO,'Ó','O'),'RECLAMACION Nº ','') like t1.numero2 + '/%' or ";
                strsql += "				 replace(replace(PROYECTO,'Ó','O'),'RECLAMACION Nº ','') like '%/'+ t1.numero2 + '/%' or ";
                strsql += "				 replace(replace(PROYECTO,'Ó','O'),'RECLAMACION Nº ','') like '%-'+ t1.numero2 or ";
                strsql += "				 replace(replace(PROYECTO,'Ó','O'),'RECLAMACION Nº ','') like t1.numero2 + '-%' or ";
                strsql += "				 replace(replace(PROYECTO,'Ó','O'),'RECLAMACION Nº ','') like '%-'+ t1.numero2 + '-%' or ";
                strsql += " ";
                strsql += "				 replace(replace(PROYECTO,'Ó','O'),'RECLAMACION Nº','') = t1.numero2 or ";
                strsql += "				 replace(replace(PROYECTO,'Ó','O'),'RECLAMACION Nº','') = '0'+ t1.numero2 or ";
                strsql += "				 replace(replace(PROYECTO,'Ó','O'),'RECLAMACION Nº','') like '%/'+ t1.numero2 or ";
                strsql += "				 replace(replace(PROYECTO,'Ó','O'),'RECLAMACION Nº','') like t1.numero2 + '/%' or ";
                strsql += "				 replace(replace(PROYECTO,'Ó','O'),'RECLAMACION Nº','') like '%/'+ t1.numero2 + '/%' or ";
                strsql += "				 replace(replace(PROYECTO,'Ó','O'),'RECLAMACION Nº','') like '%-'+ t1.numero2 or ";
                strsql += "				 replace(replace(PROYECTO,'Ó','O'),'RECLAMACION Nº','') like t1.numero2 + '-%' or ";
                strsql += "				 replace(replace(PROYECTO,'Ó','O'),'RECLAMACION Nº','') like '%-'+ t1.numero2 + '-%' ";
                strsql += "				 ) ";
                strsql += "		) = 0 ";
                strsql += "		and cast(numero2 as int) not in (select NUMERO_REC from TC_RECLAM_SIN_PROYECTO where CODEMP = 3 and FECHA_REC = fechadeentrada) ";
                strsql += "		order by cast(numero2 as int) ";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                adapter.Fill(recsConProy);


                strsql = "SELECT cast (NUMERO_REC as int) AS NUMERO, FORMAT(cast(FECHA_REC as date), 'd', 'es-es') as FECHA FROM gg.dbo.TC_RECLAM_SIN_PROYECTO WHERE FECHA_REC >= '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' AND FECHA_REC <'" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                adapter.Fill(sinProy);

                //sinProy.Columns["NUMERO"].DataType = Type.GetType("System.Int32");
                //recsConProy.Columns["NUMERO"].DataType = Type.GetType("System.Int32");

                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.dataGridView1.DataSource = recsConProy;
            this.dataGridView2.DataSource = sinProy;

            this.dataGridView1.SortCompare += customSortCompare;
            this.dataGridView2.SortCompare += customSortCompare;

            this.dataGridView1.Sort(this.dataGridView1.Columns["NUMERO"], ListSortDirection.Ascending);
            this.dataGridView2.Sort(this.dataGridView2.Columns["NUMERO"], ListSortDirection.Ascending);

            Cursor.Current = Cursors.Default;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cargar();
        }

        private void customSortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            int a = int.Parse(e.CellValue1.ToString()), b = int.Parse(e.CellValue2.ToString());

            // If the cell value is already an integer, just cast it instead of parsing

            e.SortResult = a.CompareTo(b);

            e.Handled = true;
        }

        private int getIDRow(string numeroRec, DataTable table)
        {
            int index = -1;
            int counter = 0;
            bool found = false;

            foreach (DataRow row in table.Rows)
            {
                if (row["NUMERO"].ToString() == numeroRec)
                {
                    index = counter;
                }
                    counter++;
            }

            return index;
        }


    }
}
