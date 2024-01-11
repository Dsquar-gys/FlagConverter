using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using ConverterApp.Models;
using ReactiveUI;

namespace ConverterApp.ViewModels;

public class ConverterViewModel : ViewModelBase
{
    public static ConverterViewModel Instance = null;
    public static readonly Dictionary<string, string> CountryCodes = ConfigManager.CountryCodes_To_Names;
    
    // One-way property for text box input data
    public string Input => null;

    // Property for flag image
    private Bitmap? imageFromBinding = null;
    public Bitmap? ImageFromBinding
    {
        get => imageFromBinding;
        set => this.RaiseAndSetIfChanged(ref imageFromBinding, value);
    }
    
    // Property for download button to be enabled or not
    public bool Downloadable
    {
        get => imageFromBinding != null;
        set => this.RaisePropertyChanged();
    }
    
    // Property for image tag to contain saving name
    private string _correctName;
    public string CorrectName
    {
        get => _correctName;
        set => this.RaiseAndSetIfChanged(ref _correctName, value);
    }

    // Sets static field 'Instance' just once
    public ConverterViewModel() => Instance = Instance is null ? this : Instance;
    
    // Query for the proper image
    public void GetImage(string countryKey)
    {
        var CountryName = CountryCodes[countryKey];
        CorrectName = DefineCorrectName(CountryName);
        ImageFromBinding = CorrectName is null ? null : ImageHelper.LoadFromResource(new Uri("avares://ConverterApp/Assets/Flags/" + CorrectName));
        Downloadable = true;
    }

    private string DefineCorrectName(string countryName)
    {
        countryName = new string(countryName.Where(ch => Char.IsLetter(ch)).ToArray());
        string corName;
        try
        {
            corName = ConfigManager.FlagNames.Where(fName => fName.ContainsAll(countryName.Split(' '))).ToArray()[0];
        }
        catch (Exception e)
        {
            Console.WriteLine("Flag for {0} not found", countryName);
            return null;
        }

        return corName;
    }
}