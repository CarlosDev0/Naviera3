using DM.Capitan;
using DT.Capitan;
using DT.General;
using Soporte.Arboles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BM.Capitan
{
    public class BMCapitan
    {
        public DTResultadoOperacionList<DTCapitan> GenerarArchivoCapitan(DTCapitan _Capitan)
        {
            DTResultadoOperacionList<DTCapitan> informeResultado= new DTResultadoOperacionList<DTCapitan>();
            //Verificar si ya existe un capitán con esa cédula
            bool resultadoBusqueda = ArbolCapitan.buscarNodo(Convert.ToInt32(_Capitan.cedulaCapitan));
            if (resultadoBusqueda == true) {
                //La cédula ya existe. Retornar valor False
                informeResultado.Resultado = false;
            }
            else {
                //
                //Insertar nuevo capitán en archivo
                //

                DTCapitan _dtCapitan = new DTCapitan();
                informeResultado = new DMCapitan().generarArchivoCapitan(_Capitan);
            }
            

            return informeResultado;

        }

        public DTResultadoOperacionList<DTCapitan> ConsultarCapitan()
        {

            DTResultadoOperacionList<DTCapitan> ResultList = new DTResultadoOperacionList<DTCapitan>();
            ResultList = new DMCapitan().consultarCapitanes();



            return ResultList;


        }
        public void cargarCapitanesEnArbol()
        {
            DMCapitan dMCapitan = new DMCapitan();
            dMCapitan.cargarCapitanesEnArbol();
            
          


        }
        public string imprimirArbol()
        {
            return new DMCapitan().imprimirArbol();
        }
    }
}
