using SierraMarket.UI.Tema;
using System.Drawing;
using System.Windows.Forms;

namespace SierraMarket.UI.Componentes
{
    public static class EstiloGrid
    {
        public static void Aplicar(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Colores.Borde;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Colores.Fondo;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Colores.Primario;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Colores.Fondo;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Colores.Primario;
            dgv.ColumnHeadersDefaultCellStyle.Font = Fuentes.Geist(9.5f, Peso.Semibold);
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(12, 10, 12, 10);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 42;

            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(24, 24, 27);
            dgv.DefaultCellStyle.Font = Fuentes.Geist(9.5f, Peso.Regular);
            dgv.DefaultCellStyle.Padding = new Padding(12, 8, 12, 8);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(244, 244, 245);
            dgv.DefaultCellStyle.SelectionForeColor = Colores.Primario;
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(253, 253, 253);
            dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(244, 244, 245);
            dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = Colores.Primario;

            dgv.RowTemplate.Height = 40;
            dgv.RowTemplate.Resizable = DataGridViewTriState.False;
            dgv.AllowUserToResizeRows = false;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ShowCellToolTips = false;

            // Formatear columnas de botones si existen
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (col is DataGridViewButtonColumn btnCol)
                {
                    btnCol.FlatStyle = FlatStyle.Flat;
                    btnCol.DefaultCellStyle.Font = Fuentes.Geist(8.5f, Peso.Semibold);
                    btnCol.DefaultCellStyle.BackColor = Colores.Fondo;
                    btnCol.DefaultCellStyle.ForeColor = Colores.Primario;
                    btnCol.DefaultCellStyle.SelectionBackColor = Colores.Secundario;
                    btnCol.DefaultCellStyle.SelectionForeColor = Colores.Primario;
                    btnCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    btnCol.DefaultCellStyle.Padding = new Padding(0);
                }
            }
        }
    }
}
