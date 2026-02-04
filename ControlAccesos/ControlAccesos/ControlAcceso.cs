using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using empresaGlobalProj;

namespace ControlAccesos
{
    public class ControlAcceso
    {
        private ConsultasBD consultasBD;
        public string Empresa { get; private set; }
        public string Usuario { get; private set; }

        public ControlAcceso()
        {
            consultasBD= new ConsultasBD();
            //JRM - Cambiar Empresa cambio "3" por empresaGlobal.empresaID
            Empresa = empresaGlobal.empresaID;//"3";
            Usuario = Environment.UserName;
        }

        public bool TieneAcceso(string aplicacion)
        {
            return consultasBD.TienePermisos(Empresa, aplicacion, Usuario);
        }


    }
}
