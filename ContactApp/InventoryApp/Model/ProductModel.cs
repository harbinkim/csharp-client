using InventoryRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryApp.Model
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int ProductNumber { get; set; }
        public string ProductDescription { get; set; }
        public double PricePerItem { get; set; }
        public int AvailableQuantity { get; set; }
        public double CostPerItem { get; set; }
        public double ValueOfItem { get; set; }

        public InventoryRepository.ProductModel ToRepositoryModel()
        {
            var respositoryModel = new InventoryRepository.ProductModel
            {
                Id = Id,
                ProductName = ProductName,
                ProductNumber = ProductNumber,
                ProductDescription = ProductDescription,
                PricePerItem = PricePerItem,
                AvailableQuantity = AvailableQuantity,
                CostPerItem = CostPerItem,
                ValueOfItem = ValueOfItem
            };
            return respositoryModel;
        }

        public static ProductModel ToModel(InventoryRepository.ProductModel repositoryModel)
        {
            var productModel = new ProductModel
            {
                Id = repositoryModel.Id,
                ProductName = repositoryModel.ProductName,
                ProductNumber = repositoryModel.ProductNumber,
                ProductDescription = repositoryModel.ProductDescription,
                PricePerItem = repositoryModel.PricePerItem,
                AvailableQuantity = repositoryModel.AvailableQuantity,
                CostPerItem = repositoryModel.CostPerItem,
                ValueOfItem = repositoryModel.ValueOfItem
            };

            return productModel;
        } 
        
        public ProductModel ShallowCopy()
        {
            return (ProductModel)this.MemberwiseClone();
        }
                   
    }
}
