using TopTal.JoggingApp.Configuration;
using TopTal.JoggingApp.BusinessEntities.Users;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TopTal.JoggingApp.DataAccess.Helpers;

namespace TopTal.JoggingApp.DataAccess
{
    /// <summary>
    /// Lifetime: Scoped (current request)
    /// </summary>        
    public sealed class AppDbContext : DbContext
    {
        #region Services

        private AppConfig AppConfig;

        #endregion

        #region Initialization

        /// <summary>
        /// If you wish to target a different database and/or database provider, modify the 'AppDb' 
        /// connection string in the Web.config configuration file (Web project root).
        /// </summary>
        public AppDbContext(AppConfig appConfig)
            : base(GetContextOptions(appConfig))
        {
            this.AppConfig = appConfig;
        }

        private static DbContextOptions GetContextOptions(AppConfig appConfig)
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlServer(appConfig.ConnectionStrings.AppDb);

            return optionsBuilder.Options;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(AppConfig.ConnectionStrings.AppDb);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // EF6
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Properties<decimal>().Configure(c => c.HasPrecision(AppConfig.Current.Database.DecimalPrecision, AppConfig.Current.Database.DecimalScale));

            // EF7
            var decimalType = string.Format("DECIMAL({0},{1})", AppConfig.AppDb.DecimalPrecision, AppConfig.AppDb.DecimalScale);

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal)))
            {
                property.Relational().ColumnType = decimalType;
            }

            base.OnModelCreating(modelBuilder);

        }

        #endregion

        #region Entities

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        /// <summary>
        /// Unfortunaltelly, this must be public, cannot be internal
        /// Don't use it in BusinessLogic, use the dedicated DataProvider.
        /// </summary>
        public DbSet<User> Users { get; set; }

        #endregion

        #region ITransactionScope

        public ITransactionScope BeginTransaction()
        {
            return new TransactionScope(this);
        }

        #endregion
    }
}