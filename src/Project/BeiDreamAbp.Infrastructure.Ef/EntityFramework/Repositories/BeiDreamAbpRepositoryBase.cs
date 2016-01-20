using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace BeiDreamAbp.Infrastructure.Ef.EntityFramework.Repositories
{
    /// <summary>
    /// 实体和主键都是泛型的仓储基类,继承EfRepositoryBase
    /// </summary>
    /// <typeparam name="TEntity">泛型实体</typeparam>
    /// <typeparam name="TPrimaryKey">泛型主键</typeparam>
    public abstract class BeiDreamAbpRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<BeiDreamAbpDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected BeiDreamAbpRepositoryBase(IDbContextProvider<BeiDreamAbpDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }
    }
    /// <summary>
    /// 实体是泛型，主键是int的仓储基类,继承BeiDreamAbpRepositoryBase
    /// </summary>
    /// <typeparam name="TEntity">泛型实体</typeparam>
    public abstract class BeiDreamAbpRepositoryBase<TEntity> : EfRepositoryBase<BeiDreamAbpDbContext, TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected BeiDreamAbpRepositoryBase(IDbContextProvider<BeiDreamAbpDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }
    }
}