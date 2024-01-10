using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ConverterApp.ViewModels;

namespace ConverterApp.Views;

public partial class ConverterView : UserControl
{
    public ConverterView()
    {
        InitializeComponent();
    }

    private void TextBox_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        var textBox = sender as TextBox;
        string input = textBox.Text;
        string result;

        if (input != null )
            if (ConverterViewModel.CountryCodes.TryGetValue(input, out result))
                ConverterViewModel.Instance.GetImagePath(input);
    }
}