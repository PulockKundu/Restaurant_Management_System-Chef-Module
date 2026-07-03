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
    public class CategoriesRepo(RmDbContext context, CurrentUserHelper currentUserHelper)
    {
        public Result<List<Categories>> GetAll()
        {
            var result = new Result<List<Categories>>()
            {
                Data = new List<Categories>()
            };

            try
            {
                result.Data = context.Categoriess.ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }

            return result;

        }

        public Result<Categories> GetById(int id)
        {
            var result = new Result<Categories>();

            try
            {
                result.Data = context.Categoriess.Find(id);
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

        public Result<Categories> Save(Categories model)
        {
            var result = new Result<Categories>();

            try
            {
                if (context.Categoriess.Any(e => e.CategoriesName.ToLower() == model.CategoriesName.ToLower() && e.ID != model.ID))
                {
                    result.HasError = true;
                    result.Message = "Title Category exists.";
                    return result;
                }

                var objToSave = context.Categoriess.Find(model.ID);
                if (objToSave == null)
                {
                    objToSave = new Categories();
                    context.Categoriess.Add(objToSave);
                }

                objToSave.CategoriesName = model.CategoriesName;
                objToSave.Description = model.Description;
                objToSave.IsActive = model.IsActive;
                
                objToSave.CreatedAt = DateTime.Now;
                objToSave.UpdatedAt = DateTime.Now;
                objToSave.UpdatedBy = 1;

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

        public Result<Categories> Delete(int id)
        {
            var result = new Result<Categories>();

            try
            {
                var entity = context.Categoriess.Find(id);

                if (entity == null)
                {
                    result.HasError = true;
                    result.Message = $"Event type with ID {id} was not found.";
                    return result;
                }

                context.Categoriess.Remove(entity);
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
