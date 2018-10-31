using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Turismo.Bean
{
    public class Usuario
    {
        public int idusuario { get; set; }
        public string correo { get; set; }
        public string clave { get; set; }
        public string dni { get; set; }
        public string nombres_rep { get; set; }
        public string apellidos_rep { get; set; }
        public string razonSocial_emp { get; set; }
        public string nombreComercial_emp { get; set; }
        public string ruc_emp { get; set; }
        public string telefonos_emp { get; set; }
        public string paginaWeb_emp { get; set; }
        public string direccion_emp { get; set; }
        public HttpPostedFileBase logo { get; set; }
        public int idregion { get; set; }
        public int idtipo_usuario { get; set; }
        public int idestado { get; set; }
    }
}