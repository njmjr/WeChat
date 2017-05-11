using System;
using System.Collections.Generic;
using System.Data;

namespace WeChat.DomainService.Application.IService
{
    public interface IApplicationService : IDisposable
    {
        IDbTransaction OpenTx();
        List<ITransientDependency> TransientDependencies { get; set; }
    }
}
