using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComputerWorld.Data;
using ComputerWorld.Extensions;
using ComputerWorld.Models;
using ComputerWorld.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerWorld.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public ShoppingCartViewModel ShoppingCartVM { get; set; }

        public ShoppingCartController(ApplicationDbContext db)
        {
            _db = db;
            ShoppingCartVM = new ShoppingCartViewModel()
            {
                Products = new List<Models.Products>()
            };
        }

        //Get Index Shopping Cart
        public IActionResult Index()
        {
            List<int> lstShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");

            if (lstShoppingCart.Count > 0)
            {
                foreach (int cartItem in lstShoppingCart)
                {
                    Products prod = _db.Products.Include(p => p.SpecialTags).Include(p => p.ProductTypes).Where(p => p.Id == cartItem).FirstOrDefault();
                    ShoppingCartVM.Products.Add(prod);
                }
            }

            return View(ShoppingCartVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public IActionResult IndexPost()
        {
            List<int> lstCartItems = HttpContext.Session.Get<List<int>>("ssShoppingCart");  //gives a list of cart items

            Orders  orders = ShoppingCartVM.Orders; 
            _db.Orders.Add(orders);
            _db.SaveChanges();         //taking the orders and saving it in the db

            int orderId = orders.Id;

            foreach (int productId in lstCartItems)
            {
                ProductsOrdered productsOrdered = new ProductsOrdered()
                {
                    OrderId = orderId,
                    ProductId = productId
                };
                _db.ProductsOrdered.Add(productsOrdered);

            }
            _db.SaveChanges();
            lstCartItems = new List<int>();
            HttpContext.Session.Set("ssShoppingCart", lstCartItems);

            return RedirectToAction("OrderConfirmation", "ShoppingCart", new { Id = orderId });

        }

        public IActionResult Remove(int id)  //as we mention ''remove' in the view with asp-route id
        {
            List<int> lstCartItems = HttpContext.Session.Get<List<int>>("ssShoppingCart");

            if (lstCartItems.Count > 0)
            {
                if (lstCartItems.Contains(id))
                {
                    lstCartItems.Remove(id);
                }
            }

            HttpContext.Session.Set("ssShoppingCart", lstCartItems);

            return RedirectToAction(nameof(Index));
        }
        //Get
        public IActionResult OrderConfirmation(int id)
        {
            ShoppingCartVM.Orders = _db.Orders.Where(a => a.Id == id).FirstOrDefault();
            List<ProductsOrdered> objProdList = _db.ProductsOrdered.Where(p => p.OrderId == id).ToList();

            foreach (ProductsOrdered prodAptObj in objProdList)
            {
                ShoppingCartVM.Products.Add(_db.Products.Include(p => p.ProductTypes).Include(p => p.SpecialTags).Where(p => p.Id == prodAptObj.ProductId).FirstOrDefault());
            }

            return View(ShoppingCartVM);
        }



    }
    }
