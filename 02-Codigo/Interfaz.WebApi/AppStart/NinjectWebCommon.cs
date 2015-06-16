[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.AppStart.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.AppStart.NinjectWebCommon), "Stop")]

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.AppStart
{
    using System;
    using System.Web;

    using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Fachada;
    using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Repositorios;
    using Nubise.Hc.Util.I18n.Babel.Repositorios.ImplementacionXml.Implementacion;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Controladores;
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;
    using Nubise.Hc.Util.I18n.Babel.Repositorios.ImplementacionXml.Persistencia;
    using System.Xml.Serialization;
    using Nubise.Hc.Util.I18n.Babel.Repositorios.ImplementacionXml.Modelo;
    using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Fachada.Implementacion;

    [ExcludeFromCodeCoverage]
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
            var serializadorXml=new XmlSerializer((typeof(Diccionarios)));
            var persistenciaXml = new PersistenciaArchivo();
            var diccionarioPath = HttpRuntime.AppDomainAppPath + ConfigurationManager.AppSettings["archivoDiccionario"];
            kernel.Bind<IDiccionarioRepositorio>().To<DiccionarioRepositorioXmlImpl>().WithConstructorArgument("directorio",diccionarioPath).WithConstructorArgument("persistencia",persistenciaXml).WithConstructorArgument("serializador",serializadorXml);
            kernel.Bind<IAdministradorDeDiccionarios>().To<AdministradorDeDiccionarios>();
            kernel.Bind<IAdministradorDeEtiquetas>().To<AdministradorDeEtiquetas>();
            kernel.Bind<IAdministradorDeTraducciones>().To<AdministradorDeTraducciones>();


        }        
    }
}
