using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using empresaGlobalProj;

namespace PaquetesReprocesado
{
    class DButils
    {
        public string empresa;
        SqlConnection conexion;

        public DButils()
        {

            empresa = empresaGlobal.empresaID; //ConfigurationManager.AppSettings["EMPRESA"].ToString();                        
            conexion = new SqlConnection(Utils.CD.getConexion());
            //conexion = new SqlConnection("Data Source=srvdesarrollo;Initial Catalog=gg;uid=gg;pwd=ostia;MultipleActiveResultSets=True");
        }

        public DataTable getMarcasPaqueteGalv(string codigo, bool esCamion)
        {
            Cursor.Current = Cursors.WaitCursor;
            DataTable table = null;


            string strsql = "select CODIGO AS MARCA, eti.Etiqueta as ETIQUETA, (items.cantidad - isnull((select sum( cantidad)  from GL_REPROCESADOS where gl_reprocesados.ETIQUETA = eti.Etiqueta), 0))  as CANTIDAD,";
            strsql += "      DENOMINACION, PROYECTO, expe.cb_num as EXPEDICION, concat ('73', paq.cb_num) as PAQUETE  ";
            strsql += "      from imedexsa_intranet.dbo.GL_PAQUE paq inner join  ";
            strsql += "      imedexsa_intranet.dbo.GL_ITEMSPAQ items on items.paque_id = paq.id inner join  ";
            strsql += "      TC_LANZA_ETIQUETA eti on eti.Etiqueta = items.cb inner join  ";
            strsql += "      T_ORDFAB ofab on ofab.ORDFAB = eti.OrdFab inner join  ";
            strsql += "      imedexsa_intranet.dbo.GL_ITEMSEXP iexp on iexp.paque_id = paq.id inner join  ";
            strsql += "      imedexsa_intranet.dbo.GL_EXPED expe on expe.id = iexp.exped_id  ";

            if (!esCamion)
            {
                strsql += "      where paq.cb_num = '" + codigo.Substring(2) +"' and paq.empresa = '" + empresa + "' and (items.cantidad - isnull((select sum( cantidad)  from GL_REPROCESADOS where gl_reprocesados.ETIQUETA = eti.Etiqueta), 0)) > 0";
            }
            else
            {
                strsql += "      where expe.camion = '" + codigo + "' and paq.empresa = '" + empresa + "' and (items.cantidad - isnull((select sum( cantidad)  from GL_REPROCESADOS where gl_reprocesados.ETIQUETA = eti.Etiqueta), 0)) > 0";
            }
            SqlCommand comando;
            SqlDataAdapter adapter;

            try
            {

                conexion.Open();

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);


                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursors.Default;
            return table;
        }

        public DataTable getMarcasPaqueteReproc(string paquete)
        {
            DataTable table = null;


            string strsql = "select MARCA, ETIQUETA, CANTIDAD, concat ('73', cb_num) as PAQUETE , grep.estado  FROM GL_REPROCESADOS grep left join imedexsa_intranet.dbo.GL_PAQUE gp on gp.id = grep.PAQUETE_ID where PAQUE_REPROC = '" + paquete + "' and CODEMP = '" + empresa + "'";

            SqlCommand comando;
            SqlDataAdapter adapter;

            try
            {

                conexion.Open();

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);


                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return table;
        }

        public DataTable getPaqsReprocesado()
        {
            DataTable table = null;

            string strsql = "select CONCAT ('RE', ID_PAQ) AS PAQUETE, expe.cb_num as EXPEDICIÓN, prep.ESTADO, TIPO, SEDE, FECHA_C AS FECHA from gl_paque_reprocesados prep left join imedexsa_intranet.dbo.GL_EXPED expe on expe.id = prep.EXPED_ID";

            SqlCommand comando;
            SqlDataAdapter adapter;

            try
            {

                conexion.Open();

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);


                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return table;
        }

        public DataTable getExpeds()
        {
            DataTable table = null;

            string strsql = "select distinct cb_num, fch_creacion from imedexsa_intranet.dbo.GL_EXPED where cod_empresa = '3' order by fch_creacion desc";

            SqlCommand comando;
            SqlDataAdapter adapter;

            try
            {

                conexion.Open();

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);


                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return table;
        }

        public void insertarMarcasPaquete(DataRow infoMarca, string idPaquete)
        {
            string strsql = "";

            SqlCommand comando;

            try
            {

                conexion.Open();

                strsql = "select * from GL_REPROCESADOS where ETIQUETA = '" + infoMarca["ETIQUETA"] + "' and PAQUE_REPROC = " + idPaquete;
                comando = new SqlCommand(strsql, conexion);

                if (Convert.ToInt32(comando.ExecuteScalar()) == 0)
                {

                    strsql = "INSERT INTO [dbo].[GL_REPROCESADOS] ";
                    strsql += "([CODEMP] ";
                    strsql += ",[PAQUE_REPROC] ";
                    strsql += ",[PAQUETE_ID] ";
                    strsql += ",[MARCA] ";
                    strsql += ",[ETIQUETA] ";
                    strsql += ",[CANTIDAD] ";
                    strsql += ",[ESTADO] ";
                    strsql += ",[FECHA_C] ";
                    strsql += ",[USU_CRE]) ";
                    strsql += "VALUES ";
                    strsql += "('" + empresa + "'";
                    strsql += ",'" + idPaquete + "'";
                    strsql += ", (select top (1) id from imedexsa_intranet.dbo.GL_PAQUE where cb_num = '" + infoMarca["PAQUETE"].ToString().Substring(2) + "' and empresa = '" + empresa + "')";
                    strsql += ",'" + infoMarca["MARCA"] + "'";
                    strsql += ",'" + infoMarca["ETIQUETA"] + "'";
                    strsql += ",'" + infoMarca["CANTIDAD"] + "'";
                    strsql += ",'" + infoMarca["ESTADO"] + "'";
                    strsql += ",GETDATE()";
                    strsql += ",'" + Environment.UserName + "') ";
                    
                }
                else
                {
                    strsql = "UPDATE [dbo].[GL_REPROCESADOS] SET [CANTIDAD] = [CANTIDAD] + " + infoMarca["CANTIDAD"] + "  ,[FECHA_M] = GETDATE() ,[USU_MOD] = '" + Environment.UserName + "' WHERE ETIQUETA = '" + infoMarca["ETIQUETA"] + "' AND PAQUE_REPROC = " + idPaquete;

                }

                comando = new SqlCommand(strsql, conexion);

                comando.ExecuteNonQuery();


                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void eliminarMarcasPaquete(string etiqueta, string idPaquete)
        {
            string strsql = "";

            SqlCommand comando;

            try
            {

                conexion.Open();

                strsql = "DELETE FROM [dbo].[GL_REPROCESADOS] WHERE PAQUE_REPROC = '" + idPaquete + "' AND ETIQUETA = '" + etiqueta + "' AND CODEMP = '" + empresa + "'";

                comando = new SqlCommand(strsql, conexion);

                comando.ExecuteNonQuery();


                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public int insertarPaqueteReproc(string exped, string tipo, string sede)
        {
            string strsql = "";
            int id = -1;

            SqlCommand comando;

            try
            {

                conexion.Open();


                strsql = "INSERT INTO [dbo].[GL_PAQUE_REPROCESADOS] ";
                strsql += "           ([EXPED_ID] ";
                strsql += "           ,[ESTADO] ";
                strsql += "           ,[TIPO] ";
                strsql += "           ,[SEDE] ";
                strsql += "           ,[FECHA_C] ";
                strsql += "           ,[USU_CRE]) ";
                strsql += "output INSERTED.ID_PAQ     VALUES ";
                strsql += "           ((select top(1) id from imedexsa_intranet.dbo.GL_EXPED where cb_num = '" + exped + "')";
                strsql += "           ,'PENDIENTE' ";
                strsql += "           ,'" + tipo + "' ";
                strsql += "           ,'" + sede + "' ";
                strsql += "           ,GETDATE() ";
                strsql += "           ,'" + Environment.UserName + "')";

                comando = new SqlCommand(strsql, conexion);

                id = (int)comando.ExecuteScalar();

                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return id;
        }

        public void updatePaqueteReproc(string idPaquete, string exped, string tipo, string sede)
        {
            string strsql = "";

            SqlCommand comando;

            try
            {

                conexion.Open();


                strsql = "UPDATE [dbo].[GL_PAQUE_REPROCESADOS] ";
                strsql += "   SET [EXPED_ID] = (select top(1) id from imedexsa_intranet.dbo.GL_EXPED where cb_num = '" + exped + "')";
                strsql += "      ,[TIPO] = '" + tipo + "' ";
                strsql += "      ,[SEDE] = '" + sede + "' ";
                strsql += "      ,[FECHA_M] = GETDATE() ";
                strsql += "      ,[USU_MOD] = '" + Environment.UserName + "' ";
                strsql += " WHERE ID_PAQ = '" + idPaquete + "' ";

                comando = new SqlCommand(strsql, conexion);

                comando.ExecuteScalar();

                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public string getNuevoId()
        {
            DataTable table = null;

            string strsql = "Select IDENT_CURRENT('GL_PAQUE_REPROCESADOS')+1";

            SqlCommand comando;
            SqlDataAdapter adapter;

            try
            {

                conexion.Open();

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);


                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return table.Rows[0][0].ToString();
        }

        public void eliminarPaqReproc(string idPaquete)
        {

            string strsql = "DELETE FROM GL_PAQUE_REPROCESADOS WHERE ID_PAQ = '" + idPaquete.Substring(2) + "'; DELETE FROM GL_REPROCESADOS WHERE PAQUE_REPROC = '" + idPaquete.Substring(2) + "'";

            SqlCommand comando;

            try
            {

                conexion.Open();

                comando = new SqlCommand(strsql, conexion);
                comando.ExecuteScalar();


                conexion.Close();
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
