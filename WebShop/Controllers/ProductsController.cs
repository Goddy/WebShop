using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;
using WebShop.App_GlobalResources;
using WebShop.Models;
using WebShop.Services;
using Image = WebShop.Models.Image;

namespace WebShop.Controllers
{
    public class ProductsController : AbstractController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService, ApplicationUserManager accountService)
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
        [Authorize(Roles = "Admin, Assistent")]
        public ActionResult Add()
        {
            AddData("action", "add");
            return View();
        }

        // GET: Products
        [Authorize(Roles = "Admin, Assistent")]
        public ActionResult Overview()
        {
            return View(_productService.GetAllProducts());
        }
        
        // GET: Products
        [Authorize(Roles = "Admin, Assistent")]
        public ActionResult AdminDetail(int? id)
        {
            if (id == null)
                return View("Error");
            var product = _productService.GetProduct(id.GetValueOrDefault());
            return product == null ? (ActionResult)HttpNotFound() : View(product);
        }
        
        // GET: Products
        [AllowAnonymous]
        public ActionResult ProductDetail(int? id)
        {
            if (id == null)
                return View("Error");
            var product = _productService.GetProduct(id.GetValueOrDefault());
            return product == null ? (ActionResult)HttpNotFound() : View(product);
        }
        
        // GET: Products
        [Authorize(Roles = "Admin, Assistent")]
        public async Task<ViewResult> Delete(int? id)
        {
            if (id == null)
                return View("Error");
            var product = await _productService.DeleteProduct((int)id);
            AddStatusMessage((product != null)? "Successfully removed " + product.Name : "Unable to remove product");
            return View("Overview", _productService.GetAllProducts());
        }

        // GET: Products
        [Authorize(Roles = "Admin, Assistent")]
        public ActionResult Edit(int? id)
        {
            if (id == null) 
                return View("Error");
            AddData("action", "edit");
            var product = _productService.GetProduct(id.GetValueOrDefault());
            return product == null ? (ActionResult) HttpNotFound() : View(product);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Assistent")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add([Bind(Include = "Name,Description,Category,Price")] Product product, HttpPostedFileBase productImage)
        {
            AddData("action", "add");
            if (!ModelState.IsValid)
                return View(product);
            Product result;
            if (productImage != null)
            {
                var response = HandleImage(productImage, View());
                if (response.Item2 != null)
                    return response.Item2;
                result = await _productService.AddProduct(product, response.Item1);
            }
            else
            {
                result = await _productService.AddProduct(product);
            }

            if (result == null)
                return View(product);
            AddStatusMessage("Successfully added " + result.Name);
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Assistent")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Product product, HttpPostedFileBase productImage)
        {
            AddData("action", "edit");
            if (productImage != null && productImage.ContentLength > 0)
            {
                var response = HandleImage(productImage, View());
                if (response.Item2 != null)
                    return response.Item2;
                await _productService.SaveProduct(product, response.Item1);
            }
            else
            {
                await _productService.SaveProduct(product);
            }
            AddStatusMessage(product.Name + " was successfully updated.");
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
                { 0, 50 },
                { 50, 100 },
                { 100, 500 }
            };
        }

        private Tuple<Image, ViewResult> HandleImage(HttpPostedFileBase productImage, ViewResult view)
        {
            var supportedTypes = new[] { "jpg", "jpeg", "png" };
            var extension = System.IO.Path.GetExtension(productImage.FileName);
            if (extension != null)
            {
                var fileExt = extension.Substring(1);

                if (!supportedTypes.Contains(fileExt))
                {
                    ModelState.AddModelError("photo", Locale.Unsupported_Image);
                    return new Tuple<Image, ViewResult>(null, view);
                }
            }
            else
            {
                ModelState.AddModelError("photo", Locale.Unsupported_Image);
                return new Tuple<Image, ViewResult>(null, view);  
            }
            var fileName = Guid.NewGuid() + extension;
            var uploadPath = Server.MapPath("~/Images/Custom/") + fileName;
            productImage.SaveAs(uploadPath);
            return new Tuple<Image, ViewResult>(new Image("~/Images/Custom/" + fileName, null), null);
        }
    }
}