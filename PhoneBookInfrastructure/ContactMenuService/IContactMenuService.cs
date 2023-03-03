using PhoneBookDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookInfrastructure
{
     public interface IContactMenuService
     {
          public static T ReadUserInput<T>() where T : struct { return default(T); }
          public MenuSelection ReadUserInputMain();
          public ContactSelection ReadUserInputContact();
          public PropertySelection ReadUserInputProperty();
          public ContactProperties ReadUserInputProperties();
          public string ReadUserInputProp();
          public Contact? GetSelectionContact(List<Contact> contacts);
          public void ShowAvailableMenuContactActions();
          public void ShowAvailableMenuActions();
          public void ShowAvailableMenuFindAction();
          public void ShowAvailablePropertyActions();
          public void ShowAvailableEditPropertyActions();
          public void ShowListProperties(List<string> resProps);
     }
}
