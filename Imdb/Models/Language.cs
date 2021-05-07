using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Imdb.Models
{
    public class Language 
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public String Code { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }

    }
}
