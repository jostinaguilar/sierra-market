using SierraMarket.UI.Componentes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SierraMarket.UI.Formularios.Turnos
{
    public partial class UscTurnos : UserControl
    {
        public UscTurnos()
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

            var lblTitulo = new Texto.Subtitulo("Gestión de Turnos");

            lblTitulo.Dock = DockStyle.Left;
            lblTitulo.Margin = new Padding(0);

            var btnAgregarProducto = new Boton.Primario("Agregar turno");
            btnAgregarProducto.Dock = DockStyle.Right;
            btnAgregarProducto.Margin = new Padding(0);

            pnlTitulo.Controls.AddRange(new Control[] { lblTitulo, btnAgregarProducto });

            tlpInventario.Controls.Add(pnlTitulo, 0, 0);

            this.Controls.Add(tlpInventario);
        }
    }
}
