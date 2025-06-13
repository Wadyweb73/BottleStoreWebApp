using BottleStoreWebApp.Data;
using BottleStoreWebApp.Models;

namespace BottleStoreWebApp.Repository
{
    public class ProductInformationRepository : ProductInformationRepositoryInterface
    {
        private readonly ILogger<ProductInformationRepository> _logger;
        private readonly BottleStoreDbContext _context;

        public ProductInformationRepository(ILogger<ProductInformationRepository> logger, BottleStoreDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void Add(ProductInformation productInformation)
        {
            try
            {
                _context.ProductInformations.Add(productInformation);
                _context.SaveChanges();
                _logger.LogInformation($"Product information with ID {productInformation.Id} added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding product information: {ex.Message}");
            }
        }

        public List<ProductInformation> ListAllProductInformation()
        {
            return _context.ProductInformations.ToList();
        }

        public ProductInformation GetById(int id)
        {
            return _context.ProductInformations.Find(id);
        }
        public void Update(ProductInformation productInformation)
        {
            _context.ProductInformations.Update(productInformation);
            _context.SaveChanges();
            _logger.LogInformation($"Product information with ID {productInformation.Id} updated successfully.");
        }
    }
}