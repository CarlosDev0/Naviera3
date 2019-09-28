using DM.Viaje;
using DT.General;
using DT.Mensajes;
using DT.Viaje;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BM.Viaje
{
    public class BMViaje
    {
        public DTResultadoOperacionList<DTViaje> GenerarArchivoViaje(DTViaje _Viaje)
        {
            DTResultadoOperacionList<DTViaje> ResultList = new DTResultadoOperacionList<DTViaje>();
            Boolean _zarpo = new DMViaje().verificarSiBarcoZarpo(_Viaje);
            if (!_zarpo)
            {
                
                ResultList = new DMViaje().generarArchivoViaje(_Viaje);
            }
            else
            {
                DTMensaje mensaje = new DTMensaje();
                mensaje.Texto= "El barco ya zarpó. Por favor seleccione otro barco";
                ResultList.Mensaje = mensaje;
                ResultList.Resultado = false;
            }



            return ResultList;

        }

        public DTResultadoOperacionList<DTViaje> ConsultarViajes()
        {

            DTResultadoOperacionList<DTViaje> ResultList = new DTResultadoOperacionList<DTViaje>();
            ResultList = new DMViaje().consultarViajes();



            return ResultList;
            

        }
        public DTResultadoOperacionList<DTDlleViaje> AdicionarMarinero(DTDlleViaje _dtDlleViaje)
        {

            DTResultadoOperacionList<DTDlleViaje> ResultList = new DTResultadoOperacionList<DTDlleViaje>();
            ResultList = new DMDlleViaje().generarArchivoDlleViaje(_dtDlleViaje);



            return ResultList;
            

        }

        public DTResultadoOperacionList<DTDlleViaje> ConsultarMarinerosViaje(DTDlleViaje _dtDlleViaje)
        {
            DTResultadoOperacionList<DTDlleViaje> ResultList = new DTResultadoOperacionList<DTDlleViaje>();
            ResultList = new DMDlleViaje().ConsultarMarinerosViaje(_dtDlleViaje);

            return ResultList;

        }
        public void cargarViajesEnArbol()
        {
            DMViaje dMEscala = new DMViaje();
            dMEscala.cargarViajesEnArbol();

        }
        public string imprimirArbol()
        {
            return new DMViaje().imprimirArbol();
        }

    }

}
