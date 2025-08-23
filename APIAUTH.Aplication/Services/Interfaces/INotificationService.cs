using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.Services.Interfaces
{
    public interface INotificationService
    {
        Task NotificationByConfirmOrder(int orderId);
        Task NotificationByCancelOrder(int orderId);
        Task NotificationByPayOrder(int orderId);

    }
}
