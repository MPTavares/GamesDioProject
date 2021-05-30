using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesDIOApi.Model
{
    public class Game
    {
        [Required]
        [StringLength(100,MinimumLength=3,ErrorMessage ="")]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Producer { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
