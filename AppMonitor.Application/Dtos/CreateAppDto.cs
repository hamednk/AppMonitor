using System;
using System.ComponentModel.DataAnnotations;

namespace AppMonitor.Application.Dtos
{
    public class CreateAppDto
    {
        [Required]
        public string Name { get; set; } 
        [Required]
        [Url]
        public string Url { get; set; } 
        [Required]
        public TimeSpan Interval { get; set; } 
        public string UserId { get; set; } 
    }
}