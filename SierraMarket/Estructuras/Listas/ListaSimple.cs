using SierraMarket.Estructuras.Nodos;
using System.Collections.Generic;

namespace SierraMarket.Estructuras.Listas
{
    public class ListaSimple<T>
    {
        private NodoSimple<T>? cabeza;
        private int count;

        public int Count => count;
        public bool EstaVacia => count == 0;

        public ListaSimple()
        {
            cabeza = null;
            count = 0;
        }

        public void Agregar(T dato)
        {
            NodoSimple<T> nuevo = new NodoSimple<T>(dato);

            if (EstaVacia || cabeza == null)
            {
                cabeza = nuevo;
            }
            else
            {
                NodoSimple<T> actual = cabeza;
                while (actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }
                actual.Siguiente = nuevo;
            }

            count++;
        }

        public void InsertarAlInicio(T dato)
        {
            NodoSimple<T> nuevo = new NodoSimple<T>(dato);
            nuevo.Siguiente = cabeza;
            cabeza = nuevo;
            count++;
        }

        public bool EliminarPorDato(T dato)
        {
            if (EstaVacia || cabeza == null) return false;

            if (Equals(cabeza.Dato, dato))
            {
                cabeza = cabeza.Siguiente;
                count--;
                return true;
            }

            NodoSimple<T> actual = cabeza;
            while (actual.Siguiente != null)
            {
                if (Equals(actual.Siguiente.Dato, dato))
                {
                    actual.Siguiente = actual.Siguiente.Siguiente;
                    count--;
                    return true;
                }
                actual = actual.Siguiente;
            }

            return false;
        }

        public int Buscar(T dato)
        {
            NodoSimple<T>? actual = cabeza;
            int indice = 0;
            while (actual != null)
            {
                if (Equals(actual.Dato, dato))
                {
                    return indice;
                }
                actual = actual.Siguiente;
                indice++;
            }
            return -1;
        }

        public List<T> ObtenerTodos()
        {
            List<T> lista = new List<T>();
            NodoSimple<T>? actual = cabeza;
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
            count = 0;
        }
    }
}
