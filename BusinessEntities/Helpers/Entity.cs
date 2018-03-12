namespace TopTal.JoggingApp.BusinessEntities.Helpers
{
    /// <summary>
    /// Base class for entities
    /// </summary>
    public abstract class Entity : IDataObject
    {
        public abstract object[] Keys();
    }
}