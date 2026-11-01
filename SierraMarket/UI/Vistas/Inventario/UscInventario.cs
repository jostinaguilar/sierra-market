using SierraMarket.Estructuras.Listas;
using SierraMarket.Modelos;
using System;
using System.Windows.Forms;

namespace SierraMarket.UI.Vistas.Inventario
{
    public partial class UscInventario : UserControl
    {
        private ListaSimple<Producto> _listaProductos;

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

                if (DialogResult.Yes == MessageBox.Show("¿Está seguro de que desea eliminar el producto de " + producto?.Descripcion + "?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    _listaProductos.EliminarPorDato(producto);
                    ActualizarInventario();
                }
            }
            else if (e.RowIndex >= 0 && dgvInventario.Columns[e.ColumnIndex].Name == "btnEditar")
            {
                var producto = dgvInventario.Rows[e.RowIndex].DataBoundItem as Producto;

                if (producto != null)
                {
                    var frmEditar = new FrmAgregarProducto(_listaProductos, producto);

                    if (frmEditar.ShowDialog() == DialogResult.OK)
                    {
                        ActualizarInventario();
                    }
                }
            }
        }

        private void BtnAgregarProducto_Click(object sender, EventArgs e)
        {
            var frmAgregar = new FrmAgregarProducto(_listaProductos);

            if (frmAgregar.ShowDialog() == DialogResult.OK)
            {
                ActualizarInventario();
            }
        }

        public void ActualizarInventario()
        {
            var productos = _listaProductos.ObtenerTodos();

            dgvInventario.DataSource = null;
            dgvInventario.DataSource = productos;
        }
    }
}
