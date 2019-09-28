using BM.Escala;
using DT.Escala;
using DT.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Ejemplo1.Controllers.Escala
{
    public class EscalaController : Controller
    {
        // GET: Escala
        public ActionResult Escala()
        {
            //Cargar todos los registros de cédula de capitán en el árbol.
            new BMEscala().cargarEscalasEnArbol();
            return View("Escala");
        }
        public string generarArchivoEscala(DTEscala _escala)
        {
            DTResultadoOperacionList<DTEscala> Resultado = new BMEscala().generarArchivoEscala(_escala);

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;
            return serializer.Serialize(Resultado);
        }
        public string consultarEscalas()
        {
            DTResultadoOperacionList<DTEscala> Resultado = new BMEscala().consultarEscalas();

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;
            return serializer.Serialize(Resultado);

        }
        public ActionResult TreeView()
        {
            string resultadoArbol = new BMEscala().imprimirArbol();
            ViewData["Arbol"] = resultadoArbol;
            return View("../TreeView");
        }
    }
}