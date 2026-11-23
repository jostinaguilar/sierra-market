using SierraMarket.Estructuras.Nodos;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SierraMarket.Estructuras.Pilas
{
    /// <summary>
    /// Estructura de Datos Lineal LIFO (Last In, First Out).
    /// </summary>
    public class Pila<T> : IEnumerable<T>
    {
        private NodoSimple<T>? tope;
        private int count;

        public int Count => count;
        public bool EstaVacia => count == 0;

        public Pila()
        {
            tope = null;
            count = 0;
        }

        public void Push(T dato)
        {
            NodoSimple<T> nuevo = new NodoSimple<T>(dato);
            nuevo.Siguiente = tope;
            tope = nuevo;
            count++;
        }

        public T Pop()
        {
            if (EstaVacia || tope == null)
            {
                throw new InvalidOperationException("La pila está vacía.");
            }

            T dato = tope.Dato;
            tope = tope.Siguiente;
            count--;
            return dato;
        }

        public T Peek()
        {
            if (EstaVacia || tope == null)
            {
                throw new InvalidOperationException("La pila está vacía.");
            }

            return tope.Dato;
        }

        public void Limpiar()
        {
            tope = null;
            count = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            NodoSimple<T>? actual = tope;

            while (actual != null)
            {
                yield return actual.Dato;
                actual = actual.Siguiente;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
