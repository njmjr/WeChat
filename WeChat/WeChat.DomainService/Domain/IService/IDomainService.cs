using System.Collections.Generic;

namespace WeChat.DomainService.Domain.IService
{
    public interface IDomainService :ITransientDependency
    {
       List<ITransientDependency> TransientDependencies { get; set; }
    }
}
