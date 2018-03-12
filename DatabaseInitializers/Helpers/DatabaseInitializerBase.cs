using TopTal.JoggingApp.Configuration;
using TopTal.JoggingApp.DataAccess;

namespace TopTal.JoggingApp.DatabaseInitializers.Helpers
{
    public abstract class DatabaseInitializerBase : IDatabaseInitializer
    {
        #region Services

        protected AppDbContext AppDbContext { get; private set; }

        protected AppConfig AppConfig { get; private set; }

        #endregion

        internal DatabaseInitializerBase(
            AppDbContext appDbContext,
            AppConfig appConfig)
        {
            this.AppDbContext = appDbContext;
            this.AppConfig = appConfig;
        }

        #region IDatabaseInitializer

        public abstract void AfterDatabaseInitialized();

        public abstract void InitializeDatabase();

        #endregion        
    }
}
