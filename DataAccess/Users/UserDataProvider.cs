using System;
using System.Collections.Generic;
using System.Linq;
using TopTal.JoggingApp.BusinessEntities.Users;
using TopTal.JoggingApp.CallContext;
using TopTal.JoggingApp.DataAccess.Helpers;
using TopTal.JoggingApp.Exceptions.Entities;

namespace TopTal.JoggingApp.DataAccess.Users
{
    /// <summary>
    /// Lifetime: Transient
    /// </summary>
    public sealed  class UserDataProvider : DataProviderBase
    {
        public UserDataProvider(
            ICallContext callContext,
            AppDbContext appDbContext
            )
            : base(callContext, appDbContext)
        {
        }

        /// <summary>
        /// Returns entities for listing purposes.
        /// </summary>
        public IEnumerable<User> GetList(string freeTextFilter, int? maxRows)
        {
            if (string.IsNullOrEmpty(freeTextFilter))
                return AppDbContext.Users.Take(maxRows.GetValueOrDefault(int.MaxValue));
            else
            {
                return AppDbContext.Users
                    .Where(t =>
                        t.FirstName.ToLower().Contains(freeTextFilter.ToLower()) ||
                        t.LastName.ToLower().Contains(freeTextFilter.ToLower()) ||
                        t.Email.ToLower().Contains(freeTextFilter.ToLower()) ||
                        t.GroupName.ToLower().Contains(freeTextFilter.ToLower())
                    )
                    .Take(maxRows.GetValueOrDefault(int.MaxValue));
            }
        }


        public User Get(string userId, bool throwExceptionIfNotFound)
        {
            var res = AppDbContext.Users.Find(userId);

            if (res == null && throwExceptionIfNotFound)
                throw new EntityNotFoundException(CallContext.ResourceUri, typeof(User), userId);

            return res;
        }

        public void Add(User user)
        {
            AppDbContext.Users.Add(user);
        }

        public void Update(User user)
        {
            AppDbContext.Users.Update(user);
        }

        public void Delete(string userId)
        {
            AppDbContext.Users.Remove(Get(userId, true));
        }
    }
}
