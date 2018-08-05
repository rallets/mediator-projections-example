using System;
using Stellar.MediatorExample.results;
using MediatR;

namespace Stellar.MediatorExample.requests
{
    public class ClientCreateRequest : IRequest<ClientCreatedResult>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}