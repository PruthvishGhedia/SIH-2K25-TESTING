using Microsoft.AspNetCore.SignalR;

namespace SIH.ERP.Soap.Hubs;

public class DashboardHub : Hub
{
    public async Task SendDashboardUpdate(string updateType, object data)
    {
        await Clients.All.SendAsync("ReceiveDashboardUpdate", updateType, data);
    }

    public async Task SendStudentUpdate(object student)
    {
        await Clients.All.SendAsync("ReceiveStudentUpdate", student);
    }

    public async Task SendCourseUpdate(object course)
    {
        await Clients.All.SendAsync("ReceiveCourseUpdate", course);
    }

    public async Task SendDepartmentUpdate(object department)
    {
        await Clients.All.SendAsync("ReceiveDepartmentUpdate", department);
    }

    public async Task SendFeesUpdate(object fees)
    {
        await Clients.All.SendAsync("ReceiveFeesUpdate", fees);
    }

    public async Task SendExamUpdate(object exam)
    {
        await Clients.All.SendAsync("ReceiveExamUpdate", exam);
    }

    public async Task SendUserUpdate(object user)
    {
        await Clients.All.SendAsync("ReceiveUserUpdate", user);
    }
}