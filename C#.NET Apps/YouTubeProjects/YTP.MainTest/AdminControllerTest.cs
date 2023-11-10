using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using YTP.Main.DataAccess;
using YTP.Main.Models;

namespace YTP.MainTest {
    [TestClass]
    public class AdminControllerTest {
        [TestMethod]
        public void CanChangeLoginName() {
            //Arrange (Set up a scenario)
            Member member = new Member() { LoginName = "Louna" };
            FakeMembersRepository repoParam = new FakeMembersRepository();
            repoParam.member
        }
    }

    public class FakeMembersRepository : IMembersRepository {
        public void AddMember(Member member) {
            throw new NotImplementedException();
        }

        public Member FetchByLoginName(string loginName) {
            throw new NotImplementedException();
        }

        public void SubmitChanges() {
            throw new NotImplementedException();
        }
    }
}
