using Microsoft.AspNetCore.Mvc.RazorPages;
using GadgetHub.Web.Models;
using GadgetHub.Web.Services;

namespace GadgetHub.Web.Pages.Distributors;

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
