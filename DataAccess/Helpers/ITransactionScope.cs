﻿using System;

namespace TopTal.JoggingApp.DataAccess.Helpers
{
    /// <summary>
    /// EF Core does not support ambient transactions (System.Transactions.TransactionScope), but each SaveChanges() call runs in a transaction.
    /// </summary>
    public interface ITransactionScope : IDisposable
    {
        void Complete();
    }
}
