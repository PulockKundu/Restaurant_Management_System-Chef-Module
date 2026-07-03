using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventManagement.Shared;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Data;
using RestaurantManagement.Entities;

namespace RestaurantManagement.Repos
{
    public class UserInfoRepo(RmDbContext context)
    {
        public Result<List<UserInfo>> GetAll()
        {
            var result = new Result<List<UserInfo>>()
            {
                Data = new List<UserInfo>()
            };

            try
            {
                result.Data = context.UserInfos.ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }

            return result;

        }

        public Result<UserInfo> GetById(int id)
        {
            var result = new Result<UserInfo>();

            try
            {
                result.Data = context.UserInfos.Find(id);
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

        public Result<UserInfo> Authenticate(string email, string password)
        {
            var result = new Result<UserInfo>();

            try
            {
                result.Data = context.UserInfos.FirstOrDefault(e => e.Email == email && e.Password == password);
                if (result.Data == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid Credentials";
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }

            return result;

        }

        public Result<UserInfo> Save(UserInfo model)
        {
            var result = new Result<UserInfo>();

            try
            {
                if (context.UserInfos.Any(e => e.Email.ToLower() == model.Email.ToLower() && e.ID != model.ID))
                {
                    result.HasError = true;
                    result.Message = "Title already exists.";
                    return result;
                }

                var objToSave = context.UserInfos.Find(model.ID);
                if (objToSave == null)
                {
                    objToSave = new UserInfo();
                    context.UserInfos.Add(objToSave);
                }

                objToSave.Name = model.Name;
                objToSave.Email = model.Email;
                objToSave.Password = model.Password;
                objToSave.Role = model.Role;

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

        public Result<UserInfo> Delete(int id)
        {
            var result = new Result<UserInfo>();

            try
            {
                var entity = context.UserInfos.Find(id);

                if (entity == null)
                {
                    result.HasError = true;
                    result.Message = $"Event type with ID {id} was not found.";
                    return result;
                }

                context.UserInfos.Remove(entity);
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
