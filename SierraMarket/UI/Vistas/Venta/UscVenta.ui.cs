using SierraMarket.UI.Componentes;
using SierraMarket.UI.Tema;
using System.Drawing;
using System.Windows.Forms;

namespace SierraMarket.UI.Vistas.Venta
{
    public partial class UscVenta : UserControl
    {
        private DataGridView dgvCatalogo;
        private DataGridView dgvCarrito;
        private Texto.Subtitulo lblTotal;
        private Boton.Primario btnCobrar;

        private void ConstruirInterfaz()
        {
            var tlpPrincipal = new TableLayoutPanel();
            tlpPrincipal.Dock = DockStyle.Fill;
            tlpPrincipal.ColumnCount = 1;
            tlpPrincipal.RowCount = 3;
            tlpPrincipal.Margin = new Padding(0);
            tlpPrincipal.Padding = new Padding(0);
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));

            var tlpContenededor = new TableLayoutPanel();
            tlpContenededor.Dock = DockStyle.Fill;
            tlpContenededor.Margin = new Padding(0);
            tlpContenededor.Padding = new Padding(0);
            tlpContenededor.ColumnCount = 3;
            tlpContenededor.RowCount = 1;
            tlpContenededor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tlpContenededor.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 1F));
            tlpContenededor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));

            var pnlSeparador1 = new Panel();
            pnlSeparador1.Dock = DockStyle.Fill;
            pnlSeparador1.Margin = new Padding(0);
            pnlSeparador1.Padding = new Padding(0);
            pnlSeparador1.BackColor = Color.FromArgb(228, 228, 231);

            var pnlSeparador2 = new Panel();
            pnlSeparador2.Dock = DockStyle.Fill;
            pnlSeparador2.Margin = new Padding(0);
            pnlSeparador2.Padding = new Padding(0);
            pnlSeparador2.BackColor = Color.FromArgb(228, 228, 231);

            var pnlFooter = new Panel();
            pnlFooter.Dock = DockStyle.Fill;
            pnlFooter.Margin = new Padding(0);
            pnlFooter.Padding = new Padding(0);
            pnlFooter.BackColor = Color.FromArgb(255, 255, 255);

            var tlpVenta = new TableLayoutPanel();
            tlpVenta.Dock = DockStyle.Fill;
            tlpVenta.RowCount = 4;
            tlpVenta.ColumnCount = 1;
            tlpVenta.Margin = new Padding(0);
            tlpVenta.Padding = new Padding(0, 20, 0, 20);

            tlpVenta.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F)); //0
            tlpVenta.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F)); //1
            tlpVenta.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F)); //2
            tlpVenta.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F)); //3
            tlpVenta.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F)); //4
            tlpVenta.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); //5

            var pnlSeparador3 = new Panel();
            pnlSeparador3.Dock = DockStyle.Fill;
            pnlSeparador3.Margin = new Padding(0);
            pnlSeparador3.Padding = new Padding(0);
            pnlSeparador3.BackColor = Color.FromArgb(228, 228, 231);

            var pnlSeparador4 = new Panel();
            pnlSeparador4.Dock = DockStyle.Fill;
            pnlSeparador4.Margin = new Padding(0);
            pnlSeparador4.Padding = new Padding(0);
            pnlSeparador4.BackColor = Color.FromArgb(228, 228, 231);

            var pnlCarrito = new Panel();
            pnlCarrito.Dock = DockStyle.Fill;
            pnlCarrito.Margin = new Padding(0);
            pnlCarrito.Padding = new Padding(20);
            pnlCarrito.BackColor = Color.FromArgb(250, 250, 250);

            var tlpCarrito = new TableLayoutPanel();
            tlpCarrito.Dock = DockStyle.Fill;
            tlpCarrito.RowCount = 2;
            tlpCarrito.ColumnCount = 1;
            tlpCarrito.Margin = new Padding(0);
            tlpCarrito.Padding = new Padding(0);

            tlpCarrito.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F)); //0
            tlpCarrito.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F)); //1
            tlpCarrito.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); //2
            tlpCarrito.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F)); //3
            tlpCarrito.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F)); //4
            tlpCarrito.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F)); //5
            tlpCarrito.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F)); //6

            var lblCarrito = new Texto.Subtitulo("Carrito de Compras");
            lblCarrito.Dock = DockStyle.Fill;
            lblCarrito.Margin = new Padding(0);

            lblTotal = new Texto.Subtitulo("Total: S/ 0.00");
            lblTotal.Dock = DockStyle.Fill;
            lblTotal.Margin = new Padding(0);

            btnCobrar = new Boton.Primario("Cobrar");
            btnCobrar.Click += BtnCobrar_Click;

            var lblCatalogo = new Texto.Subtitulo("Catálogo de Productos");
            lblCatalogo.Dock = DockStyle.Fill;
            lblCatalogo.Margin = new Padding(20, 0, 20, 0);

            var txtBuscarProducto = new TextBox { Dock = DockStyle.Fill, PlaceholderText = "Buscar producto...", BorderStyle = BorderStyle.None, Font = Fuentes.GeistRegular(10F), Margin = new Padding(25, 9, 25, 10) };

            dgvCatalogo = new DataGridView();
            dgvCatalogo.Dock = DockStyle.Fill;
            dgvCatalogo.Margin = new Padding(0);
            dgvCatalogo.AutoGenerateColumns = false;
            dgvCatalogo.AllowUserToAddRows = false;
            dgvCatalogo.ReadOnly = true;
            dgvCatalogo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCatalogo.RowHeadersVisible = false;

            dgvCatalogo.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Código", DataPropertyName = "Codigo", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells, DefaultCellStyle = new DataGridViewCellStyle { Padding = new Padding(20, 0, 20, 0) } });
            dgvCatalogo.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Producto", DataPropertyName = "Descripcion", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvCatalogo.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Precio", DataPropertyName = "Precio", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dgvCatalogo.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Stock", DataPropertyName = "Stock", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dgvCatalogo.Columns.Add(new DataGridViewButtonColumn { Name = "btnAgregar", HeaderText = "", Text = "Agregar", UseColumnTextForButtonValue = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dgvCatalogo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCatalogo.CellContentClick += DgvCatalogo_CellContentClick;
            EstiloGrid.Aplicar(dgvCatalogo);

            dgvCatalogo.EnableHeadersVisualStyles = false;

            dgvCatalogo.ColumnHeadersDefaultCellStyle.Padding = new Padding(20, 10, 10, 10);

            dgvCatalogo.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dgvCatalogo.ColumnHeadersHeight = 50;

            dgvCarrito = new DataGridView();
            dgvCarrito.Dock = DockStyle.Fill;
            dgvCarrito.Margin = new Padding(0);
            dgvCarrito.AutoGenerateColumns = false;
            dgvCarrito.AllowUserToAddRows = false;
            dgvCarrito.ReadOnly = true;
            dgvCarrito.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCarrito.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCarrito.RowHeadersVisible = false;
            dgvCarrito.RowTemplate.Height = 48;

            dgvCarrito.Columns.Add(new DataGridViewTextBoxColumn { Name = "colProducto", HeaderText = "Producto", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvCarrito.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSubtotal", HeaderText = "Subtotal", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dgvCarrito.Columns.Add(new DataGridViewButtonColumn { Name = "btnQuitar", HeaderText = "", Text = "Quitar", UseColumnTextForButtonValue = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dgvCarrito.CellFormatting += DgvCarrito_CellFormatting;
            dgvCarrito.CellPainting += DgvCarrito_CellPainting;
            dgvCarrito.CellContentClick += DgvCarrito_CellContentClick;
            EstiloGrid.Aplicar(dgvCarrito);

            tlpCarrito.Controls.Add(lblCarrito, 0, 0);
            tlpCarrito.Controls.Add(dgvCarrito, 0, 2);
            tlpCarrito.Controls.Add(lblTotal, 0, 4);
            tlpCarrito.Controls.Add(btnCobrar, 0, 6);
            pnlCarrito.Controls.Add(tlpCarrito);

            tlpVenta.Controls.Add(lblCatalogo, 0, 0);
            tlpVenta.Controls.Add(pnlSeparador3, 0, 2);
            tlpVenta.Controls.Add(txtBuscarProducto, 0, 3);
            tlpVenta.Controls.Add(pnlSeparador4, 0, 4);
            tlpVenta.Controls.Add(dgvCatalogo, 0, 5);

            tlpContenededor.Controls.Add(tlpVenta, 0, 0);
            tlpContenededor.Controls.Add(pnlSeparador2, 1, 0);
            tlpContenededor.Controls.Add(pnlCarrito, 2, 0);

            tlpPrincipal.Controls.Add(tlpContenededor, 0, 0);
            tlpPrincipal.Controls.Add(pnlSeparador1, 0, 1);
            tlpPrincipal.Controls.Add(pnlFooter, 0, 2);

            this.Controls.Add(tlpPrincipal);
        }
    }
}
