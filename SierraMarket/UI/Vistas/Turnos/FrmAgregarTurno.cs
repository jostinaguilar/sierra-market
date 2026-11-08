using SierraMarket.Estructuras.Listas;
using SierraMarket.Modelos;
using System;
using System.Windows.Forms;

namespace SierraMarket.UI.Vistas.Turnos
{
    public partial class FrmAgregarTurno : Form
    {
        private ListaCircular<Trabajador> _listaTrabajadores;
        private Trabajador _trabajadorEditar;

        public FrmAgregarTurno(ListaCircular<Trabajador> listaTrabajadores, Trabajador? trabajadorEditar = null)
        {
            _listaTrabajadores = listaTrabajadores;
            _trabajadorEditar = trabajadorEditar;

            ConfigurarVentana();
            CrearFormulario();
        }

        public void BtnGuardar_Click(object sender, EventArgs e)
        {
            int id;
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

            if (_trabajadorEditar != null)
            {
                _trabajadorEditar.Nombres = nombres;
                _trabajadorEditar.HoraInicioTurno = turnoInicio;
                _trabajadorEditar.HoraFinTurno = turnoFin;

                MessageBox.Show("El turno se actualizó correctamente.", "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                id = _listaTrabajadores.Count + 1;

                var nuevoTurno = new Trabajador(id, nombres, turnoInicio, turnoFin);

                _listaTrabajadores.Agregar(nuevoTurno);

                MessageBox.Show("El turno se registró correctamente.", "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
