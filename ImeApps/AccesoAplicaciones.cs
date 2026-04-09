using System;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

//using ControlAccesos;
//using EstadoTaller;
//using LanzamientoProyectos;
//using ProyectosOnLine;
//using GestionSoldadura;



namespace ImeApps
{
    public partial class AccesoAplicaciones : Form
    {
        private ProyectosOnLine.Form1 proyectosOnlineForm;
        //private LanzaProyectosForm lanzaProyectosForm;
        private ControlAccesos.ControlAcceso controlAcceso;

        
        public AccesoAplicaciones()
        {
            InitializeComponent();


            //Comprobar si el usuario tiene permisos para ejecutar la aplicación            
            controlAcceso = new ControlAccesos.ControlAcceso();
            
            if (!controlAcceso.TieneAcceso(typeof(Intranet.Intranet).Assembly.GetName().Name))
            {
                btnBuscador.Visible = false;
            }
            if (!controlAcceso.TieneAcceso(typeof(EstadoTaller.EstadoTallerForm).Assembly.GetName().Name))
            {
                btnHorasTaller.Visible = false;
            }
            if (!controlAcceso.TieneAcceso(typeof(ProyectosOnLine.Form1).Assembly.GetName().Name))
            {
                btnProyectosOnline.Visible = false;
            }
            if (!controlAcceso.TieneAcceso(typeof(AprovisionamientoCalculo.AprovisionamientoCalculo).Assembly.GetName().Name))            
            {
                btnAprovisionamiento.Visible = false;
            }
            if (!controlAcceso.TieneAcceso(typeof(LanzamientoProyectos.LanzaProyectosForm).Assembly.GetName().Name))
            {
                btnLanzamiento.Visible = false;
            }
            if (!controlAcceso.TieneAcceso(typeof(ReservaComprasMP.ReservaComprasMP).Assembly.GetName().Name))
            {
                btnReservaComprasMP.Visible = false;                                        
            }
            if (!controlAcceso.TieneAcceso(typeof(InspeccionesTablets.Form1).Assembly.GetName().Name))
            {
                btnInspeccionesCalidad.Visible = false;
            }
            if (!controlAcceso.TieneAcceso(typeof(InfoSoldaduraPDF.FormPrincipal).Assembly.GetName().Name))
            {
                btnInfoSoldaduraPDF.Visible = false;
            }
            if (!controlAcceso.TieneAcceso(typeof(PlanosPedidos.PlanosPedidos).Assembly.GetName().Name))
            {
                botonPlanosPedidos.Visible = false;                
            }
            if (!controlAcceso.TieneAcceso(typeof(TennetPintura.tennetPintura).Assembly.GetName().Name))
            {
                botonTennetPintura.Visible = false;
            }
            if (!controlAcceso.TieneAcceso(typeof(CambioTerceros.cambioTerceros).Assembly.GetName().Name))
            {
                botonCambioTerceros.Visible = false;
            }
            if (!controlAcceso.TieneAcceso(typeof(BuscadorNC.buscadorNC_MainForm).Assembly.GetName().Name))
            {
                botonBuscadorNC.Visible = false;
            }
            //string imprimacion = typeof(Imprimacion.Imprimacion).Assembly.GetName().Name;
            if (!controlAcceso.TieneAcceso(typeof(Imprimacion.Imprimacion).Assembly.GetName().Name))
            {
                botonImprimacion.Visible = false;
            }
            
            //Csanchez
            if (!controlAcceso.TieneAcceso(typeof(GestionSoldadura.Form1).Assembly.GetName().Name))
            {
                botonGestionSoldadura.Visible = false;
           }

            //Victor Alvarez 
            if (!controlAcceso.TieneAcceso(typeof(GestionGalvanizado.Form1).Assembly.GetName().Name))
            {
                botonGestionGalvanizado.Visible = false;
            }

            if (!controlAcceso.TieneAcceso(typeof(ProyectosSinExpedicion.Form1).Assembly.GetName().Name))
            {
                botonProyectosSinExpedicion.Visible = false;
            }

            //Csanchez InspeccionPinturaTablet
            if (!controlAcceso.TieneAcceso(typeof(InspeccionPinturaTablet.FormPrincipal).Assembly.GetName().Name))
            {
                botonInspeccionesPintura.Visible = false;
           }

            //Carlos ServicioPlanos
            if (!controlAcceso.TieneAcceso(typeof(ServicioPlanos.Form1).Assembly.GetName().Name))
            {
                btnPlanosServicios.Visible = false;
            }

            //Victor Alvarez 
            if (!controlAcceso.TieneAcceso(typeof(TorresStock.Form1).Assembly.GetName().Name))
            {
                botonTorresStock.Visible = false;
            }

            if (!controlAcceso.TieneAcceso(typeof(TareasFaltanDatos.Form1).Assembly.GetName().Name))
            {
                btnFaltaDatosTareas.Visible = false;
            }


            if (!controlAcceso.TieneAcceso(typeof(LanzamientoPendiente.Form1).Assembly.GetName().Name))
            {
                
                btnLanzamientosPendientes.Visible = false;
            }

            if (!controlAcceso.TieneAcceso(typeof(ModificarNC.ModificarNCyCAM).Assembly.GetName().Name))
            {

                btnModificarNCyCAM.Visible = false;
            }


            //Ángel García
            if (!controlAcceso.TieneAcceso(typeof(DivisorLotes.DivisorLotes).Assembly.GetName().Name))
            {

                btnDivisorLotes.Visible = false;
            }
            if (!controlAcceso.TieneAcceso(typeof(GestorPesoClientes.GestorPesoClientes).Assembly.GetName().Name))
            {

                btnGestorPesoClientes.Visible = false;
            }
            if (!controlAcceso.TieneAcceso(typeof(LocalizacionPaquetes.LocalizacionPaquetes).Assembly.GetName().Name))
            {

                btnLocalizacionPaquetes.Visible = false;
            }
            if (!controlAcceso.TieneAcceso(typeof(GestionVacaciones.GestionVacaciones).Assembly.GetName().Name))
            {
                btnVacaciones.Visible = false;
            }
            if (!controlAcceso.TieneAcceso(typeof(IncTornilleria.IncTornilleria).Assembly.GetName().Name))
            {
                btnTornilleria.Visible = false;
            }
            if (!controlAcceso.TieneAcceso(typeof(GestorTurnosTaller.GestorTurnosTaller).Assembly.GetName().Name))
            {
                btnTurnosTaller.Visible = false;
            }
            if (!controlAcceso.TieneAcceso(typeof(TraductorCodigos.TraductorCodigos).Assembly.GetName().Name))
            {
                btnTraductor.Visible = false;
            }
            if (!controlAcceso.TieneAcceso(typeof(GestionPintura.GestionPintura).Assembly.GetName().Name))
            {
                btnGestionPintura.Visible = false;
            }
            if (!controlAcceso.TieneAcceso(typeof(TiemposProyectos.TiemposProyectos).Assembly.GetName().Name))
            {
                btnTiempoProyectos.Visible = false;
            }
            if (!controlAcceso.TieneAcceso(typeof(TiemposProyectos.TiemposProyectos).Assembly.GetName().Name))
            {
                btnTiempoProyectos.Visible = false;
            }
            if (!controlAcceso.TieneAcceso(typeof(ReclamacionesSinProyecto.ReclamacionesSinProyecto).Assembly.GetName().Name))
            {
                btnRecSinProy.Visible = false;
            }
            if (!controlAcceso.TieneAcceso("PackingTablet"))
            {
                btnPacking.Visible = false;
            }
            if (!controlAcceso.TieneAcceso(typeof(CamionesGalvanizado.ExportacionCamiones).Assembly.GetName().Name))
            {
                btnCamiones.Visible = false;
            }
            if (!controlAcceso.TieneAcceso(typeof(PaquetesReprocesado.Form1).Assembly.GetName().Name))
            {
                btnReprocesado.Visible = false;
            }
            if (!controlAcceso.TieneAcceso(typeof(InventarioTablets.Prin).Assembly.GetName().Name))
            {
                btnInventario.Visible = false;
            }
            if (!controlAcceso.TieneAcceso(typeof(RecepcionCamiones.RecepcionCamiones).Assembly.GetName().Name))
            {
                btnRecepEtiq.Visible = false;
            }
            if (!controlAcceso.TieneAcceso("TablaPersonal"))
            {
                btnTablaPersonal.Visible = false;
            }
            
            if (!controlAcceso.TieneAcceso("GestionOfertas"))
            {
                botonAplicacion1.Visible = false;
            }

            if (!controlAcceso.TieneAcceso("Tekla2023"))
            {
                botonAplicacion2.Visible = false;
            }

            if (!controlAcceso.TieneAcceso("InfoExpediciones"))
            {
                btnInfoExp.Visible = false;
            }

            if (!controlAcceso.TieneAcceso("M2Clientes"))
            {
                botonM2Cliente.Visible = false;
            }

            if (!controlAcceso.TieneAcceso("AutoLanzamientoProyectos"))
            {
                btnAutoLanza.Visible = false;
            }

            if (!controlAcceso.TieneAcceso("Tekla2024"))
            {
                botonAplicacion3.Visible = false;
            }

            if (!controlAcceso.TieneAcceso("Retornos"))
            {
                btnRetornos.Visible = false;
            }

            if (!controlAcceso.TieneAcceso("PNItoLST"))
            {
                btnPNIaLST.Visible = false;
            }

            if (!controlAcceso.TieneAcceso("ColoresTorres"))
            {
                btnColoresTorres.Visible = false;
            }
            if (!controlAcceso.TieneAcceso(typeof(HorizonteAsprova.formHorizonte).Assembly.GetName().Name))
            {
                btnHorizonteAsp.Visible = false;
            }
            // Carlos Casquero 11/03/2026 - Se controla si tiene acceso el usuario al Tekla Analyzer
            if (!controlAcceso.TieneAcceso("TeklaAnalyzer"))
            {
                btnAnalyzer.Visible = false;
            }
        }



        //Buscador
        void btnBuscador_BotonAplicacionClick()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "Intranet").FirstOrDefault();
            if (f == null)
            {
                Intranet.Intranet formBuscador = new Intranet.Intranet();
                formBuscador.MdiParent = this.MdiParent;
                formBuscador.Show();
            }
            else
            {
                f.WindowState = FormWindowState.Maximized;
            } 
        }
        



        //Estado taller
        private void btnHorasTaller_BotonAplicacionClick()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "EstadoTallerForm").FirstOrDefault();
            if (f == null)
            {
                EstadoTaller.EstadoTallerForm estadoTallerForm = new EstadoTaller.EstadoTallerForm();

                var control = new EstadoTaller.EstadoTallerContol(estadoTallerForm);
                control.CargarVista();                
                
                estadoTallerForm.MdiParent = this.MdiParent;
                estadoTallerForm.Show();
            }
            else
            {
                f.WindowState = FormWindowState.Maximized;
            } 
        } 
        



        //Proyectos Online. Se lanza de esta manera para poder sacarlo fuera de la ventana contenedor
        private void btnProyectosOnline_BotonAplicacionClick()
        {
            if (proyectosOnlineForm == null || !proyectosOnlineForm.Visible)
            {                
                proyectosOnlineForm = new ProyectosOnLine.Form1();
                proyectosOnlineForm.Show();                
            }
            else
            {
                proyectosOnlineForm.WindowState = FormWindowState.Normal;
            } 
        }
       


        //Cálculo Aprovisionamiento
        private void btnAprovisionamiento_BotonAplicacionClick()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "AprovisionamientoCalculo").FirstOrDefault();
            if (f == null)
            {
                AprovisionamientoCalculo.AprovisionamientoCalculo formAprovCalc = new AprovisionamientoCalculo.AprovisionamientoCalculo();
                formAprovCalc.MdiParent = this.MdiParent;
                formAprovCalc.Show();
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            } 
        }


        //Lanzamientos a Fabricacion
        private void btnLanzamiento_BotonAplicacionClick()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "LanzaProyectosForm").FirstOrDefault();
            if (f == null)
            {
                LanzamientoProyectos.LanzaProyectosForm lanzaProyectosForm = new LanzamientoProyectos.LanzaProyectosForm();

                var control = new LanzamientoProyectos.LanzaProyectosControl(lanzaProyectosForm, controlAcceso);
                control.CargarVista();
                lanzaProyectosForm.SetControlador(control);

                lanzaProyectosForm.MdiParent = this.MdiParent;
                lanzaProyectosForm.Show();
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            }             
        }

       

        //ReservaComprasMP
        private void btnReservaComprasMP_BotonAplicacionClick()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "ReservaComprasMP").FirstOrDefault();
            if (f == null)
            {
                ReservaComprasMP.ReservaComprasMP formReserva = new ReservaComprasMP.ReservaComprasMP();                
                formReserva.MdiParent = this.MdiParent;
                formReserva.Show();
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            } 
        }


        //Inspecciones calidad
        private void btnInspeccionesCalidad_BotonAplicacionClick()
        {

            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "InspeccionesTablets").FirstOrDefault();

            if (f == null)
            {
                InspeccionesTablets.Form1 formInspeccion = new InspeccionesTablets.Form1();
                formInspeccion.MdiParent = this.MdiParent;
                formInspeccion.Show();
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            }             
        }

        //Info soldadura
        private void btnInfoSoldaduraPDF_BotonAplicacionClick()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "InfoSoldaduraPDF").FirstOrDefault();

            if (f == null)
            {
                InfoSoldaduraPDF.FormPrincipal formInfoSoldadura = new InfoSoldaduraPDF.FormPrincipal();
                formInfoSoldadura.MdiParent = this.MdiParent;
                formInfoSoldadura.Show();
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            } 
        }



        //Planos Pedidos
        private void botonPlanosPedidos_BotonAplicacionClick()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "PlanosPedidos").FirstOrDefault();
            if (f == null)
            {
                /*PlanosPedidos.PlanosPedidos formBuscador = new PlanosPedidos.PlanosPedidos();
                formBuscador.MdiParent = this.MdiParent;
                formBuscador.Show();*/
                Cursor.Current = Cursors.WaitCursor;
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = @"\\nas01\AppIMEDEXSA\PlanosPedidos\setup.exe";
                p.Start();
                Cursor.Current = Cursors.Arrow;
                p.WaitForExit();
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            }

        }

        //TennetPintura
        private void botonTennetPintura_BotonAplicacionClick()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "TennetPintura").FirstOrDefault();
            if (f == null)
            {
                //MODIFICAR A TENNETPINTURAS, está con planos pedidos
                TennetPintura.tennetPintura formTennetPintura = new TennetPintura.tennetPintura();
                //PlanosPedidos.PlanosPedidos formBuscador = new PlanosPedidos.PlanosPedidos();
                formTennetPintura.MdiParent = this.MdiParent;
                formTennetPintura.Show();
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            }

        }

        // JMRM - 07/02/2020 Cambio Terceros
        private void botonCambioTerceros_BotonAplicacionClick()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "cambioterceros").FirstOrDefault();
            if (f == null)
            {
                //MODIFICAR A cambioTerceros, 

                CambioTerceros.cambioTerceros formTennetPintura = new CambioTerceros.cambioTerceros();
                //PlanosPedidos.PlanosPedidos formBuscador = new PlanosPedidos.PlanosPedidos();
                formTennetPintura.MdiParent = this.MdiParent;
                formTennetPintura.Show();
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            }

        }


        // JMRM - 27/05/2020 BuscadorNC
        private void botonBuscadorNC_BotonAplicacionClick()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "BuscadorNC").FirstOrDefault();
            if (f == null)
            {
                //MODIFICAR A cambioTerceros, 

                BuscadorNC.buscadorNC_MainForm formBuscadorNC = new BuscadorNC.buscadorNC_MainForm();
                //PlanosPedidos.PlanosPedidos formBuscador = new PlanosPedidos.PlanosPedidos();
                formBuscadorNC.MdiParent = this.MdiParent;
                formBuscadorNC.Show();
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            }

        }

        private void botonImprimacion_BotonAplicacionClick()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "Imprimacion").FirstOrDefault();
            if (f == null)
            {
               

                Imprimacion.Imprimacion formImprimacion = new Imprimacion.Imprimacion();
              
                formImprimacion.MdiParent = this.MdiParent;
                formImprimacion.Show();
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            }
        }

        private void botonGestionSoldadura_BotonAplicacionClick()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "GestionSoldadura").FirstOrDefault();
            if (f == null)
            {
               
                GestionSoldadura.Form1 formImprimacion = new GestionSoldadura.Form1();
                //PlanosPedidos.PlanosPedidos formBuscador = new PlanosPedidos.PlanosPedidos();
                formImprimacion.MdiParent = this.MdiParent;
                formImprimacion.Show();
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            }
        }

        private void botonGestionGalvanizado_BotonAplicacionClick()
        {

            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "GestionGalvanizado").FirstOrDefault();
            if (f == null)
            {
                GestionGalvanizado.Form1 formGalvanizado = new GestionGalvanizado.Form1();
                //PlanosPedidos.PlanosPedidos formBuscador = new PlanosPedidos.PlanosPedidos();
                formGalvanizado.MdiParent = this.MdiParent;
                formGalvanizado.Show();
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            }


        }

        private void botonProyectosSinExpedicion_BotonAplicacionClick()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "ProyectosSinExpedicion").FirstOrDefault();
            if (f == null)
            {
                ProyectosSinExpedicion.Form1 formProyectosSinExpedicion = new ProyectosSinExpedicion.Form1();
                //PlanosPedidos.PlanosPedidos formBuscador = new PlanosPedidos.PlanosPedidos();
                formProyectosSinExpedicion.MdiParent = this.MdiParent;
                formProyectosSinExpedicion.Show();
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            }
        }

        private void botonInspeccionesPintura_Click()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "InspeccionPinturaTablet").FirstOrDefault();
            if (f == null)
            {

                InspeccionPinturaTablet.FormPrincipal formInspPintura = new InspeccionPinturaTablet.FormPrincipal();

                formInspPintura.MdiParent = this.MdiParent;
                formInspPintura.Show();
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            }
        }

        private void botonTorresStock_BotonAplicacionClick()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "TorresStock").FirstOrDefault();
            if (f == null)
            {
                TorresStock.Form1 formTorresStock = new TorresStock.Form1();
                //PlanosPedidos.PlanosPedidos formBuscador = new PlanosPedidos.PlanosPedidos();
                formTorresStock.MdiParent = this.MdiParent;
                formTorresStock.Show();
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            }
        }

        private void btnFaltaDatosTareas_BotonAplicacionClick()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "TareasFaltanDatos").FirstOrDefault();
            if (f == null)
            {
                TareasFaltanDatos.Form1 formTareasFaltanDatos = new TareasFaltanDatos.Form1();
                //PlanosPedidos.PlanosPedidos formBuscador = new PlanosPedidos.PlanosPedidos();
                formTareasFaltanDatos.MdiParent = this.MdiParent;
                formTareasFaltanDatos.Show();
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            }
        }

        private void btnLanzamientosPendientes_BotonAplicacionClick()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "LanzamientoPendiente").FirstOrDefault();
            if (f == null)
            {
                LanzamientoPendiente.Form1 formLanzamientoPendiente = new LanzamientoPendiente.Form1();
                //PlanosPedidos.PlanosPedidos formBuscador = new PlanosPedidos.PlanosPedidos();
                formLanzamientoPendiente.MdiParent = this.MdiParent;
                formLanzamientoPendiente.Show();
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            }
        }

        private void btnModificarNCyCAM_BotonAplicacionClick()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "ModificarNC").FirstOrDefault();
            if (f == null)
            {
                ModificarNC.ModificarNCyCAM F1MNC = new ModificarNC.ModificarNCyCAM();
                //PlanosPedidos.PlanosPedidos formBuscador = new PlanosPedidos.PlanosPedidos();
                F1MNC.MdiParent = this.MdiParent;
                F1MNC.Show();
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            }
        }

        private void DivisorLotes_BotonAplicacionClick()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "DivisorLotes").FirstOrDefault();
            if (f == null)
            {
                DivisorLotes.DivisorLotes F1DL = new DivisorLotes.DivisorLotes();
                //PlanosPedidos.PlanosPedidos formBuscador = new PlanosPedidos.PlanosPedidos();
                F1DL.MdiParent = this.MdiParent;
                F1DL.Show();
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            }
        }

        private void btnGestorPesoClientes_BotonAplicacionClick()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "GestorPesoClientes").FirstOrDefault();
            if (f == null)
            {
                GestorPesoClientes.GestorPesoClientes GPC = new GestorPesoClientes.GestorPesoClientes();
                GPC.MdiParent = this.MdiParent;
                GPC.Show();
                GPC.colorLoad();
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            }
        }

        private void btnLocalizacionPaquetes_BotonAplicacionClick()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "LocalizacionPaquetes").FirstOrDefault();
            if (f == null)
            {
                LocalizacionPaquetes.LocalizacionPaquetes LP = new LocalizacionPaquetes.LocalizacionPaquetes();
                LP.MdiParent = this.MdiParent;
                LP.Show();
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            }
        }

        private void btnVacaciones_BotonAplicacionClick()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "GestionVacaciones").FirstOrDefault();
            if (f == null)
            {
                Cursor.Current = Cursors.WaitCursor;
                GestionVacaciones.GestionVacaciones GV = new GestionVacaciones.GestionVacaciones();
                GV.MdiParent = this.MdiParent;
                GV.Show();
                GV.colorLoad();
                Cursor.Current = Cursors.Default;
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            }
        }

        private void btnTornilleria_BotonAplicacionClick()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "IncTornilleria").FirstOrDefault();
            if (f == null)
            {
                Cursor.Current = Cursors.WaitCursor;
                IncTornilleria.IncTornilleria TO = new IncTornilleria.IncTornilleria();
                TO.MdiParent = this.MdiParent;
                TO.Show();
                Cursor.Current = Cursors.Default;
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            }
        }

        private void btnTurnosTaller_BotonAplicacionClick()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "GestorTurnosTaller").FirstOrDefault();
            if (f == null)
            {
                Cursor.Current = Cursors.WaitCursor;
                GestorTurnosTaller.GestorTurnosTaller TT = new GestorTurnosTaller.GestorTurnosTaller();
                TT.MdiParent = this.MdiParent;
                TT.Show();
                Cursor.Current = Cursors.Default;
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            }
        }


        private void btnTraductor_BotonAplicacionClick()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "GestorTurnosTaller").FirstOrDefault();
            if (f == null)
            {
                TraductorCodigos.TraductorCodigos TT = new TraductorCodigos.TraductorCodigos();
                TT.MdiParent = this.MdiParent;
                TT.Show();
                //TT.comboClientes1.SelectedIndex = 0;
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            }
        }

        private void btnGestionPintura_BotonAplicacionClick()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "GestorPintura").FirstOrDefault();
            if (f == null)
            {
                GestionPintura.GestionPintura GP = new GestionPintura.GestionPintura();
                GP.MdiParent = this.MdiParent;
                GP.Show();
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            }
        }

        private void btnTiemposProyectos_BotonAplicacionClick()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "TiemposProyectos").FirstOrDefault();
            if (f == null)
            {
                TiemposProyectos.TiemposProyectos TP = new TiemposProyectos.TiemposProyectos();
                TP.MdiParent = this.MdiParent;
                TP.Show();
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            }
        }

        private void btnRecSinProyectos_BotonAplicacionClick()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "ReclamacionesSinProyectos").FirstOrDefault();
            if (f == null)
            {
                ReclamacionesSinProyecto.ReclamacionesSinProyecto RSP = new ReclamacionesSinProyecto.ReclamacionesSinProyecto();
                RSP.MdiParent = this.MdiParent;
                RSP.Show();
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            }
        }

        private void btnPacking_BotonAplicacionClick()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "PackingNuevo").FirstOrDefault();
            if (f == null)
            {
                PackingNuevo.Prin PP = new PackingNuevo.Prin();
                PP.MdiParent = this.MdiParent;
                PP.Show();
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            }
        }

        private void btnCamiones_BotonAplicacionClick()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "CamionesGalvanizado").FirstOrDefault();
            if (f == null)
            {
                CamionesGalvanizado.ExportacionCamiones EC = new CamionesGalvanizado.ExportacionCamiones();
                EC.MdiParent = this.MdiParent;
                EC.Show();
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            }
        }

        private void btnReprocesado_BotonAplicacionClick()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "PaquetesReprocesado").FirstOrDefault();
            if (f == null)
            {
                PaquetesReprocesado.Form1 PR = new PaquetesReprocesado.Form1();
                PR.MdiParent = this.MdiParent;
                PR.Show();
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            }
        }

        private void btnInventario_BotonAplicacionClick()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "InventarioTablets").FirstOrDefault();
            if (f == null)
            {
                InventarioTablets.Prin PR = new InventarioTablets.Prin();
                PR.MdiParent = this.MdiParent;
                PR.Show();
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            }
        }

        private void botonAplicacion1_BotonAplicacionClick()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "RecepcionEtiquetas").FirstOrDefault();
            if (f == null)
            {
                RecepcionCamiones.RecepcionCamiones PR = new RecepcionCamiones.RecepcionCamiones();
                PR.MdiParent = this.MdiParent;
                PR.Show();
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            }
        }


        

        private void btnPlanosServicios_Click()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "ServicioPlanos").FirstOrDefault();
            if (f == null)
            {
                ServicioPlanos.Form1 PR = new ServicioPlanos.Form1();
                PR.MdiParent = this.MdiParent;
                PR.Show();
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            }
        }


        private void btnTablaPersonal_BotonAplicacionClick()
        {

            
                /*PlanosPedidos.PlanosPedidos formBuscador = new PlanosPedidos.PlanosPedidos();
                formBuscador.MdiParent = this.MdiParent;
                formBuscador.Show();*/
                Cursor.Current = Cursors.WaitCursor;
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = @"\\nas01\AppIMEDEXSA\Control de Taller\Produccion\Control de Taller.exe";
                p.Start();
                Cursor.Current = Cursors.Arrow;
                //p.WaitForExit();
        }


        
        //Gestion de ofertas
        private void botonAplicacion1_BotonAplicacionClick_1()
        {

            /*PlanosPedidos.PlanosPedidos formBuscador = new PlanosPedidos.PlanosPedidos();
            formBuscador.MdiParent = this.MdiParent;
            formBuscador.Show();*/
            Cursor.Current = Cursors.WaitCursor;
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = @"M:\Comercial\OFERTAS\GestionOfertas2.0\GestionOfertas.exe";
            p.Start();
            Cursor.Current = Cursors.Arrow;



        }

        private void botonAplicacion2_BotonAplicacionClick()
        {
            /*PlanosPedidos.PlanosPedidos formBuscador = new PlanosPedidos.PlanosPedidos();
            formBuscador.MdiParent = this.MdiParent;
            formBuscador.Show();*/
            Cursor.Current = Cursors.WaitCursor;
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = @"\\nas01\AppIMEDEXSA\AnalizadorTekla2023\IMEDEXSA.Tekla_Analyzer.application";
            p.Start();
            Cursor.Current = Cursors.Arrow;
        }

        private void botonM2Cliente_Click(object sender, EventArgs e)
        {
            
            
        }

        private void botonM2Cliente_BotonAplicacionClick()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "M2Clientes").FirstOrDefault();
            if (f == null)
            {
                M2Clientes.Form1 PR = new M2Clientes.Form1();
                PR.MdiParent = this.MdiParent;
                PR.Show();
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            }
        }

        private void btnInfoExp_BotonAplicacionClick()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "InfoExpediciones").FirstOrDefault();
            if (f == null)
            {
                InfoExpediciones.InfoExp IE = new InfoExpediciones.InfoExp();
                IE.MdiParent = this.MdiParent;
                IE.Show();
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            }
        }

        private void btnAutoLanza_BotonAplicacionClick()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "AutoLanzamientoProyectos").FirstOrDefault();
            if (f == null)
            {
                AutoLanzamientoProyectos.AutoLanzamientoProyectos AL = new AutoLanzamientoProyectos.AutoLanzamientoProyectos();
                AL.MdiParent = this.MdiParent;
                AL.Show();
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            }
        }
        // CARLOS 20/01/2026 - Se añade el botón para modificar la fecha de horizonte de Asprova
        private void btnHorizonteAsp_BotonAplicacionClick()
        {
            Form f = this.MdiParent.MdiChildren.OfType<Form>().Where(x => x.Name == "HorizonteAsprova").FirstOrDefault();
            if (f == null)
            {
                HorizonteAsprova.formHorizonte HA = new HorizonteAsprova.formHorizonte();
                HA.MdiParent = this.MdiParent;
                HA.Show();
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            }
        }

        private void botonAplicacion3_BotonAplicacionClick()
        {
            /*PlanosPedidos.PlanosPedidos formBuscador = new PlanosPedidos.PlanosPedidos();
            formBuscador.MdiParent = this.MdiParent;
            formBuscador.Show();*/
            Cursor.Current = Cursors.WaitCursor;
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = @"\\nas01\AppIMEDEXSA\AnalizadorTekla2024\IMEDEXSA.Tekla_Analyzer2024.application";
            p.Start();
            Cursor.Current = Cursors.Arrow;
        }

        private void btnRetornos_BotonAplicacionClick()
        {
            Cursor.Current = Cursors.WaitCursor;
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = @"\\nas01\AppIMEDEXSA\RetornosMaterial\RetornosExpediciones.application";
            p.Start();
            Cursor.Current = Cursors.Arrow;
        }

        private void btnPNIaLST_BotonAplicacionClick()
        {
            Cursor.Current = Cursors.WaitCursor;
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = @"\\nas01\AppIMEDEXSA\PNItoLST\FormateoArchivosPNI.application";
            p.Start();
            Cursor.Current = Cursors.Arrow;
        }

        private void btnColoresTorres_BotonAplicacionClick()
        {
            Cursor.Current = Cursors.WaitCursor;
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = @"\\nas01\AppIMEDEXSA\ColoresTorres\ColorTorresPintura.exe";
            p.Start();
            Cursor.Current = Cursors.Arrow;
        }

        // Carlos Casquero 11/03/2026 - Se añade el botón para el Tekla Analyzer 25
        private void btnAnalyzer_BotonAplicacionClick()
        {
            Cursor.Current = Cursors.WaitCursor;
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = @"\\nas01\AppIMEDEXSA\TEKLA 2025\AnalizadorTekla2025_PORTABLE\IMEDEXSA.Tekla_Analyzer.exe";
            p.Start();
            Cursor.Current = Cursors.Arrow;
        }
    }
}
