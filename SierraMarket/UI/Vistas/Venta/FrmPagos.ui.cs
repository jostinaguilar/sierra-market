using SierraMarket.UI.Componentes;
using SierraMarket.UI.Tema;
using System.Drawing;
using System.Windows.Forms;

namespace SierraMarket.UI.Vistas.Venta
{
    public partial class FrmPagos : Form
    {
        // TextBox nativo para el monto y controles de formulario
        private TextBox txtMontoAPagar;
        private ComboBox cmbFormaPago;
        private Boton.Primario btnGuardar;
        private Boton.Secundario btnCancelar;

        private Boton.Secundario btnPop;
        private Boton.Primario btnPush;

        private ListBox lstPagos;
        private Texto.Etiqueta lblMontoAbonado;
        private Texto.Etiqueta lblMontoRestante;

        private void ConfigurarVentana()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.AutoSize = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Pagos | Sierra Market";
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
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 65F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));

            var lblTitulo = new Texto.Titulo("Realizar Pago");

            var lblTotalPagar = new Texto.Etiqueta($"Total a pagar: S/ {_total:0.00}");

            var pnlMonto = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, Margin = new Padding(0, 0, 0, 10) };
            pnlMonto.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            pnlMonto.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            pnlMonto.Controls.Add(new Texto.Etiqueta("Monto a Pagar"), 0, 0);
            txtMontoAPagar = new TextBox
            {
                Dock = DockStyle.Fill,
                PlaceholderText = "Ingrese el monto a pagar",
                BorderStyle = BorderStyle.FixedSingle,
                Font = Fuentes.GeistRegular(10F)
            };
            pnlMonto.Controls.Add(txtMontoAPagar, 0, 1);

            var lblFormaPago = new Texto.Etiqueta("Forma de Pago");
            cmbFormaPago = new ComboBox();
            cmbFormaPago.Dock = DockStyle.Fill;
            cmbFormaPago.Margin = new Padding(0);
            cmbFormaPago.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFormaPago.Items.Add("Efectivo");
            cmbFormaPago.Items.Add("YAPE");
            cmbFormaPago.Items.Add("PLIN");

            var tlpBotonesPila = new TableLayoutPanel();
            tlpBotonesPila.Padding = new Padding(0);
            tlpBotonesPila.Margin = new Padding(0);
            tlpBotonesPila.Dock = DockStyle.Fill;
            tlpBotonesPila.RowCount = 1;
            tlpBotonesPila.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpBotonesPila.ColumnCount = 3;
            tlpBotonesPila.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpBotonesPila.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            tlpBotonesPila.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            lstPagos = new ListBox();
            lstPagos.Dock = DockStyle.Fill;
            lstPagos.Margin = new Padding(0);
            lstPagos.Padding = new Padding(0);

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

            btnPop = new Boton.Secundario("Deshacer");
            btnPop.Dock = DockStyle.Fill;
            btnPop.Click += BtnPop_Click;

            btnPush = new Boton.Primario("Agregar");
            btnPush.Dock = DockStyle.Fill;
            btnPush.Click += BtnPush_Click;

            lblMontoAbonado = new Texto.Etiqueta("Monto Abonado: S/ 0.00");
            lblMontoRestante = new Texto.Etiqueta($"Monto Restante: S/ {_total:0.00}");

            tlpBotonesPila.Controls.Add(btnPop, 0, 0);
            tlpBotonesPila.Controls.Add(btnPush, 2, 0);

            tlpBotones.Controls.Add(btnCancelar, 0, 0);
            tlpBotones.Controls.Add(btnGuardar, 2, 0);

            tlpFormulario.Controls.Add(lblTitulo, 0, 0);
            tlpFormulario.Controls.Add(lblTotalPagar, 0, 2);
            tlpFormulario.Controls.Add(lblFormaPago, 0, 4);
            tlpFormulario.Controls.Add(cmbFormaPago, 0, 6);
            tlpFormulario.Controls.Add(pnlMonto, 0, 7);
            tlpFormulario.Controls.Add(tlpBotonesPila, 0, 9);
            tlpFormulario.Controls.Add(lstPagos, 0, 11);
            tlpFormulario.Controls.Add(lblMontoAbonado, 0, 13);
            tlpFormulario.Controls.Add(lblMontoRestante, 0, 15);
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

            ActualizarListaPagos();
        }

        public void ActualizarListaPagos()
        {
            decimal montoAbonado = 0;

            lstPagos.Items.Clear();
            foreach (var pago in _pilaPagos)
            {
                montoAbonado += pago.Monto;
                lstPagos.Items.Add($"Monto: S/ {pago.Monto:0.00} | Medio: {pago.MedioPago} | Fecha: {pago.FechaHora}");
            }

            decimal montoRestante = _total - montoAbonado;
            txtMontoAPagar.Text = montoRestante.ToString("0.00");
            lblMontoAbonado.Text = $"Monto Abonado: S/ {montoAbonado:0.00}";
            lblMontoRestante.Text = $"Monto Restante: S/ {montoRestante:0.00}";
        }
    }
}
