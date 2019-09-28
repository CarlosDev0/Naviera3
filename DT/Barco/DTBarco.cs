using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DT.Barco
{
    public class DTBarco
    {
        public int idBarco { get; set; }
        public string tipoBarco { get; set; }
        public string nombreBarco { get; set; }
        public Boolean estado { get; set; }
        public Int16 añoConstruccion { get; set; }
        public int capacidadMaxima { get; set; }
        public int registroMercantil { get; set; }
    }
}
