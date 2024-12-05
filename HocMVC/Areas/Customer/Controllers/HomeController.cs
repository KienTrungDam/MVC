using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.DataAccess.Repository;
using MVC.DataAccess.Repository.IRepository;
using MVC.Models;
using System.Diagnostics;
using System.Security.Claims;


namespace MVC.Hoc.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger , IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;   
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties:"Category");
            return View(productList);
        }
        public IActionResult Details(int productId)
        {
            ShoppingCart shoppingCart = new()
            {
                Product = _unitOfWork.Product.Get(u => u.Id == productId, includeProperties: "Category"),
                ProductId = productId,
                Count = 1
            };
            return View(shoppingCart);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart cart)
        {
            //ClaimsIdentity: chua danh sach user / User.Identity: lay user hien tai
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            // NameIdentifier: ID duy nhat c?a nguoi dung trong he thong / tim ra Id ?au tien giong
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value; 
            cart.ApplicationUserId = userId;
            // thong tin con lai lay o tren
            ShoppingCart Temp = _unitOfWork.ShoppingCart.Get(u => u.ApplicationUserId == userId && u.ProductId == cart.ProductId);
            if(Temp != null)
            {
                Temp.Count += cart.Count;
                _unitOfWork.ShoppingCart.Update(Temp);
            }
            else
            {
                _unitOfWork.ShoppingCart.Add(cart);              
            }
            _unitOfWork.Save();
            TempData["success"] = "Order seccessfully";
            return RedirectToAction("Index", "Home");
        }
        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
