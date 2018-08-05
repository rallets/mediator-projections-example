using Stellar.MediatorExample.events;
using MediatR;
using System.Threading;
using System.IO;
using System.Threading.Tasks;

namespace Stellar.MediatorExample.handlers
{
    public class ClientCreatedEventHandler2 : INotificationHandler<ClientCreatedEvent>
    {
        private readonly TextWriter _writer;
        private readonly string _fn = @".\store\projection-2.txt";

        public ClientCreatedEventHandler2(TextWriter writer)
        {
            _writer = writer;
        }

        public Task Handle(ClientCreatedEvent notification, CancellationToken cancellationToken)
        {
            if (!File.Exists(_fn))
            {
                File.Create(_fn).Close();
            }

            /*
             * Second projection example using an additional event handler: store number of Clients
             */
            int n = 0;

            int.TryParse(File.ReadAllText(_fn), out n);

            n++;

            File.WriteAllText(_fn, $"{n}");

            return _writer.WriteLineAsync($"ClientCreatedEvent2 Handled => {n}");
        }
    }

}