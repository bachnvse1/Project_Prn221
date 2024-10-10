using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SaleManagementLibrary.DataAccess;
using SaleManagementLibrary.Repository;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;

namespace SaleManagement
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;
        public IConfiguration Configuration { get; private set; }

        public App()
        {
            // Cấu hình để đọc tệp appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();

            // Cấu hình DI
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContext<DBContext>();

            services.AddSingleton<IMemberRepository, MemberRepository>();
            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddSingleton<IOrderRepository, OrderRepository>();
            services.AddSingleton<IOrderDetailRepository, OrderDetailRepository>();
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddSingleton<WindowSaleManagement>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var windowCarManagement = serviceProvider.GetService<WindowSaleManagement>();
            if (windowCarManagement != null)
            {
                windowCarManagement.Show();
            }
            else
            {
                MessageBox.Show("Failed to resolve the main window.");
            }
        }
    }

}
