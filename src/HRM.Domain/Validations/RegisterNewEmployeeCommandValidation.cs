using HRM.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRM.Domain.Validations
{
    public class RegisterNewEmployeeCommandValidation : EmployeeValidation<RegisterNewEmployeeCommand>
    {
        public RegisterNewEmployeeCommandValidation()
        {
            ValidateName();
            ValidateBirthDate();
            ValidateEmail();
        }
    }
}
