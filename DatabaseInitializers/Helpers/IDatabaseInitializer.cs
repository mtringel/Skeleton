
namespace TopTal.JoggingApp.DatabaseInitializers.Helpers
{
    public interface IDatabaseInitializer
    {
        void InitializeDatabase();

        void AfterDatabaseInitialized();
    }
}