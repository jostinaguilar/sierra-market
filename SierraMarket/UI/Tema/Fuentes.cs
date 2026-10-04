using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Text;

namespace SierraMarket.UI.Tema
{
    public enum Peso
    {
        Thin,
        Light,
        Regular,
        Medium,
        Semibold,
        Bold,
        ExtraBold,
        Black
    }

    public static class Fuentes
    {
        // Mantener una colección de fuentes privadas para la aplicación
        public static PrivateFontCollection ColGeistThin = new PrivateFontCollection();
        public static PrivateFontCollection ColGeistLight = new PrivateFontCollection();
        public static PrivateFontCollection ColGeistRegular = new PrivateFontCollection();
        public static PrivateFontCollection ColGeistMedium = new PrivateFontCollection();
        public static PrivateFontCollection ColGeistSemiBold = new PrivateFontCollection();
        public static PrivateFontCollection ColGeistExtraBold = new PrivateFontCollection();
        public static PrivateFontCollection ColGeistBlack = new PrivateFontCollection();

        public static void Inicializar()
        {
            // Cargar las fuentes personalizadas desde archivos de fuente
            ColGeistThin.AddFontFile("Recursos/Fuentes/GeistMono-Thin.ttf");
            ColGeistLight.AddFontFile("Recursos/Fuentes/GeistMono-Light.ttf");
            ColGeistRegular.AddFontFile("Recursos/Fuentes/GeistMono-Regular.ttf");
            ColGeistMedium.AddFontFile("Recursos/Fuentes/GeistMono-Medium.ttf");
            ColGeistSemiBold.AddFontFile("Recursos/Fuentes/GeistMono-SemiBold.ttf");
            ColGeistExtraBold.AddFontFile("Recursos/Fuentes/GeistMono-ExtraBold.ttf");
            ColGeistBlack.AddFontFile("Recursos/Fuentes/GeistMono-Black.ttf");
        }

        public static Font Geist(float tamano, Peso peso)
        {
            return peso switch
            {
                Peso.Thin => GeistThin(tamano),
                Peso.Light => GeistLight(tamano),
                Peso.Medium => GeistMedium(tamano),
                Peso.Semibold => GeistSemiBold(tamano),
                Peso.Bold => GeistRegular(tamano, FontStyle.Bold),
                Peso.ExtraBold => GeistExtraBold(tamano),
                Peso.Black => GeistBlack(tamano),
                _ => GeistRegular(tamano)
            };
        }

        public static Font GeistThin(float tamano, FontStyle estilo = FontStyle.Regular) => new Font(ColGeistThin.Families[0], tamano, estilo);
        public static Font GeistLight(float tamano, FontStyle estilo = FontStyle.Regular) => new Font(ColGeistLight.Families[0], tamano, estilo);
        public static Font GeistRegular(float tamano, FontStyle estilo = FontStyle.Regular) => new Font(ColGeistRegular.Families[0], tamano, estilo);
        public static Font GeistMedium(float tamano, FontStyle estilo = FontStyle.Regular) => new Font(ColGeistMedium.Families[0], tamano, estilo);
        public static Font GeistSemiBold(float tamano, FontStyle estilo = FontStyle.Regular) => new Font(ColGeistSemiBold.Families[0], tamano, estilo);
        public static Font GeistExtraBold(float tamano, FontStyle estilo = FontStyle.Regular) => new Font(ColGeistExtraBold.Families[0], tamano, estilo);
        public static Font GeistBlack(float tamano, FontStyle estilo = FontStyle.Regular) => new Font(ColGeistBlack.Families[0], tamano, estilo);
    }
}
