using SierraMarket.Estructuras.Nodos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SierraMarket.Estructuras.Arboles
{
    public class ArbolBinario<T> where T : IComparable<T>
    {
        private NodoArbol<T> raiz;
        public NodoArbol<T> ObtenerRaiz() => raiz;
        public int Count { get; private set; }

        public ArbolBinario()
        {
            raiz = null;
            Count = 0;
        }

        private NodoArbol<T> InsertarRecursivo(NodoArbol<T> nodo, T dato, ref bool insertado)
        {
            // caso base: si el nodo es null, se crea un nuevo nodo con el dato y se retorna
            if (nodo == null)
            {
                insertado = true;
                return new NodoArbol<T>(dato);
            }

            int comparador = dato.CompareTo(nodo.Dato);

            if (comparador < 0)
            {
                nodo.Izquierda = InsertarRecursivo(nodo.Izquierda, dato, ref insertado);
            }
            else if (comparador > 0)
            {
                nodo.Derecha = InsertarRecursivo(nodo.Derecha, dato, ref insertado);
            }
            else
            {
                // El dato ya existe en el árbol, no se inserta
                insertado = false;
            }

            return nodo;
        }

        public bool Insertar(T dato)
        {
            bool insertado = false;
            raiz = InsertarRecursivo(raiz, dato, ref insertado);
            if (insertado)
            {
                Count++;
            }
            return insertado;
        }

        public NodoArbol<T> Buscar(T dato)
        {
            NodoArbol<T> actual = raiz;

            while (actual != null)
            {
                int comparador = dato.CompareTo(actual.Dato);

                if (comparador == 0)
                {
                    return actual; // Nodo encontrado
                }
                else if (comparador < 0)
                {
                    actual = actual.Izquierda; // Buscar en el subárbol izquierdo
                }
                else
                {
                    actual = actual.Derecha; // Buscar en el subárbol derecho
                }
            }
            return null; // Nodo no encontrado
        }

        public bool Contiene(T dato)
        {
            return Buscar(dato) != null;
        }

        // Buscar el primer dato que cumpla con un predicado (recorrido en pre-order)
        // devuelve default(T) si no se encuentra ningun nodo que cumpla con el predicado

        public NodoArbol<T> BuscarRecursivo(NodoArbol<T> nodo, Predicate<T> predicado)
        {
            if (nodo == null)
            {
                return null;
            }

            if (predicado(nodo.Dato))
            {
                return nodo;
            }

            NodoArbol<T> izquierda = BuscarRecursivo(nodo.Izquierda, predicado);

            if (izquierda != null)
            {
                return izquierda;
            }

            return BuscarRecursivo(nodo.Derecha, predicado);
        }

        public T BuscarPor(Predicate<T> predicate)
        {
            NodoArbol<T> find = BuscarRecursivo(raiz, predicate);

            return find == null ? default(T) : find.Dato;
        }

        // Eliminación
        public bool Eliminar(T dato)
        {
            bool eliminado = false;

            raiz = EliminarRecursivo(raiz, dato, ref eliminado);

            if (eliminado)
            {
                Count--;
            }

            return eliminado;
        }

        public NodoArbol<T> EliminarRecursivo(NodoArbol<T> nodo, T dato, ref bool eliminado)
        {
            if (nodo == null)
            {
                return null;
            }

            int comparador = dato.CompareTo(nodo.Dato);

            if (comparador < 0)
            {
                nodo.Izquierda = EliminarRecursivo(nodo.Izquierda, dato, ref eliminado);
            }
            else if (comparador > 0)
            {
                nodo.Derecha = EliminarRecursivo(nodo.Derecha, dato, ref eliminado);
            }
            else
            {
                eliminado = true;

                // Caso 1: Nodo sin hijos (hoja)
                if (nodo.Izquierda == null && nodo.Derecha == null)
                {
                    return null;
                }

                // Caso 2: Nodo con un hijo
                // Si el nodo tiene un hijo izquierdo, se retorna ese hijo
                if (nodo.Izquierda == null)
                {
                    return nodo.Derecha;
                }

                // Si el nodo tiene un hijo derecho, se retorna ese hijo
                if (nodo.Derecha == null)
                {
                    return nodo.Izquierda;
                }

                // Caso 3: Nodo con dos hijos -> se reemplaza con sucesor in-orden
                // Se busca el nodo con el valor mínimo en el subárbol derecho
                NodoArbol<T> minNodo = EncontrarMinimo(nodo.Derecha);
                nodo.Dato = minNodo.Dato; // Se reemplaza el valor del nodo a eliminar con el valor mínimo encontrado

                //Ojito: Udamos un flag o bandera para no decrementar el contador de elementos del arbol, ya que no se esta eliminando un nodo, sino reemplazando su valor

                bool dummy = false;
                nodo.Derecha = EliminarRecursivo(nodo.Derecha, minNodo.Dato, ref dummy); // Se elimina el nodo mínimo del subárbol derecho

            }

            return nodo;

        }

        public NodoArbol<T> EncontrarMinimo(NodoArbol<T> node)
        {
            while (node.Izquierda != null)
            {
                node = node.Izquierda;
            }

            return node;
        }

    }
}
