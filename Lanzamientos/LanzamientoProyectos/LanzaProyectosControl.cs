using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using AdvancedDataGridView;
using ControlAccesos;
using LanzamientoProyectos.BD;
using LanzamientoProyectos.Interfaces;
using LanzamientoProyectos.Modelo;
using Utils;
using empresaGlobalProj;

namespace LanzamientoProyectos
{
    public class LanzaProyectosControl
    {
        private ConsultasBD consultasBD;
        private ILanzaProyectosForm lanzaProyectosForm;
        private ggDataSet.TC_LANZADataTable tablaProyectos;
        private int posProyectoSeleccionado;
        private List<LineaPedido> albolLineasPedidos;
        private DataTable dT; //DataTAble que almacena la lista de almacen del codigo seleccionado
       
        private string usuario; // = Environment.UserName.ToUpper();
        //JRM - Cambiar Empresa cambio '3' por empresaGlobal.empresaID; ERROR YA QUE DEBE DE SER CONSTANTE, ESTÁ POR DEFECTO
        private const string Empresa = "3";


        public LanzaProyectosControl(ILanzaProyectosForm form, ControlAcceso controlAcesso)
        {
            consultasBD = new ConsultasBD();
            lanzaProyectosForm = form;
            
            usuario = controlAcesso.Usuario;

          
        }

        public void CargarVista()
        {
            //lanzaProyectosForm.CargarPedidos(consultasBD.CargarPedidos());
            lanzaProyectosForm.CargarFamilias(consultasBD.CargarFamiliasCodigos());
            //lanzaProyectosForm.CargarProyectos(consultasBD.CargarProyectos());
           
        }

       


        //public LineaPedido[] CargarLineasPedidos(int numPedido, string familia, string tipoReg, DateTime horizonte)
        //{
        //    DataTable table = consultasBD.GetLineasPedido(numPedido, familia, tipoReg, horizonte);


        //    //cantidad = table.Rows[0][0].ToString();
        //    LineaPedido[] lineasPedido = new LineaPedido[table.Rows.Count];
        //    for (int i = 0; i < table.Rows.Count; i++)
        //    {
        //        lineasPedido[i] = new LineaPedido((String) table.Rows[i]["CODIGO"], Convert.ToInt32(table.Rows[i]["NUMPED"]), Convert.ToInt32(table.Rows[i]["LINEA"]),
        //            Convert.ToInt32(table.Rows[i]["CTDUP"]), (String) table.Rows[i]["TipoReg"]);
        //    }

        //    return lineasPedido;
        //}

        public DataTable GetListaAlmacen(string empresa, string codigo, string idioma)
        {
            return CD.GetListaAlmacen(empresa, codigo, idioma,0);
        }


        public void InsertarLineaProyecto(int numProyecto, string codigoProyecto,  string tipoReg, int numPedido, int lineaPedido, int cantidad, string codigo, string codigoPadre)
        {
            consultasBD.InsertarLineasLanza(numProyecto, codigoProyecto, tipoReg, numPedido, lineaPedido, usuario, cantidad, codigo,codigoPadre);
            CargarProyectoSeleccionado(numProyecto);
        }

        public void EliminarLineaProyecto(int numProyecto, int lineaPro)
        {
            consultasBD.EliminarLineasLanza(numProyecto, lineaPro);
          
        }

        #region Seleccionar Proyecto

        public ggDataSet.TC_LANZARow SeleccionarProyecto(int numProyecto, int numPedido, string familia, string tipoReg, string codigoProyecto)
        {
            tablaProyectos = consultasBD.GetProyectos(numProyecto, numPedido, familia, tipoReg, codigoProyecto);
            if (tablaProyectos.Count > 0)
            {
                int idProyecto = tablaProyectos[0].CODLANZA;
                CargarProyectoSeleccionado(idProyecto);

                posProyectoSeleccionado = 0;
                lanzaProyectosForm.ProyectosCargados();
                return tablaProyectos.Rows[0] as ggDataSet.TC_LANZARow;
            }
            return null;
        }

        public void CargarProyectoSeleccionado(int idProyecto)
        {
            DataTable lineasProyecto = consultasBD.CargarLineasProyecto(idProyecto);

            lanzaProyectosForm.CargarLineasProyecto(lineasProyecto);

            DataTable lineasNecesidad = consultasBD.CargarLineasNecesidad(idProyecto);
            lanzaProyectosForm.CargarLineasNecesidad(lineasNecesidad);
            lanzaProyectosForm.DesactivarValidacion();
        }


        public ggDataSet.TC_LANZARow SeleccionarProyectoSiguiente()
        {
            if (tablaProyectos != null && tablaProyectos.Count > posProyectoSeleccionado + 1)
            {
                int idProyecto = tablaProyectos[++posProyectoSeleccionado].CODLANZA;
                CargarProyectoSeleccionado(idProyecto);
                return tablaProyectos.Rows[posProyectoSeleccionado] as ggDataSet.TC_LANZARow;
            }
            return null;
        }

        public ggDataSet.TC_LANZARow SeleccionarProyectoAnterior()
        {
            if (tablaProyectos != null && tablaProyectos.Count > posProyectoSeleccionado - 1 && posProyectoSeleccionado - 1 >= 0)
            {
                int idProyecto = tablaProyectos[--posProyectoSeleccionado].CODLANZA;
                CargarProyectoSeleccionado(idProyecto);
                return tablaProyectos.Rows[posProyectoSeleccionado] as ggDataSet.TC_LANZARow;
            }
            return null;
        }

        public ggDataSet.TC_LANZARow SeleccionarProyectoUltimo()
        {
            if (tablaProyectos != null && tablaProyectos.Count - 1 > posProyectoSeleccionado)
            {
                posProyectoSeleccionado = tablaProyectos.Count - 1;
                int idProyecto = tablaProyectos[posProyectoSeleccionado].CODLANZA;
                CargarProyectoSeleccionado(idProyecto);
                return tablaProyectos.Rows[posProyectoSeleccionado] as ggDataSet.TC_LANZARow;
            }
            return null;
        }

        public ggDataSet.TC_LANZARow SeleccionarProyectoPrimero()
        {
            if (tablaProyectos != null && posProyectoSeleccionado > 0 && tablaProyectos.Count > posProyectoSeleccionado)
            {
                posProyectoSeleccionado = 0;
                int idProyecto = tablaProyectos[posProyectoSeleccionado].CODLANZA;
                CargarProyectoSeleccionado(idProyecto);
                return tablaProyectos.Rows[posProyectoSeleccionado] as ggDataSet.TC_LANZARow;
            }
            return null;
        }

        #endregion

        public void BorrarTablaProyectos()
        {
            tablaProyectos = null;
            posProyectoSeleccionado = -1;
        }

        public bool CalcularNecesidades()
        {
            if (tablaProyectos != null && posProyectoSeleccionado >= 0)
            {
                var numProyecto = tablaProyectos[posProyectoSeleccionado].CODLANZA;
                consultasBD.CalcularLineasNecesidad(numProyecto, usuario);
                CargarProyectoSeleccionado(numProyecto);
                lanzaProyectosForm.ActivarValidacion();
                return true;
            }
            return false;
        }



        public int CrearProyecto(string codigoProyecto, string tipoReg, string familia, int numPedido, DateTime horizonte)
        {
            int numProyecto = consultasBD.CrearNuevoProyecto(codigoProyecto, tipoReg, familia, numPedido, horizonte, usuario);
            CargarVista();
            return numProyecto;
        }


        public int CrearProyectoExpedicion(string codigoProyecto, string tipoReg, string familia, int numPedido, DateTime horizonte)
        {
            int numProyecto = consultasBD.CrearNuevoProyectoExpedicion(codigoProyecto, tipoReg, familia, numPedido, horizonte, usuario);
            CargarVista();
            return numProyecto;
        }

        public void GuardarDatosProyecto(int numProyecto, string codigo, int numPedido, string familia, string tipoReg, DateTime horizonte)
        {
            consultasBD.GuardarDatosProyecto(numProyecto, codigo, numPedido, familia, tipoReg, horizonte, usuario);
        }


        public bool ValidarProyecto()
        {
            var numProyecto = tablaProyectos[posProyectoSeleccionado].CODLANZA;
            if (consultasBD.ValidarProyecto(numProyecto, usuario))
            {
                lanzaProyectosForm.DesactivarValidacion();
                tablaProyectos[posProyectoSeleccionado].VALIDADO = "S";

                //x si es un proyecto de expedicion
                if ((int)numProyecto < 0)
                {
                    consultasBD.crearEtiquetaProyectoExpedicion(numProyecto, usuario);
                }


                return true;
            }
            return false;
        }

        public bool BorrarLanzamiento()
        {
            var numProyecto = tablaProyectos[posProyectoSeleccionado].CODLANZA;
            if (tablaProyectos[posProyectoSeleccionado].VALIDADO == "S")
            {
                //x si es un proyecto de expedicion
                if ((int)numProyecto < 0)
                {
                    consultasBD.borrarEtiquetaProyectoExpedicion(numProyecto, usuario);
                }


                if (consultasBD.InvalidarProyecto(numProyecto, usuario))
                {
                    tablaProyectos[posProyectoSeleccionado].VALIDADO = "N";

                    return true;
                }
            }
            return false;
        }

        public void ActualizarCantidadCodigoNecesidades(int numProyecto, string codigo, int cantidad)
        {
            consultasBD.ActualizarCantidadCodigoNecesidades(numProyecto, codigo, cantidad, usuario);
        }

        public bool ExisteCodigoManual(string codigo)
        {
            return consultasBD.ExisteCodigoManual(codigo);
        }

        public void GetCantidadLanzada(string codigoPadre,string codigo, int numPedido, int linea, out int lanzada, out string proyecto)
        {
            consultasBD.GetCantidadLanzada(codigoPadre,codigo, numPedido, linea, out lanzada, out proyecto);
        }

        #region Creación Arbol

        public List<LineaPedido> CargarLineasPedidos(int numPedido, string familia, string tipoReg, DateTime horizonte)
        {
            DataTable table = consultasBD.GetLineasPedido(numPedido, familia, tipoReg, horizonte);


            //cantidad = table.Rows[0][0].ToString();
            albolLineasPedidos = new List<LineaPedido>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var lineaPedido = new LineaPedido((String)table.Rows[i]["CODIGO"], Convert.ToInt32(table.Rows[i]["NUMPED"]), Convert.ToInt32(table.Rows[i]["LINEA"]),
                    Convert.ToInt32(table.Rows[i]["CTDUP"]), (String)table.Rows[i]["TipoReg"]);

                 //  - Si esta marcado sin desglose, para las líneas que han sido lanzadas parcialmente si se las pone desglose. Es resto sin desglose                

                if (lanzaProyectosForm.Desglosar_Lineas())
                {
                    dT = GetListaAlmacen(Empresa, lineaPedido.Codigo, "ES");
                    if (dT.Rows.Count > 0)
                        CrearArbol(lineaPedido);
                }
                else
                {
                    if (!consultasBD.LineaLanzadaParcialmente(lineaPedido.NumPedido,lineaPedido.Linea))
                    {
                        lineaPedido.Denominacion = table.Rows[i]["DENOMINACION"].ToString();

                        int lanzada = 0;
                        string proyecto;
                        GetCantidadLanzada("", lineaPedido.Codigo, lineaPedido.NumPedido, lineaPedido.Linea, out lanzada, out proyecto);
                        lineaPedido.Lanzadas = lanzada;
                        lineaPedido.Proyecto = proyecto;
                        var nodo = lanzaProyectosForm.AddNodoPrincipalArbol(lineaPedido);
                        lanzaProyectosForm.AddColorNodo(nodo, lineaPedido);
                    }
                    else
                    {
                        dT = GetListaAlmacen(Empresa, lineaPedido.Codigo, "ES");
                        if (dT.Rows.Count > 0)
                            CrearArbol(lineaPedido);
                    }
                }
                albolLineasPedidos.Add(lineaPedido);
            }

            return albolLineasPedidos;
        }
        private void CrearArbol(LineaPedido lineaPedido)
        {
            String ID = this.dT.Rows[0][0].ToString();

            string codigoPadre = this.dT.Rows[0]["CODIGO_PADRE"].ToString();
            String codigoHoja = this.dT.Rows[0][6].ToString();
            String codigoHojaDenominacion = this.dT.Rows[0][7].ToString();
            lineaPedido.Denominacion = codigoHojaDenominacion;
           
            int lanzada = 0;
            string proyecto;
            GetCantidadLanzada(codigoPadre == codigoHoja?"":codigoPadre, codigoHoja, lineaPedido.NumPedido, lineaPedido.Linea, out lanzada, out proyecto);
            lineaPedido.Lanzadas = lanzada;
            lineaPedido.Proyecto = proyecto;

            AñadirHoja(ID, lineaPedido);
            ComprobarCantidadesLanzadasHijos(lineaPedido);
            var nodo = lanzaProyectosForm.AddNodoPrincipalArbol(lineaPedido);
            LanzarNodosHijos(nodo, lineaPedido);
            lanzaProyectosForm.AddColorNodo(nodo,lineaPedido);
            
            

        }

        private void ComprobarCantidadesLanzadasHijos(LineaPedido lineaPedido)
        {
            foreach (var pedidoHijo in lineaPedido.Hijos)
            {
                ComprobarCantidadesLanzadasHijos(pedidoHijo);
            }
            if (lineaPedido.Hijos.Count>0 && lineaPedido.Hijos.All(pedidoHijo => pedidoHijo.Lanzadas == pedidoHijo.Necesarias))
            {
                lineaPedido.Lanzadas = lineaPedido.Necesarias;
            }
        }

        private void LanzarNodosHijos(TreeGridNode nodo, LineaPedido lineaPedido)
        {
            
            foreach (var pedidoHijo in lineaPedido.Hijos)
            {
                var nodoHijo = lanzaProyectosForm.AddNodoHijo(nodo, pedidoHijo);
                
                LanzarNodosHijos(nodoHijo,pedidoHijo);
                lanzaProyectosForm.AddColorNodo(nodoHijo,pedidoHijo);
            }
        }

        private void AñadirHoja( String idPadre, LineaPedido lineaPedido)
        {
            var result = (from myRow in this.dT.AsEnumerable()
                where myRow["ID_PADRE"] != DBNull.Value && (string) myRow["ID_PADRE"] == idPadre
                select myRow);
            if (result.Count() > 0)
            {
                var dtResult = result.CopyToDataTable();

                for (int x = 0; x < dtResult.Rows.Count; x++)
                {
                    String ID_PADRE = dtResult.Rows[x][4].ToString();
                    if (ID_PADRE == idPadre)
                    {
                        String ID = dtResult.Rows[x][0].ToString();

                        String cantidadHijo = dtResult.Rows[x][5].ToString();
                        String codigoHijo = dtResult.Rows[x][6].ToString();
                        String codigoHijoDenominacion = dtResult.Rows[x][7].ToString();
                        String categoriaHijo = dtResult.Rows[x][8].ToString();

                        bool fabricable = dtResult.Rows[x][14].ToString() == "1";
                        int cantidad = Convert.ToInt32(cantidadHijo)*lineaPedido.Necesarias;

                        if (!fabricable && categoriaHijo != "TO")
                        {
                            var hijoLineaPedido = new LineaPedido(codigoHijo, lineaPedido.NumPedido, lineaPedido.Linea, cantidad, lineaPedido.TipoPedido);
                            hijoLineaPedido.Padre = lineaPedido;
                            lineaPedido.Hijos.Add(hijoLineaPedido);
                            hijoLineaPedido.Denominacion = codigoHijoDenominacion;

                            int lanzada = 0;
                            string proyecto;
                            if (hijoLineaPedido.Padre != null && (hijoLineaPedido.Padre.Necesarias == hijoLineaPedido.Padre.Lanzadas || hijoLineaPedido.Padre.CompletamenteLanzado))
                            {
                                hijoLineaPedido.CompletamenteLanzado = true;
                            }
                            else
                            {
                                string codigoPadre = "";
                                if (hijoLineaPedido.Padre != null)
                                {
                                    codigoPadre = hijoLineaPedido.Padre.Codigo;
                                }
                                GetCantidadLanzada(codigoPadre, hijoLineaPedido.Codigo, hijoLineaPedido.NumPedido, hijoLineaPedido.Linea, out lanzada, out proyecto);

                                hijoLineaPedido.Lanzadas = lanzada;
                                hijoLineaPedido.Proyecto = proyecto;
                            }


                            AñadirHoja(ID, hijoLineaPedido);
                        }
                        else if (fabricable && categoriaHijo != "TO")
                        {
                            var hijoLineaPedido = new LineaPedido(codigoHijo, lineaPedido.NumPedido, lineaPedido.Linea, cantidad, lineaPedido.TipoPedido);
                            hijoLineaPedido.Padre = lineaPedido;
                            lineaPedido.Hijos.Add(hijoLineaPedido);
                            hijoLineaPedido.Denominacion = codigoHijoDenominacion;

                            int lanzada = 0;
                            string proyecto;
                            if (hijoLineaPedido.Padre != null && (hijoLineaPedido.Padre.Necesarias == hijoLineaPedido.Padre.Lanzadas || hijoLineaPedido.Padre.CompletamenteLanzado))
                            {
                                hijoLineaPedido.CompletamenteLanzado = true;
                                
                            }
                            else
                            {
                                string codigoPadre = "";
                                if (hijoLineaPedido.Padre != null)
                                {
                                    codigoPadre = hijoLineaPedido.Padre.Codigo;
                                }
                                GetCantidadLanzada(codigoPadre,hijoLineaPedido.Codigo, hijoLineaPedido.NumPedido, hijoLineaPedido.Linea, out lanzada, out proyecto);

                                hijoLineaPedido.Lanzadas = lanzada;
                                hijoLineaPedido.Proyecto = proyecto;
                            }


                            AñadirHoja(ID, hijoLineaPedido);
                        }
                    }
                }
            }
        }
        #endregion

        public bool AddLineaProyecto(int numProyectoCargado,string denoProyecto,string codigoPadre,string codigo,int numPedido,int linea,bool esCantidadManual, int cantidadManual, out string mensajeError)
        {
            mensajeError = "";
            //Busco en las hojas padres
            LineaPedido lineaPedido = null;
            
            foreach (var lineaPrincipal in albolLineasPedidos)
            {
                lineaPedido = lineaPrincipal.BuscarLineaPedido(codigoPadre,codigo, numPedido, linea);
                if (lineaPedido != null)
                {
                    break;
                }
            }
            if (lineaPedido == null)
            {
                mensajeError = "La linea seleccionada no existe";
                return false;
            }
            //if (lineaPedido.ContieneCodigoDesarrollo)
            //{
            //    mensajeError = "Este código no esta completamente desarrollado por Oficina Técnica y no puede ser lanzado";
            //    return false;
            //}

            if(!consultasBD.CodigoDelineadoPorCompleto(lineaPedido.Codigo))
            {     
                mensajeError = "Este código no esta completamente desarrollado por Oficina Técnica y no puede ser lanzado";
                return false;     
            }
            
            if (lineaPedido.Lanzadas < lineaPedido.Necesarias && !lineaPedido.CompletamenteLanzado)
            {
                var cantidad = lineaPedido.Necesarias;
                if (esCantidadManual && cantidadManual > 0)
                {
                    cantidad = cantidadManual;
                }

                if (cantidad + lineaPedido.Lanzadas <= lineaPedido.Necesarias)
                {

                    if ((!lineaPedido.ParcialmenteLanzado) || (lineaPedido.ParcialmenteLanzado && !lineaPedido.TieneHijosLanzados()))
                    {
                        if (!lineaPedido.TienePadresLanzados())
                        {

                            InsertarLineaProyecto(numProyectoCargado, denoProyecto, lineaPedido.TipoPedido, lineaPedido.NumPedido,
                                lineaPedido.Linea, cantidad, lineaPedido.Codigo,
                                lineaPedido.Padre != null ? lineaPedido.Padre.Codigo : "");
                            if (cantidad >= lineaPedido.Necesarias) lineaPedido.CompletamenteLanzado = true;
                            else lineaPedido.ParcialmenteLanzado = true;
                            return true;
                        }
                        else 
                        {
                            mensajeError = "Este código tiene códigos padres ya lanzados";
                        }
                    }
                    else
                    {
                        mensajeError = "Este código ha sido lanzado parcialmente";
                    }
                }
                else
                {
                    mensajeError = "Se esta intentando lanzar más marcas de las necesarias";
                }
            }
            else
            {
                mensajeError = "Este código ya ha sido lanzado en su totalidad";
            }


            return false;
        }
    }
}