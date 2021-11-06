using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SortingAndSearching
{
    class Program
    {

        public static int y = 0;

        public static bool switchSort = false;

        static void Main(string[] args)
        {
            List<string> comicTitles = new List<string>();

            using (StreamReader sr = new StreamReader("inputFile.csv"))
            {
                string line = sr.ReadLine();

                comicTitles = line.Split(',').ToList();
            }

            bool choosing = true;

            bool firstMenuPrint = true;

            while (choosing)
            {
                int x = (Console.WindowWidth / 2) - (Console.WindowHeight / 2);

                if (firstMenuPrint)
                {
                    firstMenuPrint = false;
                    SlowPrintMenu();
                }
                else
                {
                    PrintMenu();
                }


                if (switchSort)
                {
                    switchSort = false;

                }
                else
                {
                    switchSort = true;
                }

                int anwser = ReadChoice("Which option would you like to choose? ");

                switch (anwser)
                {
                    case 1:
                        List<string> bubbleList = new List<string>(comicTitles);

                        BubbleSort(bubbleList);

                        Console.WriteLine("\n-----------------------------------------------------------------------------------------------------------------------------\n");

                        y += 3;

                        for (int i = 0; i < bubbleList.Count(); i++)
                        {
                            SetPosition(15, y);

                            Console.Write(comicTitles[i]);

                            SetPosition((Console.WindowWidth / 2) + 15, y++);

                            Console.Write(bubbleList[i]);

                            y++;
                        }

                        Console.ReadKey();
                        Console.Clear();
                        y = 0;
                        break;
                    case 2:
                        List<string> mergeList = MergeSort(comicTitles);

                        Console.WriteLine("\n-----------------------------------------------------------------------------------------------------------------------------\n");

                        y += 3;

                        for (int i = 0; i < mergeList.Count(); i++)
                        {
                            SetPosition(15, y);

                            Console.Write(comicTitles[i]);

                            SetPosition((Console.WindowWidth / 2) + 15, y++);

                            Console.Write(mergeList[i]);

                            y++;
                        }

                        Console.ReadKey();
                        Console.Clear();
                        y = 0;
                        break;
                    case 3:
                        List<string> binaryList = MergeSort(comicTitles);

                        Console.WriteLine("\n-----------------------------------------------------------------------------------------------------------------------------\n");

                        y += 3;

                        for (int i = 0; i < binaryList.Count(); i++)
                        {
                            int foundIndex = BinarySearch(binaryList, binaryList[i], 0, comicTitles.Count() - 1);
                            
                            SetPosition(15, y);

                            Console.Write(binaryList[foundIndex]);

                            SetPosition((Console.WindowWidth / 2) + 10, y);

                            Console.Write($"Index: {i}");

                            SetPosition((Console.WindowWidth / 2) + 30, y++);

                            Console.Write($"Index: {foundIndex}");

                            y++;
                        }

                        Console.ReadKey();
                        Console.Clear();
                        y = 0;
                        break;
                    case 4:
                        SetPosition(x, y++);

                        List<string> sortedList = MergeSort(comicTitles);

                        string fileName = default;

                        ReadString(ref fileName, "What file name would you like to use? ");

                        string extension = Path.GetExtension(fileName);

                        if (extension != null)
                        {
                            if (extension != ".json")
                            {
                                fileName = Path.ChangeExtension(fileName, ".json");
                            }
                        }
                        else
                        {
                            fileName = Path.ChangeExtension(fileName, ".json");
                        }

                        using (StreamWriter sw = new StreamWriter(fileName))
                        {
                            using (JsonTextWriter jw = new JsonTextWriter(sw))
                            {
                                jw.Formatting = Formatting.Indented;

                                JsonSerializer ser = new JsonSerializer();

                                ser.Serialize(jw, sortedList);
                                jw.Flush();
                            }
                        }
                        Console.ForegroundColor = ConsoleColor.DarkGreen;

                        SetPosition(x, y++);

                        SlowPrint("File has been saved!");
                        Console.ResetColor();
                        Console.ReadKey();
                        Console.Clear();
                        y = 0;
                        break;
                    case 5:
                        choosing = false;
                        Console.Clear();
                        y = Console.WindowHeight / 2;
                        SetPosition(x, y);
                        SlowPrint("Press any key to exit...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void ReadString(ref string fileName, string prompt)
        {
            int x = (Console.WindowWidth / 2) - (Console.WindowHeight / 2);

            bool choosingAnwser = true;

            while (choosingAnwser)
            {

                SlowPrint(prompt);

                string anwser = Console.ReadLine();

                if (!String.IsNullOrWhiteSpace(anwser))
                {
                    fileName = anwser;
                        break;
                }

                Console.ForegroundColor = ConsoleColor.DarkRed;
                SetPosition(x, y++);
                SlowPrint("Must be a valid string.");
                Console.ResetColor();
                Console.ReadKey();
                ClearLastLine();
                SetPosition(x, y - 2);
                ClearLastLine();
                y--;
            }
        }

        private static int BinarySearch(List<string> titles, string searchTerm, int low, int high)
        {
            if (high < low)
            {
                return -1;
            }

            int mid = (low + high) / 2;


            int comparison = titles[mid].CompareTo(searchTerm);

            if (switchSort)
            {
                if (comparison > 0)
                {
                    return BinarySearch(titles, searchTerm, low, (mid - 1));
                }
                else if (comparison < 0)
                {
                    return BinarySearch(titles, searchTerm, (mid + 1), high);
                }
                else
                {
                    return mid;
                }
            }
            else
            {
                if (comparison < 0)
                {
                    return BinarySearch(titles, searchTerm, low, (mid - 1));
                }
                else if (comparison > 0)
                {
                    return BinarySearch(titles, searchTerm, (mid + 1), high);
                }
                else
                {
                    return mid;
                }
            }

            
        }

        static List<string> MergeSort(List<string> unsortedList)
        {
            if (unsortedList.Count <= 1)
            {
                return unsortedList;
            }
            else
            {
                List<string> leftList = new List<string>();
                List<string> rightList = new List<string>();

                for (int i = 0; i < unsortedList.Count; i++)
                {
                    if (i < (unsortedList.Count / 2))
                    {
                        leftList.Add(unsortedList[i]);
                    }
                    else
                    {
                        rightList.Add(unsortedList[i]);
                    }
                };

                leftList = MergeSort(leftList);
                rightList = MergeSort(rightList);

                return Merge(leftList, rightList);
            }
        }

        private static List<string> Merge(List<string> leftList, List<string> rightList)
        {
            List<string> result = new List<string>();

            int compare;

            while (leftList.Count != 0 && rightList.Count != 0)
            {
                if (switchSort)
                {
                    compare = leftList[0].CompareTo(rightList[0]);

                    if (compare < 0)
                    {
                        result.Add(leftList[0]);
                        leftList.Remove(leftList[0]);

                    }
                    else
                    {
                        result.Add(rightList[0]);
                        rightList.Remove(rightList[0]);
                    }
                }
                else
                {
                    compare = rightList[0].CompareTo(leftList[0]);

                    if (compare > 0)
                    {

                        result.Add(rightList[0]);
                        rightList.Remove(rightList[0]);
                    }
                    else
                    {
                        result.Add(leftList[0]);
                        leftList.Remove(leftList[0]);
                    }
                }
            }

            while (leftList.Count != 0)
            {
                result.Add(leftList[0]);
                leftList.Remove(leftList[0]);
            }

            while (rightList.Count != 0)
            {
                result.Add(rightList[0]);
                rightList.Remove(rightList[0]);
            }

            return result;
        }

        static void BubbleSort(List<String> unsortedList)
        {
            int length = unsortedList.Count();

            bool swapped = true;
            if (switchSort)
            {
                while (swapped)
                {
                    swapped = false;

                    for (int i = 1; i <= length - 1; i++)
                    {
                        if (unsortedList[i - 1].CompareTo(unsortedList[i]) > 0)
                        {
                            swap(unsortedList, i - 1, i);
                            swapped = true;
                        }
                    }
                    length--;
                }
            }
            else
            {
                while (swapped)
                {
                    swapped = false;

                    for (int i = 1; i <= length - 1; i++)
                    {
                        if (unsortedList[i - 1].CompareTo(unsortedList[i]) < 0)
                        {
                            swap(unsortedList, i - 1, i);
                            swapped = true;
                        }
                    }
                    length--;
                }
            }
        }

        static void swap(List<string> unsortedList, int index1, int index2)
        {
            string temp = unsortedList[index1];
            unsortedList[index1] = unsortedList[index2];
            unsortedList[index2] = temp;
        }

        static int ReadChoice(string prompt)
        {
            int x = (Console.WindowWidth / 2) - (Console.WindowHeight / 2);

            bool choosingAnwser = true;

            int number = default;

            while (choosingAnwser)
            {

                SlowPrint(prompt);

                string anwser = Console.ReadLine();

                if (int.TryParse(anwser, out int realNum))
                {
                    if (realNum >= 1 && realNum <= 5)
                    {
                        number = realNum;
                        choosingAnwser = false;
                        break;
                    }
                }

                Console.ForegroundColor = ConsoleColor.DarkRed;
                SetPosition(x, y++);
                SlowPrint("Must be a number between 1 through 5.");
                Console.ResetColor();
                Console.ReadKey();
                ClearLastLine();
                SetPosition(x, y - 2);
                ClearLastLine();
                y--;
            }

            return number;
        }

        static void SlowPrintMenu()
        {
            Console.SetWindowSize(125, 35);

            int x = (Console.WindowWidth / 2) - (Console.WindowHeight / 2);

            SetPosition(x, y++);
            SlowPrint("Welcome To Sort and Search!");
            Thread.Sleep(1000);
            y++;

            SetPosition(x, y++);
            SlowPrint("1. Bubble Sort");

            SetPosition(x, y++);
            SlowPrint("2. Merge Sort");

            SetPosition(x, y++);
            SlowPrint("3. Binary");

            SetPosition(x, y++);
            SlowPrint("4. Save");

            SetPosition(x, y++);
            SlowPrint("5. Exit");
            y++;

            SetPosition(x, y++);

        }

        static void PrintMenu()
        {
            Console.SetWindowSize(125, 35);

            int x = (Console.WindowWidth / 2) - (Console.WindowHeight / 2);

            SetPosition(x, y++);
            Console.WriteLine("Welcome To Sort and Search!");
            y++;

            SetPosition(x, y++);
            Console.WriteLine("1. Bubble Sort");

            SetPosition(x, y++);
            Console.WriteLine("2. Merge Sort");

            SetPosition(x, y++);
            Console.WriteLine("3. Binary");

            SetPosition(x, y++);
            Console.WriteLine("4. Save");

            SetPosition(x, y++);
            Console.WriteLine("5. Exit");
            y++;

            SetPosition(x, y++);

        }

        static void SetPosition(int xX, int xY)
        {
            Console.SetCursorPosition(xX, xY);
        }

        static void ClearLastLine()
        {
            int x = (Console.WindowWidth / 2) - (Console.WindowHeight / 2);
            int currentLinePosition = Console.CursorTop;

            Console.SetCursorPosition(x, Console.CursorTop);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(x, currentLinePosition);
        }

        static void SlowPrint(string words)
        {
            for (int i = 0; i < words.Length; i++)
            {
                Console.Write(words[i]);
                Thread.Sleep(50);

            }
        }

    }
}
