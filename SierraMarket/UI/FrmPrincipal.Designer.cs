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
            TlpPrincipal = new TableLayoutPanel();
            PHeader = new Panel();
            brdHero = new Panel();
            brdMenu = new Panel();
            brdFooter = new Panel();
            TlpVenta = new TableLayoutPanel();
            brdVenta = new Panel();
            PVentaActual = new Panel();
            label1 = new Label();
            TlpPrincipal.SuspendLayout();
            PHeader.SuspendLayout();
            TlpVenta.SuspendLayout();
            SuspendLayout();
            // 
            // TlpPrincipal
            // 
            TlpPrincipal.BackColor = Color.White;
            TlpPrincipal.ColumnCount = 1;
            TlpPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            TlpPrincipal.Controls.Add(PHeader, 0, 0);
            TlpPrincipal.Controls.Add(brdHero, 0, 1);
            TlpPrincipal.Controls.Add(brdMenu, 0, 3);
            TlpPrincipal.Controls.Add(brdFooter, 0, 5);
            TlpPrincipal.Controls.Add(TlpVenta, 0, 4);
            TlpPrincipal.Dock = DockStyle.Fill;
            TlpPrincipal.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            TlpPrincipal.Location = new Point(0, 0);
            TlpPrincipal.Name = "TlpPrincipal";
            TlpPrincipal.RowCount = 7;
            TlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            TlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));
            TlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            TlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));
            TlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            TlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));
            TlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            TlpPrincipal.Size = new Size(1008, 761);
            TlpPrincipal.TabIndex = 0;
            // 
            // PHeader
            // 
            PHeader.BackColor = Color.FromArgb(250, 250, 250);
            PHeader.Controls.Add(label1);
            PHeader.Dock = DockStyle.Fill;
            PHeader.Location = new Point(0, 0);
            PHeader.Margin = new Padding(0);
            PHeader.Name = "PHeader";
            PHeader.Size = new Size(1008, 60);
            PHeader.TabIndex = 5;
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
            // TlpVenta
            // 
            TlpVenta.ColumnCount = 3;
            TlpVenta.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            TlpVenta.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 1F));
            TlpVenta.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            TlpVenta.Controls.Add(brdVenta, 1, 0);
            TlpVenta.Controls.Add(PVentaActual, 2, 0);
            TlpVenta.Dock = DockStyle.Fill;
            TlpVenta.Location = new Point(0, 102);
            TlpVenta.Margin = new Padding(0);
            TlpVenta.Name = "TlpVenta";
            TlpVenta.RowCount = 1;
            TlpVenta.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            TlpVenta.Size = new Size(1008, 598);
            TlpVenta.TabIndex = 3;
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
            // PVentaActual
            // 
            PVentaActual.BackColor = Color.FromArgb(250, 250, 250);
            PVentaActual.Dock = DockStyle.Fill;
            PVentaActual.Location = new Point(655, 0);
            PVentaActual.Margin = new Padding(0);
            PVentaActual.Name = "PVentaActual";
            PVentaActual.Size = new Size(353, 598);
            PVentaActual.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(23, 24);
            label1.Name = "label1";
            label1.Size = new Size(76, 15);
            label1.TabIndex = 0;
            label1.Text = "Sierra Market";
            // 
            // FrmPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1008, 761);
            Controls.Add(TlpPrincipal);
            Name = "FrmPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Inicio | Sierra Market";
            TlpPrincipal.ResumeLayout(false);
            PHeader.ResumeLayout(false);
            PHeader.PerformLayout();
            TlpVenta.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel TlpPrincipal;
        private Panel brdHero;
        private Panel brdMenu;
        private Panel brdFooter;
        private TableLayoutPanel TlpVenta;
        private Panel brdVenta;
        private Panel PVentaActual;
        private Panel PHeader;
        private Label label1;
    }
}