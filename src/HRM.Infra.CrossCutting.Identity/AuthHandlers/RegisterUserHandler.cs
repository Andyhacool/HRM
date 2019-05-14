using HRM.Domain.CommandHandlers;
using HRM.Domain.Core.Bus;
using HRM.Domain.Core.Notifications;
using HRM.Domain.Interfaces;
using HRM.Domain.Models.Identity;
using HRM.Infra.CrossCutting.Identity.Events;
using HRM.Infra.CrossCutting.Identity.Interfaces;
using HRM.Infra.CrossCutting.Identity.Models;
using HRM.Infra.CrossCutting.Identity.Requests;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HRM.Infra.CrossCutting.Identity.Dto;
using System.Security.Claims;

namespace HRM.Infra.CrossCutting.Identity.UseCaseHandlers
{
    public class RegisterUserHandler : CommandHandler, IRegisterUserHandler
    {
        private readonly IMediatorHandler Bus;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;

        public RegisterUserHandler(IUserRepository userRepository,
                                    UserManager<AppUser> userManager,
                                   IUnitOfWork uow,
                                   IMediatorHandler bus,
                                   INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            Bus = bus;
        }

        public async Task Handle(RegisterUserRequest message)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return;
            }

            var appUser = new AppUser { Email = message.Email, UserName = message.UserName };
            var identityResult = await _userManager.CreateAsync(appUser, message.Password);

            if (!identityResult.Succeeded)
            {
                // User claim for write employees data
                await _userManager.AddClaimAsync(appUser, new Claim("Employees", "Write"));

                await Bus.RaiseEvent(new DomainNotification(message.MessageType, string.Join(", ",identityResult.Errors.Select(e =>  e.Description).ToArray())));
            }

            var user = new User(Guid.NewGuid(), message.FirstName, message.LastName, appUser.Id, message.Email, message.Password);
            await _userRepository.AddAsync(user);

            if (Commit())
            {
                await Bus.RaiseEvent(new UserRegisteredEvent(user.Id, user.UserName, user.Email));
                return;
            }

            await Bus.RaiseEvent(new DomainNotification(message.MessageType, "User registration failed"));
        }
    }
}
