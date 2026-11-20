using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDay.Models
{
    public class EventDto
    {
        [Required, MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateOnly EventDate { get; set; }

        [Required]
        public TimeOnly StartTime { get; set; }

        [Required]
        public TimeOnly EndTime { get; set; }

        [Required]
        public RaceDay.Models.Type EventType { get; set; }

        [Required]
        public int LocationID { get; set; }

        public IFormFile? BannerFile { get; set; }
    }
}
