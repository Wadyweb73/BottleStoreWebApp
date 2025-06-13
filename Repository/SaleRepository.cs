using BottleStoreWebApp.Data;
using BottleStoreWebApp.Models;

namespace BottleStoreWebApp.Repository
{
    public class SaleRepository : SaleRepositoryInterface
    {
        private readonly ILogger<SaleRepository> _logger;
        private readonly BottleStoreDbContext _context;

        public SaleRepository(ILogger<SaleRepository> logger, BottleStoreDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public Sale Add(Sale sale)
        {
            try
            {
                _context.Sales.Add(sale);
                _context.SaveChanges();
                Console.WriteLine("Sale saved successfully!");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while saving sale!");
            }

            return sale;
        }

        public Sale GetById(int id)
        {
            return _context.Sales.Find(id);
        }

        public List<Sale> ListAllSales()
        {
            return _context.Sales.ToList();
        }

        public void Update(Sale sale)
        {
            _context.Sales.Update(sale);
            _context.SaveChanges();
        }

        public List<Sale> GetSalesByUserId(int userId)
        {
            return _context.Sales.Where(s => s.UserId == userId).ToList();
        }
    }
};