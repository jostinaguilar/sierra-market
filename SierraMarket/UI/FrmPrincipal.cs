using SierraMarket.Estructuras.Arboles;
using SierraMarket.Estructuras.Colas;
using SierraMarket.Estructuras.Grafos;
using SierraMarket.Estructuras.Listas;
using SierraMarket.Modelos;
using SierraMarket.UI.Vistas.Delivery;
using SierraMarket.UI.Vistas.Inventario;
using SierraMarket.UI.Vistas.Turnos;
using SierraMarket.UI.Vistas.Venta;
using System;
using System.Windows.Forms;

namespace SierraMarket.UI
{
    public partial class FrmPrincipal : Form
    {
        private ListaSimple<Producto> _listaProductos = new ListaSimple<Producto>();

        private ListaCircular<Trabajador> _listaTrabajadores = new ListaCircular<Trabajador>();

        private ListaSimple<Venta> _listaVentas = new ListaSimple<Venta>();

        // Árbol Binario de Búsqueda para búsqueda rápida de productos por código (O(log n))
        private ArbolBinario<Producto> _arbolProductos = new ArbolBinario<Producto>();

        // Cola de ventas en espera (FIFO): clientes con problemas de conexión en billeteras digitales
        private Cola<VentaPendiente> _colaPendientes = new Cola<VentaPendiente>();

        private UscInventario _uscInventario;
        private UscVenta _uscVenta;
        private UscTurnos _uscTurnos;
        private UscHistorial _uscHistorial;
        private UscDelivery _uscDelivery;

        public FrmPrincipal()
        {
            this.WindowState = FormWindowState.Maximized;
            ConstruirInterfaz();

            CargarTrabajadores();
            CargarProductos();
            _uscVenta = new UscVenta(_listaProductos, _listaTrabajadores, _listaVentas, _colaPendientes, _arbolProductos);
            CargarVista(_uscVenta);
        }

        private void BtnVentas_Click(object sender, EventArgs e)
        {
            if (_uscVenta == null)
            {
                _uscVenta = new UscVenta(_listaProductos, _listaTrabajadores, _listaVentas, _colaPendientes, _arbolProductos);
            }

            CargarVista(_uscVenta);
        }

        private void BtnInventario_Click(object sender, EventArgs e)
        {
            if (_uscInventario == null)
            {
                _uscInventario = new UscInventario(_listaProductos, _arbolProductos);
            }

            CargarVista(_uscInventario);
        }

        private void BtnTurnos_Click(object sender, EventArgs e)
        {
            if (_uscTurnos == null)
            {
                _uscTurnos = new UscTurnos(_listaTrabajadores);
                _uscTurnos.OnTrabajadorCambiado += (trabajador) =>
                {
                    lblNombreUsuario.Text = trabajador?.Nombres ?? "Sin cajero";
                };
            }

            CargarVista(_uscTurnos);
        }

        private void BtnHistorial_Click(object sender, EventArgs e)
        {
            if (_uscHistorial == null)
            {
                _uscHistorial = new UscHistorial(_listaVentas);
            }

            _uscHistorial.CargarVentas();
            CargarVista(_uscHistorial);
        }

        private void BtnDelivery_Click(object sender, EventArgs e)
        {
            if (_uscDelivery == null)
            {
                _uscDelivery = new UscDelivery();
            }

            CargarVista(_uscDelivery);
        }

        private void CargarVista(UserControl vista)
        {
            pnlPrincipal.Controls.Clear();
            vista.Dock = DockStyle.Fill;
            pnlPrincipal.Controls.Add(vista);
        }

        private void CargarTrabajadores()
        {
            Trabajador trabajador1 = new Trabajador(1, "Jhonny Perez", TimeOnly.Parse("08:00"), TimeOnly.Parse("15:00"));
            Trabajador trabajador2 = new Trabajador(2, "Diana Alcantara", TimeOnly.Parse("15:00"), TimeOnly.Parse("22:00"));
            _listaTrabajadores.Agregar(trabajador1);
            _listaTrabajadores.Agregar(trabajador2);
            lblNombreUsuario.Text = _listaTrabajadores.ObtenerActual().Nombres;
        }

        private void CargarProductos()
        {
            var catalogo = new (string Nombre, decimal Precio)[]
            {
                ("Huevos Pardos BELL'S Bandeja 30un", 17.80m),
                ("Arroz Extra Añejo FARAON Naranja Bolsa 5kg", 24.90m),
                ("Leche Reconstituida Entera GLORIA Lata 390g Paquete 6un", 23.10m),
                ("Aceite Vegetal PRIMOR Premium Botella 900ml", 11.00m),
                ("Azúcar Rubia BELL'S Bolsa 5Kg", 17.90m),
                ("Mayonesa ALACENA Doypack 850g", 17.50m),
                ("Filete de Atún PRIMOR en Aceite Vegetal Lata 140g", 5.70m),
                ("Gaseosa COCA COLA sin Azúcar Botella 300ml Paquete 6un", 10.00m),
                ("Fideos DON VITTORIO Spaghetti Bolsa 950g", 5.40m),
                ("Lenteja Bebé BELL'S Bolsa 500g", 4.20m),
                ("Sopa Instántanea AJINOMEN Sabor a Gallina Bolsa 80g", 2.00m),
                ("Bebida FRUGOS DEL VALLE Durazno Caja 235ml Six Pack", 8.20m),
                ("Leche UHT GLORIA Zero Lacto Caja 946ml Paquete 3un", 16.90m),
                ("Aceite Vegetal BELL'S Botella 900ml", 5.70m),
                ("Arroz Extra COSTEÑO Bolsa 5Kg", 24.50m),
                ("Filete de Atún CAMPOMAR en Aceite Vegetal Lata 150g", 5.70m),
                ("Mayonesa ALACENA Doypack 475g", 11.90m),
                ("Azúcar Rubia DULFINA Bolsa 5Kg", 20.90m),
                ("Pack Gaseosa COCA COLA + Gaseosa INCA KOLA Paquete 2un Botella 2.25L", 15.90m),
                ("Quinua COSTEÑO Bolsa 500g", 8.50m),
                ("Fideos Tagliatelle de Huevo LA MONTANARA Bolsa 500g", 7.70m),
                ("Bebida FRUGOS Sabor a Durazno Caja 1.45L", 5.00m),
                ("Sopa Instántanea AJINOMEN Sabor a Gallina Picante Bolsa 80g", 2.00m),
                ("Palta Fuerte x kg", 11.29m),
                ("Mantequilla GLORIA Pote 390g", 16.50m),
                ("Yogurt Griego VAKIMU Original Pote 960g", 15.90m),
                ("Pan de Molde Blanco XL BIMBO Extra Grande Bolsa 770g", 12.40m),
                ("Café Instantáneo ALTOMAYO Gourmet Frasco 170g", 30.60m),
                ("Queso Fresco BELL'S Paquete 300g", 8.50m),
                ("Pack Hot Dog Ahumado SALCHICHERÍA SUIZA Paquete 600g", 11.90m),
                ("Queso Parmesano Rallado LAIVE Paquete 35g", 4.60m),
                ("Avena QUAKER Tradicional Bolsa 900g", 11.90m),
                ("Fortificante Granulado MILO Activ - Go Doypack 350g", 17.50m),
                ("Mermelada Fresa GLORIA Pote 310g", 6.40m),
                ("Mandarina W MURCOTT x kg", 4.89m),
                ("Leche Light GLORIA Lata 390g Paquete 6un", 23.30m),
                ("Mantequilla con Sal LAIVE Barra 180g", 9.90m),
                ("Leche Entera UHT GLORIA Caja 946ml Paquete 3un", 16.90m),
                ("Queso Fresco sin Lactosa LAIVE x kg", 11.37m),
                ("Café Tostado y Molido CAFETAL Selecto Bolsa 454g", 28.60m),
                ("Yogurt Griego Descremado DANLAC Natural Frasco 900g", 18.90m),
                ("Jamón Inglés CASA EUROPA Paquete 200g", 10.90m),
                ("Salchicha Huachana BRAEDT Paquete 150g", 5.50m),
                ("Chocolate para Taza SOL DE CUSCO Pasta Pura Tableta 80g", 13.70m),
                ("Cereal UNIÓN Granola Pasas y Almendras Doypack 400g", 16.50m),
                ("Avena BELL'S Bolsa 1Kg", 8.90m),
                ("Tostadas BIMBO Clásicas Paquete 10un", 6.90m),
                ("Té Canela y Clavo MC COLIN'S Caja 100un", 12.20m)
            };

            for (int i = 0; i < catalogo.Length; i++)
            {
                _listaProductos.Agregar(new Producto(i + 1, catalogo[i].Nombre, catalogo[i].Precio, 10));
            }


            // Cargar productos en el Árbol Binario de Búsqueda de forma balanceada (Divide y Vencerás)
            var listaOrdenada = _listaProductos.ObtenerTodos();
            _arbolProductos.CargarBalanceado(listaOrdenada);
        }
    }
}