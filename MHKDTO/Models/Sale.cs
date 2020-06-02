using System.ComponentModel.DataAnnotations;

namespace MHKDTO.Models
{
    public class Sale
    {
        public int Id { get; set; }

        [Required]
        [Range(1, 12)]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public decimal WeightNK { get; set; }

        [Required]
        public decimal Minimum { get; set; }

        [Required]
        public decimal Maximum { get; set; }

        [Required]
        public int TotalTasks { get; set; }

        [Required]
        public int TasksOnTime { get; set; }
    }
}
