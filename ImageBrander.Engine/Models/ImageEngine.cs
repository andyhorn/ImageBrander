using ImageMagick;
using System.IO;
using System.Threading.Tasks;

namespace ImageBrander.Engine.Models
{
    public class ImageEngine : IImageEngine
    {
        public void SaveImage(ref IImage imageData, string fileName)
        {
            var image = new MagickImage();
            image.Read(imageData.Bytes);

            var format = image.Format.ToString();

            image.Write($"{fileName}.{format}");
        }

        public void Watermark(ref IImage imageData, Watermark watermark)
        {
            var image = new MagickImage();
            image.Read(imageData.Bytes);

            var format = image.Format;

            new Drawables()
                .FontPointSize(watermark.FontSize)
                .Font(watermark.Font.ToString())
                .StrokeColor(watermark.StrokeColor)
                .FillColor(watermark.FillColor)
                .Text(watermark.X, watermark.Y, watermark.Text)
                .Draw(image);

            imageData.Bytes = image.ToByteArray(format);
        }

        public async Task<IImage> OpenAsync(string filename)
        {
            var file = File.OpenRead(filename);

            if(file.CanRead)
            {
                var length = (int)file.Length;
                var bytes = new byte[length];

                await file.ReadAsync(bytes, 0, length);

                var fileInfo = new MagickImageInfo(bytes);

                var image = new Image()
                {
                    Format = fileInfo.Format,
                    Bytes = bytes,
                    Height = fileInfo.Height,
                    Width = fileInfo.Width,
                    Name = filename
                };

                return image;
            }

            return null;
        }
    }
}
