using DM.Puerto;
using DM.Viaje;
using DT.Escala;
using DT.General;
using Soporte.Arboles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Escala
{
    public class DMEscala:EscalaDAO
    {
        private static string NOMBRE_ARCHIVO = @" C:\BOTECITO\Escala.txt";
        private static int LONGITUD_REGISTRO = 45;
        private static int LONGITUD_ID_ESCALA = 10;
        private static int LONGITUD_ID_VIAJE = 10;
        private static int LONGITUD_ID_PUERTO = 10;
        private static int LONGITUD_NUMERO_DIAS = 5;
        private static int LONGITUD_FECHA_ESCALA = 10;

        public void CrearDirectorio()
        {
            string carpeta = NOMBRE_ARCHIVO.Substring(0, 12);
            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }
        }
        public DTResultadoOperacionList<DTEscala> generarArchivoEscala(DTEscala _escala)
        {
            CrearDirectorio();
            DTResultadoOperacionList<DTEscala> ResultList = new DTResultadoOperacionList<DTEscala>();
            List<DTEscala> ResultadoReporte = new List<DTEscala>();

            try
            {

                ResultList.Resultado = true;
                //GENERAR HILO PARA LA CREACIÓN DEL ARCHIVO PLANO (SI ES NECESARIO).


                int idUltimaEscala = consultarIdUltimoEscala();
                _escala.idEscala = idUltimaEscala + 1;
                using (StreamWriter file = new StreamWriter(NOMBRE_ARCHIVO, true))   //se crea el archivo
                {
                    string cadena = parseViaje2String(_escala);

                    file.WriteLine(cadena);
                    file.Close();

                    cargarUnaEscalaEnArbol(_escala); //LLeva la nueva escala al Arbol
                }
                ResultList.Resultado = true;
               


            }
            catch (Exception ex)
            {
                ResultList.Resultado = false;

            }

            return ResultList;
        }
        private string parseViaje2String(DTEscala _escala)
        {
            StringBuilder registro = new StringBuilder();
            registro.Append(completarCampo(_escala.idEscala.ToString(), LONGITUD_ID_ESCALA));
            registro.Append(completarCampo(_escala.idViaje.ToString(), LONGITUD_ID_VIAJE));
            registro.Append(completarCampo(_escala.idPuerto.ToString(), LONGITUD_ID_PUERTO));
            registro.Append(completarCampo(_escala.numeroDias.ToString(), LONGITUD_NUMERO_DIAS));
            registro.Append(completarCampo(_escala.fechaEscala.ToString(), LONGITUD_FECHA_ESCALA));
            

            return registro.ToString();
        }
        private string completarCampo(string campo, int longitud)
        {
            if (campo.Length >= longitud)
                return campo.Substring(0, longitud);
            else
                return campo.PadRight(longitud, ' ');
        }
        private int consultarIdUltimoEscala()
        {
            int idUltimaEscala = 0;
            try
            {
                using (StreamReader file = new StreamReader(NOMBRE_ARCHIVO, true))   //se crea el archivo
                {
                    String line;

                    line = file.ReadLine();
                    //Continue to read until you reach end of file
                    while (line != null)
                    {
                        DTEscala _escala = new DTEscala();
                        //write the line to console 
                        Console.WriteLine(line);
                        _escala = viajeRegistro2Objeto(line);
                        idUltimaEscala = _escala.idEscala;


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
            return idUltimaEscala;
        }
        private DTEscala viajeRegistro2Objeto(string linea)
        {
            DTEscala _escala = new DTEscala();
            _escala.idEscala = Convert.ToInt32(linea.Substring(0, LONGITUD_ID_ESCALA).TrimEnd());
            _escala.idViaje = Convert.ToInt32(linea.Substring(10, LONGITUD_ID_VIAJE).TrimEnd());
            _escala.idPuerto = Convert.ToInt32(linea.Substring(20, LONGITUD_ID_PUERTO).TrimEnd());
            _escala.numeroDias = Convert.ToInt32(linea.Substring(30, LONGITUD_NUMERO_DIAS).TrimEnd());
            _escala.fechaEscala = linea.Substring(35, LONGITUD_FECHA_ESCALA).TrimEnd();
            

            return _escala;
        }

        



        public bool consultarViajePorNombre(string nombre)
        {
            throw new NotImplementedException();
        }
        public DTResultadoOperacionList<DTEscala> consultarEscalas()
        {
            DTResultadoOperacionList<DTEscala> ResultList = new DTResultadoOperacionList<DTEscala>();
            
            List<DTEscala> ResultadoReporte = new List<DTEscala>();
            DMPuerto _puerto = new DMPuerto();
            DTEscala _escala = new DTEscala();
            DMViaje _viaje = new DMViaje();
            //DMCapitan _capitan = new DMCapitan();
            try
            {
                using (StreamReader file = new StreamReader(NOMBRE_ARCHIVO, true))   //se crea el archivo
                {
                    String line;
                    line = file.ReadLine();
                    //Continue to read until you reach end of file
                    while (line != null)
                    {
                        //DTEscala _escala = new DTEscala();
                        //write the line to console 
                        Console.WriteLine(line);
                        _escala = viajeRegistro2Objeto(line);
                        //_viaje.PuertoOrigen = dmPuerto.consultarPuertoPorId(_viaje.idPuertoOrigen);
                        string puertoOrigen = _viaje.consultarViajePorId(_escala.idViaje).PuertoOrigen;
                        string puertoDestino = _viaje.consultarViajePorId(_escala.idViaje).PuertoDestino;
                        _escala.viaje = puertoOrigen + "-" + puertoDestino;
                        //_escala.PuertoDestino = _puerto.consultarPuertoPorId(_viaje.idPuertoDestino).nombrePuerto;
                        _escala.nombrePuerto = _puerto.consultarPuertoPorId(_escala.idPuerto).nombrePuerto;
                        //_escala.Capitan = _capitan.buscarCapitanPorCedula(_viaje.cedulaCapitan).nombreCapitan;
                        ResultadoReporte.Add(_escala);
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
        public void cargarUnaEscalaEnArbol(DTEscala _Escala)
        {
            ArbolEscala.Insertar(Convert.ToInt32(_Escala.idEscala), Convert.ToInt32(_Escala.idEscala));
        }

        public void cargarEscalasEnArbol()
        {
            ArbolEscala.destruirArbol();
            DTResultadoOperacionList<DTEscala> resultadoCapitanes = new DMEscala().consultarEscalas();
            if (resultadoCapitanes.Datos != null)
            {
                foreach (var escala in resultadoCapitanes.Datos)
                {
                    ArbolEscala.Insertar(Convert.ToInt32(escala.idEscala), Convert.ToInt32(escala.idEscala));
                }
            }
        }
        public string imprimirArbol()
        {
            string resultado = ArbolEscala.ImprimirPost();
            return resultado;
        }
    }
}
