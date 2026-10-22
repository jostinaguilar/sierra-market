using SierraMarket.Estructuras.Listas;
using SierraMarket.Modelos;
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
        private readonly ListaSimple<SierraMarket.Modelos.Venta> _listaVentas;
        private DataGridView dgvHistorial;

        public UscHistorial(ListaSimple<SierraMarket.Modelos.Venta> listaVentas)
        {
            InitializeComponent();

            _listaVentas = listaVentas;

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
            dgvHistorial.RowHeadersVisible = false;

            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Código", DataPropertyName = "IdVenta", Width = 80 });
            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Fecha", DataPropertyName = "FechaHora", Width = 250 });
            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nombre del Trabajador", DataPropertyName = "NombreTrabajador", Width = 80 });
            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Total", DataPropertyName = "Total", Width = 80 });

            pnlTitulo.Controls.Add(lblTitulo);

            tlpHistorial.Controls.Add(pnlTitulo, 0, 0);
            tlpHistorial.Controls.Add(dgvHistorial, 0, 4);

            this.Controls.Add(tlpHistorial);
            CargarVentas();
        }

        public void CargarVentas()
        {
            var ventas = _listaVentas.ObtenerTodos();

            dgvHistorial.DataSource = null;
            dgvHistorial.DataSource = ventas;
        }
    }
}
