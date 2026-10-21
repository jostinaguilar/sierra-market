using SierraMarket.UI.Componentes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SierraMarket.UI.Formularios.Venta
{
    public partial class UscHistorial : UserControl
    {
        public UscHistorial()
        {
            InitializeComponent();

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


            pnlTitulo.Controls.Add(lblTitulo);

            tlpHistorial.Controls.Add(pnlTitulo, 0, 0);

            this.Controls.Add(tlpHistorial);
        }
    }
}
