using DT.General;
using DT.Puertos;
using Soporte.Arboles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Puerto
{
    public class DMPuerto: PuertoDAO
    {
        private static string NOMBRE_ARCHIVO = @" C:\BOTECITO\Puerto.txt";
        private static int LONGITUD_REGISTRO = 110;
        private static int LONGITUD_ID_PUERTO = 10;
        private static int LONGITUD_NOMBRE_PUERTO = 50;
        private static int LONGITUD_PAIS = 20;
        private static int LONGITUD_REGISTRO_MERCANTIL = 20;
        private static int LONGITUD_ESTADO_REGISTRO_MERCANTIL = 10;

        public void CrearDirectorio()
        {
            string carpeta = NOMBRE_ARCHIVO.Substring(0, 12);
            if (!Directory.Exists(carpeta)) {
                Directory.CreateDirectory(carpeta);
            }


        }


        public DTResultadoOperacionList<DTPuerto> generarArchivoPuerto(DTPuerto _puerto)
        {
            CrearDirectorio();
            DTResultadoOperacionList<DTPuerto> ResultList = new DTResultadoOperacionList<DTPuerto>();
            List<DTPuerto> ResultadoReporte = new List<DTPuerto>();
            try
            {

                ResultList.Resultado = true;
                //GENERAR HILO PARA LA CREACIÓN DEL ARCHIVO PLANO (SI ES NECESARIO).

                if (!consultarPuertoPorNombre(_puerto.nombrePuerto))
                {
                    int idUltimoPuerto = consultarIdUltimoPuerto();
                    _puerto.idPuerto = idUltimoPuerto+1;
                    using (StreamWriter file = new StreamWriter(NOMBRE_ARCHIVO, true))   //se crea el archivo
                    {
                        
                        string cadena = parsePuerto2String(_puerto);

                        file.WriteLine(cadena);
                        file.Close();
                        cargarUnPuertoEnArbol(_puerto); //LLeva el nuevo puerto al Arbol
                    }
                    ResultList.Resultado = true;
                }
                else
                {
                    ResultList.Resultado = false;
                }

            }
            catch (Exception ex)
            {
                ResultList.Resultado = false;
                //ResultList.Mensaje = DTMensaje.ObtenerObjetoMensaje(DTCodigoMensajes.MENSAJE002);
                //GestorLog.RegistrarLogExcepcion(ex);
            }

            return ResultList;
        }
        private int consultarIdUltimoPuerto()
        {
            int ultimoIdPuerto = 0;
            try
            {
                using (StreamReader file = new StreamReader(NOMBRE_ARCHIVO, true))   //se crea el archivo
                {
                    String line;
                    
                    line = file.ReadLine();
                    //Continue to read until you reach end of file
                    while (line != null)
                    {
                        DTPuerto _puerto = new DTPuerto();
                        //write the line to console 
                        Console.WriteLine(line);
                        _puerto = puertoRegistro2Objeto(line);
                        ultimoIdPuerto = _puerto.idPuerto;


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
            return ultimoIdPuerto;
        }

        public Boolean consultarPuertoPorNombre(string nombre)
        {
            Boolean resultado = new Boolean();
            resultado = false;
            try
            {
                using (StreamReader file = new StreamReader(NOMBRE_ARCHIVO, true))   //se crea el archivo
                {
                    String line;
                    line = file.ReadLine();
                    //Continue to read until you reach end of file
                    while (line != null)
                    {
                        DTPuerto _puerto = new DTPuerto();
                        //write the line to console 
                        Console.WriteLine(line);
                        _puerto = puertoRegistro2Objeto(line);


                        if (_puerto.nombrePuerto == nombre)
                        {
                            resultado = true;
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

            return resultado;
        }
        
        private string parsePuerto2String(DTPuerto _puerto)
        {
            StringBuilder registro = new StringBuilder();
            registro.Append(completarCampo(_puerto.idPuerto.ToString(), LONGITUD_ID_PUERTO));
            registro.Append(completarCampo(_puerto.nombrePuerto, LONGITUD_NOMBRE_PUERTO));
            registro.Append(completarCampo(_puerto.pais, LONGITUD_PAIS));
            registro.Append(completarCampo(_puerto.registroMercantil.ToString(), LONGITUD_REGISTRO_MERCANTIL));
            registro.Append(completarCampo(_puerto.estadoRegistroMercantil.ToString(), LONGITUD_ESTADO_REGISTRO_MERCANTIL));
            return registro.ToString();
        }

        private string completarCampo(string campo, int longitud)
        {
            if (campo.Length >= longitud)
                return campo.Substring(0, longitud);
            else
                return campo.PadRight(longitud, ' ');
        }

        public DTResultadoOperacionList<DTPuerto> consultarPuertos()
        {


            DTResultadoOperacionList<DTPuerto> ResultList = new DTResultadoOperacionList<DTPuerto>();
            List<DTPuerto> ResultadoReporte = new List<DTPuerto>();
            try
            {
                using (StreamReader file = new StreamReader(NOMBRE_ARCHIVO, true))   //se crea el archivo
                {
                    String line;


                    line = file.ReadLine();
                    //Continue to read until you reach end of file
                    while (line != null)
                    {
                        DTPuerto _puerto = new DTPuerto();
                        //write the line to console 
                        Console.WriteLine(line);
                        _puerto = puertoRegistro2Objeto(line);

                        ResultadoReporte.Add(_puerto);
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

        private DTPuerto puertoRegistro2Objeto(String linea)
        {
             DTPuerto _puerto = new DTPuerto();
            _puerto.idPuerto = Convert.ToInt32(linea.Substring(0, LONGITUD_ID_PUERTO).TrimEnd());
            _puerto.nombrePuerto = linea.Substring(10, LONGITUD_NOMBRE_PUERTO).TrimEnd();
            _puerto.pais = linea.Substring(60, LONGITUD_PAIS).TrimEnd();
            _puerto.registroMercantil = linea.Substring(80, LONGITUD_REGISTRO_MERCANTIL).TrimEnd();
            string estado = linea.Substring(100, LONGITUD_ESTADO_REGISTRO_MERCANTIL).TrimEnd();
            if (estado == "True")
                _puerto.estadoRegistroMercantil = true;
            else
                _puerto.estadoRegistroMercantil = false;
            return _puerto;
        }
        public DTPuerto consultarPuertoPorId(int idPuerto)
        {
            DTPuerto _puerto = new DTPuerto();
            try
            {
                using (StreamReader file = new StreamReader(NOMBRE_ARCHIVO, true))   //se crea el archivo
                {
                    String line;
                    line = file.ReadLine();
                    //Continue to read until you reach end of file
                    while (line != null)
                    {
                        //write the line to console 
                        Console.WriteLine(line);
                        _puerto = puertoRegistro2Objeto(line);
                        if (_puerto.idPuerto == idPuerto)
                        {
                            break;
                        }
                        else {
                            _puerto = null;
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
            return _puerto;
        }
        public DTResultadoOperacionList<DTPuerto> consultarUnPuerto(DTPuerto _puertoBuscado)
        {

            DTResultadoOperacionList<DTPuerto> ResultList = new DTResultadoOperacionList<DTPuerto>();
            List<DTPuerto> ResultadoReporte = new List<DTPuerto>();
            try
            {
                using (StreamReader file = new StreamReader(NOMBRE_ARCHIVO, true))   //se crea el archivo
                {
                    String line;


                    line = file.ReadLine();
                    //Continue to read until you reach end of file
                    while (line != null)
                    {
                        DTPuerto _puerto = new DTPuerto();
                        //write the line to console 
                        Console.WriteLine(line);
                        _puerto = puertoRegistro2Objeto(line);
                        if (_puerto.idPuerto== _puertoBuscado.idPuerto) { 
                            ResultadoReporte.Add(_puerto);
                            break;
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
        
        public DTResultadoOperacionList<DTPuerto> GuardarPuertoEditado(DTPuerto _puertoEditado)
        {
            DTResultadoOperacionList<DTPuerto> ResultList = new DTResultadoOperacionList<DTPuerto>();
            List<DTPuerto> ResultadoReporte = new List<DTPuerto>();
            DTPuerto _puerto = new DTPuerto();
            String line;
            String cadena;
            using (var fs = File.Open(NOMBRE_ARCHIVO, FileMode.Open, FileAccess.ReadWrite))
            {
                var destinationReader = new StreamReader(fs);
                var writer = new StreamWriter(fs);
                while ((line = destinationReader.ReadLine()) != null)
                {
                    _puerto = puertoRegistro2Objeto(line);
                    if (_puerto.idPuerto == _puertoEditado.idPuerto)
                    {
                        cadena = parsePuerto2String(_puertoEditado);
                        writer.WriteLine(cadena);
                        ResultList.Resultado = true;
                        break;
                    }
                  


                }
            }

            

            return ResultList;
        }

        public void cargarUnPuertoEnArbol(DTPuerto _Puerto)
        {
            ArbolEscala.Insertar(Convert.ToInt32(_Puerto.registroMercantil), Convert.ToInt32(_Puerto.idPuerto));
        }

        public void cargarPuertosEnArbol()
        {
            ArbolPuerto.destruirArbol();
            DTResultadoOperacionList<DTPuerto> resultadoPuertos = new DMPuerto().consultarPuertos();
            if (resultadoPuertos.Datos != null)
            {
                foreach (var puerto in resultadoPuertos.Datos)
                {
                    ArbolPuerto.Insertar(Convert.ToInt32(puerto.registroMercantil), Convert.ToInt32(puerto.idPuerto));
                }
            }
        }

        public string imprimirArbol()
        {
            string resultado = ArbolPuerto.ImprimirPost();
            return resultado;
        }

    }
}
