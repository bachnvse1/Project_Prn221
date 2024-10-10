using SaleManagementLibrary.DataAccess;
using SaleManagementLibrary.Repository;
using System.Windows;


namespace SaleManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IProductRepository _productRepository;
        public MainWindow(IMemberRepository memberRepository, IProductRepository productRepository)
        {
            InitializeComponent();
            _memberRepository = memberRepository;
            _productRepository = productRepository;
            loadMember();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Member member = new Member()
                {
                    Email = txtEmail.Text,
                    CompanyName = txtCompanyName.Text,
                    City = txtCity.Text,
                    Country = txtCountry.Text,
                    Password = txtPassword.Text,
                };
                _memberRepository.Add(member);
                loadMember();
                MessageBox.Show($"{member.Email} inserted successfully.", "Insert member");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Insert member");
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Member member = new Member()
                {
                    MemberId = int.Parse(txtMemberID.Text),
                    Email = txtEmail.Text,
                    CompanyName = txtCompanyName.Text,
                    City = txtCity.Text,
                    Country = txtCountry.Text,
                    Password = txtPassword.Text,
                };
                _memberRepository.Update(member);
                loadMember();
                MessageBox.Show($"{member.Email} updated successfully.", "Update member");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update member");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                    Member member = _memberRepository.GetById(int.Parse(txtMemberID.Text));
                    _memberRepository.Delete(member.MemberId);
                    loadMember();
                    MessageBox.Show($"{member.Email} deleted successfully.", "Delete member");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete member");
            }
        }

        

        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            viewPopup.IsOpen = !viewPopup.IsOpen;
        }

        private void ViewOrder_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow orderWindow = new OrderWindow();
            orderWindow.Show();
            this.Close();
        }

        private void ViewProduct_CLick(object sender, RoutedEventArgs e)
        {
            ProductWindow productWindow = new ProductWindow(_memberRepository, _productRepository);
            productWindow.Show();
            this.Close();
        }
        
        public void loadMember()
        {
            MemberListView.ItemsSource = _memberRepository.GetAll();
        }

        private void ClearInputs()
        {
            txtMemberID.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtCompanyName.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtCountry.Text = string.Empty;
            txtPassword.Text = string.Empty;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            ClearInputs();
            loadMember();
        }

        /*private void MemberListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MemberListView.SelectedItem is Member selectedMember)
            {
                txtMemberID.Text = selectedMember.MemberId.ToString();
                txtEmail.Text = selectedMember.Email;
                txtCompanyName.Text = selectedMember.CompanyName;
                txtCity.Text = selectedMember.City;
                txtCountry.Text = selectedMember.Country;
                txtPassword.Text = selectedMember.Password;
            }
            else
            {
                ClearInputs();
            }
        }*/

        /*MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this member?",
                                          "Confirm Delete",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Warning);
            
            try
            {
                if (result == MessageBoxResult.Yes)
                {
                    Member member = _memberRepository.GetById(int.Parse(txtMemberID.Text));
        _memberRepository.Delete(member.MemberId);
                    loadMember();
        MessageBox.Show($"{member.Email} deleted successfully.", "Delete member");
                }
}
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete member");
            }*/
    }
}
