using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace CountryConverter;

public static class ImageHelper
{
    private const string NotFoundImage = "image-not-found.jpg";
    public static string DefaultFileName { get => NotFoundImage; }

    public static Bitmap LoadFromResource(Uri resourceUri) =>
        new Bitmap(AssetLoader.Open(resourceUri));

    public static Bitmap DefaultImage() => new Bitmap(AssetLoader.Open(
        new Uri(Path.Combine("avares://ExampleApp/Assets", NotFoundImage))));
}