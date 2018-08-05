using AutoMapper;
using Stellar.MediatorExample.requests;
using MediatR;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Stellar.MediatorExample.results;

namespace Stellar.MediatorExample.handlers
{
    public class ClientCreateRequestHandler : IRequestHandler<ClientCreateRequest, ClientCreatedResult>
    {
        private readonly TextWriter _writer;
        private readonly string _fn = @".\store\main-store.txt";

        public ClientCreateRequestHandler(TextWriter writer)
        {
            _writer = writer;
        }

        public async Task<ClientCreatedResult> Handle(ClientCreateRequest request, CancellationToken cancellationToken)
        {
            /*
             * Main event handler: collect all Client's GUID
             */

            await _writer.WriteLineAsync($"--- Handled ClientCreateRequest: {request.Id}, {request.Name}");

            if (!File.Exists(_fn))
            {
                File.Create(_fn).Close();
            }
            File.AppendAllText(_fn, $"{request.Id} - {request.Name}{Environment.NewLine}");

            var result = Mapper.Map<ClientCreatedResult>(request);

            return result;
        }
    }
}