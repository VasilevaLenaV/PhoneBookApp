using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookInfrastructure
{
    class ContactDto
    {
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Tels { get; set; }
        public string? Mails { get; set; }
        public string? Groups { get; set; }
    }
}
