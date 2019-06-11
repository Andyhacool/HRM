using HRM.Infra.Data.Context;
using HRM.Infra.Data.Repository;
using HRM.Infra.Data.UoW;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using HRM.Domain.Core.Bus;
using HRM.Infra.CrossCutting.Bus;
using HRM.Application.Interfaces;
using HRM.Application.Services;
using HRM.Domain.Core.Notifications;
using HRM.Domain.Events;
using HRM.Domain.EventHandlers;
using HRM.Domain.Commands;
using HRM.Domain.CommandHandlers;
using HRM.Domain.Interfaces;
using HRM.Domain.Core.Services;
using HRM.Infra.CrossCutting.Identity.Interfaces;
using HRM.Infra.CrossCutting.Identity.AuthHandlers;
using HRM.Infra.CrossCutting.Identity.UseCaseHandlers;
using HRM.Infra.CrossCutting.Identity.Repositories;
using HRM.Infra.CrossCutting.Identity.Interfaces.Services;
using HRM.Infra.CrossCutting.Identity.Auth;
using Microsoft.AspNetCore.Authorization;
using HRM.Infra.CrossCutting.Identity.Authorization;
using HRM.Infra.Data.Repository.EventSourcing;
using HRM.Domain.Core.Events;
using HRM.Infra.Data.EventSourcing;

namespace HRM.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Application
            services.AddScoped<IAccountAppService, AccountAppService>();

            //// Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<EmployeeRegisteredEvent>, EmployeeEventHandler>();
            services.AddScoped<INotificationHandler<EmployeeUpdatedEvent>, EmployeeEventHandler>();
            services.AddScoped<INotificationHandler<EmployeeRemovedEvent>, EmployeeEventHandler>();

            //// Domain - Commands
            services.AddScoped<IRequestHandler<RegisterNewEmployeeCommand>, EmployeeCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateEmployeeCommand>, EmployeeCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveEmployeeCommand>, EmployeeCommandHandler>();

            //// Infra - Data
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<HRMContext>();

            //// Infra - Identity
            services.AddTransient<IEmailSender, AuthEmailMessageSender>();
            services.AddTransient<ISmsSender, AuthSMSMessageSender>();

            // ASP.NET Authorization Polices
            services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>();

            //// Infra - Handler
            services.AddScoped<ILoginHandler, LoginHandler>();
            services.AddScoped<IExchangeRefreshTokenHandler, ExchangeRefreshTokenHandler>();
            services.AddScoped<IRegisterUserHandler, RegisterUserHandler>();

            //// Infra - Data
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddSingleton<IJwtFactory, JwtFactory>();
            services.AddSingleton<IJwtTokenHandler, JwtTokenHandler>();
            services.AddSingleton<ITokenFactory, TokenFactory>();
            services.AddSingleton<IJwtTokenValidator, JwtTokenValidator>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
            services.AddScoped<EventSourcingContext>();
        }
    }
}