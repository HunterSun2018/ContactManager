﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager.Models
{
    public class Contact
    {
        public int Id { get; set; }

        // user ID from AspNetUser table
        public string OwnerID { get; set; }
        
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Email { get; set; }

        public ContactStatus Status { get; set; }

    }

    public enum ContactStatus
    {
        Submitted,
        Approved,
        Rejected
    }
}
