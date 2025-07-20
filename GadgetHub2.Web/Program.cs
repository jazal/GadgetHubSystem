using GadgetHub2.Web.Services;

var builder = WebApplication.CreateBuilder(args);


//Configure http Client API acces
builder.Services.AddHttpClient("GadgetHubAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7003"); //API port here
});
builder.Services.AddHttpClient<DistributorProductService>();
builder.Services.AddHttpClient<DistributorProductService>();

// Add services to the container.
builder.Services.AddRazorPages();

//Services
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<DistributorProductService>();

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
