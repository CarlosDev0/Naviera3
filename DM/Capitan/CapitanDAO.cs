using DT.Capitan;
using DT.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Capitan
{
    public interface CapitanDAO
    {
        DTResultadoOperacionList<DTCapitan> generarArchivoCapitan(DTCapitan _marinero);
        Boolean consultarCapitanPorCedula(string cedula);
        DTResultadoOperacionList<DTCapitan> consultarCapitanes();
    }
}
