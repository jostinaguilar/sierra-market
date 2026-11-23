using SierraMarket.Estructuras.Grafos;
using SierraMarket.Modelos;
using SierraMarket.UI.Tema;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SierraMarket.UI.Vistas.Delivery
{
    public partial class UscDelivery : UserControl
    {
        private Grafo<PuntoEntrega> _grafoUrbano;
        private List<Vertice<PuntoEntrega>>? _rutaActual = null;
        private double _distanciaRutaActual = 0;

        public UscDelivery(Grafo<PuntoEntrega>? grafo = null)
        {
            _grafoUrbano = grafo ?? InicializarRedUrbana();

            ConstruirInterfaz();
            CargarPuntosEnSelectores();
        }

        public static Grafo<PuntoEntrega> InicializarRedUrbana()
        {
            var grafo = new Grafo<PuntoEntrega>();

            // Vértices con coordenadas relativas (X, Y) para el canvas del mapa
            // id, nombre, tipo, X, Y
            var v0 = grafo.AgregarVertice(0, "SierraMarket (Central)", new PuntoEntrega(0, "SierraMarket Central", "Central"), 110, 240);
            var v1 = grafo.AgregarVertice(1, "Plaza Central", new PuntoEntrega(1, "Plaza Central", "Comercial"), 270, 150);
            var v2 = grafo.AgregarVertice(2, "Mercado Modelo", new PuntoEntrega(2, "Mercado Modelo", "Comercial"), 270, 330);
            var v3 = grafo.AgregarVertice(3, "Av. El Sol", new PuntoEntrega(3, "Av. El Sol", "Comercial"), 430, 90);
            var v4 = grafo.AgregarVertice(4, "Hospital Regional", new PuntoEntrega(4, "Hospital Regional", "Salud"), 440, 240);
            var v5 = grafo.AgregarVertice(5, "Urb. Santa Rosa", new PuntoEntrega(5, "Urb. Santa Rosa", "Residencial"), 430, 390);
            var v6 = grafo.AgregarVertice(6, "Zona Residencial Norte", new PuntoEntrega(6, "Zona Residencial Norte", "Residencial"), 600, 100);
            var v7 = grafo.AgregarVertice(7, "Urb. Los Pinos", new PuntoEntrega(7, "Urb. Los Pinos", "Residencial"), 600, 240);
            var v8 = grafo.AgregarVertice(8, "Condominios del Sur", new PuntoEntrega(8, "Condominios del Sur", "Residencial"), 600, 380);

            // Aristas con distancias en Kilómetros (Pesos para Dijkstra)
            grafo.AgregarArista(0, 1, 2.2); // Central <-> Plaza
            grafo.AgregarArista(0, 2, 1.8); // Central <-> Mercado
            grafo.AgregarArista(1, 2, 2.5); // Plaza <-> Mercado
            grafo.AgregarArista(1, 3, 3.1); // Plaza <-> Av. El Sol
            grafo.AgregarArista(1, 4, 2.7); // Plaza <-> Hospital
            grafo.AgregarArista(2, 4, 2.4); // Mercado <-> Hospital
            grafo.AgregarArista(2, 5, 2.0); // Mercado <-> Santa Rosa
            grafo.AgregarArista(3, 6, 2.6); // Av. El Sol <-> Residencial Norte
            grafo.AgregarArista(3, 4, 1.9); // Av. El Sol <-> Hospital
            grafo.AgregarArista(4, 7, 2.8); // Hospital <-> Los Pinos
            grafo.AgregarArista(5, 4, 2.1); // Santa Rosa <-> Hospital
            grafo.AgregarArista(5, 8, 3.0); // Santa Rosa <-> Condominios del Sur
            grafo.AgregarArista(6, 7, 2.3); // Norte <-> Los Pinos
            grafo.AgregarArista(7, 8, 2.5); // Los Pinos <-> Condominios del Sur

            return grafo;
        }

        private void CargarPuntosEnSelectores()
        {
            cmbOrigen.Items.Clear();
            cmbDestino.Items.Clear();

            foreach (var vertice in _grafoUrbano.Vertices)
            {
                cmbOrigen.Items.Add(vertice);
                cmbDestino.Items.Add(vertice);
            }

            if (cmbOrigen.Items.Count > 0)
            {
                cmbOrigen.SelectedIndex = 0; // Por defecto SierraMarket Central
            }

            if (cmbDestino.Items.Count > 1)
            {
                cmbDestino.SelectedIndex = cmbDestino.Items.Count - 1; // Por defecto Condominios del Sur
            }
        }

        private void BtnCalcularRuta_Click(object? sender, EventArgs e)
        {
            var origen = cmbOrigen.SelectedItem as Vertice<PuntoEntrega>;
            var destino = cmbDestino.SelectedItem as Vertice<PuntoEntrega>;

            if (origen == null || destino == null)
            {
                MessageBox.Show("Seleccione un origen y un destino válidos.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (origen.Id == destino.Id)
            {
                MessageBox.Show("El punto de origen y destino no pueden ser el mismo.", "Ruta no requerida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Ejecución del Algoritmo de Dijkstra sobre el Grafo Ponderado
            var resultado = _grafoUrbano.Dijkstra(origen.Id, destino.Id);

            if (resultado.ExisteRuta)
            {
                _rutaActual = resultado.Camino;
                _distanciaRutaActual = resultado.DistanciaTotal;

                // Cálculos de negocio: tiempo aproximado (velocidad promedio 20 km/h en moto)
                // Tiempo en minutos = (distancia / velocidad) * 60 + 3 min fijos
                double minutos = Math.Round((_distanciaRutaActual / 20.0) * 60 + 3);

                // Costo sugerido de envío: S/ 4.00 base + S/ 1.20 por km
                decimal costo = 4.00m + (decimal)_distanciaRutaActual * 1.20m;

                lblDistancia.Text = $"📏 Distancia Mínima: {_distanciaRutaActual:0.0} km";
                lblTiempo.Text = $"⏱️ Tiempo Estimado: ~{minutos:0} minutos";
                lblCostoEnvio.Text = $"💰 Costo de Envío: S/ {costo:0.00}";

                var nombres = new List<string>();
                foreach (var paso in _rutaActual)
                {
                    nombres.Add(paso.Nombre);
                }
                lblRutaRecorrido.Text = string.Join(" ➔\n", nombres);

                btnDespachar.Enabled = true;
                pnlMapa.Invalidate(); // Redibujar mapa para iluminar el camino óptimo
            }
            else
            {
                MessageBox.Show("No se encontró una ruta vial conectada hacia el destino seleccionado.", "Sin conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLimpiarRuta_Click(object? sender, EventArgs e)
        {
            _rutaActual = null;
            _distanciaRutaActual = 0;
            lblDistancia.Text = "📏 Distancia Mínima: --";
            lblTiempo.Text = "⏱️ Tiempo Estimado: --";
            lblCostoEnvio.Text = "💰 Costo de Envío: --";
            lblRutaRecorrido.Text = "Seleccione un origen y destino para trazar la ruta en el mapa.";
            btnDespachar.Enabled = false;
            pnlMapa.Invalidate();
        }

        private void BtnDespachar_Click(object? sender, EventArgs e)
        {
            var destino = cmbDestino.SelectedItem as Vertice<PuntoEntrega>;
            MessageBox.Show($"¡Pedido despachado con éxito hacia {destino?.Nombre}!\nRuta óptima enviada al motorizado.", "Despacho Confirmado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            BtnLimpiarRuta_Click(null, EventArgs.Empty);
        }

        private void PnlMapa_MouseClick(object? sender, MouseEventArgs e)
        {
            // Permite seleccionar origen o destino haciendo clic en los nodos del mapa
            foreach (var v in _grafoUrbano.Vertices)
            {
                int radio = 18;
                int dx = e.X - v.X;
                int dy = e.Y - v.Y;

                if (dx * dx + dy * dy <= radio * radio)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        cmbDestino.SelectedItem = v;
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        cmbOrigen.SelectedItem = v;
                    }
                    return;
                }
            }
        }

        private void PnlMapa_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // 1. Dibujar todas las aristas (Calles/Conexiones viales)
            using var penArista = new Pen(Color.FromArgb(203, 213, 225), 2f);
            using var brushPeso = new SolidBrush(Color.FromArgb(100, 116, 139));
            using var fontPeso = Fuentes.Geist(7.5f, Peso.Regular);

            var aristasDibujadas = new HashSet<string>();

            foreach (var vertice in _grafoUrbano.Vertices)
            {
                foreach (var arista in vertice.Adyacentes)
                {
                    var destino = arista.Destino;
                    string clave1 = $"{vertice.Id}-{destino.Id}";
                    string clave2 = $"{destino.Id}-{vertice.Id}";

                    if (!aristasDibujadas.Contains(clave1) && !aristasDibujadas.Contains(clave2))
                    {
                        aristasDibujadas.Add(clave1);
                        g.DrawLine(penArista, vertice.X, vertice.Y, destino.X, destino.Y);

                        // Texto del peso en el punto medio
                        int mx = (vertice.X + destino.X) / 2;
                        int my = (vertice.Y + destino.Y) / 2;
                        g.DrawString($"{arista.Peso:0.0} km", fontPeso, brushPeso, mx - 12, my - 8);
                    }
                }
            }

            // 2. Si hay ruta óptima calculada por Dijkstra, iluminarla en verde vibrante
            if (_rutaActual != null && _rutaActual.Count > 1)
            {
                using var penRutaOptima = new Pen(Color.FromArgb(34, 197, 94), 4.5f); // Verde acento
                penRutaOptima.StartCap = LineCap.Round;
                penRutaOptima.EndCap = LineCap.Round;

                for (int i = 0; i < _rutaActual.Count - 1; i++)
                {
                    var u = _rutaActual[i];
                    var v = _rutaActual[i + 1];
                    g.DrawLine(penRutaOptima, u.X, u.Y, v.X, v.Y);
                }
            }

            // 3. Dibujar los vértices (Nodos urbanos)
            using var fontEtiqueta = Fuentes.Geist(8.0f, Peso.Semibold);
            using var brushTextoNodo = new SolidBrush(Color.FromArgb(30, 41, 59));

            foreach (var v in _grafoUrbano.Vertices)
            {
                int radio = 16;
                var rect = new Rectangle(v.X - radio, v.Y - radio, radio * 2, radio * 2);

                bool esOrigen = cmbOrigen.SelectedItem == v;
                bool esDestino = cmbDestino.SelectedItem == v;
                bool enRuta = _rutaActual != null && _rutaActual.Contains(v);

                Color colorRelleno;
                if (esOrigen)
                {
                    colorRelleno = Color.FromArgb(59, 130, 246); // Azul
                }
                else if (esDestino)
                {
                    colorRelleno = Color.FromArgb(239, 68, 68); // Rojo
                }
                else if (enRuta)
                {
                    colorRelleno = Color.FromArgb(34, 197, 94); // Verde
                }
                else if (v.Dato.Tipo == "Central")
                {
                    colorRelleno = Colores.Primario;
                }
                else
                {
                    colorRelleno = Color.FromArgb(241, 245, 249);
                }

                using var brushNodo = new SolidBrush(colorRelleno);
                using var penBorde = new Pen(enRuta ? Color.White : Color.FromArgb(148, 163, 184), 2f);

                g.FillEllipse(brushNodo, rect);
                g.DrawEllipse(penBorde, rect);

                // Dibujar identificador en el centro del nodo
                string textoId = v.Id.ToString();
                Color colorTextoId = (esOrigen || esDestino || enRuta || v.Dato.Tipo == "Central") ? Color.White : Color.FromArgb(51, 65, 85);
                using (var brushId = new SolidBrush(colorTextoId))
                {
                    var tam = g.MeasureString(textoId, fontEtiqueta);
                    g.DrawString(textoId, fontEtiqueta, brushId, v.X - (tam.Width / 2), v.Y - (tam.Height / 2));
                }

                // Dibujar nombre de la zona debajo del nodo
                var tamNombre = g.MeasureString(v.Nombre, fontEtiqueta);
                g.DrawString(v.Nombre, fontEtiqueta, brushTextoNodo, v.X - (tamNombre.Width / 2), v.Y + radio + 3);
            }
        }
    }
}
