using HRM.Domain.Interfaces;
using HRM.Infra.CrossCutting.Identity.Interfaces.Services;
using HRM.Infra.CrossCutting.Identity.Requests;
using HRM.Infra.CrossCutting.Identity.Responses;
using HRM.Infra.CrossCutting.Identity.Specifications;
using System.Linq;
using System.Threading.Tasks;
using HRM.Infra.CrossCutting.Identity.Interfaces;
using HRM.Domain.CommandHandlers;
using HRM.Domain.Core.Notifications;
using MediatR;
using HRM.Domain.Core.Bus;
using System;

namespace HRM.Infra.CrossCutting.Identity.UseCaseHandlers
{
    public class ExchangeRefreshTokenHandler : CommandHandler, IExchangeRefreshTokenHandler
    {
        private readonly IJwtTokenValidator _jwtTokenValidator;
        private readonly IUserRepository _userRepository;
        private readonly IJwtFactory _jwtFactory;
        private readonly ITokenFactory _tokenFactory;
        private readonly IMediatorHandler Bus;

        public ExchangeRefreshTokenHandler(IJwtTokenValidator jwtTokenValidator, 
                                           IUserRepository userRepository, 
                                           IJwtFactory jwtFactory, 
                                           ITokenFactory tokenFactory,
                                           IUnitOfWork uow,
                                           IMediatorHandler bus,
                                           INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _jwtTokenValidator = jwtTokenValidator;
            _userRepository = userRepository;
            _jwtFactory = jwtFactory;
            _tokenFactory = tokenFactory;
            Bus = bus;
        } 

        public async Task Handle(ExchangeRefreshTokenRequest message, IOutputPort<ExchangeRefreshTokenResponse> outputPort)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return;
            }

            var cp = _jwtTokenValidator.GetPrincipalFromToken(message.AccessToken, message.SigningKey);

            // invalid token/signing key was passed and we can't extract user claims
            if (cp != null)
            {
                var id = cp.Claims.First(c => c.Type == "id");
                var user = await _userRepository.GetSingleBySpec(new UserSpecification(id.Value));

                if (user.HasValidRefreshToken(message.RefreshToken))
                {
                    var jwtToken = await _jwtFactory.GenerateEncodedToken(user.Id.ToString(), user.UserName);
                    var refreshToken = _tokenFactory.GenerateToken();
                    user.RemoveRefreshToken(message.RefreshToken); // delete the token we've exchanged
                    user.AddRefreshToken(refreshToken, user.Id, ""); // add the new one
                    _userRepository.Update(user);

                    if (Commit())
                    {
                        outputPort.Handle(new ExchangeRefreshTokenResponse(jwtToken, refreshToken));
                        return;
                    }
                }
            }

            await Bus.RaiseEvent(new DomainNotification(message.MessageType, "Failed to refresh token"));
        }
    }
}
