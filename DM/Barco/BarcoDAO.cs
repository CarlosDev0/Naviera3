using DT.Barco;
using DT.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Barco
{
    public interface BarcoDAO
    {
        DTResultadoOperacionList<DTBarco> generarArchivoBarco(DTBarco _barco);
        Boolean consultarBarcoPorNombre(string nombre);
        DTResultadoOperacionList<DTBarco> consultarBarcos();
    }
}
