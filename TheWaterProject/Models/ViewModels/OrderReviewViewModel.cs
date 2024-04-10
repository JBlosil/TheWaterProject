namespace TheWaterProject.Models.ViewModels;

    public class OrderReviewViewModel
    {
        public IQueryable<Order> Orders { get; set; }
        public PaginationInfo PaginationInfo { get; set; }
    }

