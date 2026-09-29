using SierraMarket.UI.Tema;
using System;
using System.Collections.Generic;
using System.Text;

namespace SierraMarket.UI.Componentes
{
    public static class Botones
    {
        public abstract class Base : Button
        {
            protected Base(string texto = "", int x = 0, int y = 0)
            {
                Text = texto;
                Location = new Point(x, y);
                AutoSize = true;
                FlatStyle = FlatStyle.Flat;
                FlatAppearance.BorderSize = 0;
                Margin = new Padding(0);
                Padding = new Padding(20, 0, 20, 0);
            }
        }

        public class Transparente : Base
        {
            public Transparente(string texto = "", int height = 40, int x = 0, int y = 0, Font? fuente = null) : base(texto, x, y)
            {
                Font = fuente ?? Fuentes.GeistRegular(10, FontStyle.Bold);
                ForeColor = Color.Black;
                BackColor = Color.Transparent;
                Height = height;
            }
        }

        public class Primario : Base
        {
            public Primario(string texto = "", int height = 40, int x = 0, int y = 0, Font? fuente = null) : base(texto, x, y)
            {
                Font = fuente ?? Fuentes.GeistRegular(10, FontStyle.Bold);
                ForeColor = Color.White;
                BackColor = Color.Black;
                Height = height;
            }
        }
    }
}
