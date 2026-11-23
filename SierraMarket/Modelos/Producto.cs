using System;
using System.Collections.Generic;
using System.Text;

namespace SierraMarket.Modelos
{
    public class Producto : IComparable<Producto>
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        public Producto(int codigo, string descripcion, decimal precio, int stock)
        {
            Codigo = codigo;
            Descripcion = descripcion;
            Precio = precio;
            Stock = stock;
        }

        public int CompareTo(Producto? other)
        {
            if (other == null) return 1;
            return Codigo.CompareTo(other.Codigo);
        }

        public bool Vender(int cantidad)
        {
            if (cantidad <= Stock)
            {
                Stock -= cantidad;
                return true;
            }
            return false;
        }

        public void Reponer(int cantidad)
        {
            Stock += cantidad;
        }
    }
}
