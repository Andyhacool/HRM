using HRM.Domain.CommandHandlers;
using HRM.Domain.Core.Bus;
using HRM.Domain.Core.Notifications;
using HRM.Domain.Interfaces;
using HRM.Infra.CrossCutting.Identity.Interfaces;
using HRM.Infra.CrossCutting.Identity.Interfaces.Services;
using HRM.Infra.CrossCutting.Identity.Requests;
using HRM.Infra.CrossCutting.Identity.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Infra.CrossCutting.Identity.AuthHandlers
{
    public class LoginHandler : CommandHandler, ILoginHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtFactory _jwtFactory;
        private readonly ITokenFactory _tokenFactory;
        private readonly IMediatorHandler Bus;

        public LoginHandler(IUserRepository userRepository,
                            IJwtFactory jwtFactory,
                            ITokenFactory tokenFactory,
                            IUnitOfWork uow,
                            IMediatorHandler bus,
                            INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _userRepository = userRepository;
            _jwtFactory = jwtFactory;
            _tokenFactory = tokenFactory;
            Bus = bus;
        }

        public async Task Handle(LoginRequest message, IOutputPort<LoginResponse> outputPort)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return;
            }

            // ensure we have a user with the given user name
            var user = await _userRepository.FindByName(message.UserName);
            if (user != null)
            {
                // validate password
                if (await _userRepository.CheckPassword(user, message.Password))
                {
                    // generate refresh token
                    var refreshToken = _tokenFactory.GenerateToken();
                    user.AddRefreshToken(refreshToken, user.Id, message.RemoteIpAddress);
                    _userRepository.Update(user);

                    if (Commit())
                    {
                        // generate access token
                        outputPort.Handle(new LoginResponse(await _jwtFactory.GenerateEncodedToken(user.Id.ToString(), user.UserName), refreshToken));
                        return;
                    }                   
                }
            }
            await Bus.RaiseEvent(new DomainNotification(message.MessageType, "Username/password is invalid"));
        }
    }
}
