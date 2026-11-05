using SierraMarket.UI.Componentes;
using SierraMarket.UI.Tema;
using System.Drawing;
using System.Windows.Forms;

namespace SierraMarket.UI.Vistas.Turnos
{
    public partial class UscTurnos : UserControl
    {
        private DataGridView dgvTrabajadores;
        private Texto.Etiqueta lblTurnoActual;
        private Boton.Primario btnSiguienteTurno;
        private Boton.Secundario btnAnteriorTurno;

        private void ConstruirInterfaz()
        {
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

            dgvTrabajadores.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Código", DataPropertyName = "IdTrabajador", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dgvTrabajadores.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nombres Completos", DataPropertyName = "Nombres", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvTrabajadores.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Hora Inicio", DataPropertyName = "HoraInicioTurno", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dgvTrabajadores.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Hora Fin", DataPropertyName = "HoraFinTurno", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dgvTrabajadores.Columns.Add(new DataGridViewButtonColumn { Name = "btnEditar", HeaderText = "", Text = "Editar", UseColumnTextForButtonValue = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dgvTrabajadores.Columns.Add(new DataGridViewButtonColumn { Name = "btnEliminar", HeaderText = "", Text = "Eliminar", UseColumnTextForButtonValue = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dgvTrabajadores.CellContentClick += DgvTrabajadores_CellContentClick;
            dgvTrabajadores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            EstiloGrid.Aplicar(dgvTrabajadores);

            tlpTurno.Controls.Add(pnlTitulo, 0, 0);
            tlpTurno.Controls.Add(pnlTurnoActivo, 0, 2);
            tlpTurno.Controls.Add(dgvTrabajadores, 0, 4);

            this.Controls.Add(tlpTurno);
        }
    }
}
