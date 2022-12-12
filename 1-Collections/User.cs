using System;

namespace Collections
{
    public class User : IUser
    {
        public User(string fullName, string username, uint? age)
        {
            if(username == null) throw new ArgumentException("Username must be defined");
            FullName = fullName;
            Username = username;
            Age = age;
        }
        
        public uint? Age { get; }
        
        public string FullName { get; }
        
        public string Username { get; }

        public bool IsAgeDefined => Age != null;
        
        public override string ToString() => $"{Username} {FullName} {Age}";

        public override bool Equals(object obj) => obj is User u && Equals(u);

        public bool Equals(User other) => Username.Equals(other.Username) && FullName.Equals(other.FullName) &&  Age.Equals(other.Age);
    }
}
