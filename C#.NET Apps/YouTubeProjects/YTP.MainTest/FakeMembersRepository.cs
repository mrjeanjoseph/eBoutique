using System;
using System.Collections.Generic;
using System.Linq;
using YTP.Main.DataAccess;
using YTP.Main.Models;

namespace YTP.MainTest {
    public class FakeMembersRepository : IMembersRepository {

        public List<Member> Members = new List<Member>();
        public bool DidSubmitChanges = false;

        public void AddMember(Member member) {
            throw new NotImplementedException();
        }

        public Member FetchByLoginName(string loginName) {
            return Members.First(m => m.LoginName == loginName);
        }

        public void SubmitChanges() {
            DidSubmitChanges = true;
        }
    }
}
