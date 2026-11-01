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
            TimeOnly turnoInicio = TimeOnly.Parse(TurnoInicio.Valor.ToString());
            TimeOnly turnoFin = TimeOnly.Parse(TurnoFin.Valor.ToString());

            if (string.IsNullOrEmpty(nombres))
            {
                MessageBox.Show("Ingrese un nombre para continuar.", "Nombre requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
