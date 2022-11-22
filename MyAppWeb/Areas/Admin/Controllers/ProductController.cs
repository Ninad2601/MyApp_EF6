using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyApp.DataAccessLayer;
using MyApp.DataAccessLayer.Infrastructure.IRepository;
using MyApp.Models;
using MyApp.Models.ViewModel;
using XAct.Library.Settings;

namespace MyAppWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IWebHostEnvironment _hostingEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }
        #region APICall

        public IActionResult AllProducts()
        {
            var products = _unitOfWork.Product.GetAll(includeProperties: "Category");//Category is navigation property

            return Json(new { data = products });
        }

        #endregion APICall
        public IActionResult Index()
        {
            //ProductVM productVM = new ProductVM();
           // productVM.Products = _unitOfWork.Product.GetAll();
            //var products = _unitOfWork.Product.GetAll(includeProperties: "Category");
            //return View(products);
            return View();
        }

        [HttpGet]
        public IActionResult CreateUpdate(int? id)
        {
            // For getting product category on dropdown selection 
            ProductVM VM = new ProductVM()
            {
                Product = new(),//Product initialize
                Categories = _unitOfWork.Category.GetAll().Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                })

            };
            if (id == null || id == 0)
            {
                return View(VM);
            }
            else
            {
                VM.Product = _unitOfWork.Product.GetT(x => x.Id == id);
                if (VM.Product == null)
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
        public IActionResult CreateUpdate(ProductVM VM, IFormFile? file)//use file name property only
        {
            if (ModelState.IsValid)
            {
                //To Upload image & set to db
                string fileName = String.Empty;

                if (file != null)
                {
                    string uploadDir = Path.Combine(_hostingEnvironment.WebRootPath, "ProductImages");
                    fileName = Guid.NewGuid().ToString() + "~" + file.FileName;
                    string filePath = Path.Combine(uploadDir, fileName);

                    //For Deleting old image on clicking of edit
                    if (VM.Product.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(_hostingEnvironment.WebRootPath,VM.Product.ImageUrl.Trim('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    
                    using (var filestream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    VM.Product.ImageUrl = @"\ProductImages\" + fileName;
                }

                if (VM.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(VM.Product);
                    TempData["success"] = "Product successfully added !";
                }
                else
                {
                    _unitOfWork.Product.Update(VM.Product);
                    TempData["success"] = "Product successfully Updated !";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    var category = _unitOfWork.Category.GetT(x => x.Id == id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(category);
        //}

        #region DeleteAPI 
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id)
        {
            var product = _unitOfWork.Category.GetT(x => x.Id == id);
            if (product == null)
            {
                return Json(new {success=false,Error="Error while fetch"});
            }

            else
            {
                var oldImagePath = Path.Combine(_hostingEnvironment.WebRootPath,product.image Trim('\\'));

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            _unitOfWork.Category.Delete(product);
            _unitOfWork.Save();
            TempData["success"] = "Category successfully deleted !";
            return RedirectToAction("Index");
        }
        #endregion DeleteAPI
    }
}
