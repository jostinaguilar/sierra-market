namespace SierraMarket.Estructuras.Grafos
{
    public class Arista<T>
    {
        public Vertice<T> Destino { get; set; }
        public double Peso { get; set; } // Distancia en km o tiempo en minutos

        public Arista(Vertice<T> destino, double peso)
        {
            Destino = destino;
            Peso = peso;
        }
    }
}
