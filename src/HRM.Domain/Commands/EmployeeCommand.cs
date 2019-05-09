using HRM.Domain.Core.Commands;
using HRM.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRM.Domain.Commands
{
    public abstract class EmployeeCommand : Command
    {
        public Guid Id { get; set; }

        public DateTime Date_Joined { get; protected set; }

        public DateTime? Date_Left { get; protected set; }

        public string Note { get; protected set; }

        public string Street { get; protected set; }

        public string City { get; protected set; }

        public string State { get; protected set; }

        public string ZipCode { get; protected set; }

        public string Country { get; protected set; }

        public string FirstName { get; protected set; }

        public string LastName { get; protected set; }

        public string Email { get; set; }

        public Gender Gender { get; protected set; }

        public DateTime Dob { get; protected set; }

        public DateTime Created_Date { get; protected set; }
    }
}
