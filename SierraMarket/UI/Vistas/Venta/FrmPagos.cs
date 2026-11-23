using SierraMarket.Estructuras.Pilas;
using SierraMarket.Modelos;
using System;
using System.Windows.Forms;

namespace SierraMarket.UI.Vistas.Venta
{
    public partial class FrmPagos : Form
    {
        private Pila<Pago> _pilaPagos;
        private decimal _total;

        public Pila<Pago> PilaPagos => _pilaPagos;
        public bool EncolarSolicitado { get; private set; } = false;

        public FrmPagos(decimal total, Pila<Pago>? pagosExistentes = null)
        {
            _total = total;
            _pilaPagos = pagosExistentes ?? new Pila<Pago>();

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

        public void BtnEncolar_Click(object sender, EventArgs e)
        {
            // Validamos que se quiera encolar porque hay saldo pendiente
            decimal totalAbonado = 0;
            foreach (var pago in _pilaPagos)
            {
                totalAbonado += pago.Monto;
            }

            if (totalAbonado >= _total)
            {
                MessageBox.Show("La venta ya está totalmente pagada. Puede presionar 'Guardar'.", "Pago completo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirmacion = MessageBox.Show(
                "¿Desea poner esta venta en la Cola de Espera (ej. reintento de billetera digital)?\nEl cliente se encolará y la caja quedará libre para el siguiente.",
                "Poner en Espera",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                EncolarSolicitado = true;
                this.DialogResult = DialogResult.Retry; // Indicador de que va a la cola
                this.Close();
            }
        }
    }
}
