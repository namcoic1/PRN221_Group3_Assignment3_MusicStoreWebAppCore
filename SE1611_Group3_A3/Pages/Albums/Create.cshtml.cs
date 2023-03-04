using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using SE1611_Group3_A3.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace SE1611_Group3_A3.Pages.Albums
{
    public class CreateModel : PageModel
    {
        private readonly SE1611_Group3_A3.Models.MusicStoreContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;


        public CreateModel(SE1611_Group3_A3.Models.MusicStoreContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _hostEnvironment = webHostEnvironment;
        }






        [Required(ErrorMessage = "Must choose at least one file")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "png,jpg,jpeg,gif")]
        [Display(Name = "Choose a file to upload")]
        [BindProperty]
        public IFormFile FileUpload { get; set; }
        [BindProperty]
        public Album Album { get; set; }

        public IActionResult OnGet()
        {
            ViewData["ArtistName"] = new SelectList(_context.Artists, "ArtistId", "Name");
            ViewData["GenreName"] = new SelectList(_context.Genres, "GenreId", "Name");
            return Page();
        }
        



        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            if (FileUpload != null)
            {
                
                    var file = Path.Combine(_hostEnvironment.WebRootPath, "Images", FileUpload.FileName);
                    using (var fileStream = new FileStream(file, FileMode.Create))
                    {
                        await FileUpload.CopyToAsync(fileStream);
                        
                    }                             
                    Album.AlbumUrl = "/Images/" + FileUpload.FileName;
            }
            

            

            _context.Albums.Add(Album);
            await _context.SaveChangesAsync();


            return RedirectToPage("./Index");
        }
    }
}
