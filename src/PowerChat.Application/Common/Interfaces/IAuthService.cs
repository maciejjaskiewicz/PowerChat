using System.Threading.Tasks;
using PowerChat.Application.Common.Results;
using PowerChat.Application.Users.Dto;
using PowerChat.Common;

namespace PowerChat.Application.Common.Interfaces
{
    public interface IAuthService : IInfrastructureService
    {
        Task<ApplicationResult<JwtDto>> LoginAsync(string email, string password);
    }
}
