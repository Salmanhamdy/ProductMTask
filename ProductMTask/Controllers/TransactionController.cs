using Humanizer;
using Microsoft.AspNetCore.Mvc;
using ProductMTask.Dtos;
using ProductMTask.Models;
using ProductMTask.Services;
using System.Threading.Tasks;

namespace ProductMTask.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransationService _transationService;
        private readonly IProductService _productService;

        public TransactionController(ITransationService transationService,IProductService productService)
        {
           _transationService = transationService;
            this._productService = productService;
        }
        public IActionResult Index()
        {

      
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> LoadTransactions(DateTime? from = null, DateTime? to = null)
        {
            var transactions = await _transationService.GetTransations(from, to);
            return Json(transactions);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Products = await _productService.GetProductsAsync();
            return View(new TransationDto());
        }
        [HttpPost]
        public async Task<IActionResult> Create(TransationDto dto)
        {
            if (ModelState.IsValid)
            {
               
                await _transationService.Create(dto);
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Failed to save the transaction" });
        }

    }
}
