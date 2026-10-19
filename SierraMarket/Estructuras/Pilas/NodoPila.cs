using System;
using System.Collections.Generic;
using System.Text;

namespace SierraMarket.Estructuras.Pilas
{
    public class NodoPila<T>
    {
        public T Dato { get; set; }

        public NodoPila<T> Siguiente { get; set; }

        public NodoPila(T dato)
        {
            Dato = dato;
            Siguiente = null;
        }
    }
}
