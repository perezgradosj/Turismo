using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Turismo.Bean
{
    public class Itinerario
    {
        public int iditinerario { get; set; }
        public int dia { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public string alojamiento { get; set; }
        public string alimentacion { get; set; }
    }
}