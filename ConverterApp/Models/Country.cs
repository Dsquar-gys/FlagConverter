namespace ConverterApp.Models;

public class Country
{
    public string Name { get; }
    public string ISO_Code { get; }
    public string IOC_Code { get; }
    public int OKSM { get; }

    public Country(string name, string iso, string ioc, int oksm)
    {
        Name = name;
        ISO_Code = iso;
        IOC_Code = ioc;
        OKSM = oksm;
    }

    public static Country Default { get; } = new Country("", "", "", 0);
}