using System;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using ConverterApp.ViewModels;
using SkiaSharp;

namespace ConverterApp.Views;

public partial class ConverterView : UserControl
{
    public ConverterView()
    {
        InitializeComponent();
    }

    // Handler on text changed event
    private void TextBox_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        var textBox = sender as TextBox;
        string input = textBox.Text.ToLower();

        if (input != null )
            if (ConverterViewModel.CountryCodes.ContainsKey(input))
                ConverterViewModel.Instance.GetImage(input);
    }

    private async void Button_SaveImage_OnClick(object? sender, RoutedEventArgs e)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        
        var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = "Save Image",
            SuggestedFileName = this.FlagImage.Tag.ToString()
        });
        
        if (file is not null)
        {
            // Open writing stream from the file.
            await using var stream = await file.OpenWriteAsync();
            (this.FlagImage.Source as Bitmap).Save(stream, 10);
        }
    }
}