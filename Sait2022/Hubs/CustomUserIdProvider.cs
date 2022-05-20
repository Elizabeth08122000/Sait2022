using Microsoft.AspNetCore.SignalR;

namespace Sait2022.Hubs
{
    public class CustomUserIdProvider:IUserIdProvider
    {
        public virtual string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.Identity.Name;
        }
    }
}
