using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PhoneBookDomain
{
    public class Contact
    {
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public List<string> Tels { get; set; }
        public List<string> Mails { get; set; }
        public List<string> Groups { get; set; }

        public Contact() { 
            Id =Guid.NewGuid();
            Tels = new List<string>();
            Mails = new List<string>();
            Groups = new List<string>();
            BirthDate= DateTime.Now;
        }
    }
}
