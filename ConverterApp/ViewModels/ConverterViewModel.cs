using System;
using System.Collections.Generic;
using ConverterApp.Converter;
using ConverterApp.Models;
using ReactiveUI;

namespace ConverterApp.ViewModels;

public class ConverterViewModel : ViewModelBase
{
    public static ConverterViewModel Instance = null;
    public static readonly Dictionary<string, Country> CountryCodes = ConfigManager.CountryCodes_To_Names;
    private static int notFoundCounter = 0;
    
    // One-way property for text box input data
    public string Input => null;

    // Property for binding with converter
    private Country _country;
    public Country Country
    {
        get => _country;
        set => this.RaiseAndSetIfChanged(ref _country, value);
    }
    
    // Property for download button to be enabled or not
    private bool _downloadable;
    public bool Downloadable
    {
        get => _downloadable;
        set => this.RaiseAndSetIfChanged(ref _downloadable, value);
    }

    // For image tag
    private string _fileName;
    public string FileName
    {
        get => _fileName;
        set => this.RaiseAndSetIfChanged(ref _fileName, value);
    }

    // Sets static field 'Instance' just once
    public ConverterViewModel() => Instance = Instance is null ? this : Instance;

    // To push country by query
    public void GetCountry(string countryKey) => Country = CountryCodes[countryKey];
    
    // Autotest for checking name validation
    public static void Autotest()
    {
        notFoundCounter = 0;
        foreach (var country in ConfigManager.Countries)
        {
            if (FlagConverter.DefineCorrectName(CountryCodes[country.OKSM.ToString()].Name) == null)
                notFoundCounter++;
        }
        Console.WriteLine("Not Found: {0}", notFoundCounter);
    }
}