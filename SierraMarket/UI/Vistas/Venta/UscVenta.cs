using SierraMarket.Estructuras.Arboles;
using SierraMarket.Estructuras.Colas;
using SierraMarket.Estructuras.Listas;
using SierraMarket.Modelos;
using SierraMarket.UI.Tema;
using System;
using System.Collections.Generic;
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
        private Cola<VentaPendiente> _colaPendientes;
        private ArbolBinario<Producto> _arbolProductos;
        private int _contadorTicket = 1;

        public UscVenta(
            ListaSimple<Producto> listaProductos,
            ListaCircular<Trabajador> listaTrabajadores,
            ListaSimple<Modelos.Venta> listaVentas,
            Cola<VentaPendiente>? colaPendientes = null,
            ArbolBinario<Producto>? arbolProductos = null)
        {
            _listaProductos = listaProductos;
            _listaTrabajadores = listaTrabajadores;
            _listaVentas = listaVentas;
            _colaPendientes = colaPendientes ?? new Cola<VentaPendiente>();
            _arbolProductos = arbolProductos ?? new ArbolBinario<Producto>();

            // Si el árbol aún no tiene elementos, cargarlo balanceado desde la lista
            if (_arbolProductos.Count == 0 && !_listaProductos.EstaVacia)
            {
                _arbolProductos.CargarBalanceado(_listaProductos.ObtenerTodos());
            }

            ConstruirInterfaz();
            CargarCatalogo();
            ActualizarCarrito();
            ActualizarColaVisual();
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

            // Sincronizamos el Árbol Binario de Búsqueda balanceado
            _arbolProductos.CargarBalanceado(productos);

            dgvCatalogo.DataSource = null;
            dgvCatalogo.DataSource = productos;
        }

        private void TxtBuscarProducto_TextChanged(object? sender, EventArgs e)
        {
            string texto = txtBuscarProducto.Text.Trim();

            if (string.IsNullOrEmpty(texto))
            {
                dgvCatalogo.DataSource = null;
                dgvCatalogo.DataSource = _listaProductos.ObtenerTodos();
                return;
            }

            // Si el texto es numérico, realizamos la búsqueda en el Árbol Binario de Búsqueda (BST) en O(log n)
            if (int.TryParse(texto, out int codigoBuscado))
            {
                var dummy = new Producto(codigoBuscado, "", 0, 0);
                var nodoEncontrado = _arbolProductos.Buscar(dummy);

                if (nodoEncontrado != null)
                {
                    dgvCatalogo.DataSource = null;
                    dgvCatalogo.DataSource = new List<Producto> { nodoEncontrado.Dato };
                }
                else
                {
                    dgvCatalogo.DataSource = null;
                    dgvCatalogo.DataSource = new List<Producto>();
                }
            }
            else
            {
                // Búsqueda por texto / descripción parcial
                var filtrados = new List<Producto>();
                foreach (var p in _listaProductos.ObtenerTodos())
                {
                    if (p.Descripcion.Contains(texto, StringComparison.OrdinalIgnoreCase))
                    {
                        filtrados.Add(p);
                    }
                }
                dgvCatalogo.DataSource = null;
                dgvCatalogo.DataSource = filtrados;
            }
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
            if (_listaTrabajadores.EstaVacia || _listaTrabajadores.ObtenerActual() == null)
            {
                MessageBox.Show("No hay ningún cajero asignado en el turno actual. Asigne un trabajador en el módulo de turnos.", "Turno no disponible", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var carrito = _carrito.ObtenerTodos();
            decimal total = 0;
            foreach (var item in carrito)
            {
                total += item.Subtotal;
            }

            var frmPagos = new FrmPagos(total);
            var resultado = frmPagos.ShowDialog();

            if (resultado == DialogResult.OK)
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
                MessageBox.Show($"¡Venta #{idVenta} registrada en el historial!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (resultado == DialogResult.Retry && frmPagos.EncolarSolicitado)
            {
                // El cliente tuvo problemas con su pago digital:
                // Se encola la venta (FIFO) con su carrito y pagos parciales ya registrados en la Pila.
                Trabajador cajero = _listaTrabajadores.ObtenerActual();

                var ventaPendiente = new VentaPendiente(
                    _contadorTicket++,
                    DateTime.Now,
                    cajero,
                    _carrito,
                    frmPagos.PilaPagos,
                    total
                );

                _colaPendientes.Encolar(ventaPendiente);

                // Caja disponible de inmediato con un nuevo carrito para atender al siguiente cliente
                _carrito = new ListaDoble<DetalleVenta>();
                ActualizarCarrito();
                ActualizarColaVisual();

                MessageBox.Show($"Cliente encolado como Ticket #P-{ventaPendiente.IdTicket} en espera de confirmación.\nCaja disponible para atender al siguiente.", "Venta en Espera", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ActualizarColaVisual()
        {
            lstColaPendientes.Items.Clear();
            var listaPendientes = _colaPendientes.Listar();

            foreach (var pendiente in listaPendientes)
            {
                lstColaPendientes.Items.Add(pendiente.ToString());
            }

            lblColaTitulo.Text = $"En Espera ({_colaPendientes.Longitud})";
            btnDesencolar.Enabled = !_colaPendientes.EstaVacia;
        }

        private void BtnDesencolar_Click(object? sender, EventArgs e)
        {
            if (_colaPendientes.EstaVacia)
            {
                MessageBox.Show("No hay clientes en la cola de espera.", "Cola vacía", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Si hay productos en el carrito actual, advertir al cajero para no perderlos
            if (!_carrito.EstaVacia)
            {
                var resp = MessageBox.Show(
                    "El carrito actual tiene productos agregados.\n¿Desea descartar la venta actual para atender al cliente en espera?",
                    "Carrito en uso",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (resp != DialogResult.Yes)
                {
                    return;
                }
            }

            // Desencolar: Primero en entrar, primero en salir (FIFO)
            var ventaPendiente = _colaPendientes.Desencolar();
            ActualizarColaVisual();

            // Restauramos el carrito de esa venta
            _carrito = ventaPendiente.Carrito;
            ActualizarCarrito();

            // Abrimos directamente el popup de pagos con los pagos parciales recuperados
            var frmPagos = new FrmPagos(ventaPendiente.Total, ventaPendiente.PagosParciales);
            var resultado = frmPagos.ShowDialog();

            if (resultado == DialogResult.OK)
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
                    ventaPendiente.Total,
                    ventaPendiente.Total,
                    0,
                    "Completada"
                );

                _listaVentas.InsertarAlInicio(nuevaVenta);

                _carrito.Limpiar();
                ActualizarCarrito();
                CargarCatalogo();
                MessageBox.Show($"¡Venta #{idVenta} (Ticket #P-{ventaPendiente.IdTicket}) completada con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (resultado == DialogResult.Retry && frmPagos.EncolarSolicitado)
            {
                // Si vuelve a fallar la conexión, vuelve a ingresar a la cola al final
                _colaPendientes.Encolar(ventaPendiente);
                _carrito = new ListaDoble<DetalleVenta>();
                ActualizarCarrito();
                ActualizarColaVisual();
                MessageBox.Show($"El Ticket #P-{ventaPendiente.IdTicket} volvió a ponerse en cola de espera.", "Venta Re-encolada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Si el cajero canceló la ventana, damos opción de reencolar o cancelar
                var decision = MessageBox.Show(
                    "¿Desea devolver esta venta a la cola de espera?\n(Si presiona 'No', la venta se cancelará y el carrito se limpiará)",
                    "Venta no finalizada",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (decision == DialogResult.Yes)
                {
                    _colaPendientes.Encolar(ventaPendiente);
                    _carrito = new ListaDoble<DetalleVenta>();
                    ActualizarCarrito();
                    ActualizarColaVisual();
                }
                else
                {
                    _carrito = new ListaDoble<DetalleVenta>();
                    ActualizarCarrito();
                }
            }
        }
    }
}
