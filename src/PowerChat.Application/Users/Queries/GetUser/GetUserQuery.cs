using MediatR;
using PowerChat.Application.Users.Queries.GetUser.Models;

namespace PowerChat.Application.Users.Queries.GetUser
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
