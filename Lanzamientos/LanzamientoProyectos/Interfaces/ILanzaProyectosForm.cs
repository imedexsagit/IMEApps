using System.Data;
using AdvancedDataGridView;
using LanzamientoProyectos.BD;
using LanzamientoProyectos.Modelo;

namespace LanzamientoProyectos.Interfaces
{
    public interface ILanzaProyectosForm
    {
    
        void SetControlador(LanzaProyectosControl controlEt);
        void CargarProyectos(DataTable proyectos);
        void CargarLineasProyecto(DataTable lineasProyecto);
        void CargarLineasNecesidad(DataTable lineasNecesidad);
        void CargarFamilias(DataTable familias);
        void CargarPedidos(DataTable cargarPedidos);
        void ProyectosCargados();
        void ActivarValidacion();
        void DesactivarValidacion();

        TreeGridNode AddNodoHijo(TreeGridNode nodo, LineaPedido lineaPedido);
        TreeGridNode AddNodoPrincipalArbol(LineaPedido lineaPedido);
        void AddColorNodo(TreeGridNode nodo, LineaPedido lineaPedido);

        bool Desglosar_Lineas();
       
    }
}
