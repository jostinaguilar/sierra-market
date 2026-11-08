using SierraMarket.UI.Componentes;
using SierraMarket.UI.Tema;
using System.Drawing;
using System.Windows.Forms;

namespace SierraMarket.UI.Vistas.Inventario
{
    public partial class FrmAgregarProducto : Form
    {
        // TextBoxes nativos para los campos del formulario
        private TextBox txtDescripcion;
        private TextBox txtStock;
        private TextBox txtPrecio;
        private Boton.Primario btnGuardar;
        private Boton.Secundario btnCancelar;

        private void ConfigurarVentana()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.AutoSize = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Inventario | Sierra Market";
            this.BackColor = Color.FromArgb(255, 255, 255);
        }

        private void CrearFormulario()
        {
            var tlpFormulario = new TableLayoutPanel();
            tlpFormulario.Margin = new Padding(0);
            tlpFormulario.Padding = new Padding(0);
            tlpFormulario.ColumnCount = 1;
            tlpFormulario.RowCount = 16;
            tlpFormulario.Dock = DockStyle.Fill;

            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));

            var lblTitulo = new Texto.Titulo("Agregar Producto");
            lblTitulo.BackColor = Colores.Fondo;
            lblTitulo.Dock = DockStyle.Fill;
            lblTitulo.Padding = new Padding(20, 18, 20, 15);

            // Campo Descripción (Etiqueta + TextBox nativo)
            var lblDescripcion = new Texto.Etiqueta("Descripción", Peso.Semibold);
            lblDescripcion.Padding = new Padding(20, 8, 20, 0);

            txtDescripcion = new TextBox
            {
                Dock = DockStyle.Fill,
                PlaceholderText = "Ingrese una descripción",
                BorderStyle = BorderStyle.None,
                Font = Fuentes.GeistRegular(10F),
                Margin = new Padding(20, 10, 20, 10)
            };


            // Campo Precio (Etiqueta + TextBox nativo)
            var lblPrecio = new Texto.Etiqueta("Precio", Peso.Semibold);
            lblPrecio.Padding = new Padding(20, 8, 20, 0);
            txtPrecio = new TextBox
            {
                Dock = DockStyle.Fill,
                PlaceholderText = "Ingrese un precio",
                BorderStyle = BorderStyle.None,
                Font = Fuentes.GeistRegular(10F),
                Margin = new Padding(20, 10, 20, 10)
            };


            // Campo Stock (Etiqueta + TextBox nativo)
            var lblStock = new Texto.Etiqueta("Stock", Peso.Semibold);
            lblStock.Padding = new Padding(20, 8, 20, 0);
            txtStock = new TextBox
            {
                Dock = DockStyle.Fill,
                PlaceholderText = "Ingrese el stock",
                BorderStyle = BorderStyle.None,
                Font = Fuentes.GeistRegular(10F),
                Margin = new Padding(20, 10, 20, 10)
            };
            

            if (_productoEditar != null)
            {
                lblTitulo.Text = "Editar Producto";
                txtDescripcion.Text = _productoEditar.Descripcion;
                txtPrecio.Text = _productoEditar.Precio.ToString();
                txtStock.Text = _productoEditar.Stock.ToString();
            }

            var tlpBotones = new TableLayoutPanel();

            tlpBotones.Margin = new Padding(0);
            tlpBotones.Padding = new Padding(20);
            tlpBotones.Dock = DockStyle.Fill;
            tlpBotones.RowCount = 1;
            tlpBotones.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpBotones.ColumnCount = 3;
            tlpBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            tlpBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpBotones.BackColor = Colores.Fondo;

            btnCancelar = new Boton.Secundario("Cancelar");
            btnCancelar.Dock = DockStyle.Fill;

            btnGuardar = new Boton.Primario("Guardar");
            btnGuardar.Dock = DockStyle.Fill;
            btnGuardar.Click += BtnGuardar_Click;

            tlpBotones.Controls.Add(btnCancelar, 0, 0);
            tlpBotones.Controls.Add(btnGuardar, 2, 0);

            tlpFormulario.Controls.Add(lblTitulo, 0, 0);
            tlpFormulario.Controls.Add(new SeparadorHorizontal(), 0, 1);
            tlpFormulario.Controls.Add(lblDescripcion, 0, 2);
            tlpFormulario.Controls.Add(new SeparadorHorizontal(), 0, 4);
            tlpFormulario.Controls.Add(txtDescripcion, 0, 5);
            tlpFormulario.Controls.Add(new SeparadorHorizontal(), 0, 6);
            tlpFormulario.Controls.Add(lblPrecio, 0, 7);
            tlpFormulario.Controls.Add(new SeparadorHorizontal(), 0, 9);
            tlpFormulario.Controls.Add(txtPrecio, 0, 10);
            tlpFormulario.Controls.Add(new SeparadorHorizontal(), 0, 11);
            tlpFormulario.Controls.Add(lblStock, 0, 12);
            tlpFormulario.Controls.Add(new SeparadorHorizontal(), 0, 14);
            tlpFormulario.Controls.Add(txtStock, 0, 15);
            tlpFormulario.Controls.Add(new SeparadorHorizontal(), 0, 16);
            tlpFormulario.Controls.Add(tlpBotones, 0, 17);

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
    }
}
