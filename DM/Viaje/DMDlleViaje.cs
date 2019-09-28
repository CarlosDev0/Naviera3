using DM.Marinero;
using DT.General;
using DT.Viaje;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Viaje
{
    public class DMDlleViaje
    {

        private static string NOMBRE_ARCHIVO = @" C:\BOTECITO\DlleViaje.txt";
        private static int LONGITUD_REGISTRO = 30;
        private static int LONGITUD_ID_DETALLE_VIAJE = 10;
        private static int LONGITUD_ID_VIAJE = 10;
        private static int LONGITUD_CEDULA_MARINERO = 10;

        public void CrearDirectorio()
        {
            string carpeta = NOMBRE_ARCHIVO.Substring(0, 12);
            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }
        }
        public DTResultadoOperacionList<DTDlleViaje> generarArchivoDlleViaje(DTDlleViaje _dlleViaje)
        {
            CrearDirectorio();
            DTResultadoOperacionList<DTDlleViaje> ResultList = new DTResultadoOperacionList<DTDlleViaje>();
            List<DTViaje> ResultadoReporte = new List<DTViaje>();

            try
            {

                ResultList.Resultado = true;
                
                int idUltimoDlleViaje = consultarIdUltimoDlleViaje();
                _dlleViaje.idDetalleViaje = idUltimoDlleViaje + 1;
                using (StreamWriter file = new StreamWriter(NOMBRE_ARCHIVO, true))   //se crea el archivo
                {
                    string cadena = parseDlleViaje2String(_dlleViaje);

                    file.WriteLine(cadena);
                    file.Close();
                }
                List<DTDlleViaje> List = new List<DTDlleViaje>();
                
                ResultList.Datos = consultarDlleViaje(_dlleViaje.idViaje).Datos;
                ResultList.Resultado = true;
                



            }
            catch (Exception ex)
            {
                ResultList.Resultado = false;

            }

            return ResultList;
        }
        private string parseDlleViaje2String(DTDlleViaje _dlleViaje)
        {
            StringBuilder registro = new StringBuilder();
            registro.Append(completarCampo(_dlleViaje.idDetalleViaje.ToString(), LONGITUD_ID_DETALLE_VIAJE));
            registro.Append(completarCampo(_dlleViaje.idViaje.ToString(), LONGITUD_ID_VIAJE));
            registro.Append(completarCampo(_dlleViaje.cedulaMarinero.ToString(), LONGITUD_CEDULA_MARINERO));
            

            return registro.ToString();
        }
        private string completarCampo(string campo, int longitud)
        {
            if (campo.Length >= longitud)
                return campo.Substring(0, longitud);
            else
                return campo.PadRight(longitud, ' ');
        }
        private int consultarIdUltimoDlleViaje()
        {
            int idUltimoDlleViaje = 0;
            try
            {
                using (StreamReader file = new StreamReader(NOMBRE_ARCHIVO, true))   //se crea el archivo
                {
                    String line;

                    line = file.ReadLine();
                    //Continue to read until you reach end of file
                    while (line != null)
                    {
                        DTDlleViaje _dlleViaje = new DTDlleViaje();

                        _dlleViaje = viajeRegistro2Objeto(line);
                        idUltimoDlleViaje = _dlleViaje.idDetalleViaje;


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
            return idUltimoDlleViaje;
        }
        private DTDlleViaje viajeRegistro2Objeto(string linea)
        {
            DTDlleViaje _viaje = new DTDlleViaje();
            _viaje.idDetalleViaje = Convert.ToInt32(linea.Substring(0, LONGITUD_ID_VIAJE).TrimEnd());
            _viaje.idViaje = Convert.ToInt32(linea.Substring(10, LONGITUD_ID_VIAJE).TrimEnd());
            _viaje.cedulaMarinero = linea.Substring(20, LONGITUD_CEDULA_MARINERO).TrimEnd();
            

            return _viaje;
        }

        public DTResultadoOperacionList<DTDlleViaje> consultarDlleViaje(int idViaje)
        {
            DTResultadoOperacionList<DTDlleViaje> ResultList = new DTResultadoOperacionList<DTDlleViaje>();
            List<DTDlleViaje> ResultadoReporte = new List<DTDlleViaje>();
            DMMarinero _marinero = new DMMarinero();  
            
            try
            {
                using (StreamReader file = new StreamReader(NOMBRE_ARCHIVO, true))   //se crea el archivo
                {
                    String line;
                    line = file.ReadLine();
                    //Continue to read until you reach end of file
                    while (line != null)
                    {
                        DTDlleViaje _viaje = new DTDlleViaje();

                        _viaje = viajeRegistro2Objeto(line);
                        
                        _viaje.nombreMarinero = _marinero.consultarMarinero(_viaje.cedulaMarinero).nombreMarinero;
                        
                        if (_viaje.idViaje == idViaje)
                        {
                            ResultadoReporte.Add(_viaje);
                        }


                        
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



        public DTResultadoOperacionList<DTDlleViaje> ConsultarMarinerosViaje(DTDlleViaje dtDlleViaje)
        {
            DMMarinero _marinero = new DMMarinero();
            DTDlleViaje _dllviaje = new DTDlleViaje();
            DTResultadoOperacionList<DTDlleViaje> dllViajeLista = new DTResultadoOperacionList<DTDlleViaje>();
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
                        _dllviaje = viajeRegistro2Objeto(line);
                        if (_dllviaje.idViaje == dtDlleViaje.idViaje)
                        {
                            _dllviaje.nombreMarinero = _marinero.consultarMarinero(_dllviaje.cedulaMarinero).nombreMarinero.ToString();
                            //_dllviaje.PuertoDestino = _puerto.consultarPuertoPorId(_dllviaje.idPuertoDestino).nombrePuerto;
                            _listDlleViaje.Add(_dllviaje);
                            //break;
                        }
                        else
                        {
                            _dllviaje = null;
                        }
                        //Read the next line
                        line = file.ReadLine();
                    }
                    file.Close();
                }
                dllViajeLista.Datos=_listDlleViaje;
                dllViajeLista.Resultado = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }

            return dllViajeLista;
        }


    }
}
