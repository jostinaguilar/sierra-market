using SierraMarket.Estructuras.Nodos;

namespace SierraMarket.Estructuras.Colas
{
    public class Cola<T>
    {
        private NodoSimple<T> frente;
        private NodoSimple<T> final;
        private int count;
        public int Longitud => count;
        public bool EstaVacia => count == 0;

        public Cola()
        {
            frente = null;
            final = null;
            count = 0;
        }

        public void Encolar(T dato)
        {
            NodoSimple<T> nuevo = new NodoSimple<T>(dato);

            if (EstaVacia)
            {
                frente = nuevo;
                final = nuevo;
            }
            else
            {
                final.Siguiente = nuevo;
                final = nuevo;
            }

            count++;
        }

        public T Desencolar()
        {
            if (EstaVacia)
            {
                throw new InvalidOperationException("La cola está vacía");
            }

            T dato = frente.Dato;
            frente = frente.Siguiente;

            if (frente == null)
            {
                final = null;
            }

            count--;
            return dato;
        }

        /* Front: obtiene el elemento que está en la frente de la cola */
        public T Front()
        {
            if (EstaVacia)
            {
                throw new InvalidOperationException("La cola está vacía");
            }

            return frente.Dato;
        }

        public List<T> Listar()
        {
            List<T> elementos = new List<T>();

            NodoSimple<T> actual = frente;

            while (actual != null)
            {
                elementos.Add(actual.Dato);
                actual = actual.Siguiente;
            }

            return elementos;
        }

        public void Limpiar()
        {
            frente = null;
            final = null;
            count = 0;
        }
    }
}
