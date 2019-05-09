using HRM.Domain.Core.Model;
using HRM.Domain.Models;
using HRM.Domain.SharedKernel;
using System;

namespace HRM.Domain
{
    public class Employee : AggregateRoot
    {
        public DateTime Date_Joined { get; }

        public DateTime? Date_Left { get; }

        public string Note { get; }

        public Address Address { get; private set; }

        public Person Person { get; private set; }

        public DateTime Created_Date { get; }

        public string Email { get; set; }

        // Empty constructor for EF
        public Employee() { }

        public Employee(string firstName, string lastName, DateTime dob, Gender gender, DateTime dateJoined)
        {
            Created_Date = DateTime.Now;

            UpdatePersonalDetail(firstName, lastName, dob, gender);
        }

        public void UpdatePersonalDetail(string firstName, string lastName, DateTime dob, Gender gender)
        {
            Person = new Person(firstName, lastName, gender, dob);
        }

        public void UpdateAddress(string street, string city, string state, string zipCode)
        {
            Address = new Address(street, city, state, zipCode);
        }
    }
}
