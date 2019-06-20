using System.Collections.Generic;
using System.Linq;

namespace ImageBrander.UI.Helpers
{
    public static class FontHelper
    {
        public static List<System.Drawing.FontFamily> GetAllFonts()
        {
            var installedFonts = new System.Drawing.Text.InstalledFontCollection();

            var fontArray = installedFonts.Families;

            var fontList = fontArray.ToList().SkipWhile(fam => string.IsNullOrWhiteSpace(fam.Name)).ToList();

            return fontList;
        }

        public static System.Windows.Media.FontFamily GetMediaFont(System.Drawing.FontFamily drawingFont)
        {
            var fontName = drawingFont.Name;

            System.Windows.Media.FontFamily fam = new System.Windows.Media.FontFamily(fontName);

            return fam;
            /*

            //var drawingFont = new System.Drawing.FontFamily(mediaFontName);

            System.Drawing.FontFamily font;
            var fontConverter = new System.Drawing.FontConverter();

            if(fontConverter.CanConvertFrom(typeof(System.Windows.Media.FontFamily)))
            {
                try
                {
                    font = (System.Drawing.FontFamily)fontConverter.ConvertFromString(mediaFontName);
                    return font;
                }
                catch(NotSupportedException)
                {
                    // if this conversion didn't work, allow it to fall through
                }
            }

            font = new System.Drawing.FontFamily(mediaFontName);
            return font;
            */
        }
    }
}
