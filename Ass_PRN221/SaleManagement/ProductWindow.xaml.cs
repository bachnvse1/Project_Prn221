using SaleManagementLibrary.DataAccess;
using SaleManagementLibrary.Repository;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SaleManagement
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IProductRepository _productRepository;

        public ProductWindow(IMemberRepository memberRepository, IProductRepository productRepository)
        {
            InitializeComponent();
            _productRepository = productRepository;
            _memberRepository = memberRepository;
            LoadProducts();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product product = new Product()
                {
                    ProductName = txtProductName.Text,
                    CategoryId = int.TryParse(txtCategoryId.Text, out var categoryId) ? (int?)categoryId : null,
                    Weight = txtWeight.Text,
                    UnitPrice = decimal.Parse(txtUnitPrice.Text),
                    UnitsInStock = int.TryParse(txtUnitsInStock.Text, out var units) ? (int?)units : null
                };
                _productRepository.Add(product);
                LoadProducts();
                MessageBox.Show($"{product.ProductName} inserted successfully.", "Insert product");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Insert product");
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product product = new Product()
                {
                    ProductId = int.Parse(txtProductId.Text),
                    ProductName = txtProductName.Text,
                    CategoryId = int.TryParse(txtCategoryId.Text, out var categoryId) ? (int?)categoryId : null,
                    Weight = txtWeight.Text,
                    UnitPrice = decimal.Parse(txtUnitPrice.Text),
                    UnitsInStock = int.TryParse(txtUnitsInStock.Text, out var units) ? (int?)units : null
                };
                _productRepository.Update(product);
                LoadProducts();
                MessageBox.Show($"{product.ProductName} updated successfully.", "Update product");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update product");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product product = _productRepository.GetById(int.Parse(txtProductId.Text));
                _productRepository.Delete(product.ProductId);
                LoadProducts();
                MessageBox.Show($"{product.ProductName} deleted successfully.", "Delete product");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete product");
            }
        }

        private void LoadProducts()
        {
            ProductListView.ItemsSource = _productRepository.GetAll();
        }

        private void ClearInputs()
        {
            txtProductId.Text = string.Empty;
            txtCategoryId.Text = string.Empty;
            txtProductName.Text = string.Empty;
            txtWeight.Text = string.Empty;
            txtUnitPrice.Text = string.Empty;
            txtUnitsInStock.Text = string.Empty;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            ClearInputs();
            LoadProducts();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(_memberRepository, _productRepository);
            mainWindow.Show();
            this.Close();
        }
    }
}
