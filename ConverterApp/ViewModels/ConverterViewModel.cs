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
    public static bool Findable = false;
    public string Input => null;
    
    
    private string imagePath = "123";

    public string ImagePath
    {
        get => imagePath;
        set => this.RaiseAndSetIfChanged(ref imagePath, value);
    }

    public ConverterViewModel() => Instance = Instance is null ? this : Instance;
    
    public void GetImagePath(string countryCode) => ImagePath = CountryCodes[countryCode];
}