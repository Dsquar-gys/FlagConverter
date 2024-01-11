namespace ConverterApp.Models;

public class City
{
    public Country Country { get; set; }
    public int CountryId { get; set; } // Foreign key
}