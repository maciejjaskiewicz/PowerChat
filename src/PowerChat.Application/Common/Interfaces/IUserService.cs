using System.Threading.Tasks;
using PowerChat.Application.Common.Results;
using PowerChat.Common;
using PowerChat.Common.Interfaces;
using PowerChat.Common.Results;
using PowerChat.Domain.Entities;

namespace PowerChat.Application.Common.Interfaces
{
    public interface IUserService : IInfrastructureService
    {
        Task<User> GetUserAsync(string email);
        Task<bool> UserExistsAsync(string email);
        Task<ValidationResult> ValidatePasswordAsync(string password);
        Task<ApplicationResult<long>> CreateUserAsync(User user, string password);
    }
}
