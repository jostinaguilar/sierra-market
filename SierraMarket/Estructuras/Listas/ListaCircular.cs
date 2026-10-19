using System;
using System.Collections.Generic;
using System.Text;

namespace SierraMarket.Estructuras.Listas
{
    public class ListaCircular<T>
    {
        public NodoDoble<T> cabeza;
        private int count;

        public int Count => count;
        public bool EstaVacia => count == 0;

        public ListaCircular()
        {
            cabeza = null;
            count = 0;
        }

        public void Agregar(T dato)
        {
            NodoDoble<T> nuevoNodo = new NodoDoble<T>(dato);

            if (cabeza == null)
            {
                nuevoNodo.Siguiente = nuevoNodo;
                nuevoNodo.Anterior = nuevoNodo;
                cabeza = nuevoNodo;
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

        public void InsertarAlInicio(T dato)
        {
            NodoDoble<T> nuevoNodo = new NodoDoble<T>(dato);

            if (cabeza == null)
            {
                nuevoNodo.Siguiente = nuevoNodo;
                nuevoNodo.Anterior = nuevoNodo;
                cabeza = nuevoNodo;
            }
            else
            {
                NodoDoble<T> ultimo = cabeza.Anterior;

                nuevoNodo.Siguiente = cabeza;
                nuevoNodo.Anterior = ultimo;
                ultimo.Siguiente = nuevoNodo;
                cabeza.Anterior = nuevoNodo;
                cabeza = nuevoNodo; // Actualizamos la cabeza al nuevo nodo
            }

            count++;
        }

        public bool Insertar(NodoDoble<T> nodoRef, T dato)
        {
            if (nodoRef == null)
            {
                return false;
            }

            NodoDoble<T> nuevoNodo = new NodoDoble<T>(dato);
            NodoDoble<T> siguienteNodo = nodoRef.Siguiente;

            nuevoNodo.Siguiente = siguienteNodo;
            nuevoNodo.Anterior = nodoRef;
            siguienteNodo.Anterior = nuevoNodo;
            nodoRef.Siguiente = nuevoNodo;

            count++;

            return true;
        }

        public bool Eliminar(NodoDoble<T> nodo)
        {
            if (nodo == null || cabeza == null)
            {
                return false;
            }

            if (nodo == cabeza && count == 1)
            {
                cabeza = null;
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
            }

            count--;
            return true;
        }

        public NodoDoble<T> Buscar(T dato)
        {
            if (cabeza == null)
            {
                return null;
            }

            NodoDoble<T> actual = cabeza;

            do
            {
                if (actual.Dato.Equals(dato))
                {
                    return actual;
                }

                actual = actual.Siguiente;

            } while (actual != cabeza);

            return null;
        }

        public NodoDoble<T> ObtenerPorIndice(int indice)
        {
            if (indice < 0 || indice >= count)
            {
                throw new IndexOutOfRangeException("Indice fuera de rango.");
            }

            NodoDoble<T> actual = cabeza;

            for (int i = 0; i < indice; i++)
            {
                actual = actual.Siguiente;
            }

            return actual;
        }

        public List<T> ObtenerTodos()
        {
            List<T> lista = new List<T>();

            if (cabeza != null)
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
        }

        public NodoDoble<T> ObtenerCabeza() => cabeza;

        /*
        Metodos de navegación 
        */

        public NodoDoble<T> ObtenerSiguiente(NodoDoble<T> nodo)
        {
            if (nodo == null)
            {
                return null;
            }

            return nodo.Siguiente;
        }

        public NodoDoble<T> ObtenerAnterior(NodoDoble<T> nodo)
        {
            if (nodo == null)
            {
                return null;
            }

            return nodo.Anterior;
        }
    }
}
