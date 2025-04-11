namespace TaskManager.Application.Results;

public record Result(bool Success, object? Value)
{
    public static Result Ok() => new(true, null);
    public static Result Ok(object? value) => new(true, value);
    public static Result Fail() => new(false, null);
    public static Result Fail(object? value) => new(false, value);
}