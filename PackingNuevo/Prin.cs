using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using empresaGlobalProj;
using Utils;

namespace PackingNuevo
{
    public partial class Prin : Form
    {
        #region atributos de la clase
        private String CodEmp;
        private String PDA;
        private DataTable camiones = new DataTable();


        //Teclas
        //MessageWin msgWin = null;                
        #endregion


        public Prin()
        {
            InitializeComponent();
        }

        private void Prin_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            String strSql;
            System.Data.SqlClient.SqlConnection conn = new SqlConnection(Utils.CD.getConexion());

            try
            {
                this.lblPDA.Text = System.Net.Dns.GetHostName();
                //this.Text += "  v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();



                #region Leer fichero de configuracion
                DataSet set = new DataSet();


                /*String aux = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                aux = aux + "\\Config.xml";
                set.ReadXml(aux);*/

                //this.CodEmp = set.Tables["config"].Rows[0]["CodEmp"].ToString();
                this.CodEmp = empresaGlobal.empresaID;
                this.label_empresa.Text = this.CodEmp;
                //this.CadenaConexion = set.Tables["config"].Rows[0]["ConStrSQL"].ToString();

                set = null;

                this.PDA = System.Net.Dns.GetHostName();
                #endregion


                conn.Open();

                #region Obtener los operarios de almacén
                strSql = "SELECT NUMERO_PERSONAL, NOMBRE + ' ' +  APELLIDOS ";
                strSql = strSql + " FROM TC_V_OperariosAlmacen ";
                strSql = strSql + " ORDER BY NUMERO_PERSONAL ";

                System.Data.SqlClient.SqlCommand comando2 = new System.Data.SqlClient.SqlCommand(strSql, conn);
                System.Data.SqlClient.SqlDataReader reader2 = comando2.ExecuteReader();

                cmbOperario1.Items.Add("");
                cmbOperario2.Items.Add("");

                while (reader2.Read())
                {
                    cmbOperario1.Items.Add(reader2.GetValue(0).ToString() + " / " + reader2.GetValue(1).ToString());
                    cmbOperario2.Items.Add(reader2.GetValue(0).ToString() + " / " + reader2.GetValue(1).ToString());
                }

                reader2.Close();
                comando2.Dispose();
                #endregion


                #region Obtener los packings del sistema cuyos pedidos tengan alguna línea pendiente de servir
                strSql = "SELECT PACKING, PEDIDOS ";
                strSql += " FROM TC_PACKING_LIST ";
                strSql += " WHERE (CodEMP = '" + this.CodEmp + "') ";
                strSql += " 		and (SELECT count(*)  ";
                strSql += " 				FROM T_ORDTERL INNER JOIN T_ARTICULOS  ";
                strSql += " 						ON T_ORDTERL.CODEMP = T_ARTICULOS.CodEMP AND T_ORDTERL.CODIGO = T_ARTICULOS.CODIGO ";
                strSql += " 				WHERE (T_ORDTERL.CODEMP = '" + this.CodEmp + "') AND (T_ORDTERL.TIPOREG = 'CF') ";

                if (this.CodEmp != "60")
                {
                    //strSql += " and (T_ORDTERL.CTDPRE <> T_ORDTERL.CTDREAL) ";
                    //strSql += " AND (T_ORDTERL.FLAG = 0) ";
                }

                strSql += " 						and (T_ORDTERL.NUMPED in (select splitdata From dbo.SplitString_ID(TC_PACKING_LIST.PEDIDOS, ';'))) ";
                strSql += " 			) > 0 ";
                strSql += "	ORDER BY PACKING desc ";

                System.Data.SqlClient.SqlCommand comando = new System.Data.SqlClient.SqlCommand(strSql, conn);
                System.Data.SqlClient.SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    cmbPacking.Items.Add(reader.GetValue(0).ToString() + " # " + reader.GetValue(1).ToString());
                }

                reader.Close();
                comando.Dispose();
                #endregion



                /*string sql = " select	TC_PACKING_LIST_CONTENEDORES.FECHA_SALIDA,  ";
                sql += " TC_PACKING_LIST.PACKING,  ";
                sql += " TC_PACKING_LIST.CLIENTES + ' / ' + TC_PACKING_LIST.PEDIDOS AS DESCRIPCIÓN, ";
                sql += " TC_PACKING_LIST_CONTENEDORES.CONTENEDOR AS CONTENEDOR_CAMIÓN,  ";
                sql += " TC_PACKING_LIST_CONTENEDORES_BULTOS.PAQUETE,		 ";
                sql += " case (select COUNT(*) from TC_PACKING_LIST_PAQUETES_Operaciones where TC_PACKING_LIST_PAQUETES_Operaciones.CODEMP = TC_PACKING_LIST.CODEMP and TC_PACKING_LIST_PAQUETES_Operaciones.PACKING = TC_PACKING_LIST.PACKING and TC_PACKING_LIST_PAQUETES_Operaciones.PAQUETE = TC_PACKING_LIST_PAQUETES.PAQUETE and OPERAC = '900002') ";

                sql += " when 0 then NULL ";

                sql += " else 'X' ";

                sql += " end as CARGA_FICHADA, ";
                sql += " (select TC_PACKING_LIST_PAQUETES_Operaciones.FECHA from TC_PACKING_LIST_PAQUETES_Operaciones where TC_PACKING_LIST_PAQUETES_Operaciones.CODEMP = TC_PACKING_LIST.CODEMP and TC_PACKING_LIST_PAQUETES_Operaciones.PACKING = TC_PACKING_LIST.PACKING and TC_PACKING_LIST_PAQUETES_Operaciones.PAQUETE = TC_PACKING_LIST_PAQUETES.PAQUETE and OPERAC = '900002') AS FECHA_CARGA ";
                
                sql += " from    TC_PACKING_LIST inner join TC_PACKING_LIST_CONTENEDORES on ";

                sql += " TC_PACKING_LIST.CODEMP = TC_PACKING_LIST_CONTENEDORES.CODEMP and ";

                sql += " TC_PACKING_LIST.PACKING = TC_PACKING_LIST_CONTENEDORES.PACKING ";

                sql += " inner join TC_PACKING_LIST_CONTENEDORES_BULTOS on ";

                sql += " TC_PACKING_LIST_CONTENEDORES.CODEMP = TC_PACKING_LIST_CONTENEDORES_BULTOS.CODEMP AND ";

                sql += " TC_PACKING_LIST_CONTENEDORES.PACKING = TC_PACKING_LIST_CONTENEDORES_BULTOS.PACKING AND ";

                sql += "         TC_PACKING_LIST_CONTENEDORES.CONTENEDOR = TC_PACKING_LIST_CONTENEDORES_BULTOS.CONTENEDOR ";

                sql += "inner join TC_PACKING_LIST_PAQUETES on ";

                sql += "TC_PACKING_LIST_CONTENEDORES_BULTOS.CODEMP = TC_PACKING_LIST_PAQUETES.CODEMP and ";

                sql += "            TC_PACKING_LIST_CONTENEDORES_BULTOS.PACKING = TC_PACKING_LIST_PAQUETES.PACKING and ";

                sql += "TC_PACKING_LIST_CONTENEDORES_BULTOS.PAQUETE = TC_PACKING_LIST_PAQUETES.PAQUETE ";

                sql += "inner join TC_PACKING_LIST_PAQUETES_MARCAS on ";

                sql += "TC_PACKING_LIST_PAQUETES.CODEMP = TC_PACKING_LIST_PAQUETES_MARCAS.CODEMP and ";

                sql += "TC_PACKING_LIST_PAQUETES.PACKING = TC_PACKING_LIST_PAQUETES_MARCAS.PACKING and ";

                sql += "TC_PACKING_LIST_PAQUETES.PAQUETE = TC_PACKING_LIST_PAQUETES_MARCAS.PAQUETE ";
                sql += "where TC_PACKING_LIST_CONTENEDORES.CODEMP = '3' and ";

                sql += "TC_PACKING_LIST_CONTENEDORES.FECHA_SALIDA >= dateadd(DD, -1, cast(getdate() as date)) and TC_PACKING_LIST_CONTENEDORES.FECHA_SALIDA <= getdate() ";
                sql += "order by TC_PACKING_LIST_CONTENEDORES.FECHA_SALIDA,TC_PACKING_LIST_CONTENEDORES.PACKING,TC_PACKING_LIST_CONTENEDORES.CONTENEDOR, ";

                sql += "TC_PACKING_LIST_CONTENEDORES_BULTOS.PAQUETE, ";

                sql += "TC_PACKING_LIST_PAQUETES_MARCAS.MARCA,TC_PACKING_LIST_PAQUETES_MARCAS.PEDIDO,TC_PACKING_LIST_PAQUETES_MARCAS.LINEA ";
                 

                string sql = "select	TC_PACKING_LIST_CONTENEDORES.FECHA_SALIDA, ";
		        sql += "TC_PACKING_LIST.PACKING, ";
		        sql += "TC_PACKING_LIST.CLIENTES + ' / ' + TC_PACKING_LIST.PEDIDOS AS DESCRIPCIÓN, ";
		        sql += "TC_PACKING_LIST.INCREMENTO_PESO/100 AS PORCENTAJE_INCREMENTO_PESO, ";
		        sql += "TC_PACKING_LIST_CONTENEDORES.CONTENEDOR AS CONTENEDOR_CAMIÓN, ";
		        sql += "TC_PACKING_LIST_CONTENEDORES.TIPO AS TIPO_CONTENEDOR, ";
		        sql += "isnull(TC_PACKING_LIST_CONTENEDORES.NUMERO,'') + ' / ' + isnull(TC_PACKING_LIST_CONTENEDORES.PRECINTO,'') AS INFORMACIÓN_CONTENEDOR_CAMIÓN, ";
		        sql += "TC_PACKING_LIST_CONTENEDORES_BULTOS.PAQUETE,		 ";
		        sql += "case (select COUNT(*) from TC_PACKING_LIST_PAQUETES_MARCAS where TC_PACKING_LIST_PAQUETES_MARCAS.CODEMP = TC_PACKING_LIST.CODEMP and TC_PACKING_LIST_PAQUETES_MARCAS.PACKING = TC_PACKING_LIST.PACKING and TC_PACKING_LIST_PAQUETES_MARCAS.PAQUETE = TC_PACKING_LIST_PAQUETES.PAQUETE) ";
			    sql += "    when	(select COUNT(*) from TC_PACKING_LIST_PAQUETES_MARCAS where TC_PACKING_LIST_PAQUETES_MARCAS.CODEMP = TC_PACKING_LIST.CODEMP and TC_PACKING_LIST_PAQUETES_MARCAS.PACKING = TC_PACKING_LIST.PACKING and TC_PACKING_LIST_PAQUETES_MARCAS.PAQUETE = TC_PACKING_LIST_PAQUETES.PAQUETE and TC_PACKING_LIST_PAQUETES_MARCAS.CATEGORIA in ('Tornilleria','Forro')) then 'TO' ";
			    sql += "    else	case (select COUNT(*) from TC_PACKING_LIST_PAQUETES_Operaciones where TC_PACKING_LIST_PAQUETES_Operaciones.CODEMP = TC_PACKING_LIST.CODEMP and TC_PACKING_LIST_PAQUETES_Operaciones.PACKING = TC_PACKING_LIST.PACKING and TC_PACKING_LIST_PAQUETES_Operaciones.PAQUETE = TC_PACKING_LIST_PAQUETES.PAQUETE and OPERAC = '900001') ";
				sql += "		        when 0 then NULL ";
				sql += "		        else 'X' ";
				sql += "	        end ";
		        sql += "end as MONTAJE_FICHADO, ";
		        sql += "(select TC_PACKING_LIST_PAQUETES_Operaciones.FECHAC from TC_PACKING_LIST_PAQUETES_Operaciones where TC_PACKING_LIST_PAQUETES_Operaciones.CODEMP = TC_PACKING_LIST.CODEMP and TC_PACKING_LIST_PAQUETES_Operaciones.PACKING = TC_PACKING_LIST.PACKING and TC_PACKING_LIST_PAQUETES_Operaciones.PAQUETE = TC_PACKING_LIST_PAQUETES.PAQUETE and OPERAC = '900001') AS FECHA_MONTAJE,		 ";
		        sql += "(select (select NOMBRE + ' ' + APELLIDOS from CS_CPP_0713.dbo.CPP_EMPLEADOS where CS_CPP_0713.dbo.CPP_EMPLEADOS.CODEMP = TC_PACKING_LIST_PAQUETES_Operaciones.CODEMP and CS_CPP_0713.dbo.CPP_EMPLEADOS.NUMERO_PERSONAL = TC_PACKING_LIST_PAQUETES_Operaciones.NumeroPersonal) + ' (' + cast(TC_PACKING_LIST_PAQUETES_Operaciones.NumeroPersonal as varchar) + ')' from TC_PACKING_LIST_PAQUETES_Operaciones where TC_PACKING_LIST_PAQUETES_Operaciones.CODEMP = TC_PACKING_LIST.CODEMP and TC_PACKING_LIST_PAQUETES_Operaciones.PACKING = TC_PACKING_LIST.PACKING and TC_PACKING_LIST_PAQUETES_Operaciones.PAQUETE = TC_PACKING_LIST_PAQUETES.PAQUETE and OPERAC = '900001') AS OPERARIO1_MONTAJE,	 ";	
		        sql += "(select (select NOMBRE + ' ' + APELLIDOS from CS_CPP_0713.dbo.CPP_EMPLEADOS where CS_CPP_0713.dbo.CPP_EMPLEADOS.CODEMP = TC_PACKING_LIST_PAQUETES_Operaciones.CODEMP and CS_CPP_0713.dbo.CPP_EMPLEADOS.NUMERO_PERSONAL = TC_PACKING_LIST_PAQUETES_Operaciones.NumeroPersonal2) + ' (' + cast(TC_PACKING_LIST_PAQUETES_Operaciones.NumeroPersonal2 as varchar) + ')' from TC_PACKING_LIST_PAQUETES_Operaciones where TC_PACKING_LIST_PAQUETES_Operaciones.CODEMP = TC_PACKING_LIST.CODEMP and TC_PACKING_LIST_PAQUETES_Operaciones.PACKING = TC_PACKING_LIST.PACKING and TC_PACKING_LIST_PAQUETES_Operaciones.PAQUETE = TC_PACKING_LIST_PAQUETES.PAQUETE and OPERAC = '900001') AS OPERARIO2_MONTAJE,	 ";							
		        sql += "case (select COUNT(*) from TC_PACKING_LIST_PAQUETES_Operaciones where TC_PACKING_LIST_PAQUETES_Operaciones.CODEMP = TC_PACKING_LIST.CODEMP and TC_PACKING_LIST_PAQUETES_Operaciones.PACKING = TC_PACKING_LIST.PACKING and TC_PACKING_LIST_PAQUETES_Operaciones.PAQUETE = TC_PACKING_LIST_PAQUETES.PAQUETE and OPERAC = '900002') ";
			    sql += "    when 0 then NULL ";
			    sql += "    else 'X'			 ";
		        sql += "end as CARGA_FICHADA, ";
		        sql += "(select TC_PACKING_LIST_PAQUETES_Operaciones.FECHA from TC_PACKING_LIST_PAQUETES_Operaciones where TC_PACKING_LIST_PAQUETES_Operaciones.CODEMP = TC_PACKING_LIST.CODEMP and TC_PACKING_LIST_PAQUETES_Operaciones.PACKING = TC_PACKING_LIST.PACKING and TC_PACKING_LIST_PAQUETES_Operaciones.PAQUETE = TC_PACKING_LIST_PAQUETES.PAQUETE and OPERAC = '900002') AS FECHA_CARGA,		 ";
		        sql += "(select (select NOMBRE + ' ' + APELLIDOS from CS_CPP_0713.dbo.CPP_EMPLEADOS where CS_CPP_0713.dbo.CPP_EMPLEADOS.CODEMP = TC_PACKING_LIST_PAQUETES_Operaciones.CODEMP and CS_CPP_0713.dbo.CPP_EMPLEADOS.NUMERO_PERSONAL = TC_PACKING_LIST_PAQUETES_Operaciones.NumeroPersonal) + ' (' + cast(TC_PACKING_LIST_PAQUETES_Operaciones.NumeroPersonal as varchar) + ')' from TC_PACKING_LIST_PAQUETES_Operaciones where TC_PACKING_LIST_PAQUETES_Operaciones.CODEMP = TC_PACKING_LIST.CODEMP and TC_PACKING_LIST_PAQUETES_Operaciones.PACKING = TC_PACKING_LIST.PACKING and TC_PACKING_LIST_PAQUETES_Operaciones.PAQUETE = TC_PACKING_LIST_PAQUETES.PAQUETE and OPERAC = '900002') AS OPERARIO1_CARGA,		 ";
		        sql += "(select (select NOMBRE + ' ' + APELLIDOS from CS_CPP_0713.dbo.CPP_EMPLEADOS where CS_CPP_0713.dbo.CPP_EMPLEADOS.CODEMP = TC_PACKING_LIST_PAQUETES_Operaciones.CODEMP and CS_CPP_0713.dbo.CPP_EMPLEADOS.NUMERO_PERSONAL = TC_PACKING_LIST_PAQUETES_Operaciones.NumeroPersonal2) + ' (' + cast(TC_PACKING_LIST_PAQUETES_Operaciones.NumeroPersonal2 as varchar) + ')' from TC_PACKING_LIST_PAQUETES_Operaciones where TC_PACKING_LIST_PAQUETES_Operaciones.CODEMP = TC_PACKING_LIST.CODEMP and TC_PACKING_LIST_PAQUETES_Operaciones.PACKING = TC_PACKING_LIST.PACKING and TC_PACKING_LIST_PAQUETES_Operaciones.PAQUETE = TC_PACKING_LIST_PAQUETES.PAQUETE and OPERAC = '900002') AS OPERARIO2_CARGA,		 ";
		        sql += "TC_PACKING_LIST_PAQUETES_MARCAS.MARCA, ";
		        sql += "TC_PACKING_LIST_PAQUETES_MARCAS.CANTIDAD, ";
		        sql += "TC_PACKING_LIST_PAQUETES_MARCAS.PESO AS PESO_TEÓRICO, ";
		        sql += "TC_PACKING_LIST_PAQUETES_MARCAS.PESO + (TC_PACKING_LIST_PAQUETES_MARCAS.PESO * (TC_PACKING_LIST.INCREMENTO_PESO/100)) as PESO_INCREMENTADO, ";
		        sql += "TC_PACKING_LIST_PAQUETES_MARCAS.CATEGORIA AS CATEGORÍA, ";
		        sql += "TC_PACKING_LIST_PAQUETES_MARCAS.MP, ";
		        sql += "TC_PACKING_LIST_PAQUETES_MARCAS.LONGITUD,		 ";
		        sql += "TC_PACKING_LIST_PAQUETES_MARCAS.PEDIDO, ";
		        sql += "TC_PACKING_LIST_PAQUETES_MARCAS.LINEA AS LÍNEA, ";
		        sql += "TC_PACKING_LIST_PAQUETES_MARCAS.CODIGO AS CÓDIGO, ";
		        sql += "TC_PACKING_LIST_PAQUETES_MARCAS.ITEM, ";
		        sql += "TC_PACKING_LIST_PAQUETES_MARCAS.NITEM, ";
		        sql += "TC_PACKING_LIST_PAQUETES_MARCAS.DENOMINACION ";				
                sql += "from	TC_PACKING_LIST inner join TC_PACKING_LIST_CONTENEDORES on ";
			    sql += "    TC_PACKING_LIST.CODEMP = TC_PACKING_LIST_CONTENEDORES.CODEMP and ";
			    sql += "    TC_PACKING_LIST.PACKING = TC_PACKING_LIST_CONTENEDORES.PACKING ";
			    sql += "    inner join TC_PACKING_LIST_CONTENEDORES_BULTOS on ";
				sql += "	        TC_PACKING_LIST_CONTENEDORES.CODEMP = TC_PACKING_LIST_CONTENEDORES_BULTOS.CODEMP AND ";
				sql += "        TC_PACKING_LIST_CONTENEDORES.PACKING = TC_PACKING_LIST_CONTENEDORES_BULTOS.PACKING AND ";
				sql += "        TC_PACKING_LIST_CONTENEDORES.CONTENEDOR = TC_PACKING_LIST_CONTENEDORES_BULTOS.CONTENEDOR ";
				sql += "        inner join TC_PACKING_LIST_PAQUETES on ";
				sql += "	        TC_PACKING_LIST_CONTENEDORES_BULTOS.CODEMP = TC_PACKING_LIST_PAQUETES.CODEMP and ";
				sql += "	        TC_PACKING_LIST_CONTENEDORES_BULTOS.PACKING = TC_PACKING_LIST_PAQUETES.PACKING and ";
				sql += "	        TC_PACKING_LIST_CONTENEDORES_BULTOS.PAQUETE = TC_PACKING_LIST_PAQUETES.PAQUETE ";
				sql += "	        inner join TC_PACKING_LIST_PAQUETES_MARCAS on ";
				sql += "		        TC_PACKING_LIST_PAQUETES.CODEMP = TC_PACKING_LIST_PAQUETES_MARCAS.CODEMP and ";
				sql += "		        TC_PACKING_LIST_PAQUETES.PACKING = TC_PACKING_LIST_PAQUETES_MARCAS.PACKING and ";
				sql += "		        TC_PACKING_LIST_PAQUETES.PAQUETE = TC_PACKING_LIST_PAQUETES_MARCAS.PAQUETE ";
                sql += "where	TC_PACKING_LIST_CONTENEDORES.CODEMP = 3 and  ";
		        sql += "    TC_PACKING_LIST_CONTENEDORES.FECHA_SALIDA >= DATEADD(DAY, -1, GETDATE()) and TC_PACKING_LIST_CONTENEDORES.FECHA_SALIDA <= GETDATE() ";
                sql += "order by TC_PACKING_LIST_CONTENEDORES.FECHA_SALIDA,TC_PACKING_LIST_CONTENEDORES.PACKING,TC_PACKING_LIST_CONTENEDORES.CONTENEDOR, ";
		        sql += "    TC_PACKING_LIST_CONTENEDORES_BULTOS.PAQUETE, ";
                sql += "TC_PACKING_LIST_PAQUETES_MARCAS.MARCA,TC_PACKING_LIST_PAQUETES_MARCAS.PEDIDO,TC_PACKING_LIST_PAQUETES_MARCAS.LINEA ";


                foreach (DataGridViewColumn c in dataGridView1.Columns)
                {
                    c.DefaultCellStyle.Font = new Font("Arial", 10, GraphicsUnit.Pixel);
                }

                SqlCommand command = new SqlCommand(sql, conn);

                //SqlDataReader reader = sqlCmd.ExecuteReader();
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(camiones);
                 */


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Iniciar la aplicacion. Avise a su administrador: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                Close();
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
            Cursor.Current = Cursors.Default;
        }

        private void cargarTabla()
        {
            Cursor.Current = Cursors.WaitCursor;

            System.Data.SqlClient.SqlConnection conn = new SqlConnection(Utils.CD.getConexion());

            string sql = "select    distinct	convert(varchar, TC_PACKING_LIST_CONTENEDORES.FECHA_SALIDA, 103) AS FECHA, ";
            sql += "TC_PACKING_LIST.PACKING, ";
            sql += "TC_PACKING_LIST.CLIENTES + ' / ' + TC_PACKING_LIST.PEDIDOS AS DESCRIPCIÓN, ";
            //sql += "TC_PACKING_LIST.INCREMENTO_PESO/100 AS PORCENTAJE_INCREMENTO_PESO, ";
            //sql += "TC_PACKING_LIST_CONTENEDORES.CONTENEDOR AS CONTENEDOR_CAMIÓN, ";
            //sql += "TC_PACKING_LIST_CONTENEDORES.TIPO AS TIPO_CONTENEDOR, ";
            //sql += "isnull(TC_PACKING_LIST_CONTENEDORES.NUMERO,'') + ' / ' + isnull(TC_PACKING_LIST_CONTENEDORES.PRECINTO,'') AS INFORMACIÓN_CONTENEDOR_CAMIÓN, ";
            sql += "TC_PACKING_LIST_CONTENEDORES_BULTOS.PAQUETE,		 ";
            sql += "case (select COUNT(*) from TC_PACKING_LIST_PAQUETES_MARCAS where TC_PACKING_LIST_PAQUETES_MARCAS.CODEMP = TC_PACKING_LIST.CODEMP and TC_PACKING_LIST_PAQUETES_MARCAS.PACKING = TC_PACKING_LIST.PACKING and TC_PACKING_LIST_PAQUETES_MARCAS.PAQUETE = TC_PACKING_LIST_PAQUETES.PAQUETE) ";
            sql += "    when	(select COUNT(*) from TC_PACKING_LIST_PAQUETES_MARCAS where TC_PACKING_LIST_PAQUETES_MARCAS.CODEMP = TC_PACKING_LIST.CODEMP and TC_PACKING_LIST_PAQUETES_MARCAS.PACKING = TC_PACKING_LIST.PACKING and TC_PACKING_LIST_PAQUETES_MARCAS.PAQUETE = TC_PACKING_LIST_PAQUETES.PAQUETE and TC_PACKING_LIST_PAQUETES_MARCAS.CATEGORIA in ('Tornilleria','Forro')) then 'TO' ";
            sql += "    else	case (select COUNT(*) from TC_PACKING_LIST_PAQUETES_Operaciones where TC_PACKING_LIST_PAQUETES_Operaciones.CODEMP = TC_PACKING_LIST.CODEMP and TC_PACKING_LIST_PAQUETES_Operaciones.PACKING = TC_PACKING_LIST.PACKING and TC_PACKING_LIST_PAQUETES_Operaciones.PAQUETE = TC_PACKING_LIST_PAQUETES.PAQUETE and OPERAC = '900001') ";
            sql += "		        when 0 then NULL ";
            sql += "		        else 'X' ";
            sql += "	        end ";
            sql += "end as MONTAJE_FICHADO, ";
            sql += "(select TC_PACKING_LIST_PAQUETES_Operaciones.FECHAC from TC_PACKING_LIST_PAQUETES_Operaciones where TC_PACKING_LIST_PAQUETES_Operaciones.CODEMP = TC_PACKING_LIST.CODEMP and TC_PACKING_LIST_PAQUETES_Operaciones.PACKING = TC_PACKING_LIST.PACKING and TC_PACKING_LIST_PAQUETES_Operaciones.PAQUETE = TC_PACKING_LIST_PAQUETES.PAQUETE and OPERAC = '900001') AS FECHA_MONTAJE,		 ";
            //sql += "(select (select NOMBRE + ' ' + APELLIDOS from CS_CPP_0713.dbo.CPP_EMPLEADOS where CS_CPP_0713.dbo.CPP_EMPLEADOS.CODEMP = TC_PACKING_LIST_PAQUETES_Operaciones.CODEMP and CS_CPP_0713.dbo.CPP_EMPLEADOS.NUMERO_PERSONAL = TC_PACKING_LIST_PAQUETES_Operaciones.NumeroPersonal) + ' (' + cast(TC_PACKING_LIST_PAQUETES_Operaciones.NumeroPersonal as varchar) + ')' from TC_PACKING_LIST_PAQUETES_Operaciones where TC_PACKING_LIST_PAQUETES_Operaciones.CODEMP = TC_PACKING_LIST.CODEMP and TC_PACKING_LIST_PAQUETES_Operaciones.PACKING = TC_PACKING_LIST.PACKING and TC_PACKING_LIST_PAQUETES_Operaciones.PAQUETE = TC_PACKING_LIST_PAQUETES.PAQUETE and OPERAC = '900001') AS OPERARIO1_MONTAJE,	 ";
            //sql += "(select (select NOMBRE + ' ' + APELLIDOS from CS_CPP_0713.dbo.CPP_EMPLEADOS where CS_CPP_0713.dbo.CPP_EMPLEADOS.CODEMP = TC_PACKING_LIST_PAQUETES_Operaciones.CODEMP and CS_CPP_0713.dbo.CPP_EMPLEADOS.NUMERO_PERSONAL = TC_PACKING_LIST_PAQUETES_Operaciones.NumeroPersonal2) + ' (' + cast(TC_PACKING_LIST_PAQUETES_Operaciones.NumeroPersonal2 as varchar) + ')' from TC_PACKING_LIST_PAQUETES_Operaciones where TC_PACKING_LIST_PAQUETES_Operaciones.CODEMP = TC_PACKING_LIST.CODEMP and TC_PACKING_LIST_PAQUETES_Operaciones.PACKING = TC_PACKING_LIST.PACKING and TC_PACKING_LIST_PAQUETES_Operaciones.PAQUETE = TC_PACKING_LIST_PAQUETES.PAQUETE and OPERAC = '900001') AS OPERARIO2_MONTAJE,	 ";
            sql += "case (select COUNT(*) from TC_PACKING_LIST_PAQUETES_Operaciones where TC_PACKING_LIST_PAQUETES_Operaciones.CODEMP = TC_PACKING_LIST.CODEMP and TC_PACKING_LIST_PAQUETES_Operaciones.PACKING = TC_PACKING_LIST.PACKING and TC_PACKING_LIST_PAQUETES_Operaciones.PAQUETE = TC_PACKING_LIST_PAQUETES.PAQUETE and OPERAC = '900002') ";
            sql += "    when 0 then NULL ";
            sql += "    else 'X'			 ";
            sql += "end as CARGA_FICHADA, ";
            sql += "(select TC_PACKING_LIST_PAQUETES_Operaciones.FECHA from TC_PACKING_LIST_PAQUETES_Operaciones where TC_PACKING_LIST_PAQUETES_Operaciones.CODEMP = TC_PACKING_LIST.CODEMP and TC_PACKING_LIST_PAQUETES_Operaciones.PACKING = TC_PACKING_LIST.PACKING and TC_PACKING_LIST_PAQUETES_Operaciones.PAQUETE = TC_PACKING_LIST_PAQUETES.PAQUETE and OPERAC = '900002') AS FECHA_CARGA,		 ";
            //sql += "(select (select NOMBRE + ' ' + APELLIDOS from CS_CPP_0713.dbo.CPP_EMPLEADOS where CS_CPP_0713.dbo.CPP_EMPLEADOS.CODEMP = TC_PACKING_LIST_PAQUETES_Operaciones.CODEMP and CS_CPP_0713.dbo.CPP_EMPLEADOS.NUMERO_PERSONAL = TC_PACKING_LIST_PAQUETES_Operaciones.NumeroPersonal) + ' (' + cast(TC_PACKING_LIST_PAQUETES_Operaciones.NumeroPersonal as varchar) + ')' from TC_PACKING_LIST_PAQUETES_Operaciones where TC_PACKING_LIST_PAQUETES_Operaciones.CODEMP = TC_PACKING_LIST.CODEMP and TC_PACKING_LIST_PAQUETES_Operaciones.PACKING = TC_PACKING_LIST.PACKING and TC_PACKING_LIST_PAQUETES_Operaciones.PAQUETE = TC_PACKING_LIST_PAQUETES.PAQUETE and OPERAC = '900002') AS OPERARIO1_CARGA,		 ";
            //sql += "(select (select NOMBRE + ' ' + APELLIDOS from CS_CPP_0713.dbo.CPP_EMPLEADOS where CS_CPP_0713.dbo.CPP_EMPLEADOS.CODEMP = TC_PACKING_LIST_PAQUETES_Operaciones.CODEMP and CS_CPP_0713.dbo.CPP_EMPLEADOS.NUMERO_PERSONAL = TC_PACKING_LIST_PAQUETES_Operaciones.NumeroPersonal2) + ' (' + cast(TC_PACKING_LIST_PAQUETES_Operaciones.NumeroPersonal2 as varchar) + ')' from TC_PACKING_LIST_PAQUETES_Operaciones where TC_PACKING_LIST_PAQUETES_Operaciones.CODEMP = TC_PACKING_LIST.CODEMP and TC_PACKING_LIST_PAQUETES_Operaciones.PACKING = TC_PACKING_LIST.PACKING and TC_PACKING_LIST_PAQUETES_Operaciones.PAQUETE = TC_PACKING_LIST_PAQUETES.PAQUETE and OPERAC = '900002') AS OPERARIO2_CARGA,		 ";
            //sql += "TC_PACKING_LIST_PAQUETES_MARCAS.MARCA, ";
            //sql += "TC_PACKING_LIST_PAQUETES_MARCAS.CANTIDAD, ";
            //sql += "TC_PACKING_LIST_PAQUETES_MARCAS.PESO AS PESO_TEÓRICO, ";
            //sql += "TC_PACKING_LIST_PAQUETES_MARCAS.PESO + (TC_PACKING_LIST_PAQUETES_MARCAS.PESO * (TC_PACKING_LIST.INCREMENTO_PESO/100)) as PESO_INCREMENTADO, ";
            //sql += "TC_PACKING_LIST_PAQUETES_MARCAS.CATEGORIA AS CATEGORÍA, ";
            //sql += "TC_PACKING_LIST_PAQUETES_MARCAS.MP, ";
            //sql += "TC_PACKING_LIST_PAQUETES_MARCAS.LONGITUD,		 ";
            sql += "TC_PACKING_LIST_PAQUETES_MARCAS.PEDIDO ";
            //sql += "TC_PACKING_LIST_PAQUETES_MARCAS.LINEA AS LÍNEA, ";
            //sql += "TC_PACKING_LIST_PAQUETES_MARCAS.CODIGO AS CÓDIGO, ";
            //sql += "TC_PACKING_LIST_PAQUETES_MARCAS.ITEM, ";
            //sql += "TC_PACKING_LIST_PAQUETES_MARCAS.NITEM, ";
            //sql += "TC_PACKING_LIST_PAQUETES_MARCAS.DENOMINACION ";
            sql += "from	TC_PACKING_LIST inner join TC_PACKING_LIST_CONTENEDORES on ";
            sql += "    TC_PACKING_LIST.CODEMP = TC_PACKING_LIST_CONTENEDORES.CODEMP and ";
            sql += "    TC_PACKING_LIST.PACKING = TC_PACKING_LIST_CONTENEDORES.PACKING ";
            sql += "    inner join TC_PACKING_LIST_CONTENEDORES_BULTOS on ";
            sql += "	        TC_PACKING_LIST_CONTENEDORES.CODEMP = TC_PACKING_LIST_CONTENEDORES_BULTOS.CODEMP AND ";
            sql += "        TC_PACKING_LIST_CONTENEDORES.PACKING = TC_PACKING_LIST_CONTENEDORES_BULTOS.PACKING AND ";
            sql += "        TC_PACKING_LIST_CONTENEDORES.CONTENEDOR = TC_PACKING_LIST_CONTENEDORES_BULTOS.CONTENEDOR ";
            sql += "        inner join TC_PACKING_LIST_PAQUETES on ";
            sql += "	        TC_PACKING_LIST_CONTENEDORES_BULTOS.CODEMP = TC_PACKING_LIST_PAQUETES.CODEMP and ";
            sql += "	        TC_PACKING_LIST_CONTENEDORES_BULTOS.PACKING = TC_PACKING_LIST_PAQUETES.PACKING and ";
            sql += "	        TC_PACKING_LIST_CONTENEDORES_BULTOS.PAQUETE = TC_PACKING_LIST_PAQUETES.PAQUETE ";
            sql += "	        inner join TC_PACKING_LIST_PAQUETES_MARCAS on ";
            sql += "		        TC_PACKING_LIST_PAQUETES.CODEMP = TC_PACKING_LIST_PAQUETES_MARCAS.CODEMP and ";
            sql += "		        TC_PACKING_LIST_PAQUETES.PACKING = TC_PACKING_LIST_PAQUETES_MARCAS.PACKING and ";
            sql += "		        TC_PACKING_LIST_PAQUETES.PAQUETE = TC_PACKING_LIST_PAQUETES_MARCAS.PAQUETE ";
            sql += "where	TC_PACKING_LIST_CONTENEDORES.CODEMP = 3 and  ";
            sql += "    TC_PACKING_LIST_CONTENEDORES.FECHA_SALIDA >= DATEADD(DAY, -1, GETDATE()) and TC_PACKING_LIST_CONTENEDORES.FECHA_SALIDA <= GETDATE() ";
            sql += "order by FECHA,TC_PACKING_LIST.PACKING, ";
            sql += "    TC_PACKING_LIST_CONTENEDORES_BULTOS.PAQUETE";

            

            SqlCommand command = new SqlCommand(sql, conn);

            //SqlDataReader reader = sqlCmd.ExecuteReader();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(camiones);

            this.dataGridView1.DataSource = camiones;

            string valorCelda = "";

            foreach (DataGridViewColumn c in dataGridView1.Columns)
            {
                //c.DefaultCellStyle.Font = new Font("Arial", 10, GraphicsUnit.Pixel);

                if (c.Name == "CARGA_FICHADA" || c.Name == "MONTAJE_FICHADO")
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[c.Index].Value.ToString() == "X" || row.Cells[c.Index].Value.ToString() == "TO")
                        {
                            row.Cells[c.Index].Style.BackColor = Color.Lime;
                        }
                        else
                        {
                            row.Cells[c.Index].Style.BackColor = Color.Red;
                        }
                    }
                }

                /*foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[c.Index].Value.ToString() == valorCelda)
                    {
                        row.Cells[c.Index].Style.ForeColor = Color.White;
                    }
                    else
                    {
                        valorCelda = row.Cells[c.Index].Value.ToString();
                    }
                }*/
            }

            
            

            Cursor.Current = Cursors.Default;
        }




        private void btnMontaje_Click(object sender, EventArgs e)
        {
            String operacion = "900001";
            String puesto = "900";

            this.abrirPantalla(operacion, puesto);
        }

        private void btnCarga_Click(object sender, EventArgs e)
        {
            String operacion = "900002";
            String puesto = "901";

            this.abrirPantalla(operacion, puesto);
        }

        private void abrirPantalla(String operacion, String puesto)
        {
            Int32 packing;
            Int32 operario1;
            String operario2;


            if (this.cmbPacking.Text == String.Empty)
            {
                MessageBox.Show("Debe elegir un packing");
                return;
            }
            else
            {
                string[] aux = this.cmbPacking.Text.Split('#');
                packing = Convert.ToInt32(aux[0].Trim());
            }

            if (this.cmbOperario1.Text == String.Empty)
            {
                MessageBox.Show("Debe elegir el operario");
                return;
            }
            else
            {
                string[] aux2 = this.cmbOperario1.Text.Split('/');
                operario1 = Convert.ToInt32(aux2[0].Trim());
            }

            if (this.cmbOperario2.Text == String.Empty)
            {
                operario2 = "";
            }
            else
            {
                string[] aux3 = this.cmbOperario2.Text.Split('/');
                operario2 = aux3[0].Trim();
            }


            frmPaquetes paquetes = new frmPaquetes(this.PDA, operario1, operario2, packing, operacion, puesto);
            paquetes.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.panel1.Visible = true;
            cargarTabla();
            Cursor.Current = Cursors.Default;
        }

        private void btnCerrar_Click_1(object sender, EventArgs e)
        {
            this.panel1.Visible = false;
        }

        private void btnIzda_Click(object sender, EventArgs e)
        {
            if (dataGridView1.FirstDisplayedScrollingColumnIndex != 0) dataGridView1.FirstDisplayedScrollingColumnIndex = dataGridView1.FirstDisplayedScrollingColumnIndex - 1;
        }

        private void btnDcha_Click(object sender, EventArgs e)
        {
            if (dataGridView1.FirstDisplayedScrollingColumnIndex != dataGridView1.Columns.Count) dataGridView1.FirstDisplayedScrollingColumnIndex = dataGridView1.FirstDisplayedScrollingColumnIndex + 1;
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (dataGridView1.FirstDisplayedScrollingRowIndex != dataGridView1.Rows.Count) dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.FirstDisplayedScrollingRowIndex + 5;
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (dataGridView1.FirstDisplayedScrollingRowIndex != 0)
            {
                if ((dataGridView1.FirstDisplayedScrollingRowIndex - 5) > 0)
                    dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.FirstDisplayedScrollingRowIndex - 5;
                else
                    dataGridView1.FirstDisplayedScrollingRowIndex = 0;
            }
        }
    }
}
