using SierraMarket.Estructuras.Listas;
using SierraMarket.Modelos;
using SierraMarket.UI.Tema;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SierraMarket.UI.Vistas.Venta
{
    public partial class UscVenta : UserControl
    {
        private ListaSimple<Producto> _listaProductos;
        private ListaCircular<Trabajador> _listaTrabajadores;
        private ListaSimple<Modelos.Venta> _listaVentas;
        private ListaDoble<DetalleVenta> _carrito = new ListaDoble<DetalleVenta>();

        public UscVenta(ListaSimple<Producto> listaProductos, ListaCircular<Trabajador> listaTrabajadores, ListaSimple<Modelos.Venta> listaVentas)
        {
            InitializeComponent();

            _listaProductos = listaProductos;
            _listaTrabajadores = listaTrabajadores;
            _listaVentas = listaVentas;

            ConstruirInterfaz();
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
                        MessageBox.Show("No puedes agregar m�s unidades que el stock disponible.", "L�mite de Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void DgvCarrito_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvCarrito.Columns[e.ColumnIndex].Name == "colSubtotal")
            {
                var detalle = dgvCarrito.Rows[e.RowIndex].DataBoundItem as DetalleVenta;
                if (detalle != null)
                {
                    e.Value = $"S/ {detalle.Subtotal:0.00}";
                    e.FormattingApplied = true;
                }
            }
        }

        private void DgvCarrito_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvCarrito.Columns[e.ColumnIndex].Name == "colProducto")
            {
                e.Handled = true;
                e.PaintBackground(e.CellBounds, true);

                var detalle = dgvCarrito.Rows[e.RowIndex].DataBoundItem as DetalleVenta;
                if (detalle != null)
                {
                    bool isSelected = (e.State & DataGridViewElementStates.Selected) != 0;
                    Color colorTitulo = Colores.Primario;
                    Color colorSub = isSelected ? Color.FromArgb(70, 70, 75) : Color.FromArgb(113, 113, 122);

                    using var fontTitulo = Fuentes.Geist(9.5f, Peso.Semibold);
                    using var fontSub = Fuentes.Geist(8.5f, Peso.Regular);

                    int left = e.CellBounds.Left + 12;
                    int top = e.CellBounds.Top + 6;

                    using (var brushTitulo = new SolidBrush(colorTitulo))
                    {
                        e.Graphics.DrawString(detalle.Descripcion, fontTitulo, brushTitulo, left, top);
                    }

                    string infoPrecioCantidad = $"{detalle.Cantidad} x S/ {detalle.Precio:0.00}";
                    using (var brushSub = new SolidBrush(colorSub))
                    {
                        e.Graphics.DrawString(infoPrecioCantidad, fontSub, brushSub, left, top + 18);
                    }
                }

                using var penBorde = new Pen(Colores.Borde);
                e.Graphics.DrawLine(penBorde, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right, e.CellBounds.Bottom - 1);
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

        public void CargarCatalogo()
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

                var nuevaVenta = new Modelos.Venta(
                    idVenta,
                    DateTime.Now,
                    cajero,
                    _carrito,
                    frmPagos.PilaPagos,
                    total,
                    total,
                    0,
                    "Completada"
                );

                _listaVentas.InsertarAlInicio(nuevaVenta);

                _carrito.Limpiar();
                ActualizarCarrito();
                CargarCatalogo();
                MessageBox.Show($"�Venta #{idVenta} registrada en el historial!", "�xito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
