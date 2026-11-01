using SierraMarket.Estructuras.Listas;
using SierraMarket.Modelos;
using SierraMarket.UI.Vistas.Inventario;
using SierraMarket.UI.Vistas.Turnos;
using SierraMarket.UI.Vistas.Venta;
using System;
using System.Windows.Forms;

namespace SierraMarket.UI
{
    public partial class FrmPrincipal : Form
    {
        private ListaSimple<Producto> _listaProductos = new ListaSimple<Producto>();

        private ListaCircular<Trabajador> _listaTrabajadores = new ListaCircular<Trabajador>();

        private ListaSimple<Venta> _listaVentas = new ListaSimple<Venta>();

        private UscInventario _uscInventario;
        private UscVenta _uscVenta;
        private UscTurnos _uscTurnos;

        private UscHistorial _uscHistorial;

        public FrmPrincipal()
        {
            InitializeComponent();

            ConstruirInterfaz();

            CargarTrabajadores();
            CargarProductos();
            _uscVenta = new UscVenta(_listaProductos, _listaTrabajadores, _listaVentas);
            CargarVista(_uscVenta);
        }

        private void BtnVentas_Click(object sender, EventArgs e)
        {
            if (_uscVenta == null)
            {
                _uscVenta = new UscVenta(_listaProductos, _listaTrabajadores, _listaVentas);
            }

            CargarVista(_uscVenta);
        }

        private void BtnInventario_Click(object sender, EventArgs e)
        {
            if (_uscInventario == null)
            {
                _uscInventario = new UscInventario(_listaProductos);
            }

            CargarVista(_uscInventario);
        }

        private void BtnTurnos_Click(object sender, EventArgs e)
        {
            if (_uscTurnos == null)
            {
                _uscTurnos = new UscTurnos(_listaTrabajadores);
                _uscTurnos.OnTrabajadorCambiado += (trabajador) =>
                {
                    lblNombreUsuario.Text = trabajador.Nombres;
                };
            }

            CargarVista(_uscTurnos);
        }

        private void BtnHistorial_Click(object sender, EventArgs e)
        {
            if (_uscHistorial == null)
            {
                _uscHistorial = new UscHistorial(_listaVentas);
            }

            _uscHistorial.CargarVentas();
            CargarVista(_uscHistorial);
        }

        private void CargarVista(UserControl vista)
        {
            pnlPrincipal.Controls.Clear();
            vista.Dock = DockStyle.Fill;
            pnlPrincipal.Controls.Add(vista);
        }

        private void CargarTrabajadores()
        {
            Trabajador trabajador1 = new Trabajador(1, "Jhonny Perez", TimeOnly.Parse("08:00"), TimeOnly.Parse("15:00"));
            Trabajador trabajador2 = new Trabajador(2, "Diana Alcantara", TimeOnly.Parse("15:00"), TimeOnly.Parse("22:00"));
            _listaTrabajadores.Agregar(trabajador1);
            _listaTrabajadores.Agregar(trabajador2);
            lblNombreUsuario.Text = _listaTrabajadores.ObtenerActual().Nombres;
        }

        private void CargarProductos()
        {
            Producto producto1 = new Producto(1, "Arroz (1 kg)", 4.50m, 30);
            Producto producto2 = new Producto(2, "Azúcar (1 kg)", 4.00m, 25);
            Producto producto3 = new Producto(3, "Aceite vegetal (1 L)", 8.50m, 15);
            Producto producto4 = new Producto(4, "Leche evaporada (lata)", 4.20m, 40);
            Producto producto5 = new Producto(5, "Fideos (500 g)", 3.50m, 35);
            Producto producto6 = new Producto(6, "Atún en conserva (lata)", 5.80m, 20);
            Producto producto7 = new Producto(7, "Gaseosa (1.5 L)", 7.00m, 18);
            Producto producto8 = new Producto(8, "Agua mineral (625 ml)", 2.00m, 50);
            Producto producto9 = new Producto(9, "Galletas de chocolate (paquete)", 2.50m, 45);
            Producto producto10 = new Producto(10, "Huevos (docena)", 8.00m, 12);
            _listaProductos.Agregar(producto1);
            _listaProductos.Agregar(producto2);
            _listaProductos.Agregar(producto3);
            _listaProductos.Agregar(producto4);
            _listaProductos.Agregar(producto5);
            _listaProductos.Agregar(producto6);
            _listaProductos.Agregar(producto7);
            _listaProductos.Agregar(producto8);
            _listaProductos.Agregar(producto9);
            _listaProductos.Agregar(producto10);
        }
    }
}