using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace TaskManager
{
    public static class Utilities
    {
        public static void ShowTasks(List<TaskModel> tasks_list)
        {
            int padding = 60;
            Console.Clear();
            Console.Write(" Nr".PadRight(3)+'|');
            Console.Write(" Zadanie".PadRight(padding) + '|');
            Console.Write(" Data rozpoczęcia".PadRight(20) + '|');
            Console.Write(" Data zakończenia".PadRight(20) + '|');
            Console.Write(" Czy ważne".PadRight(25) + '|');
            Console.WriteLine();

            int i = 1;
            foreach (var item in tasks_list)
            {
                Console.Write(i.ToString().PadRight(3) + '|');
                Console.Write(item.Description.PadRight(padding) + '|');
                Console.Write(item.Start_date.ToString().PadRight(20) + '|');
                if (item.End_date.HasValue)
                {
                    Console.Write(item.End_date.ToString().PadRight(20) + '|');
                }
                else
                {
                    Console.Write("Zadanie całodniowe".PadRight(20) + '|');
                }

                if (item.Relevance.HasValue)
                {
                    if (item.Relevance.Value)
                    {
                        Console.Write("Ważne".PadRight(25) + '|');
                    }
                    else if (!item.Relevance.Value)
                    {
                        Console.Write("Mało ważne".PadRight(25) + '|');
                    }
                }
                else
                {
                    Console.Write("Brak statusu ważności".PadRight(25) + '|');
                }
                Console.WriteLine();
                i++;
            }
        }

        public static void SaveToFile(List<TaskModel> tasks_list)
        {
            string path = "Data.csv";
            List<string> file_inherit = new List<string>();
            foreach (TaskModel task in tasks_list)  //przygotowanie danych do zapisu linijka po linijce
            {
                string endTime, relevance;
                StringBuilder line = new StringBuilder();
                if (task.All_day)
                    endTime = "Zadanie całodniowe";
                else 
                    endTime = task.End_date.Value.ToString();

                if (task.Relevance.HasValue)
                {
                    if (task.Relevance.Value)
                    {
                        relevance = "Ważne";
                    }
                    else
                    {
                        relevance = "Mało ważne";
                    }
                }
                else
                    relevance = "Brak statusu ważności";

                line.AppendJoin(',', new [] {task.Description.ToString() , task.Start_date.ToString()
                                            , endTime , relevance  }); //tworzenie linijki do pliku csv
                file_inherit.Add(line.ToString()); //lista linijek do zapisu do pliku
            }
                File.WriteAllLines(path, file_inherit);
                Console.WriteLine($"Zmiany zapisano w pliku {path} w katalogu roboczym.");
        }

        public static List <TaskModel> LoadTasks(string path)
        {
            List <TaskModel> loadedList = new List<TaskModel>();

            if (File.Exists(path))
            {
                string [] inherit = File.ReadAllLines(path);
                foreach (string item in inherit)
                {
                    string [] splitLine = item.Split(',');
                    if (splitLine.Length!=4)
                    {
                        Console.WriteLine("Nie mogę odczytać poprawnie danych. Plik uszkodzony lub zmieniony (zła ilość danych w linji)");
                    }
                    //DateTime dateTime;
                    bool isParse = DateTime.TryParse(splitLine[1], out DateTime Start_dateTime);
                    if (!isParse)
                    {
                        Console.WriteLine("Nie mogę odczytać poprawnie danych. Plik uszkodzony lub zmieniony (niepoprawny format daty początkowej)");
                    }
                    isParse = DateTime.TryParse(splitLine[2], out DateTime End_dateTime);
                    bool allday=true;
                    if (!isParse)
                    {
                        if (splitLine[2]== "Zadanie całodniowe")
                        {
                            allday = true;
                        }
                        else
                        {
                            Console.WriteLine("Nie mogę odczytać poprawnie danych. Plik uszkodzony lub zmieniony (niepoprawny format daty końcowej lub źle oznaczone zadanie całodniowe)");
                        }
                    }
                    bool? relevance=null;
                    if (splitLine[3]== "Brak statusu ważności")
                    {
                        relevance = null;
                    }
                    else if (splitLine[3] == "Mało ważne")
                    {
                        relevance = false;
                    }
                    else if (splitLine[3] == "Ważne")
                    {
                        relevance = true;
                    }
                    else
                    {
                        Console.WriteLine("Nie mogę odczytać poprawnie danych. Plik uszkodzony lub zmieniony (niepoprawne oznaczenie ważnośći zadania)");
                    }
                    if (!allday)
                    {
                        TaskModel task = new TaskModel(splitLine[0], Start_dateTime, End_dateTime, relevance);
                        loadedList.Add(task);
                    }
                    else
                    {
                        TaskModel task = new TaskModel(splitLine[0], Start_dateTime, relevance);
                        loadedList.Add(task);
                    }
                }
            }
            return loadedList;
        }

        public static void RemoveTask(List <TaskModel> task_list ,int position)
        {
            task_list.RemoveAt(position - 1);
            Console.WriteLine("Pomyślnie usunięto zadanie nr {0}",position);
        }


    }
}
