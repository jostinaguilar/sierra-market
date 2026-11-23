using System;
using System.Collections.Generic;
using System.Text;

namespace SierraMarket.Estructuras.Nodos
{
    public class NodoArbol<T>
    {
        public T Dato { get; set; }
        public NodoArbol<T> Izquierda { get; set; }
        public NodoArbol<T> Derecha { get; set; }

        public NodoArbol(T dato)
        {
            Dato = dato;
            Izquierda = null;
            Derecha = null;
        }
    }
}
