using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DT.Puertos
{
    public class DTPuerto
    {
        public int idPuerto { get; set; }
        public String nombrePuerto { get; set; }
        public String pais { get; set; }
        public String registroMercantil { get; set; }
        public Boolean estadoRegistroMercantil { get; set; }
    }
}
