using MoviesApp.Data;
using MoviesApp.Models;

bool running = true;

while (running)
{
    Console.Clear();
    Console.WriteLine("==== ГОЛОВНЕ МЕНЮ ====");
    Console.WriteLine("1. Реєстрація користувача");
    Console.WriteLine("2. Перегляд користувачів");
    Console.WriteLine("0. Вихід");
    Console.Write("Ваш вибір: ");

    switch (Console.ReadLine())
    {
        case "1":
            RegisterUser();
            break;
        case "2":
            ViewUsers();
            break;
        case "0":
            running = false;
            break;
        default:
            Console.WriteLine("Невідомий вибір. Натисніть Enter...");
            Console.ReadLine();
            break;
    }
}

void RegisterUser()
{
    Console.Clear();
    Console.WriteLine("== Реєстрація ==");

    Console.Write("Ім'я: ");
    string name = Console.ReadLine();

    Console.Write("Логін: ");
    string login = Console.ReadLine();

    Console.Write("Пароль: ");
    string password = Console.ReadLine();

    using (var db = new MoviesContext())
    {
        var user = new User { Імя = name, Логін = login, Пароль = password };
        db.Users.Add(user);
        db.SaveChanges();
    }

    Console.WriteLine("Користувача зареєстровано. Натисніть Enter...");
    Console.ReadLine();
}

void ViewUsers()
{
    Console.Clear();
    Console.WriteLine("== Список користувачів ==");

    using (var db = new MoviesContext())
    {
        var users = db.Users.ToList();
        foreach (var user in users)
        {
            Console.WriteLine($"ID: {user.Id}, Ім’я: {user.Імя}, Логін: {user.Логін}");
        }
    }

    Console.WriteLine("Натисніть Enter для повернення...");
    Console.ReadLine();
}
