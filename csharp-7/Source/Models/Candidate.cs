using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Codenation.Challenge.Models
{
    [Table("candidate")]
    public class Candidate
    {

        [Column("user_id")]
        [Key]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        [Required]
        public User User { get; set; }

        [Column("acceleration_id")]
        public int AccelerationId { get; set; }

        [ForeignKey("AccelerationId")]
        [Required]
        public Acceleration Acceleration { get; set; }

        [Column("company_id")]
        public int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        [Required]
        public Company Company { get; set; }

        [Column("status")]
        [Required]
        public int Status { get; set; }

        [Column("created_at")]
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
