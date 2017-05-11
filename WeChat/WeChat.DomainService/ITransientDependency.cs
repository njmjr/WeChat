using System;
using System.Data;

namespace WeChat.DomainService
{
    public interface ITransientDependency:IDisposable
    {
        void SetDb(IDbConnection db, IDbTransaction tx);
        IDbTransaction CreateTx(ref IDbConnection db);
    }
}
