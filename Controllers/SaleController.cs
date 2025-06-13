using BottleStoreWebApp.Data;
using BottleStoreWebApp.Models;
using BottleStoreWebApp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BottleStoreWebApp.Controllers
{
    public class SaleController : Controller
    {
        private readonly ILogger<SaleController> _logger;
        private readonly BottleStoreDbContext _context;
        private readonly SaleRepositoryInterface _saleRepositoryInterface;

        public SaleController(ILogger<SaleController> logger, BottleStoreDbContext context, SaleRepositoryInterface saleRepositoryInterface)
        {
            _logger = logger;
            _context = context;
            _saleRepositoryInterface = saleRepositoryInterface;
        }

        // [HttpPost]
        // public IActionResult Add(ProductListViewModel productListViewModel)
        // {
        //     List<Sale> sales = productListViewModel.Sales;

        //     if (sales == null || sales.Count == 0)
        //     {
        //         return BadRequest("No sales data provided.");
        //     }

        //     foreach (Sale sale in sales)
        //     {
        //         _saleRepositoryInterface.Add(sale);
        //     }

        //     return RedirectToAction("Sale", "Home");
        // }

        [HttpPost]
        public IActionResult Add(ProductListViewModel productListViewModel)
        {
            try
            {
                List<Sale> sales = productListViewModel.Sales;

                if (sales == null || sales.Count == 0)
                {
                    TempData["ErrorMessage"] = "Nenhum produto foi adicionado ao carrinho.";
                    return RedirectToAction("Sale", "Home");
                }

                // Validar se todos os produtos existem e têm preços válidos
                foreach (Sale sale in sales)
                {
                    // Verificar se o produto existe
                    var product = _context.Products.Find(sale.ProductId);
                    if (product == null)
                    {
                        TempData["ErrorMessage"] = $"Produto com ID {sale.ProductId} não encontrado.";
                        return RedirectToAction("Sale", "Home");
                    }

                    // Verificar se o usuário existe (se necessário)
                    var user = _context.Users.Find(sale.UserId);
                    if (user == null)
                    {
                        TempData["ErrorMessage"] = $"Usuário com ID {sale.UserId} não encontrado.";
                        return RedirectToAction("Sales", "Home");
                    }

                    // Definir a data da venda
                    sale.SaleDate = DateTime.Now;
                    
                    // Adicionar a venda
                    _saleRepositoryInterface.Add(sale);
                }

                TempData["SuccessMessage"] = $"Venda realizada com sucesso! {sales.Count} item(s) vendido(s).";
                return RedirectToAction("Sales", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar venda");
                TempData["ErrorMessage"] = "Erro interno do servidor. Tente novamente.";
                return RedirectToAction("Sales", "Home");
            }
        }

        public IActionResult SalesList()
        {
            List<Sale> sales = _saleRepositoryInterface.ListAllSales();
            return View(sales);
        }
    }
}