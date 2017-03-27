using InventoryApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;


namespace InventoryApp
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window, INotifyDataErrorInfo
    {
        public ProductWindow()
        {
            InitializeComponent();
            ShowInTaskbar = false;
            IsValidProduct();
        }

        public ProductModel Product { get; set; }

        private void uxAvailableQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (uxAvailableQuantity.Text == null)
            {
                return;
            }

            if (!int.TryParse(uxAvailableQuantity.Text, out _availableQuantity))
                uxAvailableQuantity.Text = _availableQuantity.ToString();

            IsValidProduct();
        }

        private void uxPricePerItem_LostFocus(object sender, RoutedEventArgs e)
        {
            double amount = 0.0d;
            if (Double.TryParse(uxPricePerItem.Text, NumberStyles.Currency, null, out amount))
            {
                uxPricePerItem.Text = amount.ToString("C");
            }
            IsValidProduct();
        }

        private void uxCostPerItem_LostFocus(object sender, RoutedEventArgs e)
        {
            double amount = 0.0d;
            if (Double.TryParse(uxCostPerItem.Text, NumberStyles.Currency, null, out amount))
            {
                uxCostPerItem.Text = amount.ToString("C");
            }
            IsValidProduct();
        }

        private int _availableQuantity;
        public int AvailableQuantity
        {
            get { return _availableQuantity;}
            set { _availableQuantity = value;
                uxAvailableQuantity.Text = value.ToString();
            }
        }

        private void uxUpButton_Click(object sender, RoutedEventArgs e)
        {
            //increment the available quantity
            AvailableQuantity++;
        }

        private void uxDownButton_Click(object sender, RoutedEventArgs e)
        {
            //decrement the available quantity
            AvailableQuantity--;
        }

        private void uxSubmit_Click(object sender, RoutedEventArgs e)
        {
            Product = new ProductModel();

            Product.ProductName = uxProductName.Text;

            int intTemp;
            Int32.TryParse(uxProductNumber.Text, out intTemp);
            Product.ProductNumber = intTemp;

            Product.ProductDescription = uxDescription.Text;

            double amount = 0.0d;
            if (Double.TryParse(uxPricePerItem.Text, NumberStyles.Currency, null, out amount))
            {
                Product.PricePerItem = amount;
            }
            if (Double.TryParse(uxCostPerItem.Text, NumberStyles.Currency, null, out amount))
            {
                Product.CostPerItem = amount;
            }

            Product.AvailableQuantity = AvailableQuantity;

            DialogResult = true;
            Close();

        }

        private void uxCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void ux_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsValidProduct();
        }

        Regex numericRegex = new Regex("[^0-9.-]+");
        private const string EMPTYNAME_ERROR = "Product name cannot be empty.";
        private const string EMPTY_PRODUCTNUMBER_ERROR = "Product number cannot be empty.";
        private const string NONNUMERIC_PRODUCTNUMBER_ERROR = "Product number can only contain numbers.";
        private const string NONNUMERIC_PRICEPERITEM_ERROR = "Price cannot contain characters.";
        private const string NEGATIVE_PRICEPERITEM_ERROR = "Price cannot be negative.";
        private const string NEGATIVE_AVAILABLEQUANTITY_ERROR = "Available quantity cannot be negative.";


        private bool IsValidProduct()
        {
            if (!this.IsInitialized) return false;

            bool isValid = true;

            if (string.IsNullOrWhiteSpace(uxProductName.Text))
            {
                isValid = false;
                AddError(nameof(Product.ProductName),EMPTYNAME_ERROR);
            }
            else
            {
                RemoveError(nameof(Product.ProductName), EMPTYNAME_ERROR);
            }

            int tempInt;

            if (string.IsNullOrEmpty(uxProductNumber.Text))
            {
                isValid = false;
                AddError(nameof(Product.ProductNumber), EMPTY_PRODUCTNUMBER_ERROR);
            }
            else if(!int.TryParse(uxProductNumber.Text,out tempInt))
            {
                isValid = false;
                AddError(nameof(Product.ProductNumber), NONNUMERIC_PRODUCTNUMBER_ERROR);
            }
            else
            {
                RemoveError(nameof(Product.ProductNumber), NONNUMERIC_PRODUCTNUMBER_ERROR);
                RemoveError(nameof(Product.ProductNumber), EMPTY_PRODUCTNUMBER_ERROR);
            }

            double tempDbl = 0;

            if (!Double.TryParse(uxPricePerItem.Text,NumberStyles.AllowCurrencySymbol|NumberStyles.AllowDecimalPoint|NumberStyles.AllowThousands, CultureInfo.GetCultureInfo("en-US"),out tempDbl))
            {
                isValid = false;
                AddError(nameof(Product.PricePerItem), NONNUMERIC_PRICEPERITEM_ERROR);
            }
            else
            {
                RemoveError(nameof(Product.PricePerItem), NONNUMERIC_PRICEPERITEM_ERROR);
            }

            if (tempDbl < 0)
            {
                isValid = false;
                AddError(nameof(Product.PricePerItem), NEGATIVE_PRICEPERITEM_ERROR);
            }
            else
            {
                RemoveError(nameof(Product.PricePerItem), NEGATIVE_PRICEPERITEM_ERROR);
            }

            if (!Double.TryParse(uxCostPerItem.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, CultureInfo.GetCultureInfo("en-US"), out tempDbl))
            {
                isValid = false;
                AddError(nameof(Product.CostPerItem), NONNUMERIC_PRICEPERITEM_ERROR);
            }
            else
            {
                RemoveError(nameof(Product.CostPerItem), NONNUMERIC_PRICEPERITEM_ERROR);
            }

            if (tempDbl < 0)
            {
                isValid = false;
                AddError(nameof(Product.CostPerItem), NEGATIVE_PRICEPERITEM_ERROR);
            }
            else
            {
                RemoveError(nameof(Product.CostPerItem), NEGATIVE_PRICEPERITEM_ERROR);
            }


            if (AvailableQuantity < 0)
            {
                isValid = false;
                AddError(nameof(Product.AvailableQuantity), NEGATIVE_AVAILABLEQUANTITY_ERROR);
            }
            else
            {
                RemoveError(nameof(Product.AvailableQuantity), NEGATIVE_AVAILABLEQUANTITY_ERROR);
            }

            if (!(uxSubmit == null))
            {
                uxSubmit.IsEnabled = isValid;
            }

            var errorString = "";

            foreach (List<string> err in errors.Values){
                foreach(string e in err)
                {
                    errorString += e + Environment.NewLine;
                }
            }

            if (!(uxError == null))
            {
                uxError.Text = errorString;
            }
            

            return isValid;
        }

        
        

        public void AddError(string propertyName, string error)
        {
            if (!errors.ContainsKey(propertyName))
                errors[propertyName] = new List<string>();

            if (!errors[propertyName].Contains(error))
            {
                errors[propertyName].Add(error);
                RaiseErrorsChanged(propertyName);
            }
        }

        public void RemoveError(string propertyName, string error)
        {
            if (errors.ContainsKey(propertyName) &&
                errors[propertyName].Contains(error))
            {
                errors[propertyName].Remove(error);
                if (errors[propertyName].Count == 0) errors.Remove(propertyName);
                RaiseErrorsChanged(propertyName);
            }
        }

        public void RaiseErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }


        private Dictionary<String, List<String>> errors =
            new Dictionary<string, List<string>>();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            if (String.IsNullOrEmpty(propertyName) ||
                !errors.ContainsKey(propertyName)) return null;
            return errors[propertyName];
        }

        public bool HasErrors
        {
            get { return errors.Count > 0; }
        }

        
    }
}
