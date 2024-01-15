using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace CountryConverter;

public class CountryConverter : IValueConverter
{
    public static string CorrectName { get; private set; } = ImageHelper.DefaultFileName;

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Country country)
        {
            string imageUri = "avares://ExampleApp/Assets";
            
            // Output as image
            if (targetType.IsAssignableTo(typeof(IImage)))
            {
                imageUri = DefineCorrectName(country.Name) == ImageHelper.DefaultFileName
                    ? Path.Combine(imageUri, CorrectName)
                    : Path.Combine(imageUri, "Flags", CorrectName);
                
                return ImageHelper.LoadFromResource(new Uri(imageUri));
            }
            // Output as a string
            else if(targetType.IsAssignableTo(typeof(string)))
                return country.Name; // Return country name
        }
        return ImageHelper.DefaultImage();
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
    
    public static string DefineCorrectName(string countryName)
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
        
        //string corName; // correct flag name
        try
        {
            CorrectName = ConfigManager.FlagNames.Where(fName => fName.ContainsAll(splitted)).ToArray()[0];
        }
        catch (IndexOutOfRangeException e)
        {
            Console.WriteLine("Flag for {0} not found", countryName);
            CorrectName = ImageHelper.DefaultFileName;
        }

        return CorrectName;
    }
}