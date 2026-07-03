using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Entities;

namespace RestaurantManagement.Data
{
    public class RmDbContext(DbContextOptions<RmDbContext> options) : DbContext(options)
    {
        public DbSet<Categories> Categoriess { get; set; }
        public DbSet<MenuItems> MenuItemss { get; set; }
        public DbSet<OrderItems> OrderItemss { get; set; }
        public DbSet<KitchenTask> KitchenTasks { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<Inventory> Inventorys { get; set; }
    }
}
