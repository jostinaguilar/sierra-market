using SierraMarket.UI.Componentes;
using SierraMarket.UI.Tema;
using System.Drawing;
using System.Windows.Forms;

namespace SierraMarket.UI.Vistas.Turnos
{
    public partial class FrmAgregarTurno : Form
    {
        // TextBoxes nativos para el formulario
        private TextBox txtNombres;
        private TextBox txtTurnoInicio;
        private TextBox txtTurnoFin;
        private Boton.Primario btnGuardar;
        private Boton.Secundario btnCancelar;

        private void ConfigurarVentana()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.AutoSize = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Turnos | Sierra Market";
            this.BackColor = Color.FromArgb(255, 255, 255);
        }

        private void CrearFormulario()
        {
            var tlpFormulario = new TableLayoutPanel();
            tlpFormulario.Margin = new Padding(0);
            tlpFormulario.Padding = new Padding(0);
            tlpFormulario.ColumnCount = 1;
            tlpFormulario.RowCount = 18;
            tlpFormulario.Dock = DockStyle.Fill;

            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));  // 0: Titulo
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));   // 1: Separador
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));  // 2: Etiqueta Nombres
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));   // 3: Espacio
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));   // 4: Separador
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));  // 5: txtNombres
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));   // 6: Separador
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));  // 7: Etiqueta Turno Inicio
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));   // 8: Espacio
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));   // 9: Separador
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));  // 10: Selector Turno Inicio
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));   // 11: Separador
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));  // 12: Etiqueta Turno Fin
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));   // 13: Espacio
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));   // 14: Separador
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));  // 15: Selector Turno Fin
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));   // 16: Separador
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));  // 17: Barra de botones

            string titulo = "Agregar Trabajador";

            // Campo Nombres
            var lblNombres = new Texto.Etiqueta("Nombres", Peso.Semibold);
            lblNombres.Padding = new Padding(20, 8, 20, 0);

            txtNombres = new TextBox
            {
                Dock = DockStyle.Fill,
                PlaceholderText = "Ingrese los nombres",
                BorderStyle = BorderStyle.None,
                Font = Fuentes.GeistRegular(10F),
                Margin = new Padding(20, 10, 20, 10)
            };

            // Campos de Turno (TextBoxes sin borde con formato HH:mm)
            var lblTurnoInicio = new Texto.Etiqueta("Turno Inicio (HH:mm)", Peso.Semibold);
            lblTurnoInicio.Padding = new Padding(20, 8, 20, 0);

            txtTurnoInicio = new TextBox
            {
                Dock = DockStyle.Fill,
                PlaceholderText = "Ej. 08:00",
                BorderStyle = BorderStyle.None,
                Font = Fuentes.GeistRegular(10F),
                Margin = new Padding(20, 10, 20, 10)
            };

            var lblTurnoFin = new Texto.Etiqueta("Turno Fin (HH:mm)", Peso.Semibold);
            lblTurnoFin.Padding = new Padding(20, 8, 20, 0);

            txtTurnoFin = new TextBox
            {
                Dock = DockStyle.Fill,
                PlaceholderText = "Ej. 16:00",
                BorderStyle = BorderStyle.None,
                Font = Fuentes.GeistRegular(10F),
                Margin = new Padding(20, 10, 20, 10)
            };

            if (_trabajadorEditar != null)
            {
                titulo = "Editar Trabajador";
                txtNombres.Text = _trabajadorEditar.Nombres;
                txtTurnoInicio.Text = _trabajadorEditar.HoraInicioTurno.ToString("HH:mm");
                txtTurnoFin.Text = _trabajadorEditar.HoraFinTurno.ToString("HH:mm");
            }

            var lblTitulo = new Texto.Titulo(titulo);
            lblTitulo.BackColor = Colores.Fondo;
            lblTitulo.Dock = DockStyle.Fill;
            lblTitulo.Padding = new Padding(20, 18, 20, 15);

            var tlpBotones = new TableLayoutPanel();
            tlpBotones.Margin = new Padding(0);
            tlpBotones.Padding = new Padding(20);
            tlpBotones.Dock = DockStyle.Fill;
            tlpBotones.RowCount = 1;
            tlpBotones.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpBotones.ColumnCount = 3;
            tlpBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            tlpBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpBotones.BackColor = Colores.Fondo;

            btnCancelar = new Boton.Secundario("Cancelar");
            btnCancelar.Dock = DockStyle.Fill;

            btnGuardar = new Boton.Primario("Guardar");
            btnGuardar.Dock = DockStyle.Fill;
            btnGuardar.Click += BtnGuardar_Click;

            tlpBotones.Controls.Add(btnCancelar, 0, 0);
            tlpBotones.Controls.Add(btnGuardar, 2, 0);

            tlpFormulario.Controls.Add(lblTitulo, 0, 0);
            tlpFormulario.Controls.Add(new SeparadorHorizontal(), 0, 1);
            tlpFormulario.Controls.Add(lblNombres, 0, 2);
            tlpFormulario.Controls.Add(new SeparadorHorizontal(), 0, 4);
            tlpFormulario.Controls.Add(txtNombres, 0, 5);
            tlpFormulario.Controls.Add(new SeparadorHorizontal(), 0, 6);
            tlpFormulario.Controls.Add(lblTurnoInicio, 0, 7);
            tlpFormulario.Controls.Add(new SeparadorHorizontal(), 0, 9);
            tlpFormulario.Controls.Add(txtTurnoInicio, 0, 10);
            tlpFormulario.Controls.Add(new SeparadorHorizontal(), 0, 11);
            tlpFormulario.Controls.Add(lblTurnoFin, 0, 12);
            tlpFormulario.Controls.Add(new SeparadorHorizontal(), 0, 14);
            tlpFormulario.Controls.Add(txtTurnoFin, 0, 15);
            tlpFormulario.Controls.Add(new SeparadorHorizontal(), 0, 16);
            tlpFormulario.Controls.Add(tlpBotones, 0, 17);

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
