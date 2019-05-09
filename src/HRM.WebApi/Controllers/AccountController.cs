using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRM.Application.Interfaces;
using HRM.Application.ViewModels;
using HRM.Domain.Core.Bus;
using HRM.Domain.Core.Notifications;
using HRM.Infra.CrossCutting.Identity.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ApiController
    {
        private readonly IAccountAppService _accountAppService;
        private readonly ILogger _logger;

        public AccountController(
            IAccountAppService accountAppService,
            INotificationHandler<DomainNotification> notifications,
            ILoggerFactory loggerFactory,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _accountAppService = accountAppService;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        //// POST api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(model);
            }

            var response = await _accountAppService.Login(model);
            return Response(response);
        }

        // POST api/auth/refreshtoken
        [HttpPost("refreshtoken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenViewModel model)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var response =  await _accountAppService.RefreshToken(model);
            return Response(response); 
        }

        // POST api/accounts
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _accountAppService.Register(model);
            return Response(model);
        }
    }
}