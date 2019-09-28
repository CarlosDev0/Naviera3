using DT.General;
using DT.Puertos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Puerto
{
    public interface PuertoDAO
    {
        DTResultadoOperacionList<DTPuerto> generarArchivoPuerto(DTPuerto _puerto);
        Boolean consultarPuertoPorNombre(string nombre);
        DTResultadoOperacionList<DTPuerto> consultarPuertos();
    }
}
