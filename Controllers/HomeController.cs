using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BottleStoreWebApp.Models;
using BottleStoreWebApp.Data;
using BottleStoreWebApp.Repository;

namespace BottleStoreWebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BottleStoreDbContext _context; 
    private readonly UserRepositoryInterface _userRepositoryInterface;

    public HomeController(ILogger<HomeController> logger, BottleStoreDbContext context, UserRepositoryInterface userRepositoryInterface)
    {
        _logger  = logger;
        _context = context;
        _userRepositoryInterface = userRepositoryInterface;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult SignUp()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Dashboard() {
        User user = new User();

        user.Name = "Jose Eduardo Dos Santos";
        user.EmailAddress = "jose.edusantos@gmail.com";

        return View(user);
    }
    
    public IActionResult Sales()
    {
        ProductListViewModel ProductList = new ProductListViewModel();
        List<Product> products = _context.Products.ToList();
        List<ProductInformation> productInformationList = _context.ProductInformations.ToList();

        foreach (Product product in products)
        {
            if (product.IsActive == true)
            {
                ProductInformation productInformation = productInformationList.FirstOrDefault(pi => pi.ProductId == product.Id);

                if (productInformation.AmountBoxes <= 0)
                {
                    continue;
                }

                ProductList.Products.Add(product);
                ProductList.ProductInformationList.Add(productInformation);
            }
        }

        return View(ProductList);
    }

    public IActionResult ListProducts()
    {
        ProductListViewModel productListViewModel = new ProductListViewModel();
        List<Product> products = _context.Products.ToList();
        List<ProductInformation> productInformationList = _context.ProductInformations.ToList();

        foreach (Product product in products)
        {
            if (product.IsActive == true)
            {
                productListViewModel.Products.Add(product);
                productListViewModel.ProductInformationList.Add(
                    productInformationList.FirstOrDefault(pi => pi.ProductId == product.Id)
                );
            }
        }

        return View(productListViewModel);
    }
    
    public IActionResult ListBlockedProducts() {
        ProductListViewModel productListViewModel       = new ProductListViewModel();
        List<Product> products                          = _context.Products.ToList();
        List<ProductInformation> productInformationList = _context.ProductInformations.ToList();

        foreach (Product product in products)
        {
            if (product.IsActive == false)
            {
                productListViewModel.Products.Add(product);
                productListViewModel.ProductInformationList.Add(
                    productInformationList.FirstOrDefault(pi => pi.ProductId == product.Id)
                );
            }
        }

        return View(productListViewModel);
    }

    public IActionResult ListUsers()
    {
        UserListViewModel userListViewModel = new UserListViewModel();
        userListViewModel.SelectedUser = new User();
        List<User> users = _userRepositoryInterface.ListAllUsers();

        foreach (User user in users)
        {
            if (user.IsActive == true)
            {
                userListViewModel.Users.Add(user);
            }
        }

        return View(userListViewModel);
    }
    
    public IActionResult ListBlockedUsers()
    {
        UserListViewModel userListViewModel = new UserListViewModel();
        List<User> users = _userRepositoryInterface.ListAllUsers();
        userListViewModel.SelectedUser = new User();

        foreach (User user in users)
        {
            if (user.IsActive == false)
            {
                userListViewModel.Users.Add(user);
            }
        }

        return View(userListViewModel);
    }

    public IActionResult RegisterUser()
    {
        return View();
    }

    public IActionResult RegisterProduct() {
        return View();
    }

    public IActionResult ListSales()
    {
        ProductListViewModel productListViewModel = new ProductListViewModel();
        List<Product> products = _context.Products.ToList();
        List<ProductInformation> productInformationList = _context.ProductInformations.ToList();
        List<Sale> sales = _context.Sales.ToList();
        List<User> users = _userRepositoryInterface.ListAllUsers();

        foreach (Product product in products)
        {
            if (product.IsActive == true)
            {
                productListViewModel.Products.Add(product);
                productListViewModel.ProductInformationList.Add(
                    productInformationList.FirstOrDefault(pi => pi.ProductId == product.Id)
                );
            }
        }

        productListViewModel.Sales = sales;
        productListViewModel.Users = users;

        return View(productListViewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
