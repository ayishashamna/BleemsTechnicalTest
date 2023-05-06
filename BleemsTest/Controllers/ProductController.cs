using BleemsTest.Models;
using BleemsTest.Models.ViewModels;
using BleemsTest.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BleemsTest.Migrations;

namespace BleemsTest.Controllers
{
    public class ProductController : Controller
    {

        private readonly ProductServices _productServices;
        private readonly CategoryService _categoryService;
        public ProductController(ProductServices productServices, CategoryService categoryService)
        {
            _productServices = productServices;
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            var products = _productServices.GetProduts();
            var years = products.Select(p => p.Date.Year).Distinct().ToList();
            var months = products.Select(p => p.Date.Month.ToString("D2")).Distinct().ToList();
            var days = products.Select(p => p.Date.Day.ToString("D2")).Distinct().ToList();
            // Store the values in ViewBag properties
            ViewBag.Years = years;
            ViewBag.Months = months;
            ViewBag.Days = days;

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryList =await _categoryService.GetCategories();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Products product, IFormFile photo)
        {
            ViewBag.CategoryList =await _categoryService.GetCategories();
            ResponseModel model = await _productServices.AddProduct(product, photo);
            ViewBag.Message = model.Message;
            
            return View("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Products products = await _productServices.GetProductById(id);
            ViewBag.CategoryList = await _categoryService.GetCategories();
            return View(products);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Products product, IFormFile photo)
        {
            ResponseModel response =await _productServices.Update(product, photo);
            ViewBag.CategoryList = await _categoryService.GetCategories();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _productServices.GetProduts();
            return Json(products);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Products product =await _productServices.GetProductById(id);
            _productServices.Delete(product);
            return RedirectToAction("Index");
        }
    }
}
