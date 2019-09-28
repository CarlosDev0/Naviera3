using BM.Barco;
using DT.Barco;
using DT.General;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Ejemplo1.Barco
{
    public class BarcoController : Controller
    {
        // GET: Barco
       
        public string GenerarArchivoBarco(DTBarco DTBarco) {
            DTResultadoOperacionList<DTBarco> Resultado = new DTResultadoOperacionList<DTBarco>();
            Resultado = new BMBarco().GenerarArchivoBarco(DTBarco);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;
            return serializer.Serialize(Resultado);
        }
        public ActionResult Barco()
        {
            //Cargar todos los registros de barcos en el Arbol
            new BMBarco().cargarBarcosEnArbol();
            //Presenta la vista Barco
            return View("Barco");
        }
        public string ConsultarBarcos() {
            DTResultadoOperacionList<DTBarco> Resultado = new BMBarco().ConsultarBarcos();

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;
            return serializer.Serialize(Resultado);

        }
        public ActionResult TreeView()
        {
            string resultadoArbol = new BMBarco().imprimirArbol();
            ViewData["Arbol"] = resultadoArbol;
            return View("../TreeView");
        }
    }
}