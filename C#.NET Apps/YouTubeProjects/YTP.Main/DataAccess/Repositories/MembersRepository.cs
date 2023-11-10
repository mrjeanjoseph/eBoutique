using System;
using YTP.Main.Models;

namespace YTP.Main.DataAccess {
    public class MembersRepository : IMembersRepository {
        public void AddMember(Member member) {
            // Implement moi
        }

        public void FetchByLoginName(string loginName) {
            // Implement moi
        }

        public void SubmitChanges() {
            // Implement moi
        }

        Member IMembersRepository.FetchByLoginName(string loginName) {
            throw new NotImplementedException();
        }
    }
}