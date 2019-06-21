using ImageBrander.UI.Models;
using ImageBrander.UI.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

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
        private IntegerUpDown fontSizePicker;
        //private RadioButton topLeft, topMiddle, topRight, middleLeft, middle, middleRight, bottomLeft, bottomMiddle, bottomRight;
        private Collection<RadioButton> textPositionSelectors;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainWindowViewModel();
            textPositionSelectors = new Collection<RadioButton>();
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
            fontSizePicker = FontSizePicker;

            //topLeft = TopLeftRadio;
            //topMiddle = TopMiddleRadio;
            //topRight = TopRightRadio;
            //middleLeft = MiddleLeftRadio;
            //middle = MiddleRadio;
            //middleRight = MiddleRightRadio;
            //bottomLeft = BottomLeftRadio;
            //bottomMiddle = BottomMiddleRadio;
            //bottomRight = BottomRightRadio;

            //textPositionSelectors.Ad

            textPositionSelectors.Add(TopLeftRadio);
            textPositionSelectors.Add(TopMiddleRadio);
            textPositionSelectors.Add(TopRightRadio);
            textPositionSelectors.Add(MiddleLeftRadio);
            textPositionSelectors.Add(MiddleRadio);
            textPositionSelectors.Add(MiddleRightRadio);
            textPositionSelectors.Add(BottomLeftRadio);
            textPositionSelectors.Add(BottomMiddleRadio);
            textPositionSelectors.Add(BottomRightRadio);

            fontComboBox.SelectionChanged += new SelectionChangedEventHandler((sender, args) => FontChanged(sender, args));
            colorComboBox.SelectionChanged += new SelectionChangedEventHandler((sender, args) => ColorChanged(sender, args));

            selectPhotoButton.Click += new RoutedEventHandler((sender, args) => OpenPhoto(sender, args));
            savePhotoButton.Click += new RoutedEventHandler((sender, args) => SavePhoto(sender, args));

            watermarkTextBox.TextChanged += new TextChangedEventHandler((sender, args) => UpdateText(sender, args));

            fontSizePicker.ValueChanged += new RoutedPropertyChangedEventHandler<object>((sender, args) => UpdateFontSize(sender, args));

            foreach(var radio in textPositionSelectors)
            {
                radio.Click += new RoutedEventHandler((sender, args) => PositionChanged(sender, args));
            }
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

        private void UpdateFontSize(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            viewModel.FontSize = FontSizePicker.Value?? 0;
        }

        private void PositionChanged(object sender, RoutedEventArgs e)
        {
            var waterWidth = WatermarkDisplay.ActualWidth;
            var waterHeight = WatermarkDisplay.ActualHeight;
            var displayWidth = ImageDisplay.ActualWidth;
            var displayHeight = ImageDisplay.ActualHeight;

            viewModel.WatermarkWidth = waterWidth;
            viewModel.WatermarkHeight = waterHeight;

            viewModel.DisplayHeight = displayHeight;
            viewModel.DisplayWidth = displayWidth;

            var radio = sender as RadioButton;
            viewModel.TextPosition = radio.Name;

            MoveWatermark();
        }

        private void MoveWatermark()
        {
            WatermarkDisplay.RenderTransform = new TranslateTransform(viewModel.X, viewModel.Y);
        }
    }
}
