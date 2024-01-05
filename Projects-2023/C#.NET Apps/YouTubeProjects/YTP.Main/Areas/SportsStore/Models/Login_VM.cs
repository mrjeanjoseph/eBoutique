using System.ComponentModel.DataAnnotations;

namespace YTP.Main.Areas.SportsStore.Models {
    public class Login_VM {

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}