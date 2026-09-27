using SierraMarket.UI.Componentes;
using SierraMarket.UI.Tema;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SierraMarket.UI
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();

            var flpTitulo = new FlowLayoutPanel();
            var lblTitulo = new Texto.XXL("Sierra Market");

            lblTitulo.Padding = new Padding(0);

            flpTitulo.Controls.Add(lblTitulo);
            flpTitulo.Dock = DockStyle.Left;
            flpTitulo.AutoSize = true;
            flpTitulo.WrapContents = false;
            flpTitulo.FlowDirection = FlowDirection.LeftToRight;
            flpTitulo.Padding = new Padding(20, 12, 0, 0);

            pHeader.Controls.Add(flpTitulo);

            var flpCaja = new FlowLayoutPanel();
            var lblCaja = new Texto.SM("Caja", 0, 0, Fuentes.GeistLight(10));
            var lblNumeroCaja = new Texto.SM("01");

            lblCaja.Margin = new Padding(0);
            lblNumeroCaja.Margin = new Padding(0);

            flpCaja.Controls.AddRange(new Control[] { lblCaja, lblNumeroCaja });

            flpCaja.Dock = DockStyle.Right;
            flpCaja.AutoSize = true;
            flpCaja.WrapContents = false;
            flpCaja.FlowDirection = FlowDirection.LeftToRight;
            flpCaja.Padding = new Padding(0, 20, 20, 0);

            var flpUsuario = new FlowLayoutPanel();
            var lblUsuario = new Texto.SM("Usuario", 0, 0, Fuentes.GeistLight(10));
            var lblNombreUsuario = new Texto.SM("Jhon Doe");

            lblUsuario.Margin = new Padding(0);
            lblNombreUsuario.Margin = new Padding(0);

            flpUsuario.Controls.AddRange(new Control[] { lblUsuario, lblNombreUsuario });

            flpUsuario.Dock = DockStyle.Right;
            flpUsuario.AutoSize = true;
            flpUsuario.WrapContents = false;
            flpUsuario.FlowDirection = FlowDirection.LeftToRight;
            flpUsuario.Padding = new Padding(0, 20, 20, 0);

            var flpReloj = new FlowLayoutPanel();

            var lblHora = new Texto.SM(DateTime.Now.ToString("hh:mm tt"));

            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 30000;
            timer.Tick += (sender, e) => lblHora.Text = DateTime.Now.ToString("hh:mm tt");
            timer.Start();

            flpReloj.Dock = DockStyle.Right;
            flpReloj.AutoSize = true;
            flpReloj.WrapContents = false;
            flpReloj.FlowDirection = FlowDirection.LeftToRight;
            flpReloj.Padding = new Padding(0, 20, 20, 0);

            flpReloj.Controls.Add(lblHora);

            pHeader.Controls.AddRange(new Control[] {flpCaja, flpUsuario, flpReloj});
        }

        private void BtnTurnos_Click(object sender, EventArgs e)
        {
            FrmTurnos frmTurnos = new FrmTurnos();
            frmTurnos.ShowDialog();
        }
    }
}
