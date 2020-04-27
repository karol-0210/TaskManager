using System;
using System.Collections.Generic;

namespace TaskManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(160, 30);
            ConsoleEx.WriteLine("*********Twój menadżer zadań*********", ConsoleColor.Yellow, ConsoleColor.DarkBlue);
            string command = string.Empty;
            List<TaskModel> tasks_list = new List<TaskModel>();
            tasks_list = Utilities.LoadTasks("Data.csv");

            Console.WriteLine($"Masz {tasks_list.Count} zapisane zadania");

            do
            {

                Console.WriteLine("Podaj polecenie lub wpisz 'h' aby uzyskać pomoc. Polecenie \"exit\" kończy działanie programu.");
                command = Console.ReadLine().ToLower().Trim();


                if (command == "save")
                {
                    Utilities.SaveToFile(tasks_list);
                }
                if (command == "add")
                {
                    TaskModel Task = new TaskModel();
                    Task.AddTask();
                    tasks_list.Add(Task);
                }

                if (command == "list")
                {
                    Utilities.ShowTasks(tasks_list);
                }
                if (command == "del")
                {
                    Utilities.ShowTasks(tasks_list);
                    Console.WriteLine("Podaj pozycje nr do usunięcia:");
                    bool isParse;
                    do
                    {
                        isParse = int.TryParse(Console.ReadLine().Trim(), out int x);
                        if (isParse && x <= tasks_list.Count && x > 0)
                        {
                            Utilities.RemoveTask(tasks_list, x);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Nie podałeś poprawnego numeru");
                        }

                    } while (true);
                }


                if (command == "h")
                {
                    Console.WriteLine("---------------------------------------");
                    Console.WriteLine("add - dodanie nowego zadania");
                    Console.WriteLine("del - usuwanie zadania");
                    Console.WriteLine("list - Wyświetlanie wszystkich zadań");
                    Console.WriteLine("save - zapisz do pliku");
                    Console.WriteLine("open - otwórz zadania z pliku");
                    Console.WriteLine("cls - wyczyść ekran konsoli");
                    Console.WriteLine("---------------------------------------");
                }
                if (command == "cls")
                {
                    Console.Clear();
                }

                if (command == "load")
                {
                    Utilities.LoadTasks("Data.csv");
                }
                if (command == "exit")
                {
                    List<TaskModel> loadedList = Utilities.LoadTasks("Data.csv");

                    if (IsChanged.isChanged)
                    {
                        do
                        {
                            Console.WriteLine("Czy chcesz zapisać zmiany przed wyjściem? (t/n/a - anunuj)");
                            command = Console.ReadLine().ToLower().Trim();
                            if (command == "t")
                            {
                                Utilities.SaveToFile(tasks_list);
                                Console.WriteLine("Program zakończony.");
                                break;
                            }
                            else if (command == "n")
                            {
                                Console.WriteLine("Program zakończony.");
                                break;
                            }
                        } while (command != "t" && command != "n" && command != "a");
                        if (command != "a")
                        {
                            Console.WriteLine("Program zakończony.");
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Program zakończony.");
                        break;
                    }

                }


            } while (true);

        }
    }
}
