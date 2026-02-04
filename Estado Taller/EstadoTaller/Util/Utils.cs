using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EstadoTaller.Util
{
    public static class Utils
    {
        public struct DatosMaquina
        {
            public string  puesto;            
            public string denominacion;
            public double tRealizables;
            public double tNoRealizables;
            public double tInciertas;

            public double kgRealizables;
            public double kgNoRealizables;
            public double kgInciertas;            
            public double kgMaquina;


            public DatosMaquina(string puesto, double tRealizables, double tInciertas, double tNoRealizables, double kgTotales, string denominacion, double kgRealizables, double kgNoRealizables, double kgInciertas)
            {
                this.puesto = puesto;
                this.denominacion = denominacion;

                this.tRealizables = tRealizables;
                this.tNoRealizables = tNoRealizables;
                this.tInciertas = tInciertas;

                this.kgRealizables = kgRealizables;
                this.kgNoRealizables = kgNoRealizables;
                this.kgInciertas =kgInciertas;                

                this.kgMaquina = kgTotales;                
            }
           

          
        }



    }
}
