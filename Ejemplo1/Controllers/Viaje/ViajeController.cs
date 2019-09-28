using BM.Viaje;
using DT.General;
using DT.Viaje;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Ejemplo1.Controllers.Viaje
{
    public class ViajeController : Controller
    {
        public string GenerarArchivoViaje(DTViaje _Viaje)
        {
            DTResultadoOperacionList<DTViaje> Resultado = new BMViaje().GenerarArchivoViaje(_Viaje);

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;
            return serializer.Serialize(Resultado);
        }
        public ActionResult Viaje()
        {
            //Cargar todos los registros de barcos en el Arbol
            new BMViaje().cargarViajesEnArbol();
            //Presenta la vista Barco
            return View("Viaje");
        }
        public string ConsultarViajes()
        {
            DTResultadoOperacionList<DTViaje> Resultado = new BMViaje().ConsultarViajes();

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;
            return serializer.Serialize(Resultado);

        }
        //GET: DetalleViaje
        public virtual ActionResult DetalleViaje()
        {
            //return RedirectToAction("DetalleViaje");
            return View();
        }
        public string AdicionarMarinero(DTDlleViaje _dtDlleViaje)
        {
            DTResultadoOperacionList<DTDlleViaje> Resultado = new BMViaje().AdicionarMarinero(_dtDlleViaje);

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;
            return serializer.Serialize(Resultado);
        }
        public ActionResult ReporteViajes()
        {
            //Presenta la vista Barco
            return View("ReporteViajes");
        }
        public string ConsultarMarinerosViaje(DTDlleViaje _dtDlleViaje)
        {
            DTResultadoOperacionList<DTDlleViaje> Resultado = new BMViaje().ConsultarMarinerosViaje(_dtDlleViaje);

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;
            return serializer.Serialize(Resultado);
        }
        public ActionResult TreeView()
        {
            string resultadoArbol = new BMViaje().imprimirArbol();
            ViewData["Arbol"] = resultadoArbol;
            return View("../TreeView");
        }
    }
}