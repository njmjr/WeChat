using System.Collections.Generic;
using System.Data;
using Dapper.Contrib.Extensions;
using ServiceStack.Data;
using WeChat.DomainService.Repository.IRepositories;
using WeChat.Models.Base;
using WeChat.Utility;
using WeChat.Utility.Extensions;

namespace WeChat.DomainService.Repository.Repositories
{
    public class Repository
    {
        private DbStates _dbStates = DbStates.Temp;
        private IDbConnection _db;
        protected IDbTransaction Tx;
        protected IDbConnection Connection
        {
            get { return _db ?? (_db = DbFactory.OpenDbConnection()); }
        }

        public IDbConnectionFactory DbFactory { get; set; }
        public void SetDb(IDbConnection db, IDbTransaction tx)
        {
            if (_dbStates == DbStates.Temp && _db != null)
            {
                _db.Dispose();
            }
            _dbStates = DbStates.Active;
            _db = db;
            Tx = tx;
        }
        public IDbTransaction CreateTx(ref IDbConnection db)
        {
            if (_dbStates == DbStates.Temp && _db != null)
            {
                _db.Dispose();
            }
            _dbStates = DbStates.Owner;
            _db = DbFactory.OpenDbConnection();
            db = _db;
            return _db.BeginTransaction();
        }
        public void Dispose()
        {
            if ((_dbStates != DbStates.Active) && _db != null)
            {
                _db.Dispose();
            }
        }
    }
    public class Repository<TEntity, TPrimaryKey> : Repository, IRepository<TEntity, TPrimaryKey> where TEntity : class, IEntity
    {
        public string TableName { get; set; }
        private static string TableCode
        {
            get { return typeof(TEntity).Name.ToUpper(); }
        }

        private string GetTableName()
        {
            if (TableName.IsNullOrEmpty())
            {
                TableName = TableCode;
            }
            return TableName;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return Connection.GetAll<TEntity>(transaction: Tx);
        }
        public virtual TEntity Get(TPrimaryKey id, bool isTrimKey = false)
        {
            var result = Connection.Get<TEntity>(id, transaction: Tx, isTrimKey: isTrimKey);
            if (result == null)
            {
                throw new WeChatException("GET_{0}_ERROR".FormatWith(TableCode), "查询{0}失败".FormatWith(GetTableName()));
            }
            return result;
        }

        public virtual TEntity Get(TPrimaryKey id, WeChatException oscException, bool isTrimKey = false)
        {
            var result = Connection.Get<TEntity>(id, transaction: Tx, isTrimKey: isTrimKey);
            if (result == null)
            {
                throw oscException;
            }
            return result;
        }

        public TEntity GetDefault(TPrimaryKey id, bool isTrimKey = false)
        {
            var result = Connection.Get<TEntity>(id, transaction: Tx, isTrimKey: isTrimKey);
            return result;
        }

        public IEnumerable<TEntity> Find(dynamic param)
        {
            var where = DynamicQuery.GetWhereQuery(param);
            return Find(where, (object)param);
        }

        private IEnumerable<TEntity> Find(string where, object param)
        {
            return Connection.Find<TEntity>(where, param, transaction: Tx);
        }

        public void Insert(TEntity entity)
        {
            var result = Connection.Insert(entity, commandTimeout: 90, transaction: Tx);
            if (result == 0)
            {
                throw new WeChatException("INSERT_{0}_ERROR".FormatWith(TableCode), "插入{0}失败".FormatWith(GetTableName()));
            }
        }

        public void Update(TEntity entity)
        {
            var result = Connection.Update(entity, commandTimeout: 10, transaction: Tx);
            if (!result)
            {
                throw new WeChatException("UPDATE_{0}_ERROR".FormatWith(TableCode), "更新{0}失败".FormatWith(GetTableName()));
            }
        }

        public void Delete(TEntity entity)
        {
            var result = Connection.Delete(entity, commandTimeout: 90, transaction: Tx);
            if (!result)
            {
                throw new WeChatException("DELETE_{0}_ERROR".FormatWith(TableCode), "删除{0}失败".FormatWith(GetTableName()));
            }
        }


        public void Delete(TPrimaryKey key, bool isTrimKey = false)
        {
            var result = Connection.Delete<TEntity>(key, commandTimeout: 90, transaction: Tx, isTrimKey: isTrimKey);
            if (!result)
            {
                throw new WeChatException("DELETE_{0}_ERROR".FormatWith(TableCode), "删除{0}失败".FormatWith(GetTableName()));
            }
        }

    }

    public enum DbStates
    {
        Temp, //临时数据连接
        Active,//从属数据连接
        Owner //事务数据连接
    }
}
