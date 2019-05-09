using FluentValidation.Results;
using HRM.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRM.Domain.Core.Commands
{
    public abstract class Command : Message
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public abstract bool IsValid();
    }
}
