using System.Collections.Generic;

namespace ConverterApp.Models;

public static class ConfigManager
{
    public static readonly Dictionary<string, string> CountryCodes;
    
    static ConfigManager()
    {
        CountryCodes = new();
        CountryCodes.Add("rus", "rus.png");
        CountryCodes.Add("uk", "uk.png");
    }
}