using Autofac;
using ModelsGenerator.Helpers;
using ModelsGenerator.Properties;

#pragma warning disable 4014

namespace ModelsGenerator.ViewModels
{
    public class ViewModelLocator
    {
        /// <summary>
        /// This container will be used internaly to resolve registered intances
        /// </summary>
        private readonly static IContainer Container;

        /// <summary>
        /// Register all required types in this static constructor,
        /// This will be executed the first time ViewModelLocator is accessed in code
        /// </summary>
        static ViewModelLocator ()
        {
            var builder = new ContainerBuilder();

            #region DataServices and Helpers

            builder.RegisterType<ViewModelLocator>().SingleInstance();
            builder.RegisterType<ParametersCodeGenerator>();
            builder.RegisterType<RequestCodeGenerator>();
            

            #endregion

            #region ViewModels registeration
            builder.RegisterType<RequestGeneratorViewModel>().SingleInstance();

            #endregion

            Container = builder.Build();
        }

        #region data Services and Helpers Properties



        public static Resources Resources
        {
            get { return Container.Resolve<Resources>(); }
        }

        #endregion

        #region View Models Properties
        public static RequestGeneratorViewModel RequestGeneratorViewModel
        {
            get { return Container.Resolve<RequestGeneratorViewModel>(); }
        }

        #endregion

        /// <summary>
        ///Create instance in App.xaml resources so you can use it in data binding in all xaml pages
        ///And don't forget to set this instance from App.xaml.cs, So you can access it from code behind in pages(if required)
        /// </summary>
        public static ViewModelLocator Locator { set; get; }

    }
}
