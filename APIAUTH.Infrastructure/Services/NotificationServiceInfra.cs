using APIAUTH.Infrastructure.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace APIAUTH.Infrastructure.Services
{
    public class NotificationServiceInfra
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationServiceInfra(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyAll(string message)
        {
            var notification = new
            {
                Title = "Nueva Notificación",
                Message = message,
                Timestamp = DateTime.UtcNow
            };
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", notification);
        }

        public async Task NotifyUser(string userId, string message)
        {
            var notification = new
            {
                Title = "Nueva Notificación",
                Message = message,
                Timestamp = DateTime.UtcNow
            };
      
            await _hubContext.Clients.User(userId).SendAsync("ReceiveNotification", message);
        }

        public async Task NotifyUserAdmin(string[] userIds, NotificationDto payload)
        {
            await _hubContext.Clients.Users(userIds).SendAsync("ReceiveNotification", payload);
        }
    }
}
