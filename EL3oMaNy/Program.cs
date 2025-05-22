using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;

class LibrarySystem
{
    private static int arBorrowedBooks = 0;
    private static int enBorrowedBooks = 0;
    private static List<string> EnglishBooks = new List<string> { "The Push", "Bride Read", "The Hedge", "It", "The Coming of the Ice" };
    private static List<string> ArabicBooks = new List<string> { Reverse("في قلبي انثى عبرية"), Reverse("ارض زيكولا 1"), Reverse("ارض زيكولا 2"), Reverse("ارض زيكولا 3"), Reverse("ما وراء الكواليس الدحيح") };
    private static List<string> enBookLinks = new List<string> {
        "https://www.readanybook.com/online/684299",
        "https://www.readanybook.com/online/765632#583144",
        "https://www.readanybook.com/online/765193#582482",
        "https://www.readanybook.com/ebook/it-book-565296",
        "https://www.readanybook.com/online/10556#9479" };
    private static List<string> arBookLinks = new List<string> {
        "https://docs.google.com/viewerng/viewer?hl=ar&t=1&url=https://www.alarabimag.com/books/22459.pdf",
        "https://docs.google.com/viewerng/viewer?hl=ar&t=8&url=https://www.alarabimag.com/books/22375.pdf",
        "https://docs.google.com/viewerng/viewer?hl=ar&t=22&url=https://www.alarabimag.com/books/34834.pdf",
        "https://archive.org/details/2_20240503_20240503_0905/1-%20%D8%A3%D8%B1%D8%B6%20%D8%B2%D9%8A%D9%83%D9%88%D9%84%D8%A7",
        "https://kolalkotob.com/read/3141" };
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        WelcomeScreen();

        // تسجيل او تعمل حساب جديد 
        int yourChoice = choseSignInOrUp();
        Choice(yourChoice);

        MainMenu();
    }

    static void WelcomeScreen()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n\n");

        int startLeft = 10;
        int startTop = Console.CursorTop;

        PrintE(startLeft, startTop);
        PrintL(startLeft + 8, startTop);
        Print3(startLeft + 16, startTop);
        PrintO(startLeft + 25, startTop);
        PrintM(startLeft + 33, startTop);
        PrintA(startLeft + 44, startTop);
        PrintN(startLeft + 52, startTop);
        PrintY(startLeft + 61, startTop);
        Console.ForegroundColor = ConsoleColor.White;
    }

    static int choseSignInOrUp()
    {
        int your_choice;
        bool isValidInput;

        do
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\nPlease choose an option:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("1. Sign in");
            Console.WriteLine("2. Create a new account");
            Console.Write("\nEnter your choice (1 or 2): ");
            string input = Console.ReadLine();
            isValidInput = int.TryParse(input, out your_choice);

            if (!isValidInput || (your_choice != 1 && your_choice != 2 && your_choice != 3))
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Invalid input! Please enter 1 or 2.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        while (!isValidInput || (your_choice != 1 && your_choice != 2));

        return your_choice;
    }

    static void Choice(int choice)
    {
        if (choice == 1)
        {
            SignIn();
        }
        else if (choice == 2)
        {
            CreateNewAccount();
        }
    }

    static void SignIn()
    {
        Console.Write("Enter your username: ");
        string username = Console.ReadLine();
        int trayes = 3;
        bool login_ok = false;

        do
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            if (File.Exists("users.txt"))
            {
                string[] lines = File.ReadAllLines("users.txt");
                foreach (string line in lines)
                {
                    string[] parts = line.Split(' ');

                    if (parts.Length >= 2 && parts[0] == username && parts[1] == password)
                    {
                        login_ok = true;
                        break;
                    }
                }

                if (login_ok)
                {
                    Console.WriteLine("Login successful! Welcome to the library.");
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    trayes--;
                    if (trayes > 0)
                    {
                        Console.WriteLine($"Invalid username or password! You have {trayes} trayes remaining.");
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("Enter 1 if you Forget your Password \nEnter 0 to Exit");
                        points(".\n", 1, 5, false);
                        Console.ForegroundColor = ConsoleColor.White;
                        int.TryParse(Console.ReadLine(), out int forgetPassword);
                        if (forgetPassword == 1)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("1- Open the project folder\n2- Open folder 📁(bin) ---->📁(Debug) ---->📁(net8.0) ---->🗃️(users.txt) ");
                            Console.ReadKey();
                            Exit_Message();
                        }
                        else if (forgetPassword == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Exiting");
                            points(".", 1, 4, false);
                            Thread.Sleep(1000);
                            Exit_Message();
                        }
                    }
                }
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Failed to open the file 'users.txt' ❌❌\nPlease Create a new account");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("if you need to creat enter 1 ");
                points(".", 1, 5, false);
                if (int.TryParse(Console.ReadLine(), out int createAccount) && createAccount == 1)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Creating a new account...");
                    points(".", 1, 5, false);
                    Thread.Sleep(1000);
                    CreateNewAccount();
                    Console.Clear();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Exiting");
                    points(".", 1, 4, false);
                    Thread.Sleep(1000);
                    Exit_Message();
                }
            }
        }
        while (!login_ok && trayes > 0);
        Console.ForegroundColor = ConsoleColor.White;
    }

    static void CreateNewAccount()
    {
        Console.Write("Enter a new username: ");
        string username = Console.ReadLine();
        Console.Write("Enter a new password: ");
        string password = Console.ReadLine();

        using (StreamWriter file = new StreamWriter("users.txt", true))
        {
            file.WriteLine($"{username} {password}");
            Console.WriteLine("Account created successfully!");
        }
    }

    static void MainMenu()
    {
        int choice;
        do
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Console.WriteLine("\n-->Main Menu<--");
            Console.WriteLine("1. English library");
            Console.WriteLine("2. Arabic library");
            Console.WriteLine("3. Exit");
            Console.WriteLine("\t\t\t\t\t\t--------------------------");
            Console.WriteLine("\t\t\t\t\t\tChoose from the Menu (1-3)");
            Console.WriteLine("\t\t\t\t\t\t--------------------------");

            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input! Please enter a number.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    mainEnglishLibrary();
                    break;
                case 2:
                    mainArabicLibrary();
                    break;
                case 3:
                    Exit_Message();
                    return;
                default:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Invalid choice! Please select 1-3.");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }
        while (true);
    }

    static void Exit_Message()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine(@" 
                                             ╔════════════════════════════╗
                                             ║       LIBRARY SYSTEM       ║
                                             ╚════════════════════════════╝");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n\t\t\t\t\t Thank you for using our library system!");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Thread.Sleep(1000);
        Console.ForegroundColor = ConsoleColor.White;
        Environment.Exit(0);
    }

    static void mainEnglishLibrary()
    {
        int bookChoice;
        do
        {
            Console.Clear();
            Console.WriteLine("\nEnglish Library Menu");
            Console.WriteLine("1. View Books");
            Console.WriteLine("2. Add a book");
            Console.WriteLine("3. Delete a book");
            Console.WriteLine("4. Borrow a book");
            Console.WriteLine("5. Return a book");
            Console.WriteLine("6. Back to main menu");
            Console.WriteLine("----------------------");
            Console.WriteLine("Choose an action (1-6):");
            Console.WriteLine("----------------------");

            if (!int.TryParse(Console.ReadLine(), out bookChoice))
            {
                Console.WriteLine("Invalid input! Please enter a number.");
                continue;
            }

            switch (bookChoice)
            {
                case 1:
                    ViewEnglishBooks();
                    break;
                case 2:
                    AddEnglishBook();
                    break;
                case 3:
                    DeleteEnglishBook();
                    break;
                case 4:
                    BorrowEnglishBook();
                    break;
                case 5:
                    ReturnEnglishBook();
                    break;
                case 6:
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Invalid choice!");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }
        while (bookChoice != 6);
    }

    static void ViewEnglishBooks()
    {
        Console.Clear();
        Console.WriteLine("\nAvailable English Books:");
        if (EnglishBooks.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("No books available in English library.");
            Console.ForegroundColor = ConsoleColor.White;
        }
        else
        {
            for (int i = 0; i < EnglishBooks.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + EnglishBooks[i]);
            }

            Console.Write("\nEnter the number of the book to view details (or 0 to cancel): ");
            if (int.TryParse(Console.ReadLine(), out int bookChose))
            {
                if (bookChose > 0 && bookChose <= EnglishBooks.Count)
                {
                    Console.WriteLine("\nYou chose: " + EnglishBooks[bookChose - 1]);
                    if (bookChose <= enBookLinks.Count)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Link to Read: \n" + enBookLinks[bookChose - 1]);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("No available link for this book.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                else if (bookChose != 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Invalid choice!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Invalid input!");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
        Console.ForegroundColor = ConsoleColor.White;
    }

    static void AddEnglishBook()
    {
        Console.Write("Enter the name of the book to add: ");
        string new_book = Console.ReadLine();
        EnglishBooks.Add(new_book);
        Console.Clear();
        Console.WriteLine("Added: " + new_book);
        Console.WriteLine("Updated English book list:");

        for (int i = 0; i < EnglishBooks.Count; i++)
        {
            Console.WriteLine((i + 1) + ". " + EnglishBooks[i]);
        }

        Console.Write("Do you want to add a link for this book? (y/n): ");
        char add_link_chose = Console.ReadLine()[0];
        if (char.ToLower(add_link_chose) == 'y')
        {
            Console.Write("Enter the link: ");
            string link = Console.ReadLine();
            enBookLinks.Add(link);
            Console.WriteLine("Added: " + link);
            Console.WriteLine("Updated English book links:");

            for (int i = 0; i < enBookLinks.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + enBookLinks[i]);
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Thanks, The Book Added with link!");
            Console.ForegroundColor = ConsoleColor.White;
        }
        else
        {
            Console.WriteLine("Book added without link.");
        }
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
        Console.ForegroundColor = ConsoleColor.White;
    }

    static void DeleteEnglishBook()
    {
        if (EnglishBooks.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("No books available to delete in English library.");
            Console.ForegroundColor = ConsoleColor.White;
        }
        else
        {
            Console.Write("Enter the number of the book to delete: ");
            if (int.TryParse(Console.ReadLine(), out int delete_index))
            {
                if (delete_index >= 1 && delete_index <= EnglishBooks.Count)
                {
                    Console.WriteLine("Deleted: " + EnglishBooks[delete_index - 1]);
                    EnglishBooks.RemoveAt(delete_index - 1);
                    Console.WriteLine("Updated English book list:");

                    for (int i = 0; i < EnglishBooks.Count; i++)
                    {
                        Console.WriteLine((i + 1) + ". " + EnglishBooks[i]);
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Invalid book number!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Invalid input!");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
        Console.ForegroundColor = ConsoleColor.White;
    }

    static void BorrowEnglishBook()
    {
        Console.Write("Enter the number of books to borrow: ");
        if (int.TryParse(Console.ReadLine(), out int borrow_count))
        {
            if (borrow_count > 0)
            {
                enBorrowedBooks += borrow_count;
                Console.WriteLine("Borrowed " + borrow_count + " English book(s).");
                Console.WriteLine("Total borrowed English books: " + enBorrowedBooks);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Invalid number of books to borrow.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Invalid input!");
            Console.ForegroundColor = ConsoleColor.White;
        }
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
        Console.ForegroundColor = ConsoleColor.White;
    }

    static void ReturnEnglishBook()
    {
        Console.Write("Enter the number of books to return: ");
        if (int.TryParse(Console.ReadLine(), out int return_count))
        {
            if (return_count > 0)
            {
                if (return_count <= enBorrowedBooks)
                {
                    enBorrowedBooks -= return_count;
                    Console.WriteLine("Returned " + return_count + " English book(s).");
                    Console.WriteLine("Remaining borrowed English books: " + enBorrowedBooks);
                }
                else
                {
                    Console.WriteLine("You are trying to return more books than borrowed!");
                    Console.WriteLine("Currently borrowed: " + enBorrowedBooks + " books.");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Invalid number of books to return.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Invalid input!");
            Console.ForegroundColor = ConsoleColor.White;
        }
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
        Console.ForegroundColor = ConsoleColor.White;
    }

    static void mainArabicLibrary()
    {
        int bookChoice;
        do
        {
            Console.Clear();
            Console.WriteLine(Reverse("\nقائمة الكتب العربيه"));
            Console.WriteLine(Reverse("1. عرض الكتب"));
            Console.WriteLine(Reverse("2. اضافة كتاب"));
            Console.WriteLine(Reverse("3. حذف كتاب"));
            Console.WriteLine(Reverse("4. استعارة كتاب"));
            Console.WriteLine(Reverse("5. اعادة كتاب"));
            Console.WriteLine(Reverse("6. العوده الي القائمه الرئيسيه"));
            Console.WriteLine("-----------------------");
            Console.WriteLine(Reverse("اختر من القائمه= )1-6(:"));
            Console.WriteLine("-----------------------");

            if (int.TryParse(Console.ReadLine(), out bookChoice))
            {
                switch (bookChoice)
                {
                    case 1:
                        ViewArabicBooks();
                        break;
                    case 2:
                        AddArabicBook();
                        break;
                    case 3:
                        DeleteArabicBook();
                        break;
                    case 4:
                        BorrowArabicBook();
                        break;
                    case 5:
                        ReturnArabicBook();
                        break;
                    case 6:
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(Reverse("اختيار خاطئ!"));
                        break;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(Reverse("اختيار خاطئ!"));
            }
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(Reverse("\nاضغط أي مفتاح للمتابعة..."));
            Console.ReadKey();
            Console.ForegroundColor = ConsoleColor.White;
        }
        while (bookChoice != 6);
    }

    static void ViewArabicBooks()
    {
        Console.Clear();
        Console.WriteLine(Reverse("\nالكتب العربيه المتاحه:"));
        if (ArabicBooks.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(Reverse("لا كتب متاحه في المكتبه العربيه."));
            Console.ForegroundColor = ConsoleColor.White;
        }
        else
        {
            for (int i = 0; i < ArabicBooks.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + ArabicBooks[i]);
            }

            Console.Write(Reverse("   اختر رقم الكتاب لعرض التفاصيل او ختر 0 للالغاء \n"));
            if (int.TryParse(Console.ReadLine(), out int book_choice))
            {
                if (book_choice > 0 && book_choice <= ArabicBooks.Count)
                {
                    Console.WriteLine(ArabicBooks[book_choice - 1] + Reverse(" لقد اخترت : "));
                    if (book_choice <= arBookLinks.Count)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine(Reverse("\nرابط القراءه: ") + arBookLinks[book_choice - 1]);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(Reverse("لا روابط متاحه لهذا الكتاب."));
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                else if (book_choice != 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(Reverse("اختيار خاطئ!"));
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(Reverse("ادخال خاطئ!"));
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine(Reverse("\nاضغط أي مفتاح للمتابعة..."));
        Console.ReadKey();
        Console.ForegroundColor = ConsoleColor.White;
    }

    static void AddArabicBook()
    {
        Console.Write(Reverse("ادخل اسم الكتاب للاضافه: "));
        string new_book = Console.ReadLine();
        ArabicBooks.Add(new_book);
        Console.WriteLine(Reverse("اضيف: ") + new_book);
        Console.WriteLine(Reverse("تحديث قائمة الكتب:"));

        for (int i = 0; i < ArabicBooks.Count; i++)
        {
            Console.WriteLine((i + 1) + ". " + ArabicBooks[i]);
        }

        Console.Write(Reverse("هل تريد اضافه رابط لهذا الكتاب؟ (y/n): "));
        char add_link_chose = Console.ReadLine()[0];
        if (char.ToLower(add_link_chose) == 'y')
        {
            Console.Write(Reverse("ادخل الرابط: "));
            string link = Console.ReadLine();
            arBookLinks.Add(link);
            Console.WriteLine(Reverse("اضيف:\n ") + link);
            Console.WriteLine(Reverse("تحديث روابط الكتب العربيه:"));

            for (int i = 0; i < arBookLinks.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + arBookLinks[i]);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(Reverse("شكرا , تم اضافه الكتاب بالرابط!"));
            Console.ForegroundColor = ConsoleColor.White;
        }
        else
        {
            Console.WriteLine(Reverse("تم اضافه الكتاب بدون رابط."));
        }
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine(Reverse("\nاضغط أي مفتاح للمتابعة..."));
        Console.ReadKey();
        Console.ForegroundColor = ConsoleColor.White;
    }

    static void DeleteArabicBook()
    {
        if (ArabicBooks.Count == 0)
        {
            Console.WriteLine(Reverse("لا يتوفر كتب للحذف"));
        }
        else
        {
            Console.Write(Reverse("ادخل رقم الكتاب لحذفه: "));
            if (int.TryParse(Console.ReadLine(), out int delete_index))
            {
                if (delete_index >= 1 && delete_index <= ArabicBooks.Count)
                {
                    Console.WriteLine(Reverse("تم الحذف: ") + ArabicBooks[delete_index - 1]);
                    ArabicBooks.RemoveAt(delete_index - 1);
                    Console.WriteLine(Reverse("تحديث قائمة الكتب:"));

                    for (int i = 0; i < ArabicBooks.Count; i++)
                    {
                        Console.WriteLine((i + 1) + ". " + ArabicBooks[i]);
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(Reverse("رقم الكتاب خاطئ!"));
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(Reverse("ادخال خاطئ!"));
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine(Reverse("\nاضغط أي مفتاح للمتابعة..."));
        Console.ReadKey();
        Console.ForegroundColor = ConsoleColor.White;
    }

    static void BorrowArabicBook()
    {
        Console.Write(Reverse("ادخل عدد الكتب: "));
        if (int.TryParse(Console.ReadLine(), out int borrow_count))
        {
            if (borrow_count > 0)
            {
                arBorrowedBooks += borrow_count;
                Console.WriteLine(Reverse("تم استعارة  ") + borrow_count + Reverse(" كتب عربيه."));
                Console.WriteLine(Reverse("الكتب المستعاره الكليه ") + arBorrowedBooks);
            }
            else
            {
                Console.WriteLine(Reverse("عدد كتب خاطئ"));
            }
        }
        else
        {
            Console.WriteLine(Reverse("ادخال خاطئ!"));
        }
        Console.WriteLine(Reverse("\nاضغط أي مفتاح للمتابعة..."));
        Console.ReadKey();
    }

    static void ReturnArabicBook()
    {
        Console.Write(Reverse("ادخل عدد الكتب لاعادتها: "));
        if (int.TryParse(Console.ReadLine(), out int return_count))
        {
            if (return_count > 0)
            {
                if (return_count <= arBorrowedBooks)
                {
                    arBorrowedBooks -= return_count;
                    Console.WriteLine(Reverse("تم ارجاع ") + return_count + Reverse(" كتب عربيه."));
                    Console.WriteLine(Reverse("الكتب المتبقيه: ") + arBorrowedBooks);
                }
                else
                {
                    Console.WriteLine(Reverse("انت تحاول ارجاع قيمة كتب اكتر من المستعاره!"));
                    Console.WriteLine(Reverse("الكتب الحاليه: ") + arBorrowedBooks + Reverse(" كتب."));
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(Reverse("عدد خاطئ من الكتب للارجاع."));
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(Reverse("ادخال خاطئ!"));
            Console.ForegroundColor = ConsoleColor.White;
        }
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine(Reverse("\nاضغط أي مفتاح للمتابعة..."));
        Console.ReadKey();
        Console.ForegroundColor = ConsoleColor.DarkGray;
    }

    public static string Reverse(string input)
    {
        char[] charArray = input.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    static void PrintE(int startLeft, int startTop)
    {
        string[] eLines = {
            "███████╗",
            "██╔════╝",
            "█████╗░░",
            "██╔══╝░░",
            "███████╗",
            "╚══════╝"
        };
        PrintAnimatedLetter(eLines, startLeft, startTop);
    }

    static void PrintL(int startLeft, int startTop)
    {
        string[] aLines = {
            "██╗░░░░░",
            "██║░░░░░",
            "██║░░░░░░░",
            "██║░░░░░░░",
            "███████░",
            "╚═════╝░"
        };
        PrintAnimatedLetter(aLines, startLeft, startTop);
    }

    static void Print3(int startLeft, int startTop)
    {
        string[] gLines = {
            "╔███████╗",
            "╚════╗██║",
            "░░╔█████║",
            "░░╚══╗██║",
            "╔███████╝",
            "╚══════╝░"
        };
        PrintAnimatedLetter(gLines, startLeft, startTop);
    }

    static void PrintO(int startLeft, int startTop)
    {
        string[] lLines = {
            "░█████╗░",
            "██╔══██╗",
            "██║░░██║",
            "██║░░██║",
            "╚█████╔╝",
            "░╚════╝░"
        };
        PrintAnimatedLetter(lLines, startLeft, startTop);
    }

    static void PrintM(int startLeft, int startTop)
    {
        string[] sLines = {
            "███╗░░░███╗",
            "████╗░████║",
            "██╔████╔██║",
            "██║╚██╔╝██║",
            "██║░╚═╝░██║",
            "╚═╝░░░░░╚═╝"
        };
        PrintAnimatedLetter(sLines, startLeft, startTop);
    }

    static void PrintA(int startLeft, int startTop)
    {
        string[] sLines = {
            "░█████╗░",
            "██╔══██╗",
            "███████║",
            "██╔══██║",
            "██║░░██║",
            "╚═╝░░╚═╝"
        };
        PrintAnimatedLetter(sLines, startLeft, startTop);
    }

    static void PrintN(int startLeft, int startTop)
    {
        string[] sLines = {
            "███╗░░██╗",
            "████╗░██║",
            "██╔██╗██║",
            "██║╚████║",
            "██║░╚███║",
            "╚═╝░░╚══╝"
        };
        PrintAnimatedLetter(sLines, startLeft, startTop);
    }

    static void PrintY(int startLeft, int startTop)
    {
        string[] sLines = {
            "██╗░░░██╗",
            "██║░░░██║",
            "██║░░░██║",
            "██║░░░██║",
            "╚██████╔╝",
            "░╚═════╝░",
            "      ██",
            "      ██\n                  █████████████████████████████████████████████████████████████"
        };
        PrintAnimatedLetter(sLines, startLeft, startTop);
    }

    static void PrintAnimatedLetter(string[] letterLines, int startLeft, int startTop)
    {
        for (int i = 0; i < letterLines.Length; i++)
        {
            Console.SetCursorPosition(startLeft, startTop + i);
            foreach (char c in letterLines[i])
            {
                Console.Write(c);
                Thread.Sleep(5); // سرعة ظهور الحروف
            }
        }
        Thread.Sleep(60); // تأخير بعد انتهاء الحرف
    }

    static void points(string s, int ret, int num, bool cleer)
    {
        for (int i = 1; i <= ret; i++)
        {
            for (int j = 1; j <= num; j++)
            {
                Console.Write(s);
                Thread.Sleep(150);
            }
            if (cleer)
                Console.Clear();
        }
    }
}