using SierraMarket.UI.Tema;
using System;
using System.Collections.Generic;
using System.Text;

namespace SierraMarket.UI.Componentes
{
    public static class CampoTexto
    {
        public abstract class Base : TextBox
        {
            protected Base(string placeholder = "", int x = 0, int y = 0)
            {
                PlaceholderText = placeholder;
                BorderStyle = BorderStyle.None;
                BackColor = Color.FromArgb(250, 250, 250);
                Dock = DockStyle.Fill;
                Font = Fuentes.GeistRegular(10F);
            }
        }

        public class CampoBuscar : Base
        {
            public CampoBuscar(string placeholder = "", int x = 0, int y = 0, Font? fuente = null) : base(placeholder, x, y)
            {
                Font = fuente ?? Fuentes.GeistRegular(10F);
            }
        }

        public class Campo : Base
        {
            public Campo(string placeholder = "", int x = 0, int y = 0, Font? fuente = null) : base(placeholder, x, y)
            {
                Font = fuente ?? Fuentes.GeistRegular(10F);
            }
        }

        public static Panel ConBorde(this TextBox txt)
        {
            var pnlBorde = new Panel { Dock = DockStyle.Fill, Margin = new Padding(0), Padding = new Padding(1), BackColor = Color.FromArgb(228, 228, 231) };

            var pnlInterno = new Panel { Dock = DockStyle.Fill, Margin = new Padding(0), Padding = new Padding(10, 5, 10, 0), BackColor = txt.BackColor };

            pnlInterno.Controls.Add(txt);
            pnlBorde.Controls.Add(pnlInterno);

            return pnlBorde;
        }
    }
}
