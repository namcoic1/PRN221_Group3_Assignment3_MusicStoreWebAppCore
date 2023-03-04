using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using SE1611_Group3_A3.Models;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace SE1611_Group3_A3.Pages.Login
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        [DataType(DataType.Text)]
        [Required]
        [Display(Prompt = "username")]
        public string username { get; set; }
        [BindProperty]
        [DataType(DataType.Password)]
        [Required]
        [Display(Prompt = "password")]
        public string password { get; set; }
        public string message { get; set; }
        public void OnGet()
        {
            
        }
        public IActionResult OnPost()
        {
            MusicStoreContext context = new MusicStoreContext();
            User u = context.Users.FirstOrDefault(u => u.UserName == username && u.Password == password);

            if (u != null)
            {
                //message = $"Login successfully with {u.UserName}!";
                var options = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore,
                };  
                string json = JsonConvert.SerializeObject(u, options);
                HttpContext.Session.SetString("user", json);
                //return RedirectToPage("/Index", message);
                return RedirectToPage("/Index");
            }
            else
            {
                message = "Username or password is incorrect!";
                return Page();
            }
        }
        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("user");
            return RedirectToPage("/Index");
        }
    }
}
