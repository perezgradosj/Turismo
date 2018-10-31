using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace Turismo.Models
{
    public class Conexion
    {
        public SqlConnection getConexion()
        {
            string Fuente = ConfigurationManager.AppSettings["Fuente"].ToString();
            string BaseDatos = ConfigurationManager.AppSettings["BaseDatos"].ToString();
            string Usuario = ConfigurationManager.AppSettings["Usuario"].ToString();
            string Clave = ConfigurationManager.AppSettings["Clave"].ToString();

            string cadena = "Server=" + Fuente + ";Database=" + BaseDatos + ";User=" + Usuario + ";pwd=" + Clave;

            SqlConnection conexion = new SqlConnection(cadena);

            return conexion;
        }
    }
}