using System.Threading.Tasks;
using PowerChat.Application.Common.Results;
using PowerChat.Application.Users.Dto;
using PowerChat.Common;
using PowerChat.Common.Interfaces;

namespace PowerChat.Application.Common.Interfaces
{
    public interface IAuthService : IInfrastructureService
    {
        Task<ApplicationResult<JwtDto>> LoginAsync(string email, string password);
    }
}
