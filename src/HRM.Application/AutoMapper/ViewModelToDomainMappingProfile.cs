using AutoMapper;
using HRM.Application.ViewModels;
using HRM.Domain.Commands;

namespace HRM.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<EmployeeViewModel, RegisterNewEmployeeCommand>()
                .ConstructUsing(c => new RegisterNewEmployeeCommand(c.FirstName, c.LastName, c.Gender, c.Detail, c.Dob));
            CreateMap<EmployeeViewModel, UpdateEmployeeCommand>()
                .ConstructUsing(c => new UpdateEmployeeCommand());
        }
    }
}
