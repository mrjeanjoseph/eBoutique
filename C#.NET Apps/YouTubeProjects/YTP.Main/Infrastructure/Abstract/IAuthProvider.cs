

namespace YTP.Main.Infrastructure.Abstract {
    public interface IAuthProvider {
        bool Authenticate(string username, string password);
    }
}
