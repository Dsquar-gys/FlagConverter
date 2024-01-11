using System.Collections.Generic;

namespace ConverterApp.Models;

public static class ConfigManager
{
    public static readonly Dictionary<string, string> CountryCodes;
    
    static ConfigManager()
    {
        CountryCodes = new()
        {
            {"+7", "urs.png"},
            {"+44", "uk.png"}
        };
    }
}