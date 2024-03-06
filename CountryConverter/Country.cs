namespace CountryConverter;

public class Country(string name, string iso, string ioc, int oksm)
{
    public string Name { get; } = name;
    public string ISO_Code { get; } = iso;
    public string IOC_Code { get; } = ioc;
    public int OKSM { get; } = oksm;
}