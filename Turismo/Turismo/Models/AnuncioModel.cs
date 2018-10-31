using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Turismo.Bean;

namespace Turismo.Models
{
    public class AnuncioModel
    {
        Conexion con = new Conexion();
        SqlConnection conexion;
        SqlCommand comando;
        SqlDataReader reader;
        SqlTransaction transaction;

        public List<Destino> LstDestinos()
        {

            List<Destino> lista = new List<Destino>();
            Destino x = null;
            conexion = con.getConexion();
            try
            {
                conexion.Open();
                comando = new SqlCommand("Usp_LstDestinos", conexion);
                comando.CommandType = CommandType.StoredProcedure;

                reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        x = new Destino();
                        x.iddestino = reader.GetInt32(0);
                        x.destino = reader.GetString(1);
                        lista.Add(x);
                    }
                }
                conexion.Close();
            }
            catch (Exception)
            {

            }

            return lista;
        }

        public List<Alojamiento> LstAlojamientos()
        {

            List<Alojamiento> lista = new List<Alojamiento>();
            Alojamiento x = null;
            conexion = con.getConexion();
            try
            {
                conexion.Open();
                comando = new SqlCommand("Usp_LstAlojamientos", conexion);
                comando.CommandType = CommandType.StoredProcedure;

                reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        x = new Alojamiento();
                        x.id = reader.GetInt32(0);
                        x.alojamiento = reader.GetString(1);
                        lista.Add(x);
                    }
                }
                conexion.Close();
            }
            catch (Exception)
            {

            }

            return lista;
        }

        public string Create(Paquete x, string[] itiDia, string[] itiTitulo, string[] itiDescripcion, string[] itiAlojamiento, string[] itiAlimentacion)
        {

            string mensaje = null;

            conexion = con.getConexion();

            try
            {                
                conexion.Open();

                transaction = conexion.BeginTransaction("ExampleTransaction");

                //comando.Transaction = transaction;

                //1. Insertamos el objeto Paquete
                comando = new SqlCommand("Usp_InsertAnuncio", conexion);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@titulo", x.titulo);
                comando.Parameters.AddWithValue("@destino", x.destino);
                comando.Parameters.AddWithValue("@fecha_public_desde", x.fecha_public_desde);
                comando.Parameters.AddWithValue("@fecha_public_hasta", x.fecha_public_hasta);
                comando.Parameters.AddWithValue("@dias", x.dias);
                comando.Parameters.AddWithValue("@noches", x.noches);
                comando.Parameters.AddWithValue("@disponibles", x.disponibles);
                comando.Parameters.AddWithValue("@minPasajeros", x.minPasajeros);
                comando.Parameters.AddWithValue("@horasAnticipado", x.horasAnticipado);
                comando.Parameters.AddWithValue("@categoria", x.categoria);
                comando.Parameters.AddWithValue("@seguridad", x.seguridad);
                comando.Parameters.AddWithValue("@precioHabDobleTripe", x.precioHabDobleTripe);
                comando.Parameters.AddWithValue("@precioHabSimple", x.precioHabSimple);
                comando.Parameters.AddWithValue("@foto1", x.foto1.FileName);
                comando.Parameters.AddWithValue("@foto2", x.foto2.FileName);
                comando.Parameters.AddWithValue("@foto3", x.foto3.FileName);
                comando.Parameters.AddWithValue("@foto4", x.foto4.FileName);
                comando.Parameters.AddWithValue("@foto5", x.foto5.FileName);
                comando.Parameters.AddWithValue("@foto6", x.foto6.FileName);
                comando.Parameters.AddWithValue("@idusuario", x.idusuario);
                comando.Transaction = transaction;

                int resultado = comando.ExecuteNonQuery();
                mensaje = resultado == 0 ? "Error al Insertar" : "Anuncio insertado correctamente";

                //2. Recupero el último ID insertado
                comando = new SqlCommand("SELECT @@IDENTITY;", conexion);
                comando.Transaction = transaction;

                var idMax = Convert.ToInt32(comando.ExecuteScalar());

                //3. Insertamos los itineraios
                int cantItinerarios = itiDia.Length;
                int insert = 0;
                for (int i = 0; i < cantItinerarios; i++) {
                    comando = new SqlCommand("Usp_InsertItinerarios", conexion);
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@dia", itiDia[i]);
                    comando.Parameters.AddWithValue("@titulo", itiTitulo[i]);
                    comando.Parameters.AddWithValue("@descripcion", itiDescripcion[i]);
                    comando.Parameters.AddWithValue("@idpaquete", idMax);
                    comando.Parameters.AddWithValue("@idalojamiento", itiAlojamiento[i]);
                    comando.Parameters.AddWithValue("@alimentacion", itiAlimentacion[i]);
                    comando.Transaction = transaction;

                    insert = comando.ExecuteNonQuery();
                    mensaje = insert == 0 ? "Error al Insertar" : "Itinerario insertado correctamente";
                }

                transaction.Commit();
                mensaje = "Anuncio creado satisfactoriamente.";
                conexion.Close();
            }
            catch (Exception e)
            {
                transaction.Rollback();
            }

            return mensaje;
        }

        public List<Paquete> LstAnuncios()
        {

            List<Paquete> lista = new List<Paquete>();
            Paquete x = null;
            conexion = con.getConexion();
            try
            {
                conexion.Open();
                comando = new SqlCommand("Usp_LstAnuncios", conexion);
                comando.CommandType = CommandType.StoredProcedure;

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