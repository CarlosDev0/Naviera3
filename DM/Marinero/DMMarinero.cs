using DT.General;
using DT.Marinero;
using Soporte.Arboles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Marinero
{
    public class DMMarinero:MarineroDAO
    {
        private static string NOMBRE_ARCHIVO = @" C:\BOTECITO\Marinero.txt";
        private static int LONGITUD_REGISTRO = 120;
        private static int LONGITUD_ID_MARINERO = 10;
        private static int LONGITUD_NOMBRE_MARINERO = 70;
        private static int LONGITUD_CEDULA = 20;
        private static int LONGITUD_ESTADO_CIVIL = 20;

        public void CrearDirectorio()
        {
            string carpeta = NOMBRE_ARCHIVO.Substring(0, 12);
            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }
        }
        public DTResultadoOperacionList<DTMarinero> GenerarArchivoMarinero(DTMarinero _marinero)
        {
            CrearDirectorio();
            DTResultadoOperacionList<DTMarinero> ResultList = new DTResultadoOperacionList<DTMarinero>();
            List<DTMarinero> ResultadoReporte = new List<DTMarinero>();
            try
            {

                ResultList.Resultado = true;
                //GENERAR ARCHIVO PLANO:

                if (!consultarMarineroPorCedula(_marinero.cedula))
                {
                    int idUltimoMarinero = consultarIdUltimoMarinero();
                    _marinero.idMarinero = idUltimoMarinero + 1;
                    using (StreamWriter file = new StreamWriter(NOMBRE_ARCHIVO, true))   //se crea el archivo
                    {
                        string cadena = parseMarinero2String(_marinero);
                       
                        file.WriteLine(cadena);
                        file.Close();

                        cargarUnMarineroEnArbol(_marinero); //LLeva el nuevo Marinero al Arbol
                    }
                    ResultList.Resultado = true;
                }
                else {
                    ResultList.Resultado = false;
                }

            }
            catch (Exception ex)
            {
                ResultList.Resultado = false;
                
            }

            return ResultList;
        }
        public Boolean consultarMarineroPorCedula(string cedula)
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
                        DTMarinero _marinero = new DTMarinero();
                        //write the line to console 
                        Console.WriteLine(line);
                        _marinero = marineroRegistro2Objeto(line);
                        

                        if (_marinero.cedula == cedula) {
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
        public DTMarinero consultarMarinero(string cedula)
        {
            DTMarinero _marinero = new DTMarinero();
            Boolean resultado = true;
            DTMarinero marinero = new DTMarinero();
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
                        
                        _marinero = marineroRegistro2Objeto(line);


                        if (_marinero.cedula == cedula)
                        {
                            resultado = true;
                            break;
                        }
                        else
                        {
                            _marinero = null;
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
            return _marinero;

            
        }
         
        private string parseMarinero2String(DTMarinero _marinero)
        {
            StringBuilder registro = new StringBuilder();
            registro.Append(completarCampo(_marinero.idMarinero.ToString(), LONGITUD_ID_MARINERO));
            registro.Append(completarCampo(_marinero.nombreMarinero, LONGITUD_NOMBRE_MARINERO));
            registro.Append(completarCampo(_marinero.cedula,LONGITUD_CEDULA));
            registro.Append(completarCampo(_marinero.estadoCivil,LONGITUD_ESTADO_CIVIL));
            return registro.ToString();
        }

        private string completarCampo(string campo, int longitud)
        {
            if (campo.Length >= longitud)
                return campo.Substring(0, longitud);
            else 
                return campo.PadRight(longitud, ' ');
        }

        public DTResultadoOperacionList<DTMarinero> consultarMarineros()
        {
            

            DTResultadoOperacionList<DTMarinero> ResultList = new DTResultadoOperacionList<DTMarinero>();
            List<DTMarinero> ResultadoReporte = new List<DTMarinero>();
            try
            {
                using (StreamReader file = new StreamReader(NOMBRE_ARCHIVO, true))   //se crea el archivo
                {
                    String line;


                    line = file.ReadLine();
                    //Continue to read until you reach end of file
                    while (line != null)
                    {
                        DTMarinero _marinero = new DTMarinero();
                        //write the line to console 
                        Console.WriteLine(line);
                        _marinero = marineroRegistro2Objeto(line);

                        ResultadoReporte.Add(_marinero);
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

        private DTMarinero marineroRegistro2Objeto(String linea)
        {
            DTMarinero _marinero = new DTMarinero();
            _marinero.idMarinero = Convert.ToInt16(linea.Substring(0, LONGITUD_ID_MARINERO).TrimEnd());
            _marinero.nombreMarinero = linea.Substring(10, LONGITUD_NOMBRE_MARINERO).TrimEnd();
            _marinero.cedula = linea.Substring(80, LONGITUD_CEDULA).TrimEnd();
            _marinero.estadoCivil = linea.Substring(100, LONGITUD_ESTADO_CIVIL).TrimEnd();
            return _marinero;
        }

        private int consultarIdUltimoMarinero()
        {
            int idUltimoMarinero = 0;
            try
            {
                using (StreamReader file = new StreamReader(NOMBRE_ARCHIVO, true))   //se crea el archivo
                {
                    String line;

                    line = file.ReadLine();
                    //Continue to read until you reach end of file
                    while (line != null)
                    {
                        DTMarinero _marinero = new DTMarinero();
                        //write the line to console 
                        Console.WriteLine(line);
                        _marinero = marineroRegistro2Objeto(line);
                        idUltimoMarinero = _marinero.idMarinero;


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
            return idUltimoMarinero;
        }

        public void cargarUnMarineroEnArbol(DTMarinero _Marinero)
        {
            ArbolMarinero.Insertar(Convert.ToInt32(_Marinero.cedula), Convert.ToInt32(_Marinero.idMarinero));
        }

        public void cargarMarinerosEnArbol()
        {
            ArbolMarinero.destruirArbol();
            DTResultadoOperacionList<DTMarinero> resultadoMarineros = new DMMarinero().consultarMarineros();
            if (resultadoMarineros.Datos != null)
            {
                foreach (var marinero in resultadoMarineros.Datos)
                {
                    ArbolMarinero.Insertar(Convert.ToInt32(marinero.cedula), Convert.ToInt32(marinero.idMarinero));
                }
            }
        }
        public string imprimirArbol()
        {
            string resultado = ArbolMarinero.ImprimirPost();
            return resultado;
        }
    }
}
