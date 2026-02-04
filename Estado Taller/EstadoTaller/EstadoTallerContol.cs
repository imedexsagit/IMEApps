using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Configuration;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using EstadoTaller.BD;
using EstadoTaller.Interfaces;
using EstadoTaller.Properties;
using EstadoTaller.Util;

namespace EstadoTaller
{
    public class EstadoTallerContol
    {
        private ConsultasBD consultasBD;
        private IEstadoTallerForm estadoTallerForm;
        private static string sesionActual;
        private static string fechaDif;
        private static int tiempoRefresco;
        private static int tiempoBorrado;

        // Threading
        private static PantallaCarga formPantallaCarga;
        private static Thread threatPantallaCarga;
        private BackgroundWorker bgRecargaDatos;

        public EstadoTallerContol(IEstadoTallerForm estadoTallerForm)
        {
            consultasBD = new ConsultasBD();
            this.estadoTallerForm = estadoTallerForm;
            this.estadoTallerForm.SetControlador(this);
            tiempoRefresco = Convert.ToInt32(ConfigurationManager.AppSettings["TiempoRefresco"]);
            tiempoBorrado = Convert.ToInt32(ConfigurationManager.AppSettings["TiempoBorrado"]);
            bgRecargaDatos = new BackgroundWorker();
            bgRecargaDatos.WorkerSupportsCancellation = true;
            bgRecargaDatos.WorkerReportsProgress = true;
            bgRecargaDatos.DoWork += new DoWorkEventHandler(bgRecargaDatos_DoWork);
            bgRecargaDatos.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgRecargaDatos_RunWorkerCompleted);
        }


        private List<Utils.DatosMaquina> TiemposPendientesMaquinas(out double escala, int tipoMaquina, int material)
        {
            var result = new List<Utils.DatosMaquina>();
            var listaMaquinas = consultasBD.MaquinasPorOperacion(tipoMaquina);                     
            escala = double.MinValue;

            foreach (var maquina in listaMaquinas)
            {
                double tiempoRealizables;
                double tiempoNoRealizables;
                double tiempoInciertas;
                double kgRealizables;
                double kgNoRealizables;
                double kgInciertas;
                double kgTotalesMaquina;
                consultasBD.TiempoPendientePorMaquina(sesionActual, maquina, 1, material, out tiempoRealizables, out kgRealizables);
                consultasBD.TiempoPendientePorMaquina(sesionActual, maquina, 0, material, out tiempoNoRealizables, out kgNoRealizables);
                consultasBD.TiempoPendientePorMaquina(sesionActual, maquina, 2, material, out tiempoInciertas, out kgInciertas);
                kgTotalesMaquina = kgRealizables + kgNoRealizables + kgInciertas;

                var tTotal = (tiempoRealizables + tiempoInciertas + tiempoNoRealizables);
                if (tTotal > escala)
                {
                    escala = tTotal;
                }

                string denominacion = consultasBD.GetDenominacionMaquina(maquina);

                var datosMaquina = new Utils.DatosMaquina(maquina, tiempoRealizables, tiempoInciertas, tiempoNoRealizables, kgTotalesMaquina, denominacion, kgRealizables, kgNoRealizables, kgInciertas);
                result.Add(datosMaquina);
            }

            return result;
        }

        
        public void ActualizarEstadoTaller(bool forzar)
        {
            //MMM Ya no se actualiza el estado taller desde la aplicacion, se hace desde un job
            //Con forzar a true pueden producirse errores por lo que de momento no se va a utilizar
          /*  if (!forzar)
            {
                sesionActual = consultasBD.UltimoSesionEstadoTaller();
                if (String.IsNullOrEmpty(sesionActual))
                {
                    MostarPantallaCarga();
                    //Comprobamos si hay datos validos
                    while (String.IsNullOrEmpty(sesionActual) && consultasBD.ExisteCargaPendiente(tiempoRefresco))
                    {
                        Thread.Sleep(tiempoRefresco/4);
                        sesionActual = consultasBD.UltimoSesionEstadoTaller();
                    }
                    if (string.IsNullOrEmpty(sesionActual))
                    {
                        sesionActual = consultasBD.ActualizarEstadoTaller();
                    }
                    CerrarPantallaCarga();
                }
            }
            else
            {
                MostarPantallaCarga();
                sesionActual = consultasBD.ActualizarEstadoTaller();
                CerrarPantallaCarga();
            }

            fechaDif = consultasBD.TiempoSesionMs(sesionActual);
            ActualizarGraficas();
            

            try
            {
                if (Convert.ToInt32(fechaDif) > tiempoRefresco)
                {
                    if (!bgRecargaDatos.IsBusy)
                        bgRecargaDatos.RunWorkerAsync();
                }
            }
            catch (System.OverflowException)
            {
                bgRecargaDatos.RunWorkerAsync();
            }
           * */
            sesionActual = consultasBD.UltimoSesionEstadoTaller();
            ActualizarGraficas();
        }

        private void ActualizarGraficas()
        {
            double escala;
            var listaMaquinas = TiemposPendientesMaquinas(out escala, estadoTallerForm.getTipoMaquina(), estadoTallerForm.getMaterial());
            estadoTallerForm.CargarGraficas(listaMaquinas, escala);
        }

        public DataTable obtenerMaquinasRealizable(string proveedor)
        {
            DataTable dtRealizable;
            dtRealizable = consultasBD.ObtenerInformacionDetalladaExportacion(sesionActual, estadoTallerForm.getTipoMaquina(), proveedor, true, estadoTallerForm.getMaterial());
            System.Diagnostics.Debug.WriteLine(estadoTallerForm.getMaterial());
            return dtRealizable;
        }

        public DataTable obtenerMaquinasNoRealizable(string proveedor)
        {
            DataTable dtNoRealizable;
            dtNoRealizable = consultasBD.ObtenerInformacionDetalladaExportacion(sesionActual, estadoTallerForm.getTipoMaquina(), proveedor, false, estadoTallerForm.getMaterial());
            return dtNoRealizable;
        }



        public void CargarVista()
        {
            ActualizarEstadoTaller(false);
            estadoTallerForm.CargarTimer();
        }

        public void GraficoSeleccionado(string puesto)
        {
            var opRealizables = consultasBD.ObtenerInformacionDetallada(sesionActual, puesto, true, estadoTallerForm.getMaterial());
            estadoTallerForm.AgregarDataGridRealizables(opRealizables);
            var opNoRealizables = consultasBD.ObtenerInformacionDetallada(sesionActual, puesto, false, estadoTallerForm.getMaterial());
            estadoTallerForm.AgregarDataGridNoRealizalbes(opNoRealizables);
        }

        #region Pantalla de carga

        public static void MostarPantallaCarga()
        {
            if (formPantallaCarga != null)
                return;
            threatPantallaCarga = new Thread(MostarFormulario) {IsBackground = true};
            threatPantallaCarga.SetApartmentState(ApartmentState.STA);
            threatPantallaCarga.Start();
        }

        public static void CerrarPantallaCarga()
        {
            threatPantallaCarga.Abort();
            threatPantallaCarga = null;
            formPantallaCarga = null;
        }


        private static void MostarFormulario()
        {
            formPantallaCarga = new PantallaCarga();
            formPantallaCarga.ShowDialog();
            //Application.Run(msFrmSplash);
        }

        #endregion

        #region  BackgroundWorker

        private void bgRecargaDatos_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Ya no se actualiza el estado del taller desde la Aplicacion, se hace desde un job desde la base de datos. 
            //consultasBD.BorrarDatosObsoletos(tiempoBorrado);
        }


        private void bgRecargaDatos_DoWork(object sender, DoWorkEventArgs e)
        {
          /* Ya no se actualiza el estado del taller desde la Aplicacion, se hace desde un job desde la base de datos. 
           * if (!consultasBD.ExisteCargaPendiente(tiempoRefresco))
                using (ConsultasBD consultasBG = new ConsultasBD())
                {
                    string nuevaSesion;
                    nuevaSesion = consultasBG.UltimoSesionEstadoTaller();
                    if (sesionActual == nuevaSesion)
                    {
                        sesionActual = consultasBG.ActualizarEstadoTaller();
                    }
                    else
                    {
                        sesionActual = nuevaSesion;
                    }
                    fechaDif = consultasBG.TiempoSesionMs(sesionActual);
                    ActualizarGraficas();
                }
           * */
        }

      

        #endregion
    }
}