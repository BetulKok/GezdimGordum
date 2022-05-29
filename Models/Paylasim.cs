using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GezdimGördüm.Models
{
    public class Paylasim
    {
        public int Id { get; set; }
        public string Aciklama { get; set; }
        [Required , MaxLength(255)]
        public string ResimYolu { get; set; }
    }
}
