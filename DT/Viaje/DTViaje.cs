using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DT.Viaje
{
    public class DTViaje
    {
        public int idViaje { get; set; }
        public int idBarco { get; set; }
        public string NombreBarco { get; set; }
        public int idPuertoOrigen { get; set; }
        public string PuertoOrigen { get; set; }
        public int idPuertoDestino { get; set; }
        public string PuertoDestino { get; set; }
        public string cedulaCapitan { get; set; }
        public string Capitan { get; set; }

    }
}
