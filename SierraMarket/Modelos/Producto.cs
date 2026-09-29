using System;
using System.Collections.Generic;
using System.Text;

namespace SierraMarket.Modelos
{
    public class Producto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int Stock { get; set; }
        public double Precio { get; set; }

        public Producto(int id, string descripcion, int stock, double precio)
        {
            Id = id;
            Descripcion = descripcion;
            Stock = stock;
            Precio = precio;
        }
    }
}
