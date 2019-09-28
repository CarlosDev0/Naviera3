using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DT.Barco;
using DT.General;
using Soporte.Arboles;

namespace DM.Barco
{
    public class DMBarco : BarcoDAO
    {

        private static string NOMBRE_ARCHIVO = @" C:\BOTECITO\Barco.txt";
        private static int LONGITUD_REGISTRO = 116;
        private static int LONGITUD_ID_BARCO = 10;
        private static int LONGITUD_TIPO_BARCO = 20;
        private static int LONGITUD_NOMBRE_BARCO = 50;
        private static int LONGITUD_ESTADO = 10;
        private static int LONGITUD_AÑO_CONSTRUCCION = 6;
        private static int LONGITUD_CAPACIDAD_MAXIMA = 10;
        private static int LONGITUD_REGISTRO_MERCANTIL = 10;

        public void CrearDirectorio()
        {
            string carpeta = NOMBRE_ARCHIVO.Substring(0, 12);
            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }
        }
        public DTResultadoOperacionList<DTBarco> generarArchivoBarco(DTBarco _barco)
        {
            CrearDirectorio();
            DTResultadoOperacionList<DTBarco> ResultList = new DTResultadoOperacionList<DTBarco>();
            List<DTBarco> ResultadoReporte = new List<DTBarco>();
            try
            {

                ResultList.Resultado = true;
                //GENERAR HILO PARA LA CREACIÓN DEL ARCHIVO PLANO (SI ES NECESARIO).

                if (!consultarBarcoPorNombre(_barco.nombreBarco))
                {

                    int idUltimoBarco = consultarIdUltimoBarco();
                    _barco.idBarco = idUltimoBarco + 1;
                    using (StreamWriter file = new StreamWriter(NOMBRE_ARCHIVO, true))   //se crea el archivo
                    {
                        string cadena = parseBarco2String(_barco);

                        file.WriteLine(cadena);
                        file.Close();
                        cargarUnBarcoEnArbol(_barco); //LLeva el nuevo Barco al Abrol
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
        private int consultarIdUltimoBarco()
        {
            int idUltimoBarco = 0;
            try
            {
                using (StreamReader file = new StreamReader(NOMBRE_ARCHIVO, true))   //se crea el archivo
                {
                    String line;

                    line = file.ReadLine();
                    //Continue to read until you reach end of file
                    while (line != null)
                    {
                        DTBarco _barco = new DTBarco();
                        //write the line to console 
                        Console.WriteLine(line);
                        _barco = barcoRegistro2Objeto(line);
                        idUltimoBarco = _barco.idBarco;


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
            return idUltimoBarco;
        }
        public bool consultarBarcoPorNombre(string nombre)
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
                        DTBarco _barco = new DTBarco();

                        _barco = barcoRegistro2Objeto(line);


                        if (_barco.nombreBarco == nombre)
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
        private string parseBarco2String(DTBarco _barco)
        {
            StringBuilder registro = new StringBuilder();
            registro.Append(completarCampo(_barco.idBarco.ToString(), LONGITUD_ID_BARCO));
            registro.Append(completarCampo(_barco.tipoBarco, LONGITUD_TIPO_BARCO));
            registro.Append(completarCampo(_barco.nombreBarco, LONGITUD_NOMBRE_BARCO));
            registro.Append(completarCampo(_barco.estado.ToString(), LONGITUD_ESTADO));
            registro.Append(completarCampo(_barco.añoConstruccion.ToString(), LONGITUD_AÑO_CONSTRUCCION));
            registro.Append(completarCampo(_barco.capacidadMaxima.ToString(), LONGITUD_CAPACIDAD_MAXIMA));
            registro.Append(completarCampo(_barco.registroMercantil.ToString(), LONGITUD_REGISTRO_MERCANTIL));
            return registro.ToString();
        }
        private DTBarco barcoRegistro2Objeto(string linea)
        {
            DTBarco _barco = new DTBarco();
            _barco.idBarco = Convert.ToInt32( linea.Substring(0, LONGITUD_ID_BARCO).TrimEnd());
            _barco.tipoBarco = linea.Substring(10, LONGITUD_TIPO_BARCO).TrimEnd();
            _barco.nombreBarco = linea.Substring(30, LONGITUD_NOMBRE_BARCO).TrimEnd();
            string estado = linea.Substring(80, LONGITUD_ESTADO).TrimEnd();
            if (estado == "true")
                _barco.estado = true;
            else
                _barco.estado = false;
            _barco.añoConstruccion = Convert.ToInt16(linea.Substring(90, LONGITUD_AÑO_CONSTRUCCION).TrimEnd());
            _barco.capacidadMaxima = Convert.ToInt32(linea.Substring(96, LONGITUD_CAPACIDAD_MAXIMA).TrimEnd());
            _barco.registroMercantil = Convert.ToInt32(linea.Substring(106, LONGITUD_REGISTRO_MERCANTIL).TrimEnd());
            return _barco;
        }
        private string completarCampo(string campo, int longitud)
        {
            if (campo.Length >= longitud)
                return campo.Substring(0, longitud);
            else
                return campo.PadRight(longitud, ' ');
        }

        public DTResultadoOperacionList<DTBarco> consultarBarcos()
        {


            DTResultadoOperacionList<DTBarco> ResultList = new DTResultadoOperacionList<DTBarco>();
            List<DTBarco> ResultadoReporte = new List<DTBarco>();
            try
            {
                using (StreamReader file = new StreamReader(NOMBRE_ARCHIVO, true))   //se crea el archivo
                {
                    String line;
                    line = file.ReadLine();
                    //Continue to read until you reach end of file
                    while (line != null)
                    {
                        DTBarco _barco= new DTBarco();
                        //write the line to console 
                        Console.WriteLine(line);
                        _barco = barcoRegistro2Objeto(line);

                        ResultadoReporte.Add(_barco);
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

        public DTBarco consultarBarcoPorId(int idBarco)
        {
            DTBarco _barco = new DTBarco();
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
                        _barco = barcoRegistro2Objeto(line);
                        if (_barco.idBarco == idBarco)
                        {
                            break;
                        }
                        else
                        {
                            _barco = null;
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
            return _barco;
        }

        public string imprimirArbol()
        {
            string resultado = ArbolBarco.ImprimirPost();

            return resultado;
        }

        public void cargarUnBarcoEnArbol(DTBarco _Barco)
        {
            ArbolBarco.Insertar(Convert.ToInt32(_Barco.registroMercantil), Convert.ToInt32(_Barco.idBarco));
        }

        public void cargarBarcosEnArbol()
        {
            ArbolBarco.destruirArbol();
            DTResultadoOperacionList<DTBarco> resultadoBarcos = new DMBarco().consultarBarcos();
            if (resultadoBarcos.Datos != null)
            {
                foreach (var barco in resultadoBarcos.Datos)
                {
                    ArbolBarco.Insertar(Convert.ToInt32(barco.registroMercantil), Convert.ToInt32(barco.idBarco));
                }


            }
        }

    }
}
