using BleemsTest.Models;
using BleemsTest.Models.ViewModels;
using BleemsTest.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;

namespace BleemsTest.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;
        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.CategoryList = await _categoryService.GetCategories();
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if(ModelState.IsValid)
            {
                ResponseModel model = await _categoryService.AddCategory(category);
                ViewBag.Message = model.Message;
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Category category = await _categoryService.GetCategory(id);
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            if(ModelState.IsValid)
            {
                await _categoryService.Update(category);
            }
            return RedirectToAction("Index");
        }
    }
}
