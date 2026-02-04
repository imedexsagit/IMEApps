using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace ReservaComprasMP
{
    class ReservaComprasMP_BD
    {
        
        
        
        //Métodos
        public DataTable ObtenerProveedoresClientes(string empresa,string tipo)
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

                strsql = "";
                if (tipo == "proveedor")
                {
                    strsql = strsql + " SELECT  CODCLI,NOMBRE + ' (' + CODCLI + ')' AS NOMBRE";
                    strsql = strsql + " FROM    T_PROV";
                    strsql = strsql + " WHERE   CODEMP = '" + empresa + "' AND";
                    strsql = strsql + "         CNAE = 'Reserva'";
                    strsql = strsql + " ORDER BY NOMBRE";
                }
                
                if(tipo == "cliente")
                {
                    strsql = strsql + " SELECT  CODCLI,NOMBRE + ' (' + CODCLI + ')' AS NOMBRE";
                    strsql = strsql + " FROM    T_CLIENTES";
                    strsql = strsql + " WHERE   CODEMP = '" + empresa + "'";                    
                    strsql = strsql + " ORDER BY NOMBRE";
                }

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


        
        public DataTable ObtenerPedidosProveedorCliente(string empresa,string tipo,string proveedor_cliente,bool cerradas)
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

                strsql = "";
                if (tipo == "proveedor")
                {
                    strsql = strsql + " SELECT  CAST(NUMPED AS VARCHAR) AS NUMPED";
                    strsql = strsql + " FROM    T_ORDTER";
                    strsql = strsql + " WHERE   CODEMP = '" + empresa + "' AND";
                    strsql = strsql + "         TIPOREG = 'SF' AND";
                    if(proveedor_cliente != "-1")
                        strsql = strsql + "         TERCERO = '" + proveedor_cliente + "' AND";
                    if(cerradas)
                        strsql = strsql + "     (SELECT COUNT(*) FROM T_ORDTERL WHERE T_ORDTERL.CODEMP = T_ORDTER.CODEMP AND T_ORDTERL.TIPOREG = T_ORDTER.TIPOREG AND T_ORDTERL.NUMPED = T_ORDTER.NUMPED AND dbo.IME_GetCategoria_Articulo(T_ORDTERL.CODEMP,T_ORDTERL.CODIGO) in ('M1','M4','M5')) > 0";
                    else
                        strsql = strsql + "     (SELECT COUNT(*) FROM T_ORDTERL WHERE T_ORDTERL.CODEMP = T_ORDTER.CODEMP AND T_ORDTERL.TIPOREG = T_ORDTER.TIPOREG AND T_ORDTERL.NUMPED = T_ORDTER.NUMPED AND dbo.IME_GetCategoria_Articulo(T_ORDTERL.CODEMP,T_ORDTERL.CODIGO) in ('M1','M4','M5') and T_ORDTERL.FLAG = '0') > 0";
                    strsql = strsql + " ORDER BY NUMPED DESC";
                }

                if (tipo == "cliente")
                {
                    strsql = strsql + " SELECT  CAST(NUMPED AS VARCHAR) AS NUMPED";
                    strsql = strsql + " FROM    T_ORDTER";
                    strsql = strsql + " WHERE   CODEMP = '" + empresa + "' AND";
                    strsql = strsql + "         TIPOREG = 'CF' AND";
                    if (proveedor_cliente != "-1")
                        strsql = strsql + "         TERCERO = '" + proveedor_cliente + "' AND";
                    if(cerradas)
                        strsql = strsql + "         (SELECT COUNT(*) FROM T_ORDTERL WHERE T_ORDTERL.CODEMP = T_ORDTER.CODEMP AND T_ORDTERL.TIPOREG = T_ORDTER.TIPOREG AND T_ORDTERL.NUMPED = T_ORDTER.NUMPED AND ((T_ORDTERL.CODIGO LIKE 'DESARROLLO-%') OR (ISNULL(dbo.IME_GetCategoria_Articulo(T_ORDTERL.CODEMP,T_ORDTERL.CODIGO),'TO') <> 'TO'))) > 0";
                    else
                        strsql = strsql + "         (SELECT COUNT(*) FROM T_ORDTERL WHERE T_ORDTERL.CODEMP = T_ORDTER.CODEMP AND T_ORDTERL.TIPOREG = T_ORDTER.TIPOREG AND T_ORDTERL.NUMPED = T_ORDTER.NUMPED AND ((T_ORDTERL.CODIGO LIKE 'DESARROLLO-%') OR (ISNULL(dbo.IME_GetCategoria_Articulo(T_ORDTERL.CODEMP,T_ORDTERL.CODIGO),'TO') <> 'TO')) AND T_ORDTERL.FLAG = '0') > 0";
                    strsql = strsql + " ORDER BY NUMPED DESC";
                }

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




        public DataTable ObtenerLineasPedido(string empresa, string tipo, string pedido, bool cerradas, List<string> lista,string mp)
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;
            string pedidos_lineas;

            try
            {
                                               
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();

                strsql = "";
                if (tipo == "proveedor")
                {
                    strsql = strsql + " SELECT  NUMPED AS PEDIDO,LINEA as LÍNEA,CODIGO as CÓDIGO,DENOMINACION as DENOMINACIÓN,CTDUP AS PEDIDA,CTDUPREAL AS SERVIDA,FFINAL AS ENTREGA,CASE FLAG WHEN '0' THEN 'SÍ' ELSE 'NO' END AS ABIERTA, 's' AS existe";
                    strsql = strsql + " FROM    T_ORDTERL";
                    strsql = strsql + " WHERE   CODEMP = '" + empresa + "' AND";
                    strsql = strsql + "         TIPOREG = 'sf' AND";
                    if((pedido != "-1") || (pedido == "-1" && mp == ""))
                        strsql = strsql + "         NUMPED = " + pedido + " AND";
                    strsql = strsql + "         (SELECT COUNT(*) FROM T_ARTICULOS WHERE T_ARTICULOS.CODEMP = T_ORDTERL.CODEMP AND T_ARTICULOS.CODIGO = T_ORDTERL.CODIGO AND T_ARTICULOS.FAMILIA = 'MP' AND T_ARTICULOS.CATEGORIA in ('M1','M4','M5')) > 0";
                    if(!cerradas)
                        strsql = strsql + "     AND FLAG = '0'";
                    if(mp != "")
                        strsql = strsql + "     AND CODIGO = '" + mp + "'";

                    if((pedido != "-1") && (mp == ""))
                    {
                        strsql = strsql + " UNION";
                        strsql = strsql + " SELECT  PEDIDO_COMPRA,LINEA_COMPRA,CODIGO_COMPRA,DENOMINACION_COMPRA,CANTIDAD_COMPRA,-1,FECHA_COMPRA,'-','n' AS existe";
                        strsql = strsql + " FROM    TC_RESERVAS_MP_CopiaPedidosCompra";
                        strsql = strsql + " WHERE   CODEMP = '" + empresa + "' AND";
                        strsql = strsql + "         PEDIDO_COMPRA = " + pedido + " AND";
                        strsql = strsql + "         LINEA_COMPRA NOT IN";
                        strsql = strsql + "                              (SELECT    LINEA";
                        strsql = strsql + "                               FROM      T_ORDTERL";
                        strsql = strsql + "                               WHERE     CODEMP = '" + empresa + "' AND";
                        strsql = strsql + "                                         TIPOREG = 'sf' AND";
                        strsql = strsql + "                                         NUMPED = " + pedido;
                        strsql = strsql + "                               )";
                    }

                    strsql = strsql + " ORDER BY NUMPED,LINEA";
                }

                if (tipo == "cliente") 
                {
                    if (lista.Count == 0)
                        pedidos_lineas = "''";
                    else
                    {
                        pedidos_lineas = "";
                        for (int i = 0; i < lista.Count; i++)
                        {
                            if (pedidos_lineas == "")
                                pedidos_lineas = "'" + lista[i] + "'";
                            else
                                pedidos_lineas = pedidos_lineas + ",'" + lista[i] + "'";
                        }
                    }

                    strsql = strsql + " SELECT  NUMPED AS PEDIDO,LINEA as LÍNEA,CODIGO as CÓDIGO,DENOMINACION as DENOMINACIÓN,CTDUP AS PEDIDA,CTDUPREAL AS SERVIDA,FFINAL AS ENTREGA,CASE FLAG WHEN '0' THEN 'SÍ' ELSE 'NO' END AS ABIERTA";
                    strsql = strsql + " FROM    T_ORDTERL";
                    strsql = strsql + " WHERE   CODEMP = '" + empresa + "' AND";
                    strsql = strsql + "         TIPOREG = 'cf' AND";
                    strsql = strsql + "         NUMPED = " + pedido + " AND";
                    strsql = strsql + "         ((CODIGO LIKE 'DESARROLLO-%') OR (SELECT COUNT(*) FROM T_ARTICULOS WHERE T_ARTICULOS.CODEMP = T_ORDTERL.CODEMP AND T_ARTICULOS.CODIGO = T_ORDTERL.CODIGO AND T_ARTICULOS.FAMILIA <> 'TO' AND T_ARTICULOS.CATEGORIA <> 'TO') > 0) AND";
                    strsql = strsql + "         (SELECT COUNT(*) FROM TC_RESERVAS_MP WHERE TC_RESERVAS_MP.CODEMP = T_ORDTERL.CODEMP AND TC_RESERVAS_MP.PEDIDO_VENTA = T_ORDTERL.NUMPED AND TC_RESERVAS_MP.LINEA_VENTA = T_ORDTERL.LINEA AND cast(TC_RESERVAS_MP.PEDIDO_COMPRA as varchar) + '-' + cast(TC_RESERVAS_MP.LINEA_COMPRA as varchar) in (" + pedidos_lineas + ")) = 0";                    
                    if (!cerradas)
                        strsql = strsql + "     AND FLAG = '0'";
                    strsql = strsql + " ORDER BY LINEA";
                }

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



        public DataTable ObtenerLotesLineaPedido(string empresa, List<string> lista)
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;
            string pedidos_lineas;

            try
            {

                if (lista.Count == 0)
                    pedidos_lineas = "''";
                else
                {
                    pedidos_lineas = "";
                    for (int i = 0; i < lista.Count; i++)
                    {
                        if (pedidos_lineas == "")
                            pedidos_lineas = "'" + lista[i] + "'";
                        else
                            pedidos_lineas = pedidos_lineas + ",'" + lista[i] + "'";
                    }
                }
                                    
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();
                
                strsql = "";
                strsql = strsql + " select	TC_PREALBARANL.NUMPED AS PEDIDO,TC_PREALBARANL.LINPED AS LÍNEA,TC_PREALBARANL.NALBARAN AS ALB_PROV, TC_PREALBARAN.ALBARAN_IR AS ALB_IRATI,TC_PREALBARANL.NPAQUETE AS PAQUETE,"; 
		        strsql = strsql + "         TC_PREALBARANL.PESO AS PESO_PREV,TC_PREALBARAN_LOTES.LOTE,TC_PREALBARAN_LOTES.SITUACION AS SITUACIÓN,";
		        strsql = strsql + "         TC_PREALBARAN_LOTES.NUMERO_BARRAS AS BARRAS,T_STOCKS.STOCK";
                strsql = strsql + " from	TC_PREALBARANL INNER JOIN TC_PREALBARAN ON";
			    strsql = strsql + "             TC_PREALBARANL.CODEMP = TC_PREALBARAN.CODEMP AND";
			    strsql = strsql + "             TC_PREALBARANL.FECHA = TC_PREALBARAN.FECHA AND";
			    strsql = strsql + "             TC_PREALBARANL.MATRICULA = TC_PREALBARAN.MATRICULA AND";
			    strsql = strsql + "             TC_PREALBARANL.CODPROV = TC_PREALBARAN.CODPROV AND";
			    strsql = strsql + "             TC_PREALBARANL.NALBARAN = TC_PREALBARAN.NALBARAN";
			    strsql = strsql + "             LEFT JOIN TC_PREALBARAN_LOTES ON";
				strsql = strsql + "                 TC_PREALBARANL.CODEMP = TC_PREALBARAN_LOTES.CODEMP AND";
				strsql = strsql + "                 TC_PREALBARANL.FECHA = TC_PREALBARAN_LOTES.FECHA AND";
				strsql = strsql + "                 TC_PREALBARANL.MATRICULA = TC_PREALBARAN_LOTES.MATRICULA AND";
				strsql = strsql + "                 TC_PREALBARANL.CODPROV = TC_PREALBARAN_LOTES.CODPROV AND";
				strsql = strsql + "                 TC_PREALBARANL.NALBARAN = TC_PREALBARAN_LOTES.NALBARAN AND";
				strsql = strsql + "                 TC_PREALBARANL.NPAQUETE = TC_PREALBARAN_LOTES.NPaquete";
				strsql = strsql + "                 LEFT JOIN T_STOCKS ON";
				strsql = strsql + "                     TC_PREALBARAN_LOTES.CodEmp = T_STOCKS.CodEMP AND";
				strsql = strsql + "                     TC_PREALBARAN_LOTES.Lote = T_STOCKS.LOTE";
                strsql = strsql + " where	TC_PREALBARANL.CODEMP = '" + empresa + "' and cast(TC_PREALBARANL.NUMPED as varchar) + '-' + cast(TC_PREALBARANL.LINPED as varchar) in (" + pedidos_lineas + ")";
                strsql = strsql + " order by TC_PREALBARANL.NUMPED,TC_PREALBARANL.LINPED,TC_PREALBARANL.NALBARAN,TC_PREALBARAN_LOTES.LOTE";
               
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



        public void InsertarReserva(string empresa,string pedido_compra,string linea_compra,string lote,string pedido_venta,string linea_venta,string codigo_venta,string denominacion_venta,string cantidad_venta,string fecha_venta,string usuario)
        {
            string strsql;
            SqlCommand comando;
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();

                strsql = "";
                strsql = strsql + " INSERT INTO TC_RESERVAS_MP (CODEMP,PEDIDO_COMPRA,LINEA_COMPRA,LOTE,PEDIDO_VENTA,LINEA_VENTA,CODIGO_VENTA,DENOMINACION_VENTA,CANTIDAD_VENTA,FECHA_VENTA,USUCRE,FECHAC)";
                strsql = strsql + " VALUES('" + empresa + "',";
                strsql = strsql + "         " + pedido_compra + ",";
                strsql = strsql + "         " + linea_compra + ",";
                if(lote == "")
                    strsql = strsql +       "NULL,"; 
                else
                strsql = strsql + "        '" + lote + "',";
                strsql = strsql + "         " + pedido_venta + ",";
                strsql = strsql + "         " + linea_venta + ",";
                strsql = strsql + "        '" + codigo_venta + "',";
                strsql = strsql + "        '" + denominacion_venta + "',";
                strsql = strsql + "         " + cantidad_venta + ",";
                strsql = strsql + "        convert(datetime,'" + fecha_venta + "',103),";
                strsql = strsql + "        '" + usuario + "',";
                strsql = strsql + "         getdate())"; 
                                
                comando = new SqlCommand(strsql, conexion);
                comando.ExecuteScalar();

                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
                MessageBox.Show(Form.ActiveForm, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        public DataTable ObtenerReservasLineaPedido(string empresa, List<string> lista)
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;
            string pedidos_lineas;

            try
            {
                if (lista.Count == 0)
                    pedidos_lineas = "''";
                else
                {
                    pedidos_lineas = "";
                    for (int i = 0; i < lista.Count; i++)
                    {
                        if (pedidos_lineas == "")
                            pedidos_lineas = "'" + lista[i] + "'";
                        else
                            pedidos_lineas = pedidos_lineas + ",'" + lista[i] + "'";
                    }
                }
                
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();

                strsql = "";
                strsql = strsql + " select	ID AS id,PEDIDO_COMPRA AS P_COMPRA,LINEA_COMPRA AS L_COMPRA,LOTE,PEDIDO_VENTA AS P_VENTA,LINEA_VENTA AS L_VENTA,CODIGO_VENTA AS CÓDIGO,DENOMINACION_VENTA AS DENOMINACIÓN,CANTIDAD_VENTA AS PEDIDA,FECHA_VENTA AS ENTREGA,USUCRE AS RESERVA_POR,FECHAC AS FECHA";
                strsql = strsql + " ,(SELECT COUNT(*) FROM T_ORDTERL WHERE T_ORDTERL.CODEMP = TC_RESERVAS_MP.CODEMP AND T_ORDTERL.TIPOREG = 'CF' AND T_ORDTERL.NUMPED = TC_RESERVAS_MP.PEDIDO_VENTA AND T_ORDTERL.LINEA = TC_RESERVAS_MP.LINEA_VENTA) AS existe";
                strsql = strsql + " from	TC_RESERVAS_MP";
                strsql = strsql + " where	CODEMP = '" + empresa + "' and cast(PEDIDO_COMPRA as varchar) + '-' + cast(LINEA_COMPRA as varchar) in (" + pedidos_lineas + ")";
                strsql = strsql + " order by PEDIDO_COMPRA,LINEA_COMPRA,LOTE,PEDIDO_VENTA,LINEA_VENTA";

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



        public void EliminarReserva(string empresa, string ids)
        {
            string strsql;
            SqlCommand comando;
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();

                strsql = "";
                strsql = strsql + " DELETE  FROM TC_RESERVAS_MP";
                strsql = strsql + " WHERE   CODEMP = '" + empresa + "' AND";
                strsql = strsql + "         ID in (" + ids + ")";

                comando = new SqlCommand(strsql, conexion);
                comando.ExecuteScalar();

                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
                MessageBox.Show(Form.ActiveForm, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        public DataTable ObtenerMPs(string empresa, string proveedor, bool cerradas)
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

                strsql = "";
                strsql = strsql + " SELECT  T_ORDTERL.CODIGO";
                strsql = strsql + " FROM    T_ORDTERL INNER JOIN T_ORDTER ON";
                strsql = strsql + "             T_ORDTERL.CODEMP = T_ORDTER.CODEMP AND";
                strsql = strsql + "             T_ORDTERL.TIPOREG = T_ORDTER.TIPOREG AND";
                strsql = strsql + "             T_ORDTERL.NUMPED = T_ORDTER.NUMPED";
                strsql = strsql + " WHERE   T_ORDTERL.CODEMP = '" + empresa + "' AND";
                strsql = strsql + "         T_ORDTERL.TIPOREG = 'SF'";
                strsql = strsql + "         AND (SELECT COUNT(*) FROM T_ARTICULOS WHERE T_ARTICULOS.CODEMP = T_ORDTERL.CODEMP AND T_ARTICULOS.CODIGO = T_ORDTERL.CODIGO AND T_ARTICULOS.FAMILIA = 'MP' AND T_ARTICULOS.CATEGORIA in ('M1','M4','M5')) > 0";

                if(proveedor != "-1")
                    strsql = strsql + "     AND T_ORDTER.TERCERO = '" + proveedor + "'";
                
                if (!cerradas)                    
                    strsql = strsql + "     AND T_ORDTERL.FLAG = '0'";

                strsql = strsql + " GROUP BY T_ORDTERL.CODIGO";
                strsql = strsql + " ORDER BY T_ORDTERL.CODIGO";
                
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



        public DataTable ObtenerUnaLineaPedido(string empresa,string tipo,string pedido,string linea)
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

                strsql = "";
                strsql = strsql + " SELECT  CODIGO,DENOMINACION,CTDUP,FFINAL";
                strsql = strsql + " FROM    T_ORDTERL";
                strsql = strsql + " WHERE   CODEMP = '" + empresa + "'";
                if(tipo == "proveedor")
                    strsql = strsql + "     AND TIPOREG = 'SF'";
                if (tipo == "cliente")
                    strsql = strsql + "     AND TIPOREG = 'CF'";                
                strsql = strsql + "         AND NUMPED =" + pedido;
                strsql = strsql + "         AND LINEA =" + linea;

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



        public void InsertarCopiaLineaPedidoCompras(string empresa, string pedido_compra, string linea_compra, string codigo_compra, string denominacion_compra, string cantidad_compra, string fecha_compra, string usuario)
        {
            string strsql;
            SqlCommand comando;
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();

                strsql = "";
                strsql = strsql + " INSERT INTO TC_RESERVAS_MP_CopiaPedidosCompra(CODEMP,PEDIDO_COMPRA,LINEA_COMPRA,CODIGO_COMPRA,DENOMINACION_COMPRA,CANTIDAD_COMPRA,FECHA_COMPRA,USUCRE,FECHAC)";
                strsql = strsql + " VALUES('" + empresa + "',";
                strsql = strsql + "         " + pedido_compra + ",";
                strsql = strsql + "         " + linea_compra + ",";
                strsql = strsql + "        '" + codigo_compra + "',";
                strsql = strsql + "        '" + denominacion_compra + "',";
                strsql = strsql + "         " + cantidad_compra + ",";
                strsql = strsql + "        convert(datetime,'" + fecha_compra + "',103),";
                strsql = strsql + "        '" + usuario + "',";
                strsql = strsql + "         getdate())";

                comando = new SqlCommand(strsql, conexion);
                comando.ExecuteScalar();

                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
                MessageBox.Show(Form.ActiveForm, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        public bool ExisteCopiaPedidoCompras(string empresa, string pedido_compra)
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;
            string cantidad;

            try
            {
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();

                strsql = "";
                strsql = strsql + " SELECT  COUNT(*)";
                strsql = strsql + " FROM    TC_RESERVAS_MP_CopiaPedidosCompra";
                strsql = strsql + " WHERE   CODEMP = '" + empresa + "' AND";
                strsql = strsql + "         PEDIDO_COMPRA = " + pedido_compra;                

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);
                cantidad = table.Rows[0][0].ToString();

                conexion.Close();

                if (cantidad == "0")
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
                MessageBox.Show(Form.ActiveForm, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }



        public DataTable ObtenerCopiaLineaPedidoCompras(string empresa, string pedido_compra, string linea_compra)
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

                strsql = "";
                strsql = strsql + " SELECT  CODIGO_COMPRA,DENOMINACION_COMPRA,CANTIDAD_COMPRA,FECHA_COMPRA";
                strsql = strsql + " FROM    TC_RESERVAS_MP_CopiaPedidosCompra";
                strsql = strsql + " WHERE   CODEMP = '" + empresa + "' AND";
                strsql = strsql + "         PEDIDO_COMPRA = " + pedido_compra + " AND";
                strsql = strsql + "         LINEA_COMPRA = " + linea_compra;

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



        public void EliminarLineasCompraCopiaReservas(string empresa, string pedido, string lineas)
        {
            string strsql;
            SqlCommand comando;
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();

                strsql = "";
                strsql = strsql + " DELETE  FROM TC_RESERVAS_MP";
                strsql = strsql + " WHERE   CODEMP = '" + empresa + "' AND";
                strsql = strsql + "         PEDIDO_COMPRA = " + pedido + " AND";
                strsql = strsql + "         LINEA_COMPRA IN (" + lineas + ")";
                comando = new SqlCommand(strsql, conexion);
                comando.ExecuteScalar();

                strsql = "";
                strsql = strsql + " DELETE  FROM TC_RESERVAS_MP_CopiaPedidosCompra";
                strsql = strsql + " WHERE   CODEMP = '" + empresa + "' AND";
                strsql = strsql + "         PEDIDO_COMPRA = " + pedido + " AND";
                strsql = strsql + "         LINEA_COMPRA IN (" + lineas + ")";
                comando.CommandText = strsql;
                comando.ExecuteScalar();

                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
                MessageBox.Show(Form.ActiveForm, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        public void ActualizarCopiaLineaPedidoCompras(string empresa, string pedido_compra, string linea_compra, string codigo_compra, string denominacion_compra, string cantidad_compra, string fecha_compra, string usuario)
        {
            string strsql;
            SqlCommand comando;
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();

                strsql = "";
                strsql = strsql + " UPDATE  TC_RESERVAS_MP_CopiaPedidosCompra";
                strsql = strsql + " SET     CODIGO_COMPRA = '" + codigo_compra + "',";
                strsql = strsql + "         DENOMINACION_COMPRA = '" + denominacion_compra + "',";
                strsql = strsql + "         CANTIDAD_COMPRA = " + cantidad_compra + ",";
                strsql = strsql + "         FECHA_COMPRA = convert(datetime,'" + fecha_compra + "',103),";
                strsql = strsql + "         USUMOD = '" + usuario + "',";
                strsql = strsql + "         FECHAM = GETDATE()";
                strsql = strsql + " WHERE   CODEMP = '" + empresa + "' AND";
                strsql = strsql + "         PEDIDO_COMPRA = " + pedido_compra + " AND";
                strsql = strsql + "         LINEA_COMPRA =  " + linea_compra;
                
                comando = new SqlCommand(strsql, conexion);
                comando.ExecuteScalar();

                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
                MessageBox.Show(Form.ActiveForm, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
                 
        
    }
}
