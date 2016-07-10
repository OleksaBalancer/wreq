[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(wreq.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(wreq.App_Start.NinjectWebCommon), "Stop")]

namespace wreq.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using wreq.DAL;

    using Moq;
    using Models;
    using System.Collections.Generic;
    using System.Data.Entity;
    using AutoMapper;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using DAL.Abstract;
    using Models.Entities;
    using BL.Abstract;
    using BL;
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {           

            kernel.Bind<IDataService>().To<DataService>().InRequestScope();
            kernel.Bind<ICropStateModeller>().To<CropStateModeller>().InRequestScope();
            kernel.Bind<IParametersCalculator>().To<ParametersCalculator>().InRequestScope();
            kernel.Bind<IWeatherManager>().To<WeatherManager>().InRequestScope();

            kernel.Bind<ApplicationDbContext>().ToSelf().InRequestScope();

            kernel.Bind<IUserStore<ApplicationUser>>().To<UserStore<ApplicationUser>>()
            .InRequestScope()
            .WithConstructorArgument("context", kernel.Get<ApplicationDbContext>());


            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
            IMapper mapper = mapperConfiguration.CreateMapper();
            kernel.Bind<IMapper>().ToConstant(mapper);

        }
    }
}
