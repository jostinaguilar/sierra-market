using SierraMarket.UI.Tema;

namespace SierraMarket.UI
{
    partial class FrmPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tlpPrincipal = new TableLayoutPanel();
            pnlHeader = new Panel();
            brdHero = new Panel();
            brdMenu = new Panel();
            brdFooter = new Panel();
            tlpVenta = new TableLayoutPanel();
            brdVenta = new Panel();
            pnlVentaActual = new Panel();
            pnlCatalogo = new Panel();
            pnlMenu = new Panel();
            tlpPrincipal.SuspendLayout();
            tlpVenta.SuspendLayout();
            SuspendLayout();
            // 
            // tlpPrincipal
            // 
            tlpPrincipal.BackColor = Color.White;
            tlpPrincipal.ColumnCount = 1;
            tlpPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tlpPrincipal.Controls.Add(pnlHeader, 0, 0);
            tlpPrincipal.Controls.Add(brdHero, 0, 1);
            tlpPrincipal.Controls.Add(brdMenu, 0, 3);
            tlpPrincipal.Controls.Add(brdFooter, 0, 5);
            tlpPrincipal.Controls.Add(tlpVenta, 0, 4);
            tlpPrincipal.Controls.Add(pnlMenu, 0, 2);
            tlpPrincipal.Dock = DockStyle.Fill;
            tlpPrincipal.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tlpPrincipal.Location = new Point(0, 0);
            tlpPrincipal.Name = "tlpPrincipal";
            tlpPrincipal.RowCount = 7;
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tlpPrincipal.Size = new Size(1008, 761);
            tlpPrincipal.TabIndex = 0;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(250, 250, 250);
            pnlHeader.Dock = DockStyle.Fill;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Margin = new Padding(0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1008, 60);
            pnlHeader.TabIndex = 5;
            // 
            // brdHero
            // 
            brdHero.BackColor = Color.FromArgb(228, 228, 231);
            brdHero.Dock = DockStyle.Fill;
            brdHero.Location = new Point(0, 60);
            brdHero.Margin = new Padding(0);
            brdHero.Name = "brdHero";
            brdHero.Size = new Size(1008, 1);
            brdHero.TabIndex = 0;
            // 
            // brdMenu
            // 
            brdMenu.BackColor = Color.FromArgb(228, 228, 231);
            brdMenu.Dock = DockStyle.Fill;
            brdMenu.Location = new Point(0, 101);
            brdMenu.Margin = new Padding(0);
            brdMenu.Name = "brdMenu";
            brdMenu.Size = new Size(1008, 1);
            brdMenu.TabIndex = 1;
            // 
            // brdFooter
            // 
            brdFooter.BackColor = Color.FromArgb(228, 228, 231);
            brdFooter.Dock = DockStyle.Fill;
            brdFooter.Location = new Point(0, 700);
            brdFooter.Margin = new Padding(0);
            brdFooter.Name = "brdFooter";
            brdFooter.Size = new Size(1008, 1);
            brdFooter.TabIndex = 2;
            // 
            // tlpVenta
            // 
            tlpVenta.ColumnCount = 3;
            tlpVenta.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tlpVenta.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 1F));
            tlpVenta.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tlpVenta.Controls.Add(brdVenta, 1, 0);
            tlpVenta.Controls.Add(pnlVentaActual, 2, 0);
            tlpVenta.Controls.Add(pnlCatalogo, 0, 0);
            tlpVenta.Dock = DockStyle.Fill;
            tlpVenta.Location = new Point(0, 102);
            tlpVenta.Margin = new Padding(0);
            tlpVenta.Name = "tlpVenta";
            tlpVenta.RowCount = 1;
            tlpVenta.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpVenta.Size = new Size(1008, 598);
            tlpVenta.TabIndex = 3;
            // 
            // brdVenta
            // 
            brdVenta.BackColor = Color.FromArgb(228, 228, 231);
            brdVenta.Dock = DockStyle.Fill;
            brdVenta.Location = new Point(654, 0);
            brdVenta.Margin = new Padding(0);
            brdVenta.Name = "brdVenta";
            brdVenta.Size = new Size(1, 598);
            brdVenta.TabIndex = 3;
            // 
            // pnlVentaActual
            // 
            pnlVentaActual.BackColor = Color.FromArgb(250, 250, 250);
            pnlVentaActual.Dock = DockStyle.Fill;
            pnlVentaActual.Location = new Point(655, 0);
            pnlVentaActual.Margin = new Padding(0);
            pnlVentaActual.Name = "pnlVentaActual";
            pnlVentaActual.Size = new Size(353, 598);
            pnlVentaActual.TabIndex = 4;
            // 
            // pnlCatalogo
            // 
            pnlCatalogo.BackColor = Color.Transparent;
            pnlCatalogo.Dock = DockStyle.Fill;
            pnlCatalogo.Location = new Point(0, 0);
            pnlCatalogo.Margin = new Padding(0);
            pnlCatalogo.Name = "pnlCatalogo";
            pnlCatalogo.Size = new Size(654, 598);
            pnlCatalogo.TabIndex = 5;
            // 
            // pnlMenu
            // 
            pnlMenu.BackColor = Color.Transparent;
            pnlMenu.Dock = DockStyle.Fill;
            pnlMenu.Location = new Point(0, 61);
            pnlMenu.Margin = new Padding(0);
            pnlMenu.Name = "pnlMenu";
            pnlMenu.Size = new Size(1008, 40);
            pnlMenu.TabIndex = 6;
            // 
            // FrmPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1008, 761);
            Controls.Add(tlpPrincipal);
            Name = "FrmPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Inicio | Sierra Market";
            tlpPrincipal.ResumeLayout(false);
            tlpVenta.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpPrincipal;
        private Panel brdHero;
        private Panel brdMenu;
        private Panel brdFooter;
        private TableLayoutPanel tlpVenta;
        private Panel brdVenta;
        private Panel pnlVentaActual;
        private Panel pnlHeader;
        private Panel pnlMenu;
        private Panel pnlCatalogo;
    }
}