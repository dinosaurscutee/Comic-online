﻿using Microsoft.AspNetCore.SignalR;

namespace MangaOnline.Pages.Hubs;

public class NotificationHub : Hub
{
    public async Task SendMessage(string message)
    {
        await LoadMessages();
    }

    public async Task LoadMessages()
    { 
        await Clients.All.SendAsync("LoadNotification", "ff");
    }
}