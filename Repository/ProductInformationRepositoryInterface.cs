using BottleStoreWebApp.Models;

namespace BottleStoreWebApp.Repository
{
    public interface ProductInformationRepositoryInterface
    {
        void Add(ProductInformation productInformation);
        List<ProductInformation> ListAllProductInformation();
        ProductInformation GetById(int id);
    }
}