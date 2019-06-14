using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageBrander.Engine.Models
{
    public interface IImage
    {
        byte[] Bytes { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        ImageMagick.MagickFormat Format { get; set; }
        string Name { get; set; }
    }
}
