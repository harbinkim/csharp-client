using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls; // make sure this is included
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using InventoryApp.Models; // add this on top


namespace InventoryApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            LoadInventorys(); // call on method when main window is up
        }

        // do this when delete is selected
        private void uxFileDelete_Click(object sender, RoutedEventArgs e)
        {
            App.InventoryRepository.Remove(selectedInventory.Id);
            selectedInventory = null; 
            LoadInventorys();
        }

        // check the status of deletion, enable or disable the menu
        private void uxFileDelete_Loaded(object sender, RoutedEventArgs e)
        {
            uxFileDelete.IsEnabled = (selectedInventory != null);

            // Exercise: add the context menu deletion IsEnabled here
            uxContextFileDelete.IsEnabled = uxFileDelete.IsEnabled;
        }

        // for deletion: check selection being made
        private ProductModel selectedInventory;

        private void uxInventoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedInventory = (ProductModel)uxInventoryList.SelectedValue;
        }

        // sorting each column when their header is clicked
        private void GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);

            // Grab the Tag from the column header
            string sortBy = column.Tag.ToString();

            // Clear out any previous column sorting
            uxInventoryList.Items.SortDescriptions.Clear();

            // Sort the uxInventoryList by the column header tag (sortBy)
            // If you want to do Descending, look at the homework :) and SortAdorner
            var sortDescription = new System.ComponentModel.SortDescription(sortBy, 
                System.ComponentModel.ListSortDirection.Ascending);

            uxInventoryList.Items.SortDescriptions.Add(sortDescription);
        }

        // for context menu
        private void uxFileChange_Click(object sender, RoutedEventArgs e)
        {
        }

        // Add the Load Inventorys Method
        private void LoadInventorys()
        {
            var Inventorys = App.InventoryRepository.GetAll();

            uxInventoryList.ItemsSource = Inventorys
                .Select(t => ProductModel.ToModel(t))
                .ToList();

            // OR
            //var uiInventoryModelList = new List<InventoryModel>();
            //foreach (var repositoryInventoryModel in Inventorys)
            //{
            //    This is the .Select(t => ... )
            //    var uiInventoryModel = InventoryModel.ToModel(repositoryInventoryModel);
            //
            //    uiInventoryModelList.Add(uiInventoryModel);
            //}

            //uxInventoryList.ItemsSource = uiInventoryModelList;
        }

        private void uxFileNew_Click(object sender, RoutedEventArgs e)
        {
            var window = new InventoryWindow();

            if (window.ShowDialog() == true)
            {
                var uiInventoryModel = window.Product;

                var repositoryInventoryModel = uiInventoryModel.ToRepositoryModel();

                App.InventoryRepository.Add(repositoryInventoryModel);

                // OR
                //App.InventoryRepository.Add(window.Inventory.ToRepositoryModel());

                LoadInventorys(); //when adding call on load Inventorys to show up
            }
        }
    }
}