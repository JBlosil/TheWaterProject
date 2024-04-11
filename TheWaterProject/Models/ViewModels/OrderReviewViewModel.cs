namespace TheWaterProject.Models.ViewModels;

    public class OrderReviewViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
        public PaginationInfo PaginationInfo { get; set; }
    }

