using SierraMarket.UI.Tema;
using System;
using System.Collections.Generic;
using System.Text;

namespace SierraMarket.UI.Componentes
{
    public static class SelectorFechaHora
    {
        public class Selector : DateTimePicker
        {
            public Selector()
            {
                Format = DateTimePickerFormat.Custom;
                CustomFormat = "HH:mm";
                ShowUpDown = true;
                Dock = DockStyle.Fill;
                Font = Fuentes.GeistRegular(10F);
            }
        }

        public static Panel ConBorde(this DateTimePicker dtp)
        {
            var pnlBorde = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
                Padding = new Padding(1),
                BackColor = Colores.Borde
            };

            var pnlInterno = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
                Padding = new Padding(10, 4, 10, 0),
                BackColor = Colores.Fondo
            };

            pnlInterno.Controls.Add(dtp);
            pnlBorde.Controls.Add(pnlInterno);

            return pnlBorde;
        }
    }
}
