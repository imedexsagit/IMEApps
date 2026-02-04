using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;

namespace LanzamientoProyectos.Modelo
{
    public class LineaPedido
    {       
        //private static readonly string CodigoPendienteDesarrollo = ConfigurationManager.AppSettings["CodigoPendienteDesarrollo"];
        private LineaPedido padre;
        public LineaPedido Padre
        {
            get { return padre; }
            set
            {
                padre = value;
                /*if (ContieneCodigoDesarrollo)
                {
                  
                    MarcarArbolContieneCodigoDesarrollo(this);
                }*/
            }
        }

        public List<LineaPedido> Hijos { get; set; }
        public string Codigo { get; private set; }
        public int Linea { get; private set; }
        public string Denominacion { get; set; }
        public int Necesarias { get; private set; }
        public int NumPedido { get; private set; }
        public string TipoPedido { get; private set; }
        public bool ParcialmenteLanzado { get; set; }

        public int Lanzadas { get; set; }
        public string Proyecto { get; set; }
        public bool CompletamenteLanzado { get; set; }
       // public bool ContieneCodigoDesarrollo { get; set; }        


        public LineaPedido(string codigo, int numPedido, int linea, int necesarias, string tipoPedido)
        {
            Codigo = codigo;
            //if (this.Codigo == CodigoPendienteDesarrollo)
            //{
            //    ContieneCodigoDesarrollo = true;
            //}

            NumPedido = numPedido;
            Linea = linea;
            Necesarias = necesarias;
            TipoPedido = tipoPedido;
            Hijos = new List<LineaPedido>();
            ParcialmenteLanzado = false;         
        }

        /*private void MarcarArbolContieneCodigoDesarrollo(LineaPedido hijo)
        {
            if (hijo.Padre != null)
            {
                hijo.Padre.ContieneCodigoDesarrollo = true;
                MarcarArbolContieneCodigoDesarrollo(hijo.Padre);
            }
        }*/

        public Color GetColorNodo()
        {
            //if (Codigo == CodigoPendienteDesarrollo)
            //{
            //    ParcialmenteLanzado = false;
            //    return Color.Gold;
            //}
            if (Necesarias == Lanzadas)
            {
                ParcialmenteLanzado = false;
                return Color.DarkGray;
            }
            if (Lanzadas > 0 && Necesarias > Lanzadas)
            {
                ParcialmenteLanzado = true;
                return Color.Coral;
            }

            if (CompletamenteLanzado)
            {
                ParcialmenteLanzado = false;
                return Color.DarkGray;
            }


            if (Hijos.Count > 0)
            {
                var numHijosCompletos = Hijos.Count(h => h.Necesarias == h.Lanzadas);
                if (numHijosCompletos == Hijos.Count)
                {
                    ParcialmenteLanzado = false;
                    return Color.Red;
                }
                if (numHijosCompletos > 0)
                {
                    ParcialmenteLanzado = true;
                    return Color.Coral;
                }
                var numHijosParcialmenteCompletos = Hijos.Count(h =>h.ParcialmenteLanzado);
                if (numHijosParcialmenteCompletos > 0)
                {
                    ParcialmenteLanzado = true;

                    return Color.Coral;
                }
            }
            ParcialmenteLanzado = false;
            return Color.White;
        }

        public LineaPedido BuscarLineaPedido(string codigoPadre, string codigo, int numPedido, int linea)
        {
            if((this.Padre!= null && this.Padre.Codigo==codigoPadre)|| (this.Padre== null && string.IsNullOrEmpty(codigoPadre)))
                if (this.Codigo == codigo && this.NumPedido== numPedido && this.Linea==linea)
                {
                    return this;
                }
            LineaPedido lineaPedido = null;
            foreach (var hijo in Hijos)
            {
               lineaPedido= hijo.BuscarLineaPedido(codigoPadre,codigo, numPedido, linea);
                if (lineaPedido != null)
                    break;
            }
            return lineaPedido;


        }



        public bool TieneHijosLanzados()
        {                
            foreach (var hijo in this.Hijos)
            {
                if (hijo.Lanzadas > 0)
                    return true;
                if (hijo.Hijos.Count > 0) 
                {
                    bool tiene = hijo.TieneHijosLanzados();
                    if (tiene)
                        return tiene;
                }                                                    
            }

            return false;
        }



        public bool TienePadresLanzados()
        {
            if(this.Padre != null)
            {
                if(this.Padre.Lanzadas > 0)
                    return true;
                bool tiene = this.Padre.TienePadresLanzados();
                if(tiene)
                     return tiene;
            }

            return false;                        
        }



    }
}