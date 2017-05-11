using System;
using Castle.Windsor;
using ServiceStack.Configuration;

namespace WeChat.Ioc
{
    public class WindsorContainerAdapter : IContainerAdapter, IRelease
    {
        private readonly IWindsorContainer _container;

        public WindsorContainerAdapter()
        {
            _container = new WindsorContainer().Install(new WindsorInstaller());
             
        }

        public T TryResolve<T>()
        {

            if (_container.Kernel.HasComponent(typeof(T)))
            {
                return (T)_container.Resolve(typeof(T));
            }

            return default(T);
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
         
        public void Release(object obj)
        {
            var disposable = obj as IDisposable;
            if(disposable != null)
                disposable.Dispose();
            _container.Release(obj);
        }
         
        public void Dispose()
        {
            _container.Dispose();
        }
    }
}