using MobiShare.Models;
using MobiShare.Services;
using MobiShare.Validators;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSession();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpClient();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICostoService, CostoService>();
builder.Services.AddScoped<IParcheggioService, ParcheggioService>();
builder.Services.AddScoped<ICartaPrepagataService, CartaPrepagataService>();
builder.Services.AddScoped<ICorsaService, CorsaService>();

builder.Services.AddScoped<IMezzoService, MezzoService>();
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
builder.Services.AddHttpClient<ApiClient>();

//Validator
builder.Services.AddScoped<IValidator<Utente>, UserValidator>();
builder.Services.AddScoped<IValidator<CartaCredito>, PaymentValidator>();

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
app.UseSession();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
