using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Imdb.Models
{
    public class Company
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string CompanyDescription { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
