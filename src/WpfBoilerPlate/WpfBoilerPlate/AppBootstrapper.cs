namespace WpfBoilerPlate {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Caliburn.Micro;
    using WpfBoilerPlate.ViewModels;

    public class AppBootstrapper : BootstrapperBase {
        SimpleContainer container;

        public AppBootstrapper() {
            Initialize();
        }

        protected override void Configure() {
            container = new SimpleContainer();
            container.Instance(container);
            container.Singleton<IWindowManager, WindowManager>();
            container.Singleton<IEventAggregator, EventAggregator>();
            container.Singleton<HomeViewModel, HomeViewModel>();
            GetType().Assembly.GetTypes()
               .Where(type => type.IsClass)
               .Where(type => type.Name.EndsWith("ViewModel"))
               .ToList()
               .ForEach(viewModelType => container.RegisterPerRequest(
                   viewModelType, viewModelType.ToString(), viewModelType));

        }

        protected override object GetInstance(Type service, string key) {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service) {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance) {
            container.BuildUp(instance);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e) {
            base.OnStartup(sender, e);
            DisplayRootViewFor<HomeViewModel>();

        }
    }
}