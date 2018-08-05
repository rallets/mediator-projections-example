using Stellar.MediatorExample.requests;
using FluentValidation;
using MediatR;

namespace Stellar.MediatorExample.validators
{
    public class ClientCreateValidator : AbstractValidator<ClientCreateRequest>
    {
        private readonly IMediator _mediator;

        public ClientCreateValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(request => request.Id).NotEmpty();
            RuleFor(request => request.Name).NotEmpty();
        }

    }

}
