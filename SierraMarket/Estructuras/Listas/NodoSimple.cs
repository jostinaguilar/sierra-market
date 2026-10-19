using System;
using System.Collections.Generic;
using System.Text;

namespace SierraMarket.Estructuras.Listas
{
    public class NodoSimple<T>
    {
        public T Dato { get; set; }
        public NodoSimple<T> Siguiente { get; set; }

        public NodoSimple(T dato)
        {
            Dato = dato;
            Siguiente = null;
        }
    }
}
