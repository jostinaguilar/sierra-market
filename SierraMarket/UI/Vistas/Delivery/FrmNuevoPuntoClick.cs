using SierraMarket.UI.Componentes;
using SierraMarket.UI.Tema;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SierraMarket.UI.Vistas.Delivery
{
    public partial class FrmNuevoPuntoClick : Form
    {
        public string NombrePunto { get; private set; } = string.Empty;

        private TextBox txtNombre;
        private Boton.Primario btnGuardar;
        private Boton.Secundario btnCancelar;

        public FrmNuevoPuntoClick(int x, int y)
        {
            ConfigurarVentana();
            CrearFormulario(x, y);
        }

        private void ConfigurarVentana()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.AutoSize = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Nueva Dirección de Entrega";
            this.BackColor = Color.FromArgb(255, 255, 255);
        }

        private void CrearFormulario(int x, int y)
        {
            var tlp = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 8,
                Padding = new Padding(24, 20, 24, 20),
                Margin = new Padding(0)
            };
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F)); // 0: Titulo
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 6F));  // 1: Espacio
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F)); // 2: Coordenadas
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 12F)); // 3: Espacio
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F)); // 4: Label Nombre
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F)); // 5: TextBox Nombre
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 18F)); // 6: Espacio
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F)); // 7: Botones

            var lblTitulo = new Texto.Subtitulo("Nueva Dirección de Entrega");
            lblTitulo.Dock = DockStyle.Fill;

            var lblCoord = new Texto.Etiqueta($"Ubicación en mapa: Coordenadas ({x}, {y})");
            lblCoord.Dock = DockStyle.Fill;
            lblCoord.ForeColor = Color.FromArgb(100, 116, 139);

            var lblNombre = new Texto.Etiqueta("Nombre o Dirección de Entrega:");
            lblNombre.Dock = DockStyle.Fill;

            txtNombre = new TextBox
            {
                Dock = DockStyle.Fill,
                PlaceholderText = "Ej. Urb. Las Flores 123",
                BorderStyle = BorderStyle.FixedSingle,
                Font = Fuentes.GeistRegular(10.5F),
                Margin = new Padding(0)
            };

            var tlpBotones = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 3, RowCount = 1, Margin = new Padding(0) };
            tlpBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 12F));
            tlpBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpBotones.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            btnCancelar = new Boton.Secundario("Cancelar");
            btnCancelar.Dock = DockStyle.Fill;
            btnCancelar.AutoSize = false;
            btnCancelar.Padding = new Padding(0);
            btnCancelar.TextAlign = ContentAlignment.MiddleCenter;
            btnCancelar.Font = Fuentes.Geist(9.5F, Peso.Bold);
            btnCancelar.Click += (s, e) => this.Close();

            btnGuardar = new Boton.Primario("Crear Punto");
            btnGuardar.Dock = DockStyle.Fill;
            btnGuardar.AutoSize = false;
            btnGuardar.Padding = new Padding(0);
            btnGuardar.TextAlign = ContentAlignment.MiddleCenter;
            btnGuardar.Font = Fuentes.Geist(9.5F, Peso.Bold);
            btnGuardar.Click += BtnGuardar_Click;

            tlpBotones.Controls.Add(btnCancelar, 0, 0);
            tlpBotones.Controls.Add(btnGuardar, 2, 0);

            tlp.Controls.Add(lblTitulo, 0, 0);
            tlp.Controls.Add(new Panel(), 0, 1);
            tlp.Controls.Add(lblCoord, 0, 2);
            tlp.Controls.Add(new Panel(), 0, 3);
            tlp.Controls.Add(lblNombre, 0, 4);
            tlp.Controls.Add(txtNombre, 0, 5);
            tlp.Controls.Add(new Panel(), 0, 6);
            tlp.Controls.Add(tlpBotones, 0, 7);

            this.Controls.Add(tlp);
            this.ClientSize = new Size(450, 255);
            this.AcceptButton = btnGuardar;
            this.CancelButton = btnCancelar;
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("Ingrese el nombre de la dirección para continuar.", "Nombre requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            NombrePunto = nombre;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
