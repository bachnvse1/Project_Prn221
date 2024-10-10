using Microsoft.Extensions.Configuration;
using SaleManagementLibrary.Repository;
using System.Text;
using System.Windows;


namespace SaleManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WindowSaleManagement : Window
    {
        private readonly IConfiguration _configuration;
        private readonly IMemberRepository _memberRepository;
        private readonly IProductRepository _productRepository;
        public WindowSaleManagement(IConfiguration configuration, IMemberRepository memberRepository, IProductRepository productRepository)
        {
            InitializeComponent();
            _configuration = configuration;
            _productRepository = productRepository;
            _memberRepository = memberRepository;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string email = txtUsername.Text;
            string password = txtPassword.Password;

            var adminEmail = _configuration["AdminAccount:Email"];
            var adminPassword = _configuration["AdminAccount:Password"];


            var userMember = _memberRepository.getUser(email, password);

            if ((email == adminEmail && password == adminPassword) ||
                (userMember != null && password == userMember.Password))
            {
                MainWindow mainWindow = new MainWindow(_memberRepository, _productRepository);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                lblError.Text = "Invalid username or password!";
            }
        }
    }
}