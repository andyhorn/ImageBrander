using ImageBrander.Engine;
using ImageBrander.Engine.Models;
using ImageBrander.UI.Helpers;
using System.Collections.Generic;
using System.ComponentModel;

namespace ImageBrander.UI.ViewModels
{
    public class MainWindowViewModel : IMainWindowViewModel, INotifyPropertyChanged
    {
        private IImageEngine engine;
        private IImage image;
        private string text;

        public IImage Image { get => image; set => image = value; }
        public byte[] ImageBytes => Image.Bytes;
        public IList<System.Drawing.FontFamily> FontList { get; set; }
        public IList<System.Drawing.Color> ColorList { get; set; }
        public System.Drawing.FontFamily SelectedFont { get; set; }
        public System.Drawing.Color SelectedColor { get; set; }
        public string Text { get => text; set => UpdateText(value); }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
            Image = new Image();
            engine = new ImageEngine();
            FontList = FontHelper.GetAllFonts();
            ColorList = ColorHelper.GetAllColors();

            SelectedColor = System.Drawing.Color.Black;
            SelectedFont = new System.Drawing.FontFamily("Segoe UI");
        }

        public void SaveImage(string watermarkText)
        {

            var watermark = new Watermark
            {
                FillColor = new ImageMagick.MagickColor("Black"),
                Font = new ImageMagick.DrawableFont("Calibri"),
                FontSize = 36,
                StrokeColor = new ImageMagick.MagickColor("Black"),
                X = image.Width / 2,
                Y = image.Height / 2,
                Text = watermarkText
            };

            engine.Watermark(ref image, watermark);
            engine.SaveImage(ref image, @"C:/Users/Andy/Desktop/output");
        }

        public async void OpenFile(string fileName)
        {
            image = await engine.OpenAsync(fileName);
            OnPropertyChanged("ImageBytes");
        }

        private void UpdateText(string str)
        {
            text = str;
            OnPropertyChanged("Text");
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
