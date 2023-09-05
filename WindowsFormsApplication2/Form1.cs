using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        private ConMysql conMysql = new ConMysql();
        DataTable dt = new DataTable();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnconectar_Click(object sender, EventArgs e)
        {
            conMysql.ConectarBdMysql();
            if (conMysql.ConectarBdMysql() == true)
            {
                MessageBox.Show("Conección exitosa");
            }
            else
            {
                MessageBox.Show("Fallo e la Conección");
            }
            this.Dispose();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            conMysql.ConectarBdMysql();
            string consultaInsercion = "INSERT INTO tabla1 (nombre) VALUES ('dato1')";
            conMysql.altasBajasCambios(consultaInsercion);

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            conMysql.ConectarBdMysql();
            string consultaEliminar = "DELETE FROM tabla1 WHERE id = (SELECT MAX(id)FROM tabla1);";
            conMysql.altasBajasCambios(consultaEliminar);
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            conMysql.ConectarBdMysql();

            string consulta = "SELECT id, nombre FROM tabla1";
            dt = conMysql.sacarDatos(consulta);
            dataGridView1.DataSource = dt;

        }



        private void btnCambiar_Click(object sender, EventArgs e)
        {
            conMysql.ConectarBdMysql();
            string consultaActualizar = "UPDATE tabla1 SET nombre = 'nuevo' WHERE id = (SELECT MAX(id)FROM tabla1);";
            conMysql.altasBajasCambios(consultaActualizar);
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            conMysql.ConectarBdMysql();
            DataTable dt = new DataTable();
            string consulta = "SELECT id, nombre FROM tabla1";
            dt = conMysql.sacarDatos(consulta);
            dataGridView1.DataSource = dt;


            /*
            dataGridView1.ColumnCount = 1; //número de columnas
            dataGridView1.Columns[0].Name = "Nombre"; // nombre a la columna

            // Agregar la cuadrícula al formulario 
            if (!Controls.Contains(dataGridView1))
            {
                Controls.Add(dataGridView1);
                dataGridView1.Location = new System.Drawing.Point(10, 50); // Ubicación en el formulario
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // tamaño de columnas
            }.          
             */

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            conMysql.ConectarBdMysql();
            /*
            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                DataGridViewCell celda = fila.Cells["nombre"];

                if (celda.Value != null && !string.IsNullOrWhiteSpace(celda.Value.ToString()))
                {
                    int id = Convert.ToInt32(fila.Cells["id"].Value);
                    string nuevoNombre = celda.Value.ToString();

                    string consultaInsercion = "UPDATE tabla1 SET nombre = '" + nuevoNombre + "' WHERE id = " + id;
                    conMysql.altasBajasCambios(consultaInsercion);
                }
            }
             */

            string consulta = "SELECT id, nombre FROM tabla1";
            dt = conMysql.sacarDatos(consulta);

            DataTable dt2 = new DataTable();

            // Agregar columnas al DataTable 
            //dt2.Columns.Add ("id"); // nombre a la columna
            //dt2.Columns.Add("nombre");
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                dt2.Columns.Add(column.HeaderText, typeof(string));
            }

            // Agregar filas al DataTable.
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataRow dataRow = dt2.NewRow();
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataRow[i] = row.Cells[i].Value;
                }
                dt2.Rows.Add(dataRow);
            }


            DataColumn colId = dt.Columns["id"];
            DataColumn colNombre = dt.Columns["nombre"];

            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                int id1 = (int)dt.Rows[i][colId];
                int id2 = (int)dt2.Rows[i][colId];
                string nombreOriginal1 = (string)dt.Rows[i][colNombre];
                string nombreOriginal2 = (string)dt2.Rows[i][colNombre];
    

                if(id1==id2 && nombreOriginal1!=nombreOriginal2){
                    string consultaInsercion = "UPDATE tabla1 SET nombre = '" + nombreOriginal2 + "'WHERE id =" + id2 + ";";
                    conMysql.altasBajasCambios(consultaInsercion);
                }
            }


            
        }
    }
}
