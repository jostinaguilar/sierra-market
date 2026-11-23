namespace SierraMarket.Modelos
{
    public class PuntoEntrega
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; } // "Central", "Residencial", "Comercial", "Salud"

        public PuntoEntrega(int id, string nombre, string tipo)
        {
            Id = id;
            Nombre = nombre;
            Tipo = tipo;
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
