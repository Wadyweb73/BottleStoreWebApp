using BottleStoreWebApp.Models;

namespace BottleStoreWebApp.Repository 
{
    public interface ProductRepositoryInterface
    {
        public Product Add(Product product);

        public Product GetById(int id);

        public Product GetLastInsertedProduct();
        public List<Product> ListAllProducts();
        void Update(Product product);
        void BlockProduct(int id);
        void UnblockProduct(int id);
    }
}
