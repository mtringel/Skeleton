namespace TopTal.JoggingApp.DataAccess.Helpers
{
    /// <summary>
    /// EF Core does not support ambient transactions (System.Transactions.TransactionScope), but each SaveChanges() call runs in a transaction.
    /// </summary>
    public class TransactionScope : ITransactionScope
    {
        private AppDbContext AppDbContext;

        internal TransactionScope(AppDbContext appDbContext)
        {
            this.AppDbContext = appDbContext;
        }

        public void Complete()
        {
            AppDbContext.SaveChanges();
            AppDbContext.Dispose();
            AppDbContext = null;
        }

        public void Dispose()
        {
            if (AppDbContext != null)
            {
                AppDbContext.Dispose();
                AppDbContext = null;
            }
        }
    }
}
