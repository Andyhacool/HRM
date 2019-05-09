using AutoMapper;
using HRM.Application.ViewModels;
using HRM.Domain;
using HRM.Domain.Models;

namespace HRM.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Employee, EmployeeViewModel>();
        }
    }
}
