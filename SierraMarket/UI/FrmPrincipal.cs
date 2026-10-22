using SierraMarket.Estructuras.Listas;
using SierraMarket.Modelos;
using SierraMarket.UI.Componentes;
using SierraMarket.UI.Formularios.Turnos;
using SierraMarket.UI.Formularios.Venta;
using SierraMarket.UI.Tema;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SierraMarket.UI
{
    public partial class FrmPrincipal : Form
    {
        private readonly ListaSimple<Producto> _listaProductos = new ListaSimple<Producto>();

        private readonly ListaCircular<Trabajador> _listaTrabajadores = new ListaCircular<Trabajador>();

        private readonly ListaSimple<Venta> _listaVentas = new ListaSimple<Venta>();

        private UscInventario _uscInventario;
        private UscVenta _uscVenta;
        private UscTurnos _uscTurnos;

        private UscHistorial _uscHistorial;

        private Panel pnlPrincipal;

        private Texto.Etiqueta lblNombreUsuario;

        public FrmPrincipal()
        {
            InitializeComponent();

            var tlpPrincipal = new TableLayoutPanel();
            tlpPrincipal.Dock = DockStyle.Fill;
            tlpPrincipal.ColumnCount = 1;
            tlpPrincipal.RowCount = 5;
            tlpPrincipal.Margin = new Padding(0);
            tlpPrincipal.Padding = new Padding(0);
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            var pnlHeader = new Panel();
            pnlHeader.Dock = DockStyle.Fill;
            pnlHeader.Margin = new Padding(0);
            pnlHeader.Padding = new Padding(0);
            pnlHeader.BackColor = Color.FromArgb(250, 250, 250);

            var pnlSeparador1 = new Panel();
            pnlSeparador1.Dock = DockStyle.Fill;
            pnlSeparador1.Margin = new Padding(0);
            pnlSeparador1.Padding = new Padding(0);
            pnlSeparador1.BackColor = Color.FromArgb(228, 228, 231);

            pnlPrincipal = new Panel();
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.Padding = new Padding(0);
            pnlPrincipal.Margin = new Padding(0);
            pnlPrincipal.BackColor = Color.FromArgb(255, 255, 255);

            var pnlSeparador2 = new Panel();
            pnlSeparador2.Dock = DockStyle.Fill;
            pnlSeparador2.Margin = new Padding(0);
            pnlSeparador2.Padding = new Padding(0);
            pnlSeparador2.BackColor = Color.FromArgb(228, 228, 231);

            var pnlMenu = new Panel();
            pnlMenu.Dock = DockStyle.Fill;
            pnlMenu.Margin = new Padding(0);
            pnlMenu.Padding = new Padding(0);
            pnlMenu.BackColor = Color.FromArgb(255, 255, 255);

            var pnlSeparador3 = new Panel();
            pnlSeparador3.Dock = DockStyle.Fill;
            pnlSeparador3.Margin = new Padding(0);
            pnlSeparador3.Padding = new Padding(0);
            pnlSeparador3.BackColor = Color.FromArgb(228, 228, 231);

            tlpPrincipal.Controls.Add(pnlHeader, 0, 0);
            tlpPrincipal.Controls.Add(pnlSeparador1, 0, 1);
            tlpPrincipal.Controls.Add(pnlMenu, 0, 2);
            tlpPrincipal.Controls.Add(pnlSeparador2, 0, 3);
            tlpPrincipal.Controls.Add(pnlPrincipal, 0, 4);

            this.Controls.Add(tlpPrincipal);

            var flpTitulo = new FlowLayoutPanel();
            var lblTitulo = new Texto.Titulo("Sierra Market");

            lblTitulo.Padding = new Padding(0);

            flpTitulo.Controls.Add(lblTitulo);
            flpTitulo.Dock = DockStyle.Left;
            flpTitulo.AutoSize = true;
            flpTitulo.WrapContents = false;
            flpTitulo.FlowDirection = FlowDirection.LeftToRight;
            flpTitulo.Padding = new Padding(20, 15, 0, 0);

            pnlHeader.Controls.Add(flpTitulo);

            var flpCaja = new FlowLayoutPanel();
            var lblCaja = new Texto.Etiqueta("Caja");
            var lblNumeroCaja = new Texto.Etiqueta("01", Peso.Semibold);

            lblCaja.Margin = new Padding(0);
            lblNumeroCaja.Margin = new Padding(0);

            flpCaja.Controls.AddRange(new Control[] { lblCaja, lblNumeroCaja });

            flpCaja.Dock = DockStyle.Right;
            flpCaja.AutoSize = true;
            flpCaja.WrapContents = false;
            flpCaja.FlowDirection = FlowDirection.LeftToRight;
            flpCaja.Padding = new Padding(0, 20, 20, 0);

            var flpUsuario = new FlowLayoutPanel();
            var lblUsuario = new Texto.Etiqueta("Usuario");
            lblNombreUsuario = new Texto.Etiqueta("-", Peso.Semibold);

            lblUsuario.Margin = new Padding(0);
            lblNombreUsuario.Margin = new Padding(0);

            flpUsuario.Controls.AddRange(new Control[] { lblUsuario, lblNombreUsuario });

            flpUsuario.Dock = DockStyle.Right;
            flpUsuario.AutoSize = true;
            flpUsuario.WrapContents = false;
            flpUsuario.FlowDirection = FlowDirection.LeftToRight;
            flpUsuario.Padding = new Padding(0, 20, 20, 0);

            pnlHeader.Controls.AddRange(new Control[] { flpCaja, flpUsuario });

            var flpMenu = new FlowLayoutPanel();
            flpMenu.Dock = DockStyle.Fill;
            flpMenu.Padding = new Padding(0);
            flpMenu.WrapContents = false;
            flpMenu.AutoSize = true;
            flpMenu.FlowDirection = FlowDirection.LeftToRight;

            var btnVentas = new Boton.Transparente("Ventas");
            var btnInventario = new Boton.Transparente("Inventario");
            var btnTurnos = new Boton.Transparente("Turnos");
            var btnHistorial = new Boton.Transparente("Historial");

            btnVentas.Height = 40;
            btnInventario.Height = 40;
            btnTurnos.Height = 40;
            btnHistorial.Height = 40;

            btnVentas.Click += BtnVentas_Click;
            btnInventario.Click += BtnInventario_Click;
            btnTurnos.Click += BtnTurnos_Click;
            btnHistorial.Click += BtnHistorial_Click;

            flpMenu.Controls.AddRange(new Control[] { btnVentas, btnInventario, btnTurnos, btnHistorial });

            pnlMenu.Controls.Add(flpMenu);

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
                    lblNombreUsuario.Text = trabajador?.Nombres ?? "-";
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
            lblNombreUsuario.Text = _listaTrabajadores.ObtenerActual()?.Nombres ?? "-";
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
