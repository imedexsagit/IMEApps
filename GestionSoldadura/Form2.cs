using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace GestionSoldadura
{
    public partial class Form2 : Form
    {

        DataTable table = new DataTable();
        public Form2()
        {
            InitializeComponent();
        }



        private void Form2_Load(object sender, EventArgs e)
        {

            SqlConnection conexion = null;
            SqlCommand comando = null;
            string strsql;

            string connString = "Data Source=srvsql02;Initial Catalog=gg;User ID=gg;Password=ostia";

            conexion = new SqlConnection(connString);
            conexion.Open();

            SqlDataAdapter adapter;

            strsql = "exec gestionSolEstadProyectos";

            comando = new SqlCommand(strsql, conexion);

            System.Diagnostics.Debug.WriteLine(" La query que se va a hacer es: " + strsql);

            adapter = new SqlDataAdapter(comando);

            comando.CommandText = strsql;
            table.Columns.Clear();
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            conexion.Close();

            int i = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                try
                {
                    if (row.Cells["CLIENTE"].Value != null)
                    {
                        comboCliente.Items.Add(row.Cells["CLIENTE"].Value.ToString());
                    }

                    if (row.Cells["CLIENTE"].Value != null)
                    {
                        if (Convert.ToBoolean(row.Cells["finalizado"].Value.ToString()) == true)
                        {
                            this.dataGridView1.Rows[i].Cells[0].Style.BackColor = Color.GreenYellow;
                            this.dataGridView1.Rows[i].Cells[1].Style.BackColor = Color.GreenYellow;
                            this.dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.GreenYellow;
                            this.dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.GreenYellow;
                            this.dataGridView1.Rows[i].Cells[4].Style.BackColor = Color.GreenYellow;
                            this.dataGridView1.Rows[i].Cells[5].Style.BackColor = Color.GreenYellow;
                            this.dataGridView1.Rows[i].Cells[6].Style.BackColor = Color.GreenYellow;
                            this.dataGridView1.Rows[i].Cells[7].Style.BackColor = Color.GreenYellow;
                            this.dataGridView1.Rows[i].Cells[8].Style.BackColor = Color.GreenYellow;
                        }
                    }

                    i++; 
                }
                catch (ArgumentOutOfRangeException outOfRange)
                {
                    Console.WriteLine("Error: {0}", outOfRange.Message);
                }
            }


        }


        private void comboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {

            /*SqlConnection conexion = null;
            SqlCommand comando = null;
            string strsql;
            string valor = comboCliente.Text;
            string connString = "Data Source=srvsql02;Initial Catalog=gg;User ID=gg;Password=ostia";

            conexion = new SqlConnection(connString);
            conexion.Open();

            SqlDataAdapter adapter;

            strsql = "select * from TC_SoldaduraProyecto where Cliente = '" + valor + "'";

            comando = new SqlCommand(strsql, conexion);

            System.Diagnostics.Debug.WriteLine(" La query que se va a hacer es: " + strsql);

            adapter = new SqlDataAdapter(comando);

            comando.CommandText = strsql;
            table.Columns.Clear();
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;



            conexion.Close();*/
            string valor = comboCliente.Text;

            this.dataGridView1.CurrentCell = null;

        int numFilas = Convert.ToInt32(this.dataGridView1.Rows.Count.ToString());


            for (int i=0; i < numFilas-1; i++)
            {

                if (this.dataGridView1.Rows[i].Cells[1].Value.ToString().Equals(valor))
                {
                    this.dataGridView1.Rows[i].Visible = true;
                }
                else
                {
                    this.dataGridView1.Rows[i].Visible = false;
                }

            }

            
        }
    }
}
