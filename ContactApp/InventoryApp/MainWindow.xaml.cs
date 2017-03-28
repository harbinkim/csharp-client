using InventoryApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace InventoryApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            LoadProducts();
            AddHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(ListView_OnColumnClick));
            view = (CollectionView)CollectionViewSource.GetDefaultView(uxProductList.ItemsSource);
        }


        private void LoadProducts()
        {
            var products = App.InventoryRepository.GetAll();
            uxProductList.ItemsSource = products.Select(p => ProductModel.ToModel(p)).ToList();
        }

        private void uxFileNew_Click(object sender, RoutedEventArgs e)
        {
            var window = new ProductWindow();

            if (window.ShowDialog() == true)
            {
                var uiProductModel = window.Product;
                var repositoryProductModel = uiProductModel.ToRepositoryModel();

                App.InventoryRepository.Add(repositoryProductModel);

                LoadProducts();
            }

        }

        private void uxFileRemove_Click(object sender, RoutedEventArgs e)
        {

        }

        private CollectionView view;
        private void ListView_OnColumnClick(object s, RoutedEventArgs e)
        {

            view.SortDescriptions.Clear();

            if (e.OriginalSource == productNameCol)
            {
                view.SortDescriptions.Add(new SortDescription("ProductName", ListSortDirection.Ascending));
            }
            else if (e.OriginalSource == productNumberCol)
            {
                view.SortDescriptions.Add(new SortDescription("ProductNumber", ListSortDirection.Ascending));
            }
            else if (e.OriginalSource == pricePerItemCol)
            {
                view.SortDescriptions.Add(new SortDescription("PricePerItem", ListSortDirection.Ascending));
            }
            else if (e.OriginalSource == costPerItemCol)
            {
                view.SortDescriptions.Add(new SortDescription("CostPerItem", ListSortDirection.Ascending));
            }
            else if (e.OriginalSource == availableQuantityCol)
            {
                view.SortDescriptions.Add(new SortDescription("AvailableQuantity", ListSortDirection.Ascending));
            }
            else if (e.OriginalSource == productNumberCol)
            {
                view.SortDescriptions.Add(new SortDescription("Password", ListSortDirection.Ascending));
            }
        }
    }
}
