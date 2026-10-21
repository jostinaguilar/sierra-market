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
        // TextBoxes para los campos del formulario
        private CampoEtiquetado txtMontoAPagar;
        private ComboBox cmbFormaPago;
        private Boton.Primario btnGuardar;
        private Boton.Secundario btnCancelar;

        public FrmPagos()
        {
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
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 65F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));

            var lblTitulo = new Texto.Titulo("Realizar Pago");

            txtMontoAPagar = new CampoEtiquetado("Monto a Pagar", "Ingrese el monto a pagar");
            
            var lblFormaPago = new Texto.Etiqueta("Forma de Pago");
            cmbFormaPago = new ComboBox();
            cmbFormaPago.Dock = DockStyle.Fill;
            cmbFormaPago.Margin = new Padding(0);
            cmbFormaPago.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFormaPago.Items.Add("Efectivo");
            cmbFormaPago.Items.Add("YAPE");
            cmbFormaPago.Items.Add("PLIN");


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
            tlpFormulario.Controls.Add(lblFormaPago, 0, 2);
            tlpFormulario.Controls.Add(cmbFormaPago, 0, 4);
            tlpFormulario.Controls.Add(txtMontoAPagar, 0, 5);
            tlpFormulario.Controls.Add(tlpBotones, 0, 7);

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
        }
    }
}
