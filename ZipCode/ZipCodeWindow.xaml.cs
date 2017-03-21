using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace ZipCode
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ZipCodeWindow : Window
    {
        public ZipCodeWindow()
        {
            InitializeComponent();
        }

        private string USZipPattern = @"^\d{5}(?:[-\s]\d{4})?$";
        private string CAZipPattern = @"^([A-Z]\d[A-Z])\ {0,1}(\d[A-Z]\d)$";

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var zip = ((TextBox)sender).Text;
            if (Regex.Match(zip,USZipPattern).Success || Regex.Match(zip, CAZipPattern).Success)
            {
                uxSubmitButton.IsEnabled = true;
            }
            else
            {
                uxSubmitButton.IsEnabled = false;
            }

        }
    }
}
