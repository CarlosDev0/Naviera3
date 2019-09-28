using DT.General;
using DT.Puertos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.Puerto;
using Soporte.Arboles;

namespace BM.Puerto
{
    public class BMPuerto
    {
        public DTResultadoOperacionList<DTPuerto> generarArchivoPuerto(DTPuerto _Puerto)
        {
            DTResultadoOperacionList<DTPuerto> ResultList = new DTResultadoOperacionList<DTPuerto>();
            //Verificar si ya existe un barco con ese registro Mercantíl
            bool resultadoBusqueda = ArbolPuerto.buscarNodo(Convert.ToInt32(_Puerto.registroMercantil));
            if (resultadoBusqueda == true)
            {
                //El registroMercantíl ya existe. Retornar valor False
                ResultList.Resultado = false;
            }
            else
            {
                //
                //Insertar nuevo puerto en archivo
                //
                ResultList = new DMPuerto().generarArchivoPuerto(_Puerto);
            }

            return ResultList;

        }

        public DTResultadoOperacionList<DTPuerto> ConsultarPuertos()
        {

            DTResultadoOperacionList<DTPuerto> ResultList = new DTResultadoOperacionList<DTPuerto>();
            ResultList = new DMPuerto().consultarPuertos();



            return ResultList;


        }
        public DTResultadoOperacionList<DTPuerto> ConsultarUnPuerto(DTPuerto _puerto)
        {

            DTResultadoOperacionList<DTPuerto> ResultList = new DTResultadoOperacionList<DTPuerto>();
            ResultList = new DMPuerto().consultarUnPuerto(_puerto);



            return ResultList;


        }
        
        public DTResultadoOperacionList<DTPuerto> GuardarPuertoEditado(DTPuerto _Viaje)
        {
            DTResultadoOperacionList<DTPuerto> ResultList = new DTResultadoOperacionList<DTPuerto>();
            ResultList = new DMPuerto().GuardarPuertoEditado(_Viaje);

            return ResultList;
        }

        public void cargarPuertosEnArbol()
        {
            DMPuerto dMPuerto = new DMPuerto();
            dMPuerto.cargarPuertosEnArbol();

        }
        public string imprimirArbol()
        {
            return new DMPuerto().imprimirArbol();
        }
    }
}
