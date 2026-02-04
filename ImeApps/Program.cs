using System;
using System.Windows.Forms;

namespace ImeApps
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!Environment.UserName.Contains("mdt.inventario") && !Environment.UserName.Contains("tablet.inventario"))
            {
                Application.Run(new Principal());
            }
            else
            {
                Application.Run(new InventarioTablets.Prin());
            }

            //if ((Environment.UserName != "tablet.almacen") && (Environment.UserName != "operarios.tablets")) //ÁNGEL GARCÍA 02/01/2023 (Feliz año) comprobación para tablets de almacén
            //{
                //Application.Run(new Principal());
            /*}
            else
            {
                if (Environment.UserName == "operarios.tablets")
                {
                    empresaGlobalProj.empresaGlobal.empresaID = "60";
                    empresaGlobalProj.empresaGlobal.nombreEmpresa = "MADETOWER";
                }
                Application.Run(new LocalizacionPaquetes.LocalizacionPaquetes());
            }*/
        }
    }
}
