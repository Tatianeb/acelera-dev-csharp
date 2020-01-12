using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codenation.Challenge.Models
{
    [Table("challenge")]
    public class Challenge
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("name")]
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [Column("slug")]
        [MaxLength(50)]
        [Required]
        public string Slug { get; set; }

        [Column("created_at")]
        [Required]
        public DateTime CreatedAt { get; set; }

        public List<Acceleration> Accelerations { get; set; }

        public List<Submission> submission { get; set; }

    }
}
