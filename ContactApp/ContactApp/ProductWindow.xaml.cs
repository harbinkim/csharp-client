using InventoryApp.Models;
using System;
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

            Product.ItemNumber = uxName.Text;
            Product.Email = uxEmail.Text;
            
            if (uxHome.IsChecked.Value)
            {
                Product.PhoneType = "Home";
            }
            else
            {
                Product.PhoneType = "Mobile";
            }

            Product.PhoneNumber = uxPhoneNumber.Text;

            // Add an age that matches to where the slider is at
            Product.Age = (int)uxAge.Value;
            Product.Notes = uxNotes.Text;
            Product.CreatedDate = DateTime.Now;

            DialogResult = true;
            Close();
        }

        private void uxCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}