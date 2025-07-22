using GadgetHub2.WEB.Services;
using GadgetHub2.WEB.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadgetHub2.Web.Pages.Distributors;

public class IndexModel : PageModel
{
    private readonly UserService _service; // Or rename to UserService

    public IndexModel(UserService service)
    {
        _service = service;
    }

    public List<DistributorViewModel> Distributors { get; set; }

    public async Task OnGetAsync()
    {
        Distributors = await _service.GetDistributorsAsync();
    }
}
