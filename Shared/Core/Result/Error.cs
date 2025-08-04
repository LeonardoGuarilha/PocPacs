using System;

namespace Shared.Core.Result;

public record Error(string Code, string Message)
{
    public static Error None = new(string.Empty, string.Empty);
    public static Error NullValue = new("Error.NullValue", "Um valor nulo foi fornecido.");
    public static Error NoData = new("Error.NoData", "Nenhum dado encontrado.");
}
