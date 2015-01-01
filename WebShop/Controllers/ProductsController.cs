using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;
using WebShop.Models;
using WebShop.Services;

namespace WebShop.Controllers
{
    public class ProductsController : Controller
    {
        private IProductService _productService;
        public ProductsController() {}

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        // GET: Products
        public ActionResult Index()
        {
            ViewData["Categories"] = _productService.GetAllCategories();
            ViewData["Price"] = getPriceValues();
            return View(_productService.GetAllProducts());
        }

        // GET: Products
        public ActionResult Add()
        {
            return View();
        }

        // GET: Products
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                Product product = _productService.GetProduct(id.GetValueOrDefault());
                return product == null ? (ActionResult) HttpNotFound() : View(product);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add([Bind(Include = "Name,Description,Category,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.AddProduct(product);
                if (result != null)
                {
                    ViewBag.StatusMessage = "Success";
                    return View(new Product());
                }
            }

            // If we got this far, something failed, redisplay form
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Product product, HttpPostedFileBase productImage)
        {
            //Todo: Does not work for the images. Add extension and make sure image path is saved.
            if (productImage != null && productImage.ContentLength > 0)
            {
                var uploadPath = Server.MapPath("~/Images/Custom/") + Guid.NewGuid();
                productImage.SaveAs(uploadPath);
                var images = product.Images ?? new List<Image>();
                images.Add(new Image(uploadPath, null));
                product.Images = images;
            }
            var result = await _productService.SaveProduct(product);
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public PartialViewResult Search(List<String> categories, int? minPrice, int? maxPrice, String searchText, int page)
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