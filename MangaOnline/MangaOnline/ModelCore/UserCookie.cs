﻿namespace MangaOnline.ModelCore;

public class UserCookie
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string PhoneNumber { get; set; }

    public string? Avartar { get; set; }
}