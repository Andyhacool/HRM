using HRM.Application.Interfaces;
using HRM.Application.ViewModels;
using HRM.Infra.CrossCutting.Identity.Interfaces;
using HRM.Infra.CrossCutting.Identity.Presenters;
using HRM.Infra.CrossCutting.Identity.Requests;
using HRM.Infra.CrossCutting.Identity.Responses;
using HRM.Infra.CrossCutting.Identity.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Application.Services
{
    public class AccountAppService : IAccountAppService
    {
        private readonly ILoginHandler _loginHandler;
        private readonly IExchangeRefreshTokenHandler _exchangeRefreshTokenHandler;
        private readonly LoginPresenter _loginPresenter;
        private readonly ExchangeRefreshTokenPresenter _exchangeRefreshTokenPresenter;
        private readonly IRegisterUserHandler _registerUserHandler;
        private readonly AuthSettings _authSettings;

        public AccountAppService(
            ILoginHandler loginHandler, 
            LoginPresenter loginPresenter, 
            IExchangeRefreshTokenHandler exchangeRefreshTokenHandler,
            ExchangeRefreshTokenPresenter exchangeRefreshTokenPresenter,
            IRegisterUserHandler registerUserHandler,
            IOptions<AuthSettings> authSettings)
        {
            _loginHandler = loginHandler;
            _loginPresenter = loginPresenter;
            _exchangeRefreshTokenHandler = exchangeRefreshTokenHandler;
            _exchangeRefreshTokenPresenter = exchangeRefreshTokenPresenter;
            _registerUserHandler = registerUserHandler;
            _authSettings = authSettings.Value;
        }

        public async Task<LoginResponse> Login(LoginViewModel viewModel)
        {
            await _loginHandler.Handle(new LoginRequest(viewModel.Email, viewModel.Password, viewModel.RemoteIpAddress), _loginPresenter);
            return _loginPresenter.data;
        }

        public async Task<ExchangeRefreshTokenResponse> RefreshToken(RefreshTokenViewModel viewModel)
        {
            await _exchangeRefreshTokenHandler.Handle(new ExchangeRefreshTokenRequest(viewModel.AccessToken, viewModel.RefreshToken), _exchangeRefreshTokenPresenter);
            return _exchangeRefreshTokenPresenter.data;
        }

        public async Task Register(RegisterUserViewModel viewModel)
        {
            await _registerUserHandler.Handle(new RegisterUserRequest(
                viewModel.FirstName, 
                viewModel.LastName,
                viewModel.Email,
                viewModel.UserName,
                viewModel.Password));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
