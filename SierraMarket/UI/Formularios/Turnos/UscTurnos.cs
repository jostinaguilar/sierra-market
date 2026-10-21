using SierraMarket.Estructuras.Listas;
using SierraMarket.Modelos;
using SierraMarket.UI.Componentes;
using SierraMarket.UI.Formularios.Inventario;
using SierraMarket.UI.Tema;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SierraMarket.UI.Formularios.Turnos
{
    public partial class UscTurnos : UserControl
    {
        private readonly ListaCircular<Trabajador> _listaTrabajadores;
        private DataGridView dgvTrabajadores;

        private Texto.Etiqueta lblTurnoActual;
        private Boton.Primario btnSiguienteTurno;
        private Boton.Secundario btnAnteriorTurno;

        public UscTurnos(ListaCircular<Trabajador> listaTrabajadores)
        {
            InitializeComponent();

            _listaTrabajadores = listaTrabajadores;

            var tlpTurno = new TableLayoutPanel();
            tlpTurno.Dock = DockStyle.Fill;
            tlpTurno.RowCount = 5;
            tlpTurno.ColumnCount = 1;
            tlpTurno.Margin = new Padding(0);
            tlpTurno.Padding = new Padding(20);

            tlpTurno.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpTurno.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            tlpTurno.RowStyles.Add(new RowStyle(SizeType.Absolute, 33F));
            tlpTurno.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            tlpTurno.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            var pnlTitulo = new Panel();
            pnlTitulo.Dock = DockStyle.Fill;
            pnlTitulo.Margin = new Padding(0);

            var lblTitulo = new Texto.Subtitulo("Gestión de Turnos");

            lblTitulo.Dock = DockStyle.Left;
            lblTitulo.Margin = new Padding(0);

            var btnAgregarTurno = new Boton.Primario("Agregar turno");
            btnAgregarTurno.Dock = DockStyle.Right;
            btnAgregarTurno.Margin = new Padding(0);
            btnAgregarTurno.Click += BtnAgregarTurno_Click;

            pnlTitulo.Controls.AddRange(new Control[] { lblTitulo, btnAgregarTurno });

            var pnlTurnoActivo = new FlowLayoutPanel();
            pnlTurnoActivo.Dock = DockStyle.Fill;
            pnlTurnoActivo.Margin = new Padding(0);
            pnlTurnoActivo.FlowDirection = FlowDirection.LeftToRight;
            pnlTurnoActivo.WrapContents = false;
            pnlTurnoActivo.Padding = new Padding(0);

            var lblTurnoActivo = new Texto.Etiqueta("Turno activo: ");
            lblTurnoActual = new Texto.Etiqueta("-", Peso.Semibold);
            lblTurnoActual.Height = 40; // Asegura que el label tenga la misma altura que los botones
            btnSiguienteTurno = new Boton.Primario("Siguiente");
            btnSiguienteTurno.Click += BtnSiguienteTurno_Click;
            btnAnteriorTurno = new Boton.Secundario("Anterior");
            btnAnteriorTurno.Click += BtnAnteriorTurno_Click;

            pnlTurnoActivo.Controls.Add(lblTurnoActivo);
            pnlTurnoActivo.Controls.Add(lblTurnoActual);
            pnlTurnoActivo.Controls.Add(btnAnteriorTurno);
            pnlTurnoActivo.Controls.Add(btnSiguienteTurno);

            dgvTrabajadores = new DataGridView();
            dgvTrabajadores.Dock = DockStyle.Fill;
            dgvTrabajadores.Margin = new Padding(0);
            dgvTrabajadores.AutoGenerateColumns = false;
            dgvTrabajadores.AllowUserToAddRows = false;
            dgvTrabajadores.ReadOnly = true;
            dgvTrabajadores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTrabajadores.RowHeadersVisible = false;

            dgvTrabajadores.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Código", DataPropertyName = "IdTrabajador", Width = 80 });
            dgvTrabajadores.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nombres Completos", DataPropertyName = "Nombres", Width = 250 });
            dgvTrabajadores.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Hora Inicio", DataPropertyName = "HoraInicioTurno", Width = 80 });
            dgvTrabajadores.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Hora Fin", DataPropertyName = "HoraFinTurno", Width = 80 });
            dgvTrabajadores.Columns.Add(new DataGridViewButtonColumn { Name = "btnEditar", HeaderText = "", Text = "Editar", UseColumnTextForButtonValue = true, Width = 80 });
            dgvTrabajadores.Columns.Add(new DataGridViewButtonColumn { Name = "btnEliminar", HeaderText = "", Text = "Eliminar", UseColumnTextForButtonValue = true, Width = 80 });
            dgvTrabajadores.CellContentClick += DgvTrabajadores_CellContentClick;

            tlpTurno.Controls.Add(pnlTitulo, 0, 0);
            tlpTurno.Controls.Add(pnlTurnoActivo, 0, 2);
            tlpTurno.Controls.Add(dgvTrabajadores, 0, 4);

            this.Controls.Add(tlpTurno);

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
            }else if (e.RowIndex >= 0 && dgvTrabajadores.Columns[e.ColumnIndex].Name == "btnEditar")
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

        private void ActualizarTrabajadores()
        {
            var trabajadores = _listaTrabajadores.ObtenerTodos();

            dgvTrabajadores.DataSource = null;
            dgvTrabajadores.DataSource = trabajadores;

            Trabajador trabajadorActual = _listaTrabajadores.ObtenerActual();
            lblTurnoActual.Text = trabajadorActual?.Nombres ?? "-";
            OnTrabajadorCambiado?.Invoke(trabajadorActual);
        }

        private void BtnSiguienteTurno_Click(object sender, EventArgs e)
        {
            Trabajador trabajadorActual = _listaTrabajadores.Siguiente();
            lblTurnoActual.Text = trabajadorActual?.Nombres ?? "-";
            OnTrabajadorCambiado?.Invoke(trabajadorActual);
        }

        private void BtnAnteriorTurno_Click(object sender, EventArgs e)
        {
            Trabajador trabajadorActual = _listaTrabajadores.Anterior();
            lblTurnoActual.Text = trabajadorActual?.Nombres ?? "-";
            OnTrabajadorCambiado?.Invoke(trabajadorActual);
        }

        public event Action<Trabajador> OnTrabajadorCambiado;
    }
}