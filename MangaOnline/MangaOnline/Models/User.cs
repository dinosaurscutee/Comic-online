using System;
using System.Collections.Generic;

namespace MangaOnline.Models
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            FollowLists = new HashSet<FollowList>();
        }

        public Guid Id { get; set; }
        public long SubId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool EmailConfirmed { get; set; }
        public string Password { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public int AccessFailedCount { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? ModifiedAt { get; set; }
        public bool IsActive { get; set; }
        public int Status { get; set; }
        public string? Avatar { get; set; }

        public virtual UserRole? UserRole { get; set; }
        public virtual UserToken? UserToken { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<FollowList> FollowLists { get; set; }
    }
}
