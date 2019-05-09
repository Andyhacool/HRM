using HRM.Domain.Interfaces;
using HRM.Infra.CrossCutting.Identity.Responses;
using System.Net;

namespace HRM.Infra.CrossCutting.Identity.Presenters
{
    public sealed class LoginPresenter : IOutputPort<LoginResponse>
    {
        public LoginResponse data { get; set;}

        public void Handle(LoginResponse response)
        {
            data = response;
        }
    }
}
