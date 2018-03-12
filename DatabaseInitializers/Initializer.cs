using System;
using TopTal.JoggingApp.Configuration;
using TopTal.JoggingApp.DataAccess;
using TopTal.JoggingApp.DatabaseInitializers.Helpers;

namespace TopTal.JoggingApp.DatabaseInitializers
{
    public sealed class Initializer : DatabaseInitializerBase
    {
        private IDatabaseInitializer[] ObjectInitializers;

        private bool JustCreated;

        public Initializer(AppDbContext appDbContext, AppConfig appConfig)
            : base(appDbContext, appConfig)
        {
            ObjectInitializers = new IDatabaseInitializer[]{
                // Test data
                //new TestData.UserTestData(appDbContext, appConfig)
            };
        }

        public override void InitializeDatabase()
        {
            try
            {
                if (AppDbContext.Database.EnsureCreated())
                {
                    JustCreated = true;

                    foreach (var initializer in ObjectInitializers)
                        initializer.InitializeDatabase();
                }
            }
#if DEBUG
            catch (Exception ex)
            {
                throw ex;
            }
#else
            catch 
            {
                throw;
            }
#endif        
        }

        public override void AfterDatabaseInitialized()
        {
            try
            {
                if (JustCreated)
                {
                    foreach (var initializer in ObjectInitializers)
                        initializer.AfterDatabaseInitialized();
                }
            }
#if DEBUG
            catch (Exception ex)
            {
                throw ex;
            }
#else
            catch 
            {
                throw;
            }
#endif        
        }
    }
}