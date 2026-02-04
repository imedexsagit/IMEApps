using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;
using System.IO;



namespace Intranet
{
    public class AccesoServicioPlanos
    {
        public string empresa;
        public string path;
        public string usuario;



        public AccesoServicioPlanos()
        {
            empresa = ConfigurationManager.AppSettings["EMPRESA"].ToString();
            path = ConfigurationManager.AppSettings["url_DestinoPlanos"].ToString().ToUpper();
            usuario = Environment.UserName.ToUpper();            
        }



        public bool CopiarPlano(string codigo,string pathdestino)
        {            
            string resultado;
            bool salida;
            WebService_Planos.ServiceSoapClient servicio = new WebService_Planos.ServiceSoapClient();
                                            
            resultado = servicio.ObtenerFile(codigo, empresa, usuario);
            if (resultado.StartsWith("-1"))
                salida = false;
            else
            {
                if (!Directory.Exists(pathdestino))
                    Directory.CreateDirectory(pathdestino);                
                if (!File.Exists(pathdestino + "\\" + codigo + ".pdf"))
                    File.Copy(path + "\\" + usuario + "\\" + resultado, pathdestino + "\\" + codigo + ".pdf");                   
                salida = true;
            }
            servicio.Close();
            return salida;
        }

        //Carlos Sanchez - 26/01/2022 - Metodo igual que el anterior, pero creando el pdf con la "coletilla" SINMODIFICAR, para diferenciarlos de los nuevos, sin la ultima hoja, en planos pedidos.
        public bool CopiarPlanoNuevo(string codigo, string pathdestino)
        {
            string resultado;
            bool salida;
            WebService_Planos.ServiceSoapClient servicio = new WebService_Planos.ServiceSoapClient();

            resultado = servicio.ObtenerFile(codigo, empresa, usuario);
            if (resultado.StartsWith("-1"))
                salida = false;
            else
            {
                if (!Directory.Exists(pathdestino))
                    Directory.CreateDirectory(pathdestino);
                if (!File.Exists(pathdestino + "\\" + codigo + "SINMODIFICAR.pdf"))
                    File.Copy(path + "\\" + usuario + "\\" + resultado, pathdestino + "\\" + codigo + "SINMODIFICAR.pdf");
                salida = true;
            }
            servicio.Close();
            return salida;
        }


    }
}
