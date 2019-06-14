using ImageBrander.Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageBrander.Engine
{
    public interface IImageEngine
    {
        void Watermark(ref IImage imageData, Watermark watermark);
        void SaveImage(ref IImage imageData, string fileName);
        Task<IImage> OpenAsync(string filename);
    }
}
