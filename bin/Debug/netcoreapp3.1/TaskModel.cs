using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager
{
    public class TaskModel
    {
        public string Description;
        public DateTime Start_date;
        public DateTime? End_date;
        public bool All_day;
        public bool? Relevance;

        public TaskModel() { }
        public TaskModel(string description, DateTime start_date, DateTime end_date, bool? relevance)
        {
            Description = description;
            Start_date = start_date;
            End_date = end_date;
            Relevance = relevance;
        }
        public TaskModel(string description, DateTime start_date, DateTime end_date)
        {
            Description = description;
            Start_date = start_date;
            End_date = end_date;
        }
        public TaskModel(string description, DateTime start_date, bool? relevance)
        {
            Description = description;
            Start_date = start_date;
            All_day = true;
            Relevance = relevance;
        }
        public TaskModel(string description, DateTime start_date)
        {
            Description = description;
            Start_date = start_date;
            All_day = true;
        }

        public void AddTask()
        {
            string command;
            Console.WriteLine("Dodawanie nowego zadania:\nPodaj opis zadania: (max 60 znaków)");
            do
            {
                command = Console.ReadLine().Trim(new char[] { ' ', ',' }).Replace(',', ';');
                if (command.Length > 60)
                {
                    Console.WriteLine("Opis zadanie jest za długie. Max 60 znaków.");
                }
                else
                    Description = command;
            } while (command.Length>60);

            bool isParse;
            do
            {
                Console.WriteLine("Podaj date rozpoczęcia zadania:");
                isParse = DateTime.TryParse(Console.ReadLine().Trim(), out Start_date);
                if(isParse==false)
                    Console.WriteLine("Niepoprawny format daty. Spróbuj jeszcze raz: RRRR-MM-DD lub RRRR-MM-DD GG:MM");
            } while (!isParse);

            do
            {
                Console.WriteLine("Podaj datę zakończenia. Jeśli chcesz  ustawić zadanie całodniowe przejdź dalej naciskając enter");
                command = Console.ReadLine();
                if (command == string.Empty)
                {
                    End_date = null;
                    All_day = true;
                    Console.WriteLine("Oznaczono jako zadanie całodniowe");
                    break;
                }
                else
                {
                    isParse = DateTime.TryParse(command, out DateTime data);
                    if (isParse && data < Start_date)
                    {
                        Console.WriteLine("Data zakończenia nie może być wcześniejsza niż data rozpoczęcia");
                    }
                    else if (isParse)
                    {
                        End_date = data;
                        Console.WriteLine("Pomyślnie dodano datę zakończenia zadania");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Niepoprawny format daty. Spróbuj jeszcze raz: RRRR-MM-DD lub RRRR-MM-DD GG:MM");
                    }    
                }
            } while (true);
            

            do
            {
                Console.WriteLine("Czy chcesz ustawić ważność zadania (t/n?)");
                command = Console.ReadLine().Trim().ToLower();
                if (command == "t")
                {
                    do
                    {
                        Console.WriteLine("Podaj ważność zadania: w - ważne, m  -mało ważne (w/m?)");
                        command = Console.ReadLine().Trim().ToLower();
                        if (command == "w")
                        {
                            Relevance = true;
                            Console.WriteLine($"Zadanie \"{Description}\" oznaczono jako ważne");
                            break;
                        }
                        else if (command == "m")
                        {
                            Relevance = false;
                            Console.WriteLine($"Zadanie \"{Description}\" oznaczono jako mało ważne");
                            break;
                        }
                    } while (Relevance == null);
                }
                else
                {
                    Relevance = null;
                }

            } while (command != "t" && command != "n" && command != "w" && command != "m");
            Console.WriteLine($"Zadanie \"{Description}\" pomyślnie dodano do listy");
        }

        


    }
}
