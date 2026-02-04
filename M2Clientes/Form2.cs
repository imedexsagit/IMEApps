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
using AdvancedDataGridView;

namespace M2Clientes
{
    public partial class Form2 : Form
    {
        SqlConnection conexion;
        public string empresa; 
 

        public Form2()
        {
            InitializeComponent();
            conexion = new SqlConnection(Utils.CD.getConexion());
            empresa = empresaGlobal.empresaID; //ConfigurationManager.AppSettings["EMPRESA"].ToString();  
        }

        private void button2_Click(object sender, EventArgs e)
        {
           /* string pedido = textBox1.Text;
            string linea = textBox2.Text;


            string marca = obtenerCodigoLineaPedido(pedido,linea);

            if (existeMarca(marca))
            {

                // LLamo a la funcion que te devuelve la estructura con los metros cuadrados.

                DataTable dt = datosDateTable(marca);


                
                String codigo = dt.Rows[0][5].ToString();

               
                String m2imedexsa = dt.Rows[0][13].ToString();
                String m2cliente = dt.Rows[0][14].ToString();

            /*    treeGridView1.Nodes.Clear();

                TreeGridNode node = treeGridView1.Nodes.Add(codigo,dt.Rows[0][6].ToString(),
                    dt.Rows[0][8].ToString(),dt.Rows[0][9].ToString(),m2imedexsa, m2cliente);
                node.Expand();
                

               
                for (int x = 1; x < dt.Rows.Count; x++)
                {

                    
                      string codigoPadre = dt.Rows[x][2].ToString();

                      if (codigoPadre == codigo)
                      {
                          string codigoHijo = dt.Rows[x][5].ToString();
                        //  TreeGridNode node1 = node.Nodes.Add(dt.Rows[x][5].ToString(),dt.Rows[x][6].ToString(),
                //    dt.Rows[x][8].ToString(),dt.Rows[x][9].ToString(), dt.Rows[x][13].ToString(), dt.Rows[x][14].ToString());

                          for (int y = 1; y < dt.Rows.Count; y++)
                          {
                              //Nietos
                              string codigoNieto = dt.Rows[y][2].ToString();

                              if (codigoNieto == codigoHijo)
                              {

                                  TreeGridNode node2 = node1.Nodes.Add(dt.Rows[y][5].ToString(), dt.Rows[y][6].ToString(),
                                    dt.Rows[y][8].ToString(),dt.Rows[y][9].ToString(),dt.Rows[y][13].ToString(), dt.Rows[y][14].ToString());

                                  for (int z = 1; z < dt.Rows.Count; z++)
                                  {
                                      //Nietos
                                      string codigoBiNieto = dt.Rows[z][2].ToString();

                                      if (codigoNieto == codigoBiNieto)
                                      {

                                          TreeGridNode node3 = node1.Nodes.Add(dt.Rows[z][5].ToString(), dt.Rows[z][6].ToString(),
                                            dt.Rows[z][8].ToString(), dt.Rows[z][9].ToString(), dt.Rows[z][13].ToString(), dt.Rows[z][14].ToString());
                                      }
                                  }
                              }
                          }

                      }
                }




            }*/
        }


        public void añadirNodosHijos(DataTable dt,TreeGridNode nodo, String codigo)
        {

             for (int x = 1; x < dt.Rows.Count; x++)
            {

                string codigoFor = dt.Rows[0][2].ToString();

                if (codigoFor == codigo)
                {

                    

                     String codigoh = dt.Rows[x][5].ToString();
                    String m2imedexsa = dt.Rows[x][13].ToString();
                    String m2cliente = dt.Rows[x][14].ToString();


                    TreeGridNode nodoHijo = nodo.Nodes.Add(codigoh, m2imedexsa, m2cliente); //TreeGridNode
                    nodo.Expand();
                    añadirNieto(dt,nodoHijo, codigoh);


                }
             }

            
        }

        public void añadirNieto(DataTable dt, TreeGridNode nodo, String codigo)
        {

            for (int x = 1; x < dt.Rows.Count; x++)
            {

                string codigoFor = dt.Rows[0][2].ToString();

                if (codigoFor == codigo)
                {



                    String codigoh = dt.Rows[x][5].ToString();
                    String m2imedexsa = dt.Rows[x][13].ToString();
                    String m2cliente = dt.Rows[x][14].ToString();


                    TreeGridNode nodoHijo = nodo.Nodes.Add(codigoh, m2imedexsa, m2cliente);
                    nodo.Expand();
                    añadirNieto(dt, nodoHijo, codigoh);


                }
            }


        }

        public string obtenerCodigoLineaPedido(string pedido, string linea)
        {
            string  codigo = "";
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;

            strsql = "";
            strsql = strsql + " SELECT  CODIGO FROM      T_ORDTERL  ";
            strsql = strsql + " WHERE   (NUMPED = "+pedido+") AND (LINEA = "+linea+") AND (CODEMP = '"+empresa+"') ";

            comando = new SqlCommand(strsql, conexion);
            adapter = new SqlDataAdapter(comando);
            table = new DataTable();
            adapter.Fill(table);

            conexion.Close();


            foreach (DataRow row in table.Rows)
            {
                codigo = Convert.ToString(row["CODIGO"]);
            }


            return codigo;

        }



        public DataTable datosDateTable(string codigo)
        {
           
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;

            strsql = "";
            strsql = strsql + " exec IME_Lista_Almacen_m2 '"+empresa+"','0','"+codigo+"','1'  ";
            

            comando = new SqlCommand(strsql, conexion);
            adapter = new SqlDataAdapter(comando);
            table = new DataTable();
            adapter.Fill(table);


            conexion.Close();


            foreach (DataRow row in table.Rows)
            {
               
            }

            return table;

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
            strsql = strsql + " WHERE    (CodEMP = '"+empresa+"') AND (CODIGO = '" + marca + "') ";

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

        private void treeGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
