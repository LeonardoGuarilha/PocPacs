using System;
using MediatR;

namespace Shared.Core.Messaging;

public class Event : INotification
{
    public DateTime Timestamp { get; set; }

    public Event()
    {
        Timestamp = DateTime.Now;
    }
}
