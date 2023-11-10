using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTP.Main.Models;

namespace YTP.Main.DataAccess {
    public interface IMembersRepository {
        void AddMember(Member member);
        Member FetchByLoginName(string loginName);
        void SubmitChanges();
    }
}
