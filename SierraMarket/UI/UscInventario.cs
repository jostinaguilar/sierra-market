using SierraMarket.UI.Componentes;
using SierraMarket.UI.Formularios.Inventario;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SierraMarket.UI
{
    public partial class UscInventario : UserControl
    {
        public UscInventario()
        {
            InitializeComponent();

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

            var lblTitulo = new Texto.Base("Gestión de Inventario", 20, 20);

            lblTitulo.Dock = DockStyle.Left;
            lblTitulo.Margin = new Padding(0);

            var btnAgregarProducto = new Botones.Primario("Agregar producto");
            btnAgregarProducto.Dock = DockStyle.Right;
            btnAgregarProducto.Margin = new Padding(0);
            btnAgregarProducto.Click += BtnAgregarProducto_Click;

            var txtBuscarProducto = new CampoTexto.CampoBuscar("Buscar producto...");

            pnlTitulo.Controls.AddRange(new Control[] { lblTitulo, btnAgregarProducto });

            var dgvInventario = new DataGridView();
            dgvInventario.Dock = DockStyle.Fill;
            dgvInventario.Margin = new Padding(0);
            dgvInventario.AutoGenerateColumns = false;
            dgvInventario.AllowUserToAddRows = false;
            dgvInventario.ReadOnly = true;
            dgvInventario.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInventario.RowHeadersVisible = false;

            dgvInventario.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Código", DataPropertyName = "Codigo", Width = 80 });
            dgvInventario.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Producto", DataPropertyName = "Nombre", Width = 250 });
            dgvInventario.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Stock", DataPropertyName = "Stock", Width = 80 });
            dgvInventario.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Precio", DataPropertyName = "Precio", Width = 80 });
            dgvInventario.Columns.Add(new DataGridViewButtonColumn { HeaderText = "", Text = "Editar", UseColumnTextForButtonValue = true, Width = 80 });

            tlpInventario.Controls.Add(pnlTitulo, 0, 0);
            tlpInventario.Controls.Add(txtBuscarProducto.ConBorde(), 0, 2);
            tlpInventario.Controls.Add(dgvInventario, 0, 4);

            this.Controls.Add(tlpInventario);
        }

        public void BtnAgregarProducto_Click(object sender, EventArgs e)
        {
            var frmAgregar = new FrmAgregarProducto();

            if (frmAgregar.ShowDialog() == DialogResult.OK)
            {
                ActualizarInventario();
            }
        }

        public void ActualizarInventario()
        {
            
        }
    }
}
