using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SierraMarket.Estructuras.Pilas
{
    public class Pila<T> : IEnumerable<T>
    {
        private NodoPila<T> tope;
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
            NodoPila<T> nuevoNodo = new NodoPila<T>(dato);

            nuevoNodo.Siguiente = tope;
            tope = nuevoNodo;
            count++;
        }

        public T Pop()
        {
            if (EstaVacia)
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
            if (EstaVacia)
            {
                throw new InvalidOperationException("La pila está vacía.");
            }

            return tope.Dato;
        }

        public bool TryPop(out T resultado)
        {
            if (EstaVacia)
            {
                resultado = default(T);
                return false;
            }

            resultado = Pop();
            return true;
        }

        public bool TryPeek(out T resultado)
        {
            if (EstaVacia)
            {
                resultado = default(T);
                return false;
            }

            resultado = Peek();
            return true;
        }

        public bool Contains(T dato)
        {
            NodoPila<T> actual = tope;

            while (actual != null)
            {
                if (Equals(actual.Dato, dato))
                {
                    return true;
                }

                actual = actual.Siguiente;
            }

            return false;
        }

        public int Search(T dato)
        {
            NodoPila<T> actual = tope;
            int posicion = 1;

            while (actual != null)
            {
                if (Equals(actual.Dato, dato))
                {
                    return posicion;
                }

                actual = actual.Siguiente;
                posicion++;
            }
            return -1; // No encontrado
        }

        public bool SearchAndPop(T dato)
        {
            int posicion = Search(dato);

            if (posicion == -1)
            {
                return false; // No encontrado
            }

            for (int i = 0; i < posicion; i++)
            {
                Pop();
            }

            return true;
        }

        public int ContarOcurrencias(T dato)
        {
            int total = 0;
            NodoPila<T> actual = tope;

            while (actual != null)
            {
                if (Equals(actual.Dato, dato))
                {
                    total++;
                }

                actual = actual.Siguiente;
            }
            
            return total;
        }

        public Pila<T> Clonar()
        {
            Pila<T> clon = new Pila<T>();
            T[] elementos = AArray();

            for (int i = elementos.Length - 1; i >= 0; i--)
            {
                clon.Push(elementos[i]);
            }

            return clon;
        }

        private T[] AArray()
        {
            T[] array = new T[count];

            NodoPila<T> actual = tope;
            int index = 0;

            while (actual != null)
            {
                array[index++] = actual.Dato;
                actual = actual.Siguiente;
            }

            return array;
        }

        public void Limpiar()
        {
            tope = null;
            count = 0;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
