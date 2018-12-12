using System;
using System.Collections.Generic;

namespace EntityDatabase.Models
{
    public partial class ContactUs
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zipcode { get; set; }
        public string EmailId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
