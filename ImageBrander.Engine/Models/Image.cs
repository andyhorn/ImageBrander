namespace ImageBrander.Engine.Models
{
    public class Image : IImage
    {
        public byte[] Bytes { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public ImageMagick.MagickFormat Format { get; set; }
        public string Name { get; set; }
    }
}
