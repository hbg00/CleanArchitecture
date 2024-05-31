using Grpc.Core;
using GrpcService.Protos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GrpcService.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrpcService
{
    public class UserServiceImpl : UserService.UserServiceBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserServiceImpl(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public override async Task<UserList> ListUsers(Empty request, ServerCallContext context)
        {
            var allUsers = await _userManager.Users.ToListAsync();
            var nonAdminUsers = new List<User>();

            foreach (var user in allUsers)
            {
                if (!await _userManager.IsInRoleAsync(user, "Administrator"))
                {
                    nonAdminUsers.Add(new User
                    {
                        Id = user.Id,
                        Username = user.UserName,
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    });
                }
            }

            var userList = new UserList();
            userList.Users.AddRange(nonAdminUsers);

            return userList;
        }

        public override async Task<Empty> DeleteUser(UserId request, ServerCallContext context)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            return new Empty();
        }
    }
}
