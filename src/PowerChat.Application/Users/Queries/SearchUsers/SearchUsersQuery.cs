using System.Collections.Generic;
using MediatR;
using PowerChat.Application.Users.Queries.SearchUsers.Models;

namespace PowerChat.Application.Users.Queries.SearchUsers
{
    public class SearchUsersQuery : IRequest<IList<UserPreviewModel>>
    {
        public string SearchStr { get; set; }
        public bool ExcludeFriends { get; set; } = false;
    }
}
