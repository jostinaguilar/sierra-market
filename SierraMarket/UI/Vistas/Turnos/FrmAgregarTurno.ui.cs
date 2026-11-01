using SierraMarket.UI.Componentes;
using SierraMarket.UI.Tema;
using System.Drawing;
using System.Windows.Forms;

namespace SierraMarket.UI.Vistas.Turnos
{
    public partial class FrmAgregarTurno : Form
    {
        // TextBox nativo para el nombre y selectores
        private TextBox txtNombres;
        private SelectorFechaHoraEtiquetado TurnoInicio;
        private SelectorFechaHoraEtiquetado TurnoFin;
        private Boton.Primario btnGuardar;
        private Boton.Secundario btnCancelar;

        private void ConfigurarVentana()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.AutoSize = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Turnos | Sierra Market";
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

            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 65F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 65F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 65F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));

            var pnlNombres = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, Margin = new Padding(0, 0, 0, 10) };
            pnlNombres.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            pnlNombres.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            pnlNombres.Controls.Add(new Texto.Etiqueta("Nombres"), 0, 0);
            txtNombres = new TextBox
            {
                Dock = DockStyle.Fill,
                PlaceholderText = "Ingrese los nombres",
                BorderStyle = BorderStyle.FixedSingle,
                Font = Fuentes.GeistRegular(10F)
            };
            pnlNombres.Controls.Add(txtNombres, 0, 1);

            TurnoInicio = new SelectorFechaHoraEtiquetado("Turno Inicio");
            TurnoFin = new SelectorFechaHoraEtiquetado("Turno Fin");

            string titulo = "Agregar Trabajador";

            if (_trabajadorEditar != null)
            {
                titulo = "Editar Trabajador";
                txtNombres.Text = _trabajadorEditar.Nombres;
                TurnoInicio.Selector.Text = _trabajadorEditar.HoraInicioTurno.ToString();
                TurnoFin.Selector.Text = _trabajadorEditar.HoraFinTurno.ToString();
            }

            var lblTitulo = new Texto.Titulo(titulo);

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
            tlpFormulario.Controls.Add(pnlNombres, 0, 2);
            tlpFormulario.Controls.Add(TurnoInicio, 0, 3);
            tlpFormulario.Controls.Add(TurnoFin, 0, 4);
            tlpFormulario.Controls.Add(tlpBotones, 0, 6);

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
    }
}
