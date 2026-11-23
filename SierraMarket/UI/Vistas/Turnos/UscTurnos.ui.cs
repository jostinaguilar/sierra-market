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

        // Campos del formulario lateral
        private Texto.Subtitulo lblFormularioTitulo;
        private TextBox txtNombres;
        private TextBox txtTurnoInicio;
        private TextBox txtTurnoFin;
        private Boton.Primario btnGuardar;
        private Boton.Secundario btnLimpiar;

        private void ConstruirInterfaz()
        {
            var tlpContenedor = new TableLayoutPanel();
            tlpContenedor.Dock = DockStyle.Fill;
            tlpContenedor.Margin = new Padding(0);
            tlpContenedor.Padding = new Padding(0);
            tlpContenedor.ColumnCount = 3;
            tlpContenedor.RowCount = 1;
            tlpContenedor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tlpContenedor.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 1F));
            tlpContenedor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));

            // PANEL IZQUIERDO: Gestión de Turnos (Tabla y Turno Activo)
            var tlpTurno = new TableLayoutPanel();
            tlpTurno.Dock = DockStyle.Fill;
            tlpTurno.RowCount = 6;
            tlpTurno.ColumnCount = 1;
            tlpTurno.Margin = new Padding(0);
            tlpTurno.Padding = new Padding(0, 20, 0, 20);

            tlpTurno.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F)); // 0: Titulo
            tlpTurno.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F)); // 1: Espacio
            tlpTurno.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));  // 2: Separador
            tlpTurno.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F)); // 3: Turno activo
            tlpTurno.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));  // 4: Separador
            tlpTurno.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // 5: Grilla

            var lblTitulo = new Texto.Subtitulo("Gestión de Turnos");
            lblTitulo.Dock = DockStyle.Fill;
            lblTitulo.Margin = new Padding(20, 0, 20, 0);

            var pnlTurnoActivo = new FlowLayoutPanel();
            pnlTurnoActivo.Dock = DockStyle.Fill;
            pnlTurnoActivo.Margin = new Padding(20, 0, 20, 0);
            pnlTurnoActivo.FlowDirection = FlowDirection.LeftToRight;
            pnlTurnoActivo.WrapContents = false;
            pnlTurnoActivo.Padding = new Padding(0);

            var lblTurnoActivo = new Texto.Etiqueta("Turno activo: ");
            lblTurnoActivo.Padding = new Padding(0, 11, 0, 0);

            lblTurnoActual = new Texto.Etiqueta("-", Peso.Semibold);
            lblTurnoActual.Padding = new Padding(0, 11, 20, 0);

            btnAnteriorTurno = new Boton.Secundario("Anterior");
            btnAnteriorTurno.Height = 40;
            btnAnteriorTurno.Click += BtnAnteriorTurno_Click;

            btnSiguienteTurno = new Boton.Primario("Siguiente");
            btnSiguienteTurno.Height = 40;
            btnSiguienteTurno.Click += BtnSiguienteTurno_Click;

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
            dgvTrabajadores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvTrabajadores.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Código", DataPropertyName = "IdTrabajador", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells, DefaultCellStyle = new DataGridViewCellStyle { Padding = new Padding(20, 0, 20, 0) } });
            dgvTrabajadores.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nombres Completos", DataPropertyName = "Nombres", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvTrabajadores.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Hora Inicio", DataPropertyName = "HoraInicioTurno", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dgvTrabajadores.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Hora Fin", DataPropertyName = "HoraFinTurno", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dgvTrabajadores.Columns.Add(new DataGridViewButtonColumn { Name = "btnEditar", HeaderText = "", Text = "Editar", UseColumnTextForButtonValue = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dgvTrabajadores.Columns.Add(new DataGridViewButtonColumn { Name = "btnEliminar", HeaderText = "", Text = "Eliminar", UseColumnTextForButtonValue = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dgvTrabajadores.CellContentClick += DgvTrabajadores_CellContentClick;
            EstiloGrid.Aplicar(dgvTrabajadores);

            tlpTurno.Controls.Add(lblTitulo, 0, 0);
            tlpTurno.Controls.Add(new SeparadorHorizontal(), 0, 2);
            tlpTurno.Controls.Add(pnlTurnoActivo, 0, 3);
            tlpTurno.Controls.Add(new SeparadorHorizontal(), 0, 4);
            tlpTurno.Controls.Add(dgvTrabajadores, 0, 5);

            // PANEL DERECHO: Formulario lateral (Agregar / Editar)
            var pnlFormulario = new Panel();
            pnlFormulario.Dock = DockStyle.Fill;
            pnlFormulario.Margin = new Padding(0);
            pnlFormulario.Padding = new Padding(20);
            pnlFormulario.BackColor = Color.FromArgb(250, 250, 250);

            var tlpFormulario = new TableLayoutPanel();
            tlpFormulario.Dock = DockStyle.Fill;
            tlpFormulario.ColumnCount = 1;
            tlpFormulario.RowCount = 16;
            tlpFormulario.Margin = new Padding(0);
            tlpFormulario.Padding = new Padding(0);

            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));  // 0: Titulo
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));  // 1: Espacio
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));   // 2: Separador
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));  // 3: Etiqueta Nombres
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));   // 4: Separador
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));  // 5: txtNombres
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));   // 6: Separador
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));  // 7: Etiqueta Turno Inicio
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));   // 8: Separador
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));  // 9: txtTurno Inicio
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));   // 10: Separador
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));  // 11: Etiqueta Turno Fin
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));   // 12: Separador
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));  // 13: txtTurno Fin
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));   // 14: Separador
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));  // 15: Espacio flexible
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));  // 16: Espacio
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));  // 17: Boton Guardar

            lblFormularioTitulo = new Texto.Subtitulo("Agregar Trabajador");
            lblFormularioTitulo.Dock = DockStyle.Fill;
            lblFormularioTitulo.Margin = new Padding(0);

            var lblNombres = new Texto.Etiqueta("Nombres", Peso.Semibold);
            lblNombres.Padding = new Padding(0, 6, 0, 0);

            txtNombres = new TextBox
            {
                Dock = DockStyle.Fill,
                PlaceholderText = "Ingrese los nombres...",
                BorderStyle = BorderStyle.None,
                Font = Fuentes.GeistRegular(10F),
                Margin = new Padding(5, 10, 5, 10),
                BackColor = Color.White
            };
            var pnlNombresContenedor = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
                Padding = new Padding(10),
                BackColor = Color.White
            };
            pnlNombresContenedor.Controls.Add(txtNombres);

            var lblTurnoInicio = new Texto.Etiqueta("Turno Inicio (HH:mm)", Peso.Semibold);
            lblTurnoInicio.Padding = new Padding(0, 6, 0, 0);

            txtTurnoInicio = new TextBox
            {
                Dock = DockStyle.Fill,
                PlaceholderText = "Ej. 08:00",
                BorderStyle = BorderStyle.None,
                Font = Fuentes.GeistRegular(10F),
                Margin = new Padding(5, 10, 5, 10),
                BackColor = Color.White
            };
            var pnlTurnoInicioContenedor = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
                Padding = new Padding(10),
                BackColor = Color.White
            };
            pnlTurnoInicioContenedor.Controls.Add(txtTurnoInicio);

            var lblTurnoFin = new Texto.Etiqueta("Turno Fin (HH:mm)", Peso.Semibold);
            lblTurnoFin.Padding = new Padding(0, 6, 0, 0);

            txtTurnoFin = new TextBox
            {
                Dock = DockStyle.Fill,
                PlaceholderText = "Ej. 16:00",
                BorderStyle = BorderStyle.None,
                Font = Fuentes.GeistRegular(10F),
                Margin = new Padding(5, 10, 5, 10),
                BackColor = Color.White
            };
            var pnlTurnoFinContenedor = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
                Padding = new Padding(10),
                BackColor = Color.White
            };
            pnlTurnoFinContenedor.Controls.Add(txtTurnoFin);

            btnGuardar = new Boton.Primario("Guardar");
            btnGuardar.Dock = DockStyle.Fill;
            btnGuardar.Click += BtnGuardar_Click;

            btnLimpiar = new Boton.Secundario("Limpiar");
            btnLimpiar.Dock = DockStyle.Fill;
            btnLimpiar.Click += BtnLimpiar_Click;

            tlpFormulario.Controls.Add(lblFormularioTitulo, 0, 0);
            tlpFormulario.Controls.Add(lblNombres, 0, 3);
            tlpFormulario.Controls.Add(new SeparadorHorizontal(), 0, 4);
            tlpFormulario.Controls.Add(pnlNombresContenedor, 0, 5);
            tlpFormulario.Controls.Add(new SeparadorHorizontal(), 0, 6);
            tlpFormulario.Controls.Add(lblTurnoInicio, 0, 7);
            tlpFormulario.Controls.Add(new SeparadorHorizontal(), 0, 8);
            tlpFormulario.Controls.Add(pnlTurnoInicioContenedor, 0, 9);
            tlpFormulario.Controls.Add(new SeparadorHorizontal(), 0, 10);
            tlpFormulario.Controls.Add(lblTurnoFin, 0, 11);
            tlpFormulario.Controls.Add(new SeparadorHorizontal(), 0, 12);
            tlpFormulario.Controls.Add(pnlTurnoFinContenedor, 0, 13);
            tlpFormulario.Controls.Add(new SeparadorHorizontal(), 0, 14);
            tlpFormulario.Controls.Add(btnGuardar, 0, 18);

            pnlFormulario.Controls.Add(tlpFormulario);

            tlpContenedor.Controls.Add(tlpTurno, 0, 0);
            tlpContenedor.Controls.Add(new SeparadorHorizontal { Dock = DockStyle.Fill, Width = 1 }, 1, 0);
            tlpContenedor.Controls.Add(pnlFormulario, 2, 0);

            this.Controls.Add(tlpContenedor);
        }
    }
}
