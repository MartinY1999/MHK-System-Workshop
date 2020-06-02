using System.Threading.Tasks;

namespace MHKFrontEnd.Services.Interfaces
{
    public interface IAdminService
    {
        Task<bool> AllowAdminUserCreationAsync();
    }
}
