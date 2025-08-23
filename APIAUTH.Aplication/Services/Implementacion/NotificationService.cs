using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Services.Interfaces;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Enums;
using APIAUTH.Domain.Repository;
using APIAUTH.Infrastructure;
using APIAUTH.Infrastructure.Services;
using Microsoft.AspNet.SignalR.Messaging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.Services.Implementacion
{
    public class NotificationService : INotificationService
    {
        private readonly NotificationServiceInfra _notificationService;
        private readonly IRepository<User> _userRepository;

        public NotificationService(NotificationServiceInfra notificationService, IRepository<User> userRepository)
        {
            _notificationService = notificationService;
            _userRepository = userRepository;
        }

        public async Task NotificationByCancelOrder(int orderId)
        {
            var adminId = (int)RoleEnum.Admin;
            var vendId = (int)RoleEnum.Vendedor;

            var users = await _userRepository
                .GetFiltered(u => u.RoleId == adminId || u.RoleId == vendId)
                .Select(u => u.Id.ToString())
                .ToArrayAsync();

            var notification = new NotificationDto()
            {
                Title = "Compra Cancelada",
                Content = $"La compra {orderId} ha sido cancelada",
                Uri = $"/api/Order/{orderId}",
                DateNotify = DateTime.UtcNow
            };

             await _notificationService.NotifyUserAdmin(users, notification);
        }

        public async Task NotificationByConfirmOrder(int orderId)
        {
            var adminId = (int)RoleEnum.Admin;
            var vendId = (int)RoleEnum.Vendedor;

            var users = await _userRepository
                .GetFiltered(u => u.RoleId == adminId || u.RoleId == vendId)
                .Select(u => u.Id.ToString())
                .ToArrayAsync();


            var notification = new NotificationDto()
            {
                Title = "Compra Confirmada",
                Content = $"Se ha realizado una nueva compra {orderId}",
                Uri = $"/api/Order/{orderId}",
                DateNotify = DateTime.UtcNow
            };
            await _notificationService.NotifyUserAdmin(users, notification);
        }

        public async Task NotificationByPayOrder(int orderId)
        {
            var adminId = (int)RoleEnum.Admin;
            var vendId = (int)RoleEnum.Vendedor;

            var users = await _userRepository
                .GetFiltered(u => u.RoleId == adminId || u.RoleId == vendId)
                .Select(u => u.Id.ToString())
                .ToArrayAsync();

            var notification = new NotificationDto()
            {
                Title = "Compra Pagada",
                Content = $"La compra {orderId} ha sido pagada correctamente",
                Uri = $"/api/Order/{orderId}",
                DateNotify = DateTime.UtcNow
            };
            await _notificationService.NotifyUserAdmin(users, notification);
        }
    }
}
