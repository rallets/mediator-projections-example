using System;
using MediatR;

namespace Stellar.MediatorExample.events
{
    public class ClientCreatedEvent : INotification
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}