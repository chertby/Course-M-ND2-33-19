﻿using Htp.ITnews.Data.Contracts;
using Microsoft.EntityFrameworkCore.Storage;

namespace Htp.ITnews.Data.EntityFramework
{
    public class Transaction : ITransaction
    {
        private readonly IDbContextTransaction transaction;

        public Transaction(IDbContextTransaction transaction)
        {
            this.transaction = transaction;
        }

        public void Commit()
        {
            transaction.Commit();
        }

        public void Rollback()
        {
            transaction.Rollback();
        }

        public void Dispose()
        {
            transaction.Dispose();
        }
    }
}
