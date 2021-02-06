using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApplication.Models
{
    public interface IFriendRepository
    {
        friend GetFriend(int Id);
        IEnumerable<friend> GetAllFriends();
        friend Add(friend friend);
        friend Delete(int id);
    }
}
