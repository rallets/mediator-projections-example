using System;

namespace Stellar.MediatorExample.results
{
    public class ClientCreatedResult
    {
        public Guid Id { get; set; }
        public bool Success { get; set; }
        public string Name { get; set; }
    }
}