using BM.Puerto;
using DT.General;
using DT.Puertos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Ejemplo1.Controllers.Puerto
{
    public class PuertoController : Controller
    {
        // GET: Puerto
        public ActionResult Puerto()
        {
            //Cargar todos los registros de puertos en el Arbol
            new BMPuerto().cargarPuertosEnArbol();
            return View("Puerto");
        }

        public string GenerarArchivoPuerto(DTPuerto _puerto)
        {
            DTResultadoOperacionList<DTPuerto> Resultado = new BMPuerto().generarArchivoPuerto(_puerto);

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;
            return serializer.Serialize(Resultado);
        }
        public string ConsultarPuertos()
        {
            DTResultadoOperacionList<DTPuerto> Resultado = new BMPuerto().ConsultarPuertos();

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;
            return serializer.Serialize(Resultado);

        }
        public string ConsultarUnPuerto(DTPuerto _puerto)
        {
            DTResultadoOperacionList<DTPuerto> Resultado = new BMPuerto().ConsultarUnPuerto(_puerto);

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;
            return serializer.Serialize(Resultado);

        }

        public string GuardarPuertoEditado(DTPuerto _puerto)
        {
            DTResultadoOperacionList<DTPuerto> Resultado = new BMPuerto().GuardarPuertoEditado(_puerto);

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;
            return serializer.Serialize(Resultado);
        }

        public ActionResult TreeView()
        {
            string resultadoArbol = new BMPuerto().imprimirArbol();
            ViewData["Arbol"] = resultadoArbol;
            return View("../TreeView");
        }
    }
}