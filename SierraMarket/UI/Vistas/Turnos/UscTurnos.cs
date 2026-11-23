using SierraMarket.Estructuras.Listas;
using SierraMarket.Modelos;
using System;
using System.Windows.Forms;

namespace SierraMarket.UI.Vistas.Turnos
{
    public partial class UscTurnos : UserControl
    {
        private ListaCircular<Trabajador> _listaTrabajadores;
        private Trabajador? _trabajadorEnEdicion = null;

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
                if (_listaTrabajadores.Count <= 1)
                {
                    MessageBox.Show(
                        "No es posible eliminar el último turno.\nEl sistema requiere como mínimo un trabajador activo para operar la caja y registrar ventas.",
                        "Operación no permitida",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                var trabajador = dgvTrabajadores.Rows[e.RowIndex].DataBoundItem as Trabajador;

                if (DialogResult.Yes == MessageBox.Show("¿Está seguro de que desea eliminar el turno de " + trabajador?.Nombres + "?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    if (trabajador != null)
                    {
                        if (_trabajadorEnEdicion == trabajador)
                        {
                            LimpiarFormulario();
                        }
                        _listaTrabajadores.EliminarPorDato(trabajador);
                        ActualizarTrabajadores();
                        OnTrabajadorCambiado?.Invoke(_listaTrabajadores.ObtenerActual());
                    }
                }
            }
            else if (e.RowIndex >= 0 && dgvTrabajadores.Columns[e.ColumnIndex].Name == "btnEditar")
            {
                var trabajador = dgvTrabajadores.Rows[e.RowIndex].DataBoundItem as Trabajador;

                if (trabajador != null)
                {
                    CargarParaEditar(trabajador);
                }
            }
        }

        private void CargarParaEditar(Trabajador trabajador)
        {
            _trabajadorEnEdicion = trabajador;
            lblFormularioTitulo.Text = "Editar Trabajador #" + trabajador.IdTrabajador;
            txtNombres.Text = trabajador.Nombres;
            txtTurnoInicio.Text = trabajador.HoraInicioTurno.ToString("HH:mm");
            txtTurnoFin.Text = trabajador.HoraFinTurno.ToString("HH:mm");
            btnGuardar.Text = "Actualizar";
            btnLimpiar.Text = "Cancelar";
            txtNombres.Focus();
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            string nombres = txtNombres.Text.Trim();
            string strInicio = txtTurnoInicio.Text.Trim();
            string strFin = txtTurnoFin.Text.Trim();

            if (string.IsNullOrEmpty(nombres))
            {
                MessageBox.Show("Ingrese un nombre para continuar.", "Nombre requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombres.Focus();
                return;
            }

            if (!TimeOnly.TryParse(strInicio, out TimeOnly turnoInicio))
            {
                MessageBox.Show("El formato de la hora de inicio es inválido. Use el formato HH:mm (ej. 08:30).", "Hora inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTurnoInicio.Focus();
                return;
            }

            if (!TimeOnly.TryParse(strFin, out TimeOnly turnoFin))
            {
                MessageBox.Show("El formato de la hora de fin es inválido. Use el formato HH:mm (ej. 17:00).", "Hora inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTurnoFin.Focus();
                return;
            }

            if (turnoInicio == turnoFin)
            {
                MessageBox.Show("La hora de inicio y fin del turno no pueden ser iguales.", "Horas iguales", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_trabajadorEnEdicion != null)
            {
                _trabajadorEnEdicion.Nombres = nombres;
                _trabajadorEnEdicion.HoraInicioTurno = turnoInicio;
                _trabajadorEnEdicion.HoraFinTurno = turnoFin;
                MessageBox.Show("El turno se actualizó correctamente.", "Actualización exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                int nuevoId = _listaTrabajadores.Count + 1;
                var nuevoTurno = new Trabajador(nuevoId, nombres, turnoInicio, turnoFin);
                _listaTrabajadores.Agregar(nuevoTurno);
                MessageBox.Show("Turno registrado correctamente.", "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            LimpiarFormulario();
            ActualizarTrabajadores();
        }

        private void BtnLimpiar_Click(object? sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            _trabajadorEnEdicion = null;
            lblFormularioTitulo.Text = "Agregar Trabajador";
            txtNombres.Text = string.Empty;
            txtTurnoInicio.Text = string.Empty;
            txtTurnoFin.Text = string.Empty;
            btnGuardar.Text = "Guardar";
            btnLimpiar.Text = "Limpiar";
            txtNombres.Focus();
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

        private void BtnSiguienteTurno_Click(object? sender, EventArgs e)
        {
            Trabajador trabajadorActual = _listaTrabajadores.Siguiente();
            lblTurnoActual.Text = trabajadorActual != null ? trabajadorActual.Nombres : "-";
            OnTrabajadorCambiado?.Invoke(trabajadorActual);
        }

        private void BtnAnteriorTurno_Click(object? sender, EventArgs e)
        {
            Trabajador trabajadorActual = _listaTrabajadores.Anterior();
            lblTurnoActual.Text = trabajadorActual != null ? trabajadorActual.Nombres : "-";
            OnTrabajadorCambiado?.Invoke(trabajadorActual);
        }

        public event Action<Trabajador> OnTrabajadorCambiado;
    }
}