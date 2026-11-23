using SierraMarket.Estructuras.Nodos;
using System;
using System.Collections.Generic;

namespace SierraMarket.Estructuras.Listas
{
    public class ListaDoble<T>
    {
        private NodoDoble<T>? cabeza;
        private NodoDoble<T>? cola;
        private int count;

        public int Count => count;
        public bool EstaVacia => count == 0;

        public ListaDoble()
        {
            cabeza = null;
            cola = null;
            count = 0;
        }

        public void Agregar(T dato)
        {
            NodoDoble<T> nuevo = new NodoDoble<T>(dato);
            if (EstaVacia || cola == null)
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

        public bool Eliminar(NodoDoble<T>? nodo)
        {
            if (nodo == null || EstaVacia)
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
                if (cabeza != null) cabeza.Anterior = null;
            }
            else if (cola == nodo)
            {
                cola = cola.Anterior;
                if (cola != null) cola.Siguiente = null;
            }
            else
            {
                if (nodo.Anterior != null) nodo.Anterior.Siguiente = nodo.Siguiente;
                if (nodo.Siguiente != null) nodo.Siguiente.Anterior = nodo.Anterior;
            }

            count--;
            return true;
        }

        public NodoDoble<T>? Buscar(Predicate<T> condicion)
        {
            NodoDoble<T>? actual = cabeza;

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

        public List<T> ObtenerTodos()
        {
            List<T> lista = new List<T>();
            NodoDoble<T>? actual = cabeza;

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
    }
}
