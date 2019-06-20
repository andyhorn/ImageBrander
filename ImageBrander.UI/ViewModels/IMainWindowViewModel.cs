using ImageBrander.Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageBrander.UI.ViewModels
{
    public interface IMainWindowViewModel
    {
        IImage Image { get; set; }
        void SaveImage(string watermarkText);
        void OpenFile(string filename);

        IList<System.Drawing.FontFamily> FontList { get; set; }
        IList<System.Drawing.Color> ColorList { get; set; }
        System.Drawing.FontFamily SelectedFont { get; set; }
        System.Drawing.Color SelectedColor { get; set; }
        string Text { get; set; }
        int FontSize { get; set; }
    }
}
