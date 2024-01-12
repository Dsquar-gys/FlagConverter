using System;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace ConverterApp.Models;

public static class ImageHelper
{
    public static Bitmap LoadFromResource(Uri resourceUri) => new Bitmap(AssetLoader.Open(resourceUri));
}