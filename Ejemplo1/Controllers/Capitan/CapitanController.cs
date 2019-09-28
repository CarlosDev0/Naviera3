using BM.Capitan;
using DT.Capitan;
using DT.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Ejemplo1.Controllers.Capitan
{
    public class CapitanController : Controller
    {
        // GET: Capitan
        public ActionResult Capitan()
        {
            //Cargar todos los registros de cédula de capitán en el árbol.
            new BMCapitan().cargarCapitanesEnArbol();
            return View("Capitan");
        }
        public string GenerarArchivoCapitan(DTCapitan _Capitan)
        {
            DTResultadoOperacionList<DTCapitan> Resultado = new BMCapitan().GenerarArchivoCapitan(_Capitan);

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;
            return serializer.Serialize(Resultado);
        }
        public string ConsultarCapitanes()
        {
            DTResultadoOperacionList<DTCapitan> Resultado = new BMCapitan().ConsultarCapitan();

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;
            return serializer.Serialize(Resultado);

        }
        public ActionResult ReporteCapitanes()
        {
            return View("ReporteCapitanes");
        }
        public ActionResult TreeView()
        {
            string resultadoArbol = new BMCapitan().imprimirArbol();
            ViewData["Arbol"] = resultadoArbol;
            return View();
        }
    }
}