using System;
using Castle.Core.Internal;
using Castle.DynamicProxy;
using WeChat.DomainService.Application.IService;
using WeChat.Utility;

namespace WeChat.DomainService.Interceptors
{
    public class TransactionInterceptor : Attribute,IInterceptor
    {  
        public void Intercept(IInvocation invocation)
        {
            var attr = invocation.MethodInvocationTarget.GetAttribute<TransactionAttribute>();
            if (attr == null)
            {
                invocation.Proceed();
                return;
            }
            var appService = invocation.InvocationTarget as IApplicationService;
            if (appService != null)
                using (var trans = appService.OpenTx())
                {  
                    try
                    {
                        invocation.Proceed();
                        trans.Commit();
                    }
                    catch (WeChatException)
                    { 
                        trans.Rollback();
                        throw;
                    }
                    catch (Exception)
                    { 
                        trans.Rollback();
                        throw;
                    } 
                }
        }
    }
}

