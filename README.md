# Flag Converter
Flag Converter is a library to convert Contry name or it's code to flag.
Assets are customizable. It is possible to change source directory and define each
country your own flag.
## Available codes:
- ISO
- IOC
- OKSM

Each country can be got be these codes. Or you can use country name.
## Assets
Library provides its own baked assets (Embedded) that can be received from assembly.
To change assets directory [https://github.com/Dsquar-gys/FlagConverter/blob/master/CountryConverter/Resources.cs]**Resources.cs** contains:
```cs
public static void ChangeAssetsFolder(DirectoryInfo assetsFolder)
```
and
```cs
public static void ResetAssets()
```
## Tech
- .NET Core
- Avalonia UI
- Reactive UI
