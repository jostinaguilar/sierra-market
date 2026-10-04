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
    public partial class UscVenta : UserControl
    {
        private readonly ListaSimple<Producto> _listaProductos;

        public UscVenta(ListaSimple<Producto> listaProductos)
        {
            InitializeComponent();

            _listaProductos = listaProductos;

            var tlpPrincipal = new TableLayoutPanel();
            tlpPrincipal.Dock = DockStyle.Fill;
            tlpPrincipal.ColumnCount = 1;
            tlpPrincipal.RowCount = 3;
            tlpPrincipal.Margin = new Padding(0);
            tlpPrincipal.Padding = new Padding(0);
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));

            var tlpContenededor = new TableLayoutPanel();
            tlpContenededor.Dock = DockStyle.Fill;
            tlpContenededor.Margin = new Padding(0);
            tlpContenededor.Padding = new Padding(0);
            tlpContenededor.ColumnCount = 3;
            tlpContenededor.RowCount = 1;
            tlpContenededor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tlpContenededor.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 1F));
            tlpContenededor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));

            var pnlSeparador1 = new Panel();
            pnlSeparador1.Dock = DockStyle.Fill;
            pnlSeparador1.Margin = new Padding(0);
            pnlSeparador1.Padding = new Padding(0);
            pnlSeparador1.BackColor = Color.FromArgb(228, 228, 231);

            var pnlSeparador2 = new Panel();
            pnlSeparador2.Dock = DockStyle.Fill;
            pnlSeparador2.Margin = new Padding(0);
            pnlSeparador2.Padding = new Padding(0);
            pnlSeparador2.BackColor = Color.FromArgb(228, 228, 231);

            var pnlFooter = new Panel();
            pnlFooter.Dock = DockStyle.Fill;
            pnlFooter.Margin = new Padding(0);
            pnlFooter.Padding = new Padding(0);
            pnlFooter.BackColor = Color.FromArgb(255, 255, 255);

            var tlpVenta = new TableLayoutPanel();
            tlpVenta.Dock = DockStyle.Fill;
            tlpVenta.RowCount = 4;
            tlpVenta.ColumnCount = 1;
            tlpVenta.Margin = new Padding(0);
            tlpVenta.Padding = new Padding(20);

            tlpVenta.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpVenta.RowStyles.Add(new RowStyle(SizeType.Absolute, 6F));
            tlpVenta.RowStyles.Add(new RowStyle(SizeType.Absolute, 33F));
            tlpVenta.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            var pnlVenta = new Panel();
            pnlVenta.Dock = DockStyle.Fill;
            pnlVenta.Margin = new Padding(0);
            pnlVenta.Padding = new Padding(0);
            pnlVenta.BackColor = Color.FromArgb(250, 250, 250);

            var lblCatalogo = new Texto.Subtitulo("Catálogo de Productos");

            lblCatalogo.Dock = DockStyle.Fill;
            lblCatalogo.Margin = new Padding(0);

            var txtBuscarProducto = new CampoTexto.Campo("Buscar producto...");
            
            tlpVenta.Controls.Add(lblCatalogo, 0, 0);
            tlpVenta.Controls.Add(txtBuscarProducto.ConBorde(), 0, 2);

            tlpContenededor.Controls.Add(tlpVenta, 0, 0);
            tlpContenededor.Controls.Add(pnlSeparador2, 1, 0);
            tlpContenededor.Controls.Add(pnlVenta, 2, 0);

            tlpPrincipal.Controls.Add(tlpContenededor, 0, 0);
            tlpPrincipal.Controls.Add(pnlSeparador1, 0, 1);
            tlpPrincipal.Controls.Add(pnlFooter, 0, 2);

            this.Controls.Add(tlpPrincipal);
        }
    }
}
