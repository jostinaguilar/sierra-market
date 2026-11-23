using SierraMarket.Estructuras.Nodos;
using System.Collections.Generic;

namespace SierraMarket.Estructuras.Listas
{
    public class ListaCircular<T>
    {
        private NodoDoble<T>? cabeza;
        private NodoDoble<T>? nodoActual;
        private int count;

        public int Count => count;
        public bool EstaVacia => count == 0;

        public ListaCircular()
        {
            cabeza = null;
            nodoActual = null;
            count = 0;
        }

        public void Agregar(T dato)
        {
            NodoDoble<T> nuevoNodo = new NodoDoble<T>(dato);

            if (EstaVacia || cabeza == null)
            {
                cabeza = nuevoNodo;
                cabeza.Siguiente = cabeza;
                cabeza.Anterior = cabeza;
                nodoActual = cabeza;
            }
            else
            {
                NodoDoble<T> ultimo = cabeza.Anterior;

                nuevoNodo.Siguiente = cabeza;
                nuevoNodo.Anterior = ultimo;

                ultimo.Siguiente = nuevoNodo;
                cabeza.Anterior = nuevoNodo;
            }

            count++;
        }

        public bool Eliminar(NodoDoble<T>? nodo)
        {
            if (nodo == null || EstaVacia || cabeza == null)
            {
                return false;
            }

            if (nodo == cabeza && count == 1)
            {
                cabeza = null;
                nodoActual = null;
            }
            else
            {
                NodoDoble<T> anteriorNodo = nodo.Anterior;
                NodoDoble<T> siguienteNodo = nodo.Siguiente;

                anteriorNodo.Siguiente = siguienteNodo;
                siguienteNodo.Anterior = anteriorNodo;

                if (nodo == cabeza)
                {
                    cabeza = siguienteNodo;
                }

                if (nodo == nodoActual)
                {
                    nodoActual = nodoActual.Siguiente;
                }
            }

            count--;
            return true;
        }

        public bool EliminarPorDato(T dato)
        {
            NodoDoble<T>? nodoAEliminar = Buscar(dato);
            if (nodoAEliminar == null)
            {
                return false;
            }

            return Eliminar(nodoAEliminar);
        }

        public NodoDoble<T>? Buscar(T dato)
        {
            if (EstaVacia || cabeza == null)
            {
                return null;
            }

            NodoDoble<T> actual = cabeza;

            do
            {
                if (Equals(actual.Dato, dato))
                {
                    return actual;
                }

                actual = actual.Siguiente;

            } while (actual != cabeza);

            return null;
        }

        public List<T> ObtenerTodos()
        {
            List<T> lista = new List<T>();

            if (cabeza == null)
            {
                return lista;
            }

            NodoDoble<T> actual = cabeza;

            for (int i = 0; i < count; i++)
            {
                lista.Add(actual.Dato);
                actual = actual.Siguiente;
            }

            return lista;
        }

        public void Limpiar()
        {
            cabeza = null;
            count = 0;
            nodoActual = null;
        }

        /*
         * Métodos de navegación circular continua para el módulo de turnos
         */

        public T? ObtenerActual()
        {
            if (nodoActual == null)
            {
                return default;
            }

            return nodoActual.Dato;
        }

        public T? Siguiente()
        {
            if (nodoActual == null)
            {
                return default;
            }

            nodoActual = nodoActual.Siguiente;
            return nodoActual.Dato;
        }

        public T? Anterior()
        {
            if (nodoActual == null)
            {
                return default;
            }

            nodoActual = nodoActual.Anterior;
            return nodoActual.Dato;
        }
    }
}
