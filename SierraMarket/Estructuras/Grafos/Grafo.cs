using System;
using System.Collections.Generic;

namespace SierraMarket.Estructuras.Grafos
{
    public class ResultadoRuta<T>
    {
        public bool ExisteRuta { get; set; }
        public double DistanciaTotal { get; set; }
        public List<Vertice<T>> Camino { get; set; }

        public ResultadoRuta()
        {
            ExisteRuta = false;
            DistanciaTotal = double.PositiveInfinity;
            Camino = new List<Vertice<T>>();
        }
    }

    /// <summary>
    /// Estructura de Grafo Ponderado implementado con Listas de Adyacencia.
    /// Incluye el algoritmo de Dijkstra para calcular el camino mínimo entre puntos de entrega.
    /// </summary>
    public class Grafo<T>
    {
        public List<Vertice<T>> Vertices { get; private set; }

        public Grafo()
        {
            Vertices = new List<Vertice<T>>();
        }

        public Vertice<T> AgregarVertice(int id, string nombre, T dato, int x = 0, int y = 0)
        {
            var vertice = new Vertice<T>(id, nombre, dato, x, y);
            Vertices.Add(vertice);
            return vertice;
        }

        public void AgregarArista(int idOrigen, int idDestino, double peso, bool bidireccional = true)
        {
            var origen = BuscarVertice(idOrigen);
            var destino = BuscarVertice(idDestino);

            if (origen != null && destino != null)
            {
                origen.AgregarArista(destino, peso);
                if (bidireccional)
                {
                    destino.AgregarArista(origen, peso);
                }
            }
        }

        public Vertice<T>? BuscarVertice(int id)
        {
            foreach (var v in Vertices)
            {
                if (v.Id == id) return v;
            }
            return null;
        }

        /// <summary>
        /// Algoritmo de Dijkstra para calcular la ruta más corta entre dos vértices.
        /// </summary>
        public ResultadoRuta<T> Dijkstra(int idOrigen, int idDestino)
        {
            var resultado = new ResultadoRuta<T>();
            var origen = BuscarVertice(idOrigen);
            var destino = BuscarVertice(idDestino);

            if (origen == null || destino == null)
            {
                return resultado;
            }

            if (origen == destino)
            {
                resultado.ExisteRuta = true;
                resultado.DistanciaTotal = 0;
                resultado.Camino.Add(origen);
                return resultado;
            }

            // Tablas de distancias, predecesores y visitados
            Dictionary<int, double> distancias = new Dictionary<int, double>();
            Dictionary<int, Vertice<T>?> predecesores = new Dictionary<int, Vertice<T>?>();
            HashSet<int> visitados = new HashSet<int>();

            foreach (var v in Vertices)
            {
                distancias[v.Id] = double.PositiveInfinity;
                predecesores[v.Id] = null;
            }

            distancias[origen.Id] = 0;

            while (visitados.Count < Vertices.Count)
            {
                Vertice<T>? actual = null;
                double menorDistancia = double.PositiveInfinity;

                foreach (var v in Vertices)
                {
                    if (!visitados.Contains(v.Id) && distancias[v.Id] < menorDistancia)
                    {
                        menorDistancia = distancias[v.Id];
                        actual = v;
                    }
                }

                if (actual == null || menorDistancia == double.PositiveInfinity)
                {
                    break;
                }

                if (actual.Id == destino.Id)
                {
                    break;
                }

                visitados.Add(actual.Id);

                // Relajación de aristas
                foreach (var arista in actual.Adyacentes)
                {
                    var vecino = arista.Destino;
                    if (!visitados.Contains(vecino.Id))
                    {
                        double nuevaDistancia = distancias[actual.Id] + arista.Peso;
                        if (nuevaDistancia < distancias[vecino.Id])
                        {
                            distancias[vecino.Id] = nuevaDistancia;
                            predecesores[vecino.Id] = actual;
                        }
                    }
                }
            }

            // Reconstrucción del camino óptimo
            if (distancias[destino.Id] != double.PositiveInfinity)
            {
                resultado.ExisteRuta = true;
                resultado.DistanciaTotal = distancias[destino.Id];

                var caminoInverso = new List<Vertice<T>>();
                Vertice<T>? paso = destino;

                while (paso != null)
                {
                    caminoInverso.Add(paso);
                    paso = predecesores[paso.Id];
                }

                caminoInverso.Reverse();
                resultado.Camino = caminoInverso;
            }

            return resultado;
        }
    }
}
