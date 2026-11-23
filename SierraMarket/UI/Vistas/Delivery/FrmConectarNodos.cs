using SierraMarket.Estructuras.Grafos;
using SierraMarket.Modelos;
using SierraMarket.UI.Componentes;
using SierraMarket.UI.Tema;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace SierraMarket.UI.Vistas.Delivery
{
    public partial class FrmConectarNodos : Form
    {
        public double DistanciaKm { get; private set; }

        private TextBox txtDistancia;
        private Boton.Primario btnConectar;
        private Boton.Secundario btnCancelar;

        public FrmConectarNodos(Vertice<PuntoEntrega> nodo1, Vertice<PuntoEntrega> nodo2)
        {
            ConfigurarVentana();
            CrearFormulario(nodo1, nodo2);
        }

        private void ConfigurarVentana()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.AutoSize = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Conectar Puntos (Crear Calle)";
            this.BackColor = Color.FromArgb(255, 255, 255);
        }

        private void CrearFormulario(Vertice<PuntoEntrega> nodo1, Vertice<PuntoEntrega> nodo2)
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
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F)); // 2: Puntos a conectar
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 12F)); // 3: Espacio
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F)); // 4: Label Distancia
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F)); // 5: TextBox Distancia
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 18F)); // 6: Espacio
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F)); // 7: Botones

            var lblTitulo = new Texto.Subtitulo("Trazar Tramo Vial (Grafo No Dirigido)");
            lblTitulo.Dock = DockStyle.Fill;

            var lblPuntos = new Texto.Etiqueta($"Conectar calle: {nodo1.Nombre} ↔ {nodo2.Nombre}");
            lblPuntos.Dock = DockStyle.Fill;
            lblPuntos.ForeColor = Color.FromArgb(37, 99, 235); // Azul
            lblPuntos.Font = Fuentes.Geist(9.5F, Peso.Semibold);

            var lblDistancia = new Texto.Etiqueta("Distancia del tramo (en Kilómetros):");
            lblDistancia.Dock = DockStyle.Fill;

            txtDistancia = new TextBox
            {
                Dock = DockStyle.Fill,
                PlaceholderText = "Ej. 2.5",
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

            btnConectar = new Boton.Primario("Conectar Calle");
            btnConectar.Dock = DockStyle.Fill;
            btnConectar.AutoSize = false;
            btnConectar.Padding = new Padding(0);
            btnConectar.TextAlign = ContentAlignment.MiddleCenter;
            btnConectar.Font = Fuentes.Geist(9.5F, Peso.Bold);
            btnConectar.Click += BtnConectar_Click;

            tlpBotones.Controls.Add(btnCancelar, 0, 0);
            tlpBotones.Controls.Add(btnConectar, 2, 0);

            tlp.Controls.Add(lblTitulo, 0, 0);
            tlp.Controls.Add(new Panel(), 0, 1);
            tlp.Controls.Add(lblPuntos, 0, 2);
            tlp.Controls.Add(new Panel(), 0, 3);
            tlp.Controls.Add(lblDistancia, 0, 4);
            tlp.Controls.Add(txtDistancia, 0, 5);
            tlp.Controls.Add(new Panel(), 0, 6);
            tlp.Controls.Add(tlpBotones, 0, 7);

            this.Controls.Add(tlp);
            this.ClientSize = new Size(490, 260);
            this.AcceptButton = btnConectar;
            this.CancelButton = btnCancelar;
        }

        private void BtnConectar_Click(object? sender, EventArgs e)
        {
            string distTexto = txtDistancia.Text.Trim().Replace(',', '.');
            if (!double.TryParse(distTexto, NumberStyles.Any, CultureInfo.InvariantCulture, out double dist) || dist <= 0)
            {
                MessageBox.Show("Ingrese una distancia válida en km mayor a 0 (ej. 2.5).", "Distancia inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDistancia.Focus();
                return;
            }

            DistanciaKm = Math.Round(dist, 1);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
