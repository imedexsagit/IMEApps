using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using Utils;
using empresaGlobalProj;


namespace IMEDEXSA
{
    public partial class CategoriasCaracteristicas : Form
    {
        private string codEmp;
        private string cadenaConexion;

        public CategoriasCaracteristicas()
        {
            InitializeComponent();

            codEmp = empresaGlobal.empresaID;//ConfigurationManager.AppSettings["EMPRESA"].ToString(); //empresaGlobal.empresaID;
            cadenaConexion = ConfigurationManager.AppSettings["ggConnectionString"].ToString();               
            
            Cargar_Categorias_Sin_Jeraquizar();            
            Cargar_Categorias_Jeraquizadas();                        
        }

        public void Cargar_Categorias_Sin_Jeraquizar() {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;
                        
            try
            {
                conexion = new SqlConnection(cadenaConexion);
                conexion.Open();

                strsql = "";
                strsql = strsql + " SELECT  CATEGO as Código, DENOMI as Denominación";              
                strsql = strsql + " FROM    T_CATEGORIAS";
                strsql = strsql + " WHERE   CODEMP = '" + codEmp + "' AND";
                strsql = strsql + "         CATEGO NOT IN (SELECT CATEGO_PADRE FROM TC_CATEGORIAS_JERARQUIA WHERE CODEMP = '" + codEmp + "') AND";
                strsql = strsql + "         CATEGO NOT IN (SELECT CATEGO_HIJA FROM TC_CATEGORIAS_JERARQUIA WHERE CODEMP = '" + codEmp + "')";
                strsql = strsql + " ORDER BY CATEGO";
                
                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);
                dataGridView_categorias.DataSource = table;
                
                conexion.Close();                                
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();                
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView_categorias_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView_categorias.ClearSelection();
        }

        private void button_nueva_categoria_Click(object sender, EventArgs e)
        {
            
            string codigo, denominacion, abreviatura;

            try
            {
                codigo = Microsoft.VisualBasic.Interaction.InputBox("CÓDIGO (máximo 2 caracteres)", "Nueva categoría");
                codigo = codigo.Trim();
                if (codigo == "")
                    return;
                if (codigo.Length > 2) {
                    MessageBox.Show("El código debe tener como máximo 2 caracteres", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                denominacion = Microsoft.VisualBasic.Interaction.InputBox("DENOMINACIÓN (máximo 30 caracteres)", "Nueva categoría");
                denominacion = denominacion.Trim();
                if (denominacion == "")
                    return;
                if (denominacion.Length > 30)
                {
                    MessageBox.Show("La denominación debe tener como máximo 30 caracteres", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (denominacion.Length >= 3)
                    abreviatura = denominacion.Substring(0, 3);
                else {
                    if (denominacion.Length >= 2)
                        abreviatura = denominacion.Substring(0, 2);
                    else
                        abreviatura = denominacion;
                }

                if (CD.Crear_Categoria(codEmp, codigo, denominacion, abreviatura.ToUpper().Trim(), Environment.UserName))
                {
                    MessageBox.Show("Categoría creada correctamente", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Cargar_Categorias_Sin_Jeraquizar();
                }
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_eliminar_categoria_Click(object sender, EventArgs e)
        {
            string categoria;
            string nombre;
            DialogResult result;

            if (dataGridView_categorias.SelectedRows.Count > 0) {
                result = MessageBox.Show("¿Realmente desea eliminar las categorías seleccionadas?", "CONFIRMAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                    return;
            }
            
            foreach (DataGridViewRow row in dataGridView_categorias.SelectedRows)
            {
                categoria = row.Cells[0].Value.ToString();
                nombre = row.Cells[1].Value.ToString();
                if (CD.Existe_Articulo_Categoria(codEmp, categoria))
                    MessageBox.Show("La categoría [" + categoria + " - " + nombre + "] no se puede eliminar porque existen artículos con esta categoría", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                {
                    CD.Eliminar_Categoria(codEmp, categoria);
                    MessageBox.Show("La categoría [" + categoria + " - " + nombre + "] ha sido eliminada correctamente", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }                               
            }
            Cargar_Categorias_Sin_Jeraquizar();
        }       
       
        public void Cargar_Categorias_Jeraquizadas()
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;
                        
            try
            {
                conexion = new SqlConnection(cadenaConexion);
                conexion.Open();

                strsql = "";
                strsql = strsql + " SELECT      CATEGO_PADRE,CATEGO_HIJA,DENOMI_PADRE,DENOMI_HIJA";
                strsql = strsql + " FROM        TC_CATEGORIAS_JERARQUIA";
                strsql = strsql + " WHERE       CODEMP = '" + codEmp + "'";
                strsql = strsql + " ORDER BY    CATEGO_PADRE,CATEGO_HIJA";                

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);
  
                //Llenar TreeView
                treeView_jerarquia.Nodes.Clear();
                TreeNode[] nodos;                
                foreach(DataRow row in table.Rows)
                {                    
                    nodos = treeView_jerarquia.Nodes.Find(row[0].ToString(), true);
                    if (nodos.Length == 0)
                    {
                        treeView_jerarquia.Nodes.Add(row[0].ToString(), row[0].ToString() + " - " + row[2].ToString()).Expand();
                        if(row[1].ToString() != "-" && row[3].ToString() != "-")
                            treeView_jerarquia.Nodes[row[0].ToString()].Nodes.Add(row[1].ToString(), row[1].ToString() + " - " + row[3].ToString()).Expand();
                    }
                    else
                        nodos[0].Nodes.Add(row[1].ToString(), row[1].ToString() + " - " + row[3].ToString()).Expand();
                }

                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
     
        private void button_añadir_jerarquia_Click(object sender, EventArgs e)
        {                        
            string catego_padre, catego_hija, denomi_padre, denomi_hija, categoria;
            int pos;

            try
            {
                if (dataGridView_categorias.SelectedRows.Count > 0)
                {
                    if (treeView_jerarquia.SelectedNode == null)
                    {
                        foreach (DataGridViewRow row in dataGridView_categorias.SelectedRows)
                        {
                            catego_padre = row.Cells[0].Value.ToString();
                            denomi_padre = row.Cells[1].Value.ToString();
                            CD.Crear_Jerarquia_Categorias(codEmp, catego_padre, "-", denomi_padre, "-", Environment.UserName);
                        }
                    }
                    else
                    {
                        categoria = treeView_jerarquia.SelectedNode.Text;                        
                        pos = categoria.IndexOf('-');
                        catego_padre = categoria.Substring(0,pos-1);
                        denomi_padre = categoria.Substring(pos + 2, categoria.Length - (pos + 2));
                        foreach (DataGridViewRow row in dataGridView_categorias.SelectedRows)
                        {
                            catego_hija = row.Cells[0].Value.ToString();
                            denomi_hija = row.Cells[1].Value.ToString();
                            if(treeView_jerarquia.SelectedNode.Parent == null && treeView_jerarquia.SelectedNode.Nodes.Count == 0)
                                CD.Eliminar_Jerarquia_Categorias(codEmp, catego_padre, "-");
                            CD.Crear_Jerarquia_Categorias(codEmp, catego_padre, catego_hija, denomi_padre, denomi_hija, Environment.UserName);
                        }                                                                        
                    }
                    Cargar_Categorias_Sin_Jeraquizar();
                    Cargar_Categorias_Jeraquizadas();
                    treeView_jerarquia.ExpandAll();
                    Vaciar_Caracteristicas();
                }
            }
            catch (Exception ex)
            {             
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView_categorias_Click(object sender, EventArgs e)
        {
            treeView_jerarquia.SelectedNode = null;
            Vaciar_Caracteristicas();
        }

        public void Eliminar_Jerarquia_Nodo(TreeNode nodo)
        {
            if (nodo.Nodes.Count > 0 || nodo.Parent == null)
            {
                CD.Eliminar_Jerarquia_Categorias(codEmp, nodo.Text.Substring(0, nodo.Text.IndexOf('-') - 1), "*");
                CD.Eliminar_Jerarquia_Categorias(codEmp,"*", nodo.Text.Substring(0, nodo.Text.IndexOf('-') - 1));
                foreach (TreeNode node in nodo.Nodes)
                    Eliminar_Jerarquia_Nodo(node);
            }
            else {
                if (nodo.Nodes.Count == 0 && nodo.Parent != null)
                    if (nodo.Parent.Nodes.Count > 1 || nodo.Parent.Parent != null)
                        CD.Eliminar_Jerarquia_Categorias(codEmp, nodo.Parent.Text.Substring(0, nodo.Parent.Text.IndexOf('-') - 1), nodo.Text.Substring(0, nodo.Text.IndexOf('-') - 1));
                    else
                        CD.Actualizar_Jerarquia_Categorias(codEmp, nodo.Parent.Text.Substring(0, nodo.Parent.Text.IndexOf('-') - 1), nodo.Text.Substring(0, nodo.Text.IndexOf('-') - 1), "-", Environment.UserName);
            }
        }

        private void button_quitar_jerarquia_Click(object sender, EventArgs e)
        {
            if (treeView_jerarquia.SelectedNode != null)
            {
                Eliminar_Jerarquia_Nodo(treeView_jerarquia.SelectedNode);
                Cargar_Categorias_Sin_Jeraquizar();
                Cargar_Categorias_Jeraquizadas();
                treeView_jerarquia.ExpandAll();
                Vaciar_Caracteristicas();
            }
        }


        public void Cargar_Caracteristicas()
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;
            string categoria;

            try
            {
                conexion = new SqlConnection(cadenaConexion);
                conexion.Open();

                if (treeView_jerarquia.SelectedNode == null)
                    categoria = "";
                else
                    categoria = treeView_jerarquia.SelectedNode.Text.Substring(0, treeView_jerarquia.SelectedNode.Text.IndexOf('-') - 1);
                
                strsql = "";
                if (categoria == "")
                {
                    dataGridView_caracteristicas.Columns["Asignada"].ReadOnly = true;
                    strsql = strsql + " SELECT  0 as Asignada,";
                }
                else
                {
                    dataGridView_caracteristicas.Columns["Asignada"].ReadOnly = false;
                    strsql = strsql + " SELECT  (select count(*) from t_carcat where codemp = '" + codEmp + "' and catego = '" + categoria + "' and caract = t1.caract) as Asignada,";
                }
                strsql = strsql + "     t1.Caract as Caracteristica, t1.denomi as Denominacion, t1.clave1 as Numerica, t1.clave3 as Valores";
                strsql = strsql + "     FROM    T_CARACT as t1";
			    strsql = strsql + "     WHERE   t1.CODEMP = '" + codEmp + "' AND "; 
                strsql = strsql + "             t1.CARACT >= '500'";
                if(categoria != "" && !checkBox_todas_caracteristicas.Checked)
                    strsql = strsql + "         and (select count(*) from t_carcat where codemp = '" + codEmp + "' and catego = '" + categoria + "' and caract = t1.caract) = 1";
                if (categoria == "" && !checkBox_todas_caracteristicas.Checked)
                    strsql = strsql + "         and t1.codemp = '-1'";
                strsql = strsql + "     ORDER BY t1.CARACT";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);
                dataGridView_caracteristicas.DataSource = table;

                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView_caracteristicas_Click(object sender, EventArgs e)
        {
            dataGridView_categorias.ClearSelection();
        }

        private void dataGridView_caracteristicas_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView_caracteristicas.ClearSelection();
        }

        public void Vaciar_Caracteristicas() {
            label_categoria.Text = "";            
            DataTable dt = (DataTable)dataGridView_caracteristicas.DataSource;
            if(dt != null)
                dt.Clear();
            checkBox_todas_caracteristicas.Checked = false;
        }
        
        private void treeView_jerarquia_AfterSelect(object sender, TreeViewEventArgs e)
        {           
            label_categoria.Text = treeView_jerarquia.SelectedNode.Text;
            Cargar_Caracteristicas();
        }

        private void checkBox_todas_caracteristicas_CheckedChanged(object sender, EventArgs e)
        {
            if (treeView_jerarquia.SelectedNode == null && !checkBox_todas_caracteristicas.Checked)            
                Vaciar_Caracteristicas();            
            else
                Cargar_Caracteristicas();

            if (checkBox_todas_caracteristicas.Checked)
                dataGridView_categorias.ClearSelection();
            
        }
             
        private void dataGridView_caracteristicas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_caracteristicas.IsCurrentCellDirty) {
                DataGridViewCell celda = dataGridView_caracteristicas.CurrentCell;
                string categoria = label_categoria.Text.Substring(0, label_categoria.Text.IndexOf('-') - 1);
                string caracteristica = dataGridView_caracteristicas.Rows[celda.RowIndex].Cells[celda.ColumnIndex + 1].Value.ToString();
                string nombre_caracteristica = dataGridView_caracteristicas.Rows[celda.RowIndex].Cells[celda.ColumnIndex + 2].Value.ToString();
                
                if (dataGridView_caracteristicas.Columns[celda.ColumnIndex].Name == "Asignada") {                                        
                    if (celda.Value.ToString() == "1")
                    {
                        DialogResult result = MessageBox.Show("¿Realmente desea quitar la característica [" + caracteristica + " - " + nombre_caracteristica + "] de la categoría [" + label_categoria.Text + "]?" + Environment.NewLine + Environment.NewLine + "Se eliminarán los valores de esta característica para todos los códigos de la categoría.", "CONFIRMAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.No)
                        {
                            Cargar_Caracteristicas();
                            return;
                        }
                        else
                        {
                            CD.Quitar_Caracteristica_Categoria(codEmp, categoria, caracteristica);
                        }
                    }
                    else {
                        CD.Asociar_Caracteristica_Categoria(codEmp, categoria, caracteristica, Environment.UserName);
                    }
                    dataGridView_caracteristicas.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }               
            }                        
        }

        private void dataGridView_caracteristicas_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (label_categoria.Text != "" || checkBox_todas_caracteristicas.Checked)
            {
                DataGridViewCell celda = dataGridView_caracteristicas.CurrentCell;
                
                if (dataGridView_caracteristicas.Columns[celda.ColumnIndex].Name == "Denominacion")
                {
                    string caracteristica = dataGridView_caracteristicas.Rows[celda.RowIndex].Cells[celda.ColumnIndex - 1].Value.ToString();
                    CD.Actualizar_Nombre_Caracteristica(codEmp, caracteristica, dataGridView_caracteristicas.Rows[celda.RowIndex].Cells[celda.ColumnIndex].Value.ToString(), Environment.UserName);
                }                                
            }               
        }

        private void button_nueva_caracteristica_Click(object sender, EventArgs e)
        {
            dataGridView_caracteristicas.AllowUserToAddRows = true;
            dataGridView_caracteristicas.Columns["Asignada"].ReadOnly = true;            
            dataGridView_caracteristicas.Columns["Numerica"].ReadOnly = false;
            dataGridView_caracteristicas.Columns["Valores"].ReadOnly = false;
            button_guardar_caracteristica.Visible = true;
            button_guardar_caracteristica.Focus();
            button_nueva_caracteristica.Visible = false;
            button_eliminar_caracteristica.Visible = false;
        }

        private void button_guardar_caracteristica_Click(object sender, EventArgs e)
        {
            dataGridView_caracteristicas.AllowUserToAddRows = false;                        
            dataGridView_caracteristicas.Columns["Numerica"].ReadOnly = true;
            dataGridView_caracteristicas.Columns["Valores"].ReadOnly = true;
            button_nueva_caracteristica.Visible = true;
            button_eliminar_caracteristica.Visible = true;
            button_nueva_caracteristica.Focus();
            button_guardar_caracteristica.Visible = false;
            foreach (DataGridViewRow row in dataGridView_caracteristicas.Rows)
            {
                if (row.Cells[1].Value == null || row.Cells[1].Value.ToString() == "")
                {
                    if (row.Cells[2].Value.ToString() != "" && row.Cells[3].Value.ToString() != "" && row.Cells[4].Value.ToString() != "")
                    {
                        CD.Crear_Caracteristica(codEmp, CD.Obtener_Max_Caracteristica(codEmp), row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString(), row.Cells[4].Value.ToString(), Environment.UserName);
                    }
                }                    
            }
            Cargar_Caracteristicas();
        }

        private void button_eliminar_caracteristica_Click(object sender, EventArgs e)
        {
            DialogResult result;
            string caracteristica;
            string nombre_caracteristica;
            bool actualizar = false;

            foreach (DataGridViewRow row in dataGridView_caracteristicas.SelectedRows)
            {
                caracteristica = row.Cells[1].Value.ToString();
                nombre_caracteristica = row.Cells[2].Value.ToString();
                               
                result = MessageBox.Show("¿Realmente desea eliminar la característica [" + caracteristica + " - " + nombre_caracteristica + "]?", "CONFIRMAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    CD.Eliminar_Caracteristica(codEmp, caracteristica);
                    actualizar = true;
                }
            }

            if(actualizar)
                Cargar_Caracteristicas();
        }

        public void Cargar_Valores_Caracteristicas(string caracteristica)
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;            

            try
            {
                conexion = new SqlConnection(cadenaConexion);
                conexion.Open();
                
                strsql = "";
                strsql = strsql + " SELECT  valor, abrev as descripcion, valor as valor_oculto";
                strsql = strsql + " FROM    t_valcar";
                strsql = strsql + " WHERE   CODEMP = '" + codEmp + "' AND";
                strsql = strsql + "         CARACT = '" + caracteristica + "'";
                strsql = strsql + " ORDER BY valor";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);                
                dataGridView_valores.DataSource = table;

                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView_valores_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView_valores.ClearSelection();
        }

        private void dataGridView_caracteristicas_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView_caracteristicas.SelectedRows.Count > 0 && dataGridView_caracteristicas.SelectedRows.Count < 2)
            {
                Cargar_Valores_Caracteristicas(dataGridView_caracteristicas.SelectedRows[0].Cells[1].Value.ToString());
                label_caracteristica.Text = dataGridView_caracteristicas.SelectedRows[0].Cells[1].Value.ToString() + " - " + dataGridView_caracteristicas.SelectedRows[0].Cells[2].Value.ToString();
                if (dataGridView_caracteristicas.SelectedRows[0].Cells[4].Value.ToString() == "F")
                {
                    button_nuevo_valor.Visible = false;
                    button_eliminar_valor.Visible = false;
                }
                else {
                    button_nuevo_valor.Visible = true;
                    button_eliminar_valor.Visible = true;
                }
            }
            else 
            {
                Cargar_Valores_Caracteristicas("-111");
                label_caracteristica.Text = "";
                button_nuevo_valor.Visible = false;
                button_eliminar_valor.Visible = false;
            }            
        }

        private void dataGridView_valores_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!button_guardar_valor.Visible)
            {
                if (label_caracteristica.Text != "")
                {
                    DataGridViewCell celda = dataGridView_valores.CurrentCell;
                    string caracteristica = dataGridView_caracteristicas.SelectedRows[0].Cells[1].Value.ToString();

                    if (dataGridView_valores.Columns[celda.ColumnIndex].Name == "valor")
                    {
                        string numerica = dataGridView_caracteristicas.SelectedRows[0].Cells["Numerica"].Value.ToString();
                        if (numerica == "S")
                        {
                            double numero;
                            if (!double.TryParse(celda.Value.ToString().Replace('.', ','), out numero))
                                MessageBox.Show("Los valores de la característica[" + label_caracteristica.Text.ToString() + "] deben ser númericos", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            else
                                CD.Actualizar_Valor_Caracteristica(codEmp, caracteristica, dataGridView_valores.Rows[celda.RowIndex].Cells["valor_oculto"].Value.ToString(), celda.Value.ToString().Replace('.', ','), dataGridView_valores.Rows[celda.RowIndex].Cells["descripcion"].Value.ToString(), Environment.UserName);
                        }
                        else
                            CD.Actualizar_Valor_Caracteristica(codEmp, caracteristica, dataGridView_valores.Rows[celda.RowIndex].Cells["valor_oculto"].Value.ToString(), celda.Value.ToString(), dataGridView_valores.Rows[celda.RowIndex].Cells["descripcion"].Value.ToString(), Environment.UserName);
                    }
                    else
                        CD.Actualizar_Valor_Caracteristica(codEmp, caracteristica, dataGridView_valores.Rows[celda.RowIndex].Cells["valor_oculto"].Value.ToString(), dataGridView_valores.Rows[celda.RowIndex].Cells["valor"].Value.ToString(), celda.Value.ToString(), Environment.UserName);

                    Cargar_Valores_Caracteristicas(caracteristica);
                }
            }
        }

        private void button_nuevo_valor_Click(object sender, EventArgs e)
        {
            dataGridView_valores.AllowUserToAddRows = true;
            button_guardar_valor.Visible = true;
            button_guardar_valor.Focus();
            button_nuevo_valor.Visible = false;
            button_eliminar_valor.Visible = false;        
        }

        private void button_guardar_valor_Click(object sender, EventArgs e)
        {
            dataGridView_valores.AllowUserToAddRows = false;
            button_nuevo_valor.Visible = true;
            button_eliminar_valor.Visible = true;
            button_nuevo_valor.Focus();
            button_guardar_valor.Visible = false;

            string caracteristica = dataGridView_caracteristicas.SelectedRows[0].Cells["Caracteristica"].Value.ToString();
            string numerica = dataGridView_caracteristicas.SelectedRows[0].Cells["Numerica"].Value.ToString();
            double numero;

            foreach (DataGridViewRow row in dataGridView_valores.Rows)
            {
                if (row.Cells["valor_oculto"].Value == null || row.Cells["valor_oculto"].Value.ToString() == "")
                {
                    if (row.Cells["valor"].Value != null && row.Cells["valor"].Value.ToString() != "")
                    {
                        if (numerica == "S")
                        {
                            if (double.TryParse(row.Cells["valor"].Value.ToString().Replace('.', ','), out numero))
                                CD.Crear_Valor_Caracteristica(codEmp, caracteristica, row.Cells["valor"].Value.ToString().Replace('.', ','), row.Cells["descripcion"].Value.ToString(), Environment.UserName);                                
                        }
                        else
                            CD.Crear_Valor_Caracteristica(codEmp, caracteristica, row.Cells["valor"].Value.ToString(), row.Cells["descripcion"].Value.ToString(), Environment.UserName);
                    }                    
                }
            }
            Cargar_Valores_Caracteristicas(caracteristica);
        }

        private void button_eliminar_valor_Click(object sender, EventArgs e)
        {
            string caracteristica = dataGridView_caracteristicas.SelectedRows[0].Cells["Caracteristica"].Value.ToString();            
            bool actualizar = false;

            foreach (DataGridViewRow row in dataGridView_valores.SelectedRows)
            {
                CD.Eliminar_Valor_Caracteristica(codEmp, caracteristica, row.Cells["valor"].Value.ToString());
                actualizar = true;
            }

            if (actualizar)
                Cargar_Valores_Caracteristicas(caracteristica);
        }
                
    }
}
