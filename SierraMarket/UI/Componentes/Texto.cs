using SierraMarket.UI.Tema;
using System;
using System.Collections.Generic;
using System.Text;

namespace SierraMarket.UI.Componentes
{
    public static class Texto
    {
        public abstract class Basico : Label
        {
            protected Basico(string texto, Font fuente)
            {
                Text = texto;
                Font = fuente;
                AutoSize = true;
                ForeColor = Color.Black;
                BackColor = Color.Transparent;
                Padding = new Padding(0);
                Margin = new Padding(0);
            }
        }

        public class Titulo : Basico
        {
            public Titulo(string texto, Peso peso = Peso.Black) : base(texto, Fuentes.Geist(14, peso)) { }
        }

        public class Subtitulo : Basico
        {
            public Subtitulo(string texto, Peso peso = Peso.ExtraBold): base(texto, Fuentes.Geist(12, peso)) { }
        }

        public class Etiqueta : Basico
        {
            public Etiqueta(string texto, Peso peso = Peso.Regular) : base(texto, Fuentes.Geist(10, peso)) { }
        }

        public class Pequeno : Basico
        {
            public Pequeno(string texto, Peso peso = Peso.Regular) : base(texto, Fuentes.Geist(8, peso)) { }
        }
    }
}
