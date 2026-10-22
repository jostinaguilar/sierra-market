using SierraMarket.Estructuras.Listas;
using SierraMarket.Estructuras.Pilas;
using System;
using System.Collections.Generic;
using System.Text;

namespace SierraMarket.Modelos
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public DateTime FechaHora { get; set; }

        public Trabajador Trabajador { get; set; }

        public string NombreTrabajador => Trabajador?.Nombres ?? "Desconocido";

        public ListaDoble<DetalleVenta> DetalleVenta { get; set; }

        public Pila<Pago> Pagos { get; set; }

        public decimal Total { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal Vuelto { get; set; }
        public string Estado { get; set; }

        public int Intentos { get; set; }

        public Venta(int idVenta, DateTime fechaHora, Trabajador trabajador, ListaDoble<DetalleVenta> detalleVenta, Pila<Pago> pagos, decimal total, decimal montoPagado, decimal vuelto, string estado, int intentos)
        {
            IdVenta = idVenta;
            FechaHora = fechaHora;
            Trabajador = trabajador;
            DetalleVenta = detalleVenta;
            Pagos = pagos;
            Total = total;
            MontoPagado = montoPagado;
            Vuelto = vuelto;
            Estado = estado;
            Intentos = intentos;
        }
    }
}
