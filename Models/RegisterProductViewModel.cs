namespace BottleStoreWebApp.Models 
{
    public class RegisterProductViewModel
    {
        public Product Product { get; set; } = new Product();
        public ProductInformation ProductInformation { get; set; } = new ProductInformation();
    }
}