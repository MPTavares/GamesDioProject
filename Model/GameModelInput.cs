using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesDIOApi.Model
{
    public class GameModelInput
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The game name must contain between 3 and 100 characters.")]
        public string Name { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The producer of game must contain between 3 and 100 characters.")]
        public string Producer { get; set; }
        [Required]
        [Range(1,5000, ErrorMessage = "The value must be greater than 0 and less than 5000.")]
        public double Price { get; set; }
    }
}
