using System;
using System.Collections.Generic;
using System.Linq;

namespace TopTal.JoggingApp.Exceptions.Entities
{
    public class EntityNotFoundException : EntityException
    {
        public Type EntityType { get; private set; }

        public object[] Keys { get; private set; }

        public EntityNotFoundException(string resourceName, Type entityType, params object[] keys)
            : base(
                  System.Net.HttpStatusCode.BadRequest,
                  string.Format(
                      Resources.Resources.Entity_EntityNotFound  ,
                      resourceName,
                      entityType.Name,
                      keys == null ? string.Empty : string.Join(",", keys)
                      )
                  )
        {
            this.EntityType = entityType;
            this.Keys = (keys?.ToArray());
        }


    }
}
