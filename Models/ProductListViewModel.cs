namespace BottleStoreWebApp.Models
{
    public class ProductListViewModel
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public List<ProductInformation> ProductInformationList { get; set; } = new List<ProductInformation>();
        public List<Sale> Sales { get; set; } = new List<Sale>();
        public List<User> Users { get; internal set; } = new List<User>();
    }
}