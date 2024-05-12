using Microsoft.AspNetCore.SignalR;

namespace AttendanceManagement.WebAPI.Hubs
{
    public class AttendanceHub : Hub
    {
        public async Task SendAttendance(Guid userId, DateTime time, string status)
        {
            await Clients.All.SendAsync("ReceiveMessage", new
            {
                UserId = userId,
                Time = time,
                Status = status
            });
        }
    }
}
