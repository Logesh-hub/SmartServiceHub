using Grpc.Core;
using UserService.Protos;

namespace UserService.Services
{
    public class UserGroupService : Protos.UserService.UserServiceBase
    {
        public override Task<UserResponse> GetUser(UserRequest request, ServerCallContext context)
        {
            return Task.FromResult(new UserResponse
            {
                UserId = request.UserId,
                UserName = "Lokimon"
            });
        }
    }
}
