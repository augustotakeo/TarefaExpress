namespace TaskManager.Application.Results;

public record Result(bool Success, string? Message, object? Content, int StatusCode)
{
    public static Result Ok() => new(true, null, null, 200);
    public static Result Ok(string message) => new(true, message, null, 200);
    public static Result Fail(string message) => new(false, message, null, 400);
    public static Result NotFound(string message) => new(false, message, null, 404);
    public static Result Ok(object content) => new(true, null, content, 200);
    public static Result Fail(object content) => new(false, null, content, 400);
    public static Result Ok(object content, string message) => new(true, message, content, 200);
    public static Result Fail(object content, string message) => new(false, message, content, 400);
}