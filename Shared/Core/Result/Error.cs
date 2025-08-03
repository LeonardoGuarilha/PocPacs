using System;

namespace Shared.Core.Result;

public record Error(ETypeError type, string description)
{
    public static Error Validation = new(ETypeError.Validation, "");
    public static Error None = new(ETypeError.None, "");
    public static Error NullValue = new(ETypeError.NullValue, "");
    public static Error NotFound = new(ETypeError.NotFound, "");
}
