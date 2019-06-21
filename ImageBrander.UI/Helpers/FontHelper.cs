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
        }
    }
}
