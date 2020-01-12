using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Codenation.Challenge.Models
{
    public class Submission
    {
        [Column("user_id")]
        [Key]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        [Required]
        public User User { get; set; }

        [Column("challenge_id")]
        public int ChallengeId { get; set; }

        [ForeignKey("ChallengeId")]
        [Required]
        public Challenge Challenge { get; set; }

        [Column("score", TypeName = "decimal(9,2)")]
        [Required]
        public decimal Score { get; set; }

        [Column("created_at")]
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
