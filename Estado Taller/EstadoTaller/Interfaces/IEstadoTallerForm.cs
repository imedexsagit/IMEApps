using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EstadoTaller.Util;

namespace EstadoTaller.Interfaces
{
    public interface IEstadoTallerForm
    {
        void SetControlador(EstadoTallerContol controlEt);
        void CargarGraficas(List<Utils.DatosMaquina> listaMaquinas, double escala);
        void CargarTimer();
        void AgregarDataGridRealizables(DataTable realizables);
        void AgregarDataGridNoRealizalbes(DataTable noRealizables);
        int getTipoMaquina();

        int getMaterial();
    }
}
