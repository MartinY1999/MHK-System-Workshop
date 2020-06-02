using System.ComponentModel.DataAnnotations;

namespace MHKDTO.Models
{
    public class Finance
    {
        public int Id { get; set; }

        [Required]
        public decimal BSPE { get; set; }

        [Required]
        public decimal Minimum { get; set; }

        [Required]
        public decimal Maximum { get; set; }

        [Required]
        [Range(1, 12)]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public decimal MonthlySum { get; set; }

        [Required]
        public int PeopleCount { get; set; }
    }
}
