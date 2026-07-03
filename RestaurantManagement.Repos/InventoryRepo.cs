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
    public class InventoryRepo(RmDbContext context, CurrentUserHelper currentUserHelper)
    {
        public Result<List<Inventory>> GetAll()
        {
            var result = new Result<List<Inventory>>()
            {
                Data = new List<Inventory>()
            };

            try
            {
                result.Data = context.Inventorys.ToList();


            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }

            return result;

        }

        public Result<Inventory> GetById(int id)
        {
            var result = new Result<Inventory>();

            try
            {
                //result.Data = context.Inventorys.Find(id);
                result.Data = context.Inventorys.FirstOrDefault(k => k.ID == id);

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

        public Result<Inventory> Save(Inventory model)
        {
            var result = new Result<Inventory>();

            try
            {
               

                var objToSave = context.Inventorys.Find(model.ID);
                if (objToSave == null)
                {
                    
                    result.HasError = true;
                    result.Message = "Invalid Inventory ID";
                    return result;
                }




                if (model.Quantity > objToSave.Quantity)
                {
                    result.HasError = true;
                    result.Message = "Invalid Quantity";
                    return result;
                }
                else
                {
                    objToSave.Quantity = objToSave.Quantity - model.Quantity;
                }
                
                //objToSave.Unit = model.Unit;
                objToSave.UpdatedAt = DateTime.Now;
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

        public Result<Inventory> Delete(int id)
        {
            var result = new Result<Inventory>();

            try
            {
                var entity = context.Inventorys.Find(id);

                if (entity == null)
                {
                    result.HasError = true;
                    result.Message = $"Event type with ID {id} was not found.";
                    return result;
                }

                context.Inventorys.Remove(entity);
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
