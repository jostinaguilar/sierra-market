using SierraMarket.Estructuras.Nodos;
using System;
using System.Collections.Generic;

namespace SierraMarket.Estructuras.Arboles
{
    /// Árbol Binario de Búsqueda (BST) balanceado.
    public class ArbolBinario<T> where T : IComparable<T>
    {
        private NodoArbol<T>? raiz;
        public int Count { get; private set; }
        public bool EstaVacio => raiz == null;

        public ArbolBinario()
        {
            raiz = null;
            Count = 0;
        }

        /// Construye un Árbol Binario de Búsqueda balanceado a partir de una lista ordenada
        /// punto medio como raíz y ramas simétricas O(log n).
        public void CargarBalanceado(List<T> elementos)
        {
            raiz = null;
            Count = 0;

            if (elementos == null || elementos.Count == 0)
            {
                return;
            }

            raiz = ConstruirBalanceadoRecursivo(elementos, 0, elementos.Count - 1);
            Count = elementos.Count;
        }

        private NodoArbol<T> ConstruirBalanceadoRecursivo(List<T> elementos, int inicio, int fin)
        {
            if (inicio > fin)
            {
                return null;
            }

            int medio = inicio + (fin - inicio) / 2;
            var nuevoNodo = new NodoArbol<T>(elementos[medio]);

            nuevoNodo.Izquierda = ConstruirBalanceadoRecursivo(elementos, inicio, medio - 1);
            nuevoNodo.Derecha = ConstruirBalanceadoRecursivo(elementos, medio + 1, fin);

            return nuevoNodo;
        }

        /// Inserción de un elemento en el BST en O(log n).
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

        private NodoArbol<T> InsertarRecursivo(NodoArbol<T>? nodo, T dato, ref bool insertado)
        {
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
                insertado = false; // El dato ya existe en el BST
            }

            return nodo;
        }

        /// Búsqueda binaria en O(log n). Retorna el nodo encontrado o null.
        public NodoArbol<T> Buscar(T dato)
        {
            NodoArbol<T> actual = raiz;

            while (actual != null)
            {
                int comparador = dato.CompareTo(actual.Dato);

                if (comparador == 0)
                {
                    return actual;
                }
                else if (comparador < 0)
                {
                    actual = actual.Izquierda;
                }
                else
                {
                    actual = actual.Derecha;
                }
            }
            return null;
        }

        public bool Contiene(T dato)
        {
            return Buscar(dato) != null;
        }

        /// Eliminación en un BST con reemplazo por sucesor in-orden.
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

        private NodoArbol<T> EliminarRecursivo(NodoArbol<T> nodo, T dato, ref bool eliminado)
        {
            if (nodo == null) return null;

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

                // Caso 1: Hoja
                if (nodo.Izquierda == null && nodo.Derecha == null)
                {
                    return null;
                }

                // Caso 2: Un hijo
                if (nodo.Izquierda == null) return nodo.Derecha;
                if (nodo.Derecha == null) return nodo.Izquierda;

                // Caso 3: Dos hijos (sucesor in-orden: el mínimo del subárbol derecho)
                NodoArbol<T> minNodo = EncontrarMinimo(nodo.Derecha);
                nodo.Dato = minNodo.Dato;

                bool dummy = false;
                nodo.Derecha = EliminarRecursivo(nodo.Derecha, minNodo.Dato, ref dummy);
            }

            return nodo;
        }

        private NodoArbol<T> EncontrarMinimo(NodoArbol<T> node)
        {
            while (node.Izquierda != null)
            {
                node = node.Izquierda;
            }
            return node;
        }

        /// Recorrido In-Orden (Izquierda, Raíz, Derecha).
        public List<T> InOrden()
        {
            var resultado = new List<T>();
            InOrdenRecursivo(raiz, resultado);
            return resultado;
        }

        private void InOrdenRecursivo(NodoArbol<T> nodo, List<T> resultado)
        {
            if (nodo == null) return;
            InOrdenRecursivo(nodo.Izquierda, resultado);
            resultado.Add(nodo.Dato);
            InOrdenRecursivo(nodo.Derecha, resultado);
        }

        public void Limpiar()
        {
            raiz = null;
            Count = 0;
        }
    }
}
