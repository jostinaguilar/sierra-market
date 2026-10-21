using System;
using System.Collections.Generic;
using System.Text;

namespace SierraMarket.UI.Componentes
{
    public class SelectorFechaHoraEtiquetado: TableLayoutPanel
    {
        public SelectorFechaHora.Selector Selector { get; }

        public string Valor => Selector.Value.ToString("HH:mm");

        public SelectorFechaHoraEtiquetado(string etiqueta)
        {
            Selector = new SelectorFechaHora.Selector();

            Dock = DockStyle.Fill;
            ColumnCount = 1;
            RowCount = 3;
            Height = 55;
            Margin = new Padding(0, 0, 0, 10);

            ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));

            Controls.Add(new Texto.Etiqueta(etiqueta), 0, 0);
            Controls.Add(Selector.ConBorde(), 0, 2);
        }
    }
}
