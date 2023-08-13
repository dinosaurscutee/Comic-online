using System;
using System.Collections.Generic;

namespace MangaOnline.Models
{
    public partial class UserToken
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; } = null!;
        public DateTime Expires { get; set; }
        public string? Value { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
