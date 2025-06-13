using BottleStoreWebApp.Models;

namespace BottleStoreWebApp.Repository
{
    public interface SaleRepositoryInterface
    {
        Sale Add(Sale sale);
        Sale GetById(int id);
        List<Sale> ListAllSales();
        void Update(Sale sale);
        List<Sale> GetSalesByUserId(int userId);
    }
}