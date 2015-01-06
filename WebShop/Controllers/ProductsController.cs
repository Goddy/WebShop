using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;
using WebShop.Models;
using WebShop.Services;

namespace WebShop.Controllers
{
    public class ProductsController : AbstractController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService, IAccountService accountService)
            : base(accountService)
        {
            _productService = productService;
        }
        // GET: Products
        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewData["Categories"] = _productService.GetAllCategories();
            ViewData["Price"] = getPriceValues();
            return View(_productService.GetAllProducts().ToPagedList(1, 10));
        }

        // GET: Products
        [Authorize(Roles = "admin")]
        public ActionResult Add()
        {
            return View();
        }

        // GET: Products
        [Authorize(Roles = "admin")]
        public ActionResult Overview()
        {
            return View(_productService.GetAllProducts());
        }

        // GET: Products
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null) 
                return View("Error");
            var product = _productService.GetProduct(id.GetValueOrDefault());
            return product == null ? (ActionResult) HttpNotFound() : View(product);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add([Bind(Include = "Name,Description,Category,Price")] Product product)
        {
            if (!ModelState.IsValid) 
                return View(product);
            var result = await _productService.AddProduct(product);
            if (result == null)
                return View(product);
            AddStatusMessage("Product " + result.Name + " was successfully added.");
            return View(new Product());
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Product product, HttpPostedFileBase productImage)
        {
            //Todo: Image is not saved + extension not specified
            if (productImage != null && productImage.ContentLength > 0)
            {
                var uploadPath = Server.MapPath("~/Images/Custom/") + Guid.NewGuid();
                productImage.SaveAs(uploadPath);
                var image = new Image(uploadPath, null);
                product.Image = image;
                await _productService.SaveProduct(product, image);
            }
            else
            {
                await _productService.SaveProduct(product);
            }

            AddStatusMessage("Product updated.");
            
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public PartialViewResult _SearchPartial(List<String> categories, int? minPrice, int? maxPrice, String searchText, int page)
        {
            //Todo: add max per page result checkbox
            var products = _productService.SearchProducts(categories, minPrice, maxPrice, searchText);
            return PartialView(products.ToPagedList(page, 10));        
        }

        [HttpPost]
        [AllowAnonymous]
        public void AddToBasket(int productId)
        {
            var cartProducts = (Session["cartProducts"] as HashSet<int>) ?? new HashSet<int>();
            cartProducts.Add(productId);
            Session["cartProducts"] = cartProducts;
        }

        private Dictionary<int, int> getPriceValues()
        {
            return new Dictionary<int, int>
            {
                { 0, 100 },
                { 100, 500 },
                { 500, 1000 }
            };
        }
    }
}