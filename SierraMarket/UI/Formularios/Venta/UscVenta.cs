using SierraMarket.Estructuras.Listas;
using SierraMarket.Modelos;
using SierraMarket.UI.Componentes;
using SierraMarket.UI.Formularios.Inventario;
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
    public partial class UscVenta : UserControl
    {
        private readonly ListaSimple<Producto> _listaProductos;

        private readonly ListaCircular<Trabajador> _listaTrabajadores;

        private readonly ListaSimple<Venta> _listaVentas;

        private readonly ListaDoble<DetalleVenta> _carrito = new ListaDoble<DetalleVenta>();

        private DataGridView dgvCatalogo;
        private DataGridView dgvCarrito;
        private Texto.Subtitulo lblTotal;
        private Boton.Primario btnCobrar;

        public UscVenta(ListaSimple<Producto> listaProductos, ListaCircular<Trabajador> listaTrabajadores, ListaSimple<Venta> listaVentas)
        {
            InitializeComponent();

            _listaProductos = listaProductos;
            _listaTrabajadores = listaTrabajadores;
            _listaVentas = listaVentas;

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

            var pnlCarrito = new Panel();
            pnlCarrito.Dock = DockStyle.Fill;
            pnlCarrito.Margin = new Padding(0);
            pnlCarrito.Padding = new Padding(20);
            pnlCarrito.BackColor = Color.FromArgb(250, 250, 250);

            var tlpCarrito = new TableLayoutPanel();
            tlpCarrito.Dock = DockStyle.Fill;
            tlpCarrito.RowCount = 2;
            tlpCarrito.ColumnCount = 1;
            tlpCarrito.Margin = new Padding(0);
            tlpCarrito.Padding = new Padding(0);

            tlpCarrito.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpCarrito.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            tlpCarrito.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpCarrito.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            tlpCarrito.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpCarrito.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            tlpCarrito.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));

            var lblCarrito = new Texto.Subtitulo("Carrito de Compras");
            lblCarrito.Dock = DockStyle.Fill;
            lblCarrito.Margin = new Padding(0);

            lblTotal = new Texto.Subtitulo("Total: S/ 0.00");
            lblTotal.Dock = DockStyle.Fill;
            lblTotal.Margin = new Padding(0);

            btnCobrar = new Boton.Primario("Cobrar");
            btnCobrar.Click += BtnCobrar_Click;

            var lblCatalogo = new Texto.Subtitulo("Catálogo de Productos");

            lblCatalogo.Dock = DockStyle.Fill;
            lblCatalogo.Margin = new Padding(0);

            var txtBuscarProducto = new CampoTexto.Campo("Buscar producto...");

            dgvCatalogo = new DataGridView();
            dgvCatalogo.Dock = DockStyle.Fill;
            dgvCatalogo.Margin = new Padding(0);
            dgvCatalogo.AutoGenerateColumns = false;
            dgvCatalogo.AllowUserToAddRows = false;
            dgvCatalogo.ReadOnly = true;
            dgvCatalogo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCatalogo.RowHeadersVisible = false;

            dgvCatalogo.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Código", DataPropertyName = "Codigo", Width = 80 });
            dgvCatalogo.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Producto", DataPropertyName = "Descripcion", Width = 250 });
            dgvCatalogo.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Precio", DataPropertyName = "Precio", Width = 80 });
            dgvCatalogo.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Stock", DataPropertyName = "Stock", Width = 80 });
            dgvCatalogo.Columns.Add(new DataGridViewButtonColumn { Name = "btnAgregar", HeaderText = "", Text = "Agregar", UseColumnTextForButtonValue = true, Width = 80 });
            dgvCatalogo.CellContentClick += DgvCatalogo_CellContentClick;

            dgvCarrito = new DataGridView();
            dgvCarrito.Dock = DockStyle.Fill;
            dgvCarrito.Margin = new Padding(0);
            dgvCarrito.AutoGenerateColumns = false;
            dgvCarrito.AllowUserToAddRows = false;
            dgvCarrito.ReadOnly = true;
            dgvCarrito.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCarrito.RowHeadersVisible = false;

            dgvCarrito.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Producto", DataPropertyName = "Descripcion", Width = 100 });
            dgvCarrito.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Precio", DataPropertyName = "Precio", Width = 50 });
            dgvCarrito.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cantidad", DataPropertyName = "Cantidad", Width = 50 });
            dgvCarrito.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Subtotal", DataPropertyName = "Subtotal", Width = 50 });
            dgvCarrito.Columns.Add(new DataGridViewButtonColumn { Name = "btnQuitar", HeaderText = "", Text = "Quitar", UseColumnTextForButtonValue = true, Width = 50 });
            dgvCarrito.CellContentClick += DgvCarrito_CellContentClick;

            tlpCarrito.Controls.Add(lblCarrito, 0, 0);
            tlpCarrito.Controls.Add(dgvCarrito, 0, 2);
            tlpCarrito.Controls.Add(lblTotal, 0, 4);
            tlpCarrito.Controls.Add(btnCobrar, 0, 6);
            pnlCarrito.Controls.Add(tlpCarrito);

            tlpVenta.Controls.Add(lblCatalogo, 0, 0);
            tlpVenta.Controls.Add(txtBuscarProducto.ConBorde(), 0, 2);
            tlpVenta.Controls.Add(dgvCatalogo, 0, 3);

            tlpContenededor.Controls.Add(tlpVenta, 0, 0);
            tlpContenededor.Controls.Add(pnlSeparador2, 1, 0);
            tlpContenededor.Controls.Add(pnlCarrito, 2, 0);

            tlpPrincipal.Controls.Add(tlpContenededor, 0, 0);
            tlpPrincipal.Controls.Add(pnlSeparador1, 0, 1);
            tlpPrincipal.Controls.Add(pnlFooter, 0, 2);

            this.Controls.Add(tlpPrincipal);
            CargarCatalogo();
            ActualizarCarrito();
        }

        private void DgvCatalogo_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvCatalogo.Columns[e.ColumnIndex].Name == "btnAgregar")
            {
                var producto = dgvCatalogo.Rows[e.RowIndex].DataBoundItem as Producto;

                if (producto == null)
                {
                    return;
                }

                if (producto.Stock <= 0)
                {
                    MessageBox.Show("No hay stock disponible de este producto.", "Sin Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var nodoExistente = _carrito.Buscar(item => item.Producto.Codigo == producto.Codigo);
                if (nodoExistente != null)
                {
                    if (nodoExistente.Dato.Cantidad + 1 > producto.Stock)
                    {
                        MessageBox.Show("No puedes agregar más unidades que el stock disponible.", "Límite de Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    nodoExistente.Dato.Cantidad++;
                }
                else
                {
                    _carrito.Agregar(new DetalleVenta(producto, 1));
                }

                ActualizarCarrito();
            }
        }

        private void DgvCarrito_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvCarrito.Columns[e.ColumnIndex].Name == "btnQuitar")
            {
                var detalle = dgvCarrito.Rows[e.RowIndex].DataBoundItem as DetalleVenta;

                var nodoDetalle = _carrito.Buscar(item => item == detalle);
                _carrito.Eliminar(nodoDetalle);

                ActualizarCarrito();
            }
        }

        private void CargarCatalogo()
        {
            var productos = _listaProductos.ObtenerTodos();

            dgvCatalogo.DataSource = null;
            dgvCatalogo.DataSource = productos;
        }

        private void ActualizarCarrito()
        {
            dgvCarrito.DataSource = null;

            var carrito = _carrito.ObtenerTodos();

            dgvCarrito.DataSource = carrito;

            decimal total = 0;
            foreach (var item in carrito)
            {
                total += item.Subtotal;
            }

            lblTotal.Text = $"Total: S/. {total:0.00}";
            btnCobrar.Enabled = !_carrito.EstaVacia;
        }

        private void BtnCobrar_Click(object sender, EventArgs e)
        {
            var carrito = _carrito.ObtenerTodos();
            decimal total = 0;
            foreach (var item in carrito)
            {
                total += item.Subtotal;
            }

            var frmPagos = new FrmPagos(total);

            if (frmPagos.ShowDialog() == DialogResult.OK)
            {
                foreach (var detalle in _carrito.ObtenerTodos())
                {
                    detalle.Producto.Stock -= detalle.Cantidad;
                }

                int idVenta = _listaVentas.Count + 1;

                Trabajador cajero = _listaTrabajadores.ObtenerActual();

                var nuevaVenta = new Venta(
                    idVenta,
                    DateTime.Now,
                    cajero,
                    _carrito,
                    frmPagos.PilaPagos,
                    total,
                    total,
                    0,
                    "Completada",
                    1
                );

                _listaVentas.Agregar(nuevaVenta);

                _carrito.Limpiar();
                ActualizarCarrito();
                CargarCatalogo();
                MessageBox.Show($"¡Venta #{idVenta} registrada en el historial!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
