using DT.Mensajes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DT.General
{
    public class DTResultadoOperacionList<T>
    {
        /// <summary>
        /// Mensaje que se a mostrar al usuario
        /// </summary>
        public DTMensaje Mensaje { get; set; }
        /// <summary>
        /// Respuesta de la operación realizada
        /// </summary>
        public bool Resultado { get; set; }
        /// <summary>
        /// Data de la operación realizada
        /// </summary>
        public List<T> Datos { get; set; }
    }
}
