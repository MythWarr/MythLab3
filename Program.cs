using System;
using System.Runtime.Versioning;
using System.Threading;
using System.Diagnostics;
using System.Security.Cryptography;
using System.IO.Pipelines;
using System.IO.Compression;
using System.Runtime.InteropServices.Marshalling;

namespace sharp
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isClosed = false;
            while (!isClosed)
            {
                Console.Clear();
                Console.WriteLine("Менюшка.\n1. Поиграем в числа?\n2. Об авторе\n3. Сортировка массивов\n4. Тетрис\n5. Выход");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Game();
                        break;
                    case "2":
                        AboutAuthor();
                        break;
                    case "3":
                        SortingArrs();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "4":
                        PlayTetris();
                        break;
                    case "5":
                        isClosed = ClosingProgram();
                        break;
                    case "6":
                        Cat cate = new Cat();
                        Dog doge = new Dog();
                        Bird birde = new Bird();
                        List<Animal> animals = {cate, doge, birde};
                        foreach (Animal an in animals)
                        {
                            an.MakeSound();
                        }
                    default:
                        Console.WriteLine("Неверный ввод. Попробуйте снова.");
                        Console.ReadLine();
                        break;
                }
            }
        }
        static bool ClosingProgram()
        {
            Console.Clear();
            Console.WriteLine("Выходим..."); 
            return true;
        }
        static void Game()
        {
            Console.Clear();
            Console.WriteLine("Начинаем игру!");
            Console.Write("Первое число: ");
            int a = InputCheck.TakeNumber();
            Console.Write("Второе: ");
            int b = InputCheck.TakeNumber();
            double result = Function.Answer(a, b);
            for(int i = 3; i > 0; i--)
            {
                Console.Write($"Число попыток: {i}. ");
                if (InputCheck.IsCorrectNumber(result)) 
                {
                    Console.WriteLine("Ну, Вы победили кажется...");
                    break;
                }
                else {Console.WriteLine("Неверно.");}
                if (i==1) {Console.WriteLine($"Верный ответ был: {result}");}
            }
            Console.ReadKey();
            Console.Clear();
        }
        static void AboutAuthor()
        {
            Console.WriteLine("фио: Богатырёв Владимир Иванович, группа: 6105-090301D");
            Console.ReadKey();
            Console.Clear();
        }

        static void SortingArrs()
        {
            Console.Clear();
            Console.WriteLine("Начинаем сортировку! Какой длины будет массив? ");
            int lengthOfArr = TakingArrLength();
            Arrays arr1 = new Arrays();
            Arrays arr2 = new Arrays(lengthOfArr);
            int[] arr = arr2.GetArr(lengthOfArr);
            OutingArr(arr, arr2);
        }
        static int TakingArrLength()
        {
            bool isAllowed = false;
            int n=0;
            while (isAllowed==false)
            {
                n = InputCheck.TakeNumber();
                if (n>0) {isAllowed=true;}
                else {Console.WriteLine("Введите корректное значение.");}
            }
            return n;
        }
        static void OutingArr(int[] arr, Arrays arr2)
        {
            Stopwatch sw1 = new Stopwatch();
            Stopwatch sw2 = new Stopwatch();
            if (arr.Length<10)
            {
                var array1 = arr2.CopyingArr(arr);
                var array2 = arr2.CopyingArr(arr);
                sw1.Start();
                int[] bublArr = arr2.BubbleSort(array1);
                sw1.Stop();
                long time1 = sw1.ElapsedMilliseconds;
                sw2.Start();
                int[] invArr = arr2.InsertionSort(array2);
                sw2.Stop();
                long time2 = sw2.ElapsedMilliseconds;
                Console.WriteLine("Отсортированный массив методом пузырьков: {0}", string.Join(", ", bublArr));
                Console.WriteLine("Отсортированный массив методом вставок: {0}", string.Join(", ", invArr));
                if (time1<time2)
                {
                    Console.WriteLine("Сортировка пузырьками была быстрее.");
                }
                else if (time1>time2)
                {
                    Console.WriteLine("Сортировка вставками была быстрее.");
                }
                else {Console.WriteLine("Сортировки прошли одновременно быстро...");}
            }
            else
            {
                Console.WriteLine("Данный массив вывести не выйдет.");
            }
        }
        static void PlayTetris()
        {
            Tetrisik tetr = new Tetrisik();
            Console.CursorVisible=false;
            tetr.InitializeField();
            tetr.SpawnPiece();
            
            int dropInterval = 1000;
            int lastDrop = Environment.TickCount;

            Console.Clear();

            while (!tetr.gameOver)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    tetr.HandleInput(keyInfo.Key);
                }
                if (Environment.TickCount - lastDrop > dropInterval)
                {
                    tetr.UpdateState();
                    lastDrop = Environment.TickCount;
                }
                tetr.DrawField();
                Thread.Sleep(30);
            }

            Console.Clear();
            Console.WriteLine("===========================");
            Console.WriteLine("        ИГРА ОКОНЧЕНА      ");
            Console.WriteLine($"    Ваш итоговый счет: {tetr.score}    ");
            Console.WriteLine("===========================");
            Console.CursorVisible=true;
            Console.WriteLine("\nНажмите любую клавишу для выхода в меню...");
            Console.ReadKey();
        }
    }
}