namespace SierraMarket.Modelos
{
    public class PuntoEntrega
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public PuntoEntrega(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
