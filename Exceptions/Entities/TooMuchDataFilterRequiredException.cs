using System;


namespace TopTal.JoggingApp.Exceptions.Entities
{
    public class TooMuchDataFilterRequiredException : EntityException
    {
        public Type EntityType { get; private set; }

        public TooMuchDataFilterRequiredException(string resourceName, Type entityType, string requiredFilter)
            : base(
                  System.Net.HttpStatusCode.BadRequest,
                  string.Format(
                      Resources.Resources.Entity_TooMuchDataFilterRequited ,
                      resourceName,
                      entityType.Name,
                      requiredFilter
                      )
                  )
        {
            this.EntityType = entityType;
        }


    }
}
