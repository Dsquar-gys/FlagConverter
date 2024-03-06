using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace CountryConverter;

public class CountryConverter : IValueConverter
{
    public static string CorrectName { get; private set; } = Resources.DefaultFileName;

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not Country country) return Resources.DefaultImage;
            
        // Output as image
        if (targetType.IsAssignableTo(typeof(IImage)))
        {
            if (Resources.Images.TryGetValue(DefineCorrectName(country.Name), out var image))
                return image;
        }
        // Output as a string
        if(targetType.IsAssignableTo(typeof(string)))
            return country.Name; // Return country name
        return Resources.DefaultImage;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
    
    public static string DefineCorrectName(string countryName)
    {
        // Names without "and", "the", and taken before brackets or "of"
        countryName = new string(countryName.ToLower()
            .TakeWhile(x => x != '[' || x != '(')
            .Where(ch => char.IsLetter(ch) || ch == ' ' || ch == '-')
            .ToArray());
        
        var dividedName = countryName.Split(' ')
            .Where(w => w != "and" && w != "the")
            .TakeWhile(w => w != "of")
            .ToArray();
        
        try
        {
            CorrectName = Resources.Images.Keys.Where(fName => dividedName.All(fName.Contains)).ToArray()[0];
        }
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine("Flag for {0} not found", countryName);
            CorrectName = Resources.DefaultFileName;
        }

        return CorrectName;
    }
}