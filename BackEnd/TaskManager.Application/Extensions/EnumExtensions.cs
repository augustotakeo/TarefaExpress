using TaskManager.Domain.Enums;

namespace TaskManager.Application.Extensions;

public static class EnumExtensions
{
    public static T GetEnumFromString<T>(this string @enum, T defaulValue)
    {
        if(Enum.TryParse(typeof(T), @enum, out var result))
            return (T)result;
        return defaulValue;
    }
}