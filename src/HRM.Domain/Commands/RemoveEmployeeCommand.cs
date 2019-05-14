using HRM.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRM.Domain.Commands
{
    public class RemoveEmployeeCommand : EmployeeCommand
    {
        public RemoveEmployeeCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveEmployeeCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
