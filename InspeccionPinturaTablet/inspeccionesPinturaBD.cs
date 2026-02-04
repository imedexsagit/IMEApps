using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using empresaGlobalProj;

namespace InspeccionPinturaTablet
{
    class inspeccionesPinturaBD
    {
        public string empresa;                
        SqlConnection conexion;

        SqlConnection conexionapp05;

        public inspeccionesPinturaBD()
        {
            //JRM - Cambiar Empresa cambio ConfigurationManager.AppSettings["EMPRESA"].ToString(); por empresaGlobal.empresaID; 
            empresa = empresaGlobal.empresaID; //ConfigurationManager.AppSettings["EMPRESA"].ToString();                        
            conexion = new SqlConnection(Utils.CD.getConexion());
            conexionapp05 = new SqlConnection("Data Source=srvapps05;Initial Catalog=business;uid=gg;pwd=ostia;MultipleActiveResultSets=True");
            //conexion = new SqlConnection("Data Source=srvdesarrollo;Initial Catalog=gg;uid=gg;pwd=ostia;MultipleActiveResultSets=True");
        }


        public DataTable obtenerInspecciones()
        {
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;
            

            try
            {
                
                conexion.Open();

                strsql = "";
                strsql = strsql + " select certificado,finalizada from TC_INSP_PINTURA";
                strsql = strsql + " WHERE   CODEMP = '" + empresa + "' order by pedido, contenedor ASC ";
              
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

        public DataTable obtenerDatosInspeccion(string certificado)
        {
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;


            try
            {

                conexion.Open();

                strsql = "";
                strsql = strsql + " select * from TC_INSP_PINTURA";
                strsql = strsql + " WHERE   CODEMP = '" + empresa + "'";
                strsql = strsql + " AND   certificado = '" + certificado + "'";

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

        public void crearDatosInspeccion(string cliente, int pedido, int packinglist, int contenedor, string plancalidad, string certificado
           ,string proyecto,  string torre ,string fecha, string inspector, string lugar )
        {
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;


            try
            {

                conexion.Open();

                strsql = "";
                strsql = strsql + " insert into TC_INSP_PINTURA";
                strsql = strsql + " VALUES ('" + empresa + "'," + pedido + "," + packinglist + "," + contenedor + ",";
                strsql = strsql + "'" + cliente + "',";
                strsql = strsql + "'" + proyecto + "',";
                 strsql = strsql + "'" + torre + "',";
                 strsql = strsql + "'" + plancalidad + "',";
                 strsql = strsql + "'" + fecha + "',";
                 strsql = strsql + "'" + certificado + "',";
                 strsql = strsql + "'" + inspector + "',";
                 strsql = strsql + "'" + lugar + "',0)";

               
                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                strsql = "";
                strsql = strsql + " insert into TC_INSP_PINTURA_1INSPQ (codemp,certificado) ";
                strsql = strsql + " VALUES ('" + empresa + "','" + certificado + "')";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                strsql = "";
                strsql = strsql + " insert into TC_INSP_PINTURA_2INSGRN (codemp,certificado,zincinicialvalor1,zincinicialvalor2,zincinicialvalor3,zincinicialvalor4,zincinicialvalor5, ";
                strsql = strsql + " zincgranalladovalor1,zincgranalladovalor2,zincgranalladovalor3,zincgranalladovalor4,zincgranalladovalor5) ";
                strsql = strsql + " VALUES ('" + empresa + "','" + certificado + "',0,0,0,0,0,0,0,0,0,0)";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                strsql = "";
                strsql = strsql + " insert into TC_INSP_PINTURA_3RUGLSUP (codemp,certificado) ";
                strsql = strsql + " VALUES ('" + empresa + "','" + certificado + "')";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                strsql = "";
                strsql = strsql + " insert into TC_INSP_PINTURA_4RECUB (codemp,certificado) ";
                strsql = strsql + " VALUES ('" + empresa + "','" + certificado + "')";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                strsql = "";
                strsql = strsql + " insert into TC_INSP_PINTURA_5ESP (codemp,certificado) ";
                strsql = strsql + " VALUES ('" + empresa + "','" + certificado + "')";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

   
                strsql = "";
                strsql = strsql + "  INSERT INTO TC_INSP_PINTURA_5ESP_PAQUETES  ";
                strsql = strsql + " select 3,TPIN.certificado,TPAQ.PACKING, TPAQ.PAQUETE, 0,0,0,1  from TC_INSP_PINTURA TPIN ";
                strsql = strsql + "  join TC_PACKING_LIST_CONTENEDORES_BULTOS TPAQ";
                strsql = strsql + "  ON TPIN.packing = TPAQ.PACKING";
                strsql = strsql + "  WHERE TPIN.certificado='" + certificado + "' AND TPAQ.contenedor= '"+ contenedor + "'";
               
                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                strsql = "";
                strsql = strsql + "  INSERT INTO TC_INSP_PINTURA_5ESP_GALVANIZADO  ";
                strsql = strsql + " select 3,TPIN.certificado,TPAQ.PACKING, TPAQ.PAQUETE, 0,0,0,1  from TC_INSP_PINTURA TPIN ";
                strsql = strsql + "  join TC_PACKING_LIST_CONTENEDORES_BULTOS TPAQ";
                strsql = strsql + "  ON TPIN.packing = TPAQ.PACKING";
                strsql = strsql + "  WHERE TPIN.certificado='" + certificado + "' AND TPAQ.contenedor= '" + contenedor + "'";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                strsql = "";
                strsql = strsql + "  INSERT INTO TC_INSP_PINTURA_5ESP_PAQUETES  ";
                strsql = strsql + " select 3,TPIN.certificado,TPAQ.PACKING, TPAQ.PAQUETE, 0,0,0,2  from TC_INSP_PINTURA TPIN ";
                strsql = strsql + "  join TC_PACKING_LIST_CONTENEDORES_BULTOS TPAQ";
                strsql = strsql + "  ON TPIN.packing = TPAQ.PACKING";
                strsql = strsql + "  WHERE TPIN.certificado='" + certificado + "' AND TPAQ.contenedor= '" + contenedor + "'";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

           
                strsql = "";
                strsql = strsql + " insert into TC_INSP_PINTURA_6FRECUB (codemp,certificado) ";
                strsql = strsql + " VALUES ('" + empresa + "','" + certificado + "')";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                strsql = "";
                strsql = strsql + " insert into TC_INSP_PINTURA_7OBS (codemp,certificado) ";
                strsql = strsql + " VALUES ('" + empresa + "','" + certificado + "')";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                strsql = "";
                strsql = strsql + " insert into TC_INSP_PINTURA_8CARGA (codemp,certificado) ";
                strsql = strsql + " VALUES ('" + empresa + "','" + certificado + "')";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                conexion.Close();
                MessageBox.Show("CAMBIOS GUARDADOS CORRECTAMENTE");
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR AL CREAR LA INSPECCION. POR FAVOR, CONSULTE CON EL DEPARTEMENTO DE INFORMATICA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           
        }

        public void actulizarDatosInspeccion(string cliente, int pedido, int packinglist, int contenedor, string plancalidad, string certificado
           , string proyecto, string torre, string fecha, string inspector, string lugar,int finalizada)
        {
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;


            try
            {

                conexion.Open();

                strsql = "";
                strsql = strsql + " UPDATE TC_INSP_PINTURA SET ";
                strsql = strsql + " cliente='" + cliente + "',";
                strsql = strsql + " proyecto='" + proyecto + "',";
                strsql = strsql + " pedido='" + pedido + "',";
                strsql = strsql + " packing='" + packinglist + "',";
                strsql = strsql + " contenedor='" + contenedor + "',";
                strsql = strsql + " torre='" + torre + "',";
                strsql = strsql + " plancalidad='" + plancalidad + "',";
                strsql = strsql + " fecha='" + fecha + "',";
                strsql = strsql + " inspector='" + inspector + "',";
                strsql = strsql + " finalizada='" + finalizada + "',";
                strsql = strsql + " lugar='" + lugar + "' where certificado='"+certificado+"' ";


                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                conexion.Close();
                MessageBox.Show("CAMBIOS GUARDADOS CORRECTAMENTE");
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR AL ACTULIZAR LA CABECERA.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }



        public void eliminarDatosInspeccion(string certificado)
        {
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;

            try
            {

                conexion.Open();

                strsql = "";
                strsql = strsql + " delete from TC_INSP_PINTURA_1INSPQ WHERE ";
                strsql = strsql + " certificado ='" + certificado +"' ";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                strsql = "";
                strsql = strsql + " delete from TC_INSP_PINTURA_2INSGRN WHERE ";
                strsql = strsql + " certificado ='" + certificado + "' ";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                strsql = "";
                strsql = strsql + " delete from TC_INSP_PINTURA_3RUGLSUP WHERE ";
                strsql = strsql + " certificado ='" + certificado + "' ";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                strsql = "";
                strsql = strsql + " delete from TC_INSP_PINTURA_4RECUB WHERE ";
                strsql = strsql + " certificado ='" + certificado + "' ";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                strsql = "";
                strsql = strsql + " delete from TC_INSP_PINTURA_5ESP WHERE ";
                strsql = strsql + " certificado ='" + certificado + "' ";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                strsql = "";
                strsql = strsql + " delete from TC_INSP_PINTURA_5ESP_PAQUETES WHERE ";
                strsql = strsql + " certificado ='" + certificado + "' ";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                strsql = "";
                strsql = strsql + " delete from TC_INSP_PINTURA_6FRECUB WHERE ";
                strsql = strsql + " certificado ='" + certificado + "' ";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                strsql = "";
                strsql = strsql + " delete from TC_INSP_PINTURA_7OBS WHERE ";
                strsql = strsql + " certificado ='" + certificado + "' ";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                strsql = "";
                strsql = strsql + " delete from TC_INSP_PINTURA_8CARGA WHERE ";
                strsql = strsql + " certificado ='" + certificado + "' ";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                strsql = "";
                strsql = strsql + " delete from TC_INSP_PINTURA WHERE ";
                strsql = strsql + " certificado ='" + certificado + "' ";

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


                MessageBox.Show(ex.Message, "ERROR AL ACTULIZAR LA CABECERA.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        public DataTable datos1InsPaquetes(string certificado)
        {
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;


            try
            {

                conexion.Open();

                strsql = "";
                strsql = strsql + " select * from TC_INSP_PINTURA_1INSPQ";
                strsql = strsql + " WHERE   CODEMP = '" + empresa + "'";
                strsql = strsql + " AND   certificado = '" + certificado + "'";

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

        public void actulizar1InsPaquetes(string certificado, int plcorrecto, string plreclamacion
         ,  int cpcorrecto, string cpreclamacion,  int apcorrecto, string apreclamacion, string fecha)
        {
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;


            try
            {

                conexion.Open();

                strsql = "";
                strsql = strsql + " UPDATE TC_INSP_PINTURA_1INSPQ SET ";
                
                strsql = strsql + " plcorrecto=" + plcorrecto + ", "; 
                strsql = strsql + " plreclamacion='" + plreclamacion + "',";
                 strsql = strsql + " cpcorrecto='" + cpcorrecto + "',";
                 strsql = strsql + " cpreclamacion='" + cpreclamacion + "',";
                 strsql = strsql + " apcorrecto='" + apcorrecto + "',";
                 strsql = strsql + " apreclamacion='" + apreclamacion + "',";
                 strsql = strsql + " fecha='" + fecha + "' where certificado='" + certificado + "' ";


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


                MessageBox.Show(ex.Message, "ERROR AL ACTULIZAR SECCIÓN 1", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        public DataTable datos2InsGranallado(string certificado)
        {
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;


            try
            {

                conexion.Open();

                strsql = "";
                strsql = strsql + " select * from TC_INSP_PINTURA_2INSGRN";
                strsql = strsql + " WHERE   CODEMP = '" + empresa + "'";
                strsql = strsql + " AND   certificado = '" + certificado + "'";

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

        public void actulizar2InsGranallado(string certificado, int aecorrecto, string aereclamacion, int aeapicable
        , int gecorrecto, string gereclamacion, int geapicable, int zincISO, string zincinicio, string zincfinal,string fecha,
            string zincincialpieza1, string zincincialpieza2, string zincincialpieza3,
            int zincincialvalor1, int zincincialvalor2, int zincincialvalor3, 
            string zincgranalladopieza1, string zincgranalladopieza2, string zincgranalladopieza3,
            int zincgranalladovalor1, int zincgranalladovalor2, int zincgranalladovalor3, int wasserpel,
            int bresle, int compresores)
        {
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;


            try
            {

                conexion.Open();

                strsql = "";
                strsql = strsql + " UPDATE TC_INSP_PINTURA_2INSGRN SET ";
                strsql = strsql + " aecorrecto=" + aecorrecto + ", ";
                strsql = strsql + " aereclamacion='" + aereclamacion + "',";
                strsql = strsql + " aeaplicable='" + aeapicable + "',";
                strsql = strsql + " gecorrecto='" + gecorrecto + "',";
                strsql = strsql + " gereclamacion='" + gereclamacion + "',";
                strsql = strsql + " geaplicable='" + geapicable + "',";
                strsql = strsql + " zincISO='" + zincISO + "',";
                strsql = strsql + " zincinicial='" + zincinicio + "',";
                strsql = strsql + " zincfinal='" + zincfinal + "',";

                strsql = strsql + " zincinicialpieza1='" + zincincialpieza1 + "',";
                strsql = strsql + " zincinicialpieza2='" + zincincialpieza2 + "',";
                strsql = strsql + " zincinicialpieza3='" + zincincialpieza3 + "',";
               
                strsql = strsql + " zincinicialvalor1='" + zincincialvalor1 + "',";
                strsql = strsql + " zincinicialvalor2='" + zincincialvalor2 + "',";
                strsql = strsql + " zincinicialvalor3='" + zincincialvalor3 + "',";
               
                strsql = strsql + " zincgranalladopieza1='" + zincgranalladopieza1 + "',";
                strsql = strsql + " zincgranalladopieza2='" + zincgranalladopieza2 + "',";
                strsql = strsql + " zincgranalladopieza3='" + zincgranalladopieza3 + "',";
              

                strsql = strsql + " zincgranalladovalor1='" + zincgranalladovalor1 + "',";
                strsql = strsql + " zincgranalladovalor2='" + zincgranalladovalor2 + "',";
                strsql = strsql + " zincgranalladovalor3='" + zincgranalladovalor3 + "',";

                strsql = strsql + " wasserpel='" + wasserpel + "',";
                strsql = strsql + " bresle='" + bresle + "',";
                strsql = strsql + " compresores='" + compresores + "',";
                
                strsql = strsql + " fecha='" + fecha + "' where certificado='" + certificado + "'";

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


                MessageBox.Show(ex.Message, "ERROR AL ACTULIZAR EL PUNTO 2.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        public DataTable datos3RuglSup(string certificado)
        {
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;

            try
            {

                conexion.Open();

                strsql = "";
                strsql = strsql + " select * from TC_INSP_PINTURA_3RUGLSUP";
                strsql = strsql + " WHERE   CODEMP = '" + empresa + "'";
                strsql = strsql + " AND   certificado = '" + certificado + "'";


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

    
        public void actulizar3InsTest(string certificado, int test, string pieza, int evcantidad)
        {
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;

            try
            {

                conexion.Open();

                strsql = "";
                strsql = strsql + " UPDATE TC_INSP_PINTURA_3RUGLSUP SET ";
                strsql = strsql + " test" + test + "pieza='" + pieza + "', ";
                strsql = strsql + " test" + test + "evcantidad=" + evcantidad + " ";
                strsql = strsql + "  where certificado='" + certificado + "' ";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                conexion.Close();
                MessageBox.Show("CAMBIOS GUARDADOS CORRECTAMENTE");
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR AL ACTULIZAR LOS DATOS DE LOS TEST DEL APARTADO 3.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void actulizar3RuglSup(string certificado, int cscorrecto, int  rscorrecto,
              int  tpcorrecto,
              int csaplicable,
              int  tpaplicable,
            string csreclamacion,
            string cspiezaensayada,
            string csequipoensayo,
            string csvalor,
            string rsreclamacion,
            string rspiezaensayada,
            string rsequipoensayo,
            string rsvalor,
            string tpreclamacion,
            string cntadhva,
            string fondoconst,
            string fecha,
            string rsnserie,
            int limpieza,
            int tension,
            int perla,
            int ensayo,
            string tenSupP1,
            string tenSupP2,
            string tenSupP3)

        {
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;


            try
            {

                conexion.Open();

                strsql = "";
                strsql = strsql + " UPDATE TC_INSP_PINTURA_3RUGLSUP SET ";
                strsql = strsql + " cscorrecto='" + cscorrecto + "',";
                strsql = strsql + " rscorrecto='" + rscorrecto + "',";
                strsql = strsql + " tpcorrecto='" + tpcorrecto + "',";
                strsql = strsql + " csaplicable='" + csaplicable + "',";
                strsql = strsql + " tpaplicable='" + tpaplicable + "',";
                strsql = strsql + " csreclamacion='" + csreclamacion + "',";
                strsql = strsql + " cspiezaensayada='" + cspiezaensayada + "',";
                strsql = strsql + " csequipoensayo='" + csequipoensayo + "',";
                strsql = strsql + " csvalor='" + csvalor + "',";
                strsql = strsql + " rsreclamacion='" + rsreclamacion + "',";
                strsql = strsql + " rspiezaensayada='" + rspiezaensayada + "',";
                strsql = strsql + " rsequipoensayo='" + rsequipoensayo + "',";
                strsql = strsql + " rsvalor='" + rsvalor + "',";
                strsql = strsql + " tpreclamacion='" + tpreclamacion + "',";
                strsql = strsql + " cntadhva='" + cntadhva + "',";
                strsql = strsql + " fondoconst='" + fondoconst + "',";
                strsql = strsql + " fecha='" + fecha + "', ";
                strsql = strsql + " rsnserie='" + rsnserie + "',";
                strsql = strsql + " limpieza='" + limpieza + "',";
                strsql = strsql + " tension='" + tension + "',";
                strsql = strsql + " perla='" + perla + "',";
                strsql = strsql + " ensayo='" + ensayo + "', ";
                strsql = strsql + " tenSupP1='" + tenSupP1 + "',";
                strsql = strsql + " tenSupP2='" + tenSupP2 + "',";
                strsql = strsql + " tenSupP3='" + tenSupP3 + "' ";
                strsql = strsql + "  where certificado='" + certificado + "'";  


                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                conexion.Close();
                MessageBox.Show("CAMBIOS GUARDADOS CORRECTAMENTE");
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR AL ACTULIZAR LOS DATOS DE LOS TEST DEL APARTADO 3.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public DataTable datos4InsRecubrimiento(string certificado)
        {
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;

            try
            {

                conexion.Open();

                strsql = "";
                strsql = strsql + " select * from TC_INSP_PINTURA_4RECUB";
                strsql = strsql + " WHERE   CODEMP = '" + empresa + "'";
                strsql = strsql + " AND   certificado = '" + certificado + "'";

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

        public void actulizar4InsRecubrimiento(string certificado, int recorrecto, string rereclamacion, string tipopintura1,
            string lote1, string color1, string fechaaplicacion1, string tipopintura2, string lote2, string color2, string fechaaplicacion2,
            string tipopintura3, string lote3, string color3, string fechaaplicacion3, int secado, string secadoreclamacion, string fecha)
        {
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;


            try
            {

                conexion.Open();

                strsql = "";
                strsql = strsql + " UPDATE TC_INSP_PINTURA_4RECUB SET ";
                strsql = strsql + " recorrecto=" + recorrecto + ", ";
                strsql = strsql + " rereclamacion='" + rereclamacion + "',";
                strsql = strsql + " tipopintura1='" + tipopintura1 + "',";
                strsql = strsql + " lote1='" + lote1 + "',";
                strsql = strsql + " color1='" + color1 + "',";
                strsql = strsql + " fechaplicacion1='" + fechaaplicacion1 + "',";
                strsql = strsql + " tipopintura2='" + tipopintura2 + "',";
                strsql = strsql + " lote2='" + lote2 + "',";
                strsql = strsql + " color2='" + color2 + "',";
                strsql = strsql + " fechaplicacion2='" + fechaaplicacion2 + "',";
                strsql = strsql + " tipopintura3='" + tipopintura3 + "',";
                strsql = strsql + " lote3='" + lote3 + "',";
                strsql = strsql + " color3='" + color3 + "',";
                strsql = strsql + " fechaplicacion3='" + fechaaplicacion3 + "',";
                strsql = strsql + " secado=" + recorrecto + ", ";
                strsql = strsql + " secadoreclamacion='" + secadoreclamacion + "',";
                strsql = strsql + " fecha='" + fecha + "' where certificado='" + certificado + "'";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                conexion.Close();
                MessageBox.Show("CAMBIOS GUARDADOS CORRECTAMENTE");
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR AL ACTULIZAR EL PUNTO 4.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        public DataTable datos5Espesor(string certificado)
        {
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;

            try
            {

                conexion.Open();

                strsql = "";
                strsql = strsql + " select * from TC_INSP_PINTURA_5ESP";
                strsql = strsql + " WHERE   CODEMP = '" + empresa + "'";
                strsql = strsql + " AND   certificado = '" + certificado + "'";

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

        public void actulizar5Espesor(string certificado, int eschcorrecto, int eschapicable, string eschreclamacion, string galvanizado,
            string capa2, string capa3, string valorpeine, string eschequipoEnsayo, int escscorrecto, string escsreclamacion, string min1, string med1,
        string max1,string min2, string med2, string max2,string min3, string med3, string max3,string escsequipoEnsayo, string fecha) //, float min2, float med2, float max2, float min3, float med3, float max3, string eschequipoEnsayo, string fecha, string valorpeine)
        {
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;


            try
            {

                conexion.Open();

                strsql = "";
                strsql = strsql + " UPDATE TC_INSP_PINTURA_5ESP SET ";
                strsql = strsql + " eschcorrecto=" + eschcorrecto + ", ";
                strsql = strsql + " eschreclamacion='" + eschreclamacion + "',";
                strsql = strsql + " eschaplicable='" + eschapicable + "', ";
                strsql = strsql + " galvanizado='" + galvanizado + "',";
                strsql = strsql + " capa2='" + capa2+ "',";
                strsql = strsql + " capa3='" + capa3 + "',";
                strsql = strsql + " valorpeine='" + valorpeine + "', ";
                strsql = strsql + " eschequipoEnsayo='" + eschequipoEnsayo + "',";
                strsql = strsql + " escscorrecto='" + escscorrecto + "',";
                strsql = strsql + " escsreclamacion='" + escsreclamacion + "', ";
                strsql = strsql + " capa1min= ' "+ min1 + "',";
                strsql = strsql + " capa1med='" + med1 + "',";
                strsql = strsql + " capa1max='" + max1 + "', ";
                strsql = strsql + " capa2min= ' " + min2 + "',";
                strsql = strsql + " capa2med='" + med2 + "',";
                strsql = strsql + " capa2max='" + max2 + "', ";
                strsql = strsql + " capa3min= ' " + min3 + "',";
                strsql = strsql + " capa3med='" + med3 + "',";
                strsql = strsql + " capa3max='" + max3 + "', ";
                strsql = strsql + " escsequipoEnsayo='" + escsequipoEnsayo + "',";
                strsql = strsql + " fecha='" + fecha + "' where certificado='" + certificado + "'";
        

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                conexion.Close();
                MessageBox.Show("CAMBIOS GUARDADOS CORRECTAMENTE");
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR AL ACTULIZAR EL PUNTO 4.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        public DataTable datos5EspesorPaquetes(string certificado, int w)
        {
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;

            try
            {

                conexion.Open();

                strsql = "";
                strsql = strsql + " select distinct * from TC_INSP_PINTURA_5ESP_PAQUETES";
                strsql = strsql + " WHERE   CODEMP = '" + empresa + "'";
                strsql = strsql + " AND   certificado = '" + certificado + "' and w="+w;

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

        public DataTable datos5EspesorGalvanizado(string certificado, int w)
        {
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;

            try
            {

                conexion.Open();

                strsql = "";
                strsql = strsql + " select distinct * from TC_INSP_PINTURA_5ESP_GALVANIZADO";
                strsql = strsql + " WHERE   CODEMP = '" + empresa + "'";
                strsql = strsql + " AND   certificado = '" + certificado + "' and w=" + w;

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
        public DataTable datosa6InspFinalPinturaRecubrimiento(string certificado)
        {
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;

            try
            {

                conexion.Open();

                strsql = "";
                strsql = strsql + " select * from TC_INSP_PINTURA_6FRECUB";
                strsql = strsql + " WHERE   CODEMP = '" + empresa + "'";
                strsql = strsql + " AND   certificado = '" + certificado + "'";

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

        public void actulizar6InspFinalPinturaRecubrimiento(string certificado, int apcorrecto, int escorrecto, int tacorrecto, int iecorrecto,
            string apreclamacion, string esreclamacion, string gradosecado, string esmetodo,  string esequipoensayo, string tareclamacion,
            string tavalor, string tametodo, string taequipoensayo, string iereclamacion, int esaplicable, int taaplicable,
            string fecha)
        {
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;


            try
            {

                conexion.Open();

                strsql = "";
                strsql = strsql + " UPDATE TC_INSP_PINTURA_6FRECUB SET ";
                strsql = strsql + " apcorrecto=" + apcorrecto + ", ";
                strsql = strsql + " escorrecto=" + escorrecto + ", ";
                strsql = strsql + " tacorrecto=" + tacorrecto + ", ";
                strsql = strsql + " iecorrecto=" + iecorrecto + ", ";

                strsql = strsql + " apreclamacion='" + apreclamacion + "',";
                strsql = strsql + " esreclamacion='" + esreclamacion + "',";
                strsql = strsql + " iereclamacion='" + iereclamacion + "',";
                strsql = strsql + " gradosecado='" + gradosecado + "',";
                strsql = strsql + " esmetodo='" + esmetodo + "',";
                strsql = strsql + " esequipoensayo='" + esequipoensayo + "',";
                strsql = strsql + " tareclamacion='" + tareclamacion + "',";
                strsql = strsql + " tavalor='" + tavalor + "',";
                strsql = strsql + " tametodo='" + tametodo + "',";
                strsql = strsql + " taequipoensayo='" + taequipoensayo + "',";
                
                strsql = strsql + " esaplicable='" + esaplicable + "',";
                strsql = strsql + " taaplicable='" + taaplicable + "',";
                
                strsql = strsql + " fecha='" + fecha + "' where certificado='" + certificado + "'";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                conexion.Close();
                MessageBox.Show("CAMBIOS GUARDADOS CORRECTAMENTE");
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR AL ACTULIZAR EL PUNTO 4.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }


        public DataTable datosa7Comentarios(string certificado)
        {
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;

            try
            {

                conexion.Open();

                strsql = "";
                strsql = strsql + " select * from TC_INSP_PINTURA_7OBS";
                strsql = strsql + " WHERE   CODEMP = '" + empresa + "'";
                strsql = strsql + " AND   certificado = '" + certificado + "'";

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

        public void actulizar7Observaciones(string certificado, string obs, string fecha)
        {
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;


            try
            {

                conexion.Open();

                strsql = "";
                strsql = strsql + " UPDATE TC_INSP_PINTURA_7OBS SET ";
                strsql = strsql + " comentarios='" + obs + "', ";
                strsql = strsql + " fecha='" + fecha + "' where certificado='" + certificado + "'";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                conexion.Close();
                MessageBox.Show("CAMBIOS GUARDADOS CORRECTAMENTE");
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR AL ACTULIZAR EL PUNTO 4.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DataTable obtenerEquiposMade()
        {
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;


            try
            {

                conexionapp05.Open();

                strsql = "";
                strsql = strsql + " select NEQUIPO FROM EQUIPOSINSPECCION ";

                comando = new SqlCommand(strsql, conexionapp05);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                conexionapp05.Close();
            }
            catch (Exception ex)
            {
                if (conexionapp05.State == ConnectionState.Open)
                    conexionapp05.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return table;
        }

        public DataTable datosa8InspCarga(string certificado)
        {
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;

            try
            {

                conexion.Open();

                strsql = "";
                strsql = strsql + " select * from TC_INSP_PINTURA_8CARGA";
                strsql = strsql + " WHERE   CODEMP = '" + empresa + "'";
                strsql = strsql + " AND   certificado = '" + certificado + "'";

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

        public void actulizar8Especificaciones(string certificado, string titulo1, int resultado1, string nota1,
        string titulo2, int resultado2, string nota2,string titulo3, int resultado3, string nota3,string titulo4, int resultado4, string nota4)
        {
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;


            try
            {

                conexion.Open();

                strsql = "";
                strsql = strsql + " UPDATE TC_INSP_PINTURA_8CARGA SET ";
                strsql = strsql + " especifiTitulo1='" + titulo1 + "', ";
                strsql = strsql + " especifiResult1='" + resultado1 + "', ";
                strsql = strsql + " especifiNote1='" + nota1 + "', ";
                strsql = strsql + " especifiTitulo2='" + titulo2 + "', ";
                strsql = strsql + " especifiResult2='" + resultado2 + "', ";
                strsql = strsql + " especifiNote2='" + nota2 + "', ";
                strsql = strsql + " especifiTitulo3='" + titulo3 + "', ";
                strsql = strsql + " especifiResult3='" + resultado3 + "', ";
                strsql = strsql + " especifiNote3='" + nota3 + "', ";
                strsql = strsql + " especifiTitulo4='" + titulo4 + "', ";
                strsql = strsql + " especifiResult4='" + resultado4 + "', ";
                strsql = strsql + " especifiNote4='" + nota4 + "' ";
                strsql = strsql + "  where certificado='" + certificado + "'";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                conexion.Close();
                MessageBox.Show("CAMBIOS GUARDADOS CORRECTAMENTE");
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR AL ACTULIZAR EL PUNTO 8.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void actulizar8InspFinal(string certificado, int contaguaResult, string conteaguaNote, int contelimpiezaResult, string contelimpiezaNote,
            int embalajeResult, string embalajeNote, int estabilidadaResult, string estabilidadNote, int dañadasResult, string dañadasNote,
            int identificadoResult, string identificadoNote, int tmfitosanitariosResult, string tmfitosanitariosNote, int tmpaqueteResult,
            string tmpaqueteResultNote, int sinmanchasResult, string sinmanchasNote, int sinzonasdesnudasResult, string sinzonasdesnudasNote,
            int sinpinchosResult, string sinpinchosNote, int sincenizasResult, string sincenizasNote, int contenedorCorrecto, string pnc,string fecha)
        {
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;


            try
            {

                conexion.Open();

                strsql = "";
                strsql = strsql + " UPDATE TC_INSP_PINTURA_8CARGA SET ";
                strsql = strsql + " contaguaResult='" + contaguaResult + "', ";
                strsql = strsql + " conteaguaNote='" + conteaguaNote + "', ";
                strsql = strsql + " contelimpiezaResult='" + contelimpiezaResult + "', ";
                strsql = strsql + " contelimpiezaNote='" + contelimpiezaNote + "', ";

                strsql = strsql + " embalajeResult='" + embalajeResult + "', ";
                strsql = strsql + " embalajeNote='" + embalajeNote + "', ";
                strsql = strsql + " estabilidadResult='" + estabilidadaResult + "', ";
                strsql = strsql + " estabilidadNote='" + estabilidadNote + "', ";
                strsql = strsql + " dañadasResult='" + dañadasResult + "', ";
                strsql = strsql + " dañadasNote='" + dañadasNote + "', ";
                strsql = strsql + " identificadoResult='" + identificadoResult + "', ";
                strsql = strsql + " identificadoNote='" + identificadoNote + "', ";

                strsql = strsql + " tmfitosanitariosResult='" + tmfitosanitariosResult + "', ";
                strsql = strsql + " tmfitosanitariosNote='" + tmfitosanitariosNote + "', ";
                strsql = strsql + " tmpaqueteResult='" + tmpaqueteResult + "', ";
                strsql = strsql + " tmpaqueteNote='" + tmpaqueteResultNote + "', ";

                strsql = strsql + " sinmanchasResult='" + sinmanchasResult + "', ";
                strsql = strsql + " sinmanchasNote='" + sinmanchasNote + "', ";
                strsql = strsql + " sinzonasdesnudasResult='" + sinzonasdesnudasResult + "', ";
                strsql = strsql + " sinzonasdesnudasNote='" + sinzonasdesnudasNote + "', ";
                strsql = strsql + " sinpinchosResult='" + sinpinchosResult + "', ";
                strsql = strsql + " sinpinchosNote='" + sinpinchosNote + "', ";
                strsql = strsql + " sincenizasResult='" + sincenizasResult + "', ";
                strsql = strsql + " sincenizasNote='" + sincenizasNote + "', ";

                strsql = strsql + " contenedorCorrecto='" + contenedorCorrecto + "', ";
                strsql = strsql + " pnc='" + pnc + "',";
                strsql = strsql + " fecha='" + fecha + "' where certificado='" + certificado + "'";
                

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                conexion.Close();
                MessageBox.Show("CAMBIOS GUARDADOS CORRECTAMENTE");
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR AL ACTULIZAR EL PUNTO 8.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DataTable obtenerColoresPintura()
        {
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;


            try
            {

                conexion.Open();

                strsql = "";
                strsql = strsql + " select distinct COLOR from TC_COLORES_PINTURA ";


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

        public DataTable obtenerTiposPintura()
        {
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;


            try
            {

                conexion.Open();

                strsql = "";
                strsql = strsql + " select distinct TIPO_PINTURA from TC_TIPOS_PINTURA ";


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
    }
}
