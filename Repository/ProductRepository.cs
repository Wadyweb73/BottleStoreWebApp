using BottleStoreWebApp.Data;
using BottleStoreWebApp.Models;
using BottleStoreWebApp.Repository; 

namespace BottleStoreWebApp.Repository
{
    public class ProductRepository : ProductRepositoryInterface
    {
        private readonly ILogger<ProductRepository> _logger;
        private readonly BottleStoreDbContext _context;

        public ProductRepository(ILogger<ProductRepository> logger, BottleStoreDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public Product Add(Product product)
        {
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                Console.WriteLine("Product saved successfully!");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while saving product!");
            }

            return product;
        }

        public Product GetById(int id)
        {
            return _context.Products.Find(id);
        }

        public Product GetLastInsertedProduct()
        {
            return _context.Products.OrderByDescending(p => p.Id).FirstOrDefault();
        }

        public List<Product> ListAllProducts()
        {
            return _context.Products.ToList();
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public void BlockProduct(int id)
        {
            Product product = GetById(id);

            if (product != null)
            {
                product.IsActive = false;
                _logger.LogInformation($"Blocking product with ID {id}.");
                _context.Products.Update(product);
                _context.SaveChanges();
            }
            else
            {
                _logger.LogError($"Product with ID {id} not found.");
            }
        }

        public void UnblockProduct(int id)
        {
            Product product = GetById(id);

            if (product != null)
            {
                product.IsActive = true;
                _logger.LogInformation($"Unblocking product with ID {id}.");
                _context.Products.Update(product);
                _context.SaveChanges();
            }
            else
            {
                _logger.LogError($"Product with ID {id} not found.");
            }
        }
    }
}