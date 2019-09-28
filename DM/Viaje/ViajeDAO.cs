using DT.General;
using DT.Viaje;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Viaje
{
    interface ViajeDAO
    {
        DTResultadoOperacionList<DTViaje> generarArchivoViaje(DTViaje _viaje);
        DTViaje consultarViajePorId(int idViaje);
        DTResultadoOperacionList<DTViaje> consultarViajes();
    }
}
