using SierraMarket.Estructuras.Listas;
using SierraMarket.Modelos;
using System;
using System.Windows.Forms;

namespace SierraMarket.UI.Vistas.Turnos
{
    public partial class UscTurnos : UserControl
    {
        private ListaCircular<Trabajador> _listaTrabajadores;

        public UscTurnos(ListaCircular<Trabajador> listaTrabajadores)
        {
            InitializeComponent();

            _listaTrabajadores = listaTrabajadores;

            ConstruirInterfaz();
            ActualizarTrabajadores();
        }

        private void DgvTrabajadores_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvTrabajadores.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                var trabajador = dgvTrabajadores.Rows[e.RowIndex].DataBoundItem as Trabajador;

                if (DialogResult.Yes == MessageBox.Show("¿Está seguro de que desea eliminar el turno de " + trabajador?.Nombres + "?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    _listaTrabajadores.EliminarPorDato(trabajador);
                    ActualizarTrabajadores();
                    OnTrabajadorCambiado?.Invoke(_listaTrabajadores.ObtenerActual());
                }
            }
            else if (e.RowIndex >= 0 && dgvTrabajadores.Columns[e.ColumnIndex].Name == "btnEditar")
            {
                var trabajador = dgvTrabajadores.Rows[e.RowIndex].DataBoundItem as Trabajador;

                if (trabajador != null)
                {
                    var frmEditar = new FrmAgregarTurno(_listaTrabajadores, trabajador);

                    if (frmEditar.ShowDialog() == DialogResult.OK)
                    {
                        ActualizarTrabajadores();
                    }
                }
            }
        }

        private void BtnAgregarTurno_Click(object sender, EventArgs e)
        {
            var frmAgregar = new FrmAgregarTurno(_listaTrabajadores);

            if (frmAgregar.ShowDialog() == DialogResult.OK)
            {
                ActualizarTrabajadores();
            }
        }

        public void ActualizarTrabajadores()
        {
            var trabajadores = _listaTrabajadores.ObtenerTodos();

            dgvTrabajadores.DataSource = null;
            dgvTrabajadores.DataSource = trabajadores;

            Trabajador trabajadorActual = _listaTrabajadores.ObtenerActual();
            lblTurnoActual.Text = trabajadorActual != null ? trabajadorActual.Nombres : "-";
            OnTrabajadorCambiado?.Invoke(trabajadorActual);
        }

        private void BtnSiguienteTurno_Click(object sender, EventArgs e)
        {
            Trabajador trabajadorActual = _listaTrabajadores.Siguiente();
            lblTurnoActual.Text = trabajadorActual != null ? trabajadorActual.Nombres : "-";
            OnTrabajadorCambiado?.Invoke(trabajadorActual);
        }

        private void BtnAnteriorTurno_Click(object sender, EventArgs e)
        {
            Trabajador trabajadorActual = _listaTrabajadores.Anterior();
            lblTurnoActual.Text = trabajadorActual != null ? trabajadorActual.Nombres : "-";
            OnTrabajadorCambiado?.Invoke(trabajadorActual);
        }

        public event Action<Trabajador> OnTrabajadorCambiado;
    }
}