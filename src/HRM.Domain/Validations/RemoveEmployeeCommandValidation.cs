using HRM.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRM.Domain.Validations
{
    class RemoveEmployeeCommandValidation : EmployeeValidation<RemoveEmployeeCommand>
    {
        public RemoveEmployeeCommandValidation()
        {
            ValidateId();
        }
    }
}
