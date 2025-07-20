    using GadgetHub2.Web.Models;
    using GadgetHub2.Web.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadgetHub2.Web.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly ProductService _distributorProductService;

        public IndexModel(ProductService ProductService)
        {
            _distributorProductService = ProductService;
        }

        public List<ProductDto> Products { get; set; }

        public async Task OnGetAsync(ProductService productService)
        {
            var products = await productService.GetAllAsync();
            Products = await ProductService.GetAllDistributorProductsAsync(products);
        }
    }
}