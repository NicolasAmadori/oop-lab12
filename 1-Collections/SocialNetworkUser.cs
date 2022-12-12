using System;
using System.Collections.Generic;

namespace Collections
{
    public class SocialNetworkUser<TUser> : User, ISocialNetworkUser<TUser>
        where TUser : IUser
    {
        private IDictionary<string, List<TUser>> followed = new Dictionary<string, List<TUser>>();
        public SocialNetworkUser(string fullName, string username, uint? age) : base(fullName, username, age){ }

        public bool AddFollowedUser(string group, TUser user)
        {
            if(followed.TryAdd(group, new List<TUser> {user}))
                return true;

            if(followed[group].Contains(user))
                return false;            

            followed[group].Add(user);
            return true;
        }

        public IList<TUser> FollowedUsers
        {
            get
            {
                var followedUsers = new List<TUser>();
                foreach(List<TUser> l in followed.Values)
                    followedUsers.AddRange(l);
                return followedUsers;
            }
        }

        public ICollection<TUser> GetFollowedUsersInGroup(string group)
        {
            if(!followed.ContainsKey(group)) return new List<TUser>();
            else return followed[group];
        }
    }
}
