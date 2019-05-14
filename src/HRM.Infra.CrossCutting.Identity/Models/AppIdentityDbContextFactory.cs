using HRM.Infra.CrossCutting.Identity.Context;
using HRM.Infra.Data.Shared;
using Microsoft.EntityFrameworkCore;

namespace HRM.Infra.CrossCutting.Identity.Models
{
    public class AppIdentityDbContextFactory : DesignTimeDbContextFactoryBase<AppIdentityDbContext>
    {
        protected override AppIdentityDbContext CreateNewInstance(DbContextOptions<AppIdentityDbContext> options)
        {
            return new AppIdentityDbContext(options);
        }
    }
}
