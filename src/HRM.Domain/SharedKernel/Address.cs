using HRM.Domain.Core.Model;
using HRM.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRM.Domain.SharedKernel
{
    public class Address : ValueObject<Address>
    {
        public string Street { get; private set; }

        public string City { get; private set; }

        public string State { get; private set; }

        public string ZipCode { get; private set; }

        public string Country { get; private set; }

        //For EF
      
        private Address()
        {

        }
        public Address(string street, string city, string state, string zipCode)
        {
            Street = street;
            City = city;
            State = state;
            ZipCode = zipCode;
            Country = "Australia";
        }
        protected override bool EqualsCore(Address other)
        {
            return Street == other.Street
                  && City == other.City
                  && State == other.State
                  && ZipCode == other.ZipCode;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = Street.GetHashCode();
                hashCode = (hashCode * 397) ^ City.GetHashCode();
                hashCode = (hashCode * 397) ^ State.GetHashCode();
                hashCode = (hashCode * 397) ^ ZipCode.GetHashCode();
                return hashCode;
            }
        }
    }
}
