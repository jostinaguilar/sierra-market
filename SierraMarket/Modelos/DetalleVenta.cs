using System;
using System.Collections.Generic;
using System.Text;

namespace SierraMarket.Modelos
{
    public class DetalleVenta
    {
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }

        public decimal Subtotal => Producto.Precio * Cantidad;
        public string Descripcion => Producto.Descripcion;
        public decimal Precio => Producto.Precio;

        public DetalleVenta(Producto producto, int cantidad)
        {
            Producto = producto;
            Cantidad = cantidad;
        }
    }
}
