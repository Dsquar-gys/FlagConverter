using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Avalonia.Data.Converters;
using Avalonia.Media;
using ConverterApp.Models;

namespace ConverterApp.Converter;

public class FlagConverter : IValueConverter
{
    public static readonly FlagConverter Instance = new();
    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Country country)
        {
            string imageUri = "avares://ConverterApp/Assets";
            if (targetType.IsAssignableTo(typeof(IImage)))
            {
                var imageName = DefineCorrectName(country.Name);
                if (imageName is null)
                {
                    // Return Default image
                }

                imageUri = Path.Combine(imageUri, "Flags");
                imageUri = Path.Combine(imageUri, imageName);
                return ImageHelper.LoadFromResource(new Uri(imageUri));
            }
            else if(targetType.IsAssignableTo(typeof(string)))
            {
                // Output as a string
                return country.Name;
            }
            
        }

        return null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
    
    private string DefineCorrectName(string countryName)
    {
        // Names without "and", "the", and taken before brackets or "of"
        countryName = new string(countryName.ToLower()
            .TakeWhile(x => x != '[' || x != '(')
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
            return null;
        }

        return corName;
    }
}