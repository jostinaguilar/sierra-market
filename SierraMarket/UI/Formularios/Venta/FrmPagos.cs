using SierraMarket.Estructuras.Pilas;
using SierraMarket.Modelos;
using SierraMarket.UI.Componentes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SierraMarket.UI.Formularios.Venta
{
    public partial class FrmPagos : Form
    {
        private Pila<Pago> _pilaPagos = new Pila<Pago>();
        private decimal _total;

        public Pila<Pago> PilaPagos => _pilaPagos;

        // TextBoxes para los campos del formulario
        private CampoEtiquetado txtMontoAPagar;
        private ComboBox cmbFormaPago;
        private Boton.Primario btnGuardar;
        private Boton.Secundario btnCancelar;

        private Boton.Secundario btnPop;
        private Boton.Primario btnPush;

        private ListBox lstPagos;
        private Texto.Etiqueta lblMontoAbonado;
        private Texto.Etiqueta lblMontoRestante;

        public FrmPagos(decimal total)
        {
            _total = total;

            ConfigurarVentana();
            CrearFormulario();
        }

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

            txtMontoAPagar = new CampoEtiquetado("Monto a Pagar", "Ingrese el monto a pagar");

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
            tlpFormulario.Controls.Add(txtMontoAPagar, 0, 7);
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

        public void BtnPush_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbFormaPago.Text))
            {
                MessageBox.Show("Seleccione una forma de pago.", "Forma de pago requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtMontoAPagar.Valor.Trim(), out decimal monto) || monto <= 0)
            {
                MessageBox.Show("Ingrese un monto válido mayor a 0.", "Monto inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal montoAbonado = 0;
            foreach (var pagoItem in _pilaPagos)
            {
                montoAbonado += pagoItem.Monto;
            }

            decimal montoRestante = _total - montoAbonado;

            if (monto > montoRestante)
            {
                MessageBox.Show("El monto abonado no puede superar el total a pagar.", "Monto excedido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var pago = new Pago
            {
                Monto = monto,
                MedioPago = cmbFormaPago.Text.Trim(),
                FechaHora = DateTime.Now
            };

            _pilaPagos.Push(pago);
            ActualizarListaPagos();

            cmbFormaPago.SelectedIndex = -1;
            //txtMontoAPagar.Campo.Clear();
        }

        public void BtnPop_Click(object sender, EventArgs e)
        {
            if (!_pilaPagos.EstaVacia)
            {
                _pilaPagos.Pop();
                ActualizarListaPagos();
            }
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
            txtMontoAPagar.Campo.Text = montoRestante.ToString("0.00");
            lblMontoAbonado.Text = $"Monto Abonado: S/ {montoAbonado:0.00}";
            lblMontoRestante.Text = $"Monto Restante: S/ {montoRestante:0.00}";
        }

        public void BtnGuardar_Click(object sender, EventArgs e)
        {
            decimal totalAbonado = 0;

            foreach (var pago in _pilaPagos)
            {
                totalAbonado += pago.Monto;
            }

            if (totalAbonado < _total)
            {
                MessageBox.Show($"El monto abonado (S/ {totalAbonado:0.00}) debe ser S/ {_total:0.00}.", "Pago incompleto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Venta completada exitosamente.", "Venta Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
