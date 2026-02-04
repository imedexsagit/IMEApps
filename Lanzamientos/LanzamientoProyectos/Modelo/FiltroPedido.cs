using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LanzamientoProyectos.Modelo
{
    public class FiltroPedido
    {
        public int NumPedido { get; private set; }
        public string Familia { get; private set; }
        public string TipoReg { get; private set; }
        public DateTime Horizonte { get; private set; }

        public FiltroPedido(int nPedido, string familiaPedido, string tipoRegistro, DateTime fechaHorizonte)
        {
            NumPedido = nPedido;
            Familia = familiaPedido;
            TipoReg = tipoRegistro;
            Horizonte= fechaHorizonte;
        }

        public void LimpiarPedido()
        {
            NumPedido = -1;
            Familia = "";
            TipoReg = "";
            Horizonte = DateTime.Now.AddYears(1);
        }
    }
}
