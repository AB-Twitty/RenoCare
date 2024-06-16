using RenoCare.Domain.Chat;
using System;
using System.Collections.Generic;

namespace RenoCare.Core.Features.Chat.Dtos
{
    public class ContactDto
    {
        public string Name { get; set; }
        public string UserId { get; set; }
        public int ContactId { get; set; }
        public ChatMessage LastMsg { get; set; }
        public int UnreadMsgCount { get; set; }

    }

    public class ContactComparer : IEqualityComparer<ContactDto>
    {
        public bool Equals(ContactDto x, ContactDto y)
        {
            // Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y)) return true;

            // Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            // Check whether the contacts' properties are equal.
            return x.UserId == y.UserId; // Assuming Id is a unique identifier
        }

        public int GetHashCode(ContactDto contact)
        {
            // Check whether the object is null.
            if (Object.ReferenceEquals(contact, null)) return 0;

            // Get hash code for the Id field.
            return contact.UserId.GetHashCode();
        }
    }

}
