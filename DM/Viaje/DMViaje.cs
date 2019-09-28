using DM.Barco;
using DM.Capitan;
using DM.Puerto;
using DT.General;
using DT.Viaje;
using Soporte.Arboles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Viaje
{
    public class DMViaje: ViajeDAO
    {
        private static string NOMBRE_ARCHIVO = @" C:\BOTECITO\Viaje.txt";
        private static int LONGITUD_REGISTRO = 60;
        private static int LONGITUD_ID_VIAJE = 10;
        private static int LONGITUD_ID_BARCO = 10;
        private static int LONGITUD_ID_PUERTO_ORIGEN = 10;
        private static int LONGITUD_ID_PUERTO_DESTINO = 10;
        private static int LONGITUD_CEDULA_CAPITAN = 20;

        public void CrearDirectorio()
        {
            string carpeta = NOMBRE_ARCHIVO.Substring(0, 12);
            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }
        }
        public DTResultadoOperacionList<DTViaje> generarArchivoViaje(DTViaje _viaje)
        {
            CrearDirectorio();
            DTResultadoOperacionList<DTViaje> ResultList = new DTResultadoOperacionList<DTViaje>();
            List<DTViaje> ResultadoReporte = new List<DTViaje>();

            try
            {

                ResultList.Resultado = true;
                //GENERAR HILO PARA LA CREACIÓN DEL ARCHIVO PLANO (SI ES NECESARIO).

                //if (!consultarBarcoPorNombre(_viaje.nombreBarco))
                //{
                    int idUltimoViaje = consultarIdUltimoViaje();
                    _viaje.idViaje= idUltimoViaje + 1;
                    using (StreamWriter file = new StreamWriter(NOMBRE_ARCHIVO, true))   //se crea el archivo
                    {
                        string cadena = parseViaje2String(_viaje);

                        file.WriteLine(cadena);
                        file.Close();
                    }
                    ResultList.Resultado = true;
                //}
                //else
                //{
                //    ResultList.Resultado = false;
                //}



            }
            catch (Exception ex)
            {
                ResultList.Resultado = false;
             
            }
            
            return ResultList;
        }
        private string parseViaje2String(DTViaje _barco)
        {
            StringBuilder registro = new StringBuilder();
            registro.Append(completarCampo(_barco.idViaje.ToString(), LONGITUD_ID_VIAJE));
            registro.Append(completarCampo(_barco.idBarco.ToString(), LONGITUD_ID_BARCO));
            registro.Append(completarCampo(_barco.idPuertoOrigen.ToString(), LONGITUD_ID_PUERTO_ORIGEN));
            registro.Append(completarCampo(_barco.idPuertoDestino.ToString(), LONGITUD_ID_PUERTO_DESTINO));
            registro.Append(completarCampo(_barco.cedulaCapitan.ToString(), LONGITUD_CEDULA_CAPITAN));
            
            return registro.ToString();
        }
        private string completarCampo(string campo, int longitud)
        {
            if (campo.Length >= longitud)
                return campo.Substring(0, longitud);
            else
                return campo.PadRight(longitud, ' ');
        }
        private int consultarIdUltimoViaje()
        {
            int idUltimoViaje = 0;
            try
            {
                using (StreamReader file = new StreamReader(NOMBRE_ARCHIVO, true))   //se crea el archivo
                {
                    String line;

                    line = file.ReadLine();
                    //Continue to read until you reach end of file
                    while (line != null)
                    {
                        DTViaje _viaje = new DTViaje();
                        //write the line to console 
                        Console.WriteLine(line);
                        _viaje = viajeRegistro2Objeto(line);
                        idUltimoViaje = _viaje.idViaje;


                        //Read the next line
                        line = file.ReadLine();
                    }
                    file.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            return idUltimoViaje;
        }
        private DTViaje viajeRegistro2Objeto(string linea)
        {
            DTViaje _viaje = new DTViaje();
            _viaje.idViaje = Convert.ToInt32(linea.Substring(0, LONGITUD_ID_VIAJE).TrimEnd());
            _viaje.idBarco = Convert.ToInt32(linea.Substring(10, LONGITUD_ID_BARCO).TrimEnd());
            _viaje.idPuertoOrigen = Convert.ToInt32(linea.Substring(20, LONGITUD_ID_PUERTO_ORIGEN).TrimEnd());
            _viaje.idPuertoDestino = Convert.ToInt32(linea.Substring(30, LONGITUD_ID_PUERTO_DESTINO).TrimEnd());
            _viaje.cedulaCapitan = linea.Substring(40, LONGITUD_CEDULA_CAPITAN).TrimEnd();
            
            return _viaje;
        }

        public DTResultadoOperacionList<DTViaje> consultarViajes()
        {
            DTResultadoOperacionList<DTViaje> ResultList = new DTResultadoOperacionList<DTViaje>();
            List<DTViaje> ResultadoReporte = new List<DTViaje>();
            DMPuerto _puerto = new DMPuerto();
            DMBarco _barco = new DMBarco();
            DMCapitan _capitan = new DMCapitan();
            try
            {
                using (StreamReader file = new StreamReader(NOMBRE_ARCHIVO, true))   //se crea el archivo
                {
                    String line;
                    line = file.ReadLine();
                    //Continue to read until you reach end of file
                    while (line != null)
                    {
                        DTViaje _viaje = new DTViaje();
                        //write the line to console 
                        Console.WriteLine(line);
                        _viaje = viajeRegistro2Objeto(line);
                        //_viaje.PuertoOrigen = dmPuerto.consultarPuertoPorId(_viaje.idPuertoOrigen);
                        _viaje.PuertoOrigen = _puerto.consultarPuertoPorId(_viaje.idPuertoOrigen).nombrePuerto;
                        _viaje.PuertoDestino = _puerto.consultarPuertoPorId(_viaje.idPuertoDestino).nombrePuerto;
                        _viaje.NombreBarco = _barco.consultarBarcoPorId(_viaje.idBarco).nombreBarco;
                        _viaje.Capitan = _capitan.buscarCapitanPorCedula(_viaje.cedulaCapitan).nombreCapitan;
                        ResultadoReporte.Add(_viaje);
                        //Read the next line
                        line = file.ReadLine();
                    }
                    file.Close();
                    ResultList.Resultado = true;
                }
                ResultList.Datos = ResultadoReporte;
            }
            catch (Exception ex)
            {
                ResultList.Resultado = false;
                Console.WriteLine("Exception: " + ex.Message);
            }
            return ResultList;
        }

       

        public DTViaje consultarViajePorId(int  idViaje)
        {
            DMPuerto _puerto = new DMPuerto();
            DTViaje _viaje = new DTViaje();
            try
            {
                using (StreamReader file = new StreamReader(NOMBRE_ARCHIVO, true))   //se crea el archivo
                {
                    String line;
                    line = file.ReadLine();
                    //Continue to read until you reach end of file
                    while (line != null)
                    {
                        _viaje = viajeRegistro2Objeto(line);
                        if (_viaje.idViaje == idViaje)
                        {
                            _viaje.PuertoOrigen = _puerto.consultarPuertoPorId(_viaje.idPuertoOrigen).nombrePuerto;
                            _viaje.PuertoDestino = _puerto.consultarPuertoPorId(_viaje.idPuertoDestino).nombrePuerto;
                            break;
                        }
                        else
                        {
                            _viaje = null;
                        }
                        //Read the next line
                        line = file.ReadLine();
                    }
                    file.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }

            return _viaje;
        }
        public Boolean verificarSiBarcoZarpo(DTViaje dtViaje)
        {
            Boolean respuesta = false;
            
            DTViaje _viaje = new DTViaje();
            DTResultadoOperacionList<DTDlleViaje> ReViajeLista = new DTResultadoOperacionList<DTDlleViaje>();
            List<DTDlleViaje> _listDlleViaje = new List<DTDlleViaje>();
            try
            {
                using (StreamReader file = new StreamReader(NOMBRE_ARCHIVO, true))   //se crea el archivo
                {
                    String line;
                    line = file.ReadLine();
                    //Continue to read until you reach end of file
                    while (line != null)
                    {
                        _viaje = viajeRegistro2Objeto(line);
                        if (_viaje.idBarco == dtViaje.idBarco)
                        {
                            respuesta = true; //El barco ya zarpó en otro viaje

                            break;
                        }
                        
                        //Read the next line
                        line = file.ReadLine();
                    }
                    file.Close();
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }

            return respuesta;
        }

        public void cargarViajesEnArbol()
        {
            ArbolViaje.destruirArbol();
            DTResultadoOperacionList<DTViaje> resultadoViajes = new DMViaje().consultarViajes();
            if (resultadoViajes.Datos != null)
            {
                foreach (var viaje in resultadoViajes.Datos)
                {
                    ArbolViaje.Insertar(Convert.ToInt32(viaje.idViaje), Convert.ToInt32(viaje.idViaje));
                }
            }
        }
        public string imprimirArbol()
        {
            string resultado = ArbolViaje.ImprimirPost();
            return resultado;
        }


    }
}
