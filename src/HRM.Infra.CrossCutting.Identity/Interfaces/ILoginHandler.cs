using HRM.Domain.Interfaces;
using HRM.Infra.CrossCutting.Identity.Requests;
using HRM.Infra.CrossCutting.Identity.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Infra.CrossCutting.Identity.Interfaces
{
    public interface ILoginHandler
    {
        Task Handle(LoginRequest message, IOutputPort<LoginResponse> outputPort);
    }
}
