using GadgetHub2.Web.Models;
using GadgetHub2.Web.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GadgetHub2.Web.Pages.Products
{
    public class DistributorProductsModel : PageModel
    {
        private readonly ProductService _ProductService;
        private readonly DistributorProductService _distributorproductService;

        public DistributorProductsModel(
            DistributorProductService distributorService,
            ProductService productService)
        {
            _ProductService = productService;
            _distributorproductService = distributorService;
        }

        public List<ProductDto> Products { get; set; } = new();

        public async Task OnGetAsync()
        {
            var distributors = await _distributorproductService.GetAllAsync();
            Products = await _distributorproductService.GetAllDistributorProductsAsync(distributors);
        }
    }
}
