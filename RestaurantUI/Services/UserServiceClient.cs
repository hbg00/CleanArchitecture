using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using GrpcService.Protos;
using System;
using System.Net.Http;
using System.Threading.Tasks;

public class UserServiceClient
{
    private readonly UserService.UserServiceClient _userServiceClient;

    public UserServiceClient()
    {
        var httpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler());
        var channel = GrpcChannel.ForAddress("https://localhost:7286", new GrpcChannelOptions { HttpHandler = httpHandler });
        _userServiceClient = new UserService.UserServiceClient(channel);
    }

    public async Task<UserList> ListUsersAsync()
    {
        try
        {
            return await _userServiceClient.ListUsersAsync(new Empty());
        }
        catch (RpcException ex)
        {
            // Handle RpcException (e.g., server not reachable, gRPC server-side error)
            throw new Exception("Error listing users: " + ex.Status.Detail, ex);
        }
        catch (Exception ex)
        {
            // Handle other exceptions
            throw new Exception("Error listing users: " + ex.Message, ex);
        }
    }

    public async Task DeleteUserAsync(string userId)
    {
        try
        {
            await _userServiceClient.DeleteUserAsync(new UserId { Id = userId });
        }
        catch (RpcException ex)
        {
            // Handle RpcException (e.g., server not reachable, gRPC server-side error)
            throw new Exception("Error deleting user: " + ex.Status.Detail, ex);
        }
        catch (Exception ex)
        {
            // Handle other exceptions
            throw new Exception("Error deleting user: " + ex.Message, ex);
        }
    }
}
