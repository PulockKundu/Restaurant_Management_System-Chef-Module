using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagement.Entities
{
    [Table("KitchenTask")]
    public class KitchenTask
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int ID { get; set; }
        [Display(Name = "OrderItem")]
        public int OrderItemsID { get; set; }
        public int MenuItemsID { get; set; }
        public int ChefID { get; set; }  

        public string Status { get; set; }  

        public DateTime StartedAt { get; set; }

        public DateTime CompletedAt { get; set; }

        public int UpdatedBy { get; set; }

        [ForeignKey("OrderItemsID")]
        public virtual OrderItems OrderItemss { get; set; }

        [ForeignKey("MenuItemsID")]
        public virtual MenuItems MenuItemss{ get; set; }



    }
}
