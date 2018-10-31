using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Turismo.Bean
{
    public class Paquete
    {
        public int id { get; set; }

        public string fecha_public_desde { get; set; }
        public string fecha_public_hasta { get; set; }

        public int dias { get; set; }
        public int noches { get; set; }

        public int disponibles { get; set; }
        public int minPasajeros { get; set; }
        public int horasAnticipado { get; set; }

        public string titulo { get; set; }
        public string destino { get; set; }

        public string categoria { get; set; }
        public string seguridad { get; set; }

        public int precioHabDobleTripe { get; set; }
        public int precioHabSimple { get; set; }

        public HttpPostedFileBase foto1 { get; set; }
        public string foto1Name { get; set; }
        public HttpPostedFileBase foto2 { get; set; }
        public string foto2Name { get; set; }
        public HttpPostedFileBase foto3 { get; set; }
        public string foto3Name { get; set; }
        public HttpPostedFileBase foto4 { get; set; }
        public string foto4Name { get; set; }
        public HttpPostedFileBase foto5 { get; set; }
        public string foto5Name { get; set; }
        public HttpPostedFileBase foto6 { get; set; }
        public string foto6Name { get; set; }

        public int idusuario { get; set; }
        public int idestado { get; set; }

        public List<Itinerario> itinerarios { get; set; }
    }
    
}