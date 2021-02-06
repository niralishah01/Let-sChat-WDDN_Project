using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApplication.Models
{
    public class SQLFriendRepository : IFriendRepository
    {
        private readonly AppDbContext context;
        public SQLFriendRepository(AppDbContext context)
        {
            this.context = context;
        }
        friend IFriendRepository.Add(friend friend)
        {

            context.friends.Add(friend);
            context.SaveChanges();
            return friend;
        }

        public IEnumerable<friend> GetAllFriends()
        {
            return context.friends;
        }

        public friend GetFriend(int Id)
        {
            return context.friends.Find(Id);
        }
        friend IFriendRepository.Delete(int Id)
        {
            friend f = context.friends.Find(Id);
            if (f != null)
            {
                context.friends.Remove(f);
                context.SaveChanges();
            }
            return f;
            //throw new NotImplementedException();
        }
    }
}
