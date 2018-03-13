using System.Linq;
using System;
using System.Reflection;
using TopTal.JoggingApp.BusinessEntities.Helpers;
using TopTal.JoggingApp.CallContext;

namespace TopTal.JoggingApp.DataAccess.Helpers
{
    /// <summary>
    /// Lifetime: Transient
    /// Do not add public instance methods here.
    /// </summary>
    public abstract class DataProviderBase
    {
        public DataProviderBase(
            ICallContext callContext,
            AppDbContext appDbContext
            )
        {
            this.AppDbContext = appDbContext;
        }

        #region Services

        protected AppDbContext AppDbContext { get; private set; }

        protected ICallContext CallContext { get; private set; }

        #endregion

        #region Meta-data in attributes

        protected string[] GetOrderByFields(Type entityType, string propertyName)
        {
            var property = entityType.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);

            if (property == null)
                return new[] { propertyName };

            var attribute = property.GetCustomAttribute<OrderByAttribute>();

            if (attribute == null)
                return new[] { propertyName };

            return attribute.OrderByFields;
        }

        #endregion

    }
}
