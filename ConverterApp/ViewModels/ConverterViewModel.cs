using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Media.Imaging;
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
    
    // Property for flag image
    private Bitmap? imageFromBinding; // TO DELETE
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
    private string _correctName; // TO DELETE
    public string CorrectName
    {
        get => _correctName;
        set => this.RaiseAndSetIfChanged(ref _correctName, value);
    }

    // Sets static field 'Instance' just once
    public ConverterViewModel() => Instance = Instance is null ? this : Instance;

    public void GetCountry(string countryKey) => Country = CountryCodes[countryKey];
    
    // Query for the proper image
    public void GetImage(string countryKey) // TO DELETE
    {
        var CountryName = CountryCodes[countryKey];

        var s = Country;
        
        CorrectName = DefineCorrectName(CountryName.Name);
        ImageFromBinding = CorrectName is null ? null : ImageHelper.LoadFromResource(new Uri("avares://ConverterApp/Assets/Flags/" + CorrectName));
        Downloadable = true;
    }

    private string DefineCorrectName(string countryName)
    {
        // Names without "and", "the", and taken before brackets or "of"
        countryName = new string(countryName.TakeWhile(x => x != '[' || x != '(')
                                            .Where(ch => Char.IsLetter(ch) || ch == ' ' || ch == '-')
                                            .ToArray());
        
        var splitted = countryName.Split(' ')
                                        .Where(w => w != "and" && w != "the")
                                        .TakeWhile(w => w != "of")
                                        .ToArray();
        
        string corName; // correct flag name
        try
        {
            corName = ConfigManager.FlagNames.Where(fName => fName.ContainsAll(splitted)).ToArray()[0];
        }
        catch (Exception e)
        {
            Console.WriteLine("Flag for {0} not found", countryName);
            notFoundCounter++;
            return null;
        }

        return corName;
    }

    // Autotest for checking name validation
    public static void Autotest()
    {
        foreach (var country in ConfigManager.Countries)
            Instance.DefineCorrectName(CountryCodes[country.OKSM.ToString()].Name);
        
        Console.WriteLine("Not Found: {0}", notFoundCounter);
    }
}