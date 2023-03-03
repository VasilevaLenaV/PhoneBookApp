using System.Reflection;
using System.Text.Json;
using PhoneBookDomain;

public class ContactActionService : IContactActionService
{
     private readonly string[] fields = new[] { "FullName", "Tels", "Mails", "Groups" };

     public Contact CreateContact()
     {
          Contact contact = new Contact();
          Console.WriteLine("\n");
          foreach (PropertyInfo propertyInfo in contact.GetType()
                              .GetProperties(
                                      BindingFlags.Public
                                      | BindingFlags.Instance))
          {
               if (!fields.Contains(propertyInfo.Name))
                    continue;


               Console.WriteLine($"Введите {propertyInfo.Name}:");
               string? userInput = Console.ReadLine();

               if (userInput == null)
                    continue;

               switch (propertyInfo.Name)
               {
                    case "FullName":
                         contact.FullName = userInput;
                         break;
                    case "Tels":
                         contact.Tels.Add(userInput);
                         break;
                    case "Mails":
                         contact.Mails.Add(userInput);
                         break;
                    case "Groups":
                         contact.Groups.Add(userInput);
                         break;
                    default:
                         break;
               }

          }

          return contact;
     }
     public void ShowContacts(List<Contact> contacts)
     {
          Console.WriteLine("\nСписок контактов:");

          if (contacts.Count > 0)
          {
               foreach (var contact in contacts.Select((item, index) => (item, index)))
               {
                    Console.WriteLine($"\nId:{contact.index + 1}");
                    ShowInfoContact(contact.item);
               }

               return;
          }
          Console.Write(" Не содержит значений");
     }
     public void ShowContact(Contact contact)
     {
          Console.Clear();
          ShowInfoContact(contact);
     }
     public void SortByFullName()
     {
          throw new NotImplementedException();
     }

     private void ShowInfoContact(Contact contact)
     {
          Console.WriteLine($"Имя: {contact.FullName}, \nТелефон: {string.Join(", ", contact.Tels)}\nЭл. почта: {string.Join(", ", contact.Mails)}, \nГруппы: {string.Join(", ", contact.Groups)}");
     }

     public string FindQuery()
     {
          Console.Clear();
          Console.WriteLine("\nВведите данные для поиска контакта: ");
          return Console.ReadLine() ?? "";
     }

     public string GetPropertyValue(string propertyInfoName, string propertyInfoValue)
     {
          Console.WriteLine($"\nВведите {propertyInfoName}, (текущее значение = {propertyInfoValue}):");
          return Console.ReadLine() ?? "";
     }
     private string GetPropertyValue(string propertyInfoName, List<string> propertyInfoValue)
     {
          Console.WriteLine($"Введите {propertyInfoName}");

          return Console.ReadLine() ?? "";
     }
     public void SaveToFile(List<Contact> contacts){
          string json = JsonSerializer.Serialize(contacts);
          string path =Environment.CurrentDirectory + "\\export_contacts.json"; 
          File.WriteAllText(path, json);
     }
}