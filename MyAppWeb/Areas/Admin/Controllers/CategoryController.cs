using Microsoft.AspNetCore.Mvc;
using MyApp.DataAccessLayer;
using MyApp.DataAccessLayer.Infrastructure.IRepository;
using MyApp.Models;
using MyApp.Models.ViewModel;

namespace MyAppWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //public IActionResult AllCategories()
        //{
        //    var categories = _unitOfWork.Category.GetAll(includeProperties: "Category");

        //    return Json(new { data = categories });
        //}
        public IActionResult Index()
        {
            CategoryVM categoryVM = new CategoryVM();
            categoryVM.categories = _unitOfWork.Category.GetAll();
            return View(categoryVM);
            // return View();

        }
        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(Category cat)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Category.Add(cat);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Category successfully added !";
        //        return RedirectToAction("Index");
        //    }
        //    return View(cat);
        //}

        [HttpGet]
        public IActionResult CreateUpdate(int? id)
        {
            CategoryVM VM = new CategoryVM();
            if (id == null || id == 0)
            {
                return View(VM);
            }
            else
            {
                VM.Category = _unitOfWork.Category.GetT(x => x.Id == id);
                if (VM.Category == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(VM);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUpdate(CategoryVM VM)
        {
            if (ModelState.IsValid)
            {
                if (VM.Category.Id == 0)
                {
                    _unitOfWork.Category.Add(VM.Category);
                    TempData["success"] = "Category successfully Created !";
                }
                else
                {
                    _unitOfWork.Category.Update(VM.Category);
                    TempData["success"] = "Category successfully Updated !";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _unitOfWork.Category.GetT(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteData(int? id)
        {
            var category = _unitOfWork.Category.GetT(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Delete(category);
            _unitOfWork.Save();
            TempData["success"] = "Category successfully deleted !";
            return RedirectToAction("Index");
        }
    }
}
