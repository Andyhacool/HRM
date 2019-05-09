using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRM.Domain.Core.Requests
{
    public abstract class Request 
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        protected Request()
        {
            Timestamp = DateTime.Now;
        }

        public abstract bool IsValid();
    }
}
