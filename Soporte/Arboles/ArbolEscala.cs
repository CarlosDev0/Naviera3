using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soporte.Arboles
{
    public static class ArbolEscala
    {//tomado de: http://sourcecontrol.foro.com
        class Nodo
        {
            public int info;
            public Nodo izq, der;
            public int direccion;
        }
        static Nodo raiz;
        private static int valorBuscado { get; set; }
        private static bool resultadoBusqueda { get; set; }
        public static void setInfoBuscar(int info)
        {
            valorBuscado = info;
        }

        static ArbolEscala()
        {
            raiz = null;
        }


        public static void Insertar(int info, int direccion)
        {
            Nodo nuevo;
            nuevo = new Nodo();
            nuevo.info = info;
            nuevo.izq = null;
            nuevo.der = null;
            nuevo.direccion = direccion;
            if (raiz == null)
                raiz = nuevo;
            else
            {
                Nodo anterior = null, reco;
                reco = raiz;
                while (reco != null)
                {
                    anterior = reco;
                    if (info < reco.info)
                        reco = reco.izq;
                    else
                        reco = reco.der;
                }
                if (info < anterior.info)
                    anterior.izq = nuevo;
                else
                    anterior.der = nuevo;
            }
        }


        private static string ImprimirPre(Nodo reco)
        {
            string cadena = "";
            if (reco != null)
            {
                
                //Console.Write(reco.info + " ");
                cadena = cadena + ImprimirPre(reco.izq);
                cadena = cadena + ImprimirPre(reco.der);
                cadena = cadena + reco.info + " , ";
                
            }
            return cadena;
        }

        public static string ImprimirPre()
        {
            string cadena = "";
            cadena = cadena + ImprimirPre(raiz);
            //Console.WriteLine();
            return cadena;
        }

        private static void ImprimirEntre(Nodo reco)
        {
            if (reco != null)
            {
                ImprimirEntre(reco.izq);
                Console.Write(reco.info + " ");
                ImprimirEntre(reco.der);
            }
        }

        public static void ImprimirEntre()
        {
            ImprimirEntre(raiz);
            Console.WriteLine();
        }


        private static string ImprimirPost(Nodo reco)
        {
            string cadena = "";
            if (reco != null)
            {
                if (reco.info != 0)
                    cadena = cadena + "   /////   " + reco.info + "</p>";
                cadena = cadena + ImprimirPost(reco.izq);
                cadena = cadena + ImprimirPost(reco.der);
                
                
                
            }
            return cadena;
        }


        public static string ImprimirPost()
        {
            string cadena = "";
            cadena = cadena + ImprimirPost(raiz);
            return cadena;
        }

        public static bool buscarNodo(int info)
        {
            
            resultadoBusqueda = false;  //valor inicial de la variable
            ArbolEscala.setInfoBuscar(info);
             buscarNodo(raiz);
            return resultadoBusqueda;
        }

        private static bool buscarNodo(Nodo reco)
        {
            if (reco != null)
            {
                if (valorBuscado == reco.info) { resultadoBusqueda=true; return resultadoBusqueda; }

                if (resultadoBusqueda == false) { 
                    if (reco.izq != null) { buscarNodo(reco.izq); }
                    if (reco.der != null) { buscarNodo(reco.der); }
                }

            }
            return resultadoBusqueda;
        }
        public static void destruirArbol()
        {
            raiz = null;
        }
        
    }
}
