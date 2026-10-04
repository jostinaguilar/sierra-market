using System;
using System.Collections.Generic;
using System.Text;

namespace SierraMarket.UI.Componentes
{
    public class CampoEtiquetado: TableLayoutPanel
    {
        public CampoTexto.Campo Campo { get; }
        public string Valor => Campo.Text.Trim();

        public CampoEtiquetado(string etiqueta, string placeholder = "")
        {
            Campo = new CampoTexto.Campo(placeholder);

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
            Controls.Add(Campo.ConBorde(), 0, 2);
        }
    }
}
