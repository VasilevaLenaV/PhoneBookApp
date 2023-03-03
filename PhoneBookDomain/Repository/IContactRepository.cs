using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookDomain.Repository
{
    public interface IContactRepository
    {
        public Guid AddContact(Contact contact);
        public bool UpdateContact(Contact contact);
        public bool DeleteContact(Guid id);
        public List<Contact> FindContact(string? searchQuery);
        public List<Contact> AllContacts();

        //public Contact GetContact(string searchQuery);
    }
}
