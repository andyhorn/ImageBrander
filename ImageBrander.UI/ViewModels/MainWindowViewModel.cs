using ImageBrander.Engine;
using ImageBrander.Engine.Models;
using ImageBrander.UI.Factories;
using ImageBrander.UI.Helpers;
using ImageBrander.UI.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace ImageBrander.UI.ViewModels
{
    public class MainWindowViewModel : IMainWindowViewModel, INotifyPropertyChanged
    {
        private IImageEngine engine;
        private IImage image;
        private string text, textPosition;
        private System.Drawing.FontFamily font;
        private System.Drawing.Color color;
        private int fontSize;
        private double x, y;

        public IImage Image { get => image; set => image = value; }
        public byte[] ImageBytes => Image.Bytes;
        public IList<System.Drawing.FontFamily> FontList { get; set; }
        public IList<System.Drawing.Color> ColorList { get; set; }
        public System.Drawing.FontFamily SelectedFont { get => font; set => UpdateFont(value); }
        public System.Windows.Media.FontFamily DisplayFont { get => FontHelper.GetMediaFont(font); }
        public System.Drawing.Color SelectedColor { get => color; set => UpdateColor(value); }
        public System.Windows.Media.Brush DisplayColor { get => ColorHelper.GetMediaBrush(color); }
        public string Text { get => text; set => UpdateText(value); }
        public int FontSize { get => fontSize; set => UpdateFontSize(value); }
        public string TextPosition { get => textPosition; set => UpdateTextPosition(value); }
        public double X { get => x; }
        public double Y { get => y; }
        public double WatermarkHeight { get; set; }
        public double WatermarkWidth { get; set; }
        public double DisplayHeight { get; set; }
        public double DisplayWidth { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
            Image = new Image();
            engine = new ImageEngine();
            FontList = FontHelper.GetAllFonts();
            ColorList = ColorHelper.GetAllColors();

            SelectedColor = Constants.DEFAULT_FONT_COLOR;
            SelectedFont = Constants.DEFAULT_FONT_FAMILY;
            FontSize = Constants.DEFAULT_FONT_SIZE;
        }

        public void SaveImage(string watermarkText)
        {
            var watermark = WatermarkFactory.Make(watermarkText, color, font, fontSize, System.Drawing.Color.Black, 0, 0);

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

        private void UpdateFont(System.Drawing.FontFamily fam)
        {
            font = fam;
            OnPropertyChanged("SelectedFont");
            OnPropertyChanged("DisplayFont");
        }

        private void UpdateColor(System.Drawing.Color col)
        {
            color = col;
            OnPropertyChanged("SelectedColor");
            OnPropertyChanged("DisplayColor");
        }

        private void UpdateFontSize(int size)
        {
            fontSize = size;
            OnPropertyChanged("FontSize");
        }

        private void UpdateTextPosition(string pos)
        {
            textPosition = pos;

            var position = Position.GetPoint(textPosition.Substring(0, textPosition.IndexOf("Radio")));
            
            UpdateCoordinates(position);
        }

        private void UpdateCoordinates(Position.Point location)
        {
            SetXPosition(location);
            SetYPosition(location);
        }

        private void SetXPosition(Position.Point location)
        {
            switch(location)
            {
                case Position.Point.TopMiddle:
                case Position.Point.Middle:
                case Position.Point.BottomMiddle:
                    x = (DisplayWidth / 2) - (WatermarkWidth / 2);
                    break;
                case Position.Point.TopRight:
                case Position.Point.MiddleRight:
                case Position.Point.BottomRight:
                    x = DisplayWidth - WatermarkWidth;
                    break;
                default:
                    x = 0;
                    break;
            }
        }

        private void SetYPosition(Position.Point location)
        {
            // label is positioned relative to the top-left corner
            switch(location)
            {
                case Position.Point.BottomLeft:
                case Position.Point.BottomMiddle:
                case Position.Point.BottomRight:
                    y = DisplayHeight - WatermarkHeight;
                    break;
                case Position.Point.Middle:
                case Position.Point.MiddleLeft:
                case Position.Point.MiddleRight:
                    y = (DisplayHeight / 2) - (WatermarkHeight / 2);
                    break;
                default:
                    y = 0;
                    break;
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
