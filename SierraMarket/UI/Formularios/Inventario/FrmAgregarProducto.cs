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
        private CampoEtiquetado Descripcion;
        private CampoEtiquetado Stock;
        private CampoEtiquetado Precio;

        public FrmAgregarProducto(ListaSimple<Producto> listaProductos)
        {
            _listaProductos = listaProductos;

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

            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));

            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));

            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));

            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));

            var lblTitulo = new Texto.Titulo("Agregar Producto");

            Descripcion = new CampoEtiquetado("Descripción", "Ingrese una descripción");

            Stock = new CampoEtiquetado("Stock", "Ingrese el stock");

            Precio = new CampoEtiquetado("Precio", "Ingrese un precio");

            var btnGuardar = new Boton.Primario("Guardar");
            btnGuardar.Dock = DockStyle.Fill;
            btnGuardar.Click += BtnGuardar_Click;

            tlpFormulario.Controls.Add(lblTitulo, 0, 0);
            tlpFormulario.Controls.Add(Descripcion, 0, 2);
            tlpFormulario.Controls.Add(Stock, 0, 4);
            tlpFormulario.Controls.Add(Precio, 0, 6);
            tlpFormulario.Controls.Add(btnGuardar, 0, 8);

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
            string descripcion = Descripcion.Valor;
            int stock;
            decimal precio;

            if (string.IsNullOrEmpty(descripcion))
            {
                MessageBox.Show("Ingrese una descripción para continuar.", "Descripción requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(Stock.Valor, out stock))
            {
                MessageBox.Show("Ingrese un valor numérico válido para el stock.", "Stock inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(Precio.Valor.Replace(',', '.'), out precio))
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
