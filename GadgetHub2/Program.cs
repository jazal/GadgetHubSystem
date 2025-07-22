using GadgetHub2.WEB.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//Services

builder.Services.AddHttpClient("GadgetHubAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7003/api/"); //  API base URL
});

builder.Services.AddHttpClient<UserService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7003/"); // My API URL
});

builder.Services.AddHttpClient<ProductService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7003/"); // or your API base URL
});

builder.Services.AddHttpClient<RegisterService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7003/"); // or your API base URL
});

builder.Services.AddHttpClient<CartService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7003/"); // or your API base URL
});

builder.Services.AddHttpClient<QuotationService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7003/"); // or your API base URL
});

//builder.Services.AddHttpClient<LoginService>(client =>
//{
//    client.BaseAddress = new Uri("https://localhost:7003/"); // or your API base URL
//});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
