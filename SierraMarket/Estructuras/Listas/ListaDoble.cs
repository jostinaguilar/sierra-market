using System;
using System.Collections.Generic;
using System.Text;

namespace SierraMarket.Estructuras.Listas
{
    public class ListaDoble<T>
    {
        private NodoDoble<T> cabeza;
        private NodoDoble<T> cola;
        private int count;

        public int Count => count;
        public bool EstaVacia => count == 0;

        public ListaDoble()
        {
            cabeza = null;
            cola = null;
            count = 0;
        }

        public void InsertarAlInicio(T dato)
        {
            NodoDoble<T> nuevo = new NodoDoble<T>(dato);

            if (cabeza == null)
            {
                cabeza = nuevo;
                cola = nuevo;
            }
            else
            {
                nuevo.Siguiente = cabeza;
                cabeza.Anterior = nuevo;
                cabeza = nuevo;
            }

            count++;
        }

        public void Agregar(T dato)
        {
            NodoDoble<T> nuevo = new NodoDoble<T>(dato);
            if (cola == null)
            {
                cabeza = nuevo;
                cola = nuevo;
            }
            else
            {
                cola.Siguiente = nuevo;
                nuevo.Anterior = cola;
                cola = nuevo;
            }

            count++;
        }

        public bool Insertar(NodoDoble<T> nodoRef, T dato)
        {
            if (nodoRef == null)
            {
                return false;
            }

            NodoDoble<T> nuevo = new NodoDoble<T>(dato);

            if (nodoRef.Siguiente == null)
            {
                nodoRef.Siguiente = nuevo;
                nuevo.Anterior = nodoRef;
                cola = nuevo;
            }
            else
            {
                nuevo.Siguiente = nodoRef.Siguiente;
                nuevo.Anterior = nodoRef;
                nodoRef.Siguiente.Anterior = nuevo;
                nodoRef.Siguiente = nuevo;
            }

            count++;
            return true;
        }

        public bool Eliminar(NodoDoble<T> nodo)
        {
            if (nodo == null)
            {
                return false;
            }

            if (cabeza == nodo && cola == nodo)
            {
                cabeza = null;
                cola = null;
            }
            else if (cabeza == nodo)
            {
                cabeza = cabeza.Siguiente;
                cabeza.Anterior = null;
            }
            else if (cola == nodo)
            {
                cola = cola.Anterior;
                cola.Siguiente = null;
            }
            else
            {
                nodo.Anterior.Siguiente = nodo.Siguiente;
                nodo.Siguiente.Anterior = nodo.Anterior;
            }

            count--;
            return true;
        }

        public NodoDoble<T> Buscar(Predicate<T> condicion)
        {
            NodoDoble<T> actual = cabeza;

            while (actual != null)
            {
                if (condicion(actual.Dato))
                {
                    return actual;
                }

                actual = actual.Siguiente;
            }

            return null;
        }

        public NodoDoble<T> ObtenerPorIndice(int indice)
        {
            if (indice < 0 || indice >= count)
            {
                throw new ArgumentOutOfRangeException("Indice fuera del rango.");
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

            NodoDoble<T> actual = cabeza;

            while (actual != null)
            {
                lista.Add(actual.Dato);
                actual = actual.Siguiente;
            }
            return lista;
        }

        public void Limpiar()
        {
            cabeza = null;
            cola = null;
            count = 0;
        }

        /*
        Propiedades de navegación de la lista doblemente enlazada
        para el formulario de ventas, permitiendo acceder a la cabeza y cola de la lista.
        */

        public NodoDoble<T> ObtenerCabeza() => cabeza;

        public NodoDoble<T> ObtenerCola() => cola;
    }
}
