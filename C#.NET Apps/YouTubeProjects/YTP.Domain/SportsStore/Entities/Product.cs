namespace YTP.Domain.SportsStore.Entities {
    public class Product {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal ProductPrice { get; set; }
        public string Category { get; set; }
        public int IsAvailable { get; set; }
    }
}
