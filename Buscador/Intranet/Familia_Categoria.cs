using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Intranet.DAL;

namespace Intranet
{
    public partial class Familia_Categoria : Form
    {
        public bool aceptado = false;
        public string salida;

        // VSS: para almacenar el tipo de filtro y el valor
        public string filtro;
        public string valor;


        public Familia_Categoria(string filtro,string valor)
        {
            InitializeComponent();

            // VSS: almacenamos el tipo de filtro y el valor
            this.filtro = filtro;
            this.valor = valor;
            textFiltroLista.Focus();

            this.Text = filtro;
            
            //switch (filtro) { 
            //    case "Familia":            
            //        this.t_FAMILIASTableAdapter.FillBy(this.dsFamilias.T_FAMILIAS,"3");
            //        LLenar_Familias_Categorias(this.dsFamilias.T_FAMILIAS,valor);
            //        break;
            //    case "Categoría":            
            //        this.t_CATEGORIASTableAdapter.FillBy(this.dsCategorias.T_CATEGORIAS,"3");
            //        LLenar_Familias_Categorias(this.dsCategorias.T_CATEGORIAS,valor);
            //        break;
            //    case "Comercial":
            //        this.t_REPRESTableAdapter1.Fill(this.dsRepres1.T_REPRES, "3");
            //        LLenar_Familias_Categorias(this.dsRepres1.T_REPRES,valor);
            //        break;
            //    case "Lugar":
            //        this.t_ExpedicionesTableAdapter1.Fill(this.dsLugar1.T_Expediciones, "3");
            //        LLenar_Familias_Categorias(this.dsLugar1.T_Expediciones, valor);
            //        break;
            //    case "Cliente":
            //        this.t_CLIENTESTableAdapter1.Fill(this.dsClientes1.T_CLIENTES, "3");
            //        LLenar_Familias_Categorias(this.dsClientes1.T_CLIENTES, valor);
            //        break;
            //}       
     
            // VSS: 
            filtrar(filtro, valor, "");
        }

        private void filtrar(string filtro, string valor, string filtroLista)
        {
            DataTable dt;
            switch (filtro)
            {
                case "Familia":                    
                    //this.t_FAMILIASTableAdapter.FillBy(this.dsFamilias.T_FAMILIAS, "3");
                    //LLenar_Familias_Categorias(this.dsFamilias.T_FAMILIAS, valor);
                    dt = global.Familias(filtroLista);
                    LLenar_Familias_Categorias(dt, valor);
                    break;
                case "Categoría":
                    //this.t_CATEGORIASTableAdapter.FillBy(this.dsCategorias.T_CATEGORIAS, "3");
                    //LLenar_Familias_Categorias(this.dsCategorias.T_CATEGORIAS, valor);
                    dt = global.Categorias(filtroLista);
                    LLenar_Familias_Categorias(dt, valor);
                    break;
                case "Comercial":
                    //this.t_REPRESTableAdapter1.Fill(this.dsRepres1.T_REPRES, "3");
                    //LLenar_Familias_Categorias(this.dsRepres1.T_REPRES, valor);
                    dt = global.Comerciales(filtroLista);
                    LLenar_Familias_Categorias(dt, valor);
                    break;
                case "Lugar":
                    //this.t_ExpedicionesTableAdapter1.Fill(this.dsLugar1.T_Expediciones, "3");
                    //LLenar_Familias_Categorias(this.dsLugar1.T_Expediciones, valor);
                    dt = global.Lugares(filtroLista);
                    LLenar_Familias_Categorias(dt, valor);
                    break;
                case "Cliente":
                    //this.t_CLIENTESTableAdapter1.Fill(this.dsClientes1.T_CLIENTES, "3");
                    //LLenar_Familias_Categorias(this.dsClientes1.T_CLIENTES, valor);
                    dt = global.Clientes(filtroLista);
                    LLenar_Familias_Categorias(dt, valor);
                    break;
            }            
        }

        public void LLenar_Familias_Categorias(DataTable table,string valor) {
            checkedListBox_familia_categoria.Items.Clear();

            if (valor == "")
            {
                foreach (DataRow row in table.Rows)
                {
                    //checkedListBox_familia_categoria.Items.Add(row[0].ToString() + " - " + row[1].ToString(), false);
                    checkedListBox_familia_categoria.Items.Add(row[1].ToString() + "   -->   " + row[0].ToString(), false);
                }
            }
            else 
            {
                string[] separar;
                separar = valor.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (DataRow row in table.Rows)
                {
                    //checkedListBox_familia_categoria.Items.Add(row[0].ToString() + " - " + row[1].ToString(), Comprobar_Chequeo(row[0].ToString(),separar));
                    checkedListBox_familia_categoria.Items.Add(row[1].ToString() + "   -->   " + row[0].ToString(), Comprobar_Chequeo(row[0].ToString(), separar));
                }
            }
        }



        public bool Comprobar_Chequeo(string dato, string[] vector)         
        {
            foreach (string s in vector)
            {
                if (s == dato)
                    return true;
            }
            return false;
        }



        private void checkedListBox_familia_categoria_SelectedIndexChanged(object sender, EventArgs e)
        {            
            checkedListBox_familia_categoria.SetItemChecked(checkedListBox_familia_categoria.SelectedIndex, !checkedListBox_familia_categoria.GetItemChecked(checkedListBox_familia_categoria.SelectedIndex));
        }



        private void button_cancelar_Click(object sender, EventArgs e)
        {
            aceptado = false;
            Close();
        }



        private void button_aceptar_Click(object sender, EventArgs e)
        {
            aceptado = true;
            salida = "";
            int posicion,longitud;            

            foreach (object item in checkedListBox_familia_categoria.CheckedItems)
            {
                longitud = item.ToString().Length;
                posicion = item.ToString().IndexOf('>');
                    
                if (salida == "")                
                    //salida = item.ToString().Substring(0,item.ToString().IndexOf('-') - 1);
                    salida = item.ToString().Substring(posicion + 4, longitud - (posicion + 4));                
                else                
                    //salida = salida + "#" + item.ToString().Substring(0, item.ToString().IndexOf('-') - 1);                
                    salida = salida + "#" + item.ToString().Substring(posicion + 4, longitud - (posicion + 4));                                
            }
            Close();
        }

        // VSS
        private void textFiltroLista_TextChanged(object sender, EventArgs e)
        {
            string filtroLista = textFiltroLista.Text;
            //if (filtroLista.Length > 1)
            //{
                filtrar(filtro, "", filtroLista);
            //}
        }    

    }
}
