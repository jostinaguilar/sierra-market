using SierraMarket.Estructuras.Listas;
using SierraMarket.Estructuras.Pilas;
using System;

namespace SierraMarket.Modelos
{
    /// <summary>
    /// Representa una venta que no pudo completarse de inmediato (ej. por fallos en la conexión de billeteras digitales)
    /// y se encola temporalmente en espera de confirmación.
    /// </summary>
    public class VentaPendiente
    {
        public int IdTicket { get; set; }
        public DateTime FechaHora { get; set; }
        public Trabajador Cajero { get; set; }
        public ListaDoble<DetalleVenta> Carrito { get; set; }
        public Pila<Pago> PagosParciales { get; set; }
        public decimal Total { get; set; }
        public string Motivo { get; set; }

        public decimal MontoAbonado
        {
            get
            {
                decimal suma = 0;
                if (PagosParciales != null)
                {
                    foreach (var pago in PagosParciales)
                    {
                        suma += pago.Monto;
                    }
                }
                return suma;
            }
        }

        public decimal MontoRestante => Total - MontoAbonado;

        public VentaPendiente(int idTicket, DateTime fechaHora, Trabajador cajero, ListaDoble<DetalleVenta> carrito, Pila<Pago> pagosParciales, decimal total, string motivo = "Billetera digital en espera")
        {
            IdTicket = idTicket;
            FechaHora = fechaHora;
            Cajero = cajero;
            Carrito = carrito;
            PagosParciales = pagosParciales;
            Total = total;
            Motivo = motivo;
        }

        public override string ToString()
        {
            return $"#P-{IdTicket} | {FechaHora:HH:mm:ss} | Resta: S/ {MontoRestante:0.00} | Total: S/ {Total:0.00}";
        }
    }
}
