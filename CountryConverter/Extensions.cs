namespace CountryConverter;

internal static class Extensions
{
    /// <summary>
    /// Allows add single value to multiple keys
    /// </summary>
    /// <param name="dict">Target dictionary</param>
    /// <param name="value">Target value</param>
    /// <param name="keys">Keys for value</param>
    public static void Add<TValue, TKey>(this Dictionary<TKey, TValue> dict, TValue value, params TKey[] keys)
        where TKey : notnull
    {
        foreach (var key in keys)
            dict.Add(key, value);
    }
}