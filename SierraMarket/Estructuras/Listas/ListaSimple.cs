using System;
using System.Collections.Generic;
using System.Text;

namespace SierraMarket.Estructuras.Listas
{
    public class ListaSimple<T>
    {
        private NodoSimple<T> cabeza;
        private int count;
        public int Count => count;

        public ListaSimple()
        {
            cabeza = null;
            count = 0;
        }

        public void Agregar(T dato)
        {
            NodoSimple<T> nuevo = new NodoSimple<T>(dato);

            if (cabeza == null)
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

        public bool Insertar(int indice, T dato)
        {
            if (indice < 0 || indice > count)
            {
                return false;
            }

            NodoSimple<T> nuevo = new NodoSimple<T>(dato);
            if (indice == 0)
            {
                nuevo.Siguiente = cabeza;
                cabeza = nuevo;
            }
            else
            {
                NodoSimple<T> actual = cabeza;
                for (int i = 0; i < indice - 1; i++)
                {
                    actual = actual.Siguiente;
                }
                nuevo.Siguiente = actual.Siguiente;
                actual.Siguiente = nuevo;
            }
            count++;
            return true;
        }

        public void InsertarAlInicio(T dato)
        {
            NodoSimple<T> nuevo = new NodoSimple<T>(dato);

            nuevo.Siguiente = cabeza;
            cabeza = nuevo;
            count++;
        }

        public bool Eliminar(int indice)
        {
            if (indice < 0 || indice >= count || cabeza == null)
            {
                return false;
            }

            if (indice == 0)
            {
                cabeza = cabeza.Siguiente;
            }
            else
            {
                NodoSimple<T> actual = cabeza;

                for (int i = 0; i < indice - 1; i++)
                {
                    actual = actual.Siguiente;
                }

                actual.Siguiente = actual.Siguiente.Siguiente;
            }

            count--;
            return true;
        }

        public int Buscar(T dato)
        {
            NodoSimple<T> actual = cabeza;
            int indice = 0;
            while (actual != null)
            {
                if (actual.Dato.Equals(dato))
                {
                    return indice;
                }

                actual = actual.Siguiente;
                indice++;
            }
            return -1; // No encontrado
        }

        public T Obtener(int indice)
        {
            if (indice < 0 || indice >= count)
            {
                throw new ArgumentOutOfRangeException("Indice fuera del rango.");
            }

            NodoSimple<T> actual = cabeza;

            for (int i = 0; i < indice; i++)
            {
                actual = actual.Siguiente;
            }

            return actual.Dato;
        }

        public List<T> ObtenerTodos()
        {
            List<T> lista = new List<T>();
            NodoSimple<T> actual = cabeza;
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
