using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace _17870_LP2.Models
{
    class User: UserData
    {
        public static int totalCount;
        public User(string identityCard, string firstName, string lastName, string genre, int age, string contact, Address address)
        {
            Id = Interlocked.Increment(ref totalCount);
            IdentityCard = identityCard;
            FirstName = firstName;
            LastName = lastName;
            Genre = genre;
            Age = age;
            Contact = contact;
            Address = address;
        }

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   IdentityCard == user.IdentityCard;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdentityCard);
        }
    }
}
