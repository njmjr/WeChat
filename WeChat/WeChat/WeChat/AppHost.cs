using System;
using Funq;
using ServiceStack;
using ServiceStack.Logging;
using ServiceStack.Logging.NLogger;
using ServiceStack.Validation;
using WeChat.Ioc;
using WeChat.Plugins;
using WeChat.ServiceInterface;

namespace WeChat
{
    //VS.NET Template Info: https://servicestack.net/vs-templates/EmptyAspNet
    public class AppHost : AppHostBase
    {
        private static ILog _log;
        /// <summary>
        /// Base constructor requires a Name and Assembly where web service implementation is located
        /// </summary>
        public AppHost()
            : base("WeChat", typeof (WxServices).Assembly)
        {
            LogManager.LogFactory = new NLogFactory();
            _log = LogManager.GetLogger(typeof(AppHost));
        }

        /// <summary>
        /// Application specific configuration
        /// This method should initialize any IoC resources utilized by your web service classes.
        /// </summary>
        public override void Configure(Container container)
        {
            container.Adapter = new WindsorContainerAdapter();
#if DEBUG

            base.SetConfig(new HostConfig
            {
                DebugMode = true,
            });
#endif
            //Config examples
            Plugins.Add(new PostmanFeature());
            Plugins.Add(new CorsFeature());
            Plugins.Add(new RequestLogsFeature
            {
                EnableSessionTracking = false,
                EnableRequestBodyTracking = true,
                EnableResponseTracking = true,
                EnableErrorTracking = true,
                Capacity = 1000,
                RequestLogger = new FileRequestLogger()
            });
            Plugins.Add(new ValidationFeature());
        }
    }
}