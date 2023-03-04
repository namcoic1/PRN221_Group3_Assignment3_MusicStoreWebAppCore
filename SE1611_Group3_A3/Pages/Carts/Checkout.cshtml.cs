using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SE1611_Group3_A3.Models;
using System.Diagnostics;

namespace SE1611_Group3_A3.Pages.Carts
{
    public class CheckoutModel : PageModel
    {
        [BindProperty]
        public Order Order { get; set; }
        public void OnGet()
        {
            User user = null;
            MusicStoreContext context = new MusicStoreContext();
            if (HttpContext.Session.GetString("user") != null)
            {
                string json = HttpContext.Session.GetString("user");
                user = JsonConvert.DeserializeObject<User>(json);
            }

            if (user != null)
            {
                decimal total = 0;
                List<Cart> list_cart = context.Carts.ToList();
                foreach (Cart item in list_cart)
                {
                    decimal price = context.Albums.FirstOrDefault(x => x.AlbumId == item.AlbumId).Price;
                    total += item.Count * price;
                }

                Order = new Order
                {
                    OrderDate = DateTime.Today,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Address = user.Address,
                    City = user.City,
                    State = user.State,
                    Country = user.Country,
                    Phone = user.Phone,
                    Email = user.Email,
                    Total = total
                };
                Debug.WriteLine("check");
            }

        }

        public void OnPost()
        {

            MusicStoreContext context = new MusicStoreContext();
            try
            {
                context.Orders.Add(Order);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                return;
            }
            int orderID = context.Orders.Select(o => o.OrderId).Max();

            // add order details
            List<Cart> list_cart = context.Carts.ToList();
            foreach (Cart item in list_cart)
            {
                decimal Price = context.Albums.FirstOrDefault(x => x.AlbumId == item.AlbumId).Price;
                OrderDetail orderDetail = new OrderDetail
                {
                    AlbumId = item.AlbumId,
                    OrderId = orderID,
                    UnitPrice = Price,
                    Quantity = item.Count
                };
                try
                {
                    context.OrderDetails.Add(orderDetail);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return;
                }
            }

            foreach (Cart cart in list_cart)
            {
                context.Carts.Remove(cart);
            }
            context.SaveChanges();
            ViewData["message"] = "success";
        }
    }
}
