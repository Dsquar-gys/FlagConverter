using System;
using System.IO;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace ConverterApp.Models;

public static class ImageHelper
{
    private static string NotFoundImage { get; } = "image-not-found.jpg";
    public static Bitmap LoadFromResource(Uri resourceUri) => new Bitmap(AssetLoader.Open(resourceUri));

    public static Bitmap DefaultImage() => new Bitmap(AssetLoader.Open(
        new Uri(Path.Combine("avares://ConverterApp/Assets", NotFoundImage))));
}