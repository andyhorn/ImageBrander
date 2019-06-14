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
    }
}
