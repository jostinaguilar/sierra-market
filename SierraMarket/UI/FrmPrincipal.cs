using SierraMarket.Estructuras.Listas;
using SierraMarket.Modelos;
using SierraMarket.UI.Componentes;
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
        private UscInventario _uscInventario;
        private UscVenta _uscVenta;

        private Panel pnlPrincipal;

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
            var lblTitulo = new Texto.XXL("Sierra Market");

            lblTitulo.Padding = new Padding(0);

            flpTitulo.Controls.Add(lblTitulo);
            flpTitulo.Dock = DockStyle.Left;
            flpTitulo.AutoSize = true;
            flpTitulo.WrapContents = false;
            flpTitulo.FlowDirection = FlowDirection.LeftToRight;
            flpTitulo.Padding = new Padding(20, 12, 0, 0);

            pnlHeader.Controls.Add(flpTitulo);

            var flpCaja = new FlowLayoutPanel();
            var lblCaja = new Texto.SM("Caja", 0, 0, Fuentes.GeistLight(10));
            var lblNumeroCaja = new Texto.SM("01");

            lblCaja.Margin = new Padding(0);
            lblNumeroCaja.Margin = new Padding(0);

            flpCaja.Controls.AddRange(new Control[] { lblCaja, lblNumeroCaja });

            flpCaja.Dock = DockStyle.Right;
            flpCaja.AutoSize = true;
            flpCaja.WrapContents = false;
            flpCaja.FlowDirection = FlowDirection.LeftToRight;
            flpCaja.Padding = new Padding(0, 20, 20, 0);

            var flpUsuario = new FlowLayoutPanel();
            var lblUsuario = new Texto.SM("Usuario", 0, 0, Fuentes.GeistLight(10));
            var lblNombreUsuario = new Texto.SM("Jhon Doe");

            lblUsuario.Margin = new Padding(0);
            lblNombreUsuario.Margin = new Padding(0);

            flpUsuario.Controls.AddRange(new Control[] { lblUsuario, lblNombreUsuario });

            flpUsuario.Dock = DockStyle.Right;
            flpUsuario.AutoSize = true;
            flpUsuario.WrapContents = false;
            flpUsuario.FlowDirection = FlowDirection.LeftToRight;
            flpUsuario.Padding = new Padding(0, 20, 20, 0);

            var flpReloj = new FlowLayoutPanel();

            var lblHora = new Texto.SM(DateTime.Now.ToString("hh:mm:ss tt"));

            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += (sender, e) => lblHora.Text = DateTime.Now.ToString("hh:mm:ss tt");
            timer.Start();

            flpReloj.Dock = DockStyle.Right;
            flpReloj.AutoSize = true;
            flpReloj.WrapContents = false;
            flpReloj.FlowDirection = FlowDirection.LeftToRight;
            flpReloj.Padding = new Padding(0, 20, 20, 0);

            flpReloj.Controls.Add(lblHora);

            pnlHeader.Controls.AddRange(new Control[] { flpCaja, flpUsuario, flpReloj });

            var flpMenu = new FlowLayoutPanel();
            flpMenu.Dock = DockStyle.Fill;
            flpMenu.Padding = new Padding(0);
            flpMenu.WrapContents = false;
            flpMenu.AutoSize = true;
            flpMenu.FlowDirection = FlowDirection.LeftToRight;

            var btnVentas = new Botones.Transparente("Ventas", 40);
            var btnInventario = new Botones.Transparente("Inventario", 40);
            var btnTurnos = new Botones.Transparente("Turnos", 40);
            var btnHistorial = new Botones.Transparente("Historial", 40);

            btnVentas.Click += BtnVentas_Click;
            btnInventario.Click += BtnInventario_Click;
            btnTurnos.Click += BtnTurnos_Click;

            flpMenu.Controls.AddRange(new Control[] { btnVentas, btnInventario, btnTurnos, btnHistorial });

            pnlMenu.Controls.Add(flpMenu);
            
            CargarVista(new UscVenta(_listaProductos));
        }

        private void BtnVentas_Click(object sender, EventArgs e)
        {
            if (_uscVenta == null)
            {
                _uscVenta = new UscVenta(_listaProductos);
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

        public void CargarVista(UserControl vista)
        {
            pnlPrincipal.Controls.Clear();
            vista.Dock = DockStyle.Fill;
            pnlPrincipal.Controls.Add(vista);
        }

        private void BtnTurnos_Click(object sender, EventArgs e)
        {
            FrmTurnos frmTurnos = new FrmTurnos();
            frmTurnos.ShowDialog();
        }
    }
}
