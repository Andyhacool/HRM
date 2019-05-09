using HRM.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRM.Infra.Data.Mappings
{    
    public class EmployeeMap : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.Property(c => c.Email)
                .HasColumnType("varchar(100)")
                .HasMaxLength(11)
                .IsRequired();

            builder.OwnsOne(p => p.Person)
                .Property(person => person.FirstName)
                .HasColumnName("FirstName");

            builder.OwnsOne(p => p.Person)
                .Property(person => person.LastName)
                .HasColumnName("LastName");

            builder.OwnsOne(p => p.Person)
                .Property(person => person.Gender)
                .HasColumnName("Gender");

            builder.OwnsOne(p => p.Person)
                .Property(person => person.Dob)
                .HasColumnName("Dob");

            builder.OwnsOne(p => p.Address)
                .Property(address => address.Street)
                .HasColumnName("Street");

            builder.OwnsOne(p => p.Address)
                .Property(address => address.City)
                .HasColumnName("City");

            builder.OwnsOne(p => p.Address)
                .Property(address => address.State)
                .HasColumnName("State");

            builder.OwnsOne(p => p.Address)
                .Property(address => address.ZipCode)
                .HasColumnName("ZipCode");


            builder.OwnsOne(p => p.Address)
                .Property(address => address.Country)
                .HasColumnName("Country");
        }    
    }
}