using System.Reflection;
using Avalonia.Media.Imaging;

namespace CountryConverter;

public static class Resources
{
    private const string NotFoundImage = "image-not-found.png";
    private static bool _bakedAssets = true;

    /// <summary>
    /// Dictionary { File name --> Bitmap }
    /// </summary>
    public static Dictionary<string, Bitmap> Images { get; } = new();
    public static Bitmap DefaultImage => Images[NotFoundImage];
    public static string DefaultFileName => NotFoundImage;

    static Resources()
    {
        //ChangeAssetsFolder(new DirectoryInfo("/home/dmitrichenkoda@kvant-open.spb.ru/Downloads/BakedAssets/"));
        ResetAssets();
    }

    /// <summary>
    /// Set new folder with assets
    /// </summary>
    /// <remarks>Folder must contain "image-not-found.png" and "Flags" folder</remarks>
    /// <param name="assetsFolder">Target directory</param>
    public static void ChangeAssetsFolder(DirectoryInfo assetsFolder)
    {
        _bakedAssets = false;
        UpdateImages(assetsFolder);
    }

    /// <summary>
    /// Reset Assets to baked ones
    /// </summary>
    public static void ResetAssets()
    {
        _bakedAssets = true;
        UpdateImages();
    }

    private static void UpdateImages(DirectoryInfo? assetsFolder = null)
    {
        Images.Clear();
        
        if (_bakedAssets || assetsFolder is null)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var names = assembly.GetManifestResourceNames();
            

            foreach (var name in names)
            {
                using var stream = assembly.GetManifestResourceStream(name)!;
                try
                {
                    var img = new Bitmap(stream);
                    // "CountryConverter.BakedAssets.*name*.png"
                    var dividedName = name.Split('.');
                    Images.Add(dividedName[^2] + ".png", img);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Resource is not an image");
                }
            }
        }
        else
        {
            // Reading default image
            Images.Add(DefaultFileName, new Bitmap(Path.Combine(assetsFolder.FullName, DefaultFileName)));
            
            // Reading flags
            var flagsFolder = new DirectoryInfo(Path.Combine(assetsFolder.FullName, "Flags"));
            foreach (var file in flagsFolder.EnumerateFiles())
            {
                if (file.Extension == ".png")
                    Images.Add(file.Name, new Bitmap(file.FullName));
            }
        }
    }
}