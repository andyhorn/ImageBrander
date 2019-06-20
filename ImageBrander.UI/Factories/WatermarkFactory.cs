using ImageBrander.Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageBrander.UI.Factories
{
    public static class WatermarkFactory
    {
        public static Watermark Make(
            string content,
            System.Drawing.Color color, 
            System.Drawing.FontFamily font, 
            int fontSize,
            System.Drawing.Color strokeColor,
            int xPos,
            int yPos)
        {
            var watermark = new Watermark()
            {
                FillColor = new ImageMagick.MagickColor(color),
                StrokeColor = new ImageMagick.MagickColor(strokeColor),
                Font = new ImageMagick.DrawableFont(font.Name),
                FontSize = fontSize,
                Text = content,
                X = xPos,
                Y = yPos
            };

            return watermark;
        }
    }
}
