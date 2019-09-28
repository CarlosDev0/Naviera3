using DT.General;
using DT.Marinero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Marinero
{
    public interface MarineroDAO
    {
        DTResultadoOperacionList<DTMarinero> GenerarArchivoMarinero(DTMarinero _marinero);
        Boolean consultarMarineroPorCedula(string cedula);
        DTResultadoOperacionList<DTMarinero> consultarMarineros();
    }
}
