using SierraMarket.UI.Componentes;
using SierraMarket.UI.Tema;
using System.Drawing;
using System.Windows.Forms;

namespace SierraMarket.UI.Vistas.Delivery
{
    public partial class UscDelivery : UserControl
    {
        private Panel pnlMapa;
        private ComboBox cmbOrigen;
        private ComboBox cmbDestino;
        private Boton.Primario btnCalcularRuta;
        private Boton.Secundario btnLimpiarRuta;

        // Tarjeta de resultados
        private Texto.Etiqueta lblDistancia;
        private Texto.Etiqueta lblTiempo;
        private Texto.Etiqueta lblCostoEnvio;
        private Texto.Etiqueta lblRutaRecorrido;
        private Boton.Primario btnDespachar;
        private Texto.Etiqueta lblGuiaMapa;

        private void ConstruirInterfaz()
        {
            var tlpContenedor = new TableLayoutPanel();
            tlpContenedor.Dock = DockStyle.Fill;
            tlpContenedor.Margin = new Padding(0);
            tlpContenedor.Padding = new Padding(0);
            tlpContenedor.ColumnCount = 3;
            tlpContenedor.RowCount = 1;
            tlpContenedor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F)); // Mapa
            tlpContenedor.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 1F));  // Separador
            tlpContenedor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F)); // Panel de Control

            // 1. PANEL IZQUIERDO: Mapa del Grafo
            var tlpIzquierdo = new TableLayoutPanel();
            tlpIzquierdo.Dock = DockStyle.Fill;
            tlpIzquierdo.Margin = new Padding(0);
            tlpIzquierdo.Padding = new Padding(0, 20, 0, 20);
            tlpIzquierdo.RowCount = 5;
            tlpIzquierdo.ColumnCount = 1;
            tlpIzquierdo.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F)); // 0: Titulo
            tlpIzquierdo.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F)); // 1: Guia interactiva
            tlpIzquierdo.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F)); // 2: Espacio
            tlpIzquierdo.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));  // 3: Separador
            tlpIzquierdo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // 4: Canvas Mapa

            var lblTitulo = new Texto.Subtitulo("Rutas de Delivery | Red Urbana (Grafo y Dijkstra)");
            lblTitulo.Dock = DockStyle.Fill;
            lblTitulo.Margin = new Padding(20, 0, 20, 0);

            lblGuiaMapa = new Texto.Etiqueta("💡 Clic en 2 puntos = Conectar calle (km) | Clic en espacio vacío = Crear dirección | Clic derecho = Fijar Origen");
            lblGuiaMapa.Dock = DockStyle.Fill;
            lblGuiaMapa.ForeColor = Color.FromArgb(71, 85, 105);
            lblGuiaMapa.Margin = new Padding(20, 0, 20, 0);

            pnlMapa = new Panel();
            pnlMapa.Dock = DockStyle.Fill;
            pnlMapa.Margin = new Padding(20, 10, 20, 0);
            pnlMapa.BackColor = Color.FromArgb(250, 250, 252);
            pnlMapa.BorderStyle = BorderStyle.FixedSingle;
            pnlMapa.Paint += PnlMapa_Paint;
            pnlMapa.MouseClick += PnlMapa_MouseClick;

            tlpIzquierdo.Controls.Add(lblTitulo, 0, 0);
            tlpIzquierdo.Controls.Add(lblGuiaMapa, 0, 1);
            tlpIzquierdo.Controls.Add(new SeparadorHorizontal(), 0, 3);
            tlpIzquierdo.Controls.Add(pnlMapa, 0, 4);

            // Separador central
            var pnlSeparador = new Panel();
            pnlSeparador.Dock = DockStyle.Fill;
            pnlSeparador.Margin = new Padding(0);
            pnlSeparador.BackColor = Color.FromArgb(228, 228, 231);

            // 2. PANEL DERECHO: Control de Envíos y Cálculos
            var pnlDerecho = new Panel();
            pnlDerecho.Dock = DockStyle.Fill;
            pnlDerecho.Margin = new Padding(0);
            pnlDerecho.Padding = new Padding(20);
            pnlDerecho.BackColor = Color.FromArgb(255, 255, 255);

            var tlpDerecho = new TableLayoutPanel();
            tlpDerecho.Dock = DockStyle.Fill;
            tlpDerecho.ColumnCount = 1;
            tlpDerecho.RowCount = 12;
            tlpDerecho.Margin = new Padding(0);
            tlpDerecho.Padding = new Padding(0);

            tlpDerecho.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F)); // 0: Titulo Panel
            tlpDerecho.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F)); // 1: Espacio
            tlpDerecho.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F)); // 2: Label Origen
            tlpDerecho.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F)); // 3: Cmb Origen
            tlpDerecho.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F)); // 4: Espacio
            tlpDerecho.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F)); // 5: Label Destino
            tlpDerecho.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F)); // 6: Cmb Destino
            tlpDerecho.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F)); // 7: Espacio
            tlpDerecho.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F)); // 8: Botones Calcular/Limpiar
            tlpDerecho.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F)); // 9: Separador
            tlpDerecho.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // 10: Resultados
            tlpDerecho.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F)); // 11: Boton Despachar

            var lblControl = new Texto.Subtitulo("Planificador de Envíos");
            lblControl.Dock = DockStyle.Fill;

            var lblOrigen = new Texto.Etiqueta("Punto de Partida (Origen):");
            cmbOrigen = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList, Font = Fuentes.GeistRegular(9.5F) };

            var lblDestino = new Texto.Etiqueta("Destino de Entrega (Cliente):");
            cmbDestino = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList, Font = Fuentes.GeistRegular(9.5F) };

            var tlpBotones = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 3, RowCount = 1, Margin = new Padding(0) };
            tlpBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tlpBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 8F));
            tlpBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tlpBotones.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            btnCalcularRuta = new Boton.Primario("Calcular Ruta");
            btnCalcularRuta.Dock = DockStyle.Fill;
            btnCalcularRuta.AutoSize = false;
            btnCalcularRuta.Padding = new Padding(0);
            btnCalcularRuta.TextAlign = ContentAlignment.MiddleCenter;
            btnCalcularRuta.Font = Fuentes.Geist(9.5F, Peso.Bold);
            btnCalcularRuta.Click += BtnCalcularRuta_Click;

            btnLimpiarRuta = new Boton.Secundario("Limpiar");
            btnLimpiarRuta.Dock = DockStyle.Fill;
            btnLimpiarRuta.AutoSize = false;
            btnLimpiarRuta.Padding = new Padding(0);
            btnLimpiarRuta.TextAlign = ContentAlignment.MiddleCenter;
            btnLimpiarRuta.Font = Fuentes.Geist(9.5F, Peso.Bold);
            btnLimpiarRuta.Click += BtnLimpiarRuta_Click;

            tlpBotones.Controls.Add(btnCalcularRuta, 0, 0);
            tlpBotones.Controls.Add(btnLimpiarRuta, 2, 0);

            // Panel de Resultados (Tarjeta con borde)
            var pnlResultados = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(248, 249, 250),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(12),
                Margin = new Padding(0, 5, 0, 10)
            };

            var tlpInfo = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 5 };
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            lblDistancia = new Texto.Etiqueta("📏 Distancia Mínima: --");
            lblTiempo = new Texto.Etiqueta("⏱️ Tiempo Estimado: --");
            lblCostoEnvio = new Texto.Etiqueta("💰 Costo de Envío: --");
            lblCostoEnvio.ForeColor = Color.FromArgb(22, 101, 52); // Verde oscuro

            var lblRutaTitulo = new Texto.Etiqueta("📍 Trayecto sugerido (Dijkstra):");
            lblRutaRecorrido = new Texto.Etiqueta("Seleccione un origen y destino para trazar la ruta en el mapa.");
            lblRutaRecorrido.Dock = DockStyle.Fill;
            lblRutaRecorrido.ForeColor = Color.FromArgb(71, 85, 105);

            tlpInfo.Controls.Add(lblDistancia, 0, 0);
            tlpInfo.Controls.Add(lblTiempo, 0, 1);
            tlpInfo.Controls.Add(lblCostoEnvio, 0, 2);
            tlpInfo.Controls.Add(lblRutaTitulo, 0, 3);
            tlpInfo.Controls.Add(lblRutaRecorrido, 0, 4);
            pnlResultados.Controls.Add(tlpInfo);

            btnDespachar = new Boton.Primario("Despachar Pedido");
            btnDespachar.Dock = DockStyle.Fill;
            btnDespachar.AutoSize = false;
            btnDespachar.Padding = new Padding(0);
            btnDespachar.TextAlign = ContentAlignment.MiddleCenter;
            btnDespachar.Font = Fuentes.Geist(9.5F, Peso.Bold);
            btnDespachar.Enabled = false;
            btnDespachar.BackColor = Color.FromArgb(220, 220, 225);
            btnDespachar.ForeColor = Color.FromArgb(140, 140, 145);
            btnDespachar.EnabledChanged += (s, e) =>
            {
                btnDespachar.BackColor = btnDespachar.Enabled ? Colores.Primario : Color.FromArgb(220, 220, 225);
                btnDespachar.ForeColor = btnDespachar.Enabled ? Color.White : Color.FromArgb(140, 140, 145);
            };
            btnDespachar.Click += BtnDespachar_Click;

            tlpDerecho.Controls.Add(lblControl, 0, 0);
            tlpDerecho.Controls.Add(lblOrigen, 0, 2);
            tlpDerecho.Controls.Add(cmbOrigen, 0, 3);
            tlpDerecho.Controls.Add(lblDestino, 0, 5);
            tlpDerecho.Controls.Add(cmbDestino, 0, 6);
            tlpDerecho.Controls.Add(tlpBotones, 0, 8);
            tlpDerecho.Controls.Add(new SeparadorHorizontal(), 0, 9);
            tlpDerecho.Controls.Add(pnlResultados, 0, 10);
            tlpDerecho.Controls.Add(btnDespachar, 0, 11);

            pnlDerecho.Controls.Add(tlpDerecho);

            tlpContenedor.Controls.Add(tlpIzquierdo, 0, 0);
            tlpContenedor.Controls.Add(pnlSeparador, 1, 0);
            tlpContenedor.Controls.Add(pnlDerecho, 2, 0);

            this.Controls.Add(tlpContenedor);
        }
    }
}
