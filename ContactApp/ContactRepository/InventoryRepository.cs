using InventoryDB;
using System.Collections.Generic;
using System.Linq;

namespace InventoryRepository
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
    }

    public class InventoryRepository
    {
        public ProductModel Add(ProductModel InventoryModel)
        {
            var ProductDb = ToDbModel(InventoryModel);

            DatabaseManager.Instance.Products.Add(ProductDb);
            DatabaseManager.Instance.SaveChanges();

            InventoryModel = new ProductModel
            {
                Id = ProductDb.ProductId,
                ItemNumber = ProductDb.ProductNumber,
                Description = ProductDb.ProductDescription,
                PricePerItem = ProductDb.PricePerItem,
                AvailableQuantity = ProductDb.AvailableQuantity,
                CostPerItem = ProductDb.CostPerItem,
                ValueOfItem = ProductDb.CostPerItem * ProductDb.AvailableQuantity,
            };
            return InventoryModel;
        }

        public List<ProductModel> GetAll()
        {
            // Use .Select() to map the database Product to ProductModel
            var items = DatabaseManager.Instance.Products
              .Select(t => new ProductModel
              {
                  Id = t.ProductId,
                  ItemNumber = t.ProductNumber,
                  Description = t.ProductDescription,
                  PricePerItem = t.PricePerItem,
                  AvailableQuantity = t.AvailableQuantity,
                  CostPerItem = t.CostPerItem,
                  ValueOfItem = t.CostPerItem * t.AvailableQuantity,
              }).ToList();

            return items;
        }

        public bool Update(ProductModel ProductModel)
        {
            var original = DatabaseManager.Instance.Products.Find(ProductModel.Id);

            if (original != null)
            {
                DatabaseManager.Instance.Entry(original).CurrentValues.SetValues(ToDbModel(ProductModel));
                DatabaseManager.Instance.SaveChanges();
            }

            return false;
        }

        public bool Remove(int ProductId)
        {
            var items = DatabaseManager.Instance.Products
                                .Where(t => t.ProductId == ProductId);
            if (items.Count() == 0)
            {
                return false;
            }

            DatabaseManager.Instance.Products.Remove(items.First());
            DatabaseManager.Instance.SaveChanges();

            return true;
        }

        private Product ToDbModel(ProductModel ProductModel)
        {
            var InventoryDb = new Product
            {
                ProductId = ProductModel.Id,
                ProductNumber = ProductModel.ItemNumber,
                ProductDescription = ProductModel.Description,
                PricePerItem = ProductModel.PricePerItem,
                AvailableQuantity = ProductModel.AvailableQuantity,
                CostPerItem = ProductModel.CostPerItem,
                ValueOfItem = ProductModel.ValueOfItem
            };

            return InventoryDb;
        }
    }
}