using System;
using System.Collections.Generic;
using System.Text;

namespace SierraMarket.Estructuras.Listas
{
    public class Nodo<T>
    {
        public T Valor { get; set; }
        public Nodo<T> Siguiente { get; set; }

        public Nodo(T valor)
        {
            Valor = valor;
            Siguiente = null;
        }
    }
}
