using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Windows;
using Caliburn.Micro;

namespace QuickStack
{
    internal class MefBootstrapper : BootstrapperBase
    {
        private CompositionContainer _container;

        public MefBootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            var catalog = // Caliburn.Micro looks for views in AssemblySource.Instance
                new AggregateCatalog(AssemblySource.Instance.Select(a => new AssemblyCatalog(a)).Where(a => a != null));
            // Changed .OfType<ComposablePartCatalog>() to .Where(a => a != null)

            _container = new CompositionContainer(catalog);

            var batch = new CompositionBatch();

            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue(_container);

            _container.Compose(batch);
        }

        protected override object GetInstance(Type service, string key)
        {
            var contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(service) : key;
            var exports = _container.GetExportedValues<object>(contract);

            // ReSharper disable PossibleMultipleEnumeration
            if (exports.Any()) return exports.First();
            // ReSharper restore PossibleMultipleEnumeration

            throw new Exception($"Could not locate any instance of contract {contract}.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetExportedValues<object>(AttributedModelServices.GetContractName(service));
        }

        protected override void BuildUp(object instance)
        {
            _container.SatisfyImportsOnce(instance);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<IShell>();
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[]
            {
                Assembly.GetExecutingAssembly()
            };
        }
    }
}