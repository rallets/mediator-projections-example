using Autofac;
using AutoMapper;
using Stellar.MediatorExample.events;
using Stellar.MediatorExample.handlers;
using Stellar.MediatorExample.mapping;
using Stellar.MediatorExample.requests;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using System;
using System.IO;
using System.Reflection;
using Stelllar.MediatorExample.Helpers;

/*
 * Based on https://github.com/jbogard/MediatR
 *
 * For object projections see http://docs.automapper.org/en/stable/Projection.html
 *
 */

namespace Stellar.MediatorExample
{
    class Program
    {
        private static IMediator _mediator;
        private static WrappingWriter _writer;

        public static void Main(string[] args)
        {
            InitStore();
            ConfigMappings();

            _writer = new WrappingWriter(Console.Out);
            _mediator = BuildMediator(_writer);

            while (true)
            {
                CreateNewClient();

                Console.WriteLine("Press any key to create a new client...");
                Console.ReadKey();
            }

        }

        public static async void CreateNewClient()
        {
            var clientId = Guid.NewGuid();
            //clientId = Guid.Empty; // Validator will raise an ValidationException

            var request = new ClientCreateRequest
            {
                Id = clientId,
                Name = "New client " + clientId.ToString("N")
            };

            try
            {
                var result = await _mediator.Send(request);

                await _writer.WriteLineAsync("Result: " + result.Success);

                var @event = Mapper.Map<ClientCreatedEvent>(result);

                await _mediator.Publish(@event);
            }
            catch (ValidationException ex)
            {
                await _writer.WriteLineAsync($"Validation error: {ex.Message}");
            }
        }

        private static void InitStore()
        {
            if(!Directory.Exists("./store/"))
            {
                Directory.CreateDirectory("./store/");
            }
        }

        private static void ConfigMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfiles(typeof(ClientProfile).GetTypeInfo().Assembly);
            });
        }

        private static IMediator BuildMediator(WrappingWriter writer)
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            var mediatrOpenTypes = new[]
            {
                typeof(IRequestHandler<,>),
                typeof(INotificationHandler<>),
                typeof(IValidator<>),
            };

            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                builder
                    .RegisterAssemblyTypes(typeof(ClientCreateRequest).GetTypeInfo().Assembly)
                    .AsClosedTypesOf(mediatrOpenType)
                    .AsImplementedInterfaces();
            }

            builder.RegisterInstance(writer).As<TextWriter>();

            // It appears Autofac returns the last registered types first
            builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));

            builder.RegisterGeneric(typeof(ValidationBehavior<,>)).As(typeof(IPipelineBehavior<,>));

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            var container = builder.Build();

            var mediator = container.Resolve<IMediator>();

            return mediator;
        }

    }

}
