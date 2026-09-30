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

            var lblCatalogo = new Texto.Base("Catálogo de Productos", 20, 20);

            lblCatalogo.Dock = DockStyle.Fill;
            lblCatalogo.Margin = new Padding(0);

            var txtBuscarProducto = new CampoTexto.CampoBuscar("Buscar producto...");
            
            tlpVenta.Controls.Add(lblCatalogo, 0, 0);
            tlpVenta.Controls.Add(txtBuscarProducto.ConBorde(), 0, 2);

            this.Controls.Add(tlpVenta);
        }
    }
}
