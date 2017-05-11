using System.Collections.Generic;
using WeChat.Models.Base;
using WeChat.Utility;

namespace WeChat.DomainService.Repository.IRepositories
{
    public interface IRepository :  ITransientDependency
    { 

    }

    public interface IRepository<TEntity, TPrimaryKey> : IRepository where TEntity : class, IEntity
    {  
        string TableName { get; set; }
        IEnumerable<TEntity> GetAll();
        TEntity Get(TPrimaryKey id, bool isTrimKey = false);
        TEntity Get(TPrimaryKey id, WeChatException weChatException, bool isTrimKey = false);
        TEntity GetDefault(TPrimaryKey id, bool isTrimKey = false);
        IEnumerable<TEntity> Find(dynamic param);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity); 
        void Delete(TPrimaryKey primaryKey, bool isTrimKey = false);

    }
}
