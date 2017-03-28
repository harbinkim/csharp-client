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
            var previousSortDescription = view.SortDescriptions.Any() ? view.SortDescriptions.First():new SortDescription();
            view.SortDescriptions.Clear();

            var newSortDescription = new SortDescription();

            if (e.OriginalSource == productNameCol)
            {
                newSortDescription.PropertyName = "ProductName";                
            }
            else if (e.OriginalSource == productNumberCol)
            {
                newSortDescription.PropertyName = "ProductNumber";
            }
            else if (e.OriginalSource == pricePerItemCol)
            {
                newSortDescription.PropertyName = "PricePerItem";
            }
            else if (e.OriginalSource == costPerItemCol)
            {
                newSortDescription.PropertyName = "CostPerItem";
            }
            else if (e.OriginalSource == availableQuantityCol)
            {
                newSortDescription.PropertyName = "AvailableQuantity";
            }


            if (previousSortDescription.PropertyName == newSortDescription.PropertyName)
            {
                if (previousSortDescription.Direction.Equals(ListSortDirection.Ascending))
                {
                    newSortDescription.Direction = ListSortDirection.Descending;
                }
                else
                {
                    newSortDescription.Direction = ListSortDirection.Ascending;
                }
            }

            view.SortDescriptions.Add(newSortDescription);

        }

        private ProductModel selectedProduct;
        private void uxProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedProduct = (ProductModel)uxProductList.SelectedValue;
        }
    }
}
