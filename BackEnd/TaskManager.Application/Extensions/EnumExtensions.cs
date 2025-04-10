using TaskManager.Domain.Enums;

namespace TaskManager.Application.Extensions;

public static class EnumExtensions
{
    public static string? GetEnumDescription(this EStatus status)
    {
        return status switch
        {
            EStatus.Concluido => "Concluída",
            EStatus.Pendente => "Pendente",
            EStatus.EmProgresso => "Em Progresso",
            _ => null
        };
    }

    public static T GetEnumFromString<T>(this string @enum, T defaulValue)
    {
        if(Enum.TryParse(typeof(T), @enum, out var result))
            return (T)result;
        return defaulValue;
    }
}