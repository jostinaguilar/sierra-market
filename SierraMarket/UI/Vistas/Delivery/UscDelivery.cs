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

        // Estado interactivo para conectar aristas entre dos nodos (grafo no dirigido ponderado)
        private Vertice<PuntoEntrega>? _primerNodoAConectar = null;

        public UscDelivery(Grafo<PuntoEntrega>? grafo = null)
        {
            _grafoUrbano = grafo ?? InicializarRedUrbana();

            ConstruirInterfaz();
            CargarPuntosEnSelectores();
        }

        public static Grafo<PuntoEntrega> InicializarRedUrbana()
        {
            var grafo = new Grafo<PuntoEntrega>();

            // Centro del mapa radial: (400, 260)
            int cx = 400;
            int cy = 260;

            // Nodo central: SierraMarket
            var v0 = grafo.AgregarVertice(0, "SierraMarket", new PuntoEntrega(0, "SierraMarket"), cx, cy);

            // Nodos periféricos distribuidos radialmente
            var v1 = grafo.AgregarVertice(1, "FAST + MEDIC", new PuntoEntrega(1, "FAST + MEDIC"), cx - 180, cy - 130);
            var v2 = grafo.AgregarVertice(2, "Consorcio Simon's Lab", new PuntoEntrega(2, "Consorcio Simon's Lab"), cx - 180, cy + 130);
            var v3 = grafo.AgregarVertice(3, "IINCAP Jorge Basadre", new PuntoEntrega(3, "IINCAP Jorge Basadre"), cx, cy - 170);
            var v4 = grafo.AgregarVertice(4, "Parroquia San Roque", new PuntoEntrega(4, "Parroquia San Roque"), cx, cy + 170);
            var v5 = grafo.AgregarVertice(5, "Urban shop", new PuntoEntrega(5, "Urban shop"), cx + 180, cy - 130);
            var v6 = grafo.AgregarVertice(6, "AUTONORT CAJAMARCA SAC", new PuntoEntrega(6, "AUTONORT CAJAMARCA SAC"), cx + 220, cy);
            var v7 = grafo.AgregarVertice(7, "LAS PATITAS", new PuntoEntrega(7, "LAS PATITAS"), cx + 180, cy + 130);
            var v8 = grafo.AgregarVertice(8, "Distribuidoramiguelito", new PuntoEntrega(8, "Distribuidoramiguelito"), cx + 120, cy - 200);

            // Aristas radiales desde el centro
            grafo.AgregarArista(0, 1, 2.2); // Central <-> FAST + MEDIC
            grafo.AgregarArista(0, 2, 2.0); // Central <-> Consorcio Simon's Lab
            grafo.AgregarArista(0, 3, 1.8); // Central <-> IINCAP Jorge Basadre
            grafo.AgregarArista(0, 4, 1.9); // Central <-> Parroquia San Roque
            grafo.AgregarArista(0, 5, 2.3); // Central <-> Urban shop
            grafo.AgregarArista(0, 6, 2.4); // Central <-> AUTONORT CAJAMARCA SAC
            grafo.AgregarArista(0, 7, 2.6); // Central <-> LAS PATITAS

            // Aristas perimetrales / anillos de conexión
            grafo.AgregarArista(1, 3, 2.1); // FAST + MEDIC <-> IINCAP Jorge Basadre
            grafo.AgregarArista(1, 2, 2.7); // FAST + MEDIC <-> Consorcio Simon's Lab
            grafo.AgregarArista(2, 4, 2.5); // Consorcio Simon's Lab <-> Parroquia San Roque
            grafo.AgregarArista(3, 8, 1.7); // IINCAP Jorge Basadre <-> Distribuidoramiguelito
            grafo.AgregarArista(8, 5, 2.0); // Distribuidoramiguelito <-> Urban shop
            grafo.AgregarArista(5, 6, 1.8); // Urban shop <-> AUTONORT CAJAMARCA SAC
            grafo.AgregarArista(6, 7, 2.2); // AUTONORT CAJAMARCA SAC <-> LAS PATITAS
            grafo.AgregarArista(4, 7, 2.4); // Parroquia San Roque <-> LAS PATITAS

            return grafo;
        }

        private void CargarPuntosEnSelectores()
        {
            var seleccionadoOrigen = cmbOrigen.SelectedItem as Vertice<PuntoEntrega>;
            var seleccionadoDestino = cmbDestino.SelectedItem as Vertice<PuntoEntrega>;

            cmbOrigen.Items.Clear();
            cmbDestino.Items.Clear();

            foreach (var vertice in _grafoUrbano.Vertices)
            {
                cmbOrigen.Items.Add(vertice);
                cmbDestino.Items.Add(vertice);
            }

            if (seleccionadoOrigen != null && _grafoUrbano.BuscarVertice(seleccionadoOrigen.Id) != null)
            {
                cmbOrigen.SelectedItem = _grafoUrbano.BuscarVertice(seleccionadoOrigen.Id);
            }
            else if (cmbOrigen.Items.Count > 0)
            {
                cmbOrigen.SelectedIndex = 0; // Por defecto SierraMarket Central
            }

            if (seleccionadoDestino != null && _grafoUrbano.BuscarVertice(seleccionadoDestino.Id) != null)
            {
                cmbDestino.SelectedItem = _grafoUrbano.BuscarVertice(seleccionadoDestino.Id);
            }
            else if (cmbDestino.Items.Count > 1)
            {
                cmbDestino.SelectedIndex = cmbDestino.Items.Count - 1;
            }
        }

        private void PnlMapa_MouseClick(object? sender, MouseEventArgs e)
        {
            // 1. Detectar si el clic fue sobre un nodo existente
            Vertice<PuntoEntrega>? verticeClickeado = null;
            int radio = 18;

            foreach (var v in _grafoUrbano.Vertices)
            {
                int dx = e.X - v.X;
                int dy = e.Y - v.Y;
                if (dx * dx + dy * dy <= radio * radio)
                {
                    verticeClickeado = v;
                    break;
                }
            }

            // Clic derecho: fijar punto como Origen y reiniciar selección de conexión
            if (e.Button == MouseButtons.Right)
            {
                if (verticeClickeado != null)
                {
                    cmbOrigen.SelectedItem = verticeClickeado;
                    _primerNodoAConectar = null;
                    lblGuiaMapa.Text = $"Origen fijado: \"{verticeClickeado.Nombre}\". Haz clic en 2 puntos para conectarlos.";
                    lblGuiaMapa.ForeColor = Color.FromArgb(71, 85, 105);
                    pnlMapa.Invalidate();
                }
                return;
            }

            if (e.Button != MouseButtons.Left) return;

            // 2. Si se hizo clic sobre un nodo
            if (verticeClickeado != null)
            {
                if (_primerNodoAConectar == null)
                {
                    // Primer clic: seleccionar el primer punto
                    _primerNodoAConectar = verticeClickeado;
                    cmbOrigen.SelectedItem = verticeClickeado;
                    lblGuiaMapa.Text = $"Punto 1: \"{_primerNodoAConectar.Nombre}\" seleccionado. Haz clic en el SEGUNDO punto para conectarlos.";
                    lblGuiaMapa.ForeColor = Color.FromArgb(234, 88, 12); // Naranja
                    pnlMapa.Invalidate();
                }
                else
                {
                    // Si se hace clic en el mismo nodo, deseleccionar
                    if (_primerNodoAConectar.Id == verticeClickeado.Id)
                    {
                        _primerNodoAConectar = null;
                        lblGuiaMapa.Text = "Selección cancelada. Haz clic en 2 puntos para conectar o en espacio vacío para crear dirección.";
                        lblGuiaMapa.ForeColor = Color.FromArgb(71, 85, 105);
                        pnlMapa.Invalidate();
                        return;
                    }

                    // Segundo clic en nodo diferente: abrir modal para pedir distancia y conectar (grafo no dirigido ponderado)
                    var nodo1 = _primerNodoAConectar;
                    var nodo2 = verticeClickeado;
                    _primerNodoAConectar = null;
                    cmbDestino.SelectedItem = nodo2;

                    var frmDistancia = new FrmConectarNodos(nodo1, nodo2);
                    if (frmDistancia.ShowDialog() == DialogResult.OK)
                    {
                        _grafoUrbano.AgregarArista(nodo1.Id, nodo2.Id, frmDistancia.DistanciaKm, bidireccional: true);
                        lblGuiaMapa.Text = $"Calle conectada: \"{nodo1.Nombre}\" ↔ \"{nodo2.Nombre}\" a {frmDistancia.DistanciaKm:0.0} km.";
                        lblGuiaMapa.ForeColor = Color.FromArgb(22, 101, 52); // Verde
                    }
                    else
                    {
                        lblGuiaMapa.Text = "Conexión cancelada. Haz clic en 2 puntos para conectar o en espacio vacío para crear dirección.";
                        lblGuiaMapa.ForeColor = Color.FromArgb(71, 85, 105);
                    }

                    pnlMapa.Invalidate();
                }
            }
            else
            {
                // 3. Clic en espacio vacío: Crear nueva dirección en estas coordenadas exactas
                var frmNuevo = new FrmNuevoPuntoClick(e.X, e.Y);
                if (frmNuevo.ShowDialog() == DialogResult.OK)
                {
                    int nuevoId = 0;
                    foreach (var v in _grafoUrbano.Vertices)
                    {
                        if (v.Id >= nuevoId) nuevoId = v.Id + 1;
                    }

                    var nuevoPunto = new PuntoEntrega(nuevoId, frmNuevo.NombrePunto);
                    var nuevoVertice = _grafoUrbano.AgregarVertice(nuevoId, frmNuevo.NombrePunto, nuevoPunto, e.X, e.Y);

                    CargarPuntosEnSelectores();
                    cmbDestino.SelectedItem = nuevoVertice;

                    // Si ya había un nodo seleccionado antes de hacer clic en espacio vacío, conectarlo directamente
                    if (_primerNodoAConectar != null)
                    {
                        var nodo1 = _primerNodoAConectar;
                        _primerNodoAConectar = null;

                        var frmDistancia = new FrmConectarNodos(nodo1, nuevoVertice);
                        if (frmDistancia.ShowDialog() == DialogResult.OK)
                        {
                            _grafoUrbano.AgregarArista(nodo1.Id, nuevoVertice.Id, frmDistancia.DistanciaKm, bidireccional: true);
                            lblGuiaMapa.Text = $"Dirección \"{nuevoVertice.Nombre}\" creada y conectada a \"{nodo1.Nombre}\" ({frmDistancia.DistanciaKm:0.0} km).";
                            lblGuiaMapa.ForeColor = Color.FromArgb(22, 101, 52);
                        }
                        else
                        {
                            lblGuiaMapa.Text = $"Dirección \"{nuevoVertice.Nombre}\" creada. Haz clic en otro punto para conectarla.";
                            lblGuiaMapa.ForeColor = Color.FromArgb(71, 85, 105);
                        }
                    }
                    else
                    {
                        // Dejar el nuevo punto como primer nodo para que el siguiente clic a un vecino lo conecte de inmediato
                        _primerNodoAConectar = nuevoVertice;
                        lblGuiaMapa.Text = $"Dirección \"{nuevoVertice.Nombre}\" creada. Haz clic en otro punto para conectarla.";
                        lblGuiaMapa.ForeColor = Color.FromArgb(234, 88, 12);
                    }

                    pnlMapa.Invalidate();
                }
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

            var resultado = _grafoUrbano.Dijkstra(origen.Id, destino.Id);

            if (resultado.ExisteRuta)
            {
                _rutaActual = resultado.Camino;
                _distanciaRutaActual = resultado.DistanciaTotal;

                double minutos = Math.Round((_distanciaRutaActual / 20.0) * 60 + 3);
                decimal costo = 4.00m + (decimal)_distanciaRutaActual * 1.20m;

                lblDistancia.Text = $"Distancia Mínima: {_distanciaRutaActual:0.0} km";
                lblTiempo.Text = $"Tiempo Estimado: ~{minutos:0} minutos";
                lblCostoEnvio.Text = $"Costo de Envío: S/ {costo:0.00}";

                var nombres = new List<string>();
                foreach (var paso in _rutaActual)
                {
                    nombres.Add(paso.Nombre);
                }
                lblRutaRecorrido.Text = string.Join(" ➔\n", nombres);

                btnDespachar.Enabled = true;
                pnlMapa.Invalidate();
            }
            else
            {
                MessageBox.Show("No se encontró una ruta vial conectada hacia el destino seleccionado.\nAsegúrate de conectar los puntos haciendo clic en uno y luego en otro.", "Sin conexión vial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnLimpiarRuta_Click(object? sender, EventArgs e)
        {
            _rutaActual = null;
            _distanciaRutaActual = 0;
            _primerNodoAConectar = null;
            lblGuiaMapa.Text = "Clic en 2 puntos = Conectar calle (km) | Clic en espacio vacío = Crear dirección | Clic derecho = Fijar Origen";
            lblGuiaMapa.ForeColor = Color.FromArgb(71, 85, 105);
            lblDistancia.Text = "Distancia Mínima: --";
            lblTiempo.Text = "Tiempo Estimado: --";
            lblCostoEnvio.Text = "Costo de Envío: --";
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

        private void PnlMapa_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // 1. Dibujar todas las calles / aristas
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

                        int mx = (vertice.X + destino.X) / 2;
                        int my = (vertice.Y + destino.Y) / 2;
                        g.DrawString($"{arista.Peso:0.0} km", fontPeso, brushPeso, mx - 12, my - 8);
                    }
                }
            }

            // 2. Iluminar la ruta óptima de Dijkstra
            if (_rutaActual != null && _rutaActual.Count > 1)
            {
                using var penRutaOptima = new Pen(Color.FromArgb(34, 197, 94), 4.5f);
                penRutaOptima.StartCap = LineCap.Round;
                penRutaOptima.EndCap = LineCap.Round;

                for (int i = 0; i < _rutaActual.Count - 1; i++)
                {
                    var u = _rutaActual[i];
                    var v = _rutaActual[i + 1];
                    g.DrawLine(penRutaOptima, u.X, u.Y, v.X, v.Y);
                }
            }

            // 3. Dibujar nodos
            using var fontEtiqueta = Fuentes.Geist(8.0f, Peso.Semibold);
            using var brushTextoNodo = new SolidBrush(Color.FromArgb(30, 41, 59));

            foreach (var v in _grafoUrbano.Vertices)
            {
                int radio = (v.Dato.Nombre == "Sierra Market") ? 20 : 16;
                var rect = new Rectangle(v.X - radio, v.Y - radio, radio * 2, radio * 2);

                bool esOrigen = cmbOrigen.SelectedItem == v;
                bool esDestino = cmbDestino.SelectedItem == v;
                bool enRuta = _rutaActual != null && _rutaActual.Contains(v);
                bool seleccionadoConexion = _primerNodoAConectar == v;

                Color colorRelleno;
                if (seleccionadoConexion)
                {
                    colorRelleno = Color.FromArgb(249, 115, 22); // Naranja llamativo
                }
                else if (esOrigen)
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
                else if (v.Dato.Nombre == "Sierra Market")
                {
                    colorRelleno = Colores.Primario;
                }
                else
                {
                    colorRelleno = Color.FromArgb(241, 245, 249);
                }

                using var brushNodo = new SolidBrush(colorRelleno);
                using var penBorde = new Pen(seleccionadoConexion ? Color.Black : (enRuta ? Color.White : Color.FromArgb(148, 163, 184)), 2f);

                g.FillEllipse(brushNodo, rect);
                g.DrawEllipse(penBorde, rect);

                // Texto del ID en el centro
                string textoId = v.Id.ToString();
                Color colorTextoId = (esOrigen || esDestino || enRuta || seleccionadoConexion || v.Dato.Nombre == "Sierra Market") ? Color.White : Color.FromArgb(51, 65, 85);
                using (var brushId = new SolidBrush(colorTextoId))
                {
                    var tam = g.MeasureString(textoId, fontEtiqueta);
                    g.DrawString(textoId, fontEtiqueta, brushId, v.X - (tam.Width / 2), v.Y - (tam.Height / 2));
                }

                // Nombre de la zona
                var tamNombre = g.MeasureString(v.Nombre, fontEtiqueta);
                g.DrawString(v.Nombre, fontEtiqueta, brushTextoNodo, v.X - (tamNombre.Width / 2), v.Y + radio + 3);
            }
        }
    }
}
