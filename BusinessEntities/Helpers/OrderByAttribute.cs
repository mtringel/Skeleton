using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopTal.JoggingApp.BusinessEntities.Helpers
{
    /// <summary>
    /// Used by DataAccess components to specify ordering fields for calculated fields.
    /// For example: FullName -> ["FirstName", "LastName"]
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class OrderByAttribute : Attribute
    {
        public string[] OrderByFields { get; private set; }

        public OrderByAttribute(params string[] orderByFields)
        {
            this.OrderByFields = orderByFields;
        }
    }
}
