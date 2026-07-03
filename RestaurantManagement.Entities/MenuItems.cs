using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Entities
{
    [Table("MenuItems")]
    public class MenuItems
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Display(Name = "Name")]
        public string ItemsName { get; set; }  
        public int CategoriesID { get; set; }  

        public int Price { get; set; }
        public string Description { get; set; } 
        public bool IsActive { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; } 

        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; } 
        public int UpdatedBy { get; set; }

        [ForeignKey("CategoriesID")]
        public virtual Categories Categoriess { get; set; }
    }
}
