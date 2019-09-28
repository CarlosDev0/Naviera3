using DM.Barco;
using DT.Barco;
using DT.General;
using Soporte.Arboles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace BM.Barco
{
    public class BMBarco {

        public DTResultadoOperacionList<DTBarco> GenerarArchivoBarco(DTBarco _barco) {
            DTResultadoOperacionList<DTBarco> ResultList = new DTResultadoOperacionList<DTBarco>();
            //Verificar si ya existe un barco con ese registro Mercantíl
            bool resultadoBusqueda = ArbolBarco.buscarNodo(Convert.ToInt32(_barco.registroMercantil));
            if (resultadoBusqueda == true)
            {
                //El registroMercantíl ya existe. Retornar valor False
                ResultList.Resultado = false;
            }
            else
            {
                //
                //Insertar nuevo capitán en archivo
                //
                ResultList = new DMBarco().generarArchivoBarco(_barco);
            }
            return ResultList;

        }

        public DTResultadoOperacionList<DTBarco> ConsultarBarcos() {
            DTResultadoOperacionList<DTBarco> ResultList = new DTResultadoOperacionList<DTBarco>();
            ResultList = new DMBarco().consultarBarcos();



            return ResultList;

            
        }
        public string imprimirArbol()
        {
            return new DMBarco().imprimirArbol();
        }
        public void cargarBarcosEnArbol()
        {
            new DMBarco().cargarBarcosEnArbol();
        }
    }

}
