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

namespace SierraMarket.UI.Formularios.Inventario
{
    public partial class FrmAgregarProducto : Form
    {
        private ListaSimple<Producto> listProductos;

        public FrmAgregarProducto()
        {
            InitializeComponent();

            listProductos = new ListaSimple<Producto>();

            this.Width = 400;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            var tlpFormulario = new TableLayoutPanel();
            tlpFormulario.Margin = new Padding(0);
            tlpFormulario.Padding = new Padding(20);
            tlpFormulario.ColumnCount = 1;
            tlpFormulario.RowCount = 16;
            tlpFormulario.Dock = DockStyle.Fill;

            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));

            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));

            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));

            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));

            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));

            var lblTitulo = new Texto.Base("Agregar Producto");

            var lblDescripcion = new Texto.SM("Descripción");
            var txtDescripcion = new CampoTexto.Campo("Ingrese una descripción");

            var lblStock = new Texto.SM("Stock");
            var txtStock = new CampoTexto.Campo("Ingrese el stock");

            var lblPrecio = new Texto.SM("Precio");
            var txtPrecio = new CampoTexto.Campo("Ingrese un precio");

            var btnGuardar = new Botones.Primario("Guardar");
            btnGuardar.Dock = DockStyle.Fill;

            tlpFormulario.Controls.Add(lblTitulo, 0, 0);
            tlpFormulario.Controls.Add(lblDescripcion, 0, 2);
            tlpFormulario.Controls.Add(txtDescripcion.ConBorde(), 0, 4);
            tlpFormulario.Controls.Add(lblStock, 0, 6);
            tlpFormulario.Controls.Add(txtStock.ConBorde(), 0, 8);
            tlpFormulario.Controls.Add(lblPrecio, 0, 10);
            tlpFormulario.Controls.Add(txtPrecio.ConBorde(), 0, 12);
            tlpFormulario.Controls.Add(btnGuardar, 0, 14);

            this.Controls.Add(tlpFormulario);
        }

        public void BtnGuardar_Click(object sender, EventArgs e)
        {

        }
    }
}
