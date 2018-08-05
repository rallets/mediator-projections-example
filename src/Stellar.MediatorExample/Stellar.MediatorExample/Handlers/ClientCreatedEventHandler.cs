using Stellar.MediatorExample.events;
using MediatR;
using System.Threading;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace Stellar.MediatorExample.handlers
{
    public class ClientCreatedEventHandler : INotificationHandler<ClientCreatedEvent>
    {
        private readonly TextWriter _writer;
        private readonly string _fn = @".\store\projection-1.txt";

        public ClientCreatedEventHandler(TextWriter writer)
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
             * First projection example using an additional event handler: store unique and ordered Client's GUID
             */
            var list = File.ReadAllLines(_fn).ToList();
            list.Add(notification.Id.ToString());

            list = list.Distinct().OrderBy(x => x).ToList();

            File.WriteAllLines(_fn, list);

            return _writer.WriteLineAsync("ClientCreatedEvent Handled");
        }
    }

}