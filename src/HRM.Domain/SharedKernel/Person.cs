using HRM.Domain.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRM.Domain.Models
{
    public class Person : ValueObject<Person>
    {
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public Gender Gender { get; private set; }

        public DateTime Dob { get; private set; }

        //For EF
        private Person()
        {

        }

        public Person(string firstName, string lastName, Gender gender, DateTime dob)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Dob = Dob;
        }

        protected override bool EqualsCore(Person other)
        {
            return FirstName == other.FirstName 
                && LastName == other.LastName
                && Gender == other.Gender
                && Dob == other.Dob;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = FirstName.GetHashCode();
                hashCode = (hashCode * 397) ^ LastName.GetHashCode();
                hashCode = (hashCode * 397) ^ Gender.GetHashCode();
                hashCode = (hashCode * 397) ^ Dob.GetHashCode();
                return hashCode;
            }
        }
    }

    public enum Gender
    {
        Male, Female
    }
}
