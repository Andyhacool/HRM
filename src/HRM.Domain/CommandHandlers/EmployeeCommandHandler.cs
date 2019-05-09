using HRM.Domain.Commands;
using HRM.Domain.Core.Bus;
using HRM.Domain.Core.Notifications;
using HRM.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HRM.Domain.CommandHandlers
{
    public class EmployeeCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewEmployeeCommand>,
        IRequestHandler<UpdateEmployeeCommand>,
        IRequestHandler<RemoveEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMediatorHandler Bus;

        public EmployeeCommandHandler(IEmployeeRepository employeeRepository,
                                      IUnitOfWork uow,
                                      IMediatorHandler bus,
                                      INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _employeeRepository = employeeRepository;
            Bus = bus;
        }

        public Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Unit> Handle(RemoveEmployeeCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Unit> Handle(RegisterNewEmployeeCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
