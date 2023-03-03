using PhoneBookDomain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookApp.Services
{
    public class ContactClient
    {
        private IContactRepository _repository;
        public ContactClient(IContactRepository repository) {
            _repository = repository;
        }
        public void FindContact() { }
        public void AddContact() { }
        public void RemoveContact() { }
        public void EditContact() { }
        public void DisplayAllContact() { }
    }
}
