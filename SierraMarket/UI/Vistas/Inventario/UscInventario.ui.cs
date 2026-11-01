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
            tlpInventario.RowCount = 5;
            tlpInventario.ColumnCount = 1;
            tlpInventario.Margin = new Padding(0);
            tlpInventario.Padding = new Padding(20);

            tlpInventario.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpInventario.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            tlpInventario.RowStyles.Add(new RowStyle(SizeType.Absolute, 33F));
            tlpInventario.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            tlpInventario.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            var pnlTitulo = new Panel();
            pnlTitulo.Dock = DockStyle.Fill;
            pnlTitulo.Margin = new Padding(0);

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
                BorderStyle = BorderStyle.FixedSingle,
                Font = Fuentes.GeistRegular(10F)
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

            dgvInventario.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Código", DataPropertyName = "Codigo", Width = 80 });
            dgvInventario.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Producto", DataPropertyName = "Descripcion", Width = 250 });
            dgvInventario.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Precio", DataPropertyName = "Precio", Width = 80 });
            dgvInventario.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Stock", DataPropertyName = "Stock", Width = 80 });
            dgvInventario.Columns.Add(new DataGridViewButtonColumn { Name = "btnEditar", HeaderText = "", Text = "Editar", UseColumnTextForButtonValue = true, Width = 80 });
            dgvInventario.Columns.Add(new DataGridViewButtonColumn { Name = "btnEliminar", HeaderText = "", Text = "Eliminar", UseColumnTextForButtonValue = true, Width = 80 });
            dgvInventario.CellContentClick += DgvInventario_CellContentClick;

            tlpInventario.Controls.Add(pnlTitulo, 0, 0);
            tlpInventario.Controls.Add(txtBuscarProducto, 0, 2);
            tlpInventario.Controls.Add(dgvInventario, 0, 4);

            this.Controls.Add(tlpInventario);
        }
    }
}
