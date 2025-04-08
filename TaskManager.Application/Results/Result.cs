namespace TaskManager.Application.Results;

public record Result(bool Success, string? Message, object? Content)
{    
    public static Result Ok() => new(true, null, null);
    public static Result Ok(string message) => new(true, message, null);
    public static Result Fail(string message) => new(false, message, null);    
    public static Result Ok(object content) => new(true, null, content);
    public static Result Fail(object content) => new(false, null, content);
    public static Result Ok(object content, string message) => new(true, message, content);
    public static Result Fail(object content, string message) => new(false, message, content);
}