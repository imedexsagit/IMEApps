using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

using System.Transactions;

using System.IO;
using System.Diagnostics;
using System.Collections;
using AdvancedDataGridView;
using Intranet.DAL;
using Utils;
using empresaGlobalProj;


namespace Intranet
{    
    public partial class Intranet : Form
    {
        private String Codemp;
        private String cadenaConexion;
        private String Usuario;
        private String Path;

        //JRM - InformeExcel - ImeApps
        public static string currentDgResultadosRow { get; set; }
        public static DataGridView currentDGVcodesRow { get; set; }
        public static List<Tuple<string, string>> list = new List<Tuple<string, string>>();
        public static DataTable list1 = new DataTable();
        public static DataTable copylist1 = new DataTable();
        public static DataGridView getMaterialesInforme = new DataGridView();

        public static DataTable dT;   //DataTAble que almacena la lista de almacen del codigo seleccionado

        //Diccionario para almacenar el ID de la fila y el código
        private Dictionary<Int32, String> DiccionarioCodigosSeleccionados;                
        private List<String> ListaValoresOriginales;

        private bool ActualizarGridCaracteristicas = false;        
        private string ValorActual;
        private string ValorNuevo;

        // VSS: se utiliza para la exportación a excel del listado de componentes
        private DataTable dtExcel;
        ArrayList rutaRecursivaNodo;        

        //MMM 11_01_18 Guardamos los dataTable en caso de seleccionar varios elementos en la lista.
        public static List<DataTable> listdtAux = new List<DataTable>();

        //JRM - InformeExcel - ImeApps
        /*public DataTable getAllcodes()
        {        
            foreach (DataGridViewRow row in dGViewResultados.Rows)
            {
                DataRow dr = list1.NewRow();
                // los códigos se cogen de treeGridViewListaAlmacen

               // System.Diagnostics.Debug.WriteLine(" =????????????=???????????????=" + dGViewResultados.Columns.Count + " " + dGViewResultados.Columns[dGViewResultados.SelectedCells[1].ColumnIndex].Name);
                list.Add(new Tuple<string, string>(row.Cells["cODIGODataGridViewTextBoxColumn"].Value.ToString(), row.Cells["dENOMINACIONDataGridViewTextBoxColumn"].Value.ToString()));
                dr["Código"] = row.Cells["cODIGODataGridViewTextBoxColumn"].Value.ToString();
                dr["Denominación"] = row.Cells["dENOMINACIONDataGridViewTextBoxColumn"].Value.ToString();
                list1.Rows.Add(dr);
                //dGViewResultados.Columns[dGViewResultados.SelectedCells[0].ColumnIndex].Name == "cODIGODataGridViewTextBoxColumn
            }

            return list1;
        }*/

        public DataTable getAllcodes()
        {
            foreach (DataGridViewRow row in treeGridViewListaAlmacen.Rows)
            {

                DataRow dr = list1.NewRow();
                // los códigos se cogen de treeGridViewListaAlmacen

                //int columnIndex = treeGridViewListaAlmacen.CurrentCell.ColumnIndex;
                /*System.Diagnostics.Debug.WriteLine(" Probando Nombre: " + treeGridViewListaAlmacen.Rows[0].Cells["Nombre"].Value.ToString());
                System.Diagnostics.Debug.WriteLine(" Probando Denominacion: " + treeGridViewListaAlmacen.Rows[0].Cells["Denominacion"].Value.ToString());
                System.Diagnostics.Debug.WriteLine(" Probando Id: " + treeGridViewListaAlmacen.Rows[0].Cells["Id"].Value.ToString());
                System.Diagnostics.Debug.WriteLine(" Probando ID_PADRE: " + treeGridViewListaAlmacen.Rows[0].Cells["ID_PADRE"].Value.ToString());
                string columnNamex;

                for (int i = 0; i < 10; i++)
                {
                    columnNamex = treeGridViewListaAlmacen.Columns[i].Name;
                    System.Diagnostics.Debug.WriteLine(" El nombre de la columna es: " + columnNamex);
                }*/
                
            
                DataColumn[] columns = list1.Columns.Cast<DataColumn>().ToArray();
                bool codeExist = list1.AsEnumerable()
                    .Any(rowx => columns.Any(col => rowx[col].ToString() == row.Cells["Nombre"].Value.ToString()));

                //bool contains = list1.AsEnumerable().Any(roww => list1.Columns["Nombre"].ToString == roww.Field<String>("Nombre"));
                if (row.Cells["Id"].Value.ToString().Equals("0") && !codeExist)//.(row.Cells["Nombre"].Value.ToString(), row.Cells["Denominacion"].Value.ToString()))// == "0")
                {
                    System.Diagnostics.Debug.WriteLine(" Dentro del if id = zero, El Id es : " + row.Cells["Id"].Value.ToString()); //+ treeGridViewListaAlmacen.Rows[0].Cells["Id"].Value.ToString());
                    //System.Diagnostics.Debug.WriteLine(" =????????????=???????????????=" + dGViewResultados.Columns.Count + " " + dGViewResultados.Columns[dGViewResultados.SelectedCells[1].ColumnIndex].Name);
                
                    list.Add(new Tuple<string, string>(row.Cells["Nombre"].Value.ToString(), row.Cells["Denominacion"].Value.ToString()));
                    //list.Add(new Tuple<string, string>(treeGridViewListaAlmacen.Rows[0].Cells["Nombre"].Value.ToString(), treeGridViewListaAlmacen.Rows[0].Cells["Denominacion"].Value.ToString()));
                    dr["Código"] = row.Cells["Nombre"].Value.ToString();
                    dr["Denominación"] = row.Cells["Denominacion"].Value.ToString();

                    //dr["Código"] = treeGridViewListaAlmacen.Rows[0].Cells["Nombre"].Value.ToString();
                    //dr["Denominación"] = treeGridViewListaAlmacen.Rows[0].Cells["Denominacion"].Value.ToString();

                    list1.Rows.Add(dr);
                }
            //

                //dGViewResultados.Columns[dGViewResultados.SelectedCells[0].ColumnIndex].Name == "cODIGODataGridViewTextBoxColumn
            }

            return list1;
        }

        public Intranet()
        {
            InitializeComponent();
            empresaGlobal.showEmp = false;
            // Create new DataColumn ccode, set DataType, ColumnName and add to DataTable.    
            DataColumn ccode = new DataColumn();
            ccode.DataType = System.Type.GetType("System.String");
            ccode.ColumnName = "Código";
            if (!list1.Columns.Contains("Código")) {
                list1.Columns.Add(ccode);
            }
            

            // Create denom column.
            DataColumn cdenom = new DataColumn();
            cdenom.DataType = Type.GetType("System.String");
            cdenom.ColumnName = "Denominación";
            if (!list1.Columns.Contains("Denominación"))
            {
                list1.Columns.Add(cdenom);
            }

            //se añade cols para que no esté vacío nunca
            for (int i = 0; i <= 6; i++)
            {
                //getMaterialesInforme.Columns.Add($"Col{i}", $"Col{i}");
                getMaterialesInforme.Columns.Add(i.ToString(), i.ToString());
            }
            //se añaden filas para que no esté vacío nunca
            for (int i = 0; i <= 12; i++)
            {
                //getMaterialesInforme.Columns.Add($"Col{i}", $"Col{i}");
                getMaterialesInforme.Rows.Add();
            }

            //Create Caract column
            /*DataColumn c1 = new DataColumn();
            c1.DataType = Type.GetType("System.String");
            c1.ColumnName = "CARACT";
            list1.Columns.Add(c1);

            //Create Valor column
            DataColumn c2 = new DataColumn();
            c2.DataType = Type.GetType("System.String");
            c2.ColumnName = "VALOR";
            list1.Columns.Add(c2);*/
            
            this.Codemp = empresaGlobal.empresaID;//ConfigurationManager.AppSettings["EMPRESA"].ToString().ToUpper();
            this.cadenaConexion = ConfigurationManager.AppSettings["ggConnectionString"].ToString();            
            this.Path = ConfigurationManager.AppSettings["url_DestinoPlanos"].ToString().ToUpper();
            this.Usuario = Environment.UserName.ToUpper();
            


            this.DiccionarioCodigosSeleccionados = new Dictionary<Int32, String>();
            this.ListaValoresOriginales = new List<String>();

            Ver_Caracteristicas(false);

            //JMRM 25/06/2020 se añade esta condición para que solo se puede copiar códigos desde la empresa 3
            if (empresaGlobal.empresaID != "3")
            {
                copiarAMade.Visible = false;
            }
            else
            {
                copiarAMade.Visible = true;
            }

            //CSanchez 10/03/2021 se añade esta condición para que solo se puede eliminar códigos desde la empresa 60
            if (empresaGlobal.empresaID != "60")
            {
                eliminarDeMade.Visible = false;
            }
            else
            {
                eliminarDeMade.Visible = true;
            }



        }

        private void Intranet_Load(object sender, EventArgs e)
        {
            UseWaitCursor = true;

            // TODO: esta línea de código carga datos en la tabla 'dsEstructuras.T_ESTRUC' Puede moverla o quitarla según sea necesario.
            this.t_CATEGORIASTableAdapter.Fill(this.dsCategorias.T_CATEGORIAS);
            this.t_FAMILIASTableAdapter.Fill(this.dsFamilias.T_FAMILIAS, empresaGlobal.empresaID);
            this.t_ARTICULOSTableAdapter1.Fill(this.dsMP.T_ARTICULOS, empresaGlobal.empresaID);
            //this.t_ARTICULOSTableAdapter2.Fill(this.dsCodigos.T_ARTICULOS);
            //this.t_ESTRUCTableAdapter.Fill(this.dsEstructuras.T_ESTRUC);           

            // VSS: inicializamos los tamaños de los espacios a la nueva dimensión
            Mantener_Distancias_Split();

            UseWaitCursor = false;

            //MMM
            splitContainer2.SplitterDistance = 40;
        }


        //Busqueda de códigos según criterio
        private void btnBuscarCodigo_Click(object sender, EventArgs e)
        {
            // Ahora solo limpiamos el grid de componentes si se tiene marcada la opción de limpiar listado de componentes
            if (checkBox_LimpiarComponentes.Checked)
            {
                limpiarArbol();
            }

            // limpiarArbol();
            //limpiarNavegador();

            String codigo = this.txtBuscarCodigo.Text.Replace("*", "%");
            if (codigo == "")
            {
                MessageBox.Show("Error!!! debe escribir un código");
                return;
            }

            if (this.t_ARTICULOSTableAdapter.Fill(this.dsBusquedaArticulos.T_ARTICULOS, empresaGlobal.empresaID, codigo) == 0)
            {
                if (MessageBox.Show("No se encontraron códigos en la búsqueda. ¿Desea crear el código buscado?", "Resultado búsqueda", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    CrearCodigo creacion = new CrearCodigo(0, this.Codemp, "", this.Usuario);
                    creacion.ShowDialog();
                }
            }
   
        }


        private void btnBuscarDenominacion_Click(object sender, EventArgs e)
        {
            String denominacion = this.txtBuscarDenominacion.Text.Replace("*", "%");
            if (denominacion == "")
            {
                MessageBox.Show("Error: debe escribir una denominación");
                return;
            }

            treeGridViewListaAlmacen.Nodes.Clear();

            this.t_ARTICULOSTableAdapter.Fill(this.dsBusquedaArticulos.T_ARTICULOS, empresaGlobal.empresaID, denominacion);
        }


        private void dGViewResultados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ahora solo limpiamos el grid de componentes si se tiene marcada la opción de limpiar listado de componentes
            if (checkBox_LimpiarComponentes.Checked)
            {
                limpiarArbol();
            }
            // limpiarArbol();
            // limpiarNavegador();

            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                String codigo = dGViewResultados[e.ColumnIndex, e.RowIndex].Value.ToString().Trim();
                
                //JRM
                //getMaterialesInforme = obtenerMixMateriales(codigo);

                dGViewResultados.Enabled = false;
                //UseWaitCursor = true;
                Cursor.Current = Cursors.WaitCursor;                

                if (codigo.Length > 0)
                {
                    
                    //TOMÁS 30/11/2017. Comprobar que pesos sacar
                    int peso_tornillos = 0;
                    if (radioButton_PesoConTornillos.Checked)
                        peso_tornillos = 1;
                    if (radioButton_PesoSoloTornillos.Checked)
                        peso_tornillos = 2;
                    
                    //MMM 11/01/18 Guardamos los registros del dT en caso de que no limpiemos el listado.                    
                    if ((!checkBox_LimpiarComponentes.Checked) && (dT != null))
                    {
                        DataTable dtAux = new DataTable();
                        dtAux = dT.Copy();
                        listdtAux.Add(dtAux);
                    }
                    else
                    {//Si limpiamos los componenetes, limpiamos la lista de dt.
                        if (listdtAux.Count() > 0)
                            listdtAux.Clear();
                    }
                    

                    //JRegino 05/07/2016. Añadido parámetro al proc para que devuelva la denominacion en el idioma deseado                    
                    dT = CD.GetListaAlmacen(this.Codemp, codigo, cmbIdioma.Text,peso_tornillos);


                    if (dT.Rows.Count > 0)
                    {
                       
                            crearArbol();
                        
                    }
                    else
                        MessageBox.Show("El código seleccionado no tiene lista de almacén");
                }
                else
                    MessageBox.Show("El código elegido no tiene caracteres");

                //UseWaitCursor = false;
                Cursor.Current = Cursors.Arrow;
                dGViewResultados.Enabled = true;
            }
            //JRM - InformeExcel - ImeApps

            //Al hacer doble click cargar las características de ese código llamando a lo siguiente (buscarlo y ver los otros códigos)
            //MostrarMixMateriales(dgvc_List, tgn_List);

            

            this.getAllcodes();
        }



        #region Métodos para el arbol
        private void crearArbol()
        {
            String ID = dT.Rows[0][0].ToString();
            String Cantidad_Padre = dT.Rows[0][1].ToString();
            String Codigo_Padre = dT.Rows[0][2].ToString();
            String Correlacion = dT.Rows[0][3].ToString();
            String Cantidad_Hijo = dT.Rows[0][5].ToString();
            String ID_PADRE = dT.Rows[0][4].ToString();
            String Codigo_Hijo = dT.Rows[0][6].ToString();
            String Codigo_Hijo_Denominacion = dT.Rows[0][7].ToString();
            String Categoria_Hijo = dT.Rows[0][8].ToString();            
            String Familia_Hijo = dT.Rows[0][9].ToString();            
            String PesoUnitario = dT.Rows[0][10].ToString();
            String MP = dT.Rows[0][11].ToString();

            //Añadido por José Manuel, Se calcula la cantidad_total a través del padre y el hijo
            String Cantidad_Total = "";
            if (Cantidad_Padre != "")
            {
                    Cantidad_Total = Convert.ToString(Convert.ToDouble(Cantidad_Padre) * Convert.ToDouble(Cantidad_Hijo));
            }//Fin añadido por Jose

            String PesoTotal = "";
            if (PesoUnitario != "")
                PesoTotal = Convert.ToString(Convert.ToDouble(PesoUnitario) * Convert.ToDouble(Cantidad_Hijo));
            String Long = dT.Rows[0][12].ToString();
            String Area = dT.Rows[0][13].ToString();
            bool Fabricable = false;
            if (dT.Rows[0][14].ToString() == "1")
                Fabricable = true;
            else
                Fabricable = false;
            String Nivel = dT.Rows[0][15].ToString();
            //MMM 09_01_18
            String Categoria_MP = dT.Rows[0][16].ToString();

            //MMM 22_05_19
            String taladrado = dT.Rows[0][17].ToString();

            TreeGridNode node = treeGridViewListaAlmacen.Nodes.Add(Codigo_Hijo, ID, ID_PADRE, Cantidad_Hijo, Codigo_Hijo_Denominacion, Categoria_Hijo, Familia_Hijo, PesoUnitario, MP, Long, Area, Fabricable, Correlacion, Cantidad_Total, PesoTotal, Nivel, Categoria_MP, taladrado);
            node.Expand();

            añadirHoja(node, ID);
            if (radioButton_PesoConTornillos.Checked || radioButton_PesoSoloTornillos.Checked)
                buscarPesosCero(node, node.Cells[0].Value.ToString());
        }

        private bool buscarPesosCero(TreeGridNode node, string codigo)
        {
            decimal kg = 0;
            bool pesoCero = false;
            
            string codigo_Padre = "";
            if (node.Parent.Cells[0].Value != null)
                codigo_Padre = node.Parent.Cells[0].Value.ToString();
            else
                codigo_Padre = codigo;

            string s = "Codigo_Hijo = '" + codigo + "' and Codigo_Padre = '" + codigo_Padre + "'";

            DataRow dr = GetDataRow(s);
            if (dr == null)
            {
                pesoCero = true;
                return false;
            }
                        
            //Vemos si tiene peso
            if (!String.IsNullOrEmpty(dr["PesoUnitario"].ToString()))
                kg = Convert.ToDecimal(dr["PesoUnitario"]);
            else
                kg = 0;

            //if (codigo == "M16x40(DIN934)") Para simular peso 0 en un tornillo
            //    kg = 0;

            if (kg == 0)
            {
                pesoCero = true;
                //Si tiene peso cero, pintamos el nodo de amarillo.
                node.Cells[0].Style.BackColor = Color.Yellow;
            }
            else
                pesoCero = false;



            if (node.HasChildren)
            {
                foreach (TreeGridNode nodeAux in node.Nodes)
                {

                    //kg += GetKgMaterial(nodeAux.Cells[0].Value.ToString(), nodeAux, Categoria_MP, out cantidadAux, out pesoCeroAux) * cantidad;
                    bool pesoCeroAux = buscarPesosCero(nodeAux, nodeAux.Cells[0].Value.ToString());
                    pesoCero = pesoCero || pesoCeroAux;
                }
                if (pesoCero)//Si alguno de sus hijos tiene peso cero, pintamos el nodo padre
                    node.Cells[0].Style.BackColor = Color.Yellow;
            }

            return pesoCero;

        }

        private void añadirHoja(TreeGridNode nodo, String idPadre)
        {
            //José Manuel 26/11/2019 - Variable que va a almacenar la cantidad total y se actualiza con cada padre
            String valorCantidad_Total = dT.Rows[0][5].ToString();
            //String para comprobar el nombre del padre y actualizar la variable ValorCantidad_Total
            String CodPadreAnterior = dT.Rows[0][2].ToString();
            String Cantidad_Total = "";

            for (int x = 1; x < dT.Rows.Count; x++)
            {
                String ID_PADRE = dT.Rows[x][4].ToString();
                if (ID_PADRE == idPadre)
                {
                    String ID = dT.Rows[x][0].ToString();
                    String Cantidad_Padre = dT.Rows[x][1].ToString();
                    String Codigo_Padre = dT.Rows[x][2].ToString();
                    String Correlacion = dT.Rows[x][3].ToString();
                    String Cantidad_Hijo = dT.Rows[x][5].ToString();
                    String Codigo_Hijo = dT.Rows[x][6].ToString();
                    String Codigo_Hijo_Denominacion = dT.Rows[x][7].ToString();
                    String Categoria_Hijo = dT.Rows[x][8].ToString();
                    String Familia_Hijo = dT.Rows[x][9].ToString();
                    String PesoUnitario = dT.Rows[x][10].ToString();
                    String MP = dT.Rows[x][11].ToString();
                    System.Diagnostics.Debug.WriteLine("----------------------------------------");
                    //Añadido por José Manuel, Se calcula la cantidad_total a través del padre y el hijo
                    // String Cantidad_Total = ""; // LO he puesto fuera del bucle
                    if (Cantidad_Padre != "")
                    {


                        //Se comprueba el nivel, si es uno de los siguientes la cantidad no se multiplica, de lo contrario se multiplica el valor unitario por la cantidad total del padre
                        if (ID == "0")// || ID == "1" || ID == "2")
                        {
                            System.Diagnostics.Debug.WriteLine("___________ El id: " + ID + " tiene nivel igual a 0, 1 o 2: " + ID);
                            Cantidad_Total = Cantidad_Hijo;
                        }
                        else
                        {

                            System.Diagnostics.Debug.WriteLine("___________ El nivel es DIFERente a 0 o 1: " + ID + " y se multiplica Cantidad_Padre " + Cantidad_Padre + " * Cantidad_Hijo " + Cantidad_Hijo);
                            Cantidad_Total = Convert.ToString(Convert.ToDouble(Cantidad_Hijo) * Convert.ToDouble(Cantidad_Padre));
                            //Double un = Convert.ToDouble(valorCantidad_Total);

                        }
                          
                    }
                    
                    String PesoTotal = "";
                    String Long = dT.Rows[x][12].ToString();
                    String Area = dT.Rows[x][13].ToString();
                    bool Fabricable = false;
                    if (dT.Rows[x][14].ToString() == "1")
                        Fabricable = true;
                    else
                        Fabricable = false;
                    String Nivel = dT.Rows[x][15].ToString();                           
                     
                    string valueChijo = "1";
                    //variable que va a almacenar en double el valor del hijo y se va a ir actualizando multiplicándolo por cada valor hijo de su padre hasta que el padre sea ID = 1
                    double valuechVal = Convert.ToDouble(Cantidad_Hijo) * Convert.ToDouble(Cantidad_Padre);
                    string valuexs = ID_PADRE;
                    //Si el nivel es mayor a 2
                    int z = 0;
                    //bandera fin para cuando se encuentra el valor ID 1
                    bool fin = false;

                    //System.Diagnostics.Debug.WriteLine(" El ID actual es: " + Codigo_Hijo + " y su nombre es: " + Codigo_Hijo_Denominacion);
                   /* EnumerableRowCollection<DataRow> results = from myRow in dT.AsEnumerable()
                                                               where Convert.ToInt32(myRow.Field<string>("ID")) == 1
                                                               select myRow;*/


                    //System.Diagnostics.Debug.WriteLine(results.ToString());

                    //Con esta opción obtengo lo que sea igual al padre y su cantidad, así que puedo ir escalando así
                    int varID = Convert.ToInt32(ID_PADRE);
                    DataRow[] drowd;
                    //string idejemplo = "443MC.";
                   // int ittttt = 0;

                    drowd = dT.Select("[ID] = '" + ID_PADRE + "'");
                    if (Convert.ToString(drowd[0]["ID"]) != "0" && Convert.ToInt32(Nivel) > 1)
                    {

                        while (varID != 0)
                        { //&& idejemplo == Codigo_Hijo && ittttt < 1) {
                            drowd = dT.Select("[ID] = '" + varID + "'");
                            System.Diagnostics.Debug.WriteLine("La longitud de drowd es: " + drowd.Length);
                            for (int i = 0; i < drowd.Length; i++)
                            {
                                
                                System.Diagnostics.Debug.WriteLine("El ID_Padre es diferente a 0: " + ID_PADRE);
                                varID = Convert.ToInt32(drowd[i]["ID_PADRE"]);
                                valuechVal = valuechVal * Convert.ToDouble(drowd[i]["Cantidad_Padre"]);
                                System.Diagnostics.Debug.WriteLine("Muestro el ID_PADRE: " + drowd[i]["ID_PADRE"]);
                               /* if (Convert.ToString(drowd[i]["ID_PADRE"]) != "" && Convert.ToString(drowd[i]["ID_PADRE"]) != "0")
                                {
                                    System.Diagnostics.Debug.WriteLine("Muestro con System.Diagnostics.Debug.WriteLine: " + drowd[i]["Cantidad_Hijo"] + " y ahora el valor VarID para comparar en el While es: " + varID);
                                    
                                    
                                }*/
                               // varID = Convert.ToInt32(drowd[i]["ID"]);
                                Cantidad_Total = Convert.ToString(valuechVal);
                                System.Diagnostics.Debug.WriteLine("PRUEBAS SELEECT El valor ahora es: " + valuechVal);

                            }
                            // ittttt++;
                            // varID = drowd;
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("NO ENTRO. -> El ID actual es 0: " + drowd[0]["ID"] + " ");
                    }
                    

                    //List<DataRow> resultfs = dT.AsEnumerable().Where(xl => xl.Field<string>("Cantidad").ToList());

                   
                    System.Diagnostics.Debug.WriteLine("555555555555555555555555555555555555555555555555555555555555");


                    //VERSIÓN HACIA DELANTE sin consultar database
                  /*  valuechVal = Convert.ToDouble(Cantidad_Hijo) * Convert.ToDouble(Cantidad_Padre);
                    if (Convert.ToInt32(Nivel) > 2)
                    {
                        System.Diagnostics.Debug.WriteLine("ççççççççççççççççççççççççççç > NiVEL superior a 2 ENCONTrado, el ID_Padre es: " + ID_PADRE + " y la variable valuexs contiene lo mismo que el padre: " + valuexs + " | más info: -ID actual " + ID + " -nombreactual: " + Codigo_Hijo_Denominacion + " con códigohijo: " + Codigo_Hijo);
                        while (z < dT.Rows.Count && !fin)
                        {
                            System.Diagnostics.Debug.WriteLine("Bucle While iteracción: " + z);

                            if (valuexs == dT.Rows[z].Field<string>("ID"))
                            {
                                valuexs = dT.Rows[z].Field<string>("ID_Padre");//almaceno el ID_padre del level 2
                                System.Diagnostics.Debug.WriteLine("_______valuexs tiene el valor del ID Padre del level 2________: " + valuexs + " y se multiplica aquí con el valor cantidad padre " + dT.Rows[z].Field<string>("Cantidad_Padre"));
                                //actualizo el valor del hijo total con el valordelhijototal * valorhijocantidadpadre
                                //primero almaceno en double y luego convierto a string
                                valuechVal = valuechVal * Convert.ToDouble(dT.Rows[z].Field<string>("Cantidad_Padre"));
                                System.Diagnostics.Debug.WriteLine("El valor ahora es: " + valuechVal);
                                valueChijo = Convert.ToString(valuechVal);
                                //Convert.ToString(Convert.ToDouble(PesoUnitario) * Convert.ToDouble(Cantidad_Total));

                                //Cuando el valor del padre sea 1 se para de buscar ya que es el valor más alto del árbol y siempre va a tener cantidad 1
                                if (valuexs == "0")
                                {
                                    fin = true;
                                    Cantidad_Total = Convert.ToString(Convert.ToDouble(valueChijo)); //Convert.ToString(Convert.ToDouble(Cantidad_Hijo) * Convert.ToDouble(valueChijo));
                                }
                                else
                                {
                                    System.Diagnostics.Debug.WriteLine("El valor ID_PADRE es diferente a 1: " + valuexs);
                                }
                                z = 0;
                            }
                            else
                            {
                                //z = 0;
                                z++;
                            }
                       }
                   }*/


                    //VERSIÓN HACIA ATRÁS sin consultar database


                    /*int nivelActualCT = Convert.ToInt32(Nivel); //almaceno el nivel actual
                    int nivelPadre = nivelActualCT -1 ;
                    System.Diagnostics.Debug.WriteLine("^^^^^^^^^^^^^ El nivel actual es: " + nivelActualCT + " y el nivel Padre es: " + nivelPadre + " comienza Bucle While.");
                    z = Convert.ToInt32(ID);
                    if (Convert.ToInt32(Nivel) > 2)
                    {
                        System.Diagnostics.Debug.WriteLine("ççççççççççççççççççççççççççç > NiVEL superior a 2 ENCONTrado, el ID_Padre es: " + ID_PADRE + " y la variable valuexs contiene lo mismo que el padre: " + valuexs + " | más info: -ID actual " + ID + " -nombreactual: " + Codigo_Hijo_Denominacion + " con códigohijo: " + Codigo_Hijo);
                        while (z < dT.Rows.Count && !fin) //añadir que el nivel sea uno menos que el actual
                        {
                            System.Diagnostics.Debug.WriteLine("Bucle While iteracción: " + z + " y estamos buscando en el nivel padre: " + nivelPadre + " y nivel actual es: " + nivelActualCT);

                            if (valuexs == dT.Rows[z].Field<string>("ID"))
                            {

                                string nose = dT.Rows[z].Field<string>("ID_Padre");
                                dT.Rows[1].Field<string>("Cantidad_Padre");

                                valuexs = dT.Rows[z].Field<string>("ID_Padre");//almaceno el ID_padre del level 2
                                System.Diagnostics.Debug.WriteLine("_______valuexs tiene el valor del ID Padre del level 2________: " + valuexs + " y se multiplica aquí con el valor cantidad padre " + dT.Rows[z].Field<string>("Cantidad_Padre"));
                                //actualizo el valor del hijo total con el valordelhijototal * valorhijocantidadpadre
                                //primero almaceno en double y luego convierto a string
                                valuechVal = valuechVal * Convert.ToDouble(dT.Rows[z].Field<string>("Cantidad_Padre"));
                                System.Diagnostics.Debug.WriteLine("El valor ahora es: " + valuechVal);
                                valueChijo = Convert.ToString(valuechVal);
                                nivelActualCT = nivelActualCT - 1; //disminuyo en 1 el nivel
                                nivelPadre = nivelPadre - 1;
                                //Convert.ToString(Convert.ToDouble(PesoUnitario) * Convert.ToDouble(Cantidad_Total));




                                //Cuando el valor del padre sea 1 se para de buscar ya que es el valor más alto del árbol y siempre va a tener cantidad 1
                                if (valuexs == "0")
                                {
                                    fin = true;
                                    Cantidad_Total = Convert.ToString(Convert.ToDouble(valueChijo)); //Convert.ToString(Convert.ToDouble(Cantidad_Hijo) * Convert.ToDouble(valueChijo));
                                }
                                else
                                {
                                    System.Diagnostics.Debug.WriteLine("El valor ID_PADRE es diferente a 1: " + valuexs);
                                }
                               // z = z;
                            }
                            else
                            {
                                //z = 0;
                                z--;
                            }
                        }
                    }*/




                    //finjose

                    if (PesoUnitario != "")
                    {
                        if (ID == "0")
                        {
                            PesoTotal = PesoUnitario;
                        }
                        else
                        {
                            PesoTotal = Convert.ToString(Convert.ToDouble(PesoUnitario) * Convert.ToDouble(Cantidad_Total));
                        }
                    }


                    //MMM 09_01_18
                    String Categoria_MP = dT.Rows[x][16].ToString();

                    //MMM 22_05_19
                    String taladrado = dT.Rows[x][17].ToString();

                    TreeGridNode nodoHijo = nodo.Nodes.Add(Codigo_Hijo, ID, ID_PADRE, Cantidad_Hijo, Codigo_Hijo_Denominacion, Categoria_Hijo, Familia_Hijo, PesoUnitario, MP, Long, Area, Fabricable, Correlacion, Cantidad_Total, PesoTotal, Nivel, Categoria_MP, taladrado);

                    //System.Diagnostics.Debug.WriteLine(" Muestro la hoja que añado: - CodigoHijo: " + Codigo_Hijo + " -CódigoPadre: " + Codigo_Padre + " -ID: " + ID + " -ID_Padre: " + ID_PADRE + " -Cantidad_Hijo: " + Cantidad_Hijo + " -CantidadPadre: " + Cantidad_Padre + " -Codigo_Hijo_Denominacion: " + Codigo_Hijo_Denominacion + " -Categoria_Hijo: " + Categoria_Hijo + " -Familia_Hijo: " + Familia_Hijo + " -PesoUnitario: " + PesoUnitario + " -MP: " + MP + " -Long: " + Long + " -Area: " + Area + " -Fabricable: " + Fabricable + " -Correlacion: " + Correlacion + " -Cantidad_Total: " + Cantidad_Total + " -PesoTotal: " + PesoTotal + " -Nivel: " + Nivel + " -Categoria_MP: " + Categoria_MP + " -taladrado: " + taladrado);
                    //System.Diagnostics.Debug.WriteLine("------La múltplicación hecha es: ------- CantHijo " + Cantidad_Hijo + " * valorCantidad_Total " + valorCantidad_Total);
                    //System.Diagnostics.Debug.WriteLine("----------------------------------------");

                    añadirHoja(nodoHijo, ID);
                }
            }
        }

        private void limpiarArbol()
        {
            treeGridViewListaAlmacen.Nodes.Clear();
        }
        #endregion


        //Al pulsar con el boton dcho sobre el nombre de un código mostrar el menu
        private void treeGridViewListaAlmacen_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Limpiar la lista            
            this.DiccionarioCodigosSeleccionados.Clear();
            
            if (e.Button == MouseButtons.Right && e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                // VSS: para evitar el error de varias celdas seleccionadas de la misma fila
                treeGridViewListaAlmacen.CurrentCell = treeGridViewListaAlmacen.Rows[e.RowIndex].Cells[e.ColumnIndex];                                

                DataGridViewSelectedCellCollection celdasSeleccionadas = treeGridViewListaAlmacen.SelectedCells;
                if (celdasSeleccionadas.Count > 0)
                {
                    if (celdasSeleccionadas.Count > 1)
                    {
                        montajePToolStripMenuItem.Enabled = false;
                        piezaPToolStripMenuItem.Enabled = false;
                        editarCToolStripMenuItem.Enabled = false;
                        editarEToolStripMenuItem.Enabled = false;
                        añadirEToolStripMenuItem.Enabled = false;
                        copiarEToolStripMenuItem.Enabled = false;
                        HeredarCaracterísticasCompartidasToolStripMenuItem.Enabled = false;
                        copiarSuperFamiliaALosHijosToolStripMenuItem.Enabled = false;
                        copiarDiseñadoPorALosHijosToolStripMenuItem.Enabled = false;
                    }
                    else
                    {
                        montajePToolStripMenuItem.Enabled = true;
                        piezaPToolStripMenuItem.Enabled = true;
                        editarCToolStripMenuItem.Enabled = true;
                        editarEToolStripMenuItem.Enabled = true;
                        añadirEToolStripMenuItem.Enabled = true;
                        copiarEToolStripMenuItem.Enabled = true;
                        TreeGridNode articuloOrigen = treeGridViewListaAlmacen.CurrentNode;
                        //GMM: Se comprueba si el nodo seleccionado tiene hijos.
                        int nivelNodoActual = Convert.ToInt32(articuloOrigen.Cells["NIVEL"].Value);
                        if (nivelNodoActual >= 0 && articuloOrigen.Nodes.Count > 0)
                        {
                            //GMM:Uso el indice (15) en lugar del nombre (NIVEL) ya que si esta contraido el nodo me da una excepción.
                            var nivelNodoHijo = Convert.ToInt32(articuloOrigen.Nodes[0].Cells[15].Value);
                            //GMM: Si el hijo tiene un nivel >0 y mayor que el padre se muestra. Si es menor seria una implosión y no debemos mostrarlo
                            HeredarCaracterísticasCompartidasToolStripMenuItem.Enabled = nivelNodoHijo > nivelNodoActual;
                            copiarSuperFamiliaALosHijosToolStripMenuItem.Enabled = nivelNodoHijo > nivelNodoActual;
                            copiarDiseñadoPorALosHijosToolStripMenuItem.Enabled = nivelNodoHijo > nivelNodoActual;                                
                        }
                        else
                        {
                            HeredarCaracterísticasCompartidasToolStripMenuItem.Enabled = false;
                            copiarSuperFamiliaALosHijosToolStripMenuItem.Enabled = false;
                            copiarDiseñadoPorALosHijosToolStripMenuItem.Enabled = false;
                        }
                    }

                    foreach (DataGridViewCell dataGridViewCell in celdasSeleccionadas)
                    {
                        DataGridViewRow dr = dataGridViewCell.OwningRow;
                        Int32 id = Convert.ToInt32(dr.Cells[1].Value);
                        String codigo = dr.Cells[0].Value.ToString();

                        this.DiccionarioCodigosSeleccionados.Add(id, codigo);
                    }
                    
                    contextMenuStripAcciones.Show(MousePosition);
                }                
            }

            //MMM 09_01_18
            //if (e.ColumnIndex == 0)
            //{                
            //    MostrarMixMateriales(treeGridViewListaAlmacen.CurrentCell, treeGridViewListaAlmacen.CurrentNode);
            //}

            if ((e.RowIndex > -1) && (treeGridViewListaAlmacen.CurrentNode != null))
            {                
                //e.RowIndex
                //DataGridViewCell dgvc = treeGridViewListaAlmacen.Rows[e.RowIndex].Cells[0];

                List<DataGridViewCell> dgvc_List = new List<DataGridViewCell>();
                List<TreeGridNode> tgn_List = new List<TreeGridNode>();
              
                for (int i = 0; i < treeGridViewListaAlmacen.Rows.Count; i++)
                {                    
                    //Para esta fila, vemos si existe alguna celda seleccionada.
                    for (int j = 0; j < treeGridViewListaAlmacen.Rows[i].Cells.Count; j++)
                    {
                        if (treeGridViewListaAlmacen.Rows[i].Cells[j].Selected)
                        {
                            DataGridViewCell dgvcAux = treeGridViewListaAlmacen.Rows[i].Cells[0];
                            dgvc_List.Add(dgvcAux);
                            TreeGridNode tgn = GetNodeByText(treeGridViewListaAlmacen.Nodes, dgvcAux.Value.ToString());
                            if (tgn != null)
                                tgn_List.Add(tgn);
                            break;
                        }
                    }
                }



                MostrarMixMateriales(dgvc_List, tgn_List);
            }
            
        }


        public static TreeGridNode GetNodeByText(TreeGridNodeCollection nodes, string searchtext)
        {
            TreeGridNode n_found_node = null;
            bool b_node_found = false;

            foreach (TreeGridNode node in nodes)
            {

                if (node.Cells[0].Value.ToString() == searchtext)
                {
                    b_node_found = true;
                    n_found_node = node;

                    return n_found_node;
                }

                if (!b_node_found)
                {
                    n_found_node = GetNodeByText(node.Nodes, searchtext);

                    if (n_found_node != null)
                    {
                        return n_found_node;
                    }
                }
            }
            return null;

        }

        //MostrarMixMateriales(dgvc_List, tgn_List);
       /* public static void updateMixMat(string code, List<TreeGridNode> node_List)
        {
            //getMaterialesInforme = obtenerMixMateriales("", node_List);
            //List<DataGridViewCell> dgVC_List = null;
           // MostrarMixMateriales(dgVC_List,node_List);
        }*/

        //Método static que permite obtener los materiales de un código desde otros proyectos
        public static DataGridView obtenerMixMateriales(string code)
        {

            //string pr = listdtAux[0].Rows[0][1].ToString();
            //System.Diagnostics.Debug.WriteLine(listdtAux.Count + " - " + pr + " Código introducido a procesar los materiales: " + code);
            //node = 0;

            List<DataGridViewCell> dgvc_List = new List<DataGridViewCell>();
            List<TreeGridNode> tgn_List = new List<TreeGridNode>();

            System.Diagnostics.Debug.WriteLine(" Probañdo. El nº de filas es: " + treeGridViewListaAlmacen.Rows.Count);
            for (int i = 0; i < treeGridViewListaAlmacen.Rows.Count; i++)
            {
                
                //Para esta fila, vemos si existe alguna celda seleccionada.
                for (int j = 0; j < treeGridViewListaAlmacen.Rows[i].Cells.Count; j++)
                {
                    //System.Diagnostics.Debug.WriteLine(" Probañdo. treeGridViewListaAlmacen.Rows[i].Cells.Count es: " + treeGridViewListaAlmacen.Rows[i].Cells.Count);
                    //if (treeGridViewListaAlmacen.Rows[i].Cells[j].Selected)
                    //{
                        //System.Diagnostics.Debug.WriteLine(" Probañdo. treeGridViewListaAlmacen.Rows[i].Cells[j].Selected es: " + treeGridViewListaAlmacen.Rows[i].Cells[j].Selected);
                        DataGridViewCell dgvcAux = treeGridViewListaAlmacen.Rows[i].Cells[0];
                        dgvc_List.Add(dgvcAux);
                        TreeGridNode tgn = GetNodeByText(treeGridViewListaAlmacen.Nodes, dgvcAux.Value.ToString());
                        if (tgn != null)
                            System.Diagnostics.Debug.WriteLine(" Probañdo. tgn: " + tgn);
                            tgn_List.Add(tgn);
                        break;
                   // }
                }
            }
            //System.Diagnostics.Debug.WriteLine(" tgn_List contiene este nº de elementos : " + tgn_List.Count + " el nodo contiene " + tgn_List[0].Cells[0].ToString());

            //TreeGridNode node = node_List[i];
            //node_List = tgn_List;

            //dataGridViewMixMateriales_CellValueChangedInforme();

            /*getMaterialesInforme.Columns[0].HeaderCell.Style.BackColor = Color.Gray;
            getMaterialesInforme.Columns[1].HeaderCell.Style.BackColor = Color.Gray;
            getMaterialesInforme.Columns[2].HeaderCell.Style.BackColor = Color.Gray;
            getMaterialesInforme.Columns[3].HeaderCell.Style.BackColor = Color.Gray;
            getMaterialesInforme.Columns[4].HeaderCell.Style.BackColor = Color.Gray;
            getMaterialesInforme.Columns[5].HeaderCell.Style.BackColor = Color.Gray;
            getMaterialesInforme.Columns[6].HeaderCell.Style.BackColor = Color.Gray;*/


            //1. Vamos a recorrer el arbol, para ver todos los elementos del nodo, y poder mostrar el MIX de los materiales.
            decimal kg_M1_Angular = 0;
            decimal kg_M3_Pletina = 0;
            decimal kg_M2_Chapa = 0;
            decimal kg_M5_UPN = 0;
            decimal kg_M4_Tub = 0;

            decimal kg_TO_ES = 0;
            decimal kg_TO_PA = 0;
            decimal kg_TO_PE = 0;
            decimal kg_TO_Aux = 0;

            decimal cantidadTO_ES = 0;
            decimal cantidadTO_PA = 0;
            decimal cantidadTO_PE = 0;
            decimal cantidadTO_Aux = 0;
            bool pesoCeroES = false;
            bool pesoCeroPA = false;
            bool pesoCeroPE = false;
            bool pesoCeroAux = false;

            decimal cantidadTotal = 0;//Para la MP, esta variable no la usamos
            bool pesoCero = false;//Para la MP, esta variable no la usamos

            decimal kg_Punzonado_Angular = 0;
            decimal kg_Punzonado_Pletina = 0;
            decimal kg_Punzonado_Chapa = 0;
            decimal kg_Punzonado_UPN = 0;
            decimal kg_Punzonado_Tub = 0;
            decimal kg_Taladrado_Angular = 0;
            decimal kg_Taladrado_Pletina = 0;
            decimal kg_Taladrado_Chapa = 0;
            decimal kg_Taladrado_UPN = 0;
            decimal kg_Taladrado_Tub = 0;

            int js = 0;
            bool enc = false;
            //for (int i = 0; i < dgvc_List.Count; i++)

            for (int i = 0; i < dgvc_List.Count; i++) {
                System.Diagnostics.Debug.WriteLine(" ________-> Los códigos de dgvc_List[i] son " + dgvc_List[js].Value.ToString());
            }

                while (js < dgvc_List.Count && !enc)
                {
                    //if (dgvc_List[i].SelectedCells[0].Value.ToString()) ; //dataGridView1.SelectedCells[0].Value.ToString()
                    DataGridViewCell dgVC = dgvc_List[js];
                    System.Diagnostics.Debug.WriteLine(" Bucle For dgvc_List[i] contiene " + dgvc_List[js].Value.ToString());
                    TreeGridNode node = tgn_List[js];
                    if (code == dgvc_List[js].Value.ToString())
                    {
                        System.Diagnostics.Debug.WriteLine(" EncontradO " + code + " = " + dgvc_List[js].Value.ToString());
                        enc = true;
                        //TreeGridNode node = tgn_List[0];

                        kg_M1_Angular += GetKgMaterial(code, node, "M1", out cantidadTotal, out pesoCero);
                        kg_M3_Pletina += GetKgMaterial(code, node, "M3", out cantidadTotal, out pesoCero);
                        kg_M2_Chapa += GetKgMaterial(code, node, "M2", out cantidadTotal, out pesoCero);
                        kg_M5_UPN += GetKgMaterial(code, node, "M5", out cantidadTotal, out pesoCero);
                        kg_M4_Tub += GetKgMaterial(code, node, "M4", out cantidadTotal, out pesoCero);

                        //Guardamos los kg para los punzonados/taladrado
                        decimal kg_Punzonado_Angular_Aux = 0;
                        decimal kg_Punzonado_Pletina_Aux = 0;
                        decimal kg_Punzonado_Chapa_Aux = 0;
                        decimal kg_Punzonado_UPN_Aux = 0;
                        decimal kg_Punzonado_Tub_Aux = 0;
                        decimal kg_Taladrado_Angular_Aux = 0;
                        decimal kg_Taladrado_Pletina_Aux = 0;
                        decimal kg_Taladrado_Chapa_Aux = 0;
                        decimal kg_Taladrado_UPN_Aux = 0;
                        decimal kg_Taladrado_Tub_Aux = 0;
                        GetKgTaladrado(code, node, "M1", out kg_Punzonado_Angular_Aux, out kg_Taladrado_Angular_Aux);
                        GetKgTaladrado(code, node, "M3", out kg_Punzonado_Pletina_Aux, out kg_Taladrado_Pletina_Aux);
                        GetKgTaladrado(code, node, "M2", out kg_Punzonado_Chapa_Aux, out kg_Taladrado_Chapa_Aux);
                        GetKgTaladrado(code, node, "M5", out kg_Punzonado_UPN_Aux, out kg_Taladrado_UPN_Aux);
                        GetKgTaladrado(code, node, "M4", out kg_Punzonado_Tub_Aux, out kg_Taladrado_Tub_Aux);
                        kg_Punzonado_Angular += kg_Punzonado_Angular_Aux;
                        kg_Punzonado_Pletina += kg_Punzonado_Pletina_Aux;
                        kg_Punzonado_Chapa += kg_Punzonado_Chapa_Aux;
                        kg_Punzonado_UPN += kg_Punzonado_UPN_Aux;
                        kg_Punzonado_Tub += kg_Punzonado_Tub_Aux;
                        kg_Taladrado_Angular += kg_Taladrado_Angular_Aux;
                        kg_Taladrado_Pletina += kg_Taladrado_Pletina_Aux;
                        kg_Taladrado_Chapa += kg_Taladrado_Chapa_Aux;
                        kg_Taladrado_UPN += kg_Taladrado_UPN_Aux;
                        kg_Taladrado_Tub += kg_Taladrado_Tub_Aux;
                        ///////////////////////

                        decimal cantidadTO_ES_Aux = 0;
                        decimal cantidadTO_PA_Aux = 0;
                        decimal cantidadTO_PE_Aux = 0;
                        decimal cantidadTO_Aux_Aux = 0;
                        bool pesoCeroES_Aux = false;
                        bool pesoCeroPA_Aux = false;
                        bool pesoCeroPE_Aux = false;
                        bool pesoCeroAux_Aux = false;
                        kg_TO_ES += GetKgMaterial(code, node, "Estructural", out cantidadTO_ES_Aux, out pesoCeroES_Aux);
                        kg_TO_PA += GetKgMaterial(code, node, "Pate", out cantidadTO_PA_Aux, out pesoCeroPA_Aux);
                        kg_TO_PE += GetKgMaterial(code, node, "Perno", out cantidadTO_PE_Aux, out pesoCeroPE_Aux);
                        kg_TO_Aux += GetKgMaterial(code, node, "Auxiliar", out cantidadTO_Aux_Aux, out pesoCeroAux_Aux);
                        cantidadTO_ES += cantidadTO_ES_Aux;
                        cantidadTO_PA += cantidadTO_PA_Aux;
                        cantidadTO_PE += cantidadTO_PE_Aux;
                        cantidadTO_Aux += cantidadTO_Aux_Aux;
                        pesoCeroES = pesoCeroES || pesoCeroES_Aux;
                        pesoCeroPA = pesoCeroPA || pesoCeroPA_Aux;
                        pesoCeroPE = pesoCeroPE || pesoCeroPE_Aux;
                        pesoCeroAux = pesoCeroAux || pesoCeroAux_Aux;
                    }
                    js++;
                }
            //2. Rellenamos el dataGridViewMixMateriales con los datos calculados.
                
                
                getMaterialesInforme.RowCount = 13;


                getMaterialesInforme.Columns[0].HeaderCell.Value = "Mix de: " + code;


                getMaterialesInforme[0, 0].Value = "TEÓRICO";
                getMaterialesInforme[0, 0].Style.BackColor = Color.LightGray;
                getMaterialesInforme[0, 1].Value = "M.P.";
                getMaterialesInforme[0, 1].Style.BackColor = Color.LightGray;
                getMaterialesInforme[0, 2].Value = "Angular";
                getMaterialesInforme[0, 3].Value = "Pletina";
                getMaterialesInforme[0, 4].Value = "Chapa";
                getMaterialesInforme[0, 5].Value = "UPN-IPN-HEB";
                getMaterialesInforme[0, 6].Value = "Tub-Red";
                getMaterialesInforme[0, 7].Value = "TORNILLERIA";
                getMaterialesInforme[0, 7].Style.BackColor = Color.LightGray;
                getMaterialesInforme[0, 8].Value = "Estructural";
                if (pesoCeroES && (!radioButton_PesoSinTornillos.Checked)) getMaterialesInforme[0, 8].Style.BackColor = Color.Yellow;
                else getMaterialesInforme[0, 8].Style.BackColor = Color.White;
                getMaterialesInforme[0, 9].Value = "Pates";
            if (pesoCeroPA && (!radioButton_PesoSinTornillos.Checked)) getMaterialesInforme[0, 9].Style.BackColor = Color.Yellow;
            else getMaterialesInforme[0, 9].Style.BackColor = Color.White;
            getMaterialesInforme[0, 10].Value = "Pernos";
            if (pesoCeroPE && (!radioButton_PesoSinTornillos.Checked)) getMaterialesInforme[0, 10].Style.BackColor = Color.Yellow;
            else getMaterialesInforme[0, 10].Style.BackColor = Color.White;
            getMaterialesInforme[0, 11].Value = "Auxiliares";
            if (pesoCeroAux && (!radioButton_PesoSinTornillos.Checked)) getMaterialesInforme[0, 11].Style.BackColor = Color.Yellow;
            else getMaterialesInforme[0, 11].Style.BackColor = Color.White;
            getMaterialesInforme[0, 12].Value = "M.P.+TORN.";
            getMaterialesInforme[0, 12].Style.BackColor = Color.LightGray;


            //2.1 PESOS
            decimal sumaMP = kg_M1_Angular + kg_M3_Pletina + kg_M2_Chapa + kg_M5_UPN + kg_M4_Tub;
            //decimal sumaMP = Convert.ToInt32(kg_M1_Angular) + Convert.ToInt32(kg_M3_Pletina) + Convert.ToInt32(kg_M2_Chapa) + Convert.ToInt32(kg_M5_UPN) + Convert.ToInt32(kg_M4_Tub);
            getMaterialesInforme[2, 1].Value = Convert.ToInt32(sumaMP).ToString();
            getMaterialesInforme[2, 0].Style.BackColor = Color.LightGray;
            getMaterialesInforme[2, 1].Style.BackColor = Color.LightGray;
            getMaterialesInforme[2, 2].Value = Convert.ToInt32(kg_M1_Angular).ToString();
            getMaterialesInforme[2, 3].Value = Convert.ToInt32(kg_M3_Pletina).ToString();
            getMaterialesInforme[2, 4].Value = Convert.ToInt32(kg_M2_Chapa).ToString();
            getMaterialesInforme[2, 5].Value = Convert.ToInt32(kg_M5_UPN).ToString();
            getMaterialesInforme[2, 6].Value = Convert.ToInt32(kg_M4_Tub).ToString();

            decimal sumaTO = kg_TO_ES + kg_TO_PA + kg_TO_PE + kg_TO_Aux;
            //decimal sumaTO = Decimal.Round(kg_TO_ES, 1) + Decimal.Round(kg_TO_PA,1) + Decimal.Round(kg_TO_PE,1) + Decimal.Round(kg_TO_Aux,1);
            getMaterialesInforme[2, 7].Value = Decimal.Round(sumaTO, 1).ToString();
            getMaterialesInforme[2, 7].Style.BackColor = Color.LightGray;
            getMaterialesInforme[2, 8].Value = Decimal.Round(kg_TO_ES, 1).ToString();
            getMaterialesInforme[2, 9].Value = Decimal.Round(kg_TO_PA, 1).ToString();
            getMaterialesInforme[2, 10].Value = Decimal.Round(kg_TO_PE, 1).ToString();
            getMaterialesInforme[2, 11].Value = Decimal.Round(kg_TO_Aux, 1).ToString();
            getMaterialesInforme[2, 12].Value = Decimal.Round(sumaMP + sumaTO, 0).ToString();
            getMaterialesInforme[2, 12].Style.BackColor = Color.LightGray;
            
            //2.2 PORCENTAJES
            getMaterialesInforme[1, 1].Value = "100%";
            getMaterialesInforme[1, 0].Style.BackColor = Color.LightGray;
            getMaterialesInforme[1, 1].Style.BackColor = Color.LightGray;
            if (sumaMP > 0)
            {
                getMaterialesInforme[1, 2].Value = Convert.ToString(Decimal.Round((kg_M1_Angular / sumaMP) * 100, 1)) + "%";
                getMaterialesInforme[1, 3].Value = Convert.ToString(Decimal.Round((kg_M3_Pletina / sumaMP) * 100, 1)) + "%";
                getMaterialesInforme[1, 4].Value = Convert.ToString(Decimal.Round((kg_M2_Chapa / sumaMP) * 100, 1)) + "%";
                getMaterialesInforme[1, 5].Value = Convert.ToString(Decimal.Round((kg_M5_UPN / sumaMP) * 100, 1)) + "%";
                getMaterialesInforme[1, 6].Value = Convert.ToString(Decimal.Round((kg_M4_Tub / sumaMP) * 100, 1)) + "%";

                decimal sumaPorcTO = Decimal.Round((kg_TO_ES / sumaMP) * 100, 1) + Decimal.Round((kg_TO_PA / sumaMP) * 100, 1) +
                    Decimal.Round((kg_TO_PE / sumaMP) * 100, 1) + Decimal.Round((kg_TO_Aux / sumaMP) * 100, 1);
                getMaterialesInforme[1, 7].Value = Convert.ToString(Decimal.Round(sumaPorcTO, 1)) + "%";
                getMaterialesInforme[1, 7].Style.BackColor = Color.LightGray;
                getMaterialesInforme[1, 8].Value = Convert.ToString(Decimal.Round((kg_TO_ES / sumaMP) * 100, 1)) + "%";
                getMaterialesInforme[1, 9].Value = Convert.ToString(Decimal.Round((kg_TO_PA / sumaMP) * 100, 1)) + "%";
                getMaterialesInforme[1, 10].Value = Convert.ToString(Decimal.Round((kg_TO_PE / sumaMP) * 100, 1)) + "%";
                getMaterialesInforme[1, 11].Value = Convert.ToString(Decimal.Round((kg_TO_Aux / sumaMP) * 100, 1)) + "%";
                getMaterialesInforme[1, 12].Value = Convert.ToString(100 + sumaPorcTO) + "%";
                getMaterialesInforme[1, 12].Style.BackColor = Color.LightGray;

            }
            else
            {
                getMaterialesInforme[1, 2].Value = "0%";
                getMaterialesInforme[1, 3].Value = "0%";
                getMaterialesInforme[1, 4].Value = "0%";
                getMaterialesInforme[1, 5].Value = "0%";
                getMaterialesInforme[1, 6].Value = "0%";

                getMaterialesInforme[1, 7].Value = "0%";
                getMaterialesInforme[1, 8].Value = "0%";
                getMaterialesInforme[1, 9].Value = "0%";
                getMaterialesInforme[1, 10].Value = "0%";
                getMaterialesInforme[1, 11].Value = "0%";
                getMaterialesInforme[1, 12].Value = "100%";
                getMaterialesInforme[1, 12].Style.BackColor = Color.LightGray;
            }
            

            //Hacemos editable la celda del peso teorico
            //dataGridViewMixMateriales[2,0].ReadOnly = false;
            getMaterialesInforme.Columns[1].Width = 50;
            getMaterialesInforme.Columns[2].Width = 40;
            getMaterialesInforme.Columns[3].Width = 60;

            //Si tenemos dato en el peso teorico, podemos calcular los coeficientes
            //2.3 COEFICIENTES
            try
            {
                getMaterialesInforme[3, 0].Style.BackColor = Color.LightGray;
                getMaterialesInforme[3, 1].Style.BackColor = Color.LightGray;
                getMaterialesInforme[3, 5].Style.BackColor = Color.LightGray;
                getMaterialesInforme[3, 6].Style.BackColor = Color.LightGray;
                getMaterialesInforme[3, 7].Style.BackColor = Color.LightGray;
                getMaterialesInforme[3, 12].Style.BackColor = Color.LightGray;

                //Peso teorico

                getMaterialesInforme[2, 0].Value = "";
                getMaterialesInforme[3, 1].Value = "";
                getMaterialesInforme[3, 2].Value = "";
                getMaterialesInforme[3, 3].Value = "";
                getMaterialesInforme[3, 4].Value = "";
                getMaterialesInforme[3, 7].Value = "";
                getMaterialesInforme[3, 8].Value = "";
                getMaterialesInforme[3, 9].Value = "";
                getMaterialesInforme[3, 10].Value = "";
                getMaterialesInforme[3, 11].Value = "";
                getMaterialesInforme[3, 12].Value = "";
                      
                
            }
            catch (Exception ex)
            {
                
            }
             //2.4 CANTIDADES DE TORNILLOS
            getMaterialesInforme[4, 0].Style.BackColor = Color.LightGray;
            getMaterialesInforme[4, 1].Style.BackColor = Color.LightGray;
            getMaterialesInforme[4, 2].Style.BackColor = Color.LightGray;
            getMaterialesInforme[4, 3].Style.BackColor = Color.LightGray;
            getMaterialesInforme[4, 4].Style.BackColor = Color.LightGray;
            getMaterialesInforme[4, 5].Style.BackColor = Color.LightGray;
            getMaterialesInforme[4, 6].Style.BackColor = Color.LightGray;
            getMaterialesInforme[4, 7].Style.BackColor = Color.LightGray;
            getMaterialesInforme[4, 12].Style.BackColor = Color.LightGray;

            getMaterialesInforme[4, 7].Value = "";
            getMaterialesInforme[4, 8].Value = cantidadTO_ES.ToString();
            getMaterialesInforme[4, 9].Value = cantidadTO_PA.ToString();
            getMaterialesInforme[4, 10].Value = cantidadTO_PE.ToString();
            getMaterialesInforme[4, 11].Value = cantidadTO_Aux.ToString();
            

            //KG PUNZONADOS
            getMaterialesInforme[5, 0].Style.BackColor = Color.LightGray;

            decimal sumaPunzonado = kg_Punzonado_Angular + kg_Punzonado_Pletina + kg_Punzonado_Chapa + kg_Punzonado_UPN + kg_Punzonado_Tub;
            getMaterialesInforme[5, 1].Value = Convert.ToInt32(sumaPunzonado).ToString();
            getMaterialesInforme[5, 1].Style.BackColor = Color.LightGray;
            getMaterialesInforme[5, 2].Value = Convert.ToInt32(kg_Punzonado_Angular).ToString();
            getMaterialesInforme[5, 3].Value = Convert.ToInt32(kg_Punzonado_Pletina).ToString();
            getMaterialesInforme[5, 4].Value = Convert.ToInt32(kg_Punzonado_Chapa).ToString();
            getMaterialesInforme[5, 5].Value = Convert.ToInt32(kg_Punzonado_UPN).ToString();
            getMaterialesInforme[5, 6].Value = Convert.ToInt32(kg_Punzonado_Tub).ToString();

            getMaterialesInforme[5, 7].Style.BackColor = Color.LightGray;
            getMaterialesInforme[5, 8].Style.BackColor = Color.LightGray;
            getMaterialesInforme[5, 9].Style.BackColor = Color.LightGray;
            getMaterialesInforme[5, 10].Style.BackColor = Color.LightGray;
            getMaterialesInforme[5, 11].Style.BackColor = Color.LightGray;
            getMaterialesInforme[5, 12].Style.BackColor = Color.LightGray;

            //KG TALADRADO
            getMaterialesInforme[6, 0].Style.BackColor = Color.LightGray;

            decimal sumaTaladrado = kg_Taladrado_Angular + kg_Taladrado_Pletina + kg_Taladrado_Chapa + kg_Taladrado_UPN + kg_Taladrado_Tub;
            getMaterialesInforme[6, 1].Value = Convert.ToInt32(sumaTaladrado).ToString();
            getMaterialesInforme[6, 1].Style.BackColor = Color.LightGray;
            getMaterialesInforme[6, 2].Value = Convert.ToInt32(kg_Taladrado_Angular).ToString();
            getMaterialesInforme[6, 3].Value = Convert.ToInt32(kg_Taladrado_Pletina).ToString();
            getMaterialesInforme[6, 4].Value = Convert.ToInt32(kg_Taladrado_Chapa).ToString();
            getMaterialesInforme[6, 5].Value = Convert.ToInt32(kg_Taladrado_UPN).ToString();
            getMaterialesInforme[6, 6].Value = Convert.ToInt32(kg_Taladrado_Tub).ToString();

            getMaterialesInforme[6, 7].Style.BackColor = Color.LightGray;
            getMaterialesInforme[6, 8].Style.BackColor = Color.LightGray;
            getMaterialesInforme[6, 9].Style.BackColor = Color.LightGray;
            getMaterialesInforme[6, 10].Style.BackColor = Color.LightGray;
            getMaterialesInforme[6, 11].Style.BackColor = Color.LightGray;
            getMaterialesInforme[6, 12].Style.BackColor = Color.LightGray;

            //se copia dataGridViewMixMateriales a una dataGridView global para usarse en el informe de características, se limpia previamente
            //getMaterialesInforme.Rows.Clear();
            //getMaterialesInforme.Refresh();
            DataGridView getMaterialesInforme1 = getMaterialesInforme;

            if (getMaterialesInforme1 != null)
            {
                System.Diagnostics.Debug.WriteLine("PROBANDO -> getMaterialesInforme no es null, getMaterialesInforme[1, 2].Value contiene: " + getMaterialesInforme[1, 2].Value);//+ " y getMaterialesInforme[1, 2].Value contiene: " + getMaterialesInforme[1, 2].Value);
            }

            return getMaterialesInforme1;
        }




        public void MostrarMixMateriales(List<DataGridViewCell> dgVC_List, List<TreeGridNode> node_List)
        {
            dataGridViewMixMateriales.Columns[0].HeaderCell.Style.BackColor = Color.Gray;
            dataGridViewMixMateriales.Columns[1].HeaderCell.Style.BackColor = Color.Gray;
            dataGridViewMixMateriales.Columns[2].HeaderCell.Style.BackColor = Color.Gray;
            dataGridViewMixMateriales.Columns[3].HeaderCell.Style.BackColor = Color.Gray;
            dataGridViewMixMateriales.Columns[4].HeaderCell.Style.BackColor = Color.Gray;
            dataGridViewMixMateriales.Columns[5].HeaderCell.Style.BackColor = Color.Gray;
            dataGridViewMixMateriales.Columns[6].HeaderCell.Style.BackColor = Color.Gray;


            //1. Vamos a recorrer el arbol, para ver todos los elementos del nodo, y poder mostrar el MIX de los materiales.
            decimal kg_M1_Angular = 0;
            decimal kg_M3_Pletina = 0;
            decimal kg_M2_Chapa = 0;
            decimal kg_M5_UPN = 0;
            decimal kg_M4_Tub = 0;

            decimal kg_TO_ES = 0;
            decimal kg_TO_PA = 0;
            decimal kg_TO_PE = 0;
            decimal kg_TO_Aux = 0;

            decimal cantidadTO_ES = 0;
            decimal cantidadTO_PA = 0;
            decimal cantidadTO_PE = 0;
            decimal cantidadTO_Aux = 0;
            bool pesoCeroES = false;
            bool pesoCeroPA = false;
            bool pesoCeroPE = false;
            bool pesoCeroAux = false;

            decimal cantidadTotal = 0;//Para la MP, esta variable no la usamos
            bool pesoCero = false;//Para la MP, esta variable no la usamos

            decimal kg_Punzonado_Angular = 0;
            decimal kg_Punzonado_Pletina = 0;
            decimal kg_Punzonado_Chapa = 0;
            decimal kg_Punzonado_UPN = 0;
            decimal kg_Punzonado_Tub = 0;
            decimal kg_Taladrado_Angular = 0;
            decimal kg_Taladrado_Pletina = 0;
            decimal kg_Taladrado_Chapa = 0;
            decimal kg_Taladrado_UPN = 0;
            decimal kg_Taladrado_Tub = 0;

            for (int i = 0; i < dgVC_List.Count; i++)
            {
                DataGridViewCell dgVC = dgVC_List[i];
                TreeGridNode node = node_List[i];

                System.Diagnostics.Debug.WriteLine("dgVC_List.Count contiene: " + dgVC_List.Count + " dgVC_List[i] contiene: --> " + dgVC.Value.ToString() + " y node: " + node);

                kg_M1_Angular += GetKgMaterial(dgVC.Value.ToString(), node, "M1", out cantidadTotal, out pesoCero);
                kg_M3_Pletina += GetKgMaterial(dgVC.Value.ToString(), node, "M3", out cantidadTotal, out pesoCero);
                kg_M2_Chapa += GetKgMaterial(dgVC.Value.ToString(), node, "M2", out cantidadTotal, out pesoCero);
                kg_M5_UPN += GetKgMaterial(dgVC.Value.ToString(), node, "M5", out cantidadTotal, out pesoCero);
                kg_M4_Tub += GetKgMaterial(dgVC.Value.ToString(), node, "M4", out cantidadTotal, out pesoCero);

                //Guardamos los kg para los punzonados/taladrado
                decimal kg_Punzonado_Angular_Aux = 0;
                decimal kg_Punzonado_Pletina_Aux = 0;
                decimal kg_Punzonado_Chapa_Aux = 0;
                decimal kg_Punzonado_UPN_Aux = 0;
                decimal kg_Punzonado_Tub_Aux = 0;
                decimal kg_Taladrado_Angular_Aux = 0;
                decimal kg_Taladrado_Pletina_Aux = 0;
                decimal kg_Taladrado_Chapa_Aux = 0;
                decimal kg_Taladrado_UPN_Aux = 0;
                decimal kg_Taladrado_Tub_Aux = 0;
                GetKgTaladrado(dgVC.Value.ToString(), node, "M1", out kg_Punzonado_Angular_Aux, out kg_Taladrado_Angular_Aux);
                GetKgTaladrado(dgVC.Value.ToString(), node, "M3", out kg_Punzonado_Pletina_Aux, out kg_Taladrado_Pletina_Aux);
                GetKgTaladrado(dgVC.Value.ToString(), node, "M2", out kg_Punzonado_Chapa_Aux, out kg_Taladrado_Chapa_Aux);
                GetKgTaladrado(dgVC.Value.ToString(), node, "M5", out kg_Punzonado_UPN_Aux, out kg_Taladrado_UPN_Aux);
                GetKgTaladrado(dgVC.Value.ToString(), node, "M4", out kg_Punzonado_Tub_Aux, out kg_Taladrado_Tub_Aux);
                kg_Punzonado_Angular += kg_Punzonado_Angular_Aux;
                kg_Punzonado_Pletina += kg_Punzonado_Pletina_Aux;
                kg_Punzonado_Chapa += kg_Punzonado_Chapa_Aux;
                kg_Punzonado_UPN += kg_Punzonado_UPN_Aux;
                kg_Punzonado_Tub += kg_Punzonado_Tub_Aux;
                kg_Taladrado_Angular += kg_Taladrado_Angular_Aux;
                kg_Taladrado_Pletina += kg_Taladrado_Pletina_Aux;
                kg_Taladrado_Chapa += kg_Taladrado_Chapa_Aux;
                kg_Taladrado_UPN += kg_Taladrado_UPN_Aux;
                kg_Taladrado_Tub += kg_Taladrado_Tub_Aux;
                ///////////////////////

                decimal cantidadTO_ES_Aux = 0;
                decimal cantidadTO_PA_Aux = 0;
                decimal cantidadTO_PE_Aux = 0;
                decimal cantidadTO_Aux_Aux = 0;
                bool pesoCeroES_Aux = false;
                bool pesoCeroPA_Aux = false;
                bool pesoCeroPE_Aux = false;
                bool pesoCeroAux_Aux = false;
                kg_TO_ES += GetKgMaterial(dgVC.Value.ToString(), node, "Estructural", out cantidadTO_ES_Aux, out pesoCeroES_Aux);
                kg_TO_PA += GetKgMaterial(dgVC.Value.ToString(), node, "Pate", out cantidadTO_PA_Aux, out pesoCeroPA_Aux);
                kg_TO_PE += GetKgMaterial(dgVC.Value.ToString(), node, "Perno", out cantidadTO_PE_Aux, out pesoCeroPE_Aux);
                kg_TO_Aux += GetKgMaterial(dgVC.Value.ToString(), node, "Auxiliar", out cantidadTO_Aux_Aux, out pesoCeroAux_Aux);
                cantidadTO_ES += cantidadTO_ES_Aux;
                cantidadTO_PA += cantidadTO_PA_Aux;
                cantidadTO_PE += cantidadTO_PE_Aux;
                cantidadTO_Aux += cantidadTO_Aux_Aux;
                pesoCeroES = pesoCeroES || pesoCeroES_Aux;
                pesoCeroPA = pesoCeroPA || pesoCeroPA_Aux;
                pesoCeroPE = pesoCeroPE || pesoCeroPE_Aux;
                pesoCeroAux = pesoCeroAux || pesoCeroAux_Aux;

            }
            //2. Rellenamos el dataGridViewMixMateriales con los datos calculados.
            dataGridViewMixMateriales.RowCount = 13;

            if (dgVC_List.Count()==1)
                dataGridViewMixMateriales.Columns[0].HeaderCell.Value = "Mix de: " + dgVC_List[0].Value.ToString();
            else
                dataGridViewMixMateriales.Columns[0].HeaderCell.Value = "Mix de: (varios)" ;

            dataGridViewMixMateriales[0, 0].Value = "TEÓRICO";
            dataGridViewMixMateriales[0, 0].Style.BackColor = Color.LightGray;
            dataGridViewMixMateriales[0, 1].Value = "M.P.";
            dataGridViewMixMateriales[0, 1].Style.BackColor = Color.LightGray;
            dataGridViewMixMateriales[0, 2].Value = "Angular";
            dataGridViewMixMateriales[0, 3].Value = "Pletina";
            dataGridViewMixMateriales[0, 4].Value = "Chapa";
            dataGridViewMixMateriales[0, 5].Value = "UPN-IPN-HEB";
            dataGridViewMixMateriales[0, 6].Value = "Tub-Red";
            dataGridViewMixMateriales[0, 7].Value = "TORNILLERIA";
            dataGridViewMixMateriales[0, 7].Style.BackColor = Color.LightGray;
            dataGridViewMixMateriales[0, 8].Value = "Estructural";
            if (pesoCeroES && (!radioButton_PesoSinTornillos.Checked))  dataGridViewMixMateriales[0, 8].Style.BackColor = Color.Yellow;
            else dataGridViewMixMateriales[0, 8].Style.BackColor = Color.White;
            dataGridViewMixMateriales[0, 9].Value = "Pates";
            if (pesoCeroPA && (!radioButton_PesoSinTornillos.Checked)) dataGridViewMixMateriales[0, 9].Style.BackColor = Color.Yellow;
            else dataGridViewMixMateriales[0, 9].Style.BackColor = Color.White;
            dataGridViewMixMateriales[0, 10].Value = "Pernos";
            if (pesoCeroPE && (!radioButton_PesoSinTornillos.Checked)) dataGridViewMixMateriales[0, 10].Style.BackColor = Color.Yellow;
            else dataGridViewMixMateriales[0, 10].Style.BackColor = Color.White;
            dataGridViewMixMateriales[0, 11].Value = "Auxiliares";
            if (pesoCeroAux && (!radioButton_PesoSinTornillos.Checked)) dataGridViewMixMateriales[0, 11].Style.BackColor = Color.Yellow;
            else dataGridViewMixMateriales[0, 11].Style.BackColor = Color.White;
            dataGridViewMixMateriales[0, 12].Value = "M.P.+TORN.";
            dataGridViewMixMateriales[0, 12].Style.BackColor = Color.LightGray;


            //2.1 PESOS
            decimal sumaMP = kg_M1_Angular + kg_M3_Pletina + kg_M2_Chapa + kg_M5_UPN + kg_M4_Tub;
            //decimal sumaMP = Convert.ToInt32(kg_M1_Angular) + Convert.ToInt32(kg_M3_Pletina) + Convert.ToInt32(kg_M2_Chapa) + Convert.ToInt32(kg_M5_UPN) + Convert.ToInt32(kg_M4_Tub);
            dataGridViewMixMateriales[2, 1].Value = Convert.ToInt32(sumaMP).ToString();
            dataGridViewMixMateriales[2, 0].Style.BackColor = Color.LightGray;
            dataGridViewMixMateriales[2, 1].Style.BackColor = Color.LightGray;
            dataGridViewMixMateriales[2, 2].Value = Convert.ToInt32(kg_M1_Angular).ToString();
            dataGridViewMixMateriales[2, 3].Value = Convert.ToInt32(kg_M3_Pletina).ToString();
            dataGridViewMixMateriales[2, 4].Value = Convert.ToInt32(kg_M2_Chapa).ToString();
            dataGridViewMixMateriales[2, 5].Value = Convert.ToInt32(kg_M5_UPN).ToString();
            dataGridViewMixMateriales[2, 6].Value = Convert.ToInt32(kg_M4_Tub).ToString();

            decimal sumaTO = kg_TO_ES + kg_TO_PA + kg_TO_PE + kg_TO_Aux;
            //decimal sumaTO = Decimal.Round(kg_TO_ES, 1) + Decimal.Round(kg_TO_PA,1) + Decimal.Round(kg_TO_PE,1) + Decimal.Round(kg_TO_Aux,1);
            dataGridViewMixMateriales[2, 7].Value = Decimal.Round(sumaTO,1).ToString();
            dataGridViewMixMateriales[2, 7].Style.BackColor = Color.LightGray;
            dataGridViewMixMateriales[2, 8].Value = Decimal.Round(kg_TO_ES,1).ToString();
            dataGridViewMixMateriales[2, 9].Value = Decimal.Round(kg_TO_PA,1).ToString();
            dataGridViewMixMateriales[2, 10].Value = Decimal.Round(kg_TO_PE,1).ToString();
            dataGridViewMixMateriales[2, 11].Value = Decimal.Round(kg_TO_Aux,1).ToString();
            dataGridViewMixMateriales[2, 12].Value = Decimal.Round(sumaMP + sumaTO, 0).ToString();
            dataGridViewMixMateriales[2, 12].Style.BackColor = Color.LightGray;
            
            //2.2 PORCENTAJES
            dataGridViewMixMateriales[1, 1].Value = "100%";
            dataGridViewMixMateriales[1, 0].Style.BackColor = Color.LightGray;
            dataGridViewMixMateriales[1, 1].Style.BackColor = Color.LightGray;
            if (sumaMP > 0)
            {
                dataGridViewMixMateriales[1, 2].Value = Convert.ToString(Decimal.Round((kg_M1_Angular / sumaMP) * 100, 1)) + "%";
                dataGridViewMixMateriales[1, 3].Value = Convert.ToString(Decimal.Round((kg_M3_Pletina / sumaMP) * 100, 1)) + "%";
                dataGridViewMixMateriales[1, 4].Value = Convert.ToString(Decimal.Round((kg_M2_Chapa / sumaMP) * 100, 1)) + "%";
                dataGridViewMixMateriales[1, 5].Value = Convert.ToString(Decimal.Round((kg_M5_UPN / sumaMP) * 100, 1)) + "%";
                dataGridViewMixMateriales[1, 6].Value = Convert.ToString(Decimal.Round((kg_M4_Tub / sumaMP) * 100, 1)) + "%";

                decimal sumaPorcTO = Decimal.Round((kg_TO_ES / sumaMP) * 100, 1) + Decimal.Round((kg_TO_PA / sumaMP) * 100, 1) +
                    Decimal.Round((kg_TO_PE / sumaMP) * 100, 1) + Decimal.Round((kg_TO_Aux / sumaMP) * 100, 1);
                dataGridViewMixMateriales[1, 7].Value = Convert.ToString(Decimal.Round(sumaPorcTO, 1)) + "%";
                dataGridViewMixMateriales[1, 7].Style.BackColor = Color.LightGray;
                dataGridViewMixMateriales[1, 8].Value = Convert.ToString(Decimal.Round((kg_TO_ES / sumaMP) * 100, 1)) + "%";
                dataGridViewMixMateriales[1, 9].Value = Convert.ToString(Decimal.Round((kg_TO_PA / sumaMP) * 100, 1)) + "%";
                dataGridViewMixMateriales[1, 10].Value = Convert.ToString(Decimal.Round((kg_TO_PE / sumaMP) * 100, 1)) + "%";
                dataGridViewMixMateriales[1, 11].Value = Convert.ToString(Decimal.Round((kg_TO_Aux / sumaMP) * 100, 1)) + "%";
                dataGridViewMixMateriales[1, 12].Value = Convert.ToString(100+sumaPorcTO) + "%";
                dataGridViewMixMateriales[1, 12].Style.BackColor = Color.LightGray;

            }
            else
            {
                dataGridViewMixMateriales[1, 2].Value = "0%";
                dataGridViewMixMateriales[1, 3].Value = "0%";
                dataGridViewMixMateriales[1, 4].Value = "0%";
                dataGridViewMixMateriales[1, 5].Value = "0%";
                dataGridViewMixMateriales[1, 6].Value = "0%";

                dataGridViewMixMateriales[1, 7].Value = "0%";
                dataGridViewMixMateriales[1, 8].Value = "0%";
                dataGridViewMixMateriales[1, 9].Value = "0%";
                dataGridViewMixMateriales[1, 10].Value ="0%";
                dataGridViewMixMateriales[1, 11].Value ="0%";
                dataGridViewMixMateriales[1, 12].Value = "100%";
                dataGridViewMixMateriales[1, 12].Style.BackColor = Color.LightGray;
            }
            

            //Hacemos editable la celda del peso teorico
            //dataGridViewMixMateriales[2,0].ReadOnly = false;
            dataGridViewMixMateriales.Columns[1].Width = 50;
            dataGridViewMixMateriales.Columns[2].Width = 40;
            dataGridViewMixMateriales.Columns[3].Width=60;

            //Si tenemos dato en el peso teorico, podemos calcular los coeficientes
            //2.3 COEFICIENTES
            try
            {
                dataGridViewMixMateriales[3, 0].Style.BackColor = Color.LightGray;
                dataGridViewMixMateriales[3, 1].Style.BackColor = Color.LightGray;
                dataGridViewMixMateriales[3, 5].Style.BackColor = Color.LightGray;
                dataGridViewMixMateriales[3, 6].Style.BackColor = Color.LightGray;
                dataGridViewMixMateriales[3, 7].Style.BackColor = Color.LightGray;
                dataGridViewMixMateriales[3, 12].Style.BackColor = Color.LightGray;

                //Consultamos la caracteristica de peso teorico
                decimal pesoTeorico = getCaracteristica(dgVC_List, 613);
                if (pesoTeorico > 0)
                    dataGridViewMixMateriales[2, 0].Value = pesoTeorico.ToString();
                else
                {
                    dataGridViewMixMateriales[2, 0].Value = "";
                    dataGridViewMixMateriales[3, 1].Value = "";
                    dataGridViewMixMateriales[3, 2].Value = "";
                    dataGridViewMixMateriales[3, 3].Value = "";
                    dataGridViewMixMateriales[3, 4].Value = "";
                    dataGridViewMixMateriales[3, 7].Value = "";
                    dataGridViewMixMateriales[3, 8].Value = "";
                    dataGridViewMixMateriales[3, 9].Value = "";
                    dataGridViewMixMateriales[3, 10].Value = "";
                    dataGridViewMixMateriales[3, 11].Value = "";
                    dataGridViewMixMateriales[3, 12].Value = "";
                }
                
                


                if ((dataGridViewMixMateriales[2, 0].Value != null) && (dataGridViewMixMateriales[2, 0].Value.ToString() != ""))
                {                                                                               
                    decimal coeficiente = Decimal.Round(sumaMP / pesoTeorico, 3);
                    dataGridViewMixMateriales[3, 1].Value = coeficiente.ToString();

                    decimal CM1 = ((kg_M1_Angular + kg_M4_Tub + kg_M5_UPN) - pesoTeorico) /
                                    ((kg_M1_Angular + kg_M4_Tub + kg_M5_UPN - pesoTeorico) + kg_M2_Chapa + kg_M3_Pletina);
                    string s = (Decimal.Round(CM1 * 100, 0)).ToString() + '%';
                    decimal coefAux = Decimal.Round(CM1 * (coeficiente - 1), 3);
                    s += " " + coefAux.ToString();
                    dataGridViewMixMateriales[3, 2].Value = s;

                    decimal CM3 = kg_M3_Pletina /
                                    ((kg_M1_Angular + kg_M4_Tub + kg_M5_UPN - pesoTeorico) + kg_M2_Chapa + kg_M3_Pletina);
                    s = (Decimal.Round(CM3 * 100, 0)).ToString() + '%';
                    coefAux = Decimal.Round(CM3 * (coeficiente - 1), 3);
                    s += " " + coefAux.ToString();
                    dataGridViewMixMateriales[3, 3].Value = s;

                    decimal CM2 = kg_M2_Chapa /
                                    ((kg_M1_Angular + kg_M4_Tub + kg_M5_UPN - pesoTeorico) + kg_M2_Chapa + kg_M3_Pletina);
                    s = (Decimal.Round(CM2 * 100, 0)).ToString() + '%';
                    coefAux = Decimal.Round(CM2 * (coeficiente - 1), 3);
                    s += " " + coefAux.ToString();
                    dataGridViewMixMateriales[3, 4].Value = s;

                    decimal coeficienteTO = Decimal.Round(sumaTO / pesoTeorico, 3);
                    dataGridViewMixMateriales[3, 7].Value = coeficienteTO.ToString();

                    decimal CES = (kg_TO_ES / sumaTO);
                    s = (Decimal.Round(CES * 100, 0)).ToString() + '%';
                    coefAux = Decimal.Round(CES * (coeficienteTO), 3);
                    s += " " + coefAux.ToString();
                    dataGridViewMixMateriales[3, 8].Value = s;

                    decimal CPA = (kg_TO_PA / sumaTO);
                    s = (Decimal.Round(CPA * 100, 0)).ToString() + '%';
                    coefAux = Decimal.Round(CPA * (coeficienteTO), 3);
                    s += " " + coefAux.ToString();
                    dataGridViewMixMateriales[3, 9].Value = s;

                    decimal CPE = (kg_TO_PE / sumaTO);
                    s = (Decimal.Round(CPE * 100, 0)).ToString() + '%';
                    coefAux = Decimal.Round(CPE * (coeficienteTO), 3);
                    s += " " + coefAux.ToString();
                    dataGridViewMixMateriales[3, 10].Value = s;

                    decimal CAux = (kg_TO_Aux / sumaTO);
                    s = (Decimal.Round(CAux * 100, 0)).ToString() + '%';
                    coefAux = Decimal.Round(CAux * (coeficienteTO), 3);
                    s += " " + coefAux.ToString();
                    dataGridViewMixMateriales[3, 11].Value = s;

                    decimal CMTO = Decimal.Round((sumaMP + sumaTO) / pesoTeorico, 3);
                    dataGridViewMixMateriales[3, 12].Value = CMTO.ToString();

                }
                
            }
            catch (Exception ex)
            {
                
            }
             //2.4 CANTIDADES DE TORNILLOS
            dataGridViewMixMateriales[4,0].Style.BackColor = Color.LightGray;
            dataGridViewMixMateriales[4,1].Style.BackColor = Color.LightGray;
            dataGridViewMixMateriales[4,2].Style.BackColor = Color.LightGray;
            dataGridViewMixMateriales[4,3].Style.BackColor = Color.LightGray;
            dataGridViewMixMateriales[4,4].Style.BackColor = Color.LightGray;
            dataGridViewMixMateriales[4,5].Style.BackColor = Color.LightGray;
            dataGridViewMixMateriales[4,6].Style.BackColor = Color.LightGray;
            dataGridViewMixMateriales[4,7].Style.BackColor = Color.LightGray;
            dataGridViewMixMateriales[4,12].Style.BackColor = Color.LightGray;

            dataGridViewMixMateriales[4, 7].Value = "";
            dataGridViewMixMateriales[4, 8].Value = cantidadTO_ES.ToString();
            dataGridViewMixMateriales[4, 9].Value = cantidadTO_PA.ToString();
            dataGridViewMixMateriales[4, 10].Value = cantidadTO_PE.ToString();
            dataGridViewMixMateriales[4, 11].Value = cantidadTO_Aux.ToString();
            

            //KG PUNZONADOS
            dataGridViewMixMateriales[5, 0].Style.BackColor = Color.LightGray;

            decimal sumaPunzonado = kg_Punzonado_Angular + kg_Punzonado_Pletina + kg_Punzonado_Chapa + kg_Punzonado_UPN + kg_Punzonado_Tub;
            dataGridViewMixMateriales[5, 1].Value = Convert.ToInt32(sumaPunzonado).ToString();            
            dataGridViewMixMateriales[5, 1].Style.BackColor = Color.LightGray;
            dataGridViewMixMateriales[5, 2].Value = Convert.ToInt32(kg_Punzonado_Angular).ToString();
            dataGridViewMixMateriales[5, 3].Value = Convert.ToInt32(kg_Punzonado_Pletina).ToString();
            dataGridViewMixMateriales[5, 4].Value = Convert.ToInt32(kg_Punzonado_Chapa).ToString();
            dataGridViewMixMateriales[5, 5].Value = Convert.ToInt32(kg_Punzonado_UPN).ToString();
            dataGridViewMixMateriales[5, 6].Value = Convert.ToInt32(kg_Punzonado_Tub).ToString();

            dataGridViewMixMateriales[5, 7].Style.BackColor = Color.LightGray;
            dataGridViewMixMateriales[5, 8].Style.BackColor = Color.LightGray;
            dataGridViewMixMateriales[5, 9].Style.BackColor = Color.LightGray;
            dataGridViewMixMateriales[5, 10].Style.BackColor = Color.LightGray;
            dataGridViewMixMateriales[5, 11].Style.BackColor = Color.LightGray;
            dataGridViewMixMateriales[5, 12].Style.BackColor = Color.LightGray;

            //KG TALADRADO
            dataGridViewMixMateriales[6, 0].Style.BackColor = Color.LightGray;

            decimal sumaTaladrado = kg_Taladrado_Angular + kg_Taladrado_Pletina + kg_Taladrado_Chapa + kg_Taladrado_UPN + kg_Taladrado_Tub;
            dataGridViewMixMateriales[6, 1].Value = Convert.ToInt32(sumaTaladrado).ToString();
            dataGridViewMixMateriales[6, 1].Style.BackColor = Color.LightGray;
            dataGridViewMixMateriales[6, 2].Value = Convert.ToInt32(kg_Taladrado_Angular).ToString();
            dataGridViewMixMateriales[6, 3].Value = Convert.ToInt32(kg_Taladrado_Pletina).ToString();
            dataGridViewMixMateriales[6, 4].Value = Convert.ToInt32(kg_Taladrado_Chapa).ToString();
            dataGridViewMixMateriales[6, 5].Value = Convert.ToInt32(kg_Taladrado_UPN).ToString();
            dataGridViewMixMateriales[6, 6].Value = Convert.ToInt32(kg_Taladrado_Tub).ToString();

            dataGridViewMixMateriales[6, 7].Style.BackColor = Color.LightGray;
            dataGridViewMixMateriales[6, 8].Style.BackColor = Color.LightGray;
            dataGridViewMixMateriales[6, 9].Style.BackColor = Color.LightGray;
            dataGridViewMixMateriales[6, 10].Style.BackColor = Color.LightGray;
            dataGridViewMixMateriales[6, 11].Style.BackColor = Color.LightGray;
            dataGridViewMixMateriales[6, 12].Style.BackColor = Color.LightGray;

            //se copia dataGridViewMixMateriales a una dataGridView global para usarse en el informe de características, se limpia previamente
            //getMaterialesInforme.Rows.Clear();
            //getMaterialesInforme.Refresh();
            getMaterialesInforme = dataGridViewMixMateriales;
            //getMaterialesInforme = obtenerMixMateriales("COX-12-3SCS4CSCH", node_List);

            if (getMaterialesInforme != null)
            {
                System.Diagnostics.Debug.WriteLine("getMaterialesInforme no es null, dataGridViewMixMateriales[1, 2].Value contiene: " + dataGridViewMixMateriales[1, 2].Value + " y getMaterialesInforme[1, 2].Value contiene: " + getMaterialesInforme[1, 2].Value);
            }

        }

        public static void GetKgTaladrado(String codigo, TreeGridNode node, string Categoria_MP, out decimal kg_Punzonado, out decimal kg_Taladrado)
        {

            kg_Punzonado = 0;
            kg_Taladrado = 0;

            string codigo_Padre = "";
            if (node.Parent.Cells[0].Value != null)
                codigo_Padre = node.Parent.Cells[0].Value.ToString();
            else
                codigo_Padre = codigo;

            string s = "Codigo_Hijo = '" + codigo + "' and Codigo_Padre = '" + codigo_Padre + "'";

            DataRow dr = GetDataRow(s);
            if (dr == null)
            {
                kg_Punzonado = 0;
                kg_Taladrado = 0;
            }

            decimal cantidad = Convert.ToDecimal(dr["Cantidad_Hijo"]);

            if ((dr["Categoria_MP"].ToString() == Categoria_MP))
            {
                //Nos aseguramos que tenga peso.
                if (!String.IsNullOrEmpty(dr["PesoUnitario"].ToString()) && (!String.IsNullOrEmpty(dr["Taladrado"].ToString())))
                {
                    string taladrado = dr["Taladrado"].ToString();
                    if (taladrado=="S")
                        kg_Taladrado = Convert.ToDecimal(dr["PesoUnitario"]) * cantidad;    
                    else
                        kg_Punzonado = Convert.ToDecimal(dr["PesoUnitario"]) * cantidad;                        
                }
                else
                {
                    kg_Punzonado = 0;
                    kg_Taladrado = 0;
                }
                
            }

            if (node.HasChildren)
                foreach (TreeGridNode nodeAux in node.Nodes)
                {
                    decimal kg_Punzonado_Aux = 0;
                    decimal kg_Taladrado_Aux  = 0;
                    GetKgTaladrado(nodeAux.Cells[0].Value.ToString(), nodeAux, Categoria_MP, out kg_Punzonado_Aux, out kg_Taladrado_Aux);
                    kg_Punzonado += kg_Punzonado_Aux * cantidad;
                    kg_Taladrado += kg_Taladrado_Aux * cantidad;                    
                }            
        }

        //Buscamos en dataGridView_caracteristicas y devolvemos el valor de la caracteristica, si la encuentra.
        private decimal getCaracteristica(List<DataGridViewCell> dgVC_List, int num)
        {
            decimal valor = 0;
            foreach (DataGridViewCell row in dgVC_List)
            {
                //if (row.Cells["id_caracteristica"].Value.ToString() == num.ToString())
                //{
                //    if ((row.Cells["valor_caracteristica"].Value!=null) && (row.Cells["valor_caracteristica"].Value.ToString()!="")
                //        && (row.Cells["valor_caracteristica"].Value.ToString()!="???"))
                //        valor = Convert.ToDecimal(row.Cells["valor_caracteristica"].Value);
                //}

                try
                {
                    string codigo = row.Value.ToString();
                    SqlConnection conexion = new SqlConnection(cadenaConexion);
                    conexion.Open();

                    string strsql = "";
                    strsql = strsql + " select";
                    strsql = strsql + "         case valor ";
                    strsql = strsql + "         when '???' then '' ";
                    strsql = strsql + "         else valor ";
                    strsql = strsql + "         end ";
                    strsql = strsql + "         from t_artcar ";
                    strsql = strsql + "         where codemp = '" + Codemp + "' and codigo = '" + codigo + "' and caract ='613'";


                    SqlCommand comando = new SqlCommand(strsql, conexion);
                    SqlDataAdapter adapter = new SqlDataAdapter(comando);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    if (table.Rows.Count == 0)
                    {
                        valor = 0;
                        break;
                    }
                    else
                    {
                        if (table.Rows[0][0].ToString() != "")
                            valor += Convert.ToDecimal(table.Rows[0][0]);
                        else
                        {
                            valor = 0;
                            break;
                        }
                    }

                }
                catch (Exception e)
                {

                }


            }
            return valor;
        }


        static decimal GetKgMaterial(String codigo, TreeGridNode node, string Categoria_MP, out decimal cantidadTotal, out bool pesoCero)
        {
            decimal kg = 0;
            pesoCero = false;
            cantidadTotal = 0;

            string codigo_Padre = "";
            if (node.Parent.Cells[0].Value != null)
                codigo_Padre = node.Parent.Cells[0].Value.ToString();
            else
                codigo_Padre = codigo;

            string s = "Codigo_Hijo = '" + codigo + "' and Codigo_Padre = '" + codigo_Padre + "'";

            DataRow dr = GetDataRow(s);
            if (dr == null)
            {
                pesoCero = true;
                return 0;
            }

            decimal cantidad = Convert.ToDecimal(dr["Cantidad_Hijo"]);            

            if ((dr["Categoria_MP"].ToString() == Categoria_MP))
            {                                        
                //Nos aseguramos que tenga peso.
                if (!String.IsNullOrEmpty(dr["PesoUnitario"].ToString()))                
                    kg = Convert.ToDecimal(dr["PesoUnitario"]) * cantidad;                
                else
                    kg = 0;

                //if (codigo == "M16x40(DIN934)") Para simular peso 0 en un tornillo
                //    kg = 0;

                if (kg == 0)
                    pesoCero = true;
                else
                    pesoCero = false;

                cantidadTotal = cantidad;
            }
               
            if (node.HasChildren)
                foreach (TreeGridNode nodeAux in node.Nodes)
                {
                    decimal cantidadAux = 0;
                    bool pesoCeroAux = false;
                    kg += GetKgMaterial(nodeAux.Cells[0].Value.ToString(), nodeAux, Categoria_MP, out cantidadAux, out pesoCeroAux) * cantidad;
                    cantidadTotal += cantidadAux * cantidad;
                    pesoCero = pesoCero || pesoCeroAux;
                }
            
            return kg;
        }

        private static DataRow GetDataRow(string s)
        {
            DataRow dr = null;
            if (dT.Select(s).Count() > 0)
            {
                return dT.Select(s).First();
            }
            else
            {
                //Buscamos en la lista de dT.
                foreach (DataTable dtAux in listdtAux)
                {
                    if (dtAux.Select(s).Count() > 0)
                    {
                        return dtAux.Select(s).First();
                    }
                }
            }
            return dr;
        }

        #region Planos
        private void montajePToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //limpiarNavegador();

            String codigo = "";
            foreach (KeyValuePair<Int32, String> pair in this.DiccionarioCodigosSeleccionados)
                codigo = pair.Value;
            

            WebService_Planos.ServiceSoapClient servicio = new WebService_Planos.ServiceSoapClient();
            String Resultado = servicio.ObtenerFile(codigo, this.Codemp, this.Usuario);
            if (Resultado.StartsWith("-1"))
                MessageBox.Show("Error al obtener el plano de montaje del código: " + Environment.NewLine + Resultado);
            else
            {
                System.Threading.Thread.Sleep(1000);
                //webBrowserPlanos.Navigate(this.Path + "\\" + this.Usuario + "\\" + Resultado);
                System.Diagnostics.Process.Start(this.Path + "\\" + this.Usuario + "\\" + Resultado);
            }

            servicio.Close();
        }
        private void piezaPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //limpiarNavegador();

            String codigo = "";
            foreach (KeyValuePair<Int32, String> pair in this.DiccionarioCodigosSeleccionados)
                codigo = pair.Value;


            WebService_Planos.ServiceSoapClient servicio = new WebService_Planos.ServiceSoapClient();
            String Resultado = servicio.ObtenerFile2(codigo, this.Codemp, this.Usuario);
            if (Resultado.StartsWith("-1"))
                MessageBox.Show("Error al obtener el plano del código: " + Environment.NewLine + Resultado);
            else
            {
                System.Threading.Thread.Sleep(1000);
                //webBrowserPlanos.Navigate(this.Path + "\\" + this.Usuario + "\\" + Resultado);
                System.Diagnostics.Process.Start(this.Path + "\\" + this.Usuario + "\\" + Resultado);
            }

            servicio.Close();
        }
       

        //private void limpiarNavegador()
        //{
        //    webBrowserPlanos.Navigate("about:blank");
        //}

        private void btnExpandir_Click(object sender, EventArgs e)
        {
            this.splitContainer3.SplitterDistance = 800;
            
        }

        private void btnContraer_Click(object sender, EventArgs e)
        {
            this.splitContainer3.SplitterDistance = 1121;
        }
        #endregion


        #region Codigos
        private void editarCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String codigo = "";
            foreach (KeyValuePair<Int32, String> pair in this.DiccionarioCodigosSeleccionados)
            {
                codigo = pair.Value;
                CrearCodigo cc = new CrearCodigo(1, this.Codemp, codigo, this.Usuario);
                cc.ShowDialog();
            }
        }

        private void editarCDenominacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String codigo = "";
            foreach (KeyValuePair<Int32, String> pair in this.DiccionarioCodigosSeleccionados)
            {
                codigo = pair.Value;
                CrearCodigo cc = new CrearCodigo(2, this.Codemp, codigo, this.Usuario);
                cc.ShowDialog();
            }
        }

        private void crearCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("se hace al buscar un código que no exista");
        }

        private void borrarCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (KeyValuePair<Int32, String> pair in this.DiccionarioCodigosSeleccionados)
                    {
                        String codigo = pair.Value;

                        CD.borrarRutaArticulo(this.Codemp, codigo);
                        CD.borrarEstructuraArticulo(this.Codemp, codigo);
                        CD.borrarCaracteristicasArticulo(this.Codemp, codigo);
                        CD.borrarArticulo(this.Codemp, codigo);
                    }

                    //The Complete method commits the transaction. If an exception has been thrown, complete is not called and the transaction is rolled back.
                    scope.Complete();

                    MessageBox.Show("Códigos eliminados correctamente", "Borrar", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al eliminar los códigos de la estrucutura: " + ex.Message, "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        #region Estructuras
        private void editarEToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
        }

            #region métodos auxiliares relacionados con la edicion
        private void treeGridViewListaAlmacen_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (this.ListaValoresOriginales.Count == 0)
            {
                for (int x = 0; x < treeGridViewListaAlmacen.CurrentNode.Cells.Count; x++ )
                    this.ListaValoresOriginales.Add(treeGridViewListaAlmacen.CurrentNode.Cells[x].Value.ToString());
            }
        }

        private void treeGridViewListaAlmacen_RowLeave(object sender, DataGridViewCellEventArgs e)
        {                        
            //Si hay cambios pendientes (xq no se haya pulsado el boton de guardar => deshacer cambios)
            if (treeGridViewListaAlmacen.IsCurrentRowDirty)
            {
                treeGridViewListaAlmacen.CancelEdit();

                if (this.ListaValoresOriginales.Count > 0)
                {
                    for (int x = 0; x < treeGridViewListaAlmacen.CurrentNode.Cells.Count; x++)
                        treeGridViewListaAlmacen.CurrentNode.Cells[x].Value = this.ListaValoresOriginales[x];             
                }
            }

            this.ListaValoresOriginales.Clear();
        }
            #endregion



        private void añadirEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportarCodigo aniadirCodigo = new ImportarCodigo((DataSet)dsCodigos);
            if (aniadirCodigo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    String codigoAniadir = aniadirCodigo.getResultado();
                    String estructura = "";
                    foreach (KeyValuePair<Int32, String> pair in this.DiccionarioCodigosSeleccionados)
                        estructura = pair.Value;

                    if (codigoAniadir != "" && estructura != "")
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            CD.copiarEstructura(this.Codemp, estructura, codigoAniadir, this.Usuario);

                            //The Complete method commits the transaction. If an exception has been thrown, complete is not called and the transaction is rolled back.
                            scope.Complete();

                            MessageBox.Show("Código añadido correctamente", "Añadir", MessageBoxButtons.OK, MessageBoxIcon.None);
                        }
                    }
                    else
                        MessageBox.Show("Debe elegir una Estructura Origen y un Código a añadir válido");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al añadir el Código a la Estrucutura: " + ex.Message, "Copiar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void quitarEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {                
                using (TransactionScope scope = new TransactionScope())
                {                    
                    foreach (KeyValuePair<Int32, String> pair in this.DiccionarioCodigosSeleccionados)
                    {
                        String estructura = dT.Rows[pair.Key][2].ToString();

                        CD.quitarDeEstructura(this.Codemp, estructura, pair.Value);
                    }

                    //The Complete method commits the transaction. If an exception has been thrown, complete is not called and the transaction is rolled back.
                    scope.Complete();

                    MessageBox.Show("Códigos eliminados correctamente", "Borrar", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al eliminar los códigos de la estrucutura: " + ex.Message, "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void copiarEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportarCodigo copiarEstructura = new ImportarCodigo((DataSet)dsEstructuras);
            if (copiarEstructura.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    String codigoOrigen = copiarEstructura.getResultado();
                    String codigoDestino = "";
                    foreach (KeyValuePair<Int32, String> pair in this.DiccionarioCodigosSeleccionados)
                        codigoDestino = pair.Value;

                    if (codigoOrigen != "" && codigoDestino != "")
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            CD.copiarEstructura(this.Codemp, codigoOrigen, codigoDestino, this.Usuario);

                            //The Complete method commits the transaction. If an exception has been thrown, complete is not called and the transaction is rolled back.
                            scope.Complete();

                            MessageBox.Show("Estructura copiada correctamente", "Copiar", MessageBoxButtons.OK, MessageBoxIcon.None);
                        }
                    }
                    else
                        MessageBox.Show("Debe elegir un Código Origen y un Código Destino válido para poder realizar la copia de estructura");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al copiar la estrucutura: " + ex.Message, "Copiar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }        
        #endregion

        #region Tomás

        private void tabControl_filtros_Enter(object sender, EventArgs e)
        {
            tabControl_filtros.BringToFront();
        }



        private void tabControl_filtros_Leave(object sender, EventArgs e)
        {
            tabControl_filtros.SendToBack();
        }



        private void tabPage_filtros_generales_Enter(object sender, EventArgs e)
        {
            //JRM - Cambiar Empresa Se cambia 3 por empresaGlobal.empresaID
            this.tC_BUSCADOR_FILTROSTableAdapter.Fill(this.dsFiltros.TC_BUSCADOR_FILTROS, empresaGlobal.empresaID, "generales"); //empresaGlobal.empresaID
        }



        private void button_añadir_fila_filtros_generales_Click(object sender, EventArgs e)
        {
            if (tabPage_filtros_generales.Focus())
                dataGridView_filtros_generales.Rows.Add();
            else
                dataGridView_filtros_caracteristicas.Rows.Add();            
        }



        private void button_quitar_fila_filtros_generales_Click(object sender, EventArgs e)
        {
            int i;

            if (tabPage_filtros_generales.Focus())
            {
                for (i = dataGridView_filtros_generales.Rows.Count - 1; i >= 0; i--)
                {
                    if (dataGridView_filtros_generales.Rows[i].Selected)
                        dataGridView_filtros_generales.Rows.RemoveAt(i);
                }
            }
            else {
                for (i = dataGridView_filtros_caracteristicas.Rows.Count - 1; i >= 0; i--)
                {
                    if (dataGridView_filtros_caracteristicas.Rows[i].Selected)
                        dataGridView_filtros_caracteristicas.Rows.RemoveAt(i);
                }
            }
        }



        private bool Crear_Condicion_Filtros_Generales(string operador, out string condicion)
        {        
            string[] separar;
            string filtro;
            string valor;
            string tipo;
            int i;

            condicion = "";
            tipo = "";
            filtro = "";
            valor = "";
                                                   
            foreach (DataGridViewRow fila in dataGridView_filtros_generales.Rows)
            {                
                if ((fila.Cells[0].Value == null) || (fila.Cells[1].Value == null))
                    return false;

                filtro = fila.Cells[0].Value.ToString();
                if (filtro == "") 
                    return false;

                if (filtro == "Denominación")
                    valor = fila.Cells[1].Value.ToString();
                else
                    valor = fila.Cells[1].Value.ToString().Replace(" ", "");

                if (valor == "")
                    return false;

                if (filtro == "Código" || filtro == "Denominación" || filtro == "Familia" || filtro == "Categoría")
                {
                    if (condicion != "")
                        condicion = condicion + " " + operador;
                    
                    switch (filtro)
                    {
                        case "Código": tipo = "T_ARTICULOS.CODIGO LIKE"; break;
                        case "Denominación": tipo = "T_ARTICULOS.DENOMINACION LIKE"; break;
                        case "Familia": tipo = "T_ARTICULOS.FAMILIA ="; break;
                        case "Categoría": tipo = "T_ARTICULOS.CATEGORIA ="; break;
                    }
                    
                    if (valor.IndexOf('#') == -1)
                        condicion = condicion + " (" + tipo + " '" + valor.Replace('*', '%') + "')";
                    else
                    {
                        separar = valor.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
                        i = 0;
                        foreach (string s in separar)
                        {
                            if (i == 0)
                                condicion = condicion + " (" + tipo + " '" + s.Replace('*', '%') + "'";
                            else
                                condicion = condicion + " OR " + tipo + " '" + s.Replace('*', '%') + "'";
                            i++;
                        }
                        condicion = condicion + ")";
                    }                    
                }
            }
            if (condicion != "")
                condicion = " (" + condicion + ") ";
            return true;
        }


       
        private void Crear_Condicion_Comercial(string operador, out string condicion)
        {
            string condicion_aux = "";
            string valor;
            string[] separar;
            int i;

            condicion = "";
            foreach (DataGridViewRow fila in dataGridView_filtros_generales.Rows)
            {
                if (fila.Cells[0].Value.ToString() == "Comercial")
                {
                    if (condicion != "")
                        condicion = condicion + " " + operador;

                    condicion = condicion + " T_ARTICULOS.CODIGO IN (  select	t_ordterl.codigo";
                    condicion = condicion + "                           from	t_ordter inner join t_ordterl on";
                    condicion = condicion + "                                       t_ordter.numped = t_ordterl.numped and";
                    condicion = condicion + "                                       t_ordter.tiporeg = t_ordterl.tiporeg and";
                    condicion = condicion + "                                       t_ordter.codemp = t_ordterl.codemp";
                    condicion = condicion + "                           where	t_ordter.codemp = '" + Codemp + "' and t_ordter.tiporeg = 'cf' and t_ordter.repres in ";

                    valor = fila.Cells[1].Value.ToString();
                    if (valor.IndexOf('#') == -1)
                    {
                        condicion = condicion + "('" + valor + "')";
                        if (condicion_aux == "")
                            condicion_aux = "'" + valor + "'";
                        else
                            condicion_aux = condicion_aux + ",'" + valor + "'";
                    }
                    else
                    {
                        separar = valor.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
                        i = 0;
                        foreach (string s in separar)
                        {
                            if (i == 0)
                                condicion = condicion + "('" + s + "'";
                            else
                                condicion = condicion + ",'" + s + "'";
                            i++;

                            if (condicion_aux == "")
                                condicion_aux = "'" + s + "'";
                            else
                                condicion_aux = condicion_aux + ",'" + s + "'";
                        }
                        condicion = condicion + ")";
                    }

                    condicion = condicion + "                           group   by t_ordterl.codigo";
                    condicion = condicion + "                        )";
                }
            }

            if (condicion != "")
            {
                if (checkBox_detallado.Checked)
                    condicion = "( (" + condicion + ") and t_ordter.repres in (" + condicion_aux + ") )";
                else
                    condicion = " (" + condicion + ") ";
            }
        }



        private void Crear_Condicion_Lugar(string operador, out string condicion)
        {
            string condicion_aux = "";
            string valor;
            string[] separar;
            int i;

            condicion = "";
            foreach (DataGridViewRow fila in dataGridView_filtros_generales.Rows)
            {
                if (fila.Cells[0].Value.ToString() == "Lugar")
                {
                    if (condicion != "")
                        condicion = condicion + " " + operador;

                    condicion = condicion + " T_ARTICULOS.CODIGO IN (  select	t_ordterl.codigo";
                    condicion = condicion + "                           from	t_ordter inner join t_ordterl on";
                    condicion = condicion + "                                       t_ordter.numped = t_ordterl.numped and";
                    condicion = condicion + "                                       t_ordter.tiporeg = t_ordterl.tiporeg and";
                    condicion = condicion + "                                       t_ordter.codemp = t_ordterl.codemp";
                    condicion = condicion + "                           where	t_ordter.codemp = '" + Codemp + "' and t_ordter.tiporeg = 'cf' and t_ordter.codexp in "; 
                    
                    valor = fila.Cells[1].Value.ToString();
                    if (valor.IndexOf('#') == -1)
                    {
                        condicion = condicion + "('" + valor + "')";
                        if (condicion_aux == "")
                            condicion_aux = "'" + valor + "'";
                        else
                            condicion_aux = condicion_aux + ",'" + valor + "'";
                    }
                    else
                    {
                        separar = valor.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
                        i = 0;
                        foreach (string s in separar)
                        {
                            if (i == 0)
                                condicion = condicion + "('" + s + "'";
                            else
                                condicion = condicion + ",'" + s + "'";
                            i++;

                            if (condicion_aux == "")
                                condicion_aux = "'" + s + "'";
                            else
                                condicion_aux = condicion_aux + ",'" + s + "'";
                        }
                        condicion = condicion + ")";
                    } 
                                                                                                                        
                    condicion = condicion + "                           group   by t_ordterl.codigo";
                    condicion = condicion + "                        )";                   
                }
            }

            if (condicion != "")
            {
                if (checkBox_detallado.Checked)
                    condicion = "( (" + condicion + ") and t_ordter.codexp in (" + condicion_aux + ") )";
                else
                    condicion = " (" + condicion + ") ";
            }
        }



        private void Crear_Condicion_Pedido(string operador, out string condicion)
        {
            string condicion_aux = "";
            string valor;
            string[] separar;
            int i;

            condicion = "";
            foreach (DataGridViewRow fila in dataGridView_filtros_generales.Rows)
            {
                if (fila.Cells[0].Value.ToString() == "Pedido")
                {
                    if (condicion != "")
                        condicion = condicion + " " + operador;

                    condicion = condicion + " T_ARTICULOS.CODIGO IN (  select	t_ordterl.codigo";
                    condicion = condicion + "                           from	t_ordter inner join t_ordterl on";
                    condicion = condicion + "                                       t_ordter.numped = t_ordterl.numped and";
                    condicion = condicion + "                                       t_ordter.tiporeg = t_ordterl.tiporeg and";
                    condicion = condicion + "                                       t_ordter.codemp = t_ordterl.codemp";
                    condicion = condicion + "                           where	t_ordter.codemp = '" + Codemp + "' and t_ordter.tiporeg = 'cf' and t_ordter.numped in ";

                    valor = fila.Cells[1].Value.ToString();
                    if (valor.IndexOf('#') == -1)
                    {
                        condicion = condicion + "('" + valor + "')";
                        if (condicion_aux == "")
                            condicion_aux = "'" + valor + "'";
                        else
                            condicion_aux = condicion_aux + ",'" + valor + "'";
                    }
                    else
                    {
                        separar = valor.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
                        i = 0;
                        foreach (string s in separar)
                        {
                            if (i == 0)
                                condicion = condicion + "('" + s + "'";
                            else
                                condicion = condicion + ",'" + s + "'";
                            i++;

                            if (condicion_aux == "")
                                condicion_aux = "'" + s + "'";
                            else
                                condicion_aux = condicion_aux + ",'" + s + "'";
                        }
                        condicion = condicion + ")";
                    }

                    condicion = condicion + "                           group   by t_ordterl.codigo";
                    condicion = condicion + "                        )";
                }
            }

            if (condicion != "")
            {
                if (checkBox_detallado.Checked)
                    condicion = "( (" + condicion + ") and t_ordter.numped in (" + condicion_aux + ") )";
                else
                    condicion = " (" + condicion + ") ";
            }
        }



        private void Crear_Condicion_Denominacion_Pedido(string operador, out string condicion)
        {
            string condicion_aux = "";
            string valor;
            string[] separar;
            int i;

            condicion = "";
            foreach (DataGridViewRow fila in dataGridView_filtros_generales.Rows)
            {
                if (fila.Cells[0].Value.ToString() == "Denominación Pedido")
                {
                    if (condicion != "")
                        condicion = condicion + " " + operador;

                    condicion = condicion + " T_ARTICULOS.CODIGO IN (  select	t_ordterl.codigo";
                    condicion = condicion + "                           from	t_ordter inner join t_ordterl on";
                    condicion = condicion + "                                       t_ordter.numped = t_ordterl.numped and";
                    condicion = condicion + "                                       t_ordter.tiporeg = t_ordterl.tiporeg and";
                    condicion = condicion + "                                       t_ordter.codemp = t_ordterl.codemp";
                    condicion = condicion + "                           where	t_ordter.codemp = '" + Codemp + "' and t_ordter.tiporeg = 'cf' and";

                    valor = fila.Cells[1].Value.ToString();
                    if (valor.IndexOf('#') == -1)
                    {
                        condicion = condicion + " t_ordterl.denominacion like '" + valor.Replace('*', '%') + "'";
                        if (condicion_aux == "")
                            condicion_aux = " t_ordterl.denominacion like '" + valor.Replace('*', '%') + "'";
                        else
                            condicion_aux = condicion_aux + " or t_ordterl.denominacion like '" + valor.Replace('*', '%') + "'";
                    }
                    else
                    {
                        separar = valor.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
                        i = 0;
                        foreach (string s in separar)
                        {
                            if (i == 0)
                                condicion = condicion + " (t_ordterl.denominacion like '" + s.Replace('*', '%') + "'";
                            else
                                condicion = condicion + " or t_ordterl.denominacion like '" + s.Replace('*', '%') + "'";
                            i++;

                            if (condicion_aux == "")
                                condicion_aux = " t_ordterl.denominacion like '" + s.Replace('*', '%') + "'";
                            else
                                condicion_aux = condicion_aux + " or t_ordterl.denominacion like '" + s.Replace('*', '%') + "'";
                        }
                        condicion = condicion + ")";
                    }

                    condicion = condicion + "                           group   by t_ordterl.codigo";
                    condicion = condicion + "                        )";
                }
            }

            if (condicion != "")
            {
                if (checkBox_detallado.Checked)
                {
                    condicion = "( (" + condicion + ") and t_ordter.numped in ( select	t_ordter.numped";
                    condicion = condicion + "                                   from	t_ordter inner join t_ordterl on";
                    condicion = condicion + "                                               t_ordter.numped = t_ordterl.numped and";
                    condicion = condicion + "                                               t_ordter.tiporeg = t_ordterl.tiporeg and";
                    condicion = condicion + "                                               t_ordter.codemp = t_ordterl.codemp";
                    condicion = condicion + "                                   where	t_ordter.codemp = '" + Codemp + "' and t_ordter.tiporeg = 'cf' and (" + condicion_aux + ")";
                    condicion = condicion + "                                   group by t_ordter.numped";
                    condicion = condicion + "                                   )";
                    condicion = condicion + ")";
                }
                else
                    condicion = " (" + condicion + ") ";
            }
        }



        private void Crear_Condicion_Cliente(string operador, out string condicion)
        {
            string condicion_aux = "";
            string valor;
            string[] separar;
            int i;

            condicion = "";
            foreach (DataGridViewRow fila in dataGridView_filtros_generales.Rows)
            {
                if (fila.Cells[0].Value.ToString() == "Cliente")
                {
                    if (condicion != "")
                        condicion = condicion + " " + operador;

                    condicion = condicion + " T_ARTICULOS.CODIGO IN (  select	t_ordterl.codigo";
                    condicion = condicion + "                           from	t_ordter inner join t_ordterl on";
                    condicion = condicion + "                                       t_ordter.numped = t_ordterl.numped and";
                    condicion = condicion + "                                       t_ordter.tiporeg = t_ordterl.tiporeg and";
                    condicion = condicion + "                                       t_ordter.codemp = t_ordterl.codemp";
                    condicion = condicion + "                           where	t_ordter.codemp = '" + Codemp + "' and t_ordter.tiporeg = 'cf' and t_ordter.tercero in ";

                    valor = fila.Cells[1].Value.ToString();
                    if (valor.IndexOf('#') == -1)
                    {
                        condicion = condicion + "('" + valor + "')";
                        if (condicion_aux == "")
                            condicion_aux = "'" + valor + "'";
                        else
                            condicion_aux = condicion_aux + ",'" + valor + "'";
                    }
                    else
                    {
                        separar = valor.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
                        i = 0;
                        foreach (string s in separar)
                        {
                            if (i == 0)
                                condicion = condicion + "('" + s + "'";
                            else
                                condicion = condicion + ",'" + s + "'";
                            i++;

                            if (condicion_aux == "")
                                condicion_aux = "'" + s + "'";
                            else
                                condicion_aux = condicion_aux + ",'" + s + "'";
                        }
                        condicion = condicion + ")";
                    }

                    condicion = condicion + "                           group   by t_ordterl.codigo";
                    condicion = condicion + "                        )";
                }
            }

            if (condicion != "")
            {
                if (checkBox_detallado.Checked)
                    condicion = "( (" + condicion + ") and t_ordter.tercero in (" + condicion_aux + ") )";
                else
                    condicion = " (" + condicion + ") ";
            }
        }


        private void Crear_Eliminar_Columnas_Detallado() {
            
            if (checkBox_detallado.Checked)
            {
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();

                if (!dGViewResultados.Columns.Contains("Pedido"))
                {
                    col.Name = "Pedido";
                    col.DataPropertyName = "Pedido";
                    col.Width = 50;
                    dGViewResultados.Columns.Add(col);
                }

                if (!dGViewResultados.Columns.Contains("Línea"))
                {
                    col = new DataGridViewTextBoxColumn();
                    col.Name = "Línea";
                    col.DataPropertyName = "Línea";
                    col.Width = 40;
                    dGViewResultados.Columns.Add(col);
                }

                if (!dGViewResultados.Columns.Contains("Denominación_Pedido"))
                {
                    col = new DataGridViewTextBoxColumn();
                    col.Name = "Denominación_Pedido";
                    col.DataPropertyName = "Denominación_Pedido";
                    col.Width = 300;
                    dGViewResultados.Columns.Add(col);
                }

                if (!dGViewResultados.Columns.Contains("Lugar"))
                {
                    col = new DataGridViewTextBoxColumn();
                    col.Name = "Lugar";
                    col.DataPropertyName = "Lugar";
                    col.Width = 130;
                    dGViewResultados.Columns.Add(col);
                }

                if (!dGViewResultados.Columns.Contains("Comercial"))
                {
                    col = new DataGridViewTextBoxColumn();
                    col.Name = "Comercial";
                    col.DataPropertyName = "Comercial";
                    col.Width = 150;
                    dGViewResultados.Columns.Add(col);
                }

                if (!dGViewResultados.Columns.Contains("Cliente"))
                {
                    col = new DataGridViewTextBoxColumn();
                    col.Name = "Cliente";
                    col.DataPropertyName = "Cliente";
                    col.Width = 300;
                    dGViewResultados.Columns.Add(col);
                }
            }
            else 
            {
                if (dGViewResultados.Columns.Contains("Pedido"))
                    dGViewResultados.Columns.Remove("Pedido");

                if (dGViewResultados.Columns.Contains("Línea"))
                    dGViewResultados.Columns.Remove("Línea");

                if (dGViewResultados.Columns.Contains("Denominación_Pedido"))
                    dGViewResultados.Columns.Remove("Denominación_Pedido");

                if (dGViewResultados.Columns.Contains("Lugar"))
                    dGViewResultados.Columns.Remove("Lugar");

                if (dGViewResultados.Columns.Contains("Comercial"))
                    dGViewResultados.Columns.Remove("Comercial");

                if (dGViewResultados.Columns.Contains("Cliente"))
                    dGViewResultados.Columns.Remove("Cliente");
            }
        }



        private bool Crear_Condicion_Caracteristicas_Ingenieria(string operador, out string condicion)
        {
            string caracteristica;
            string signo;
            string valor;
            string[] separar;
            bool esnumerica;
            int i;
            string categoria;

            condicion = "";           
            foreach (DataGridViewRow fila in dataGridView_filtros_caracteristicas.Rows)
            {
                if ((fila.Cells["categoria2"].Value == null) || (fila.Cells["caracteristica"].Value == null) || (fila.Cells["signo"].Value == null) || (fila.Cells["valor2"].Value == null))
                    return false;

                categoria = fila.Cells["categoria2"].Value.ToString();
                caracteristica = fila.Cells["caracteristica"].Value.ToString();
                signo = fila.Cells["signo"].Value.ToString();
                valor = fila.Cells["valor2"].Value.ToString();

                if (caracteristica == "" || signo == "" || valor == "")
                    return false;
                
                esnumerica = CD.Es_Numerica_Caracteristica(Codemp,caracteristica);

                if (condicion != "")
                    condicion = condicion + " " + operador;

                condicion = condicion + " ( T_ARTICULOS.CODIGO IN ( select  codigo";
                condicion = condicion + "                           from	t_artcar";
                condicion = condicion + "                           where	codemp = '" + Codemp + "' and";

                // VSS: ahora si nos llega la categoría en blanco no filtramos por categoria en la tabla t_caract
                if (categoria != "")
                {
                    condicion = condicion + "                               	catego = '" + categoria + "' and";
                }

                condicion = condicion + "                                   caract = '" + caracteristica + "' and";     
                if (valor.IndexOf('#') == -1)
                {
                    if(!esnumerica)
                        condicion = condicion + "                           valor like '" + valor.Replace('*', '%') + "' ))";     
                    else
                        condicion = condicion + "                           isnumeric(valor) = 1 and cast(replace(valor,',','.') as float) " + signo + " cast(replace('" + valor + "',',','.') as float) ))";                              
                }
                else
                {
                    separar = valor.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
                    i=0;
                    foreach (string s in separar)
                    {
                        if(i==0)
                            condicion = condicion + " ( ";
                        else
                            condicion = condicion + " or "; 
                                                                                    
                        condicion = condicion + "                   valor like '" + s.Replace('*', '%') + "'";                             
                        
                        i++;
                    }
                    condicion = condicion + " ) ))";
                }                
            }                                                
            return true;
        }



        private void Auditar_Busqueda() 
        {
            string strsql;
            SqlCommand comando;
            SqlConnection conexion = null;

            try
            {                
                conexion = new SqlConnection(cadenaConexion);
                conexion.Open();

                strsql = "EXECUTE dbo.IME_Reg_Uso_Mod 'ImeApps','Buscador','IMEDEXSA','" + this.Usuario + "'";

                comando = new SqlCommand(strsql, conexion);
                comando.ExecuteScalar();

                conexion.Close();
                
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();                ;
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void button_buscar_filtros_generales_Click(object sender, EventArgs e)
        {
            string strsql;
            SqlConnection conexion;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;
            string condicion,condicion_caracteristicas;
            string operador;
            bool haycondicion = false;

            conexion = new SqlConnection(ConfigurationManager.AppSettings["ggConnectionString"].ToString()); 
            try
            {
                //AUDITAR BÚSQUEDA
                Auditar_Busqueda();
                
                if (dataGridView_filtros_generales.Rows.Count == 0 && dataGridView_filtros_caracteristicas.Rows.Count == 0)
                {
                    MessageBox.Show("NO HA INDICADO FILTROS", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                
                if (radioButton_Y_filtros_generales.Checked)
                    operador = "AND";
                else
                    operador = "OR";
                
                if (!Crear_Condicion_Filtros_Generales(operador,out condicion)) {
                    MessageBox.Show("FALTAN DATOS EN LOS FILTROS INDICADOS EN LAS PROPIEDADES GENERALES", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (!Crear_Condicion_Caracteristicas_Ingenieria(operador, out condicion_caracteristicas))
                {
                    MessageBox.Show("FALTAN DATOS EN LOS FILTROS INDICADOS EN LAS CARACTERÍSTICAS DE INGENIERÍA", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;

                // Ahora solo limpiamos el grid de componentes si se tiene marcada la opción de limpiar listado de componentes
                if (checkBox_LimpiarComponentes.Checked)
                {
                    limpiarArbol();
                }

                //limpiarArbol();
                //limpiarNavegador();

                conexion.Open();

                strsql = "";
                strsql = strsql + " SELECT T_ARTICULOS.CODIGO, ";


                //JRegino 11/07/2016            //strsql = strsql + "         T_ARTICULOS.Denominacion, ";
                if (cmbIdioma.Text == "es")
                    strsql = strsql + "         T_ARTICULOS.Denominacion, ";
                else
                    strsql = strsql + "         isnull((Select Denominacion From TC_ARTICULOS_IDIOMAS Where Codemp = T_ARTICULOS.Codemp and Codigo = T_ARTICULOS.Codigo and idioma = '" + cmbIdioma.Text + "' ), '*** ' + T_ARTICULOS.DENOMINACION) as DENOMINACION, ";


                strsql = strsql + "             T_CATEGORIAS.DENOMI AS Categoria, T_FAMILIAS.DENOMI AS Familia, T_ARTICULOS.FAMILIA AS idFamilia, T_ARTICULOS.CLAOBS";
                if (checkBox_detallado.Checked)
                {
                    strsql = strsql + " ,t_ordter.numped as Pedido, t_ordterl.linea as Línea, t_ordterl.denominacion as Denominación_Pedido, t_ordter.dcodexp as Lugar, t_ordter.drepres as Comercial, t_ordter.rtercero as Cliente";
                }
                strsql = strsql + " FROM    T_ARTICULOS LEFT JOIN T_FAMILIAS ON"; 
                strsql = strsql + "             T_ARTICULOS.FAMILIA = T_FAMILIAS.FAMILIA AND";
                strsql = strsql + "             T_ARTICULOS.CodEMP = T_FAMILIAS.CodEMP"; 
                strsql = strsql + "             LEFT JOIN T_CATEGORIAS ON";
                strsql = strsql + "                 T_ARTICULOS.CATEGORIA = T_CATEGORIAS.CATEGO AND";
                strsql = strsql + "                 T_ARTICULOS.CodEMP = T_CATEGORIAS.CODEMP";
                if (checkBox_detallado.Checked)
                { 
                    strsql = strsql + "             LEFT JOIN T_ORDTERL ON";
                    strsql = strsql + "                 T_ARTICULOS.CODIGO = T_ORDTERL.CODIGO AND";
                    strsql = strsql + "                 T_ARTICULOS.CODEMP = T_ORDTERL.CODEMP AND";
                    strsql = strsql + "                 'CF' = T_ORDTERL.TIPOREG";
                    strsql = strsql + "                 LEFT JOIN T_ORDTER ON";
                    strsql = strsql + "                     T_ORDTERL.NUMPED = T_ORDTER.NUMPED AND";
                    strsql = strsql + "                     T_ORDTERL.TIPOREG = T_ORDTER.TIPOREG AND";
                    strsql = strsql + "                     T_ORDTERL.CODEMP = T_ORDTER.CODEMP";
                }
                strsql = strsql + " WHERE   T_ARTICULOS.CodEMP = '"+ Codemp +"' AND ";
                
                //CONDICIONES TABLA ARTÍCULO
                if (condicion != "")
                {
                    strsql = strsql + condicion;
                    haycondicion = true;
                }

                //COMERCIAL
                Crear_Condicion_Comercial(operador, out condicion);
                if(condicion != "")
                {
                    if (haycondicion)
                        strsql = strsql + operador + condicion;
                    else
                    {
                        strsql = strsql + condicion;
                        haycondicion = true;
                    }
                }

                //LUGAR
                Crear_Condicion_Lugar(operador, out condicion);
                if (condicion != "")
                {
                    if (haycondicion)
                        strsql = strsql + operador + condicion;
                    else
                    {
                        strsql = strsql + condicion;
                        haycondicion = true;
                    }
                }

                //PEDIDO
                Crear_Condicion_Pedido(operador, out condicion);
                if (condicion != "")
                {
                    if (haycondicion)
                        strsql = strsql + operador + condicion;
                    else
                    {
                        strsql = strsql + condicion;
                        haycondicion = true;
                    }
                }

                //DENOMINACIÓN PEDIDO
                Crear_Condicion_Denominacion_Pedido(operador, out condicion);
                if (condicion != "")
                {
                    if (haycondicion)
                        strsql = strsql + operador + condicion;
                    else
                    {
                        strsql = strsql + condicion;
                        haycondicion = true;
                    }
                }

                //CLIENTE
                Crear_Condicion_Cliente(operador, out condicion);
                if (condicion != "")
                {
                    if (haycondicion)
                        strsql = strsql + operador + condicion;
                    else
                    {
                        strsql = strsql + condicion;
                        haycondicion = true;
                    }
                }

                //CARACTERÍSTICAS INGENIERÍA
                if (condicion_caracteristicas != "")
                {
                    if (haycondicion)
                        strsql = strsql + operador + condicion_caracteristicas;
                    else
                    {
                        strsql = strsql + condicion_caracteristicas;
                        haycondicion = true;
                    }
                }
                
                strsql = strsql + " ORDER BY T_ARTICULOS.CODIGO";
                if (checkBox_detallado.Checked)
                    strsql = strsql + ",t_ordter.numped, t_ordterl.linea";                                
                comando = new SqlCommand(strsql,conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                Crear_Eliminar_Columnas_Detallado();
                
                dGViewResultados.DataSource = table;

                conexion.Close();

                if (dGViewResultados.Rows.Count == 0)
                    Cargar_Caracteristicas_Codigo("*");                      

                Cursor.Current = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
                Cursor.Current = Cursors.Arrow;
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //JRM - InformeExcel - ImeApps
            //dGViewResultados.head
            //this.getAllcodes();
        }



        private void dataGridView_filtros_generales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string filtro;      
            string valor;

            try
            {
                if (e.ColumnIndex == 1 && dataGridView_filtros_generales.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value != null)
                {
                    filtro = dataGridView_filtros_generales.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString();
                    valor = "";
                    if (filtro == "Familia" || filtro == "Categoría" || filtro == "Comercial" || filtro == "Lugar" || filtro == "Cliente")
                    {
                        if (dataGridView_filtros_generales.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                            valor = dataGridView_filtros_generales.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                        Familia_Categoria f_c = new Familia_Categoria(filtro, valor);
                        f_c.ShowDialog();
                        if (f_c.aceptado)
                        {
                            dataGridView_filtros_generales.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = f_c.salida;
                            button_buscar_filtros_generales.Focus();
                        }
                    }
                }
            }
            catch (Exception) {
                
            }
        }



        private void button_ayuda_filtros_Click(object sender, EventArgs e)
        {
            string texto = "";

            texto = texto + "- Se pueden indicar varios criterios de búsqueda en un mismo valor de un filtro separándolos por #" + System.Environment.NewLine + System.Environment.NewLine;
            texto = texto + "- Se puede emplear el * como caracter comodín" + System.Environment.NewLine + System.Environment.NewLine;
            texto = texto + "- Y: Los resultados de la búsqueda deben cumplir todas las condiciones indicadas" + System.Environment.NewLine + System.Environment.NewLine;
            texto = texto + "- O: Los resultados de la búsqueda deben cumplir al menos una condición de las indicadas" + System.Environment.NewLine + System.Environment.NewLine;
            texto = texto + "- Detallado: Se mostrará información de los pedidos de venta asociados a los códigos resultado de la búsqueda" + System.Environment.NewLine + System.Environment.NewLine;
            texto = texto + "- Características: Se mostrará los valores de las características asociadas al código seleccionado (ya sea en los resultados de la búsqueda o en la lista de almacén desplegada)" + System.Environment.NewLine;

            MessageBox.Show(texto, "Ayuda", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void dataGridView_filtros_caracteristicas_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView_filtros_caracteristicas.Columns[dataGridView_filtros_caracteristicas.CurrentCell.ColumnIndex].Name == "categoria2")
            {
                if (dataGridView_filtros_caracteristicas.IsCurrentCellDirty)
                {
                    dataGridView_filtros_caracteristicas.CommitEdit(DataGridViewDataErrorContexts.Commit);

                    //VACIAR CARACTERÍSTICA                                                            
                    if (dataGridView_filtros_caracteristicas.CurrentRow.Cells["caracteristica"].Value != null && dataGridView_filtros_caracteristicas.CurrentRow.Cells["caracteristica"].Value.ToString() != "")                                        
                        dataGridView_filtros_caracteristicas.CurrentRow.Cells["caracteristica"].Value = "";                    
                    Cargar_Combo_Caracteristicas_Categoria(dataGridView_filtros_caracteristicas.CurrentRow.Cells["categoria2"].Value.ToString(), (DataGridViewComboBoxCell)(dataGridView_filtros_caracteristicas.CurrentRow.Cells["caracteristica"]));

                    //VACIAR VALOR
                    if (dataGridView_filtros_caracteristicas.CurrentRow.Cells["valor2"].Value != null && dataGridView_filtros_caracteristicas.CurrentRow.Cells["valor2"].Value.ToString() != "")
                        dataGridView_filtros_caracteristicas.CurrentRow.Cells["valor2"].Value = "";
                    DataGridViewComboBoxCell combo = (DataGridViewComboBoxCell)(dataGridView_filtros_caracteristicas.CurrentRow.Cells["valor2"]);
                    combo.DataSource = null;
                    combo.Items.Clear();                    

                    //VACIAR SIGNO
                    combo = (DataGridViewComboBoxCell)(dataGridView_filtros_caracteristicas.CurrentRow.Cells["signo"]);
                    combo.Items.Clear();
                    dataGridView_filtros_caracteristicas.CurrentRow.Cells["signo"].Value = "";
                }
            }
            else
            {
                if (dataGridView_filtros_caracteristicas.Columns[dataGridView_filtros_caracteristicas.CurrentCell.ColumnIndex].Name == "caracteristica")
                {
                    if (dataGridView_filtros_caracteristicas.IsCurrentCellDirty)
                    {
                        dataGridView_filtros_caracteristicas.CommitEdit(DataGridViewDataErrorContexts.Commit);

                        //VACIAR VALOR
                        if (dataGridView_filtros_caracteristicas.CurrentRow.Cells["valor2"].Value != null && dataGridView_filtros_caracteristicas.CurrentRow.Cells["valor2"].Value.ToString() != "")
                            dataGridView_filtros_caracteristicas.CurrentRow.Cells["valor2"].Value = "";
                        DataGridViewComboBoxCell combo = (DataGridViewComboBoxCell)(dataGridView_filtros_caracteristicas.CurrentRow.Cells["valor2"]);
                        combo.DataSource = null;
                        combo.Items.Clear();

                        //CARGAR POSIBLES VALORES
                        if (CD.Tipo_Valores_Caracteristica(Codemp, dataGridView_filtros_caracteristicas.CurrentRow.Cells["caracteristica"].Value.ToString()) == "T")
                            Cargar_Valores_Combos_Caracteristicas_Filtros(dataGridView_filtros_caracteristicas.CurrentRow.Cells["caracteristica"].Value.ToString(), (DataGridViewComboBoxCell)(dataGridView_filtros_caracteristicas.CurrentRow.Cells["valor2"]));

                        //PONER SIGNO
                        combo = (DataGridViewComboBoxCell)(dataGridView_filtros_caracteristicas.CurrentRow.Cells["signo"]);
                        combo.Items.Clear();                        
                        if (!CD.Es_Numerica_Caracteristica(Codemp, dataGridView_filtros_caracteristicas.CurrentRow.Cells["caracteristica"].Value.ToString())){                                                                                                            
                            combo.Items.Add("=");    
                            dataGridView_filtros_caracteristicas.CurrentRow.Cells["signo"].Value = "=";                        
                        }
                        else {
                            combo.Items.Add(">");
                            combo.Items.Add(">=");
                            combo.Items.Add("=");
                            combo.Items.Add("<=");
                            combo.Items.Add("<");
                            dataGridView_filtros_caracteristicas.CurrentRow.Cells["signo"].Value = "";                        
                        }
                    }
                }                
            }
        }

        private void dataGridView_filtros_caracteristicas_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is ComboBox)
            {                                
                if (dataGridView_filtros_caracteristicas.Columns[dataGridView_filtros_caracteristicas.CurrentCell.ColumnIndex].Name == "valor2")
                {
                    if (dataGridView_filtros_caracteristicas.CurrentRow.Cells["caracteristica"].Value != null && dataGridView_filtros_caracteristicas.CurrentRow.Cells["caracteristica"].Value.ToString() != "")
                    {
                        if (CD.Tipo_Valores_Caracteristica(Codemp, dataGridView_filtros_caracteristicas.CurrentRow.Cells["caracteristica"].Value.ToString()) == "F")
                        {
                            ComboBox combo = (ComboBox)e.Control;
                            combo.DropDownStyle = ComboBoxStyle.DropDown;
                        }
                    }
                }                    
            }
        }

        private void dataGridView_filtros_caracteristicas_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridView_filtros_caracteristicas.Columns[dataGridView_filtros_caracteristicas.CurrentCell.ColumnIndex].Name == "valor2")
            {
                if (dataGridView_filtros_caracteristicas.CurrentRow.Cells["caracteristica"].Value != null && dataGridView_filtros_caracteristicas.CurrentRow.Cells["caracteristica"].Value.ToString() != "")
                {
                    if (CD.Tipo_Valores_Caracteristica(Codemp, dataGridView_filtros_caracteristicas.CurrentRow.Cells["caracteristica"].Value.ToString()) == "F")
                    {
                        DataGridViewComboBoxCell combo_celda = (DataGridViewComboBoxCell)(dataGridView_filtros_caracteristicas.CurrentRow.Cells["valor2"]);
                        combo_celda.DataSource = null;
                        combo_celda.Items.Add(e.FormattedValue.ToString());

                        if (CD.Es_Numerica_Caracteristica(Codemp, dataGridView_filtros_caracteristicas.CurrentRow.Cells["caracteristica"].Value.ToString()))
                        {
                            double numero;
                            if (!double.TryParse(e.FormattedValue.ToString().Replace('.', '#'), out numero))
                            {
                                MessageBox.Show("El valor de esta característica debe ser númerico", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                combo_celda.Items.Remove(e.FormattedValue.ToString());
                                return;
                            }
                        }

                        ActualizarGridCaracteristicas = true;
                        if (dataGridView_filtros_caracteristicas[e.ColumnIndex, e.RowIndex].Value == null)
                            ValorActual = "";
                        else
                            ValorActual = dataGridView_filtros_caracteristicas[e.ColumnIndex, e.RowIndex].Value.ToString();
                        ValorNuevo = e.FormattedValue.ToString();
                    }
                }
            }
        }

        private void dataGridView_filtros_caracteristicas_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (ActualizarGridCaracteristicas)
            {
                if (dataGridView_filtros_caracteristicas.CurrentCell != null)
                {
                    dataGridView_filtros_caracteristicas.CurrentCell.Value = ValorNuevo;
                    DataGridViewComboBoxCell combo_celda = (DataGridViewComboBoxCell)(dataGridView_filtros_caracteristicas.CurrentRow.Cells["valor2"]);
                    combo_celda.Items.Remove(ValorActual);
                    ActualizarGridCaracteristicas = false;
                }
            }
        }
    
        private void Cargar_Combo_Caracteristicas_Categoria(string categoria,DataGridViewComboBoxCell combo)
        {
            string sql = "";
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;                        

            try
            {
                conexion = new SqlConnection(cadenaConexion);
                conexion.Open();
                               
                // VSS: ahora si nos llega la categoría en blanco, en lugar de mostrar las características de la categoria 
                // (tabla t_carcat), mostramos el catálogo de características (tabla t_caract)
                if (categoria == "")
                {
                    sql = " select  t_caract.caract, ";
                    sql = sql + "         t_caract.denomi ";
                    sql = sql + " from    t_caract ";
                    sql = sql + " where	t_caract.codemp = '" + Codemp + "' and ";
                    sql = sql + "         t_caract.caract >= '500' ";
                    sql = sql + " order by t_caract.denomi ";
                }
                else
                {                    
                    sql = sql + " select  t_caract.caract,";
                    sql = sql + "         t_caract.denomi";
                    sql = sql + " from    t_caract inner join t_carcat on";
                    sql = sql + "             t_caract.codemp = t_carcat.codemp and";
                    sql = sql + "             t_caract.caract = t_carcat.caract";
                    sql = sql + " where	t_caract.codemp = '" + Codemp + "' and";
                    sql = sql + "         t_caract.caract >= '500' and";
                    sql = sql + "         t_carcat.catego = '" + categoria + "'";
                    sql = sql + " order by t_caract.denomi";
                }

                comando = new SqlCommand(sql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                combo.DataSource = table;
                combo.DisplayMember = "denomi";
                combo.ValueMember = "caract";                
                
                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cargar_Valores_Combos_Caracteristicas_Filtros(string caracteristica,DataGridViewComboBoxCell combo)
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;
            
            try
            {
                conexion = new SqlConnection(cadenaConexion);
                conexion.Open();
                
                strsql = "";
                strsql = strsql + " SELECT  valor";
                strsql = strsql + " FROM    t_valcar";
                strsql = strsql + " WHERE   CODEMP = '" + Codemp + "' AND";
                strsql = strsql + "         caract = '" + caracteristica + "'";
                strsql = strsql + " order by valor";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                combo.DataSource = table;
                combo.DisplayMember = "valor";
                combo.ValueMember = "valor";

                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        

        public void Mantener_Distancias_Split()
        {
            
            //splitContainer4.SplitterDistance = 315;
            //splitContainer5.SplitterDistance = 490;            

            // VSS: ajustamos los tamaños de los espacios a las nuevas dimensiones
            try
            {
                splitContainer4.SplitterDistance = 320;
                splitContainer5.SplitterDistance = 473;
            }
            catch (Exception)
            {

            }
        }

        private void Intranet_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized || this.WindowState == FormWindowState.Normal)            
                Mantener_Distancias_Split();                        
        }

        private void Ver_Caracteristicas(bool ver)
        {
            // VSS: comentamos para evitar establecer elementos en posiciones fijas
            //if (ver)
            //    splitContainer3.SplitterDistance = 1480;
            //else
            //    splitContainer3.SplitterDistance = 10000;    

            // VSS: ahora jugamos con la propiedad MinSize de los paneles
            if (ver)
            {
                //splitContainer3.Panel2MinSize = 250;
                //splitContainer3.SplitterDistance = this.Size.Width - 250;                
                //splitContainer3.Panel2MinSize = 350;
                //splitContainer3.SplitterDistance = this.Size.Width - 350;                
                splitContainer3.Panel2MinSize = 450;
                splitContainer3.SplitterDistance = this.Size.Width - 450;                
            }
            else
            {
                splitContainer3.Panel2MinSize = 0;
                splitContainer3.SplitterDistance = 10000;                
            }

            dataGridView_caracteristicas.Visible = ver;
            if (ver)
                dataGridView_caracteristicas.Columns[2].Width = 190;
            Mantener_Distancias_Split();
        }

        private void checkBox_caracteristicas_CheckedChanged(object sender, EventArgs e)
        {
            Ver_Caracteristicas(checkBox_caracteristicas.Checked);

            if (!checkBox_caracteristicas.Checked)
            {
                Cargar_Caracteristicas_Codigo("*");
                label_flecha.Visible = false;
                checkBox_editar.Visible = false;
                checkBox_editar.Checked = false;
            }
            else
            {
                if (dGViewResultados.SelectedCells.Count == 1)
                {
                    if (dGViewResultados.Columns[dGViewResultados.SelectedCells[0].ColumnIndex].Name == "cODIGODataGridViewTextBoxColumn")
                    {
                        treeGridViewListaAlmacen.ClearSelection();
                        Cargar_Caracteristicas_Codigo(dGViewResultados.SelectedCells[0].Value.ToString());
                        Cargar_Valores_Combos_Caracteristicas();
                    }
                }
                else
                {
                    if (treeGridViewListaAlmacen.SelectedCells.Count == 1)
                    {
                        if (treeGridViewListaAlmacen.Columns[treeGridViewListaAlmacen.SelectedCells[0].ColumnIndex].Name == "Nombre")
                        {
                            dGViewResultados.ClearSelection();
                            Cargar_Caracteristicas_Codigo(treeGridViewListaAlmacen.CurrentCell.Value.ToString());
                            Cargar_Valores_Combos_Caracteristicas();
                        }
                    }
                }
                label_flecha.Visible = true;
                checkBox_editar.Visible = true;
            }
            
            // VSS: Ahora en lugar de deshabilitar a nivel de grid trabajamos con la única columna 
            // que alterna el modo de edición
            //dataGridView_caracteristicas.ReadOnly = true;
            dataGridView_caracteristicas.Columns["valor_caracteristica"].ReadOnly = true;
        }

        //JRM - InformeExcel - ImeApps
        private void butInforme_Click(object sender, EventArgs e)
        {
            elegirColsInforme formCols = new elegirColsInforme();
            System.Diagnostics.Debug.WriteLine(" =????????????=???????????????= Caracteristicas elegirColsInforme, botón clicked");
            formCols.Show();
        }

        public void Cargar_Caracteristicas_Codigo(string codigo)
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;
            string categoria;

            //MMM Controlamos la caracteristica Peso Teorico
            ControlAccesos.ControlAcceso controlAcceso = new ControlAccesos.ControlAcceso();
            bool MostramosPesoTeorico = controlAcceso.TieneAcceso("PesoTeorico");            
            try
            {                                  

                conexion = new SqlConnection(cadenaConexion);
                conexion.Open();

                strsql = "";
                strsql = strsql + " SELECT  categoria";
                strsql = strsql + " FROM    t_articulos";
                strsql = strsql + " WHERE   CODEMP = '" + Codemp + "' AND";
                strsql = strsql + "         codigo = '" + codigo + "'";                

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);
                if (table.Rows.Count == 0)
                    categoria = "-11";
                else
                    categoria = table.Rows[0][0].ToString();

                /* NUEVOS CÓDIGOS SUPERFAMILIAS
                strsql = "";
                strsql = strsql + " select  t_caract.caract as id_caracteristica,"; 
			    strsql = strsql + "         t_caract.denomi as denomi_caracteristica,";
			    strsql = strsql + "         (select case valor when '???' then '' else valor end from t_artcar where codemp = '" + Codemp + "' and codigo = '" + codigo + "' and catego = '" + categoria + "' and caract = t_caract.caract) as valor_caracteristica,";
                strsql = strsql + "         (select abrev from t_valcar where codemp = '" + Codemp + "' and caract = t_caract.caract and valor = (select valor from t_artcar where codemp = '" + Codemp + "' and codigo = '" + codigo + "' and catego = '" + categoria + "' and caract = t_caract.caract)) as descripcion_caracteristica,"; 
                strsql = strsql + "         t_caract.clave1 as numerica_oculto,";
                strsql = strsql + "         t_caract.clave3 as tipo_valores_oculto,";
                strsql = strsql + "         '" + categoria + "' as categoria_oculta";
                strsql = strsql + " from    t_caract inner join t_carcat on";
				strsql = strsql + "             t_caract.codemp = t_carcat.codemp and";
				strsql = strsql + "             t_caract.caract = t_carcat.caract";				
                strsql = strsql + " where	t_caract.codemp = '" + Codemp + "' and";
			    strsql = strsql + "         t_caract.caract >= '500' and";
                if (!MostramosPesoTeorico)
                    strsql = strsql + "         t_caract.caract != '613' and";

			    strsql = strsql + "         t_carcat.catego = '" + categoria + "'";                
                strsql = strsql + " order by t_caract.denomi";
                */
                strsql = "";
                strsql = strsql + " select  t1.id_caracteristica,";
                strsql = strsql + "         t1.denomi_caracteristica,";
                strsql = strsql + "         case id_caracteristica when '999' then t1.valor_caracteristica + ' --> ' + t1.descripcion_caracteristica else t1.valor_caracteristica end as valor_caracteristica,";
                strsql = strsql + "         t1.descripcion_caracteristica,";
                strsql = strsql + "         t1.numerica_oculto,";
                strsql = strsql + "         t1.tipo_valores_oculto,";
                strsql = strsql + "         t1.categoria_oculta";
                strsql = strsql + " from (";
                strsql = strsql + " select  t_caract.caract as id_caracteristica,";
                strsql = strsql + "         t_caract.denomi as denomi_caracteristica,";
                strsql = strsql + "         (select case valor when '???' then '' else valor end from t_artcar where codemp = '" + Codemp + "' and codigo = '" + codigo + "' and catego = '" + categoria + "' and caract = t_caract.caract) as valor_caracteristica,";
                strsql = strsql + "         (select abrev from t_valcar where codemp = '" + Codemp + "' and caract = t_caract.caract and valor = (select valor from t_artcar where codemp = '" + Codemp + "' and codigo = '" + codigo + "' and catego = '" + categoria + "' and caract = t_caract.caract)) as descripcion_caracteristica,";
                strsql = strsql + "         t_caract.clave1 as numerica_oculto,";
                strsql = strsql + "         t_caract.clave3 as tipo_valores_oculto,";
                strsql = strsql + "         '" + categoria + "' as categoria_oculta";
                strsql = strsql + " from    t_caract inner join t_carcat on";
                strsql = strsql + "             t_caract.codemp = t_carcat.codemp and";
                strsql = strsql + "             t_caract.caract = t_carcat.caract";
                strsql = strsql + " where	t_caract.codemp = '" + Codemp + "' and";
                strsql = strsql + "         t_caract.caract >= '500' and";
                if (!MostramosPesoTeorico)
                    strsql = strsql + "         t_caract.caract != '613' and";

                strsql = strsql + "         t_carcat.catego = '" + categoria + "') as t1";
                strsql = strsql + " order by t1.denomi_caracteristica";

                comando.CommandText = strsql;
                table.Columns.Clear();
                table.Clear();                
                adapter.Fill(table);                                
                

                dataGridView_caracteristicas.DataSource = table;                

                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cargar_Valores_Combos_Caracteristicas()
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;            
            DataGridViewComboBoxCell combo_celda;

            try
            {
                conexion = new SqlConnection(cadenaConexion);
                conexion.Open();

                comando = new SqlCommand("", conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                                
                foreach (DataGridViewRow row in dataGridView_caracteristicas.Rows)
                {
                    strsql = "";
                    strsql = strsql + " SELECT  valor,abrev";
                    strsql = strsql + " FROM    t_valcar";
                    strsql = strsql + " WHERE   CODEMP = '" + Codemp + "' AND";
                    strsql = strsql + "         caract = '" + row.Cells["id_caracteristica"].Value.ToString() + "'";
                    strsql = strsql + " order by valor";
                    comando.CommandText = strsql;
                    table.Clear();                    
                    adapter.Fill(table);                    

                    combo_celda = (DataGridViewComboBoxCell)(dataGridView_caracteristicas.Rows[row.Index].Cells["valor_caracteristica"]);

                    if (table.Rows.Count == 0)
                        combo_celda.Items.Add(row.Cells["valor_caracteristica"].Value.ToString());
                    else
                    {
                        foreach (DataRow row_table in table.Rows)
                        {
                            if (row.Cells["id_caracteristica"].Value.ToString() == "999")
                                combo_celda.Items.Add(row_table[0].ToString() + " --> " + row_table[1].ToString());
                            else
                                combo_celda.Items.Add(row_table[0].ToString());
                        }
                    }
                }
                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        

        private void dGViewResultados_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            treeGridViewListaAlmacen.ClearSelection();
            if (dGViewResultados.SelectedCells.Count < 2) 
            {
                if (dGViewResultados.Columns[e.ColumnIndex].Name == "cODIGODataGridViewTextBoxColumn")
                {
                    if (checkBox_caracteristicas.Checked)
                    {
                        Cargar_Caracteristicas_Codigo(dGViewResultados.CurrentCell.Value.ToString());
                        Cargar_Valores_Combos_Caracteristicas();
                        return;
                    }
                }
            }
            Cargar_Caracteristicas_Codigo("*");            
        }       

        private void treeGridViewListaAlmacen_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (treeGridViewListaAlmacen.SelectedCells.Count > 0 && treeGridViewListaAlmacen.SelectedCells.Count < 2)
            {
                dGViewResultados.ClearSelection();
                if (treeGridViewListaAlmacen.Columns[e.ColumnIndex].Name == "Nombre")
                {
                    if (checkBox_caracteristicas.Checked)
                    {
                        Cargar_Caracteristicas_Codigo(treeGridViewListaAlmacen.CurrentCell.Value.ToString());
                        Cargar_Valores_Combos_Caracteristicas();
                    }
                    else
                        Cargar_Caracteristicas_Codigo("*");
                }
                else
                    Cargar_Caracteristicas_Codigo("*");
            }
            else
            {
                if (dGViewResultados.SelectedCells.Count == 0)
                    Cargar_Caracteristicas_Codigo("*");
            }
        }       

        private void dataGridView_caracteristicas_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is ComboBox) 
            {   
                e.CellStyle.BackColor = Color.Aquamarine;
                ComboBox combo = (ComboBox)e.Control;
                if (dataGridView_caracteristicas.CurrentRow.Cells["tipo_valores_oculto"].Value.ToString() == "F")
                    combo.DropDownStyle = ComboBoxStyle.DropDown;
                else
                {                    
                    combo.SelectedIndexChanged -= Actualizacion_Valor_Combo;
                    combo.SelectedIndexChanged += Actualizacion_Valor_Combo;                    
                }
            }
        }

        private void Actualizacion_Valor_Combo(object sender, EventArgs e)
        {
            var sendingCB = sender as DataGridViewComboBoxEditingControl;
            string valor_actual = dataGridView_caracteristicas.CurrentCell.Value.ToString();
            string valor_nuevo = sendingCB.EditingControlFormattedValue.ToString();
            string codigo;
            if (dGViewResultados.SelectedCells.Count == 1)
                codigo = dGViewResultados.CurrentCell.Value.ToString();
            else
                codigo = treeGridViewListaAlmacen.CurrentCell.Value.ToString();
            string categoria = dataGridView_caracteristicas.CurrentRow.Cells["categoria_oculta"].Value.ToString();
            string caracteristica = dataGridView_caracteristicas.CurrentRow.Cells["id_caracteristica"].Value.ToString();

            if ((/*(caracteristica == "501") ||*/ (caracteristica == "999")) && (valor_actual != "")) //Carlos Sanchez - Se quita la caracteristica 501, para que todos puedan modificar.
            {
                ControlAccesos.ControlAcceso controlAcceso = new ControlAccesos.ControlAcceso();
                if (!controlAcceso.TieneAcceso("ModificarSuperFamila/DiseñadoPor")) 
                {
                    MessageBox.Show("No tiene permisos para cambiar el valor de esta característica", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    if (valor_nuevo != valor_actual)
                    {
                        Cargar_Caracteristicas_Codigo(codigo);
                        Cargar_Valores_Combos_Caracteristicas();
                    }
                    return;
                }                        
            }

            if (valor_nuevo != valor_actual)
            {
                if (CD.Tiene_Valor_Caracteristica_Codigo(Codemp, codigo, categoria, caracteristica))
                    CD.Actualizar_Valor_Caracteristica_Codigo(Codemp, codigo, categoria, caracteristica, valor_nuevo, Environment.UserName);
                else
                    CD.Insertar_Valor_Caracteristica_Codigo(Codemp, codigo, categoria, caracteristica, valor_nuevo, Environment.UserName);
                Cargar_Caracteristicas_Codigo(codigo);
                Cargar_Valores_Combos_Caracteristicas();                                                            
            }            
        }

        private void dataGridView_caracteristicas_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {                        
            if (dataGridView_caracteristicas.CurrentRow.Cells["tipo_valores_oculto"].Value.ToString() == "F")
            {                                                                 
                if (dataGridView_caracteristicas[e.ColumnIndex, e.RowIndex].Value.ToString() != e.FormattedValue.ToString())
                {
                    DataGridViewComboBoxCell combo_celda = (DataGridViewComboBoxCell)(dataGridView_caracteristicas.CurrentRow.Cells["valor_caracteristica"]);                    
                    combo_celda.Items.Add(e.FormattedValue.ToString());

                    string valor_actual = dataGridView_caracteristicas[e.ColumnIndex, e.RowIndex].Value.ToString();
                    string valor_nuevo = e.FormattedValue.ToString();
                    string codigo;
                    if (dGViewResultados.SelectedCells.Count == 1)
                        codigo = dGViewResultados.CurrentCell.Value.ToString();
                    else
                        codigo = treeGridViewListaAlmacen.CurrentCell.Value.ToString();
                    string categoria = dataGridView_caracteristicas.CurrentRow.Cells["categoria_oculta"].Value.ToString();
                    string caracteristica = dataGridView_caracteristicas.CurrentRow.Cells["id_caracteristica"].Value.ToString();

                    if (dataGridView_caracteristicas.CurrentRow.Cells["numerica_oculto"].Value.ToString() == "S")
                    {
                        double numero;
                        if (!double.TryParse(valor_nuevo.Replace('.','#'), out numero) && (valor_nuevo != ""))
                        {
                            MessageBox.Show("El valor de esta característica debe ser númerico", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);                                                        
                            //Cargar_Caracteristicas_Codigo(codigo);
                            //Cargar_Valores_Combos_Caracteristicas();
                            combo_celda.Items.Remove(e.FormattedValue.ToString());
                            return;
                        }
                    }

                    if (CD.Tiene_Valor_Caracteristica_Codigo(Codemp, codigo, categoria, caracteristica))
                        CD.Actualizar_Valor_Caracteristica_Codigo(Codemp, codigo, categoria, caracteristica, valor_nuevo, Environment.UserName);
                    else
                        CD.Insertar_Valor_Caracteristica_Codigo(Codemp, codigo, categoria, caracteristica, valor_nuevo, Environment.UserName);
                    //Cargar_Caracteristicas_Codigo(codigo);
                    //Cargar_Valores_Combos_Caracteristicas();          
                    ActualizarGridCaracteristicas = true;
                    ValorActual = valor_actual;
                    ValorNuevo = valor_nuevo;                    
                }                
            }                        
        }      

        private void dataGridView_caracteristicas_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {            
           if (ActualizarGridCaracteristicas)
           {                
               dataGridView_caracteristicas.CurrentCell.Value = ValorNuevo;
               DataGridViewComboBoxCell combo_celda = (DataGridViewComboBoxCell)(dataGridView_caracteristicas.CurrentRow.Cells["valor_caracteristica"]);
               combo_celda.Items.Remove(ValorActual);  
               ActualizarGridCaracteristicas = false;
           }
        }

        private void checkBox_editar_CheckedChanged(object sender, EventArgs e)
        {
            // VSS: Ahora en lugar de deshabilitar a nivel de grid trabajamos con la única columna 
            // que alterna el modo de edición

            if (checkBox_editar.Checked)
            {
                //dataGridView_caracteristicas.ReadOnly = false;
                dataGridView_caracteristicas.Columns["valor_caracteristica"].ReadOnly = false;
            }
            else
            {
                //dataGridView_caracteristicas.ReadOnly = true;
                dataGridView_caracteristicas.Columns["valor_caracteristica"].ReadOnly = true;
            }
        }

        private void button_Ficha_Diseño_Click(object sender, EventArgs e)
        {
            string archivo,codigos;
            string[] array_codigos;            
                
            archivo = "C:\\Users\\" + Environment.UserName + "\\Documents\\CodigosFichaDiseñoBuscador.txt";
            if (File.Exists(archivo)) 
            {
                StreamReader leer = new StreamReader(archivo);
                codigos = leer.ReadLine();
                leer.Close();
                if (codigos != "")
                { 
                    array_codigos = codigos.Split(new Char[]{'#'});
                    //JRM - Cambiar Empresa se cambia 3 por empresaGlobal.empresaID
                    tC_BUSCADOR_FILTROSTableAdapter.Fill(dsFiltros.TC_BUSCADOR_FILTROS, empresaGlobal.empresaID, "generales");                                         
                    foreach (string s in array_codigos)                    
                        dataGridView_filtros_generales.Rows.Add(dsFiltros.TC_BUSCADOR_FILTROS.Rows[2]["NOMBRE"], s);                                                                       

                    radioButton_Y_filtros_generales.Checked = false;
                    radioButton_O_filtros_generales.Checked = true;
                    checkBox_caracteristicas.Checked = true;
                    checkBox_editar.Checked = true;
                    button_buscar_filtros_generales_Click(sender, e);
                }
                File.Delete(archivo);
            }
        }

        private void button_guia_inventario_Click(object sender, EventArgs e)
        {
            Process proceso = new Process();
            proceso.StartInfo.FileName = "M:\\ImeApps\\Buscador\\GuiaAyudaCaracteristicasInventario.pdf";
            proceso.Start();
        }

        #endregion                                                                                                                                                                         

        // VSS
        private void dataGridView_caracteristicas_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow fila = dataGridView_caracteristicas.CurrentRow;

            if (fila.Cells["denomi_caracteristica"].Value.ToString() == "AA-Ficha de Diseño")
            {
                string url = fila.Cells["valor_caracteristica"].Value.ToString();
                url = "http://sistemacalidad/IX-PGC-04/FICHAS%20DE%20DISEO%20nueva/" + url + ".xml"; 

                //MessageBox.Show();
                VisorWeb frm = new VisorWeb(url);
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
            }           
        }

        // VSS: Funcionalidad Heredar Características Compartidas
        private void HeredarCaracterísticasCompartidasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DiccionarioCodigosSeleccionados.Count != 1)
            {
                MessageBox.Show("Acción no válida, debe seleccionar una única celda para poder realizar esta acción.");
            }
            else
            {
                //string componente = DiccionarioCodigosSeleccionados.[0].ToString();
                string articuloOrigen = treeGridViewListaAlmacen.CurrentNode.Cells[0].Value.ToString();

                var confirmResult = MessageBox.Show(
                    "Se copiarán desde el artículo \"" + articuloOrigen + "\" a toda su estructura, todas las características comunes entre el artículo padre y cada uno de los artículos de su estructura (no se reemplazan los valores en los artículos hijos si ya tuvierán)",
                        "Confirmación de acción", MessageBoxButtons.YesNo);

                //"¿Desea traspasar los valores de las características del componente \"" + componente + "\" a las características coincidentes de sus componentes hijos?",
                
                if (confirmResult == DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    foreach (TreeGridNode nodo in treeGridViewListaAlmacen.CurrentNode.Nodes)
                    {
                        // Función recursiva
                        SobreescribirValoresCaracteristicasComunesEnSubcomponente(nodo, articuloOrigen);
                    }                                        

                    Cursor.Current = Cursors.Arrow;
                }
            }
        }

        private void copiarSuperFamiliaALosHijosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DiccionarioCodigosSeleccionados.Count != 1)
            {
                MessageBox.Show("Acción no válida, debe seleccionar una única celda para poder realizar esta acción.");
            }
            else
            {                
                string articuloOrigen = treeGridViewListaAlmacen.CurrentNode.Cells[0].Value.ToString();
                string categoriaOrigen = treeGridViewListaAlmacen.CurrentNode.Cells[5].Value.ToString();
                string superfamilia = Utils.CD.TieneSuperFamiliaCodigo(Codemp, articuloOrigen, categoriaOrigen);

                if (superfamilia == "-111")
                    MessageBox.Show("El artículo no tiene SuperFamilia", "Información", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                else
                {
                    var confirmResult = MessageBox.Show(
                        "Se copiará la SuperFamilia desde el artículo \"" + articuloOrigen + "\" a toda su estructura (reemplaza la SuperFamilia en los artículos hijos si ya la tienen asignada)",
                            "Confirmación de acción", MessageBoxButtons.YesNo);

                    if (confirmResult == DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        if(Utils.CD.CopiarCaracteristicaHijos(Codemp, articuloOrigen, "999", superfamilia, Environment.UserName))
                            MessageBox.Show("Copia de superfamilia realizada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Error al realizar la copia de superfamilia", "Información", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        Cursor.Current = Cursors.Arrow;
                    }
                }
            }
        }

        private void copiarDiseñadoPorALosHijosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DiccionarioCodigosSeleccionados.Count != 1)
            {
                MessageBox.Show("Acción no válida, debe seleccionar una única celda para poder realizar esta acción.");
            }
            else
            {
                string articuloOrigen = treeGridViewListaAlmacen.CurrentNode.Cells[0].Value.ToString();
                string categoriaOrigen = treeGridViewListaAlmacen.CurrentNode.Cells[5].Value.ToString();
                string diseñadopor = Utils.CD.TieneDiseñadoPorCodigo(Codemp, articuloOrigen, categoriaOrigen);

                if (diseñadopor == "zzz")
                    MessageBox.Show("El artículo no tiene valor en Diseñado por", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                {
                    var confirmResult = MessageBox.Show(
                        "Se copiará el valor de Diseñado por desde el artículo \"" + articuloOrigen + "\" a toda su estructura (reemplaza el valor en los artículos hijos si ya lo tienen asignado)",
                            "Confirmación de acción", MessageBoxButtons.YesNo);

                    if (confirmResult == DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        if (Utils.CD.CopiarCaracteristicaHijos(Codemp, articuloOrigen, "501", diseñadopor, Environment.UserName))
                            MessageBox.Show("Copia de Diseñado por realizada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Error al realizar la copia de Diseñado por", "Información", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        Cursor.Current = Cursors.Arrow;
                    }
                }
            }
        }

        // VSS: Función recursiva utilizada por la Funcionalidad Heredar Características Compartidas
        private void SobreescribirValoresCaracteristicasComunesEnSubcomponente(TreeGridNode nodo, string articuloOrigen)
        {
            string articuloDestino = nodo.Cells[0].Value.ToString();
            DateTime horaActual = DateTime.Now;

            // Esta función se encarga de actualizar valores de las características existentes en T_ARTCAR 
            // tanto para el origen como para el artículo destino
            global.ActualizaValoresCaracteristicasCompartidasUPDATE(Codemp, articuloOrigen, articuloDestino, Environment.UserName, horaActual);

            // Esta función se encarga de insertar en T_ARTCAR características que correspondan al artículo destino según su categoría (T_CARCAT)
            // que no existan todavía en T_ARTCAR con valores de carácteristicas del artículo origen
            global.ActualizaValoresCaracteristicasCompartidasINSERT(Codemp, articuloOrigen, articuloDestino, Environment.UserName, horaActual);

            // Obtenemos todos los subcomponentes del componente para recorrer recursivamente            
            foreach (TreeGridNode nodoHijo in nodo.Nodes)
            {
                SobreescribirValoresCaracteristicasComunesEnSubcomponente(nodoHijo, articuloOrigen);
            }
        }        

        //// VSS: Funcionalidad Heredar Características Compartidas
        //private void HeredarCaracterísticasCompartidasToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (DiccionarioCodigosSeleccionados.Count != 1)
        //    {
        //        MessageBox.Show("Acción no válida, debe seleccionar una única celda para poder realizar esta acción.");
        //    }
        //    else
        //    {
        //        //string componente = DiccionarioCodigosSeleccionados.[0].ToString();
        //        string componente = treeGridViewListaAlmacen.CurrentNode.Cells[0].Value.ToString();

        //        var confirmResult = MessageBox.Show(
        //            "Se reemplazarán y copiarán desde el artículo \"" + componente + "\" a toda su estructura, todas las características comunes entre el articulo padre y cada uno de los artículos de su estructura",
        //                "Confirmación de acción", MessageBoxButtons.YesNo);

        //        //"¿Desea traspasar los valores de las características del componente \"" + componente + "\" a las características coincidentes de sus componentes hijos?",


        //        if (confirmResult == DialogResult.Yes)
        //        {
        //            Cursor.Current = Cursors.WaitCursor;

        //            // Obtener todas las características y valores del componente seleccionado
        //            DataTable dtCaracteristicas = DAL.global.ValoresCaracteristicaDeComponente(Codemp, componente);

        //            // Obtenemos todos los subcomponentes del componente para recorrer recursivamente
        //            //DataTable dtSubcomponentes = DAL.global.Subcomponentes(Codemp, componente);
        //            //foreach (DataRow fila in dtSubcomponentes.Rows)
        //            //{
        //            //    // Función recursiva
        //            //    SobreescribirValoresCaracteristicasComunesEnSubcomponente(fila["COMPON"].ToString(), ref dtCaracteristicas);
        //            //}

        //            foreach (TreeGridNode nodo in treeGridViewListaAlmacen.CurrentNode.Nodes)
        //            {
        //                // Función recursiva
        //                SobreescribirValoresCaracteristicasComunesEnSubcomponente(nodo, ref dtCaracteristicas);
        //            }

        //            //MessageBox.Show(componente);

        //            Cursor.Current = Cursors.Arrow;
        //        }
        //    }
        //}

        //// VSS: Función recursiva utilizada por la Funcionalidad Heredar Características Compartidas
        //private void SobreescribirValoresCaracteristicasComunesEnSubcomponente(TreeGridNode nodoPadre, ref DataTable dtCaracteristicas)
        //{
        //    // Obtenemos las características del subcomponente
        //    DataTable dtCaracteristicas2 = DAL.global.ValoresCaracteristicaDeComponente(Codemp, nodoPadre.Cells[0].Value.ToString());

        //    // Si destino tiene características, buscamos las coincidencias con respecto a origen
        //    if (dtCaracteristicas2.Rows.Count > 0)
        //    {
        //        DataRow[] consulta;
        //        // Recorremos todas las características origen
        //        foreach (DataRow fila in dtCaracteristicas.Rows)
        //        {
        //            consulta = dtCaracteristicas2.Select("CARACT = '" + fila["CARACT"].ToString() + "'");
        //            // Si el destino contiene la característica en estudio de esta iteración:
        //            if (consulta.Length > 0)
        //            {
        //                // Si para la característica en estudio el valor es diferente en el subcomponente: sobreescribimos con el valor del origen
        //                if (consulta[0]["VALOR"].ToString() != fila["VALOR"].ToString())
        //                {
        //                    DataRow fila2 = consulta[0];

        //                    DAL.global.ActualizaValorCaracteristica(Codemp, fila2["CODIGO"].ToString(),
        //                                fila2["CATEGO"].ToString(), fila2["CARACT"].ToString(), fila["VALOR"].ToString());
        //                }
        //            }
        //        }
        //    }

        //    // Obtenemos todos los subcomponentes del componente para recorrer recursivamente            
        //    foreach (TreeGridNode nodo in nodoPadre.Nodes)
        //    {
        //        SobreescribirValoresCaracteristicasComunesEnSubcomponente(nodo, ref dtCaracteristicas);
        //    }
        //}

        //// VSS: Función recursiva utilizada por la Funcionalidad Heredar Características Compartidas
        //private void SobreescribirValoresCaracteristicasComunesEnSubcomponente(string subcomponente, ref DataTable dtCaracteristicas)
        //{
        //    // Obtenemos las características del subcomponente
        //    DataTable dtCaracteristicas2 = DAL.global.ValoresCaracteristicaDeComponente(Codemp, subcomponente);

        //    // Si destino tiene características, buscamos las coincidencias con respecto a origen
        //    if (dtCaracteristicas2.Rows.Count > 0)
        //    {
        //        DataRow[] consulta;
        //        // Recorremos todas las características origen
        //        foreach (DataRow fila in dtCaracteristicas.Rows)
        //        {
        //            consulta = dtCaracteristicas2.Select("CARACT = '" + fila["CARACT"].ToString() + "'");
        //            // Si el destino contiene la característica en estudio de esta iteración:
        //            if (consulta.Length > 0)
        //            {
        //                // Si para la característica en estudio el valor es diferente en el subcomponente: sobreescribimos con el valor del origen
        //                if (consulta[0]["VALOR"].ToString() != fila["VALOR"].ToString())
        //                {
        //                    DataRow fila2 = consulta[0];

        //                    DAL.global.ActualizaValorCaracteristica(Codemp, fila2["CODIGO"].ToString(),
        //                                fila2["CATEGO"].ToString(), fila2["CARACT"].ToString(), fila["VALOR"].ToString());
        //                }
        //            }
        //        }
        //    }

        //    // Obtenemos todos los subcomponentes del componente para recorrer recursivamente
        //    DataTable dtSubcomponentes = DAL.global.Subcomponentes(Codemp, subcomponente);
        //    foreach (DataRow fila in dtSubcomponentes.Rows)
        //    {
        //        SobreescribirValoresCaracteristicasComunesEnSubcomponente(fila["COMPON"].ToString(), ref dtCaracteristicas);
        //    }       
        //}


        // VSS: funcionalidad exportar a excel grid de componentes
        private void butExcel_Click(object sender, EventArgs e)
        {
            //Añadido por José Manuel Romero 10/12/2019
            //condición para mostrar una tabla dinámica al excel
            bool excelPivotTable = false;
            if (checkBox_pivotTable.Checked)
            {
                excelPivotTable = true;
            }

            //Añadido por José Manuel Romero 10/12/2019
            //condición para incluir fórmulas en el excel
            bool formulas = false;
            if (checkBox_formulas.Checked)
            {
                formulas = true;
            }

            if (treeGridViewListaAlmacen.Nodes.Count == 0)
            {
                MessageBox.Show("No hay elementos que exportar");
            }
            else
            {
                rutaRecursivaNodo = new ArrayList();

                int nivelMax = 0;
                foreach (TreeGridNode nodo in treeGridViewListaAlmacen.Nodes)
                {
                    if (nodo.Level > nivelMax)
                    {
                        nivelMax = nodo.Level;
                    }
                    

                    obtieneNivelMaximo(nodo, ref nivelMax);
                    System.Diagnostics.Debug.WriteLine(" ========== El nivel máximo obtenido es: " + nivelMax);

                }
                

                dtExcel = new DataTable();

                for (int i = 0; i < nivelMax; i++)
                {
                    dtExcel.Columns.Add("Nivel " + i.ToString());
                }

                dtExcel.Columns.Add("Cantidad");
                dtExcel.Columns.Add("Denominación");
                dtExcel.Columns.Add("Categoría");
                dtExcel.Columns.Add("Familia");
                dtExcel.Columns.Add("Peso Unitario");
                dtExcel.Columns.Add("MP");
                dtExcel.Columns.Add("Longitud");
                dtExcel.Columns.Add("Area");
                dtExcel.Columns.Add("Fabricable");
                dtExcel.Columns.Add("Correl");

                //Añadido por Jose Manuel Romero 25/11/2019, Se añade la columna Cantidad Total al Excel
                dtExcel.Columns.Add("Cantidad Total");

                dtExcel.Columns.Add("Peso Total");
                dtExcel.Columns.Add("Nivel");

                DataRow fila;
            
                foreach (TreeGridNode nodo in treeGridViewListaAlmacen.Nodes)
                {
          
                    string nombre = nodo.Cells[0].Value.ToString();
                    rutaRecursivaNodo.Add(nombre);
                    System.Diagnostics.Debug.WriteLine(" ========== Muestro el nombre: " + nombre);
                    fila = dtExcel.NewRow();
                    //Nivel en el que estamos
                    int n = rutaRecursivaNodo.Count;
                    for (int i = 0; i < n; i++)
                    {
                        System.Diagnostics.Debug.WriteLine(" ========== Nivel: " + i);
                        fila["Nivel " + i.ToString()] = "'" + rutaRecursivaNodo[i].ToString();
                    }

                    
                    /*for (int i = 0; i <= dtExcel.Rows.Count - 1; i++) {
                        for (int j = 0; j <= dtExcel.Columns.Count - 1; j++)
                        {
                            //Object cellx = dtExcel.Rows[i][j];
                            int numberx = dtExcel.Rows[i].Field<int>(j);
                            System.Diagnostics.Debug.WriteLine(" ========== Nivel: " + i);
                        }
                    }*/

                    
                    fila["Cantidad"] = nodo.Cells[3].Value.ToString();
                    fila["Denominación"] = nodo.Cells[4].Value.ToString();
                    //fila["Categoría"] = Convert.ToString((nodo.Cells[5] as DataGridViewComboBoxCell).FormattedValue.ToString());
                    fila["Categoría"] = nodo.Cells[5].Value.ToString();
                   // fila["Categoría"] = valorfinalString;
                    //fila["Familia"] = Convert.ToString((nodo.Cells[6] as DataGridViewComboBoxCell).FormattedValue.ToString());
                    fila["Familia"] = nodo.Cells[6].Value.ToString();
                    fila["Peso Unitario"] = nodo.Cells[7].Value.ToString();
                    //fila["MP"] = Convert.ToString((nodo.Cells[8] as DataGridViewComboBoxCell).FormattedValue.ToString());
                    fila["MP"] = nodo.Cells[8].Value.ToString();
                    fila["Longitud"] = nodo.Cells[9].Value.ToString();
                    fila["Area"] = nodo.Cells[10].Value.ToString();
                    fila["Fabricable"] = nodo.Cells[11].Value.ToString();
                    fila["Correl"] = nodo.Cells[12].Value.ToString();

                    //Añadido por Jose 25/11/2019, fila para Cantidad_total
                    fila["Cantidad Total"] = nodo.Cells[13].Value.ToString();
                    //valores de debajo han sido aumentado en 1

                    fila["Peso Total"] = nodo.Cells[14].Value.ToString();
                    fila["Nivel"] = nodo.Cells[15].Value.ToString();
                    dtExcel.Rows.Add(fila);

                    recorreNodoTreeView(nodo);

                    rutaRecursivaNodo.RemoveAt(rutaRecursivaNodo.Count - 1);
                    Debug.Flush();
                }

                //InteropExcel.DataTableToExcel(dtExcel, "c:\\temporalesPlanificacionST\\1.xls");
                
                InteropExcel.DataTableToExcel(dtExcel, "", excelPivotTable, formulas);

            }
        }

        // VSS: función recursiva para obtener la profundidad máxima del árbol
        private void obtieneNivelMaximo(TreeGridNode nodoPadre, ref int nivelMax)
        {             
             foreach (TreeGridNode nodo in nodoPadre.Nodes)
             {
                 if (nodo.Level > nivelMax)
                 {
                     nivelMax = nodo.Level;
                 }

                 obtieneNivelMaximo(nodo, ref nivelMax);
             }
        }

        // VSS: función recursiva para construir datatable a partir del árbol
        private void recorreNodoTreeView(TreeGridNode nodoPadre)
        {
            DataRow fila;
            foreach (TreeGridNode nodo in nodoPadre.Nodes)
            {
                string nombre = nodo.Cells[0].Value.ToString();
               
                rutaRecursivaNodo.Add(nombre);   

                fila = dtExcel.NewRow();
                int n = rutaRecursivaNodo.Count;
                for (int i = 0; i < n; i++)
                {
                    fila["Nivel " + i.ToString()] = "'" + rutaRecursivaNodo[i].ToString();
                }
                
                
                fila["Cantidad"] = nodo.Cells[3].Value.ToString();
                int ri = nodo.Cells[3].ColumnIndex;
                int ci = nodo.Cells[3].ColumnIndex;
                //String prueba1 = Convert.ToString((nodo.Cells[3] as DataGridViewComboBoxCell).FormattedValue.ToString());
                //System.Diagnostics.Debug.WriteLine(" ========== Probando fila ... : " + ri + " y columna: " + ci);

                fila["Denominación"] = nodo.Cells[4].Value.ToString();
                //fila["Categoría"] = Convert.ToString((nodo.Cells[5] as DataGridViewComboBoxCell).FormattedValue.ToString());
                fila["Categoría"] = nodo.Cells[5].Value.ToString();
                //fila["Familia"] = Convert.ToString((nodo.Cells[6] as DataGridViewComboBoxCell).FormattedValue.ToString());
                fila["Familia"] = nodo.Cells[6].Value.ToString();
                fila["Peso Unitario"] = nodo.Cells[7].Value.ToString();
                //fila["MP"] = Convert.ToString((nodo.Cells[8] as DataGridViewComboBoxCell).FormattedValue.ToString());
                fila["MP"] = nodo.Cells[8].Value.ToString();
                fila["Longitud"] = nodo.Cells[9].Value.ToString();
                fila["Area"] = nodo.Cells[10].Value.ToString();
                fila["Fabricable"] = nodo.Cells[11].Value.ToString();
                fila["Correl"] = nodo.Cells[12].Value.ToString();
                //Añadido por José Manuel 25/11/2019, Fila para cantidad_total
                fila["Cantidad Total"] = nodo.Cells[13].Value.ToString();
                //valores de debajo han sido aumentado en 1
                fila["Peso Total"] = nodo.Cells[14].Value.ToString();
                fila["Nivel"] = nodo.Cells[15].Value.ToString();                       
                dtExcel.Rows.Add(fila);

                recorreNodoTreeView(nodo);

                rutaRecursivaNodo.RemoveAt(rutaRecursivaNodo.Count - 1);
            }
        }

        #region GMM: Implosion

        private void implosiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Ahora solo limpiamos el grid de componentes si se tiene marcada la opción de limpiar listado de componentes
            if (checkBox_LimpiarComponentes.Checked)
            {
                limpiarArbol();
            }
            // limpiarArbol();
            // limpiarNavegador();

            if (dGViewResultados.SelectedCells.Count > 0)
            {
                DataGridViewCell cell = dGViewResultados.SelectedCells[0];


                if (cell.ColumnIndex == 0 && cell.RowIndex >= 0)
                {
                    String codigo = dGViewResultados[cell.ColumnIndex, cell.RowIndex].Value.ToString().Trim();

                    dGViewResultados.Enabled = false;
                    //UseWaitCursor = true;
                    Cursor.Current = Cursors.WaitCursor;

                    if (codigo.Length > 0)
                    {
                        //TOMÁS 30/11/2017. Comprobar que pesos sacar
                        int peso_tornillos = 0;
                        if (radioButton_PesoConTornillos.Checked)
                            peso_tornillos = 1;
                        if (radioButton_PesoSoloTornillos.Checked)
                            peso_tornillos = 2;
                                                
                        dT = CD.GetListaAlmacenAscendentes(this.Codemp, codigo, peso_tornillos);
                        if (dT.Rows.Count > 0)
                            crearArbol();
                        else
                            MessageBox.Show("El código seleccionado no tiene lista de almacén");
                    }
                    else
                        MessageBox.Show("El código elegido no tiene caracteres");

                    //UseWaitCursor = false;
                    Cursor.Current = Cursors.Arrow;
                    dGViewResultados.Enabled = true;
                }
            }
        }

        //
        private void copiarAMadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            //srvdesarrollo srvsql02
            string connetionString = @"Data Source=srvsql02;Initial Catalog=gg;User ID=gg;Password=ostia";
            string codigo = dGViewResultados.SelectedCells[0].Value.ToString();

            string sql = "IME_CopiarCodigoEntreEmpresas " + "'3'" + "," + "'60'" + ", '" + codigo + "', '" + Environment.UserName + "'";

            using (SqlConnection connection =
                new SqlConnection(connetionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.CommandTimeout = 1600;
                connection.Open();

                string resultout = (string)command.ExecuteScalar();
                
                connection.Close();
                MessageBox.Show(resultout, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Cursor.Current = Cursors.Default;
        }

        //CSanchez 10/03/2021 se añade esta condición para poder eliminar codigos de la empresa 60 (Made)
        private void eliminarDeMadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
           Cursor.Current = Cursors.WaitCursor;
           List<String> codigos = new List<String>();
            string codigo = dGViewResultados.SelectedCells[0].Value.ToString();

            DataTable dt = obtenerCodigosEliminar("60", codigo);

            for (int i = 0; i < dt.Rows.Count; i++) {
                codigos.Add(dt.Rows[i][0].ToString());
            }

            foreach (string cod in codigos)
            {

                //srvdesarrollo srvsql02
                string connetionString = @"Data Source=srvsql02;Initial Catalog=gg;User ID=gg;Password=ostia";




                string sql = "eliminarDeEmpresa" + "'60'" + ", '" + cod + "'";

                using (SqlConnection connection =
                new SqlConnection(connetionString))
                {
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.CommandTimeout = 1600;
                    connection.Open();

                    string resultout = (string)command.ExecuteScalar();

                    connection.Close();
                    if (!resultout.Equals("Codigo eliminado correctamente"))
                    {
                        MessageBox.Show(resultout, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }

            MessageBox.Show("Se han aplicado cambios", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Cursor.Current = Cursors.Default;

        }


        public DataTable obtenerCodigosEliminar(string codemp, string codigo)
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;

            try
            {
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();

                strsql = @"	declare @CodEmp varchar(2) = '"+codemp+@"'
	                        declare @Codigo varchar(20) = '" + codigo + @"'

	                        declare @Altern varchar(1) = '0'
	                        declare @Idioma varchar(2) = 'es'
	                        declare @peso_tornillos int = 0
	
	                        -- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	                        SET NOCOUNT ON;	

	                        Declare @Cantidad float = 1
	                        Declare @maxNivel int = 20
	                        Declare @semilla int = 0



	                        IF OBJECT_ID(N'tempdb..#T_Lista_Almacen', N'U') IS NOT NULL 
		                        DROP TABLE #T_Lista_Almacen
    
                            SELECT @CodEmp as CodEmp,
			                        @Altern as Altern,
			                        0 as ID,
			                        CODIGO as Codigo_Padre, 
			                        @Cantidad as Cantidad_Padre, 			
			                        null as ID_PADRE,
			                        CODIGO as Codigo_Hijo, 
			                        @Cantidad as Cantidad_Hijo, 
			                        CATEGORIA as Categoria_Hijo, 
			                        FAMILIA as Familia_Hijo, 
			                        0 as Correlacion,
			                        0 as Nivel
			                        Into #T_Lista_Almacen
		                        FROM T_ARTICULOS
		                        WHERE CodEmp = @CodEmp 
			                        AND CODIGO = @Codigo

	                        Declare @Nivel int = 0
                            WHILE (@Nivel <= @maxNivel) AND (@@ROWCOUNT > 0) BEGIN
                                SET @Nivel = @Nivel + 1
        
                                INSERT INTO #T_Lista_Almacen
                                SELECT @CodEmp as CodEmp,
				                        @Altern as Altern,
				                        @semilla + row_number() over(ORDER BY @maxNivel) as Id,				
				                        T_ESTRUC.CONJUN as Codigo_Padre, 
				                        T1.Cantidad_Hijo as Cantidad_Padre, 				
				                        T1.ID as Id_Padre,
				                        T_ESTRUC.COMPON as Codigo_Hijo, 				
				                        T_ESTRUC.CTDENL as Cantidad_Hijo,		--(T1.Cantidad_Hijo * T_ESTRUC.CTDENL) as Cantidad_Hijo, 
				                        T_ARTICULOS.CATEGORIA as Categoria_Hijo, 
				                        T_ARTICULOS.FAMILIA Familia_Hijo, 
				                        T_ESTRUC.CORREL as Correlacion, 
				                        @Nivel as Nivel
			                        FROM #T_Lista_Almacen T1 INNER JOIN T_ESTRUC 
					                        ON T1.Codigo_Hijo = T_ESTRUC.CONJUN INNER JOIN T_ARTICULOS 
					                        ON T_ESTRUC.CodEMP = T_ARTICULOS.CodEMP AND T_ESTRUC.COMPON = T_ARTICULOS.CODIGO
			                        WHERE (T_ESTRUC.CodEmp = @CodEmp)
					                        AND (T_ESTRUC.Altern = @Altern)
					                        AND (T1.Nivel = @Nivel - 1) 
					                        AND (T1.Familia_Hijo <> 'TO')		--Así no descompone la estructura de los TO
					                        AND (T_ARTICULOS.FAMILIA <> 'MP')	--Ásí no devuelve la MP por la que está formada un código
					
		                        set @semilla = @semilla + @@ROWCOUNT
                            END        
	
	
	                        select Codigo_Hijo from(
	                        SELECT  			t_1.Codigo_Hijo,			Case
				                        when t_1.categoria_Hijo='TO' then
					                        (Select Valor
					                        from T_ARTCAR
					                        where (CodEMP = @CodEmp) and (CODIGO = t_1.Codigo_Hijo) and (CATEGO =  'TO') and (CARACT = '611')
					                        )
				                        else
					                        (Select T_ARTICULOS.CATEGORIA
						                        From T_ESTRUC INNER JOIN T_ARTICULOS 
								                        ON T_ESTRUC.CodEMP = T_ARTICULOS.CodEMP AND T_ESTRUC.COMPON = T_ARTICULOS.CODIGO
						                        Where (T_ESTRUC.CodEMP = @CodEmp) AND (T_ARTICULOS.FAMILIA = 'MP') AND (T_ESTRUC.ALTERN = '0') AND (T_ESTRUC.CONJUN = t_1.Codigo_Hijo)
					                        )
			                        end as CATEGORIA,
			                        t_1.Nivel
		                        FROM #T_Lista_Almacen as t_1 Left Outer Join T_ARTICULOS
			                        on t_1.CodEmp = T_ARTICULOS.CodEMP and t_1.Codigo_Hijo = T_ARTICULOS.CODIGO 
					                        Left Outer Join (SELECT CodEMP, CODIGO, CATEGO, VALOR
										                        FROM T_ARTCAR
										                        WHERE (CARACT = '04')
									                        ) as T_ARTCAR_Long
			                        on T_ARTICULOS.CodEMP = T_ARTCAR_Long.CodEMP and T_ARTICULOS.CODIGO = T_ARTCAR_Long.CODIGO and T_ARTICULOS.CATEGORIA = T_ARTCAR_Long.CATEGO
					                        Left Outer Join (SELECT CodEMP, CODIGO, CATEGO, VALOR
										                        FROM T_ARTCAR
										                        WHERE (CARACT = '05')
									                        ) as T_ARTCAR_Area
			                        on T_ARTICULOS.CodEMP = T_ARTCAR_Area.CodEMP and T_ARTICULOS.CODIGO = T_ARTCAR_Area.CODIGO and T_ARTICULOS.CATEGORIA = T_ARTCAR_Area.CATEGO
					                        Left Outer Join (SELECT CodEMP, CODIGO, CATEGO, VALOR
										                        FROM T_ARTCAR
										                        WHERE (CARACT = '100')
									                        ) as T_ARTCAR_Fabricable
			                        on T_ARTICULOS.CodEMP = T_ARTCAR_Fabricable.CodEMP and T_ARTICULOS.CODIGO = T_ARTCAR_Fabricable.CODIGO and T_ARTICULOS.CATEGORIA = T_ARTCAR_Fabricable.CATEGO		

			                        --MMM 22_05_19
			                        Left Outer Join (SELECT CodEMP, CODIGO, CATEGO, VALOR
										                        FROM T_ARTCAR
										                        WHERE (CARACT = '17')
									                        ) as T_ARTCAR_Taladrado
			                        on T_ARTICULOS.CodEMP = T_ARTCAR_Taladrado.CodEMP and T_ARTICULOS.CODIGO = T_ARTCAR_Taladrado.CODIGO and T_ARTICULOS.CATEGORIA = T_ARTCAR_Taladrado.CATEGO) as t1 where CATEGORIA is null--where CATEGORIA <> 'M1'
			
		                        ORDER BY Nivel desc, Codigo_Hijo";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                conexion.Close();

                return table;
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
                MessageBox.Show(Form.ActiveForm, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void dGViewResultados_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            //JoseManuelRM 09/06/2020 click en la columna Pedido y que se abra la carpeta
            //obtengo el nombre de la columna
            if (e.ColumnIndex >= 0)
            {
                string xxd = dGViewResultados.Columns[e.ColumnIndex].Name;


                //Se compruba que el click sea en la columna Pedido
                if (dGViewResultados.Columns[e.ColumnIndex].Name == "Pedido")
                {
                    string ped = dGViewResultados.SelectedCells[0].Value.ToString();
                    string path = @"L:\";

                    DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(path);
                    FileSystemInfo[] filesAndDirs = hdDirectoryInWhichToSearch.GetFileSystemInfos("*" + ped + "*");
                    string fullName = "";
                    //Se busca la carpeta
                    foreach (FileSystemInfo foundFile in filesAndDirs)
                    {
                        fullName = foundFile.FullName + @"\";
                        Process.Start(fullName);
                    }
                    //Si no se encuentra ninguna carpeta
                    var found = filesAndDirs.Any();
                    if (found == false)
                    {
                        MessageBox.Show("No se ha encontrado ninguna carpeta relacionada con el pedido " + ped, "Carpeta No Existente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }


                //JRM - InformeExcel - ImeApps
                currentDgResultadosRow = dGViewResultados.CurrentRow.Cells["cODIGODataGridViewTextBoxColumn"].Value.ToString();

                if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex >= 0 && dGViewResultados.Columns[e.ColumnIndex].HeaderText == cODIGODataGridViewTextBoxColumn.HeaderText)
                {
                    dGViewResultados.CurrentCell = dGViewResultados.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cmsResultados.Show(MousePosition);
                }

                DiccionarioCodigosSeleccionados.Clear();
                DataGridViewSelectedCellCollection celdasSeleccionadas = dGViewResultados.SelectedCells;
                foreach (DataGridViewCell dataGridViewCell in celdasSeleccionadas)
                {
                    DataGridViewRow dr = dataGridViewCell.OwningRow;

                    String codigo = dr.Cells[0].Value.ToString();

                    this.DiccionarioCodigosSeleccionados.Add(this.DiccionarioCodigosSeleccionados.Count, codigo);
                }
            }
        }
        #endregion


        //JRegino 27/06/2017. Resaltar códigos obsoletos para que lo vean de un vistazo por si la columna obsoleto la tienen "oculta" por tener el campo denominacion muy expandido.
        private void dGViewResultados_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (this.dGViewResultados.Rows[e.RowIndex].Cells[5].Value != DBNull.Value)
                if (Convert.ToInt32(this.dGViewResultados.Rows[e.RowIndex].Cells[5].Value) == 1)
                    this.dGViewResultados.Rows[e.RowIndex].Cells[0].Style.BackColor = Color.LightGray;
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void treeGridViewListaAlmacen_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        public void dataGridViewMixMateriales_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex == 0) && (e.ColumnIndex == 2))
            {
                //DataGridViewCell dgvc = treeGridViewListaAlmacen.Rows[treeGridViewListaAlmacen.CurrentRow.Index].Cells[0];
                List<DataGridViewCell> dgvc_List = new List<DataGridViewCell>();
                List<TreeGridNode> tgn_List = new List<TreeGridNode>();

                for (int i = 0; i < treeGridViewListaAlmacen.Rows.Count; i++)
                {
                    //Para esta fila, vemos si existe alguna celda seleccionada.
                    for (int j = 0; j < treeGridViewListaAlmacen.Rows[i].Cells.Count; j++)
                    {
                        if (treeGridViewListaAlmacen.Rows[i].Cells[j].Selected)
                        {
                            DataGridViewCell dgvcAux = treeGridViewListaAlmacen.Rows[i].Cells[0];
                            dgvc_List.Add(dgvcAux);
                            TreeGridNode tgn = GetNodeByText(treeGridViewListaAlmacen.Nodes, dgvcAux.Value.ToString());
                            if (tgn != null)
                                tgn_List.Add(tgn);
                            break;
                        }
                    }
                }

                MostrarMixMateriales(dgvc_List, tgn_List);
            }
        }

        public static void dataGridViewMixMateriales_CellValueChangedInforme(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex == 0) && (e.ColumnIndex == 2))
            {
                //DataGridViewCell dgvc = treeGridViewListaAlmacen.Rows[treeGridViewListaAlmacen.CurrentRow.Index].Cells[0];
                List<DataGridViewCell> dgvc_List = new List<DataGridViewCell>();
                List<TreeGridNode> tgn_List = new List<TreeGridNode>();

                for (int i = 0; i < treeGridViewListaAlmacen.Rows.Count; i++)
                {
                    //Para esta fila, vemos si existe alguna celda seleccionada.
                    for (int j = 0; j < treeGridViewListaAlmacen.Rows[i].Cells.Count; j++)
                    {
                        if (treeGridViewListaAlmacen.Rows[i].Cells[j].Selected)
                        {
                            DataGridViewCell dgvcAux = treeGridViewListaAlmacen.Rows[i].Cells[0];
                            dgvc_List.Add(dgvcAux);
                            TreeGridNode tgn = GetNodeByText(treeGridViewListaAlmacen.Nodes, dgvcAux.Value.ToString());
                            if (tgn != null)
                                tgn_List.Add(tgn);
                            break;
                        }
                    }
                }

            }
        }

        private void dataGridViewMixMateriales_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void radioButton_PesoConTornillos_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton_PesoSinTornillos_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox_detallado_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox_pivotTable_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox_formulas_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void Intranet_FormClosing(object sender, FormClosingEventArgs e)
        {
            empresaGlobal.showEmp = true;
        }


        private void btn_todos_codigos_Click(object sender, EventArgs e)
        {

            if (checkBox_LimpiarComponentes.Checked)
            {
                limpiarArbol();
            }


            foreach (DataGridViewRow row in dGViewResultados.Rows)
            {
               // if (row.Cells[0].Selected)
               // {
                    String codigo = row.Cells[0].Value.ToString().Trim();



                    //JRM
                    //getMaterialesInforme = obtenerMixMateriales(codigo);

                    dGViewResultados.Enabled = false;
                    //UseWaitCursor = true;
                    Cursor.Current = Cursors.WaitCursor;

                    if (codigo.Length > 0)
                    {

                        //TOMÁS 30/11/2017. Comprobar que pesos sacar
                        int peso_tornillos = 0;
                        if (radioButton_PesoConTornillos.Checked)
                            peso_tornillos = 1;
                        if (radioButton_PesoSoloTornillos.Checked)
                            peso_tornillos = 2;

                        //MMM 11/01/18 Guardamos los registros del dT en caso de que no limpiemos el listado.                    
                        if ((!checkBox_LimpiarComponentes.Checked) && (dT != null))
                        {
                            DataTable dtAux = new DataTable();
                            dtAux = dT.Copy();
                            listdtAux.Add(dtAux);
                        }
                        else
                        {//Si limpiamos los componenetes, limpiamos la lista de dt.
                            if (listdtAux.Count() > 0)
                                listdtAux.Clear();
                        }


                        //JRegino 05/07/2016. Añadido parámetro al proc para que devuelva la denominacion en el idioma deseado                    
                        dT = CD.GetListaAlmacen(this.Codemp, codigo, cmbIdioma.Text, peso_tornillos);


                        if (dT.Rows.Count > 0)
                        {

                            crearArbol();

                        }
                        else
                            MessageBox.Show("El código seleccionado no tiene lista de almacén");
                    }
                    else
                        MessageBox.Show("El código elegido no tiene caracteres");

                    //UseWaitCursor = false;
                    Cursor.Current = Cursors.Arrow;
                    dGViewResultados.Enabled = true;


                    this.getAllcodes();
                }

          //  }
        
        }



        /*private void getValores_CantidadPadres(DataTable dT, int id) {
        
        }*/

        //JRM - InformeExcel - ImeApps
        /*public string getRowDgV()
        {
            return dGViewResultados.CurrentRow.Cells["emp"].Value.ToString();
        }*/

    }
}


       