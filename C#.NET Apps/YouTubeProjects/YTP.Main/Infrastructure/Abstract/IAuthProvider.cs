

namespace YTP.Main.Infrastructure.Abstract {
    internal interface IAuthProvider {
        bool Authenticate(string username, string password);
    }
}
