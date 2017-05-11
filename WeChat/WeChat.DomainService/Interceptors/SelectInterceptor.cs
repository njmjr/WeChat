using System;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;

namespace WeChat.DomainService.Interceptors
{
    public class SelectInterceptor : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var attributes = type.GetMethod(method.Name).GetCustomAttributes(false);
            return attributes.OfType<TransactionInterceptor>().Any() ? interceptors.Where(i => i is TransactionInterceptor).ToArray() : interceptors.Where(i => i is ExceptionInterceptor).ToArray();
        }
    }
}
