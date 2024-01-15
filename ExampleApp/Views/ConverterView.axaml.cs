using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using ExampleApp.ViewModels;

namespace ExampleApp.Views;

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
        
        if(input == "autotest")
            ConverterViewModel.Autotest();

        if (ConverterViewModel.CountryCodes.ContainsKey(input))
            ConverterViewModel.Instance.GetCountry(input);
    }

    private async void Button_SaveImage_OnClick(object? sender, RoutedEventArgs e)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        
        var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = "Save Image",
            SuggestedFileName = this.FlagImage.FileName
        });
        
        if (file is not null)
        {
            // Open writing stream from the file.
            await using var stream = await file.OpenWriteAsync();
            (this.FlagImage.Source as Bitmap).Save(stream, 10);
        }
    }
}