using System;
using System.Collections.Generic;
using System.Text;

namespace HRM.Domain.Interfaces
{
    public interface IOutputPort<in TUseCaseResponse>
    {
        void Handle(TUseCaseResponse response);
    }
}
