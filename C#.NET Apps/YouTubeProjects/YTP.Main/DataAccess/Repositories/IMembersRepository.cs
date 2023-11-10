using YTP.Main.Models;

namespace YTP.Main.DataAccess {
    public interface IMembersRepository {
        void AddMember(Member member);
        Member FetchByLoginName(string loginName);
        void SubmitChanges();
    }
}
