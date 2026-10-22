using System;
using System.Collections.Generic;
using System.Text;

namespace SierraMarket.Modelos
{
    public class Pago
    {
        public string MedioPago { get; set; }
        public decimal Monto { get; set; }
        public string NumeroOperacion { get; set; }
        public DateTime FechaHora { get; set; }
    }
}
