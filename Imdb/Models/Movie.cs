using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Imdb.Models
{
    public class Movie
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string MovieTitle { get; set; }

        [Required]
        public int? LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public virtual Language Language { get; set; }

        public int Prod_year { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string MovieDescription { get; set; }

        [Required]
        public int? ProducerId { get; set; }
        [ForeignKey("ProducerId")]
        public virtual Producer Producer { get; set; }

        public List<Actor> Actors { get; set; } = new List<Actor>();
        [Required]
        public int? CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

    }
}
