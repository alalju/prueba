using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using System.Data.OleDb;
using MySql.Data.MySqlClient;
using Microsoft.VisualBasic;
using System.Runtime.InteropServices;


namespace WindowsFormsApplication2
{
    public class ConMysql
    {
        public MySqlConnection con_fact;
        public static string ipservidor =  "127.0.0.1"; 
     

        public ConMysql()
        {

        }
        public bool ConectarBdMysql()
        {
            //conexion de BD 
            try
            {
                MySqlConnection conlocal = new MySqlConnection();
                conlocal.ConnectionString = "Data Source=" + ipservidor + ";Database=prueba;User ID=prueba;Password=prueba;";

                conlocal.Open();
                con_fact = conlocal;
                return true;
            }
            catch
            {
                MessageBox.Show("Error al conectarse a la base de datos de contabilidad");
                return false;
            }
        }


        public MySqlConnection ConectarBdMysql2()
        {
            //conexion de BD 
            try
            {
                MySqlConnection conlocal = new MySqlConnection();
                conlocal.ConnectionString = "Data Source=" + ipservidor + ";Database=prueba;User ID=user;Password=user;";

                conlocal.Open();
                con_fact = conlocal;
                return con_fact;
            }
            catch
            {
                MessageBox.Show("Error al conectarse a la base de datos de contabilidad");
                return con_fact;
            }
        }

        public void altasBajasCambios(string cad)
        {
            if (ConectarBdMysql() == true)
            {
                MySqlCommand comando = new MySqlCommand();
                comando.Connection = con_fact;
                comando.CommandText = cad;
                comando.ExecuteNonQuery();
                cerrar_fact();
            }
        }

        public DataTable sacarDatos(string sql)
        {
            DataTable dt = new DataTable();
            if (ConectarBdMysql() == true)
            {
                MySqlDataAdapter da = new MySqlDataAdapter(sql, con_fact);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0];
                cerrar_fact();
            }
            return dt;
        }

        public void cerrar_fact()
        {
            con_fact.Close();
            con_fact.Dispose();
        }

      
      
        



    }
}
