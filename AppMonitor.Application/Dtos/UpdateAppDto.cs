using System;
using System.ComponentModel.DataAnnotations;

namespace AppMonitor.Application.Dtos
{
    public class UpdateAppDto
    {
        public int Id { get; set; } 
        [Required]
        public string Name { get; set; } 
        [Required]
        [Url]
        public string Url { get; set; } 
        [Required]
        public TimeSpan Interval { get; set; } 
    }
}