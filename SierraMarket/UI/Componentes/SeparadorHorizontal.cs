using SierraMarket.UI.Tema;
using System.Drawing;
using System.Windows.Forms;

namespace SierraMarket.UI.Componentes
{
    public class SeparadorHorizontal : Panel
    {
        public SeparadorHorizontal()
        {
            Dock = DockStyle.Fill;
            Margin = new Padding(0);
            Padding = new Padding(0);
            Height = 1;
            BackColor = Colores.Borde;
        }
    }
}
