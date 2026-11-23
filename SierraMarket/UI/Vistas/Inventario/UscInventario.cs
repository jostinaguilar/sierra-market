using SierraMarket.Estructuras.Listas;
using SierraMarket.Modelos;
using System;
using System.Windows.Forms;

namespace SierraMarket.UI.Vistas.Inventario
{
    public partial class UscInventario : UserControl
    {
        private ListaSimple<Producto> _listaProductos;
        private Producto? _productoEnEdicion = null;

        public UscInventario(ListaSimple<Producto> listaProductos)
        {
            InitializeComponent();

            _listaProductos = listaProductos;
            ConstruirInterfaz();
            ActualizarInventario();
        }

        private void DgvInventario_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvInventario.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                var producto = dgvInventario.Rows[e.RowIndex].DataBoundItem as Producto;

                if (DialogResult.Yes == MessageBox.Show("¿Está seguro de que desea eliminar el producto \"" + producto?.Descripcion + "\"?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    if (producto != null)
                    {
                        if (_productoEnEdicion == producto)
                        {
                            LimpiarFormulario();
                        }
                        _listaProductos.EliminarPorDato(producto);
                        ActualizarInventario();
                    }
                }
            }
            else if (e.RowIndex >= 0 && dgvInventario.Columns[e.ColumnIndex].Name == "btnEditar")
            {
                var producto = dgvInventario.Rows[e.RowIndex].DataBoundItem as Producto;

                if (producto != null)
                {
                    CargarParaEditar(producto);
                }
            }
        }

        private void CargarParaEditar(Producto producto)
        {
            _productoEnEdicion = producto;
            lblFormularioTitulo.Text = "Editar Producto #" + producto.Codigo;
            txtDescripcion.Text = producto.Descripcion;
            txtPrecio.Text = producto.Precio.ToString("0.00");
            txtStock.Text = producto.Stock.ToString();
            btnGuardar.Text = "Actualizar";
            btnLimpiar.Text = "Cancelar";
            txtDescripcion.Focus();
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            string descripcion = txtDescripcion.Text.Trim();

            if (string.IsNullOrEmpty(descripcion))
            {
                MessageBox.Show("Ingrese una descripción para continuar.", "Descripción requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescripcion.Focus();
                return;
            }

            if (!decimal.TryParse(txtPrecio.Text.Trim().Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal precio) || precio < 0)
            {
                MessageBox.Show("Ingrese un precio válido mayor o igual a 0.", "Precio inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecio.Focus();
                return;
            }

            if (!int.TryParse(txtStock.Text.Trim(), out int stock) || stock < 0)
            {
                MessageBox.Show("Ingrese un stock numérico entero válido mayor o igual a 0.", "Stock inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStock.Focus();
                return;
            }

            if (_productoEnEdicion != null)
            {
                _productoEnEdicion.Descripcion = descripcion;
                _productoEnEdicion.Precio = precio;
                _productoEnEdicion.Stock = stock;
                MessageBox.Show("El producto se actualizó correctamente.", "Actualización exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                int nuevoCodigo = _listaProductos.Count + 1;
                var nuevoProducto = new Producto(nuevoCodigo, descripcion, precio, stock);
                _listaProductos.Agregar(nuevoProducto);
                MessageBox.Show("Producto registrado exitosamente.", "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            LimpiarFormulario();
            ActualizarInventario();
        }

        private void BtnLimpiar_Click(object? sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            _productoEnEdicion = null;
            lblFormularioTitulo.Text = "Agregar Producto";
            txtDescripcion.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            txtStock.Text = string.Empty;
            btnGuardar.Text = "Guardar";
            btnLimpiar.Text = "Limpiar";
            txtDescripcion.Focus();
        }

        private void TxtBuscarProducto_TextChanged(object? sender, EventArgs e)
        {
            string filtro = txtBuscarProducto.Text.Trim().ToLower();
            var todos = _listaProductos.ObtenerTodos();

            if (string.IsNullOrEmpty(filtro))
            {
                dgvInventario.DataSource = null;
                dgvInventario.DataSource = todos;
            }
            else
            {
                var filtrados = todos.FindAll(p =>
                    p.Codigo.ToString().Contains(filtro) ||
                    p.Descripcion.ToLower().Contains(filtro));

                dgvInventario.DataSource = null;
                dgvInventario.DataSource = filtrados;
            }
        }

        public void ActualizarInventario()
        {
            TxtBuscarProducto_TextChanged(null, EventArgs.Empty);
        }
    }
}
