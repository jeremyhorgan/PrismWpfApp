using System.Windows;
using Prism.Ioc;
using Prism.Unity;

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
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
    }
}
