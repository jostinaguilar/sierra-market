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

        private void ConstruirInterfaz()
        {
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

            var pnlTitulo = new Panel();
            pnlTitulo.Dock = DockStyle.Fill;
            pnlTitulo.Margin = new Padding(20, 0, 20, 0);

            var lblTitulo = new Texto.Subtitulo("Gestión de Inventario");

            lblTitulo.Dock = DockStyle.Left;
            lblTitulo.Margin = new Padding(0);

            var btnAgregarProducto = new Boton.Primario("Agregar producto");
            btnAgregarProducto.Dock = DockStyle.Right;
            btnAgregarProducto.Margin = new Padding(0);
            btnAgregarProducto.Click += BtnAgregarProducto_Click;

            txtBuscarProducto = new TextBox
            {
                Dock = DockStyle.Fill,
                PlaceholderText = "Buscar producto...",
                BorderStyle = BorderStyle.None,
                Font = Fuentes.GeistRegular(10F),
                Margin = new Padding(25, 9, 25, 10)
            };

            pnlTitulo.Controls.AddRange(new Control[] { lblTitulo, btnAgregarProducto });

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

            tlpInventario.Controls.Add(pnlTitulo, 0, 0);
            tlpInventario.Controls.Add(new SeparadorHorizontal(), 0, 2);
            tlpInventario.Controls.Add(txtBuscarProducto, 0, 3);
            tlpInventario.Controls.Add(new SeparadorHorizontal(), 0, 4);
            tlpInventario.Controls.Add(dgvInventario, 0, 5);

            this.Controls.Add(tlpInventario);
        }
    }
}
