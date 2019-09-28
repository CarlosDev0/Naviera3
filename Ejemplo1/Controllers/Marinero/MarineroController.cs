using DM.Marinero;
using DT.General;
using DT.Marinero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BM.Marinero;

namespace Ejemplo1.Controllers.Marinero
{
    public class MarineroController : Controller
    {
        // GET: Marinero
        public ActionResult Marinero()
        {
            //Cargar todos los registros de cédula de marinero en el árbol.
            new BMMarinero().cargarMarinerosEnArbol();
            return View("Marinero");
        }
        public string GenerarArchivoMarinero(DTMarinero _marinero)
        {
            DTResultadoOperacionList<DTMarinero> Resultado = new BMMarinero().GenerarArchivoMarinero(_marinero);

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;
            return serializer.Serialize(Resultado);
        }
      
        public string ConsultarMarineros()
        {
            DTResultadoOperacionList<DTMarinero> Resultado = new BMMarinero().ConsultarMarineros();

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;
            return serializer.Serialize(Resultado);

        }
        public ActionResult ReporteMarineros()
        {
            return View("ReporteMarineros");
        }
        public ActionResult TreeView()
        {
            string resultadoArbol = new BMMarinero().imprimirArbol();
            ViewData["Arbol"] = resultadoArbol;
            return View("../TreeView");
        }
    }
}