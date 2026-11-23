using System.Collections.Generic;

namespace SierraMarket.Estructuras.Grafos
{
    public class Vertice<T>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public T Dato { get; set; }
        public int X { get; set; } // Coordenada X para dibujar en el mapa
        public int Y { get; set; } // Coordenada Y para dibujar en el mapa

        public List<Arista<T>> Adyacentes { get; set; }

        public Vertice(int id, string nombre, T dato, int x = 0, int y = 0)
        {
            Id = id;
            Nombre = nombre;
            Dato = dato;
            X = x;
            Y = y;
            Adyacentes = new List<Arista<T>>();
        }

        public void AgregarArista(Vertice<T> destino, double peso)
        {
            Adyacentes.Add(new Arista<T>(destino, peso));
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
