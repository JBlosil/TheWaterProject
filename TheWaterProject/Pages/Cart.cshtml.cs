using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheWaterProject.Infrastructrue;
using TheWaterProject.Models;

namespace TheWaterProject.Pages
{
    public class CartModel : PageModel
    {
        private IIntexRepository _repo;
        
        public CartModel(IIntexRepository temp, Cart cartService)
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

        public IActionResult OnPost(int product_ID, string returnUrl)
        {
            Product product = _repo.Products
                .FirstOrDefault(x => x.product_ID == product_ID);

            if (product != null)
            {
                Cart.AddItem(product, 1); 
            }

            return RedirectToPage(new { returnUrl = returnUrl });
        }

        public IActionResult OnPostRemove(int product_ID, string returnUrl)
        {
             Cart.RemoveLine(Cart.Lines.First(x => x.Product.product_ID == product_ID).Product);
             return RedirectToPage(new { returnUrl = returnUrl });
        }
    }
}

