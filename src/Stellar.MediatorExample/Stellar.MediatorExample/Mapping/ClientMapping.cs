using AutoMapper;
using Stellar.MediatorExample.events;
using Stellar.MediatorExample.requests;
using Stellar.MediatorExample.results;

namespace Stellar.MediatorExample.mapping
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<ClientCreatedResult, ClientCreatedEvent>();

            CreateMap<ClientCreateRequest, ClientCreatedResult>()
                .ForMember(src => src.Success, src => src.UseValue(true));
        }
    }
}
