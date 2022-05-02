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
    [Table("matches")]
    public class Match
    {
        //id, userid1, userid2

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey(nameof(_User_1))]
        public int User_1 { get; set; }

        [ForeignKey(nameof(_User_2))]

        public int User_2 { get; set; }

        public bool DeletedMatch { get; set; } = false;

        [NotMapped]
        [JsonIgnore]
        public virtual User _User_1 { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual User _User_2 { get; set; }

        public override string ToString()
        {
            return "{ Id: " + this.Id + " | User1: " + this.User_1 + " | User2: " + this.User_2 + " }";
        }
    }
}
