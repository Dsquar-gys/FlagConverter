using System;
using System.IO;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using ConverterApp.ViewModels;

namespace ConverterApp.Models;

public static class ImageHelper
{
    private const string NotFoundImage = "image-not-found.jpg";

    public static Bitmap LoadFromResource(Uri resourceUri)
    {
        ConverterViewModel.Instance.Downloadable = true;
        return new Bitmap(AssetLoader.Open(resourceUri));
    }

    public static Bitmap DefaultImage()
    {
        ConverterViewModel.Instance.Downloadable = false;
        return new Bitmap(AssetLoader.Open(
            new Uri(Path.Combine("avares://ConverterApp/Assets", NotFoundImage))));
    }
}