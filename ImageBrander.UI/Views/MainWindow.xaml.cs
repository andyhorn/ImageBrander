using ImageBrander.UI.ViewModels;
using Microsoft.Win32;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ImageBrander.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IMainWindowViewModel viewModel;
        private Button selectPhotoButton, savePhotoButton;
        private TextBox watermarkTextBox;
        private ComboBox colorComboBox, fontComboBox;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainWindowViewModel();
            Setup();
            DataContext = viewModel;
        }

        private void Setup()
        {
            selectPhotoButton = SelectPhotoButton;
            savePhotoButton = SaveButton;
            watermarkTextBox = WatermarkTextBox;
            colorComboBox = ColorComboBox;
            fontComboBox = FontComboBox;

            fontComboBox.SelectionChanged += new SelectionChangedEventHandler((sender, args) => FontChanged(sender, args));
            colorComboBox.SelectionChanged += new SelectionChangedEventHandler((sender, args) => ColorChanged(sender, args));

            selectPhotoButton.Click += new RoutedEventHandler((sender, args) => OpenPhoto(sender, args));
            savePhotoButton.Click += new RoutedEventHandler((sender, args) => SavePhoto(sender, args));

            watermarkTextBox.TextChanged += new TextChangedEventHandler((sender, args) => UpdateText(sender, args));
        }

        private void OpenPhoto(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();

            if(dialog.ShowDialog() == true)
            {
                viewModel.OpenFile(dialog.FileName);
            }
        }

        private void SavePhoto(object sender, RoutedEventArgs e)
        {
            if(viewModel.Image.Bytes != null)
            {
                string brand = string.Empty;

                if (!string.IsNullOrWhiteSpace(watermarkTextBox.Text))
                {
                    brand = watermarkTextBox.Text;
                }

                viewModel.SaveImage(brand);
            }
        }

        private void FontChanged(object sender, RoutedEventArgs e)
        {
            viewModel.SelectedFont = (System.Drawing.FontFamily)fontComboBox.SelectedItem;
        }

        private void ColorChanged(object sender, RoutedEventArgs e)
        {
            viewModel.SelectedColor = (System.Drawing.Color)colorComboBox.SelectedItem;
        }

        private void UpdateText(object sender, TextChangedEventArgs e)
        {
            viewModel.Text = watermarkTextBox.Text;
        }
    }
}
