using Microsoft.AspNetCore.SignalR;
using SecurityGateApv.Domain.Interfaces.DomainDTOs;
using SecurityGateApv.Domain.Interfaces.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Infras.Notifications
{
    public class NotificationHub : Hub
    {
        private IDictionary<string, UserConnectionDTO> _connections;
        public NotificationHub(IDictionary<string, UserConnectionDTO> connections)
        {
            _connections = connections;
        }
        public async Task<string> JoinHub(UserConnectionDTO conn)
        {
            _connections[Context.ConnectionId] = conn;
            await Clients.All.SendAsync("ReceiveMessage", "admind", $"{Context.ConnectionId} has joined");
            return await Task.FromResult("");
        }

        public async Task<string> SendMessage()
        {
            await Clients.All.SendAsync("ReceiveMessage", "admind", $"Test API");
            return await Task.FromResult("");
        }
        public async Task<string> SendMessageAssignForStaff(string title, string description, int staffId, int scheduleId)
        {
            try
            {
                var staffs = _connections.Where(s => s.Value.UserId == staffId);
                foreach (var staff in staffs)
                {
                    if (staff.Value != null)
                    {
                        await Clients.Client(staff.Key).SendAsync("ReceiveNotification", title, description, scheduleId);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return await Task.FromResult("");
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            _connections.Remove(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
