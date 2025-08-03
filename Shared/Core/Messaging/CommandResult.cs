namespace Shared.Core.Messaging;

public class CommandResult
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public object Data { get; set; }

    public CommandResult() { }

    public CommandResult(bool success, string message, object data)
    {
        Success = success;
        Message = message;
        Data = data;
    }
}
