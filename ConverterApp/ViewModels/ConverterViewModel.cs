using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using ConverterApp.Models;
using ReactiveUI;

namespace ConverterApp.ViewModels;

public class ConverterViewModel : ViewModelBase
{
    public static ConverterViewModel Instance = null;
    public static readonly Dictionary<string, string> CountryCodes = ConfigManager.CountryCodes;
    public string Input => null;

    private Bitmap? imageFromBinding = ImageHelper.LoadFromResource(new Uri("avares://ConverterApp/Assets/uk.png"));
    public Bitmap? ImageFromBinding
    {
        get => imageFromBinding;
        set => this.RaiseAndSetIfChanged(ref imageFromBinding, value);
    }

    public ConverterViewModel() => Instance = Instance is null ? this : Instance;
    
    public void GetImagePath(string countryCode) => ImageFromBinding = ImageHelper.LoadFromResource(new Uri("avares://ConverterApp/Assets/" + CountryCodes[countryCode]));
}