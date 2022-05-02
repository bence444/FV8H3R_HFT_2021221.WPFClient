using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FV8H3R_HFT_2021221.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Bio { get; set; }

        public DateTime RegDate { get; set; }

        public int AvailableLikes { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Match> Matches { get; set; }

        public User()
        {
            Matches = new HashSet<Match>();
        }

        public override string ToString()
        {
            return "{ Id: " + this.Id + " | Name: " + this.Name + " | Bio: " + this.Bio + " | Regdate: " + this.RegDate + " | Likes: " + this.AvailableLikes + " } ";
        }
    }
}