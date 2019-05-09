using HRM.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRM.Domain.Validations
{
    public class UpdateEmployeeCommandValidation : EmployeeValidation<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidation()
        {
            ValidateId();
            ValidateName();
            ValidateBirthDate();
            ValidateEmail();
        }
    }
}
