using ADO.NETTest;
using Microsoft.Data.SqlClient;
using System.Data;

class Program
{
    static Manager manager;

    public enum Commands
    {
        stop,
        add,
        delete,
        update,
        show
    }

    static void Main(string[] args)
    {

        manager.Connect();

        static void Update()
        {
            Console.WriteLine("Введите логин для обновления:");

            var login = Console.ReadLine();

            Console.WriteLine("Введите имя для обновления:");
            var name = Console.ReadLine();

            var count = manager.UpdateUserByLogin(login, name);

            Console.WriteLine("Строк обновлено" + count);

            manager.ShowData();
        }

        static void Add()
        {
            Console.WriteLine("Введите логин для добавления:");

            var login = Console.ReadLine();

            Console.WriteLine("Введите имя для добавления:");
            var name = Console.ReadLine();

            manager.AddUser(login, name);

            manager.ShowData();
        }

        static void Delete()
        {
            Console.WriteLine("Введите логин для удаления:");

            var count = manager.DeleteUserByLogin(Console.ReadLine());

            Console.WriteLine("Количество удаленных строк " + count);

            manager.ShowData();
        }

        Console.WriteLine("Список команд для работы консоли:");
        Console.WriteLine(Commands.stop + ": прекращение работы");
        Console.WriteLine(Commands.add + ": добавление данных");
        Console.WriteLine(Commands.delete + ": удаление данных");
        Console.WriteLine(Commands.update + ": обновление данных");
        Console.WriteLine(Commands.show + ": просмотр данных");

        string command;

        command = Console.ReadLine();

        switch (command)
        {
            case
            nameof(Commands.add):
                {
                    Add();
                    break;
                }

            case
            nameof(Commands.delete):
                {
                    Delete();
                    break;
                }
            case
            nameof(Commands.update):
                {
                    Update();
                    break;
                }
            case
            nameof(Commands.show):
                {
                    manager.ShowData();
                    break;
                }

        }
    }
}