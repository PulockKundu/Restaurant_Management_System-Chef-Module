using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Entities
{
    [Table("UserInfo")]
    public class UserInfo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; } = null!;
        [Required]
        [StringLength(250)]
        public string Email { get; set; } = null!;
        [Required]
        [StringLength(50)]
        public string Password { get; set; } = null!;
        [Required]
        [StringLength(50)]
        public string Role { get; set; } = null!;

    }
}
