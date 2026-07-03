using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Entities
{
    [Table("OrderItems")]
    public class OrderItems
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int TableOrderID { get; set; }

        [Display(Name = "Name")]
        public int MenuItemsID { get; set; }   

        public int OrderQuantity { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } 
        public int UpdatedBy { get; set; }

        [ForeignKey("MenuItemsID")]
        public virtual MenuItems MenuItemss { get; set; }
    }
}
