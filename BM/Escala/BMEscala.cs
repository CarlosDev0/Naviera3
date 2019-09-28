using DM.Escala;
using DT.Escala;
using DT.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BM.Escala
{
    public class BMEscala
    {
        public DTResultadoOperacionList<DTEscala> generarArchivoEscala(DTEscala _escala)
        {
            DTResultadoOperacionList<DTEscala> ResultList = new DTResultadoOperacionList<DTEscala>();
            ResultList = new DMEscala().generarArchivoEscala(_escala);

            return ResultList;
        }

        public DTResultadoOperacionList<DTEscala> consultarEscalas()
        {

            DTResultadoOperacionList<DTEscala> ResultList = new DTResultadoOperacionList<DTEscala>();
            ResultList = new DMEscala().consultarEscalas();

            return ResultList;

        }

        public void cargarEscalasEnArbol()
        {
            DMEscala dMEscala = new DMEscala();
            dMEscala.cargarEscalasEnArbol();

        }
        public string imprimirArbol()
        {
            return new DMEscala().imprimirArbol();
        }

    }
}
