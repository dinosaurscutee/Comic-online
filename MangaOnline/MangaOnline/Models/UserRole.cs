﻿using System;
using System.Collections.Generic;

namespace MangaOnline.Models
{
    public partial class UserRole
    {
        public long SubId { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
