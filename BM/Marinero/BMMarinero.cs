using DM.Marinero;
using DT.General;
using DT.Marinero;
using Soporte.Arboles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BM.Marinero
{
    public class BMMarinero
    {
    
        public DTResultadoOperacionList<DTMarinero> GenerarArchivoMarinero(DTMarinero _marinero)
        {
            DTResultadoOperacionList<DTMarinero> ResultList = new DTResultadoOperacionList<DTMarinero>();
            //Verificar si ya existe un marinero con esa cédula
            bool resultadoBusqueda = ArbolMarinero.buscarNodo(Convert.ToInt32(_marinero.cedula));
            if (resultadoBusqueda == true)
            {
                //La cédula ya existe. Retornar valor False
                ResultList.Resultado = false;
            }
            else
            {
                //
                //Insertar nuevo marinero en archivo
                //

                ResultList = new DMMarinero().GenerarArchivoMarinero(_marinero);

            }
            return ResultList;
        }

        public DTResultadoOperacionList<DTMarinero> ConsultarMarineros()
        {

            DTResultadoOperacionList<DTMarinero> ResultList = new DTResultadoOperacionList<DTMarinero>();
            ResultList = new DMMarinero().consultarMarineros();

            return ResultList;

        }

        public DTResultadoOperacionList<DTMarinero> consultarMarineros()
        {

            DTResultadoOperacionList<DTMarinero> ResultList = new DTResultadoOperacionList<DTMarinero>();
            ResultList = new DMMarinero().consultarMarineros();

            return ResultList;

        }

        public void cargarMarinerosEnArbol()
        {
            DMMarinero dMEscala = new DMMarinero();
            dMEscala.cargarMarinerosEnArbol();

        }
        public string imprimirArbol()
        {
            return new DMMarinero().imprimirArbol();
        }
    }
}
