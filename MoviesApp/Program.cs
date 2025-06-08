using MoviesApp.Data;
using MoviesApp.Models;

User? currentUser = null;
bool running = true;

while (running)
{
    Console.Clear();
    Console.WriteLine("==== MAIN MENU ====");
    Console.WriteLine("1. Register");
    Console.WriteLine("2. Login");
    Console.WriteLine("3. View users");
    Console.WriteLine("0. Exit");
    Console.Write("Choose: ");

    switch (Console.ReadLine())
    {
        case "1":
            RegisterUser();
            break;
        case "2":
            LoginUser();
            break;
        case "3":
            ViewUsers();
            break;
        case "0":
            running = false;
            break;
        default:
            Console.WriteLine("Unknown option. Press Enter...");
            Console.ReadLine();
            break;
    }

    if (currentUser != null)
    {
        ShowUserMenu();
    }
}

void RegisterUser()
{
    Console.Clear();
    Console.WriteLine("== Registration ==");

    Console.Write("Username: ");
    string username = Console.ReadLine();

    Console.Write("Login: ");
    string login = Console.ReadLine();

    Console.Write("Password: ");
    string password = Console.ReadLine();

    using (var db = new MoviesContext())
    {
        var user = new User { Username = username, Login = login, Password = password };
        db.Users.Add(user);
        db.SaveChanges();
    }

    Console.WriteLine("User registered. Press Enter...");
    Console.ReadLine();
}

void LoginUser()
{
    Console.Clear();
    Console.WriteLine("== Login ==");

    Console.Write("Login: ");
    string login = Console.ReadLine();

    Console.Write("Password: ");
    string password = Console.ReadLine();

    using (var db = new MoviesContext())
    {
        currentUser = db.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
    }

    if (currentUser != null)
    {
        Console.WriteLine("Login successful. Press Enter...");
    }
    else
    {
        Console.WriteLine("Wrong login or password. Press Enter...");
    }
    Console.ReadLine();
}

void ViewUsers()
{
    Console.Clear();
    Console.WriteLine("== Users ==");

    using (var db = new MoviesContext())
    {
        var users = db.Users.ToList();
        foreach (var user in users)
        {
            Console.WriteLine($"ID: {user.Id}, Username: {user.Username}, Login: {user.Login}");
        }
    }

    Console.WriteLine("Press Enter to go back...");
    Console.ReadLine();
}

void ShowUserMenu()
{
    bool loggedIn = true;

    while (loggedIn)
    {
        Console.Clear();
        Console.WriteLine($"=== User Menu: {currentUser.Username} ===");
        Console.WriteLine("1. Add movie");
        Console.WriteLine("2. View my movies");
        Console.WriteLine("3. Delete movie");
        Console.WriteLine("4. Edit profile");
        Console.WriteLine("0. Logout");
        Console.Write("Choose: ");

        switch (Console.ReadLine())
        {
            case "1":
                AddTitle();
                break;
            case "2":
                ViewMyTitles();
                break;
            case "3":
                DeleteTitle();
                break;
            case "4":
                EditProfile();
                break;
            case "0":
                currentUser = null;
                loggedIn = false;
                break;
            default:
                Console.WriteLine("Unknown option. Press Enter...");
                Console.ReadLine();
                break;
        }
    }
}

void AddTitle()
{
    Console.Clear();
    Console.WriteLine("== Add Movie ==");

    Console.Write("Title name: ");
    string name = Console.ReadLine();

    Console.Write("Duration (minutes): ");
    int duration = int.Parse(Console.ReadLine() ?? "0");

    using (var db = new MoviesContext())
    {
        var title = new Title
        {
            Name = name,
            Duration = duration,
            UserId = currentUser.Id
        };
        db.Titles.Add(title);
        db.SaveChanges();
    }

    Console.WriteLine("Movie added. Press Enter...");
    Console.ReadLine();
}

void ViewMyTitles()
{
    Console.Clear();
    Console.WriteLine("== My Movies ==");

    using (var db = new MoviesContext())
    {
        var titles = db.Titles.Where(t => t.UserId == currentUser.Id).ToList();
        if (titles.Count == 0)
        {
            Console.WriteLine("You have no movies.");
        }
        else
        {
            foreach (var title in titles)
            {
                Console.WriteLine($"ID: {title.Id}, Name: {title.Name}, Duration: {title.Duration} min");
            }
        }
    }

    Console.WriteLine("Press Enter...");
    Console.ReadLine();
}

void DeleteTitle()
{
    Console.Clear();
    Console.WriteLine("== Delete Movie ==");

    ViewMyTitles();

    Console.Write("Enter movie ID to delete: ");
    int id = int.Parse(Console.ReadLine());

    using (var db = new MoviesContext())
    {
        var title = db.Titles.FirstOrDefault(t => t.Id == id && t.UserId == currentUser.Id);
        if (title != null)
        {
            db.Titles.Remove(title);
            db.SaveChanges();
            Console.WriteLine("Movie deleted.");
        }
        else
        {
            Console.WriteLine("Movie not found or not yours.");
        }
    }

    Console.WriteLine("Press Enter...");
    Console.ReadLine();
}

void EditProfile()
{
    Console.Clear();
    Console.WriteLine("== Edit Profile ==");

    Console.Write("New username (leave empty to skip): ");
    string username = Console.ReadLine();

    Console.Write("New login (leave empty): ");
    string login = Console.ReadLine();

    Console.Write("New password (leave empty): ");
    string password = Console.ReadLine();

    using (var db = new MoviesContext())
    {
        var user = db.Users.Find(currentUser.Id);
        if (!string.IsNullOrWhiteSpace(username)) user.Username = username;
        if (!string.IsNullOrWhiteSpace(login)) user.Login = login;
        if (!string.IsNullOrWhiteSpace(password)) user.Password = password;

        db.SaveChanges();
        currentUser = user;
    }

    Console.WriteLine("Profile updated. Press Enter...");
    Console.ReadLine();
}
