using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheWaterProject.Infrastructrue;
using TheWaterProject.Models;

namespace TheWaterProject.Pages
{
    public class CartModel : PageModel
    {
        private IWaterRepository _repo;
        
        public CartModel(IWaterRepository temp, Cart cartService)
        {
            _repo = temp;
            Cart = cartService;
        }
        
        public Cart Cart { get; set; }

        public string ReturnUrl { get; set; } = "/";

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }

        public IActionResult OnPost(int projectId, string returnUrl)
        {
            Project proj = _repo.Projects
                .FirstOrDefault(x => x.ProjectId == projectId);

            if (proj != null)
            {
                Cart.AddItem(proj, 1); 
            }

            return RedirectToPage(new { returnUrl = returnUrl });
        }

        public IActionResult OnPostRemove(int projectId, string returnUrl)
        {
             Cart.RemoveLine(Cart.Lines.First(x => x.Project.ProjectId == projectId).Project);
             return RedirectToPage(new { returnUrl = returnUrl });
        }
    }
}

