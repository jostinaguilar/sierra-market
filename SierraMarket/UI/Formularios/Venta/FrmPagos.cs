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
        private readonly Pila<Pago> _pilaPagos;
        private decimal _total;

        // TextBoxes para los campos del formulario
        private CampoEtiquetado txtMontoAPagar;
        private ComboBox cmbFormaPago;
        private Boton.Primario btnGuardar;
        private Boton.Secundario btnCancelar;

        private Boton.Secundario btnPop;
        private Boton.Primario btnPush;

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
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));

            var lblTitulo = new Texto.Titulo("Realizar Pago");

            var lblTotalPagar = new Texto.Pequeno($"Total a pagar: S/ {_total:0.00}");

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

            btnPop = new Boton.Secundario("Pop");
            btnPop.Dock = DockStyle.Fill;

            btnPush = new Boton.Primario("Guardar");
            btnPush.Dock = DockStyle.Fill;

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
            tlpFormulario.Controls.Add(tlpBotones, 0, 11);

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
            if(decimal.TryParse(txtMontoAPagar.Valor, out decimal montoPago) || string.IsNullOrEmpty(cmbFormaPago.Text))
            {
                MessageBox.Show("Complete el monto a pagar y forma de pago");
                return;
            }

            string medioPago = cmbFormaPago.Text;

            if (montoPago < _total)
            {
                MessageBox.Show("El monto a pagar debe ser igual al total de la venta.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
