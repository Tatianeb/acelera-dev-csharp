using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Codenation.Challenge.Models
{
    [Table("user")]
    public class User
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("full_name")]
        [MaxLength(100)]
        [Required]
        public string FullName { get; set; }

        [Column("email")]
        [MaxLength(100)]
        [Required]
        public string Email { get; set; }

        [Column("nickname")]
        [MaxLength(50)]
        [Required]
        public string Nickname { get; set; }

        [Column("password")]
        [MaxLength(255)]
        [Required]
        public string Password { get; set; }

        [Column("created_at")]
        [Required]
        public DateTime CreatedAt { get; set; }

        public List<Candidate> Candidates { get; set; }

        public List<Submission> submission { get; set; }
    }
}
