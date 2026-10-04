using SierraMarket.UI.Tema;
using System;
using System.Collections.Generic;
using System.Text;

namespace SierraMarket.UI.Componentes
{
    public static class CampoTexto
    {
        public class Campo : TextBox
        {
            public Campo(string placeholder = "")
            {
                PlaceholderText = placeholder;
                BorderStyle = BorderStyle.None;
                BackColor = Colores.Fondo;
                Dock = DockStyle.Fill;
                Font = Fuentes.GeistRegular(10F);
            }
        }

        public static Panel ConBorde(this TextBox txt)
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

            pnlInterno.Controls.Add(txt);
            pnlBorde.Controls.Add(pnlInterno);

            return pnlBorde;
        }
    }
}
