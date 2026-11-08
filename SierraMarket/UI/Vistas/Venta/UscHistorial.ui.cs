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
            tlpHistorial.RowCount = 4;
            tlpHistorial.ColumnCount = 1;
            tlpHistorial.Margin = new Padding(0);
            tlpHistorial.Padding = new Padding(0, 20, 0, 20);

            tlpHistorial.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F)); // 0: Titulo
            tlpHistorial.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F)); // 1: Espacio
            tlpHistorial.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));  // 2: Separador
            tlpHistorial.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // 3: Grilla

            var pnlSeparador1 = new Panel();
            pnlSeparador1.Dock = DockStyle.Fill;
            pnlSeparador1.Margin = new Padding(0);
            pnlSeparador1.Padding = new Padding(0);
            pnlSeparador1.BackColor = Color.FromArgb(228, 228, 231);

            var pnlTitulo = new Panel();
            pnlTitulo.Dock = DockStyle.Fill;
            pnlTitulo.Margin = new Padding(20, 0, 20, 0);

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

            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Código", DataPropertyName = "IdVenta", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells, DefaultCellStyle = new DataGridViewCellStyle { Padding = new Padding(20, 0, 20, 0) } });
            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Fecha", DataPropertyName = "FechaHora", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nombre del Trabajador", DataPropertyName = "NombreTrabajador", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Total", DataPropertyName = "Total", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            EstiloGrid.Aplicar(dgvHistorial);

            dgvHistorial.EnableHeadersVisualStyles = false;
            dgvHistorial.ColumnHeadersDefaultCellStyle.Padding = new Padding(20, 10, 10, 10);
            dgvHistorial.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvHistorial.ColumnHeadersHeight = 50;

            pnlTitulo.Controls.Add(lblTitulo);

            tlpHistorial.Controls.Add(pnlTitulo, 0, 0);
            tlpHistorial.Controls.Add(pnlSeparador1, 0, 2);
            tlpHistorial.Controls.Add(dgvHistorial, 0, 3);

            this.Controls.Add(tlpHistorial);
        }
    }
}