using System;
using System.Collections.Generic;

namespace MangaOnline.Models
{
    public partial class History
    {
        public Guid Id { get; set; }
        public string? User { get; set; }
        public string? Hash { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public string? Value { get; set; }
        public DateTime? Date { get; set; }
    }
}
