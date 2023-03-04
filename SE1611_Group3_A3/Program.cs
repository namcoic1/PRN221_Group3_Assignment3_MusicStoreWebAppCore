using Microsoft.EntityFrameworkCore;
using SE1611_Group3_A3.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<MusicStoreContext>();
builder.Services.AddSession();

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

app.UseSession();

MusicStoreContext context = new MusicStoreContext();
List<Cart> list_cart = context.Carts.ToList();
foreach(Cart cart in list_cart)
{
    context.Carts.Remove(cart);
}
context.SaveChanges();

app.Run();


