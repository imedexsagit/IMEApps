using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AprovisionamientoCalculo;
using System.Threading;
using empresaGlobalProj;


namespace AprovisionamientoCalculo
{
    public partial class AprovisionamientoCalculo : Form
    {
        //Atributos
        public string empresa;
        public string usuario;
        private AprovisionamientoCalculoBD bd;
        public int fin;
        

        //Métodos
        public AprovisionamientoCalculo()
        {
            InitializeComponent();
            empresaGlobal.showEmp = false;
            //JRM - Cambiar Empresa cambio "3" por empresaGlobal.empresaID
            empresa = empresaGlobal.empresaID; // "3";
            usuario = Environment.UserName;
            bd = new AprovisionamientoCalculoBD();
            fin = 0;

            //if (!bd.Tiene_Permisos(empresa, usuario))
            //{                
            //    label_aviso.Text = "NO TIENE PERMISOS PARA CALCULAR EL APROVISIONAMIENTO";
            //    button_calcular.Visible = false;
            //    barra_progreso.Visible = false;
            //    label_informes.Visible = false;
            //    label_mp.Visible = false;
            //    label_to.Visible = false;
            //    button_mp_completo.Visible = false;
            //    button_to_completo.Visible = false;
            //    button_to_completo.Visible = false;
            //    button_to_roturas.Visible = false;
            //    panel1.Visible = false;
            //}            
        }


        private void button_calcular_Click(object sender, EventArgs e)
        {
            string calculo;

            calculo = bd.Calculo_En_Ejecucion(empresa);
            if (calculo != "")
            {
                label_aviso.Text = "YA HAY UN CÁLCULO EN EJECUCIÓN POR: " + calculo;
                label_progreso.Text = "";
            }
            else {                                
                Cursor = Cursors.WaitCursor;
                button_calcular.Enabled = false;
                button_mp_completo.Enabled = false;
                button_mp_roturas.Enabled = false;
                button_to_completo.Enabled = false;
                button_to_roturas.Enabled = false;
                label_aviso.Text = "";
                label_aviso.Update();
                label_progreso.Text = "CALCULANDO...";
                label_progreso.Update();
                
                fin = 0;
                backgroundWorker.RunWorkerAsync();                
                Gestionar_Barra_Progreso();                                
            }
            
        }


        public void Gestionar_Barra_Progreso()
        {
            barra_progreso.Minimum = 1;
            barra_progreso.Maximum = 100;
            barra_progreso.Step = 1;
            barra_progreso.Value = 1;
            for (int i = barra_progreso.Minimum; i <= barra_progreso.Maximum - 5; i++)
            {
                if (fin != 0)
                    i = barra_progreso.Maximum;
                else
                {                    
                    barra_progreso.PerformStep();
                    Thread.Sleep(9000);
                }
                Application.DoEvents();
            }
        }              


        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {                
            if (bd.Calcular_Aprovisionamiento(empresa, usuario))               
                fin = 1;                
            else                 
                fin = -1;                
        }


        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (fin == 1)
            {
                barra_progreso.Value = barra_progreso.Maximum;
                label_progreso.Text = "CÁLCULO FINALIZADO CORRECTAMENTE";
            }
            else
            {
               // bd.Finalizar_Calculo_Erroneo(empresa);
                barra_progreso.Value = 1;
                label_progreso.Text = "ERROR AL REALIZAR EL CÁLCULO. INTÉNTELO DE NUEVO";
            }

            button_calcular.Enabled = true;
            button_mp_completo.Enabled = true;
            button_mp_roturas.Enabled = true;
            button_to_completo.Enabled = true;
            button_to_roturas.Enabled = true;                
            Cursor = Cursors.Arrow;
        }

        
        private void button_mp_completo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("IExplore.exe", "http://srvsql02/Reports/Pages/Report.aspx?ItemPath=%2fCompras%2fAprovisionamiento%2fAprovisionamiento_MP"); 
        }


        private void button_mp_roturas_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("IExplore.exe", "http://srvsql02/Reports/Pages/Report.aspx?ItemPath=%2fCompras%2fAprovisionamiento%2fAprovisionamiento_MP_SoloRoturas"); 
        }


        private void button_to_completo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("IExplore.exe", "http://srvsql02/Reports/Pages/Report.aspx?ItemPath=%2fCompras%2fAprovisionamiento%2fAprovisionamiento_TO"); 
        }


        private void button_to_roturas_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("IExplore.exe", "http://srvsql02/Reports/Pages/Report.aspx?ItemPath=%2fCompras%2fAprovisionamiento%2fAprovisionamiento_TO_SoloRoturas"); 
        }

        private void AprovisionamientoCalculo_FormClosing(object sender, FormClosingEventArgs e)
        {
            empresaGlobal.showEmp = true;
        }
       
                                       
    }
}
