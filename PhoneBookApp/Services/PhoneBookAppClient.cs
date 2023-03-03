using PhoneBookDomain;
using PhoneBookDomain.Repository;
using PhoneBookInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookApp.Services
{
     public class PhoneBookAppClient
     {
          private IContactMenuService _contactMenuService;
          private IContactActionService _contactActionService;
          private IContactRepository _repository;
          private bool applicationRunning = true;
          public PhoneBookAppClient(IContactMenuService contactMenuService, IContactActionService contactActionService, IContactRepository repository)
          {
               _contactMenuService = contactMenuService;
               _contactActionService = contactActionService;
               _repository = repository;
          }
          public void Start()
          {
               while (applicationRunning)
               {
                    _contactMenuService.ShowAvailableMenuActions();
                    MenuSelection action = _contactMenuService.ReadUserInputMain();

                    MainRunAction(action);

                    // if (action != MenuSelection.Exit)
                    // {
                    //      Console.WriteLine("\nНажмите любую клавишу чтобы продолжить ..");
                    //      Console.ReadKey();
                    //      Console.Clear();
                    // }
               }
          }
          private void MainRunAction(MenuSelection command)
          {
               switch (command)
               {
                    case MenuSelection.FindContact:
                         FindAction();
                         break;
                    case MenuSelection.AddContact:
                         var contact = _contactActionService.CreateContact();
                         var documentId = _repository.AddContact(contact);
                         if (documentId != Guid.Empty)
                         {
                              Console.WriteLine("\nКонтакт успешно добавлен");
                         }
                         break;
                    case MenuSelection.DisplayAllContact:
                         var allContacts = _repository.AllContacts();
                         _contactActionService.ShowContacts(allContacts);

                         break;
                    case MenuSelection.SortByFullName:
                         break;
                    case MenuSelection.SaveToFile:
                         var contacts = _repository.AllContacts();
                         _contactActionService.SaveToFile(contacts);
                         break;
                    case MenuSelection.LoadFromFile:
                         break;
                    case MenuSelection.Exit:
                         applicationRunning = false;
                         break;
                    default:
                         break;
               }
          }
          private void FindAction()
          {
               var findQuery = _contactActionService.FindQuery();
               var findContacts = _repository.FindContact(findQuery);
               _contactActionService.ShowContacts(findContacts);

               var contactSelected = _contactMenuService.GetSelectionContact(findContacts);
               if (contactSelected == null)
                    return;

               _contactActionService.ShowContact(contactSelected);
               _contactMenuService.ShowAvailableMenuContactActions();
               ContactSelection action = _contactMenuService.ReadUserInputContact();
               RunContactAction(action, contactSelected);
          }
          private void RunContactAction(ContactSelection command, Contact contact)
          {
               switch (command)
               {
                    case ContactSelection.EditContact:
                         ChangeAction(contact);
                         break;
                    case ContactSelection.RemoveContact:
                         _repository.DeleteContact(contact.Id);
                         break;
                    case ContactSelection.Exit:
                         Console.Clear();
                         break;
                    default:
                         break;
               }
          }
          private (int?, string?) GetPropertyValue(List<string> values)
          {
               Console.Clear();
               foreach (var item in values.Select((item, index) => (item, index)))
               {
                    Console.WriteLine($"{item.index + 1}. {item.item}");
               }
               Console.WriteLine("0. Выход");

               while (true)
               {
                    Console.WriteLine("Выберите значение:");
                    var userInputSelected = Console.ReadLine();

                    if (userInputSelected == "0")
                         return (0, null);

                    int.TryParse(userInputSelected, out int selectedNum);
                    var propIndex = selectedNum - 1;

                    if (selectedNum != 0 && (propIndex) < values.Count)
                    {
                         return (propIndex, values[propIndex]);
                    }
                    else
                    {
                         Console.WriteLine("Ошибка выбора значения, повторите действие.");
                    }
               }
          }
          private void ChangeAction(Contact contact)
          {
               while (true)
               {
                    _contactMenuService.ShowAvailableEditPropertyActions();
                    ContactProperties userInputProps = _contactMenuService.ReadUserInputProperties();

                    switch (userInputProps)
                    {
                         case ContactProperties.FullName:
                              var userInput = _contactActionService.GetPropertyValue("Имя", contact.FullName ?? "");
                              if (userInput != null && userInput.Trim() != "")
                                   contact.FullName = userInput;
                              break;
                         case ContactProperties.Tels:
                              contact.Tels = SetProperty(contact.Tels);
                              break;
                         case ContactProperties.Mails:
                              contact.Mails = SetProperty(contact.Mails);
                              break;
                         case ContactProperties.Groups:
                              contact.Groups = SetProperty(contact.Groups);
                              break;
                         case ContactProperties.Exit:
                              _repository.UpdateContact(contact);
                              return;
                    }
               }
          }
          private List<string> SetProperty(List<string> props)
          {
               var resProps = props;
               (int?, string?) propValue = new(null, null);

               while (true)
               {
                    _contactMenuService?.ShowListProperties(resProps);
                    _contactMenuService?.ShowAvailablePropertyActions();
                    var action = _contactMenuService?.ReadUserInputProperty() ?? PropertySelection.Exit;

                    if (action == PropertySelection.Exit)
                    {
                         return resProps;
                    }
                    else if (action != PropertySelection.Add)
                    {
                         propValue = GetPropertyValue(props);
                    }

                    resProps = RunPropertyChangeAction(action, resProps, propValue);
               }
          }
          private List<string> RunPropertyChangeAction(PropertySelection command, List<string> list, (int?, string?) value)
          {
               var result = list;
               var valueIndex = value.Item1.GetValueOrDefault();
               switch (command)
               {
                    case PropertySelection.Add:
                         var addUserInput = _contactMenuService.ReadUserInputProp();
                         if (!list.Contains(addUserInput))
                              list.Add(addUserInput);
                         break;
                    case PropertySelection.Replace:
                         var replaceUserInput = _contactMenuService.ReadUserInputProp();

                         if (!list.Contains(replaceUserInput))
                         {
                              result[valueIndex] = replaceUserInput;
                         }
                         else
                         {
                              Console.WriteLine("Значение уже содержится в списке");
                         }
                         break;
                    case PropertySelection.Remove:
                         list.RemoveAt(valueIndex);
                         break;
                    case PropertySelection.Exit:
                         Console.Clear();
                         break;
               }

               return result;
          }
          private void ViewMenuSelection()
          {
               while (applicationRunning)
               {
                    _contactMenuService.ShowAvailableMenuContactActions();
                    var action = _contactMenuService.ReadUserInputMain();


                    if (action != MenuSelection.Exit)
                    {
                         Console.WriteLine("\nНажмите любую клавишу чтобы продолжить ..");
                         Console.ReadKey();
                         Console.Clear();
                    }
               }

          }
     }
}
