using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using empresaGlobalProj;
using System.Data;

namespace CamionesGalvanizado
{
    class DBUtils
    {
        public string empresa;                
        SqlConnection conexion;

        public DBUtils()
        {

            empresa = empresaGlobal.empresaID; //ConfigurationManager.AppSettings["EMPRESA"].ToString();                        
            conexion = new SqlConnection(Utils.CD.getConexion());
            //conexion = new SqlConnection("Data Source=srvdesarrollo;Initial Catalog=gg;uid=gg;pwd=ostia;MultipleActiveResultSets=True");
        }

        public DataTable obtenerAlmacenes(string codBarras)
        {
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;


            try
            {

                conexion.Open();

                strsql = "";
                strsql = strsql + " select CODIGO as MARCA,CANTIDAD,t1.CODLANZA AS PROYECTO, CAMION, id AS PAQUETE, fecha_fin AS SEMANA from ( ";
                strsql = strsql + "(SELECT        Dense_Rank() OVER (ORDER BY imedexsa_intranet.dbo.GL_ITEMSEXP.fch_creacion) AS id, gg.dbo.TC_V_LANZAMIENTO_ETIQ_PROYECTO.CODIGO, imedexsa_intranet.dbo.GL_ITEMSPAQ.cantidad, gg.dbo.TC_V_LANZAMIENTO_ETIQ_PROYECTO.CODLANZA,  ";
                strsql = strsql + "gg.dbo.TC_V_LANZAMIENTO_ETIQ_PROYECTO.PROYECTO ";
                strsql = strsql + "FROM            imedexsa_intranet.dbo.GL_ITEMSEXP INNER JOIN ";
                strsql = strsql + "                         gg.dbo.TC_V_LANZAMIENTO_ETIQ_PROYECTO INNER JOIN ";
                strsql = strsql + "                         imedexsa_intranet.dbo.GL_PAQUE INNER JOIN ";
                strsql = strsql + "                         imedexsa_intranet.dbo.GL_ITEMSPAQ ON imedexsa_intranet.dbo.GL_PAQUE.id = imedexsa_intranet.dbo.GL_ITEMSPAQ.paque_id ON gg.dbo.TC_V_LANZAMIENTO_ETIQ_PROYECTO.Etiqueta = imedexsa_intranet.dbo.GL_ITEMSPAQ.cb ON  ";
                strsql = strsql + "                         imedexsa_intranet.dbo.GL_ITEMSEXP.paque_id = imedexsa_intranet.dbo.GL_PAQUE.id ";
                strsql = strsql + "LEFT JOIN  gg.dbo.TC_PROYECTOS_PINTURA_MADE pintura on pintura.Proyecto = TC_V_LANZAMIENTO_ETIQ_PROYECTO.CODLANZA ";
                strsql = strsql + "WHERE        (imedexsa_intranet.dbo.GL_ITEMSEXP.exped_id = (select top (1) id from imedexsa_intranet.dbo.GL_EXPED where cb_num = '" + codBarras + "')) ";
                strsql = strsql + ") as t1 ";
                strsql = strsql + "JOIN ";
                strsql = strsql + "( ";
                strsql = strsql + "SELECT       ";
                strsql = strsql + "gg.dbo.TC_V_LANZAMIENTO_ETIQ_PROYECTO.CODLANZA, /*DATEPART(ISO_WEEK,min(ffinal))as fecha_fin*/ (SELECT SEMANA FROM GG.DBO.TC_LANZA_SEMANA WHERE CODLANZA = TC_V_LANZAMIENTO_ETIQ_PROYECTO.CODLANZA) as fecha_fin, camion ";
                strsql = strsql + "FROM            imedexsa_intranet.dbo.GL_ITEMSEXP INNER JOIN ";
                strsql = strsql + "                         gg.dbo.TC_V_LANZAMIENTO_ETIQ_PROYECTO INNER JOIN ";
                strsql = strsql + "                         imedexsa_intranet.dbo.GL_PAQUE INNER JOIN ";
                strsql = strsql + "                         imedexsa_intranet.dbo.GL_ITEMSPAQ ON imedexsa_intranet.dbo.GL_PAQUE.id = imedexsa_intranet.dbo.GL_ITEMSPAQ.paque_id ON gg.dbo.TC_V_LANZAMIENTO_ETIQ_PROYECTO.Etiqueta = imedexsa_intranet.dbo.GL_ITEMSPAQ.cb ON  ";
                strsql = strsql + "                         imedexsa_intranet.dbo.GL_ITEMSEXP.paque_id = imedexsa_intranet.dbo.GL_PAQUE.id ";
                strsql = strsql + "LEFT JOIN  gg.dbo.TC_PROYECTOS_PINTURA_MADE pintura on pintura.Proyecto = TC_V_LANZAMIENTO_ETIQ_PROYECTO.CODLANZA ";
                strsql = strsql + "left join gg.dbo.tc_lanza_pedlin on gg.dbo.TC_V_LANZAMIENTO_ETIQ_PROYECTO.CODLANZA = TC_LANZA_PEDLIN.CODLANZA ";
                strsql = strsql + "left join gg.dbo.T_ORDTERL on tc_lanza_pedlin.NUMPED = T_ORDTERL.NUMPED and tc_lanza_pedlin.lineap = T_ORDTERL.LINEA ";
                strsql = strsql + "left join imedexsa_intranet.dbo.GL_EXPED exped on exped.id = imedexsa_intranet.dbo.GL_ITEMSEXP.exped_id ";
                strsql = strsql + "WHERE        (imedexsa_intranet.dbo.GL_ITEMSEXP.exped_id = (select top (1) id from imedexsa_intranet.dbo.GL_EXPED where cb_num = '" + codBarras + "')) group by gg.dbo.TC_V_LANZAMIENTO_ETIQ_PROYECTO.CODLANZA, camion ";
                strsql = strsql + ") as t2 on t1.codlanza = t2.CODLANZA)  order by id, CODIGO ";

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
            }

            return table;
        }
    }
}
