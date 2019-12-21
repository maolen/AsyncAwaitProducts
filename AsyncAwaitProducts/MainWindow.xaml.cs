using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AsyncAwaitProducts
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Product> Products { get; set; }
        private ObservableCollection<User> Users { get; set; }
       

        public MainWindow()
        {
            InitializeComponent();
            FillAsync();
        }

        private async void FillAsync()
        {
            Products = new ObservableCollection<Product>(await GetProductsAsync());
            Users = new ObservableCollection<User>(await GetUsersAsync());
            dataGrid.ItemsSource = Products;
        }

        private async Task<List<Product>> GetProductsAsync()
        {
            using (var context = new Context())
            {
                return await context.Products.ToListAsync();
            }
        }
        private async Task<List<User>> GetUsersAsync()
        {
            using (var context = new Context())
            {
                return await context.Users.ToListAsync();
            }
        }

        //private void OnRowDeleted(object sender, KeyEventArgs e)
        //{
        //    var currentRow = (DataGridRow)dataGrid
        //        .ItemContainerGenerator
        //        .ContainerFromIndex(dataGrid.SelectedIndex);
        //    var product = Products[dataGrid.SelectedIndex];
        //    if (e.Key == Key.Delete && !currentRow.IsEditing)
        //    {
        //        DeleteRowAsync(product);
        //        Products.Remove(product);
        //    }
        //    else if (e.Key == Key.Enter && product != null)
        //    {
        //        AddRowAsync(product);
        //    }
        //}

        private async static void AddRowAsync(Product product)
        {
            using (var context = new Context())
            {
                context.Products.Add(product);
                await context.SaveChangesAsync();
            }
        }

        private async void DeleteRowAsync(Product product)
        {
            using (var context = new Context())
            {
                context.Products.Remove(product);
                await context.SaveChangesAsync();
            }           
        }

        private void DeleteRow(object sender, RoutedEventArgs e)
        {
            var currentRow = (DataGridRow)dataGrid
                .ItemContainerGenerator
                .ContainerFromIndex(dataGrid.SelectedIndex);
            var product = Products[dataGrid.SelectedIndex];
            if (!currentRow.IsEditing)
            {
                DeleteRowAsync(product);
                Products.Remove(product);
            }
        }

        private void InsertRow(object sender, RoutedEventArgs e)
        {
            var currentRow = (DataGridRow)dataGrid
                .ItemContainerGenerator
                .ContainerFromIndex(dataGrid.SelectedIndex);
            var product = Products[dataGrid.SelectedIndex];
            if (!currentRow.IsEditing)
            {
                AddRowAsync(product);
                Products.Add(product);
            }
        }

        private void UpdateRow(object sender, RoutedEventArgs e)
        {
            var currentRow = (DataGridRow)dataGrid
                .ItemContainerGenerator
                .ContainerFromIndex(dataGrid.SelectedIndex);
            var product = Products[dataGrid.SelectedIndex];
            if (!currentRow.IsEditing)
            {
                UpdateRowAsync(product);
            }
        }

        private async void UpdateRowAsync(Product product)
        {
            using (var context = new Context())
            {
                var oldProduct = context.Products.SingleOrDefault(x => x.Id == product.Id); 
                if (oldProduct != null)
                {
                    oldProduct = product;
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
