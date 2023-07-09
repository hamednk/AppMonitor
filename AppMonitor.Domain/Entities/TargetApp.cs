using System;
using System.ComponentModel.DataAnnotations;

namespace AppMonitor.Domain.Entities
{
    public class TargetApp
    {
        public int Id { get; set; } 
        [Required]
        public string Name { get; set; } 
        [Required]
        [Url]
        public string Url { get; set; } 
        [Required]
        public TimeSpan Interval { get; set; } 
        public bool IsUp { get; set; } 
        public DateTime? LastChecked { get; set; } 
        public string UserId { get; set; } 
        public User User { get; set; } 
    }
}