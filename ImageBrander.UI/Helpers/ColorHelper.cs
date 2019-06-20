using System.Collections.Generic;

namespace ImageBrander.UI.Helpers
{
    public static class ColorHelper
    {
        public static List<System.Drawing.Color> GetAllColors()
        {
            var colorList = new List<System.Drawing.Color>();

            var colorType = typeof(System.Drawing.Color);

            var propertyInfoArray = colorType.GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.DeclaredOnly | System.Reflection.BindingFlags.Public);

            foreach(var property in propertyInfoArray)
            {
                if(property.PropertyType == colorType)
                {
                    colorList.Add(System.Drawing.Color.FromName(property.Name));
                }
            }

            return colorList;
        }

        public static System.Windows.Media.Color GetMediaColor(System.Drawing.Color drawingColor)
        {
            var a = drawingColor.A;
            var r = drawingColor.R;
            var g = drawingColor.G;
            var b = drawingColor.B;

            var color = new System.Windows.Media.Color
            {
                A = a,
                R = r,
                G = g,
                B = b
            };

            return color;
        }

        public static System.Windows.Media.Brush GetMediaBrush(System.Drawing.Color drawingColor)
        {
            var color = GetMediaColor(drawingColor);

            var mediaBrush = new System.Windows.Media.SolidColorBrush(color);

            return mediaBrush;
        }
    }
}
