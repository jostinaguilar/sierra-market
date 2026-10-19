using System;
using System.Collections.Generic;
using System.Text;

namespace SierraMarket.Modelos
{
    public class DetalleVenta
    {
        public Producto Producto { get; set; }
        int Cantidad { get; set; }

        decimal Subtotal => Producto.Precio * Cantidad;
    }
}
