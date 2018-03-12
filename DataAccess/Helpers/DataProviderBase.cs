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

        protected string[] GetOrderByFields(Type entityType, string propertyOrFieldName)
        {
            var member = entityType.GetMember(
                propertyOrFieldName,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy
                )
                .FirstOrDefault();

            OrderByAttribute attribute;

            if (member is PropertyInfo)
                attribute = (member as PropertyInfo).GetCustomAttribute<OrderByAttribute>();
            else if (member is FieldInfo)
                attribute = (member as FieldInfo).GetCustomAttribute<OrderByAttribute>();
            else
                return new[] { propertyOrFieldName };

            return attribute == null ? new[] { propertyOrFieldName } : attribute.OrderByFields;
        }

        #endregion

    }
}
