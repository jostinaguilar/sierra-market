using SierraMarket.UI.Componentes;
using SierraMarket.UI.Tema;
using System.Drawing;
using System.Windows.Forms;

namespace SierraMarket.UI.Vistas.Inventario
{
    public partial class UscInventario : UserControl
    {
        private DataGridView dgvInventario;
        private TextBox txtBuscarProducto;

        // Campos del formulario lateral
        private Texto.Subtitulo lblFormularioTitulo;
        private TextBox txtDescripcion;
        private TextBox txtPrecio;
        private TextBox txtStock;
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

            // PANEL IZQUIERDO: Gestión de Inventario (Tabla y Buscador)
            var tlpInventario = new TableLayoutPanel();
            tlpInventario.Dock = DockStyle.Fill;
            tlpInventario.RowCount = 6;
            tlpInventario.ColumnCount = 1;
            tlpInventario.Margin = new Padding(0);
            tlpInventario.Padding = new Padding(0, 20, 0, 20);

            tlpInventario.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F)); // 0: Titulo
            tlpInventario.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F)); // 1: Espacio
            tlpInventario.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));  // 2: Separador
            tlpInventario.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F)); // 3: Buscador
            tlpInventario.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));  // 4: Separador
            tlpInventario.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // 5: Grilla

            var lblTitulo = new Texto.Subtitulo("Gestión de Inventario");
            lblTitulo.Dock = DockStyle.Fill;
            lblTitulo.Margin = new Padding(20, 0, 20, 0);

            txtBuscarProducto = new TextBox
            {
                Dock = DockStyle.Fill,
                PlaceholderText = "Buscar producto...",
                BorderStyle = BorderStyle.None,
                Font = Fuentes.GeistRegular(10F),
                Margin = new Padding(25, 9, 25, 10)
            };
            txtBuscarProducto.TextChanged += TxtBuscarProducto_TextChanged;

            dgvInventario = new DataGridView();
            dgvInventario.Dock = DockStyle.Fill;
            dgvInventario.Margin = new Padding(0);
            dgvInventario.AutoGenerateColumns = false;
            dgvInventario.AllowUserToAddRows = false;
            dgvInventario.ReadOnly = true;
            dgvInventario.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInventario.RowHeadersVisible = false;
            dgvInventario.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvInventario.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Código", DataPropertyName = "Codigo", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells, DefaultCellStyle = new DataGridViewCellStyle { Padding = new Padding(20, 0, 20, 0) } });
            dgvInventario.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Producto", DataPropertyName = "Descripcion", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvInventario.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Precio", DataPropertyName = "Precio", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dgvInventario.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Stock", DataPropertyName = "Stock", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dgvInventario.Columns.Add(new DataGridViewButtonColumn { Name = "btnEditar", HeaderText = "", Text = "Editar", UseColumnTextForButtonValue = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dgvInventario.Columns.Add(new DataGridViewButtonColumn { Name = "btnEliminar", HeaderText = "", Text = "Eliminar", UseColumnTextForButtonValue = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dgvInventario.CellContentClick += DgvInventario_CellContentClick;
            EstiloGrid.Aplicar(dgvInventario);

            tlpInventario.Controls.Add(lblTitulo, 0, 0);
            tlpInventario.Controls.Add(new SeparadorHorizontal(), 0, 2);
            tlpInventario.Controls.Add(txtBuscarProducto, 0, 3);
            tlpInventario.Controls.Add(new SeparadorHorizontal(), 0, 4);
            tlpInventario.Controls.Add(dgvInventario, 0, 5);

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
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));  // 3: Etiqueta Descripcion
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));   // 4: Separador
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));  // 5: txtDescripcion
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));   // 6: Separador
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));  // 7: Etiqueta Precio
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));  // 8: txtPrecio
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));   // 9: Separador
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));  // 9: Etiqueta Stock
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));  // 10: txtStock
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));   // 11: Separador
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));  // 12: Espacio flexible
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));  // 13: Espacio
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));  // 14: Boton Guardar

            lblFormularioTitulo = new Texto.Subtitulo("Agregar Producto");
            lblFormularioTitulo.Dock = DockStyle.Fill;
            lblFormularioTitulo.Margin = new Padding(0);

            var lblDescripcion = new Texto.Etiqueta("Descripción", Peso.Semibold);
            lblDescripcion.Padding = new Padding(0, 6, 0, 0);

            txtDescripcion = new TextBox
            {
                Dock = DockStyle.Fill,
                PlaceholderText = "Ingrese una descripción...",
                BorderStyle = BorderStyle.None,
                Font = Fuentes.GeistRegular(10F),
                Margin = new Padding(5, 10, 5, 10),
                BackColor = Color.White
            };
            var pnlDescContenedor = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
                Padding = new Padding(10),
                BackColor = Color.White,
            };
            pnlDescContenedor.Controls.Add(txtDescripcion);

            var lblPrecio = new Texto.Etiqueta("Precio (S/)", Peso.Semibold);
            lblPrecio.Padding = new Padding(0, 6, 0, 0);

            txtPrecio = new TextBox
            {
                Dock = DockStyle.Fill,
                PlaceholderText = "Ej. 4.50",
                BorderStyle = BorderStyle.None,
                Font = Fuentes.GeistRegular(10F),
                Margin = new Padding(5, 10, 5, 10),
                BackColor = Color.White
            };
            var pnlPrecioContenedor = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
                Padding = new Padding(10),
                BackColor = Color.White
            };
            pnlPrecioContenedor.Controls.Add(txtPrecio);

            var lblStock = new Texto.Etiqueta("Stock disponible", Peso.Semibold);
            lblStock.Padding = new Padding(0, 6, 0, 0);

            txtStock = new TextBox
            {
                Dock = DockStyle.Fill,
                PlaceholderText = "Ej. 25",
                BorderStyle = BorderStyle.None,
                Font = Fuentes.GeistRegular(10F),
                Margin = new Padding(5, 10, 5, 10),
                BackColor = Color.White
            };
            var pnlStockContenedor = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
                Padding = new Padding(10),
                BackColor = Color.White
            };
            pnlStockContenedor.Controls.Add(txtStock);

            btnGuardar = new Boton.Primario("Guardar");
            btnGuardar.Dock = DockStyle.Fill;
            btnGuardar.Click += BtnGuardar_Click;

            btnLimpiar = new Boton.Secundario("Limpiar");
            btnLimpiar.Dock = DockStyle.Fill;
            btnLimpiar.Click += BtnLimpiar_Click;

            tlpFormulario.Controls.Add(lblFormularioTitulo, 0, 0);
            tlpFormulario.Controls.Add(lblDescripcion, 0, 3);
            tlpFormulario.Controls.Add(new SeparadorHorizontal(), 0, 4);
            tlpFormulario.Controls.Add(pnlDescContenedor, 0, 5);
            tlpFormulario.Controls.Add(new SeparadorHorizontal(), 0, 6);
            tlpFormulario.Controls.Add(lblPrecio, 0, 7);
            tlpFormulario.Controls.Add(new SeparadorHorizontal(), 0, 8);
            tlpFormulario.Controls.Add(pnlPrecioContenedor, 0, 9);
            tlpFormulario.Controls.Add(new SeparadorHorizontal(), 0, 10);
            tlpFormulario.Controls.Add(lblStock, 0, 11);
            tlpFormulario.Controls.Add(new SeparadorHorizontal(), 0, 12);
            tlpFormulario.Controls.Add(pnlStockContenedor, 0, 13);
            tlpFormulario.Controls.Add(new SeparadorHorizontal(), 0, 14);
            tlpFormulario.Controls.Add(btnGuardar, 0, 18);

            pnlFormulario.Controls.Add(tlpFormulario);

            tlpContenedor.Controls.Add(tlpInventario, 0, 0);
            tlpContenedor.Controls.Add(new SeparadorHorizontal { Dock = DockStyle.Fill, Width = 1 }, 1, 0);
            tlpContenedor.Controls.Add(pnlFormulario, 2, 0);

            this.Controls.Add(tlpContenedor);
        }
    }
}
