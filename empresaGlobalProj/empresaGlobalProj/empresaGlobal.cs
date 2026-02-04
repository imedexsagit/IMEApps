using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace empresaGlobalProj
{
    public static class empresaGlobal
    {
        public static string empresaID = "3";
        public static string nombreEmpresa = "IMEDEXSA";
        public static bool showEmp = true;
        public static String EmpresaID
        {
            get { return empresaID; }
            set { empresaID = value; }
        }

    }
}