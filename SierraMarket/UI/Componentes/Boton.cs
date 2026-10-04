using SierraMarket.UI.Tema;
using System;
using System.Collections.Generic;
using System.Text;

namespace SierraMarket.UI.Componentes
{
    public static class Boton
    {
        public abstract class Basico : Button
        {
            protected Basico(string texto, Color colorFondo, Color colorTexto)
            {
                Text = texto;
                BackColor = colorFondo;
                ForeColor = colorTexto;
                FlatStyle = FlatStyle.Flat;
                FlatAppearance.BorderSize = 0;
                Font = Fuentes.Geist(10, Peso.Bold);
                Cursor = Cursors.Hand;
                Dock = DockStyle.Fill;
                AutoSize = true;
                MinimumSize = new Size(0, 30);
                Margin = new Padding(0);
                Padding = new Padding(20, 0, 20, 0);
            }
        }

        public class Transparente: Basico
        {
            public Transparente(string texto): base(texto, Color.Transparent, Colores.Primario) { }
        }

        public class Primario: Basico
        {
            public Primario(string texto):base(texto, Colores.Primario, Color.White) { }
        }
    }
}
