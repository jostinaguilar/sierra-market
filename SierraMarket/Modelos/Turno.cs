using System;
using System.Collections.Generic;
using System.Text;

namespace SierraMarket.Modelos
{
    public class Turno
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }
        public Turno(int id, string nombre, DateTime horaInicio, DateTime horaFin)
        {
            Id = id;
            Nombre = nombre;
            HoraInicio = horaInicio;
            HoraFin = horaFin;
        }

        public override string ToString()
        {
            return $"{Id} - {Nombre}  - Horario: {HoraInicio:HH:mm} - {HoraFin:HH:mm}";
        }
    }
}
