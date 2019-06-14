using ImageMagick;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageBrander.Engine.Models
{
    public class Watermark : IOverlay
    {
        public string Text { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int FontSize { get; set; }
        public MagickColor FillColor { get; set; }
        public MagickColor StrokeColor { get; set; }
        public DrawableFont Font { get; set; }
    }
}
