using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using Castle.DynamicProxy.Internal;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Oracle;
using WeChat.DomainService.Application.IService;
using WeChat.DomainService.Domain.IService;
using WeChat.DomainService.Interceptors;
using WeChat.DomainService.Repository.IRepositories;
using WeChat.Utility.Secutiry;


namespace WeChat.Ioc
{

    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {

            //注册应用服务
            var assembly = System.Reflection.Assembly.GetAssembly(typeof(IApplicationService));
            container.Register(
                Classes.FromAssembly(assembly)
                .IncludeNonPublicTypes()
                .BasedOn<IApplicationService>()
                .WithService.DefaultInterfaces()
                .LifestyleTransient()
                .Configure(c => c.Interceptors<ExceptionInterceptor, TransactionInterceptor>().Named(c.Implementation.FullName)),
                Classes.FromAssembly(assembly)
                .IncludeNonPublicTypes()
                .BasedOn<IApplicationService>()
                .WithService.Select((type, @base) => type.GetAllInterfaces().Where(i => type.Name.Contains(GetInterfaceNameFromConf(i))))
                .LifestyleTransient()
                .Configure(c => c.Interceptors<ExceptionInterceptor, TransactionInterceptor>().IsDefault().Named(c.Implementation.FullName +
                                                                                                     "_" + ConfigurationManager.AppSettings["City"])));


            container.Register(Classes.FromAssembly(assembly)
                                    .IncludeNonPublicTypes()
                                    .BasedOn<IDomainService>()
                                    .WithService.DefaultInterfaces()
                                    .LifestyleTransient(),
                                    Classes.FromAssembly(assembly)
                                    .IncludeNonPublicTypes()
                                    .BasedOn<IDomainService>()
                                    .WithService.Select((type, @base) => type.GetAllInterfaces().Where(i => type.Name.Contains(GetInterfaceNameFromConf(i))))
                                    .LifestyleTransient()
                                    .Configure(c => c.IsDefault().Named(c.Implementation.FullName + "_" + ConfigurationManager.AppSettings["City"]))
                                    );

            container.Register(Classes.FromAssembly(assembly)
                                  .IncludeNonPublicTypes()
                                  .BasedOn<IRepository>()
                                  .WithService.DefaultInterfaces()
                                  .LifestyleTransient().Configure(c => c.Named(c.Implementation.FullName)),
                                  Classes.FromAssembly(assembly)
                                  .IncludeNonPublicTypes()
                                  .BasedOn<IRepository>()
                                  .WithService.Select((type, @base) => type.GetAllInterfaces().Where(i => type.Name.Contains(GetInterfaceNameFromConf(i))))
                                  .LifestyleTransient()
                                  .Configure(c => c.IsDefault().Named(c.Implementation.FullName + "_" + ConfigurationManager.AppSettings["City"]))
                                  );


            //注册数据库工厂
            container.Register(Component.For<IDbConnectionFactory>().Instance(new OrmLiteConnectionFactory(GetConnStr(), OracleOrmLiteDialectProvider.Instance)));

            container.Register(Component.For<ExceptionInterceptor>().LifestyleTransient());
            container.Register(Component.For<TransactionInterceptor>().LifestyleTransient());


        }
        private string GetInterfaceNameFromConf(Type @interface)
        {
            var name = @interface.Name;
            if ((name.Length > 1 && name[0] == 'I') && char.IsUpper(name[1]))
            {
                return name.Substring(1) + ConfigurationManager.AppSettings["City"];
            }
            return name;
        }

        private static string GetConnStr()
        {
            string connectString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            string[] strs = connectString.Split(';');
            string pwd = string.Empty;
            for (int i = 0; i < strs.Length; i++)
            {
                if (strs[i].ToLower().Contains("password="))
                {
                    pwd = strs[i].Trim().Substring(9);
                    break;
                }
            }

            string strBuilder = AesHelper.Decrypt(pwd);
            connectString = connectString.Replace(pwd, strBuilder.ToString(CultureInfo.InvariantCulture).Trim());
            return connectString;
        }
    }
}