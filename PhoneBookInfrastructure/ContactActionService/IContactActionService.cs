using PhoneBookDomain;

public interface IContactActionService
{
     public string FindQuery();
     public Contact CreateContact();
     public void ShowContacts(List<Contact> contacts);
     public void ShowContact(Contact contact);
     public void SortByFullName();
     public string GetPropertyValue(string propertyInfoName, string propertyInfoValue);

     public void SaveToFile(List<Contact> contacts);
//          public void LoadFromFile();
}