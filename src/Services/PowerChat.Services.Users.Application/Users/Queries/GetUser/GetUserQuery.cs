using MediatR;
using PowerChat.Services.Users.Application.Users.Queries.GetUser.Models;

namespace PowerChat.Services.Users.Application.Users.Queries.GetUser
{
    public class GetUserQuery : IRequest<UserModel>
    {
        public long Id { get; }

        public GetUserQuery(long id)
        {
            Id = id;
        }
    }
}
