using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventManagement.Shared;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Data;
using RestaurantManagement.Entities;
using RestaurantManagement.Shared;

namespace RestaurantManagement.Repos
{
    public class KitchenTaskRepo(RmDbContext context, CurrentUserHelper currentUserHelper)
    {
        public Result<List<KitchenTask>> GetAll()
        {
            var result = new Result<List<KitchenTask>>()
            {
                Data = new List<KitchenTask>()
            };

            try
            {
                result.Data = context.KitchenTasks
                    .Include(k => k.OrderItemss).Include(s=>s.MenuItemss).ToList();


            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }

            return result;

        }

        public Result<KitchenTask> GetById(int id)
        {
            var result = new Result<KitchenTask>();

            try
            {
                //result.Data = context.KitchenTasks.Find(id);
                result.Data = context.KitchenTasks
                    .Include(k => k.MenuItemss)
                    .Include(k => k.OrderItemss)
                    .FirstOrDefault(k => k.ID == id);

                if (result.Data == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid Id";
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }

            return result;

        }

        public Result<List<KitchenTask>> GetByChef(int chefId)
        {
            var result = new Result<List<KitchenTask>>()
            {
                Data = new List<KitchenTask>()
            };

            try
            {
                result.Data = context.KitchenTasks
                    .Include(k => k.MenuItemss)
                    .Include(k => k.OrderItemss)
                    .Where(k => k.ChefID == chefId && k.Status == "Ready")
                    .ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }

            return result;
        }
        public Result<KitchenTask> Save(KitchenTask model)
        {
            var result = new Result<KitchenTask>();

            try
            {
                
                var objToSave = context.KitchenTasks.Find(model.ID);
                if (objToSave == null)
                {
                    
                    result.HasError = true;
                    result.Message = "Invalid KitchenTask ID";
                    return result;
                }

                if (currentUserHelper.Role == "chef")
                {
                    objToSave.ChefID = currentUserHelper.UserId;
                }
                else
                {
                    objToSave.ChefID = model.ChefID;
                }

                objToSave.Status = model.Status;
                objToSave.StartedAt = DateTime.Now;
                objToSave.CompletedAt = DateTime.Now;
                objToSave.UpdatedBy = currentUserHelper.UserId;

                context.SaveChanges();
                result.Data = objToSave;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }

            return result;
        }

        public Result<KitchenTask> Delete(int id)
        {
            var result = new Result<KitchenTask>();

            try
            {
                var entity = context.KitchenTasks.Find(id);

                if (entity == null)
                {
                    result.HasError = true;
                    result.Message = $"Event type with ID {id} was not found.";
                    return result;
                }

                context.KitchenTasks.Remove(entity);
                context.SaveChanges();

                result.Data = entity;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = "An error occurred while deleting the event type.";
            }

            return result;
        }

    }
}
