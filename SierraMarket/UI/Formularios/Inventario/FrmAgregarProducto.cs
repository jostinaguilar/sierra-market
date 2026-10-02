using SierraMarket.Estructuras.Listas;
using SierraMarket.Modelos;
using SierraMarket.UI.Componentes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SierraMarket.UI.Formularios.Inventario
{
    public partial class FrmAgregarProducto : Form
    {
        private readonly ListaSimple<Producto> _listaProductos;

        // TextBoxes para los campos del formulario
        private CampoTexto.Campo txtDescripcion;
        private CampoTexto.Campo txtStock;
        private CampoTexto.Campo txtPrecio;

        public FrmAgregarProducto(ListaSimple<Producto> listaProductos)
        {
            InitializeComponent();

            _listaProductos = listaProductos;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.AutoSize = false;

            var tlpFormulario = new TableLayoutPanel();
            tlpFormulario.Margin = new Padding(0);
            tlpFormulario.Padding = new Padding(20);
            tlpFormulario.ColumnCount = 1;
            tlpFormulario.ColumnStyles.Clear();
            tlpFormulario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpFormulario.RowCount = 16;
            tlpFormulario.Dock = DockStyle.Fill;

            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));

            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));

            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));

            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));

            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));

            var lblTitulo = new Texto.Base("Agregar Producto");

            var lblDescripcion = new Texto.SM("Descripción");
            txtDescripcion = new CampoTexto.Campo("Ingrese una descripción");

            var lblStock = new Texto.SM("Stock");
            txtStock = new CampoTexto.Campo("Ingrese el stock");

            var lblPrecio = new Texto.SM("Precio");
            txtPrecio = new CampoTexto.Campo("Ingrese un precio");

            var btnGuardar = new Botones.Primario("Guardar");
            btnGuardar.Dock = DockStyle.Fill;
            btnGuardar.Click += BtnGuardar_Click;

            tlpFormulario.Controls.Add(lblTitulo, 0, 0);
            tlpFormulario.Controls.Add(lblDescripcion, 0, 2);
            tlpFormulario.Controls.Add(txtDescripcion.ConBorde(), 0, 4);
            tlpFormulario.Controls.Add(lblStock, 0, 6);
            tlpFormulario.Controls.Add(txtStock.ConBorde(), 0, 8);
            tlpFormulario.Controls.Add(lblPrecio, 0, 10);
            tlpFormulario.Controls.Add(txtPrecio.ConBorde(), 0, 12);
            tlpFormulario.Controls.Add(btnGuardar, 0, 14);

            this.Controls.Add(tlpFormulario);

            float altoTotalFilas = 0;
            foreach (RowStyle fila in tlpFormulario.RowStyles)
            {
                altoTotalFilas += fila.Height;
            }

            int altoFinal = (int)altoTotalFilas + tlpFormulario.Padding.Vertical;

            this.ClientSize = new Size(400, altoFinal);
        }

        public void BtnGuardar_Click(object sender, EventArgs e)
        {
            int id;
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

            id = _listaProductos.Count + 1;

            var nuevoProducto = new Producto(id, descripcion, stock, precio);

            _listaProductos.Agregar(nuevoProducto);

            MessageBox.Show("El producto se registró correctamente.", "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
