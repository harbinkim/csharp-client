using InventoryApp.Models;
using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace InventoryApp
{
    /// <summary>
    /// Interaction logic for InventoryWindow.xaml
    /// </summary>
    public partial class InventoryWindow : Window
    {
        public InventoryWindow()
        {
            InitializeComponent();

            // Don't show this window in the task bar
            ShowInTaskbar = false;
        }

        public ProductModel Product { get; set; }

        private void uxSubmit_Click(object sender, RoutedEventArgs e)
        {
            Product = new ProductModel();

            Product.ProductName = uxProductName.Text;

            int intTemp;
            Int32.TryParse(uxItemNumber.Text, out intTemp);
            Product.ItemNumber = intTemp;

            double doubleTemp;
            Double.TryParse(uxPricePerItem.Text,out doubleTemp);
            Product.PricePerItem = doubleTemp;
            //// Add an age that matches to where the slider is at
            //Product.Age = (int)uxAge.Value;
            //Product.Notes = uxNotes.Text;
            //Product.CreatedDate = DateTime.Now;

            DialogResult = true;
            Close();
        }

        private void uxCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        Regex numericRegex = new Regex("[^0-9.-]+");
        private void uxItemNumber_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            //Only allows numeric input into uxItemNumber
            e.Handled = !numericRegex.IsMatch(e.Text);           
        }
    }
}