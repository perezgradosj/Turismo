using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Turismo.Bean;

namespace Turismo.Models
{
    public class PaqueteModel
    {
        Conexion con = new Conexion();
        SqlConnection conexion;
        SqlCommand comando;
        SqlDataReader reader;

        public Paquete FindById(int idpaquete)
        {
            Paquete x = null;
            Itinerario y = null;
            conexion = con.getConexion();
            try
            {
                conexion.Open();
                comando = new SqlCommand("Usp_FindPaqueteById", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idpaquete", idpaquete);

                reader = comando.ExecuteReader();

                x = new Paquete();
                x.itinerarios = new List<Itinerario>();

                if (reader.HasRows)
                { 
                    while (reader.Read())
                    {
                        y = new Itinerario();
                        x.titulo = reader.GetString(0);
                        x.destino = reader.GetString(1);
                        x.dias = reader.GetInt32(2);
                        x.noches = reader.GetInt32(3);
                        x.minPasajeros = reader.GetInt32(4);
                        x.horasAnticipado = reader.GetInt32(5);
                        x.precioHabDobleTripe = reader.GetInt32(6);
                        x.foto1Name = reader.GetString(7);
                        x.foto2Name = reader.GetString(8);
                        x.foto3Name = reader.GetString(9);
                        x.foto4Name = reader.GetString(10);
                        x.foto5Name = reader.GetString(11);
                        x.foto6Name = reader.GetString(12);
                        y.dia = reader.GetInt32(13);
                        y.titulo = reader.GetString(14);
                        y.descripcion = reader.GetString(15);
                        y.alojamiento = reader.GetString(16);
                        y.alimentacion = reader.GetString(17);
                        x.itinerarios.Add(y);
                    }
                }
                conexion.Close();
            }
            catch (Exception ex)
            {

            }

            return x;
        }

        public List<Paquete> LstByRegion(string region)
        {

            List<Paquete> lista = new List<Paquete>();
            Paquete x = null;
            conexion = con.getConexion();
            try
            {
                conexion.Open();
                comando = new SqlCommand("Usp_LstAnunciosByRegion", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@region", region);

                reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        x = new Paquete();
                        x.id = reader.GetInt32(0);
                        x.titulo = reader.GetString(1);
                        x.destino = reader.GetString(2);
                        x.fecha_public_desde = reader.GetDateTime(3).ToShortDateString();
                        x.fecha_public_hasta = reader.GetDateTime(4).ToShortDateString();
                        x.dias = reader.GetInt32(5);
                        x.noches = reader.GetInt32(6);
                        x.precioHabDobleTripe = reader.GetInt32(12);
                        x.idestado = reader.GetInt32(21);
                        x.foto1Name = reader.GetString(14);
                        lista.Add(x);
                    }
                }
                conexion.Close();
            }
            catch (Exception ex)
            {

            }

            return lista;
        }
    }
}