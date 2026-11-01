using SierraMarket.Estructuras.Listas;
using SierraMarket.Modelos;
using System.Windows.Forms;

namespace SierraMarket.UI.Vistas.Venta
{
    public partial class UscHistorial : UserControl
    {
        private ListaSimple<SierraMarket.Modelos.Venta> _listaVentas;

        public UscHistorial(ListaSimple<SierraMarket.Modelos.Venta> listaVentas)
        {
            InitializeComponent();

            _listaVentas = listaVentas;
            ConstruirInterfaz();
            CargarVentas();
        }

        public void CargarVentas()
        {
            var ventas = _listaVentas.ObtenerTodos();

            dgvHistorial.DataSource = null;
            dgvHistorial.DataSource = ventas;
        }
    }
}