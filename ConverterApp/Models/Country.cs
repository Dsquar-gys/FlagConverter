using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ConverterApp.Models;

public partial class Country
{
    public int Id { get; set; } // Principal key
    public IEnumerable<City> Locations { get; set; }
    public string Name { get; set; }
    public string ISO_Code { get; set; }
    public string IOC_Code { get; set; }
    public int OKSM { get; set; }

    public Country(string name, string iso, string ioc, int oksm)
    {
        Name = name;
        ISO_Code = iso;
        IOC_Code = ioc;
        OKSM = oksm;
    }

    public static Country Default { get; } = new Country(null, null, null, 0);
}