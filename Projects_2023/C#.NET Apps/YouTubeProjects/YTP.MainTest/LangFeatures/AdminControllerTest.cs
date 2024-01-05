using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using YTP.Main.Controllers;
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

        [TestMethod]
        public void CanAddBid() {
            //Arrange - set up the scenario
            Item target = new Item();
            Member memberParam = new Member();
            Decimal amtParam = 150M;

            //Act - perform the test
            target.AddBid(memberParam, amtParam);

            //Assert - Check the behavior
            Assert.AreEqual(1, target.Bids.Count());
            Assert.AreEqual(amtParam, target.Bids[0].BidAmount);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CannotAddLowerBid() {
            //Arrange - set up the scenario
            Item target = new Item();
            Member memberParam = new Member();
            Decimal amtParam = 150M;

            //Act - perform the test
            target.AddBid(memberParam, amtParam);
            target.AddBid(memberParam, amtParam - 10);
        }


        [TestMethod]
        public void CanAddHigherBid() {
            //Arrange - set up the scenario
            Item target = new Item();
            Member firstMember = new Member();
            Member secondMember = new Member();
            Decimal amtParam = 150M;

            //Act - perform the test
            target.AddBid(firstMember, amtParam);
            target.AddBid(secondMember, amtParam + 10);

            //Assert - Check the behavior
            Assert.AreEqual(2, target.Bids.Count());
            Assert.AreEqual(amtParam + 10, target.Bids[1].BidAmount);
        }
    }
}
