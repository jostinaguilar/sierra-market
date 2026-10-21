using System;
using System.Collections.Generic;
using System.Text;

namespace SierraMarket.Modelos
{
    public class Trabajador
    {
        public int IdTrabajador { get; set; }
        public string Nombres { get; set; }
        public TimeOnly HoraInicioTurno { get; set; }
        public TimeOnly HoraFinTurno { get; set; }

        public Trabajador(int idTrabajador, string nombres, TimeOnly horaInicioTurno, TimeOnly horaFinTurno)
        {
            IdTrabajador = idTrabajador;
            Nombres = nombres;
            HoraInicioTurno = horaInicioTurno;
            HoraFinTurno = horaFinTurno;
        }
    }
}
