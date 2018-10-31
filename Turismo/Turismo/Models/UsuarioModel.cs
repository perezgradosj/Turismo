using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Turismo.Bean;

namespace Turismo.Models
{
    public class UsuarioModel
    {
        Conexion con = new Conexion();
        SqlConnection conexion;
        SqlCommand comando;
        SqlDataReader reader;

        public string Create(Usuario usuario)
        {
            string mensaje = null;
            conexion = con.getConexion();
            try
            {
                conexion.Open();

                comando = new SqlCommand("Usp_InsertUser", conexion);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@correo", usuario.correo);
                comando.Parameters.AddWithValue("@clave", usuario.clave);
                comando.Parameters.AddWithValue("@dni", usuario.dni == null ? "": usuario.dni);
                comando.Parameters.AddWithValue("@nombres_rep", usuario.nombres_rep == null ? "" : usuario.nombres_rep);
                comando.Parameters.AddWithValue("@apellidos_rep", usuario.apellidos_rep == null ? "" : usuario.apellidos_rep);
                comando.Parameters.AddWithValue("@razonSocial_emp", usuario.razonSocial_emp == null ? "" : usuario.razonSocial_emp);
                comando.Parameters.AddWithValue("@nombreComercial_emp", usuario.nombreComercial_emp == null ? "" : usuario.nombreComercial_emp);
                comando.Parameters.AddWithValue("@ruc_emp", usuario.ruc_emp == null ? "" : usuario.ruc_emp);
                comando.Parameters.AddWithValue("@telefonos_emp", usuario.telefonos_emp == null ? "" : usuario.telefonos_emp);
                comando.Parameters.AddWithValue("@paginaWeb_emp", usuario.paginaWeb_emp == null ? "" : usuario.paginaWeb_emp);
                comando.Parameters.AddWithValue("@direccion_emp", usuario.direccion_emp == null ? "" : usuario.direccion_emp);
                comando.Parameters.AddWithValue("@logo", usuario.logo == null ? "" : usuario.logo.FileName);
                comando.Parameters.AddWithValue("@idregion", usuario.idregion);
                comando.Parameters.AddWithValue("@idtipo_usuario", usuario.idtipo_usuario);
                comando.Parameters.AddWithValue("@idestado", 1);

                int resultado = comando.ExecuteNonQuery();
                mensaje = resultado == 0 ? "Error al Insertar" : "Usuario creado exitosamente";

                conexion.Close();
            }
            catch (Exception ex)
            {
                mensaje = ex.Message + " | " + ex.InnerException;
            }

            return mensaje;
        }

        public Usuario Autentication(Usuario usuario) {
            Usuario x = null;
            conexion = con.getConexion();
            try
            {
                conexion.Open();
                comando = new SqlCommand("Usp_AutenticationUser", conexion);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@correo", usuario.correo);
                comando.Parameters.AddWithValue("@clave", usuario.clave);

                reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        x = new Usuario();
                        x.idusuario = reader.GetInt32(0);
                        x.correo = reader.GetString(1);
                        x.clave = reader.GetString(2);
                        x.nombres_rep = reader.GetString(4);
                        x.apellidos_rep = reader.GetString(5);
                        x.idtipo_usuario = reader.GetInt32(14);
                        x.idestado = reader.GetInt32(15);
                    }
                }
                conexion.Close();
            }
            catch (Exception)
            {

            }

            return x;
        }

        public bool FindUserByCorreo(string correo) {
            bool response = false;
            conexion = con.getConexion();
            try
            {
                conexion.Open();
                comando = new SqlCommand("Usp_FindUserByCorreo", conexion);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@correo", correo);

                reader = comando.ExecuteReader();

                if (reader.HasRows)
                    response = true;

                conexion.Close();
            }
            catch (Exception ex) {
                response = false;
            }
            return response;
        }
        
    }
}