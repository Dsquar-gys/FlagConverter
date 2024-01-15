using Avalonia;
using Avalonia.Controls;
using CountryConverter;

namespace ExampleApp.CustomControls;

public class FlagImage : Image
{
    public static readonly StyledProperty<bool> DownloadableProperty =
        AvaloniaProperty.Register<FlagImage, bool>(nameof(Downloadable), defaultValue: false);

    public bool Downloadable
    {
        get => GetValue(DownloadableProperty);
        set => SetValue(DownloadableProperty, value);
    }
    
    public static readonly StyledProperty<string> FileNameProperty =
        AvaloniaProperty.Register<FlagImage, string>(nameof(FileName), defaultValue: ImageHelper.DefaultFileName);

    public string FileName
    {
        get => GetValue(FileNameProperty);
        set => SetValue(FileNameProperty, value);
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        if (change.Property.Name == "Source")
        {
            FileName = CountryConverter.CountryConverter.CorrectName;
            Downloadable = FileName != ImageHelper.DefaultFileName;
        }
        base.OnPropertyChanged(change);
    }
}