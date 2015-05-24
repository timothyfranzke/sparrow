using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Validation;
using SprwMusic.Models;
using SprwMusic.Models.CreateModels;
using SprwMusic.Models.ViewModels;

namespace SprwMusic.Repository.Impl
{
    public class UserRepository : IUserRepository
    {
        public SPRW_USER CreateUser(CreateUserModel model, int salt)
        {
            var messages = new List<string>();
            var user = new SPRW_USER
            {
                FIRST_NAME = model.FirstName,
                LAST_NAME = model.LastName,
                EMAIL = model.Email,
                SALT = salt,
                CC = "",
                PASSWORD = model.Password,
                LAST_MAINT_TIME = DateTime.Now,
                LAST_MAINT_USER_ID = model.Email
            };
            try
            {
                using (var context = new SparrowMusicEntities11())
                {
                    context.SPRW_USER.Add(user);
                    context.SaveChanges();
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Console.Out.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
            catch (Exception e)
            {
                messages.Add("Exception: " + e);
            }

            return user;
        }
        public SPRW_USER GetUser(string userEmail)
        {
            var messages = new List<string>();
            var user = new SPRW_USER();
            try
            {
                using (var context = new SparrowMusicEntities11())
                {
                    var selectedUser = context.SPRW_USER.FirstOrDefault(i => i.EMAIL.ToLower().Equals(userEmail.ToLower()));
                    if (selectedUser != null)
                    {
                        user.ACT_IND = selectedUser.ACT_IND;
                        user.CC = selectedUser.CC;
                        user.EMAIL = selectedUser.EMAIL;
                        user.FIRST_NAME = selectedUser.FIRST_NAME;
                        user.LAST_MAINT_TIME = selectedUser.LAST_MAINT_TIME;
                        user.LAST_MAINT_USER_ID = selectedUser.LAST_MAINT_USER_ID;
                        user.LAST_NAME = selectedUser.LAST_NAME;
                        user.PASSWORD = selectedUser.PASSWORD;
                        user.SALT = selectedUser.SALT;
                    }
                }
            }
            catch (Exception e)
            {

            }

            return user;
        }
    }
}
