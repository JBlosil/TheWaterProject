namespace TheWaterProject.Models.ViewModels;

    public class OrderReviewViewModel
    {
        public IEnumerable<OrderDetailsViewModel> Orders { get; set; }
        public PaginationInfo PaginationInfo { get; set; }
        
        public class OrderDetailsViewModel
        {  
            public int TransactionId { get; set; }
            public string OrderStatus { get; set; }
            public string? CustomerName { get; set; }
            public string? Date { get; set; }
            public int? Amount { get; set; }
            public string? TransactionCountry { get; set; }
            public string? ShippingAddress { get; set; }
            public int? Fraud { get; set; }
            // Other properties related to an Order
        }
    }

