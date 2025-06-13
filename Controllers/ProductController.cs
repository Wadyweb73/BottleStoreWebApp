using System.Diagnostics;
using BottleStoreWebApp.Models;
using BottleStoreWebApp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BottleStoreWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ProductRepositoryInterface _productRepositoryInterface;
        private readonly ProductInformationRepositoryInterface _productInformationRepositoryInterface;

        public ProductController(ProductRepositoryInterface productRepositoryInterface, ProductInformationRepositoryInterface productInformationRepositoryInterface, ILogger<ProductController> logger)
        {
            _logger = logger;
            _productRepositoryInterface = productRepositoryInterface;
            _productInformationRepositoryInterface = productInformationRepositoryInterface;
        }

        [HttpPost]
        public IActionResult RegisterNewProduct(RegisterProductViewModel registerProductViewModel)
        {
            Product product = registerProductViewModel.Product;
            ProductInformation productInformation = registerProductViewModel.ProductInformation;

            if (product == null || productInformation == null)
            {
                _logger.LogError("Product or Product Information is null.");
                return BadRequest("Product or Product Information cannot be null.");
            }

            productInformation.ProductId = product.Id;

            this.Create(product);
            product = _productRepositoryInterface.GetLastInsertedProduct();

            productInformation.ProductId = product.Id;
            this.CreateProductInformation(productInformation);

            return RedirectToAction("ListProducts", "Home");
        }

        public void Create(Product product)
        {
            _productRepositoryInterface.Add(product);
        }

        public void CreateProductInformation(ProductInformation productInformation)
        {
            _productInformationRepositoryInterface.Add(productInformation);
        }

        [HttpPost]
        public IActionResult BlockProduct(int id)
        {
            Product product = _productRepositoryInterface.GetById(id);

            if (product == null)
            {
                _logger.LogError($"Product with ID {id} not found.");
                return NotFound();
            }

            _productRepositoryInterface.BlockProduct(id);

            return RedirectToAction("ListProducts", "Home");
        }

        public IActionResult UnBlockProduct(int id)
        {
            Product product = _productRepositoryInterface.GetById(id);

            if (product == null)
            {
                _logger.LogError($"Product with ID {id} not found.");
                return NotFound();
            }

            if (!product.IsActive)
            {
                _productRepositoryInterface.UnblockProduct(id);
                _logger.LogInformation($"Product with ID {id} has been unblocked.");
            }

            return RedirectToAction("ListBlockedProducts", "Home");
        }
    }
}