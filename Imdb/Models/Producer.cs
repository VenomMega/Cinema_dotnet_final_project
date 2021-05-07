using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Imdb.Models
{
    public class Producer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string FullName { get; set; }

        public int BornYear { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }



    }
}
