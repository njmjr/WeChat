using System.Collections.Generic;
using System.Data;
using ServiceStack;

namespace WeChat.DomainService.Domain.Service
{
    public  abstract  class DomainService 
    {
        protected DomainService()
        {
            TransientDependencies = new List<ITransientDependency>();
        }
        public List<ITransientDependency> TransientDependencies { get; set; }

        public void SetDb(IDbConnection db, IDbTransaction tx)
        {
            foreach (var repository in TransientDependencies)
            {
                repository.SetDb(db,tx);
            }
        }

        public IDbTransaction CreateTx(ref IDbConnection db)
        {
            IDbTransaction tx = null;
            for (int i = 0; i < TransientDependencies.Count; i++)
            {
                if (i == 0)
                {
                    tx = TransientDependencies[i].CreateTx(ref db);
                }
                else
                {
                    TransientDependencies[i].SetDb(db, tx);
                }
            }
            return tx;
        }

        public virtual void Dispose()
        { 
            foreach (var repository in TransientDependencies)
            {
                repository.Dispose();
            }
        } 

        protected void AddMessage(ResponseStatus responseStateus, string msg)
        {
            if (string.IsNullOrEmpty(responseStateus.Message))
            {
                responseStateus.Message = msg;
            }
            else
            {
                responseStateus.Message = responseStateus.Message + '|' + msg;
            }
        }
    }
}
