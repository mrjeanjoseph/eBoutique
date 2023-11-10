using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using YTP.Main.Controllers;
using YTP.Main.DataAccess;
using YTP.Main.Models;

namespace YTP.MainTest {
    [TestClass]
    public class AdminControllerTest {
        [TestMethod]
        public void CanChangeLoginName() {
            //Arrange (Set up a scenario)
            Member member = new Member() { LoginName = "lounapaniague" };
            FakeMembersRepository repoParam = new FakeMembersRepository();
            repoParam.Members.Add(member);
            AdminController target = new AdminController(repoParam);
            string oldLoginname = member.LoginName;
            string newLoginName = "lounajeanjoseph";

            //Act (Attempt the operation)
            target.ChangeLoginName(oldLoginname, newLoginName);

            //Assert (Verify the result)
            Assert.AreEqual(newLoginName, member.LoginName);
            Assert.IsTrue(repoParam.DidSubmitChanges);
        }
    }

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
