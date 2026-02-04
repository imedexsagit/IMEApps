using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Configuration;
using Intranet;
using AdvancedDataGridView;

namespace Intranet
{
    public partial class elegirColsInforme : Form
    {

        public List<Tuple<string, string>> listCodeDenom = new List<Tuple<string, string>>();
        public DataTable listCodeDenom1 = Intranet.list1;//new DataTable();
        public int nCodes = Intranet.list1.Rows.Count;
        public DataTable copylistCodeDenom1;
        //public Intranet cls = new Intranet();

        public elegirColsInforme()
        {
            InitializeComponent();

        }

        //Método de creación del informe excel
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            DataGridView newDGV = new DataGridView();
            Cursor.Current = Cursors.WaitCursor;
            if (Intranet.copylist1 != null)
            {
                System.Diagnostics.Debug.WriteLine(" --- Intranet.copylist1 no está vacío ---");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(" --- Intranet.copylist1 Vacío");
            }

            string message = string.Empty;
            listCodeDenom1 = Intranet.list1;

            //Datatable que se crea para cuando no se cambia de código en el DataGridView
            //if para evitar excel vacío cuando No se cambia de código buscado
            if (listCodeDenom1.Rows.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine(" Se copia listCodeDenom1 a lo que contiene Intranet.copylist1");
                listCodeDenom1 = Intranet.copylist1;
            }

            //List<Tuple<T1, T2>> list;
            List<Tuple<string, string>> caracSeleccionadas = new List<Tuple<string, string>>();

            //Datatable que se vuelca y contendrá código, denominación característica, valor característica
            DataTable dtExcelInforme = new DataTable();
            dtExcelInforme = listCodeDenom1;

            foreach (DataGridViewRow row in colsInforme.Rows)
            {
                //Recorre las filas comprobando cuales han sido seleccionadas
                bool isSelected = Convert.ToBoolean(row.Cells["SelectCol"].Value);
                if (isSelected)
                {
                    message += Environment.NewLine;
                    message += row.Cells["CARACT"].Value.ToString() + row.Cells["DENOMI"].Value.ToString();

                    //se añaden las características seleccionadas a una lista
                    caracSeleccionadas.Add(Tuple.Create(row.Cells["CARACT"].Value.ToString(), row.Cells["DENOMI"].Value.ToString()));
                }
                //caracSeleccionadas.Add(Tuple.Create("%PeeeeeESO", "%PeeeeeESO"));

            }

            //MessageBox.Show("Características seleccionadas" + message);

            //almacenar el nº de columnas * 2 valor y característica, + 2 ya que code y denominación siempre están para Limpiar Columnas innecesarias
            int ncol = (caracSeleccionadas.Count * 2) + 2;

            //se borran las columnas no utilizadas
            //System.Diagnostics.Debug.WriteLine(" [[[[[[[[ el nº de columnas total es: " + ncol + " y el nº de columnas de la DataTable es: " + dtExcelInforme.Columns.Count);

            int borrarCol = dtExcelInforme.Columns.Count;
            while (borrarCol >= ncol && ncol != dtExcelInforme.Columns.Count)
            {
                System.Diagnostics.Debug.WriteLine(" Dentro de borrar columnas, se borra la col en el índice: " + ncol);
                //dtExcelInforme.Columns.RemoveAt(ncol);
                //dtExcelInforme.Columns.Remove("CARACT" + (lastvaluei));
                //dtExcelInforme.Columns.Remove("VALOR" + (lastvaluei));
                dtExcelInforme.Columns.RemoveAt(ncol);
                //ncol++;

            }

            DataColumnCollection columns = dtExcelInforme.Columns;
            dtExcelInforme.Columns.Cast<DataColumn>().Where(col => col.ColumnName != "Código" && col.ColumnName != "Denominación").ToList().ForEach(col => dtExcelInforme.Columns.Remove(col.ColumnName));

            //Añadir columnas
            for (int i = 0; i < caracSeleccionadas.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine(" %%%%%%%%%%%%%% Bucle for de características seleccionadas, el código es: " + caracSeleccionadas[i].ToString());
                if (!columns.Contains(caracSeleccionadas[i].Item2.ToString()))
                {
                    //dtExcelInforme.Columns.Add("CARACT" + (i), typeof(System.String));
                    System.Diagnostics.Debug.WriteLine(" La columna que se añade en la iteración: " + i + " es " + caracSeleccionadas[i].ToString());
                    dtExcelInforme.Columns.Add(caracSeleccionadas[i].Item2.ToString(), typeof(System.String));
                    //dtExcelInforme.Columns.Add("VALOR" + (i), typeof(System.String));
                }
            }


            //Consultas a la base de datos, hechas las consultas se añaden a la DataTable
            string strsql;
            SqlConnection conexion = null;
            string connString = @"Data Source=db-imedexsa;Initial Catalog=gg;User ID=gg;Password=ostia"; //ConfigurationManager.ConnectionStrings["Interface_Usuario.Properties.Settings.CS_CPP_0713ConnectionString"].ConnectionString;
            SqlCommand comando = null;
            SqlDataAdapter adapter;

            DataTable table = new DataTable();

            System.Diagnostics.Debug.WriteLine(" El nº de códigos es: " + nCodes + " y el nº de características seleccionadas es: " + caracSeleccionadas.Count);
            string name = "";
            List<TreeGridNode> node_List = new List<TreeGridNode>();
            TreeGridNode node = new TreeGridNode();
           // int pos = 0;
            foreach (DataRow row in dtExcelInforme.Rows)
            {
                newDGV = Intranet.obtenerMixMateriales(row["Código"].ToString());
                System.Diagnostics.Debug.WriteLine(" Inicial Value getMaterialesInforme: " + newDGV[1, 2].Value);
                //Intranet.getMaterialesInforme.Actualizar(row["Código"], node_List);
                for (int j = 0; j < caracSeleccionadas.Count; j++)
                {

                    name = caracSeleccionadas[j].Item1.ToString();
                    if (!name.StartsWith("n"))
                    {

                        System.Diagnostics.Debug.WriteLine(" Iteración i: Iteración j " + j);

                        conexion = new SqlConnection(connString);
                        conexion.Open();

                        strsql = "";
                        strsql = strsql + " SELECT  ac.CARACT, ac.VALOR, c.DENOMI";
                        strsql = strsql + " FROM    T_ARTCAR ac JOIN T_CARACT c ON ac.CARACT = c.CARACT";
                        strsql = strsql + " WHERE   ac.CODIGO = '" + row["Código"] + "' AND ac.CARACT = '" + caracSeleccionadas[j].Item1.ToString() + "'";
                        System.Diagnostics.Debug.WriteLine(" La query que se va a hacer es: " + strsql);
                        comando = new SqlCommand(strsql, conexion);
                        adapter = new SqlDataAdapter(comando);
                        table = new DataTable();

                        comando.CommandText = strsql;
                        table.Columns.Clear();
                        table.Clear();
                        adapter.Fill(table);

                        DataRow rowInsert = dtExcelInforme.NewRow();


                        if (table.Rows.Count > 0)
                        {
                            //System.Diagnostics.Debug.WriteLine(" table caract " + " prueba dtValor+j: " + dtExcelInforme.Columns[caracSeleccionadas[j].ToString()].Expression);
                            //System.Diagnostics.Debug.WriteLine(" table valor " + " prueba dtValor+j: " + dtExcelInforme.Columns["VALOR" + j].Expression);

                            //row["CARACT" + j] = table.Rows[0]["DENOMI"].ToString();
                            row[caracSeleccionadas[j].Item2.ToString()] = table.Rows[0]["VALOR"].ToString();
                            //row["VALOR" + j] = table.Rows[0]["VALOR"].ToString();
                        }
                        conexion.Close();


                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine(" No es para la base de datos");
                        /*if (caracSeleccionadas[j].Item1.ToString() == "n2") //Este If funciona
                        {
                            System.Diagnostics.Debug.WriteLine(" Value getMaterialesInforme: " + Intranet.getMaterialesInforme[1, 1].Value);
                            //dtExcelInforme.Columns.Add(caracSeleccionadas[j].Item2.ToString(), typeof(System.String));
                            row[caracSeleccionadas[j].Item2.ToString()] = newDGV[1, 2].Value;//Intranet.getMaterialesInforme[1, 2].Value; //newDGV[1, 2].Value //aquí se introduce el valor del DataGrid
                        }*/

                        //si se añade una columna manual más se debe de añadir debajo en GetCaracteristicasList y en este switch
                        switch (caracSeleccionadas[j].Item1.ToString())
                        {
                            case "n1":
                                System.Diagnostics.Debug.WriteLine(" Value getMaterialesInforme: " + Intranet.getMaterialesInforme[1, 1].Value);
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[1, 1].Value;
                                break;
                            case "n2":
                                System.Diagnostics.Debug.WriteLine(" Value getMaterialesInforme: " + Intranet.getMaterialesInforme[1, 2].Value);
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[1, 2].Value;
                                break;
                            case "n3":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[1, 3].Value;
                                break;
                            case "n4":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[1, 4].Value;
                                break;
                            case "n5":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[1, 5].Value;
                                break;
                            case "n6":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[1, 6].Value;
                                break;
                            case "n7":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[1, 7].Value;
                                break;
                            case "n8":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[1, 8].Value;
                                break;
                            case "n9":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[1, 9].Value;
                                break;
                            case "n10":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[1, 10].Value;
                                break;
                            case "n11":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[1, 11].Value;
                                break;
                            case "n12":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[1, 12].Value;
                                break;
                            case "n13":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[2, 1].Value;
                                break;
                            case "n14":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[2, 2].Value;
                                break;
                            case "n15":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[2, 3].Value;
                                break;
                            case "n16":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[2, 4].Value;
                                break;
                            case "n17":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[2, 5].Value;
                                break;
                            case "n18":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[2, 6].Value;
                                break;
                            case "n19":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[2, 7].Value;
                                break;
                            case "n20":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[2, 8].Value;
                                break;
                            case "n21":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[2, 9].Value;
                                break;
                            case "n22":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[2, 10].Value;
                                break;
                            case "n23":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[2, 11].Value;
                                break;
                            case "n24":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[2, 12].Value;
                                break;
                            case "n25":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[4, 8].Value;
                                break;
                            case "n26":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[4, 9].Value;
                                break;
                            case "n27":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[4, 10].Value;
                                break;
                            case "n28":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[4, 11].Value;
                                break;
                            case "n29":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[5, 1].Value;
                                break;
                            case "n30":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[5, 2].Value;
                                break;
                            case "n31":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[5, 3].Value;
                                break;
                            case "n32":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[5, 4].Value;
                                break;
                            case "n33":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[5, 5].Value;
                                break;
                            case "n34":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[5, 6].Value;
                                break;
                            case "n35":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[6, 1].Value;
                                break;
                            case "n36":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[6, 2].Value;
                                break;
                            case "n37":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[6, 3].Value;
                                break;
                            case "n38":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[6, 4].Value;
                                break;
                            case "n39":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[6, 5].Value;
                                break;
                            case "n40":
                                row[caracSeleccionadas[j].Item2.ToString()] = newDGV[6, 6].Value;
                                break;
                            //default:
                                //return false;
                        }
                        
                        /*DataRow drw = dtExcelInforme.Rows[1];
                        drw[1] = table.Rows[0]["VALOR"].ToString(); ;*/
                    }
                }
                //pos++;
            }


            
            /* foreach (DataGridViewRow rowCaract in colsInforme.Rows)
             {
                 //Recorre las filas comprobando cuales han sido seleccionadas
                 bool isSelected = Convert.ToBoolean(rowCaract.Cells["SelectCol"].Value);
                 if (isSelected)
                 {
                     message += Environment.NewLine;
                     message += rowCaract.Cells["CARACT"].Value.ToString() + rowCaract.Cells["DENOMI"].Value.ToString();

                     bool pr = Convert.ToBoolean(rowCaract.Cells["SelectCol"].Value);
                     //se añaden las características seleccionadas a una lista
                     if (rowCaract.Cells["CARACT"].Value.ToString() == "%Peso M.P.") 
                     {
                         dtExcelInforme.Columns.Add(rowCaract.Cells["CARACT"].Value.ToString(), typeof(System.String));
                         DataRow drw = dtExcelInforme.Rows[1];
                         drw[1] = table.Rows[0]["VALOR"].ToString(); ;

                     }
                 }
                 //caracSeleccionadas.Add(Tuple.Create("%PeeeeeESO", "%PeeeeeESO"));

             }*/

            System.Diagnostics.Debug.WriteLine(" --- Se sale de los bucles ---");

            //Datatable que se crea para cuando no se cambia de código
            Intranet.copylist1 = listCodeDenom1.Copy();
            if (copylistCodeDenom1 != null)
            {
                System.Diagnostics.Debug.WriteLine(" --- copylistCodeDenom1 no está vacío ---");
            }

            ExportToExcelInforme.DataTableToExcelInforme(dtExcelInforme, "");
            dtExcelInforme.Clear();
            listCodeDenom1.Clear();


            table.Clear();
            base.Close();
            Cursor.Current = Cursors.Default;
        }

        //Carga de las características y se muestran en el DataGridView
        public void FormCaracteristicas_Load(object sender, EventArgs e)
        {
            //este valor tiene la celda actual, obtenerla y ponerla en el GetCaracteristicasList
            string codigo = Intranet.currentDgResultadosRow;
            colsInforme.DataSource = GetCaracteristicasList(codigo);
            //colsInforme.Rows.Add


        }

        //Método para obtener todas las características y permitir elegirlas
        public DataTable GetCaracteristicasList(string codigo)
        {

            listCodeDenom = Intranet.list;
            //En esta lista están todos los códigos y denominaciones para el excel

            /*foreach (DataRow row in listCodeDenom1.Rows)
            {
                System.Diagnostics.Debug.WriteLine(" Código son: " + row["Código"].ToString() + " y denominación es " + row["Denominación"].ToString());// + listCodeDenom[i].ToString());
            }*/


            codigo = Intranet.currentDgResultadosRow;
            System.Diagnostics.Debug.WriteLine(" =????????????=???????????????= Caracteristicas elegirColsInforme, el código es: " + codigo);
            DataTable table = new DataTable();

            string connString = @"Data Source=db-imedexsa;Initial Catalog=gg;User ID=gg;Password=ostia"; //ConfigurationManager.ConnectionStrings["Interface_Usuario.Properties.Settings.CS_CPP_0713ConnectionString"].ConnectionString;
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            //string categoria;

            ControlAccesos.ControlAcceso controlAcceso = new ControlAccesos.ControlAcceso();
            bool MostramosPesoTeorico = controlAcceso.TieneAcceso("PesoTeorico");

            conexion = new SqlConnection(connString);
            conexion.Open();

            strsql = "";
            strsql = strsql + " SELECT  CARACT, DENOMI";
            strsql = strsql + " FROM    T_CARACT";
            strsql = strsql + " WHERE   CARACT >= '500' AND CARACT != 'MAT' AND CARACT != 'KGN' AND CARACT != 'LON' AND CARACT != 'MAR' AND CARACT != 'PES' AND CARACT != 'T-P' ORDER BY CARACT";

            comando = new SqlCommand(strsql, conexion);
            adapter = new SqlDataAdapter(comando);
            table = new DataTable();

            comando.CommandText = strsql;
            table.Columns.Clear();
            table.Clear();


            adapter.Fill(table);

            //Añadir columnas deseadas más allá de la base de datos aquí, algunas comentadas porque no son necesarias
            //Se añaden las características de Peso porcentual
            table.Rows.Add(new object[] { "n1", "%Peso M.P." });
            table.Rows.Add(new object[] { "n2", "%Peso Angular" });
            table.Rows.Add(new object[] { "n3", "%Peso Pletina" });
            table.Rows.Add(new object[] { "n4", "%Peso Chapa" });
            table.Rows.Add(new object[] { "n5", "%Peso UPN-IPN-HEB" });
            table.Rows.Add(new object[] { "n6", "%Peso Tub-Red" });
            table.Rows.Add(new object[] { "n7", "%Peso Tornillería" });
            table.Rows.Add(new object[] { "n8", "%Peso Estructural" });
            table.Rows.Add(new object[] { "n9", "%Peso Pates" });
            table.Rows.Add(new object[] { "n10", "%Peso Pernos" });
            table.Rows.Add(new object[] { "n11", "%Peso Auxiliares" });
            table.Rows.Add(new object[] { "n12", "%Peso M.P. + Torn." });
            //Se añaden las características de Peso (Kg)
            table.Rows.Add(new object[] { "n13", "Peso(Kg) M.P." });
            table.Rows.Add(new object[] { "n14", "Peso(Kg) Angular" });
            table.Rows.Add(new object[] { "n15", "Peso(Kg) Pletina" });
            table.Rows.Add(new object[] { "n16", "Peso(Kg) Chapa" });
            table.Rows.Add(new object[] { "n17", "Peso(Kg) UPN-IPN-HEB" });
            table.Rows.Add(new object[] { "n18", "Peso(Kg) Tub-Red" });
            table.Rows.Add(new object[] { "n19", "Peso(Kg) Tornillería" });
            table.Rows.Add(new object[] { "n20", "Peso(Kg) Estructural" });
            table.Rows.Add(new object[] { "n21", "Peso(Kg) Pates" });
            table.Rows.Add(new object[] { "n22", "Peso(Kg) Pernos" });
            table.Rows.Add(new object[] { "n23", "Peso(Kg) Auxiliares" });
            table.Rows.Add(new object[] { "n24", "Peso(Kg) M.P. + Torn." });
            //Se añaden las características de Coef. sobre Teórico
            /*table.Rows.Add(new object[] { "n25", "Coef. sobre Teórico M.P." });
            table.Rows.Add(new object[] { "n26", "Coef. sobre Teórico Angular" });
            table.Rows.Add(new object[] { "n27", "Coef. sobre Teórico Pletina" });
            table.Rows.Add(new object[] { "n28", "Coef. sobre Teórico Chapa" });
            table.Rows.Add(new object[] { "n29", "Coef. sobre Teórico UPN-IPN-HEB" });
            table.Rows.Add(new object[] { "n30", "Coef. sobre Teórico Tub-Red" });
            table.Rows.Add(new object[] { "n31", "Coef. sobre Teórico Tornillería" });
            table.Rows.Add(new object[] { "n32", "Coef. sobre Teórico Estructural" });
            table.Rows.Add(new object[] { "n33", "Coef. sobre Teórico Pates" });
            table.Rows.Add(new object[] { "n34", "Coef. sobre Teórico Pernos" });
            table.Rows.Add(new object[] { "n35", "Coef. sobre Teórico Auxiliares" });
            table.Rows.Add(new object[] { "n36", "Coef. sobre Teórico M.P. + Torn." });*/
            //Se añaden las características de Cant.
            /*table.Rows.Add(new object[] { "n37", "Cant. M.P." });
            table.Rows.Add(new object[] { "n38", "Cant. Angular" });
            table.Rows.Add(new object[] { "n39", "Cant. Pletina" });
            table.Rows.Add(new object[] { "n40", "Cant. Chapa" });
            table.Rows.Add(new object[] { "n41", "Cant. UPN-IPN-HEB" });
            table.Rows.Add(new object[] { "n42", "Cant. Tub-Red" });
            table.Rows.Add(new object[] { "n43", "Cant. Tornillería" });*/
            table.Rows.Add(new object[] { "n25", "Cant. Estructural" });
            table.Rows.Add(new object[] { "n26", "Cant. Pates" });
            table.Rows.Add(new object[] { "n27", "Cant. Pernos" });
            table.Rows.Add(new object[] { "n28", "Cant. Auxiliares" });
            //table.Rows.Add(new object[] { "n48", "Cant. M.P. + Torn." });
            //Se añaden las características de Punz. (Kg)
            table.Rows.Add(new object[] { "n29", "Punz. (Kg) M.P." });
            table.Rows.Add(new object[] { "n30", "Punz. (Kg) Angular" });
            table.Rows.Add(new object[] { "n31", "Punz. (Kg) Pletina" });
            table.Rows.Add(new object[] { "n32", "Punz. (Kg) Chapa" });
            table.Rows.Add(new object[] { "n33", "Punz. (Kg) UPN-IPN-HEB" });
            table.Rows.Add(new object[] { "n34", "Punz. (Kg) Tub-Red" });
            /*table.Rows.Add(new object[] { "n55", "Punz. (Kg) Tornillería" });
            table.Rows.Add(new object[] { "n56", "Punz. (Kg) Estructural" });
            table.Rows.Add(new object[] { "n57", "Punz. (Kg) Pates" });
            table.Rows.Add(new object[] { "n58", "Punz. (Kg) Pernos" });
            table.Rows.Add(new object[] { "n59", "Punz. (Kg) Auxiliares" });
            table.Rows.Add(new object[] { "n60", "Punz. (Kg) M.P. + Torn." });*/
            //Se añaden las características de Taladr. (Kg)
            table.Rows.Add(new object[] { "n35", "Taladr. (Kg) M.P." });
            table.Rows.Add(new object[] { "n36", "Taladr. (Kg) Angular" });
            table.Rows.Add(new object[] { "n37", "Taladr. (Kg) Pletina" });
            table.Rows.Add(new object[] { "n38", "Taladr. (Kg) Chapa" });
            table.Rows.Add(new object[] { "n39", "Taladr. (Kg) UPN-IPN-HEB" });
            table.Rows.Add(new object[] { "n40", "Taladr. (Kg) Tub-Red" });
            /*table.Rows.Add(new object[] { "n67", "Taladr. (Kg) Tornillería" });
            table.Rows.Add(new object[] { "n68", "Taladr. (Kg) Estructural" });
            table.Rows.Add(new object[] { "n69", "Taladr. (Kg) Pates" });
            table.Rows.Add(new object[] { "n70", "Taladr. (Kg) Pernos" });
            table.Rows.Add(new object[] { "n71", "Taladr. (Kg) Auxiliares" });
            table.Rows.Add(new object[] { "n72", "Taladr. (Kg) M.P. + Torn." });*/

            // System.Diagnostics.Debug.WriteLine("getMaterialesInforme no es null " + Intranet.getMaterialesInforme[1, 2].Value);

            conexion.Close();


            return table;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //JRM - InformeExcel - Método para seleccionar o deseleccionar todas las características
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                foreach (DataGridViewRow row in colsInforme.Rows)
                {
                    if (row.Cells["CARACT"].Value != null)
                    {
                        row.Cells["SelectCol"].Value = true;
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow row in colsInforme.Rows)
                {
                    if (row.Cells["CARACT"].Value != null)
                    {
                        row.Cells["SelectCol"].Value = false;
                    }
                }
            }
        }

        /*private string codeToDenomination(string x) {
            string strsql;
            SqlConnection conexion = null;
            string connString = @"Data Source=db-imedexsa;Initial Catalog=gg;User ID=gg;Password=ostia"; //ConfigurationManager.ConnectionStrings["Interface_Usuario.Properties.Settings.CS_CPP_0713ConnectionString"].ConnectionString;
            SqlCommand comando = null;
            SqlDataAdapter adapter;
            

            conexion = new SqlConnection(connString);
            conexion.Open();

            strsql = "";
            strsql = strsql + " SELECT  c.DENOMI";
            strsql = strsql + " FROM    T_ARTCAR ac JOIN T_CARACT c ON ac.CARACT = c.CARACT";
            strsql = strsql + " WHERE   ac.CARACT = '" + x + "'";

            comando = new SqlCommand(strsql, conexion);
            adapter = new SqlDataAdapter(comando);
            string n;
            DataTable table = new DataTable();
            
            comando.CommandText = strsql;
            table.Columns.Clear();
            table.Clear();
            adapter.Fill(table);

            n = table.Rows[0].ToString();

            return n;
        }*/

    }
}
