using Newtonsoft.Json;

namespace Redlock.Domain.Extensions;

public static class GenericExtension
{
    public static object? GetPropertyValue<T>(this T? @object, string propertyName) where T : class
    {
        if (@object == null || string.IsNullOrWhiteSpace(propertyName))
        {
            return null;
        }

        var propertyInfo = @object.GetType().GetProperty(propertyName);

        if (propertyInfo == null)
        {
            return null;
        }

        return propertyInfo.GetValue(@object);
    }

    public static string? GetPropertyValueAsString<T>(this T? @object, string propertyName) where T : class
    {
        var value = GetPropertyValue(@object, propertyName);

        return value?.ToString();
    }

    public static string? GetPropertyValueAsJson<T>(this T? @object, string propertyName) where T : class
    {
        var value = GetPropertyValue(@object, propertyName);

        return value != null ? JsonConvert.SerializeObject(value) : null;
    }

    public static void SetPropertyValue<T>(this T? @object, string propertyName, object? value) where T : class
    {
        if (@object == null || string.IsNullOrWhiteSpace(propertyName))
        {
            return;
        }

        var propertyInfo = @object.GetType().GetProperty(propertyName);

        if (propertyInfo == null || !propertyInfo.CanWrite)
        {
            return;
        }

        propertyInfo.SetValue(@object, value);
    }

    public static bool HasProperty<T>(this T? @object, string propertyName) where T : class
    {
        if (@object == null || string.IsNullOrWhiteSpace(propertyName))
        {
            return false;
        }

        var propertyInfo = @object.GetType().GetProperty(propertyName);

        return propertyInfo != null;
    }
}
