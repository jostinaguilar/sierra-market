using SierraMarket.UI.Componentes;
using System.Drawing;
using System.Windows.Forms;

namespace SierraMarket.UI.Vistas.Venta
{
    public partial class UscHistorial : UserControl
    {
        private DataGridView dgvHistorial;

        private void ConstruirInterfaz()
        {
            var tlpHistorial = new TableLayoutPanel();
            tlpHistorial.Dock = DockStyle.Fill;
            tlpHistorial.RowCount = 5;
            tlpHistorial.ColumnCount = 1;
            tlpHistorial.Margin = new Padding(0);
            tlpHistorial.Padding = new Padding(20);

            tlpHistorial.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpHistorial.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            tlpHistorial.RowStyles.Add(new RowStyle(SizeType.Absolute, 33F));
            tlpHistorial.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            tlpHistorial.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            var pnlTitulo = new Panel();
            pnlTitulo.Dock = DockStyle.Fill;
            pnlTitulo.Margin = new Padding(0);

            var lblTitulo = new Texto.Subtitulo("Historial de Ventas");

            lblTitulo.Dock = DockStyle.Left;
            lblTitulo.Margin = new Padding(0);

            dgvHistorial = new DataGridView();
            dgvHistorial.Dock = DockStyle.Fill;
            dgvHistorial.Margin = new Padding(0);
            dgvHistorial.AutoGenerateColumns = false;
            dgvHistorial.AllowUserToAddRows = false;
            dgvHistorial.ReadOnly = true;
            dgvHistorial.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistorial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistorial.RowHeadersVisible = false;

            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Código", DataPropertyName = "IdVenta", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Fecha", DataPropertyName = "FechaHora", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nombre del Trabajador", DataPropertyName = "NombreTrabajador", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Total", DataPropertyName = "Total", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            EstiloGrid.Aplicar(dgvHistorial);

            pnlTitulo.Controls.Add(lblTitulo);

            tlpHistorial.Controls.Add(pnlTitulo, 0, 0);
            tlpHistorial.Controls.Add(dgvHistorial, 0, 4);

            this.Controls.Add(tlpHistorial);
        }
    }
}