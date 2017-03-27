using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace InventoryApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static InventoryRepository.InventoryRepository inventoryRepository;

        static App()
        {
            inventoryRepository = new InventoryRepository.InventoryRepository();

        }

        public static InventoryRepository.InventoryRepository InventoryRepository
        {
            get
            {
                return inventoryRepository;
            }
        }
    }
}
