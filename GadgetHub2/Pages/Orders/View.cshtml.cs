using GadgetHub.Dtos.Distributors;
using GadgetHub.Dtos.Order;
using GadgetHub.Dtos.Quotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace GadgetHub.Web.Pages.Orders;

public class ViewModel : PageModel
{
    private readonly HttpClient _httpClient;

    public ViewModel(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("GadgetHubAPI");
    }

    [BindProperty(SupportsGet = true)]

    public int OrderId { get; set; }

    [BindProperty]
    public int SelectedQuotationId { get; set; }

    [BindProperty]
    public string SelectedApiUrlId { get; set; }

    [BindProperty]
    public string SelectedDistributorName { get; set; }

    [BindProperty]
    public List<ConfirmedQuotationItemDto> QuotationItems { get; set; } = new();

    public CustomerOrderDto Order { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var response = await _httpClient.GetAsync($"order/{OrderId}");

        if (!response.IsSuccessStatusCode)
            return RedirectToPage("/Orders/Index");

        var OrderDetails = await response.Content.ReadFromJsonAsync<List<CustomerOrderDto>>();

        Order = OrderDetails?.FirstOrDefault();

        await GetDistributorQuoutations();

        return Page();
    }

    public async Task<IActionResult> OnPostConfirmQuotation()
    {
        // Example: Call API to place order using quotation details
        var request = new AssignQuotationDto
        {
            OrderId = OrderId,
            QuotationId = SelectedQuotationId,
            ApiUrlId = SelectedApiUrlId,
            DistributorName = SelectedDistributorName,
            Items = QuotationItems
        };

        // Call your Quotations API
        var response = await AssignQuotation(SelectedQuotationId, SelectedApiUrlId, SelectedDistributorName, QuotationItems); ;

        if (response.IsSuccessStatusCode)
        {
            TempData["Success"] = "Order confirmed successfully!";
            return RedirectToPage("/Pages/Orders");
        }

        TempData["Error"] = "Failed to confirm order.";
        return Page();
    }

    private async Task GetDistributorQuoutations()
    {
        var response = await _httpClient.GetAsync($"Quotations/GetAllByOrderId/{OrderId}");

        if (response.IsSuccessStatusCode)
        {
            var quotations = await response.Content.ReadFromJsonAsync<List<QuotationsViewModel>>();

            ViewData["quotations"] = quotations;

            if (quotations.Count >= 3)
            {
                await AutomaticallyConfirmTheOrderByTheSystem(quotations);
            }
        }
    }

    private async Task<HttpResponseMessage> AssignQuotation(int selectedQuotationId, string selectedApiUrlId, string selectedDistributorName, List<ConfirmedQuotationItemDto> quotationItems)
    {
        var request = new AssignQuotationDto
        {
            OrderId = OrderId,
            QuotationId = selectedQuotationId,
            ApiUrlId = selectedApiUrlId,
            DistributorName = selectedDistributorName,
            Items = quotationItems
        };

        var response = await _httpClient.PostAsJsonAsync("Quotations/AssignQuotations", request);
        return response;
    }

    private async Task AutomaticallyConfirmTheOrderByTheSystem(List<QuotationsViewModel> input)
    {
        var lowestQuotation = input
            .OrderBy(q => q.QuotationItems.Sum(item => item.Quantity * item.Price))
            .FirstOrDefault();

        if (lowestQuotation is null)
            return;

        var items = lowestQuotation.QuotationItems.Select(x => new ConfirmedQuotationItemDto
        {
            ProductName = x.ProductName,
            Price = x.Price,
            Quantity = x.Quantity
        }).ToList();

        await AssignQuotation(lowestQuotation.Id, lowestQuotation.ApiUrl, lowestQuotation.CompanyName, items);
    }

}
