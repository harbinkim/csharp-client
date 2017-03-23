using System;

namespace InventoryApp.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public int ItemNumber { get; set; }
        public string Description { get; set; }
        public double PricePerItem { get; set; }
        public int AvailableQuantity { get; set; }
        public double CostPerItem { get; set; }
        public double ValueOfItem { get; set; }

        public InventoryRepository.ProductModel ToRepositoryModel()
        {
            var repositoryModel = new InventoryRepository.ProductModel
            {
                Id = Id,
                ItemNumber = ItemNumber,
                Description = Description,
                PricePerItem = PricePerItem,
                AvailableQuantity = AvailableQuantity,
                CostPerItem = CostPerItem,
                ValueOfItem = ValueOfItem               
            };

            return repositoryModel;
        }

        public static ProductModel ToModel(InventoryRepository.ProductModel respositoryModel)
        {
            var InventoryModel = new ProductModel
            {
                Id = respositoryModel.Id,
                ItemNumber = respositoryModel.ItemNumber,
                Description = respositoryModel.Description,
                PricePerItem = respositoryModel.PricePerItem,
                AvailableQuantity = respositoryModel.AvailableQuantity,
                CostPerItem = respositoryModel.CostPerItem,
                ValueOfItem = respositoryModel.ValueOfItem
            };

            return InventoryModel;
        }
    }
}