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

namespace SierraMarket.UI.Vistas.Inventario
{
    public partial class FrmAgregarProducto : Form
    {
        private ListaSimple<Producto> _listaProductos;
        private Producto _productoEditar;

        // TextBoxes nativos para los campos del formulario
        private TextBox txtDescripcion;
        private TextBox txtStock;
        private TextBox txtPrecio;
        private Boton.Primario btnGuardar;
        private Boton.Secundario btnCancelar;

        public FrmAgregarProducto(ListaSimple<Producto> listaProductos, Producto? productoEditar = null)
        {
            _listaProductos = listaProductos;
            _productoEditar = productoEditar;

            ConfigurarVentana();
            CrearFormulario();
        }

        private void ConfigurarVentana()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.AutoSize = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Inventario | Sierra Market";
        }

        private void CrearFormulario()
        {
            var tlpFormulario = new TableLayoutPanel();
            tlpFormulario.Margin = new Padding(0);
            tlpFormulario.Padding = new Padding(20);
            tlpFormulario.ColumnCount = 1;
            tlpFormulario.ColumnStyles.Clear();
            tlpFormulario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpFormulario.RowCount = 16;
            tlpFormulario.Dock = DockStyle.Fill;

            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));

            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 65F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 65F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 65F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));

            var lblTitulo = new Texto.Titulo("Agregar Producto");

            // Campo Descripción (Etiqueta + TextBox nativo)
            var pnlDesc = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, Margin = new Padding(0, 0, 0, 10) };
            pnlDesc.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            pnlDesc.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            pnlDesc.Controls.Add(new Texto.Etiqueta("Descripción"), 0, 0);
            txtDescripcion = new TextBox
            {
                Dock = DockStyle.Fill,
                PlaceholderText = "Ingrese una descripción",
                BorderStyle = BorderStyle.FixedSingle,
                Font = Fuentes.GeistRegular(10F)
            };
            pnlDesc.Controls.Add(txtDescripcion, 0, 1);

            // Campo Precio (Etiqueta + TextBox nativo)
            var pnlPrecio = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, Margin = new Padding(0, 0, 0, 10) };
            pnlPrecio.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            pnlPrecio.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            pnlPrecio.Controls.Add(new Texto.Etiqueta("Precio"), 0, 0);
            txtPrecio = new TextBox
            {
                Dock = DockStyle.Fill,
                PlaceholderText = "Ingrese un precio",
                BorderStyle = BorderStyle.FixedSingle,
                Font = Fuentes.GeistRegular(10F)
            };
            pnlPrecio.Controls.Add(txtPrecio, 0, 1);

            // Campo Stock (Etiqueta + TextBox nativo)
            var pnlStock = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, Margin = new Padding(0, 0, 0, 10) };
            pnlStock.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            pnlStock.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            pnlStock.Controls.Add(new Texto.Etiqueta("Stock"), 0, 0);
            txtStock = new TextBox
            {
                Dock = DockStyle.Fill,
                PlaceholderText = "Ingrese el stock",
                BorderStyle = BorderStyle.FixedSingle,
                Font = Fuentes.GeistRegular(10F)
            };
            pnlStock.Controls.Add(txtStock, 0, 1);

            if (_productoEditar != null)
            {
                lblTitulo.Text = "Editar Producto";
                txtDescripcion.Text = _productoEditar.Descripcion;
                txtPrecio.Text = _productoEditar.Precio.ToString();
                txtStock.Text = _productoEditar.Stock.ToString();
            }

            var tlpBotones = new TableLayoutPanel();

            tlpBotones.Padding = new Padding(0);
            tlpBotones.Margin = new Padding(0);
            tlpBotones.Dock = DockStyle.Fill;
            tlpBotones.RowCount = 1;
            tlpBotones.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpBotones.ColumnCount = 3;
            tlpBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            tlpBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            btnCancelar = new Boton.Secundario("Cancelar");
            btnCancelar.Dock = DockStyle.Fill;

            btnGuardar = new Boton.Primario("Guardar");
            btnGuardar.Dock = DockStyle.Fill;
            btnGuardar.Click += BtnGuardar_Click;

            tlpBotones.Controls.Add(btnCancelar, 0, 0);
            tlpBotones.Controls.Add(btnGuardar, 2, 0);

            tlpFormulario.Controls.Add(lblTitulo, 0, 0);
            tlpFormulario.Controls.Add(pnlDesc, 0, 2);
            tlpFormulario.Controls.Add(pnlPrecio, 0, 3);
            tlpFormulario.Controls.Add(pnlStock, 0, 4);
            tlpFormulario.Controls.Add(tlpBotones, 0, 6);

            this.Controls.Add(tlpFormulario);

            float altoTotalFilas = 0;
            foreach (RowStyle fila in tlpFormulario.RowStyles)
            {
                altoTotalFilas += fila.Height;
            }

            int altoFinal = (int)altoTotalFilas + tlpFormulario.Padding.Vertical;

            this.ClientSize = new Size(400, altoFinal);
            this.AcceptButton = btnGuardar;
            this.CancelButton = btnCancelar;
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
