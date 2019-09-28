using DT.Capitan;
using DT.General;
using Soporte.Arboles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Capitan
{
    public class DMCapitan : CapitanDAO
    {
        private static string NOMBRE_ARCHIVO = @" C:\BOTECITO\Capitan.txt";
        private static int LONGITUD_REGISTRO = 95;
        private static int LONGITUD_ID_CAPITAN = 5;
        private static int LONGITUD_NOMBRE_CAPITAN = 70;
        private static int LONGITUD_CEDULA = 20;


        public void CrearDirectorio()
        {
            string carpeta = NOMBRE_ARCHIVO.Substring(0, 12);
            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }
        }
        public DTResultadoOperacionList<DTCapitan> generarArchivoCapitan(DTCapitan _capitan)
        {
            CrearDirectorio();
            DTResultadoOperacionList<DTCapitan> ResultList = new DTResultadoOperacionList<DTCapitan>();
            List<DTCapitan> ResultadoReporte = new List<DTCapitan>();

            try
            {


                ResultList.Resultado = true;

                //GENERAR HILO PARA LA CREACIÓN DEL ARCHIVO PLANO (SI ES NECESARIO).


                if (!consultarCapitanPorCedula(_capitan.cedulaCapitan))
                {
                    int ultimoCapitan = consultarIdUltimoCapitan();
                    using (StreamWriter file = new StreamWriter(NOMBRE_ARCHIVO, true))   //se crea el archivo
                    {
                        //Averiguar el último consecutivo del archivo CAPITAN
                        
                        ultimoCapitan++;
                        _capitan.idCapitan = ultimoCapitan.ToString();
                        string cadena = parseCapitan2String(_capitan);

                        file.WriteLine(cadena);
                        file.Close();
                        cargarUnCapitanEnArbol(_capitan); //LLeva el nuevo Capitán al Abrol
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

            }

            return ResultList;
        }
        private int consultarIdUltimoCapitan()
        {
            int idUltimoCapitan = 0;
            try
            {
                using (StreamReader file = new StreamReader(NOMBRE_ARCHIVO, true))   //se crea el archivo
                {
                    String line;

                    line = file.ReadLine();
                    //Continue to read until you reach end of file
                    while (line != null)
                    {
                        DTCapitan _capitan = new DTCapitan();
                        //write the line to console 
                        Console.WriteLine(line);
                        _capitan = capitanRegistro2Objeto(line);
                        idUltimoCapitan = Convert.ToInt32(_capitan.idCapitan);


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
            return idUltimoCapitan;
        }
        private string parseCapitan2String(DTCapitan _capitan)
        {
            StringBuilder registro = new StringBuilder();
            registro.Append(completarCampo(_capitan.idCapitan, LONGITUD_ID_CAPITAN));
            registro.Append(completarCampo(_capitan.nombreCapitan, LONGITUD_NOMBRE_CAPITAN));
            registro.Append(completarCampo(_capitan.cedulaCapitan, LONGITUD_CEDULA));

            return registro.ToString();
        }
        private string completarCampo(string campo, int longitud)
        {
            if (campo.Length >= longitud)
                return campo.Substring(0, longitud);
            else
                return campo.PadRight(longitud, ' ');
        }
        public Boolean consultarCapitanPorCedula(string cedula)
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
                        DTCapitan _capitan = new DTCapitan();
                        //write the line to console 
                        Console.WriteLine(line);
                        _capitan = capitanRegistro2Objeto(line);


                        if (_capitan.cedulaCapitan == cedula)
                        {
                            resultado = true;  //Ya existe un capitán con esa cédula en el sistema
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
        private DTCapitan capitanRegistro2Objeto(String linea)
        {
            DTCapitan _capitan = new DTCapitan();
            _capitan.idCapitan = linea.Substring(0, LONGITUD_ID_CAPITAN).TrimEnd();
            _capitan.nombreCapitan = linea.Substring(5, LONGITUD_NOMBRE_CAPITAN).TrimEnd();
            _capitan.cedulaCapitan = linea.Substring(75, LONGITUD_CEDULA).TrimEnd();

            return _capitan;
        }

        public DTResultadoOperacionList<DTCapitan> consultarCapitanes()
        {
            DTResultadoOperacionList<DTCapitan> ResultList = new DTResultadoOperacionList<DTCapitan>();
            List<DTCapitan> ResultadoReporte = new List<DTCapitan>();

            try
            {
                using (StreamReader file = new StreamReader(NOMBRE_ARCHIVO, true))   //se crea el archivo
                {
                    String line;


                    line = file.ReadLine();
                    //Continue to read until you reach end of file
                    while (line != null)
                    {

                        DTCapitan _capitan = new DTCapitan();

                        //write the line to console 
                        Console.WriteLine(line);
                        _capitan = capitanRegistro2Objeto(line);



                        ResultadoReporte.Add(_capitan);

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
        public DTCapitan buscarCapitanPorCedula(string cedula)
        {
            DTCapitan _capitan = new DTCapitan();
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
                        _capitan = capitanRegistro2Objeto(line);
                        if (_capitan.cedulaCapitan == cedula)
                        {
                            break;
                        }
                        else
                        {
                            _capitan = null;
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
            return _capitan;
        }

        public void cargarUnCapitanEnArbol(DTCapitan _Capitan)
        {
            ArbolCapitan.Insertar(Convert.ToInt32(_Capitan.cedulaCapitan), Convert.ToInt32(_Capitan.idCapitan));
        }

        public void cargarCapitanesEnArbol()
        {
            ArbolCapitan.destruirArbol();
            DTResultadoOperacionList<DTCapitan> resultadoCapitanes = new DMCapitan().consultarCapitanes();
            if (resultadoCapitanes.Datos != null)
            {
                foreach (var capitan in resultadoCapitanes.Datos)
                {
                    ArbolCapitan.Insertar(Convert.ToInt32(capitan.cedulaCapitan), Convert.ToInt32(capitan.idCapitan));
                }
                
                
            }
        }
        public string imprimirArbol()
        {
            string resultado = ArbolCapitan.ImprimirPost();
                return resultado;
        }


    }
}
