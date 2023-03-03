using LiteDB;
using PhoneBookDomain;
using PhoneBookDomain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookInfrastructure
{
     public class ContactRepository : IContactRepository
     {
          private LiteDatabase _liteDb;

          public ContactRepository(ILiteDbContext liteDbContext)
          {
               _liteDb = liteDbContext.Database;
          }

          public Guid AddContact(Contact contact)
          {
               return _liteDb.GetCollection<Contact>("Contact").Insert(contact);
          }

          public List<Contact> AllContacts()
          {
               List<Contact> contacts = new List<Contact>();
               var response = _liteDb.GetCollection<Contact>("Contact").FindAll();


               if (response != null && response.Any())
               {
                    contacts.AddRange(response);
               }

               return contacts;
          }

          public bool UpdateContact(Contact contact)
          {
               return _liteDb.GetCollection<Contact>().Upsert(contact);
          }

          public bool DeleteContact(Guid id)
          {
               return _liteDb.GetCollection<Contact>().Delete(id);
          }

          public List<Contact> FindContact(string? searchQuery)
          {
               var result = new List<Contact>();

               if (!string.IsNullOrEmpty(searchQuery))
               {
                    var res =_liteDb.GetCollection<Contact>();
                    var findQuery =$"";
                    var items =res.Query()
                    .Where($"$.FullName LIKE '%{searchQuery}%' OR ($.Mails[*] ANY LIKE '%{searchQuery}%') OR ($.Tels[*] ANY LIKE '%{searchQuery}%') OR ($.Groups[*] ANY LIKE '%{searchQuery}%')")
                    .ToArray();

                    return items.ToList();
               }

               return result;
          }

          // public  Contact GetContact(string searchQuery)
          // {
          //     return null;
          // }
     }
}
