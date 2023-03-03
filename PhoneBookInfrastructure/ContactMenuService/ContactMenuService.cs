using PhoneBookDomain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookInfrastructure
{
     public class ContactMenuService : IContactMenuService
     {
          private readonly int[] availableMenuActions = new[] { 1, 2, 3, 4, 5, 6, 0 };
          private string showMenuNameActions = "\n1. Найти контакт" +
                                   "\n2. Добавить контакт" +
                                   "\n3. Показать список контактов" +
                                   "\n4. Отсортировать по ФИО" +
                                   "\n5. Сохранить список в файл" +
                                   "\n6. Загрузить список из файла" +
                                   "\n0. Выход\n";

          private string showContactMenuNameActions = "\n1. Изменить контакт" +
                                   "\n2. Удалить контакт" +
                                   "\n3. Экспортировать контакт" +
                                   "\n0. Выход\n";
          private string showPropertyNameActions = "\n1. Добавить значение" +
            "\n2. Изменить значение" +
            "\n3. Удалить значение" +
            "\n0. Выход\n";
          private string showEditProperties = "\nИзменить свойство контакта:\n1. Имя" +
            "\n2. Телефон" +
            "\n3. Почта" +
            "\n4. Группа" +
            "\n0. Выход\n";

          private string showMenuFindInfoAction = "\nВыберите контакт для редактирования или для возрата в главное меню введите 0";
          private string titleMenuSelection = "Выберите пункт меню:";
          private void ShowAvailableActions(string menuSelection)
          {
               Console.WriteLine($"\n{titleMenuSelection}{menuSelection}");
          }
          public static TEnum ReadUserInput<TEnum>() where TEnum : struct
          {
               var enums = Enum.GetValues(typeof(TEnum));
               TEnum resultInputType = default(TEnum);
               bool enumParseResult = false;

               while (!enumParseResult)
               {
                    Console.WriteLine("Введите выбранный пункт меню: ");
                    ConsoleKeyInfo userInput = Console.ReadKey();
                    enumParseResult = Enum.TryParse(userInput.KeyChar.ToString(), true, out resultInputType);

                    if (!enumParseResult)
                    {
                         Console.WriteLine($"Для выбора доступны значения: {string.Join(",", enums)}. ");
                    }
               }
               return resultInputType;
          }
          public void ShowAvailableMenuActions()
          {
               ShowAvailableActions(showMenuNameActions);
          }
          public void ShowAvailableMenuContactActions()
          {
               ShowAvailableActions(showContactMenuNameActions);
          }
          public void ShowAvailableMenuFindAction()
          {
               Console.WriteLine(showMenuFindInfoAction);
          }
          public void ShowAvailablePropertyActions()
          {
               Console.WriteLine(showPropertyNameActions);
          }
          public void ShowAvailableEditPropertyActions()
          {
               Console.WriteLine(showEditProperties);
          }
          public MenuSelection ReadUserInputMain()
          {
               return ReadUserInput<MenuSelection>();
          }
          public PropertySelection ReadUserInputProperty()
          {
               return ReadUserInput<PropertySelection>();
          }
          public ContactProperties ReadUserInputProperties()
          {
               return ReadUserInput<ContactProperties>();
          }
          public Contact? GetSelectionContact(List<Contact> contacts)
          {
               if (contacts == null || !contacts.Any())
                    return null;

               while (true)
               {
                    Console.WriteLine(showMenuFindInfoAction);
                    var userInputSelected = Console.ReadLine();

                    if (userInputSelected == "0")
                         return null;

                    int.TryParse(userInputSelected, out int selectedNum);

                    if (selectedNum != 0 && (selectedNum - 1) < contacts.Count)
                    {
                         return contacts[(selectedNum - 1)];
                    }
                    Console.WriteLine("Ошибка выбора контакта, повторите действие.");
               }
          }

          public ContactSelection ReadUserInputContact()
          {
               return ReadUserInput<ContactSelection>();
          }

          public string ReadUserInputProp()
          {
               Console.WriteLine("\nВведите значение:");
               return Console.ReadLine() ?? "";
          }
          public void ShowListProperties(List<string> resProps)
          {
            Console.Clear();
            Console.WriteLine("Список значений:");
            foreach (var item in resProps)
            {
             Console.WriteLine(item);   
            }
          }
     }
}
