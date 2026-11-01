using SierraMarket.Estructuras.Listas;
using SierraMarket.Modelos;
using System;
using System.Windows.Forms;

namespace SierraMarket.UI.Vistas.Inventario
{
    public partial class FrmAgregarProducto : Form
    {
        private ListaSimple<Producto> _listaProductos;
        private Producto _productoEditar;

        public FrmAgregarProducto(ListaSimple<Producto> listaProductos, Producto? productoEditar = null)
        {
            _listaProductos = listaProductos;
            _productoEditar = productoEditar;

            ConfigurarVentana();
            CrearFormulario();
        }

        public void BtnGuardar_Click(object sender, EventArgs e)
        {
            int codigo;
            string descripcion = txtDescripcion.Text.Trim();
            int stock;
            decimal precio;

            if (string.IsNullOrEmpty(descripcion))
            {
                MessageBox.Show("Ingrese una descripción para continuar.", "Descripción requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtStock.Text.Trim(), out stock))
            {
                MessageBox.Show("Ingrese un valor numérico válido para el stock.", "Stock inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrecio.Text.Trim().Replace(',', '.'), out precio))
            {
                MessageBox.Show("Ingrese un valor numérico válido para el precio.", "Precio inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_productoEditar != null)
            {
                _productoEditar.Descripcion = descripcion;
                _productoEditar.Stock = stock;
                _productoEditar.Precio = precio;
                MessageBox.Show("El producto se actualizó correctamente.", "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                codigo = _listaProductos.Count + 1;

                var nuevoProducto = new Producto(codigo, descripcion, precio, stock);

                _listaProductos.Agregar(nuevoProducto);

                MessageBox.Show("El producto se registró correctamente.", "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
