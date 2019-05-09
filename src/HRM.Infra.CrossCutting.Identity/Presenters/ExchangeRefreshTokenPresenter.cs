using HRM.Domain.Interfaces;
using HRM.Infra.CrossCutting.Identity.Responses;
using System.Net;

namespace HRM.Infra.CrossCutting.Identity.Presenters
{
    public sealed class ExchangeRefreshTokenPresenter : IOutputPort<ExchangeRefreshTokenResponse>
    {
        public ExchangeRefreshTokenResponse data { get; set; } 

        public void Handle(ExchangeRefreshTokenResponse response)
        {
            data = new ExchangeRefreshTokenResponse(response.AccessToken, response.RefreshToken);
        }
    }
}
