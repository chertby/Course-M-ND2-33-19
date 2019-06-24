using System;
using System.Linq;
using Htp.ITnews.Data.Contracts.Extensions;
using Microsoft.EntityFrameworkCore.Query;

namespace Htp.ITnews.Data.EntityFramework.Extensions
{
    internal class Includable<TEntity> : IIncludable<TEntity> where TEntity : class
    {
        internal IQueryable<TEntity> Input { get; }

        internal Includable(IQueryable<TEntity> queryable)
        {
            Input = queryable ?? throw new ArgumentNullException(nameof(queryable));
        }
    }

    internal class Includable<TEntity, TProperty> :
        Includable<TEntity>, IIncludable<TEntity, TProperty>
        where TEntity : class
    {
        internal IIncludableQueryable<TEntity, TProperty> IncludableInput { get; }

        internal Includable(IIncludableQueryable<TEntity, TProperty> queryable) :
            base(queryable)
        {
            IncludableInput = queryable;
        }
    }
}
