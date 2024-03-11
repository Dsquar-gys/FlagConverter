using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using CountryConverter;
using DynamicData.Binding;
using ReactiveUI;

namespace ExampleApp.ViewModels;

public class ConverterViewModel : ViewModelBase
{
    private static int _notFoundCounter;
    private string? _input;
    
    // One-way property for text box input data
    public string? Input
    {
        get => _input;
        set => this.RaiseAndSetIfChanged(ref _input, value);
    }

    // Property for binding with converter
    private Country? _country;
    public Country? Country
    {
        get => _country;
        set => this.RaiseAndSetIfChanged(ref _country, value);
    }

    // To push country by query
    public void GetCountry(string countryKey) => Country = ConfigManager.CountryCodesToNames[countryKey];
    
    // Autotest for checking name validation
    public static void Autotest()
    {
        _notFoundCounter = 0;
        foreach (var country in ConfigManager.Countries)
            if (CountryConverter.CountryConverter.DefineCorrectName(ConfigManager.CountryCodesToNames[country.OKSM.ToString()].Name) == Resources.DefaultFileName)
                _notFoundCounter++;
        
        Console.WriteLine("Not Found: {0}", _notFoundCounter);
    }

    public ReactiveCommand<Window, Unit> SetAssetsCommand => ReactiveCommand.CreateFromTask<Window>(async window =>
    {
        Resources.ChangeAssetsFolder(new DirectoryInfo(await new OpenFolderDialog().ShowAsync(window)));
        Country = null;
        Input = null;
    });
    public ReactiveCommand<Unit, Unit> ResetAssetsCommand => ReactiveCommand.Create(() =>
    {
        Resources.ResetAssets();
        Country = null;
        Input = null;
    });
    public ReactiveCommand<Window, Unit> SaveImageCommand => ReactiveCommand.CreateFromTask<Window>(async window =>
    {
        var dialog = new SaveFileDialog
        {
            DefaultExtension = ".png",
            InitialFileName = Country!.Name + ".png"
        };

        var path = await dialog.ShowAsync(window);
        
        if (path is not null)
        {
            // Open writing stream.
            await using var stream = new StreamWriter(path);
            Resources.Images[CountryConverter.CountryConverter.CorrectName].Save(stream.BaseStream, 10);
        }
    });
    public ReactiveCommand<Unit, Unit> CheckCountryCommand => ReactiveCommand.Create(() =>
    {
        var input = Input?.ToLower();
        
        if(input == "autotest")
            Autotest();

        if (input is null) return;
        if (ConfigManager.CountryCodesToNames.ContainsKey(input))
            GetCountry(input);
    });
}