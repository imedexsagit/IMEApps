using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using empresaGlobalProj;

namespace TiemposProyectos
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

        public DataTable obtenerOrdfabs(string proyecto)
        {
            DataTable table = null;

            string strsql = "   select distinct ofb.ORDFAB, ofb.CODIGO, (select distinct DENOMI from T_CATEGORIAS cat where cat.CodEMP = '" + empresa + "' and cat.CATEGO = art.CATEGORIA) as CATEGORIA, art.DENOMINACION, ofb.DENOMI as DESCRIPCION, CORREL, PUESTO, PREPAR, FABRIC, ofo.DENOMI, f.DENOMI as N_FAMILIA, f.FAMILIA, (SELECT top (1) VALOR FROM T_ARTCAR WHERE (CODIGO = ofb.CODIGO) AND (CARACT = '17') AND (CodEMP = '" + empresa + "') ) as TALADRADO, (SELECT top (1) CTDENL FROM T_ESTRUC WHERE (CONJUN = ofb.CODIGO) AND (CodEMP = '" + empresa + "')) AS PESO, PUESTO AS PUESTOANTIGUO ";
            strsql += "         FROM t_ordfab ofb left join T_ORDFABO ofo on ofb.ORDFAB = ofo.ORDFAB left join T_ARTICULOS art on art.CODIGO = ofb.CODIGO join T_FAMILIAS f on art.FAMILIA = f.FAMILIA where  PROYECTO = " + proyecto + " and ofb.CODEMP = '" + empresa + "' and PUESTO <> 800 and PUESTO <> 650 and PUESTO <> 640";

            

            SqlCommand comando;
            SqlDataAdapter adapter;

            try
            {

                conexion.Open();               

                comando = new SqlCommand(strsql, conexion);

                comando.CommandTimeout = 120;
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

        public int actualizarOrdfabs(string ORDFAB, string CODIGO, string DENOMINACION, string DESCRIPCION, string CORREL, string PUESTO, string PREPAR, string FABRIC, string DENOMI, string NuevoPuesto)
        {



            string strsql = "UPDATE T_ORDFABO SET PUESTO = '" + NuevoPuesto + "', PREPAR = '" + PREPAR.Replace(",", ".") + "', FABRIC = '" + FABRIC.Replace(",", ".") + "', USUMOD = '" + Environment.UserName + "', FECHAM = GETDATE()";
            strsql += " WHERE ORDFAB = '" + ORDFAB + "' AND CORREL = '" + CORREL + "' AND PUESTO = '" + PUESTO + "' --AND CODEMP = '" + empresa + "'";

            int afectadas = 0;

            SqlCommand comando;

            try
            {

                conexion.Open();

                comando = new SqlCommand(strsql, conexion);
                afectadas = comando.ExecuteNonQuery();


                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return afectadas;
        }

        public int actualizarRutas(string CODIGO, string CORREL, string PUESTO, string PREPAR, string FABRIC, string NuevoPuesto)
        {
            string strsql = "UPDATE T_RUTAS SET PUESTO = '" + NuevoPuesto + "', PREPAR = '" + PREPAR.Replace(",", ".") + "', FABRIC = '" + FABRIC.Replace(",", ".") + "', USUMOD = '" + Environment.UserName + "', FECHAM = GETDATE()";
            strsql += " WHERE CODIGO = '" + CODIGO + "' AND PUESTO = '" + PUESTO + "' AND CODEMP = '" + empresa + "'";

            int afectadas = 0;

            SqlCommand comando;

            try
            {

                conexion.Open();

                comando = new SqlCommand(strsql, conexion);
                afectadas = comando.ExecuteNonQuery();


                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return afectadas;
        }

        public void updateAgujeros(int numAgujeros, string codigo)
        {
            string strSql = "";

            SqlCommand comando;

            try
            {

                conexion.Open();

                strSql = "UPDATE T_ArtCar SET VALOR=" + numAgujeros + ", USUMOD = '" + Environment.UserName + "', FECHAM = GETDATE()";
                strSql += "	WHERE Caract='15' AND CODIGO='" + codigo + "'";
                comando = new SqlCommand(strSql, conexion);
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

        public void updateBonoL(string fab, string prepara, string codigo, string ordfab, string puesto, string NuevoPuesto)
        {
            string strSql = "";

            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;

            try
            {

                conexion.Open();

                strSql = "SELECT BONO FROM TC_BONOL bo WHERE ORDFAB = '" + ordfab + "' and MARCA = '" + codigo + "' and puesto = '" + puesto + "' AND BONO NOT IN ( ";
                strSql += " SELECT codBono FROM CS_CPP_0713.dbo.MP_TRABAJOCONBONO WHERE codBono = bo.BONO)";

                comando = new SqlCommand(strSql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                    strSql = "UPDATE TC_BONOL SET PUESTO = '" + NuevoPuesto + "', FABRICA='" + fab.Replace(",", ".") + "', PREPARA='" + prepara.Replace(",", ".") + "', USUMOD = '" + Environment.UserName + "', FECHAM = GETDATE()";
                    strSql += " WHERE ORDFAB = '" + ordfab + "' AND MARCA ='" + codigo + "' AND PUESTO = '" + puesto + "' and BONO = '" + table.Rows[0][0].ToString() + "'";
                    comando = new SqlCommand(strSql, conexion);
                    comando.ExecuteScalar();

                    strSql = "update TC_BONO set FABRICA = ( select SUM((FABRICA*CANTIDAD)) FROM TC_BONOL WHERE BONO = " + table.Rows[0][0].ToString() + ") ,USUMOD='" + Environment.UserName + "', FECHAM=GETDATE() where bono=" + table.Rows[0][0].ToString();
                    comando = new SqlCommand(strSql, conexion);
                    comando.ExecuteScalar();
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


        public DataTable getValoresFormula(string puesto)
        {
            string strsql = "";
            DataTable table = new DataTable();

            SqlDataAdapter adapter;
            SqlCommand comando;

            try
            {

                conexion.Open();

                strsql = "select T_FAB, T_PREP, TIENE_AGUJEROS from TC_TIEMPOS_FORMULAS_PUESTOS where puesto = '" + puesto + "'";

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


        public int actualizarBonos(string ORDFAB, string CORREL, string PUESTO, string PREPAR, string FABRIC, string NuevoPuesto)
        {
            string strsql = "UPDATE TC_BONOL SET PUESTO = '" + NuevoPuesto + "', PREPARA = '" + PREPAR.Replace(",", ".") + "', FABRICA = '" + FABRIC.Replace(",", ".") + "', USUMOD = '" + Environment.UserName + "', FECHAM = GETDATE()";
            strsql += " WHERE ORDFAB = '" + ORDFAB + "' AND CORREL = '" + CORREL + "' AND PUESTO = '" + PUESTO + "' AND CODEMP = '" + empresa + "'";

            int afectadas = 0;

            SqlCommand comando;

            try
            {

                conexion.Open();

                comando = new SqlCommand(strsql, conexion);
                afectadas = comando.ExecuteNonQuery();


                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return afectadas;
        }

        public void procesarTiempos(string proyecto)
        {
            string ejecutar = "exec tiemposTaladrado '" + proyecto + "'";
            //MessageBox.Show("EJECTUAR EL SIGUIENTE PROCEDIMIENTO EN SQL SERVER :: " + ejecutar + "", "aviso ejecutar:" + ejecutar, MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        public string getRutaFamilia(string codigo)
        {
            string strsql = "";
            strsql = strsql + " SELECT * FROM TC_FAMILIA_PATH WHERE   TIPO='nc' AND CODEMP='3' AND FAMILIA  ";
            strsql = strsql + " IN ( select T_FAMILIAS.FAMILIA   from t_articulos inner join T_FAMILIAS  ";
            strsql = strsql + " on T_ARTICULOS.FAMILIA = T_FAMILIAS.FAMILIA where codigo='" + codigo + "' and T_FAMILIAS.CodEMP='3')";

            SqlCommand comando = new SqlCommand(strsql, conexion);
            SqlDataAdapter adapter = new SqlDataAdapter(comando);
            DataTable table = new DataTable();
            adapter.Fill(table);

            conexion.Close();


            return table.Rows[0][3].ToString();
        }
        

    }
}
