using YTP.Domain.SportsStore.Entities;

namespace YTP.Main.Areas.SportsStore.Models {
    public class CartIndex_VM {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}