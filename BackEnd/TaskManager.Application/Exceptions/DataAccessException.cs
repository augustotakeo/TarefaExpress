namespace TaskManager.Application.Exceptions;

public class DataAccessException : Exception
{
    public DataAccessException(string message) : base(message) { }

    public static void ThrowsIfNull(object value, string message)
    {
        if (value is null)
            throw new DataAccessException(message);
    }
}