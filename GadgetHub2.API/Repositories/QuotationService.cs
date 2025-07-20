using GadgetHub2.API.DTOs;


    namespace GadgetHub2.API.Repositories;

    public class QuotationService
    {
        private readonly HttpClient _httpClient;

        public QuotationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<QuotationResponseDto>> GetQuotationsAsync(QuotationRequestDto request)
        {
            var quotations = new List<QuotationResponseDto>();

            // URLs of distributor APIs
            var distributorApis = new[]
            {
            "https://localhost:7166/api/quotation/get-quotation", // TechWorld
            "https://localhost:7121/api/quotation/get-quotation", // ElectroCom
            "https://localhost:7196/api/quotation/get-quotation"  // Gadget Central
        };

            foreach (var apiUrl in distributorApis)
            {
                var response = await _httpClient.PostAsJsonAsync(apiUrl, request);

                if (response.IsSuccessStatusCode)
                {
                    var quote = await response.Content.ReadFromJsonAsync<QuotationResponseDto>();
                    if (quote != null)
                    {
                        // Add distributor name based on URL
                        quote.DistributorName = GetDistributorName(apiUrl);
                        quotations.Add(quote);
                    }
                }
            }

            return quotations;
        }

        private string GetDistributorName(string apiUrl)
        {
            if (apiUrl.Contains("7166")) return "TechWorld";
            if (apiUrl.Contains("7121")) return "ElectroCom";
            if (apiUrl.Contains("7196")) return "Gadget Central";
            return "Unknown";
        }
    }


