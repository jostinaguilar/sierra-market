using SierraMarket.UI.Tema;
using System;
using System.Collections.Generic;
using System.Text;

namespace SierraMarket.UI.Componentes
{
    public static class Texto
    {
        public abstract class TextoBase : Label
        {
            protected TextoBase(string texto = "", int x = 0, int y = 0)
            {
                Text = texto;
                Location = new Point(x, y);
                AutoSize = true;
                ForeColor = Color.Black;
                BackColor = Color.Transparent;
            }
        }

        public class XXL : TextoBase
        {
            public XXL(string texto = "", int x = 0, int y = 0, Font? fuente = null) : base(texto, x, y)
            {
                Font = fuente ?? Fuentes.GeistBlack(18);
            }
        }

        public class XL : TextoBase
        {
            public XL(string texto = "", int x = 0, int y = 0, Font? fuente = null) : base(texto, x, y)
            {
                Font = fuente ?? Fuentes.GeistRegular(16, FontStyle.Bold);
            }
        }

        public class LG : TextoBase
        {
            public LG(string texto = "", int x = 0, int y = 0, Font? fuente = null) : base(texto, x, y)
            {
                Font = fuente ?? Fuentes.GeistBold(14);
            }
        }

        public class Base : TextoBase
        {
            public Base(string texto = "", int x = 0, int y = 0, Font? fuente = null) : base(texto, x, y)
            {
                Font = fuente ?? Fuentes.GeistRegular(12, FontStyle.Bold);
            }
        }

        public class SM : TextoBase
        {
            public SM(string texto = "", int x = 0, int y = 0, Font? fuente = null) : base(texto, x, y)
            {
                Font = fuente ?? Fuentes.GeistMedium(10);
            }
        }

        public class XS : TextoBase
        {
            public XS(string texto = "", int x = 0, int y = 0, Font? fuente = null) : base(texto, x, y)
            {
                Font = fuente ?? Fuentes.GeistLight(8);
            }
        }
    }
}
