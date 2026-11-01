using SierraMarket.Estructuras.Pilas;
using SierraMarket.Modelos;
using System;
using System.Windows.Forms;

namespace SierraMarket.UI.Vistas.Venta
{
    public partial class FrmPagos : Form
    {
        private Pila<Pago> _pilaPagos = new Pila<Pago>();
        private decimal _total;

        public Pila<Pago> PilaPagos => _pilaPagos;

        public FrmPagos(decimal total)
        {
            _total = total;

            ConfigurarVentana();
            CrearFormulario();
        }

        public void BtnPush_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbFormaPago.Text))
            {
                MessageBox.Show("Seleccione una forma de pago.", "Forma de pago requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtMontoAPagar.Text.Trim(), out decimal monto) || monto <= 0)
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
            //txtMontoAPagar.Clear();
        }

        public void BtnPop_Click(object sender, EventArgs e)
        {
            if (!_pilaPagos.EstaVacia)
            {
                _pilaPagos.Pop();
                ActualizarListaPagos();
            }
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
