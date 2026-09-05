using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ---------------------------------------------------------
// File: Contact.cs
// Author: Pratig Thapa Magar (provided solution)
// Date: 2025-11-30
// Description:
//   Represents a single contact record for the Mini Contact
//   Manager assignment. Simple business object with properties.
// ---------------------------------------------------------

namespace Assignment4_ContactManagar
{
    /// <summary>
    /// Contact represents a person in the user's address book.
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// Unique identifier for the contact when stored in the database.
        /// This matches the Id column in dbo.Contacts.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Contact's first name. Required.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Contact's last name. Required.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Contact's email address. Required and must be valid format.
        /// </summary>
        /// 
        public string Phone { get; set; }

        /// <summary>
        /// Category (Friend, Family, Work, etc.). Required.
        /// </summary>
        /// 
        public string Email { get; set; }

        /// <summary>
        /// Contact's phone number. Optional; validated if present.
        /// </summary>

        public string Category { get; set; }

        /// <summary>
        /// Default constructor important for serializers and data binding.
        /// </summary>
        public Contact()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            Category = string.Empty;
        }

        /// <summary>
        /// Convenience constructor to quickly create a Contact with values.
        /// </summary>
        public Contact(int id, string firstName, string lastName,  string phone, string email, string category)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Email = email;
            Category = category;
        }

        /// <summary>
        /// Friendly representation used for debugging.
        /// </summary>
        public override string ToString()
        {
            return $"{LastName}, {FirstName} ({Category})";
        }
    }
}

