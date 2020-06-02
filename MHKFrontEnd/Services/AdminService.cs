using MHKFrontEnd.Data;
using MHKFrontEnd.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace MHKFrontEnd.Services
{
    public class AdminService : IAdminService
    {
        private readonly IServiceProvider _serviceProvider;

        private bool _adminExists;

        public AdminService(IServiceProvider provider)
        {
            _serviceProvider = provider;
        }

        public async Task<bool> AllowAdminUserCreationAsync()
        {
            if (_adminExists)
                return false;
            else
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();

                    if (await dbContext.Users.AnyAsync(user => user.IsAdmin))
                    {
                        // There are already admin users so disable admin creation
                        _adminExists = true;
                        return false;
                    }

                    // There are no admin users so enable admin creation
                    return true;
                }
            }
        }
    }
}
