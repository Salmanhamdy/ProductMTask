using Microsoft.AspNetCore.Mvc;
using ProductMTask.Dtos;
using ProductMTask.Models;
using ProductMTask.Services;
using System.Threading.Tasks;

namespace ProductMTask.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Create()
        {    
            return View(new ProductDto());
        }
        [HttpPost]
        public async Task<JsonResult> Create(ProductDto dto)
        {
            await _productService.CreateAsync(dto);
            return  Json(new { success = true,message="Created", redirectUrl = Url.Action("Index", "Product") });
        }
        public IActionResult Index()
        {
           return View();
        }
        [HttpGet]
        public async Task<IActionResult> LoadProducts()
        {
            var products = await _productService.GetProductsAsync();
            return Json(products); 
        }
        [HttpGet("Product/Edit/{code}")]
        public async Task<IActionResult> Edit( [FromRoute] string Code)
        {
            var  product = await _productService.GetProductByCodeAsync(Code);
            if (product == null)
                return NotFound();
             var productdto=new ProductDto() 
             {
                 Name=product.Name,
                 InitialQuantity=product.InitialQuantity,
                 Price=product.Price,
                 Unit=product.Unit,
                 Code=product.Code
             }
             ;
            return View(productdto);
        }

        [HttpPost]
        public async Task<JsonResult> Edit(ProductDto productDto)
        {
            await _productService.Update(productDto);
            return Json(new { success = true, message = "Product Updated", redirectUrl = Url.Action("Index", "Product") });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string code)
        {
            var deleted = await _productService.DeleteAsync(code);
            if (!deleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
