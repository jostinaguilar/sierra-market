using System;
using System.Collections.Generic;
using System.Text;

namespace SierraMarket.Estructuras.Listas
{
    public class ListaSimple<T>
    {
        private Nodo<T> cabeza;
        private int count;
        public int Count => count;

        public ListaSimple()
        {
            cabeza = null;
            count = 0;
        }

        public void Agregar(T valor)
        {
            Nodo<T> nuevo = new Nodo<T>(valor);

            if (cabeza == null)
            {
                cabeza = nuevo;
            }
            else
            {
                Nodo<T> actual = cabeza;

                while(actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }

                actual.Siguiente = nuevo;
                count++;
            }
        }

        public List<T> ObtenerTodos()
        {
            List<T> lista = new List<T>();
            Nodo<T> actual = cabeza;
            while (actual != null)
            {
                lista.Add(actual.Valor);
                actual = actual.Siguiente;
            }
            return lista;
        }
    }
}
