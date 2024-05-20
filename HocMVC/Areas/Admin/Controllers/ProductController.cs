using Microsoft.AspNetCore.Mvc;
using MVC.DataAccess.Repository.IRepository;
using MVC.Models;

namespace MVC.Hoc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Product> objproducts = _unitOfWork.Product.GetAll().ToList();
            return View(objproducts);
        }
        //create 
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product obj)
        {
            if (ModelState.IsValid) // so sanh validation
            {
                _unitOfWork.Product.Add(obj); // same add migration
                _unitOfWork.Save(); // luu vao csdl // trong unitof work  co save
                TempData["success"] = "product created seccessfully";
                return RedirectToAction("Index", "Product"); // vi cung controller nen co the bo product
            }
            TempData["error"] = "Product insert failed";
            return View();
        }
        //update
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            //product? productFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //product? productFromDb2 = _db.Categories.Where(u=>u.Id == id).FirstOrDefault();  
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Product obj)
        {

            if (ModelState.IsValid) // so sanh validation
            {
                _unitOfWork.Product.Update(obj); // same add migration
                _unitOfWork.Save(); // luu vao csdl 
                TempData["success"] = "product updated seccessfully";
                return RedirectToAction("Index", "product"); // vi cung controller nen co the bo product
            }
            TempData["error"] = "product updated failed";
            return View();


        }
        //delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);

            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id) // doi ten thanh deletepost vi 2 ham khong the trung nhau ca ten va tham so
        {
            Product obj = _unitOfWork.Product.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save(); // luu vao csdl 
            TempData["success"] = "product deleted seccessfully";
            return RedirectToAction("Index", "product"); // vi cung controller nen co the bo product


        }
    }
}
