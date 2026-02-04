using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace ProyectosOnLine
{
    class ConsultasBD
    {

        public void recuperarFichajesSinExpedicion(string proyecto)
        {


            using (SqlConnection con = new SqlConnection(Utils.CD.getConexion()))
            {
                using (SqlCommand cmd = new SqlCommand("Ime_recuperarFichajesProyectosSinExpedicion", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@PROYECTO", SqlDbType.VarChar).Value = proyecto;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }


        }


        public DataTable obtenerInformacionProyectos(string proyecto)
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

                strsql = @"
                    declare @empresa varchar(2) = '3'
                    declare @CodLanza int = " + proyecto + @"
                    declare @FiltroEstadoOF int = 2
                    declare @M1 int = 1
                    declare @M2 int = 1
                    declare @M3 int = 1
                    declare @M4 int = 1
                    declare @M5 int = 1
                    declare @CS int = 1
                    declare @codigo varchar(20) = ''


SELECT   T_ORDFAB.CODIGO, T_ORDFAB.ORDFAB, T_ORDFAB.CTDLAN,
                             (SELECT        TOP (1) T_ARTICULOS.CODIGO
                               FROM            T_ORDFABM INNER JOIN
                                                         T_ARTICULOS ON T_ORDFABM.CODEMP = T_ARTICULOS.CodEMP AND T_ORDFABM.CODIGO = T_ARTICULOS.CODIGO
                               WHERE        (T_ORDFABM.CODEMP = T_ORDFAB.CODEMP) AND (T_ORDFABM.ORDFAB = T_ORDFAB.ORDFAB) AND (T_ORDFABM.CTDENL > 0) AND (T_ARTICULOS.FAMILIA = 'MP')) AS MP, ISNULL
                             ((SELECT        VALOR
                                 FROM            T_ARTCAR
                                 WHERE        (CodEMP = T_ORDFAB.CODEMP) AND (CODIGO = T_ORDFAB.CODIGO) AND (CATEGO = T_ORDFAB.CATEGORIA) AND (CARACT = '04')), '') AS Long, 
								 CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpFicep()) 
                         = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.Fice), 0) ELSE NULL END AS Fice,

						 CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpSierra()) = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.Serr), 0) ELSE NULL END AS Serr, 
						 
						 
						 CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpFresar()) 
                         = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.Fres), 0) ELSE NULL END AS Fres, CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpGeka()) 
                         = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.Geka), 0) ELSE NULL END AS Geka,

						CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpPlegadora()) = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.Pleg), 0) ELSE NULL END AS Pleg, 

						           cast (CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpPlasma()) = 1 THEN CASE WHEN
                             (SELECT        COUNT(*)
                               FROM            TC_BONOL
                               WHERE        CODEMP = T_ORDFAB.CODEMP AND ORDFAB = T_ORDFAB.ORDFAB AND OPERAC = dbo.IME_GetOpPlasma()) > 0 THEN isnull(SUM(Vista_TC_IMPORTBONOS.Plasm), 0) ELSE - 1 END ELSE NULL END as varchar(15)) AS Plasm, 
						 
						 
						 cast ( CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpCizalla()) = 1 THEN CASE WHEN
                             (SELECT        COUNT(*)
                               FROM            TC_BONOL
                               WHERE        CODEMP = T_ORDFAB.CODEMP AND ORDFAB = T_ORDFAB.ORDFAB AND OPERAC = dbo.IME_GetOpCizalla()) > 0 THEN isnull(SUM(Vista_TC_IMPORTBONOS.Ciza), 0) ELSE - 1 END ELSE NULL END  as varchar(15)) AS Ciza, 
                         cast ( CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpFicepV()) = 1 THEN CASE WHEN
                             (SELECT        COUNT(*)
                               FROM            TC_BONOL
                               WHERE        CODEMP = T_ORDFAB.CODEMP AND ORDFAB = T_ORDFAB.ORDFAB AND OPERAC = dbo.IME_GetOpFicepV()) > 0 THEN isnull(SUM(Vista_TC_IMPORTBONOS.Punz), 0) ELSE - 1 END ELSE NULL END  as varchar(15)) AS Punz, 

						CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpGranallado()) = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.Gran), 0) ELSE NULL END AS Gran, 

                         CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpFresarCH()) = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.FresCH), 0) ELSE NULL END AS FresCH, 
                         CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpBiselarCH()) = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.BiselCH), 0) ELSE NULL END AS BiselCH, 
				        CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpPlegadoraCH()) = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.PlegCH), 0) ELSE NULL END AS PlegCH, 

						
                         CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpContornear()) = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.Contor), 0) ELSE NULL END AS Contor, 
                         CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpAvellanar()) = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.Avella), 0) ELSE NULL END AS Avella, 

						CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpRetaladrar()) = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.Retala), 0) ELSE NULL END AS Retala, 

                         CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpManual()) = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.Manu), 0) ELSE NULL END AS Manu, 

  

                         CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpPunteoArcoSumergido()) = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.PunteoArcoSumergido), 0) ELSE NULL END AS PunteoAS, 
                         CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpSoldaduraArcoSumergido()) = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.SoldaduraArcoSumergido), 0) ELSE NULL 
                         END AS SoldAS, CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpPunteoSoldadura()) = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.Punteo), 0) ELSE NULL END AS Punteo, 
                         CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpSoldar()) = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.Sold), 0) ELSE NULL END AS Sold, 
                         CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpExterior()) = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.Exte), 0) ELSE NULL END AS Exte, NULL as Expe,NULL as Galv,NULL as Rece,


                             cast((SELECT        CASE WHEN isnull(numInspecciones, 0) = 0 THEN 0 WHEN Incorrecto > 0 THEN - 1 ELSE 1 END AS Expr1
                               FROM            (SELECT        COUNT(TC_InspeccionEtiquetas.id) AS numInspecciones, SUM(CASE WHEN TC_InspeccionEtiquetas.Correcto = 1 THEN 1 ELSE 0 END) AS Correcto, 
                                                                                   SUM(CASE WHEN TC_InspeccionEtiquetas.Correcto = 0 THEN 1 ELSE 0 END) AS Incorrecto
                                                         FROM            TC_InspeccionEtiquetas INNER JOIN
                                                                                   TC_LANZA_ETIQUETA ON TC_InspeccionEtiquetas.Etiqueta = TC_LANZA_ETIQUETA.Etiqueta AND TC_InspeccionEtiquetas.CodEmp = TC_LANZA_ETIQUETA.CodEmp
                                                         WHERE        (TC_InspeccionEtiquetas.CodEmp = @empresa) AND (TC_LANZA_ETIQUETA.OrdFab = T_ORDFAB.ORDFAB)) AS t_) as varchar(10)) AS Calidad

                            FROM T_ORDFAB left join 
                            (SELECT ORDFAB, OPERAC from T_ORDFABO where OPERAC = '800001') as T_ORDFABO
                            on T_ORDFABO.ORDFAB = T_ORDFAB.ORDFAB
                            LEFT OUTER JOIN
                                                     AUX_PROYECTOS_ONLINE AS Vista_TC_IMPORTBONOS ON T_ORDFAB.CODEMP = Vista_TC_IMPORTBONOS.CODEMP AND T_ORDFAB.ORDFAB = Vista_TC_IMPORTBONOS.ORDFAB
                            WHERE        (T_ORDFAB.CODEMP = @empresa) AND (T_ORDFAB.TIPOREG = 'F') AND (T_ORDFAB.PROYECTO = CAST(@CodLanza AS varchar(10)))  and (OPERAC IS NULL)  AND 
                                                     (T_ORDFAB.FLAG = CASE WHEN @FiltroEstadoOF = 0 THEN '0' WHEN @FiltroEstadoOF = 1 THEN '1' ELSE T_ORDFAB.FLAG END) AND (ISNULL
                                                         ((SELECT        TOP (1) T_ARTICULOS_1.CATEGORIA
                                                             FROM            T_ORDFABM AS T_ORDFABM_1 INNER JOIN
                                                                                      T_ARTICULOS AS T_ARTICULOS_1 ON T_ORDFABM_1.CODEMP = T_ARTICULOS_1.CodEMP AND T_ORDFABM_1.CODIGO = T_ARTICULOS_1.CODIGO
                                                             WHERE        (T_ORDFABM_1.CODEMP = T_ORDFAB.CODEMP) AND (T_ORDFABM_1.ORDFAB = T_ORDFAB.ORDFAB) AND (T_ORDFABM_1.CTDENL > 0) AND (T_ARTICULOS_1.FAMILIA = 'MP')), '') 
                                                     IN (CASE WHEN @M1 = 1 THEN 'M1' END, CASE WHEN @M2 = 1 THEN 'M2' END, CASE WHEN @M3 = 1 THEN 'M3' END, CASE WHEN @M4 = 1 THEN 'M4' END, CASE WHEN @M5 = 1 THEN 'M5' END, 
                                                     CASE WHEN @CS = 1 THEN '' END)) AND (@codigo <> '' AND T_ORDFAB.CODIGO LIKE @codigo + '%' OR
                                                     @codigo = '')
                            GROUP BY T_ORDFAB.CODEMP, T_ORDFAB.PROYECTO, T_ORDFAB.CODIGO, T_ORDFAB.ORDFAB, T_ORDFAB.CTDLAN, T_ORDFAB.FLAG, T_ORDFAB.CATEGORIA
                            ORDER BY T_ORDFAB.CODIGO, T_ORDFAB.ORDFAB";

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



        public DataTable obtenerInformacionProyectosOnline(string empresa, string proyecto, Int32 estadoOF, Int32 M1, Int32 M2, Int32 M3, Int32 M4, Int32 M5, Int32 CS, string marca, Int32 estadoExp)
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
                

                strsql = @"
                    declare @empresa varchar(2) = '" + empresa + @"'
                    declare @CodLanza int = " + proyecto + @"
                    declare @FiltroEstadoOF int = " + estadoOF + @"
                    declare @M1 int = " + M1 + @"
                    declare @M2 int = " + M2 + @"
                    declare @M3 int = " + M3 + @"
                    declare @M4 int = " + M4 + @"
                    declare @M5 int = " + M5 + @"
                    declare @CS int = " + CS + @"
                    declare @codigo varchar(20) = '" + marca + @"'
                    declare @EXPE int = " + estadoExp + @"

                   SELECT T_ORDFAB.PROYECTO, T_ORDFAB.CODIGO, T_ORDFAB.ORDFAB, T_ORDFAB.CTDLAN, T_ORDFAB.FLAG , T_ORDFAB.MP, T_ORDFAB.Caract, T_ORDFAB.SinProcesar,CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpFicep()) = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.Fice), 0) ELSE NULL END AS Fice, CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpFresar()) = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.Fres), 0) ELSE NULL END AS Fres, CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpGeka()) = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.Geka), 0) ELSE NULL END AS Geka, CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpCizalla()) = 1 THEN CASE WHEN
                             (SELECT        COUNT(*)
                               FROM            TC_BONOL
                               WHERE        CODEMP = T_ORDFAB.CODEMP AND ORDFAB = T_ORDFAB.ORDFAB AND OPERAC = dbo.IME_GetOpCizalla()) > 0 THEN isnull(SUM(Vista_TC_IMPORTBONOS.Ciza), 0) ELSE - 1 END ELSE NULL END AS Ciza, 
                         CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpFicepV()) = 1 THEN CASE WHEN
                             (SELECT        COUNT(*)
                               FROM            TC_BONOL
                               WHERE        CODEMP = T_ORDFAB.CODEMP AND ORDFAB = T_ORDFAB.ORDFAB AND OPERAC = dbo.IME_GetOpFicepV()) > 0 THEN isnull(SUM(Vista_TC_IMPORTBONOS.Punz), 0) ELSE - 1 END ELSE NULL END AS Punz, 
                         CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpPlasma()) = 1 THEN CASE WHEN
                             (SELECT        COUNT(*)
                               FROM            TC_BONOL
                               WHERE        CODEMP = T_ORDFAB.CODEMP AND ORDFAB = T_ORDFAB.ORDFAB AND OPERAC = dbo.IME_GetOpPlasma()) > 0 THEN isnull(SUM(Vista_TC_IMPORTBONOS.Plasm), 0) ELSE - 1 END ELSE NULL END AS Plasm, 
                         CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpPlegadora()) = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.Pleg), 0) ELSE NULL END AS Pleg, 
                         CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpManual()) = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.Manu), 0) ELSE NULL END AS Manu, 
                         CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpSierra()) = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.Serr), 0) ELSE NULL END AS Serr, 
                         CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpGranallado()) = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.Gran), 0) ELSE NULL END AS Gran, 
                         CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpPunteoArcoSumergido()) = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.PunteoArcoSumergido), 0) ELSE NULL END AS PunteoAS, 
                         CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpSoldaduraArcoSumergido()) = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.SoldaduraArcoSumergido), 0) ELSE NULL 
                         END AS SoldAS, CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpPunteoSoldadura()) = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.Punteo), 0) ELSE NULL END AS Punteo, 
                         CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpSoldar()) = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.Sold), 0) ELSE NULL END AS Sold, 
                         CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpExterior()) = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.Exte), 0) ELSE NULL END AS Exte, 
                         CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpPlegadoraCH()) = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.PlegCH), 0) ELSE NULL END AS PlegCH, 
                         CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpFresarCH()) = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.FresCH), 0) ELSE NULL END AS FresCH, 
                         CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpBiselarCH()) = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.BiselCH), 0) ELSE NULL END AS BiselCH, 
                         CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpContornear()) = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.Contor), 0) ELSE NULL END AS Contor, 
                         CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpAvellanar()) = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.Avella), 0) ELSE NULL END AS Avella, 
                         CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpRetaladrar()) = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.Retala), 0) ELSE NULL END AS Retala,



                        T_ORDFAB.Expe, CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, 
                                                 T_ORDFAB.ORDFAB, dbo.IME_GetOpGalvanizar()) = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.Recep), 0) ELSE NULL END AS Rece,CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, 
                                                 T_ORDFAB.ORDFAB, dbo.IME_GetOpGalvanizar()) = 1 THEN isnull(SUM(Vista_TC_IMPORTBONOS.Recep), 0) ELSE NULL END AS Galv,
                                                     (SELECT        CASE WHEN isnull(numInspecciones, 0) = 0 THEN 0 WHEN Incorrecto > 0 THEN - 1 ELSE 1 END AS Expr1
                                                       FROM            (SELECT        COUNT(TC_InspeccionEtiquetas.id) AS numInspecciones, SUM(CASE WHEN TC_InspeccionEtiquetas.Correcto = 1 THEN 1 ELSE 0 END) AS Correcto, 
                                                                                                           SUM(CASE WHEN TC_InspeccionEtiquetas.Correcto = 0 THEN 1 ELSE 0 END) AS Incorrecto
                                                                                 FROM            TC_InspeccionEtiquetas INNER JOIN
                                                                                                           TC_LANZA_ETIQUETA ON TC_InspeccionEtiquetas.Etiqueta = TC_LANZA_ETIQUETA.Etiqueta AND TC_InspeccionEtiquetas.CodEmp = TC_LANZA_ETIQUETA.CodEmp
                                                                                 WHERE        (TC_InspeccionEtiquetas.CodEmp = @empresa) AND (TC_LANZA_ETIQUETA.OrdFab = T_ORDFAB.ORDFAB)) AS t_) AS calidad FROM (
                        (select  * from (
                        SELECT       T_ORDFAB.CODEMP, T_ORDFAB.PROYECTO, T_ORDFAB.CODIGO, T_ORDFAB.ORDFAB, T_ORDFAB.CTDLAN, T_ORDFAB.FLAG,T_ORDFAB.CATEGORIA,
                                                     (SELECT        TOP (1) T_ARTICULOS.CODIGO
                                                       FROM            T_ORDFABM INNER JOIN
                                                                                 T_ARTICULOS ON T_ORDFABM.CODEMP = T_ARTICULOS.CodEMP AND T_ORDFABM.CODIGO = T_ARTICULOS.CODIGO
                                                       WHERE        (T_ORDFABM.CODEMP = T_ORDFAB.CODEMP) AND (T_ORDFABM.ORDFAB = T_ORDFAB.ORDFAB) AND (T_ORDFABM.CTDENL > 0) AND (T_ARTICULOS.FAMILIA = 'MP')) AS MP, ISNULL
                                                     ((SELECT        VALOR
                                                         FROM            T_ARTCAR
                                                         WHERE        (CodEMP = T_ORDFAB.CODEMP) AND (CODIGO = T_ORDFAB.CODIGO) AND (CATEGO = T_ORDFAB.CATEGORIA) AND (CARACT = '04')), '') AS Caract, ISNULL
                                                     ((SELECT        SUM(1) AS Expr1
                                                         FROM            TC_IMPORT_BONOS INNER JOIN
                                                                                  T_MAQUINAS ON TC_IMPORT_BONOS.CODEMP = T_MAQUINAS.CODEMP AND TC_IMPORT_BONOS.MAQUINA = T_MAQUINAS.MAQUI
                                                         WHERE        (T_MAQUINAS.TIPOP = '010') AND (TC_IMPORT_BONOS.CODEMP = T_ORDFAB.CODEMP) AND (TC_IMPORT_BONOS.ORDFAB_int = T_ORDFAB.ORDFAB) AND (TC_IMPORT_BONOS.PBUENAS_float <> 0) AND 
                                                                                  (TC_IMPORT_BONOS.CLAHOR = 0) AND (TC_IMPORT_BONOS.FLAGS = 0)), 0) AS SinProcesar,
														  
														                          CASE WHEN dbo.IME_Tiene_OF_Ope(T_ORDFAB.CODEMP, T_ORDFAB.ORDFAB, dbo.IME_GetOpGalvanizar()) = 1 THEN isnull
                                                     ((SELECT        SUM(imedexsa_intranet.dbo.GL_ITEMSPAQ.cantidad) AS ctd
                                                         FROM            gg.dbo.TC_LANZA_ETIQUETA INNER JOIN
                                                                                  imedexsa_intranet.dbo.GL_ITEMSPAQ INNER JOIN
                                                                                  imedexsa_intranet.dbo.GL_PAQUE ON imedexsa_intranet.dbo.GL_ITEMSPAQ.paque_id = imedexsa_intranet.dbo.GL_PAQUE.id ON 
                                                                                  gg.dbo.TC_LANZA_ETIQUETA.Etiqueta = imedexsa_intranet.dbo.GL_ITEMSPAQ.cb INNER JOIN
                                                                                  imedexsa_intranet.dbo.GL_EXPED INNER JOIN
                                                                                  imedexsa_intranet.dbo.GL_ITEMSEXP ON imedexsa_intranet.dbo.GL_EXPED.id = imedexsa_intranet.dbo.GL_ITEMSEXP.exped_id ON 
                                                                                  imedexsa_intranet.dbo.GL_PAQUE.id = imedexsa_intranet.dbo.GL_ITEMSEXP.paque_id
                                                         WHERE        (imedexsa_intranet.dbo.GL_EXPED.estado = 1) AND (imedexsa_intranet.dbo.GL_PAQUE.estado = 'ACE') AND (gg.dbo.TC_LANZA_ETIQUETA.CodEmp = T_ORDFAB.CODEMP) AND 
                                                                                  (gg.dbo.TC_LANZA_ETIQUETA.TIPOREG = 'F') AND (gg.dbo.TC_LANZA_ETIQUETA.OrdFab = T_ORDFAB.ORDFAB)), 0) ELSE NULL END AS Expe

                        FROM            T_ORDFAB 
                        WHERE        (T_ORDFAB.CODEMP = @empresa) AND (T_ORDFAB.TIPOREG = 'F') AND (T_ORDFAB.PROYECTO = CAST(@CodLanza AS varchar(10))) AND 
                                                 (T_ORDFAB.FLAG = CASE WHEN @FiltroEstadoOF = 0 THEN '0' WHEN @FiltroEstadoOF = 1 THEN '1' ELSE T_ORDFAB.FLAG END) AND (ISNULL
                                                     ((SELECT        TOP (1) T_ARTICULOS_1.CATEGORIA
                                                         FROM            T_ORDFABM AS T_ORDFABM_1 INNER JOIN
                                                                                  T_ARTICULOS AS T_ARTICULOS_1 ON T_ORDFABM_1.CODEMP = T_ARTICULOS_1.CodEMP AND T_ORDFABM_1.CODIGO = T_ARTICULOS_1.CODIGO
                                                         WHERE        (T_ORDFABM_1.CODEMP = T_ORDFAB.CODEMP) AND (T_ORDFABM_1.ORDFAB = T_ORDFAB.ORDFAB) AND (T_ORDFABM_1.CTDENL > 0) AND (T_ARTICULOS_1.FAMILIA = 'MP')), '') 
                                                 IN (CASE WHEN @M1 = 1 THEN 'M1' END, CASE WHEN @M2 = 1 THEN 'M2' END, CASE WHEN @M3 = 1 THEN 'M3' END, CASE WHEN @M4 = 1 THEN 'M4' END, CASE WHEN @M5 = 1 THEN 'M5' END, 
                                                 CASE WHEN @CS = 1 THEN '' END)) AND (@codigo <> '' AND T_ORDFAB.CODIGO LIKE @codigo + '%' OR
                                                 @codigo = '')
                        --GROUP BY T_ORDFAB.CODEMP, T_ORDFAB.PROYECTO, T_ORDFAB.CODIGO, T_ORDFAB.ORDFAB, T_ORDFAB.CTDLAN, T_ORDFAB.FLAG, T_ORDFAB.CATEGORIA
                        ) as T_ORDFAB_TEMP where (@EXPE = 1 and (Expe = CTDLAN)) or (@EXPE = 0 and (Expe <> CTDLAN OR Expe IS NULL)) or (@EXPE = 2)) T_ORDFAB
	                          --where Expe <> CTDLAN) T_ORDFAB
                        LEFT OUTER JOIN
                        Vista_TC_IMPORTBONOS ON T_ORDFAB.CODEMP = Vista_TC_IMPORTBONOS.CODEMP AND T_ORDFAB.ORDFAB = Vista_TC_IMPORTBONOS.ORDFAB
                        ) 
                        GROUP BY T_ORDFAB.CODEMP, T_ORDFAB.PROYECTO, T_ORDFAB.CODIGO, T_ORDFAB.ORDFAB, T_ORDFAB.CTDLAN, T_ORDFAB.FLAG, T_ORDFAB.CATEGORIA, T_ORDFAB.MP, T_ORDFAB.Caract, T_ORDFAB.SinProcesar,T_ORDFAB.Expe
                        ORDER BY T_ORDFAB.CODIGO, T_ORDFAB.ORDFAB";

                comando = new SqlCommand(strsql, conexion);
                comando.CommandTimeout = 900;
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



        public DataTable obtenerInformacionProyectosOnlineNEW(string empresa, string proyecto, Int32 estadoOF, Int32 M1, Int32 M2, Int32 M3, Int32 M4, Int32 M5, Int32 CS, string marca, Int32 estadoExp)
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


                strsql = @"
                    declare @empresa varchar(2) = '" + empresa + @"'
                    declare @CodLanza int = " + proyecto + @"
                    declare @FiltroEstadoOF int = " + estadoOF + @"
                    declare @M1 int = " + M1 + @"
                    declare @M2 int = " + M2 + @"
                    declare @M3 int = " + M3 + @"
                    declare @M4 int = " + M4 + @"
                    declare @M5 int = " + M5 + @"
                    declare @CS int = " + CS + @"
                    declare @codigo varchar(20) = '" + marca + @"'
                    declare @EXPE int = " + estadoExp + @"

                   select * from(
                    select TC_PROYECTOS_ONLINE.PROYECTO, TC_PROYECTOS_ONLINE.CODIGO, TC_PROYECTOS_ONLINE.ORDFAB, TC_PROYECTOS_ONLINE.CTDLAN, TC_PROYECTOS_ONLINE.FLAG, MP, caract, SinProcesar, Fice, Fres, Geka, Ciza, Punz, Plasm, Pleg, Manu, Serr, Gran, PunteoAS, SoldAS, Punteo, Sold, Exte, PlegCH, FresCH, BiselCH, Contor, Avella, Retala, Expe, Rece, Galv, calidad from TC_PROYECTOS_ONLINE
                    join T_ORDFAB on TC_PROYECTOS_ONLINE.ORDFAB = T_ORDFAB.ORDFAB and T_ORDFAB.CODEMP = TC_PROYECTOS_ONLINE.codemp
                    where (TC_PROYECTOS_ONLINE.CODEMP = @empresa) and (T_ORDFAB.TIPOREG = 'F')  and TC_PROYECTOS_ONLINE.proyecto = @codlanza AND
                    (TC_PROYECTOS_ONLINE.FLAG = CASE WHEN @FiltroEstadoOF = 0 THEN '0' WHEN @FiltroEstadoOF = 1 THEN '1' ELSE TC_PROYECTOS_ONLINE.FLAG END)
					AND (ISNULL ((SELECT        TOP (1) T_ARTICULOS_1.CATEGORIA FROM            T_ORDFABM AS T_ORDFABM_1 INNER JOIN
                    T_ARTICULOS AS T_ARTICULOS_1 ON T_ORDFABM_1.CODEMP = T_ARTICULOS_1.CodEMP AND T_ORDFABM_1.CODIGO = T_ARTICULOS_1.CODIGO
                    WHERE        (T_ORDFABM_1.CODEMP = TC_PROYECTOS_ONLINE.CODEMP) AND (T_ORDFABM_1.ORDFAB = TC_PROYECTOS_ONLINE.ORDFAB) AND (T_ORDFABM_1.CTDENL > 0) AND (T_ARTICULOS_1.FAMILIA = 'MP')), '') 
                    IN (CASE WHEN @M1 = 1 THEN 'M1' END, CASE WHEN @M2 = 1 THEN 'M2' END, CASE WHEN @M3 = 1 THEN 'M3' END, CASE WHEN @M4 = 1 THEN 'M4' END, CASE WHEN @M5 = 1 THEN 'M5' END, 
                    CASE WHEN @CS = 1 THEN '' END))  AND (@codigo <> '' AND TC_PROYECTOS_ONLINE.CODIGO LIKE @codigo + '%' OR @codigo = ''))
					as T_ORDFAB_TEMP where (@EXPE = 1 and (Expe = CTDLAN)) or (@EXPE = 0 and (Expe <> CTDLAN OR Expe IS NULL)) or (@EXPE = 3 and (Expe IS NULL))  or (@EXPE = 2) ";

                comando = new SqlCommand(strsql, conexion);
                comando.CommandTimeout = 900;
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






    }
}
