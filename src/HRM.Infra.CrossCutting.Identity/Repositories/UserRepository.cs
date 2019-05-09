using AutoMapper;
using HRM.Domain.Interfaces;
using HRM.Domain.Models;
using HRM.Domain.Models.Identity;
using HRM.Infra.CrossCutting.Identity.Models;
using HRM.Infra.CrossCutting.Identity.Specifications;
using HRM.Infra.Data.Context;
using HRM.Infra.Data.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Infra.CrossCutting.Identity.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserRepository(UserManager<AppUser> userManager, IMapper mapper, HRMContext context) : base(context)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<User> FindByName(string userName)
        {
            var appUser = await _userManager.FindByNameAsync(userName);
            return appUser == null ? null : _mapper.Map(appUser, await GetSingleBySpec(new UserSpecification(appUser.Id)));
        }

        public async Task<bool> CheckPassword(User user, string password)
        {
            return await _userManager.CheckPasswordAsync(_mapper.Map<AppUser>(user), password);
        }
    }
}
