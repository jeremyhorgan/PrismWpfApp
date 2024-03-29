﻿using System.Windows;
using Prism.Ioc;
using Prism.Logging;
using Prism.Unity;
using PrismWpfApp.Infrastructure;
using PrismWpfApp.Model;
using PrismWpfApp.Repository;
using PrismWpfApp.Views;

namespace PrismWpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IExcelRepository, ExcelRepository>();
            containerRegistry.Register<ILoggerFacade, ApplicationLogger>();
            containerRegistry.Register<IOpenFileDialogService, OpenFileDialogService>();
            containerRegistry.Register<ApplicationState>();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindowView>();
        }
    }
}
