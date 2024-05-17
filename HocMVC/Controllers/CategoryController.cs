using Microsoft.AspNetCore.Mvc;
using MVC.DataAccess.Data;
using MVC.DataAccess.Repository.IRepository;
using MVC.Models;

namespace HocMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository db) 
        {
            _categoryRepo = db;
        }
        //ApplicationDbContext db = new ApplicationDbContext()
        //get
        public IActionResult Index()
        {
            List<Category> objCategoryList = _categoryRepo.GetAll().ToList();
            return View(objCategoryList);
        }
        //insert
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DesplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Name va Display Order khong duoc trung nhau"); // key(Name) de hien thong bao loi validation
            }
            if (ModelState.IsValid) // so sanh validation
            {
                _categoryRepo.Add(obj); // same add migration
                _categoryRepo.Save(); // luu vao csdl 
                TempData["success"] = "Category created seccessfully";
                return RedirectToAction("Index", "Category"); // vi cung controller nen co the bo Category
            }
            TempData["error"] = "Category insert failed";
            return View();
            
        }
        //update
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _categoryRepo.Get(u=>u.Id==id);
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id == id).FirstOrDefault();  
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {

            if (ModelState.IsValid) // so sanh validation
            {
                _categoryRepo.Update(obj); // same add migration
                _categoryRepo.Save(); // luu vao csdl 
                TempData["success"] = "Category updated seccessfully";
                return RedirectToAction("Index", "Category"); // vi cung controller nen co the bo Category
            }
            TempData["error"] = "Category updated failed";
            return View();
            

        }
        //delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _categoryRepo.Get(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int? id) // doi ten thanh deletepost vi 2 ham khong the trung nhau ca ten va tham so
        {
            Category obj = _categoryRepo.Get(u => u.Id == id);
            if (obj == null)
            {  
                return NotFound();
            }
            _categoryRepo.Remove(obj);
            _categoryRepo.Save(); // luu vao csdl 
            TempData["success"] = "Category deleted seccessfully";
            return RedirectToAction("Index", "Category"); // vi cung controller nen co the bo Category
           

        }
    }
}
