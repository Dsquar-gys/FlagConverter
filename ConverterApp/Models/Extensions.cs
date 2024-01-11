using System.Collections.Generic;

namespace ConverterApp.Models;

public static class Extensions
{
    public static void Add<TValue, TKey>(this Dictionary<TKey, TValue> dict, TValue value, params TKey[] keys)
    {
        foreach (var key in keys)
            dict.Add(key, value);
    }

    public static bool ContainsAll(this string source, params string[] subs)
    {
        foreach (var substring in subs)
            if (!source.Contains(substring)) return false;
        return true;
    }
}