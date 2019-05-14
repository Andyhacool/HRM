using HRM.Application.ViewModels;
using HRM.Infra.CrossCutting.Identity.Requests;
using HRM.Infra.CrossCutting.Identity.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Application.Interfaces
{
    public interface IAccountAppService : IDisposable
    {
        Task Register(RegisterUserViewModel viewModel);
        Task<LoginResponse> Login(LoginViewModel request);
        Task<ExchangeRefreshTokenResponse> RefreshToken(RefreshTokenViewModel viewModel);
    }
}
