using System;
namespace sharp
{
    /// <summary>
    /// Класс для работы с массивами.
    /// </summary>
    public class Arrays
    {
        /// <summary>
        /// Длина массива.
        /// </summary>
        private int n;
        /// <summary>
        /// Массев.
        /// </summary>
        private int[] arr;
        /// <summary>
        /// Getter для длины массива.
        /// </summary>
        /// <returns>Значение н'ки.</returns>
        public int getN()
        {
            return n;
        }
        /// <summary>
        /// Конструтор 1. Без параметров(базовые).
        /// </summary>
        public Arrays()
        {
            n=10;
        }
        /// <summary>
        /// Конструтор 2. С одним параметром.
        /// </summary>
        /// <param name="n">Длина массива.</param>
        public Arrays(int n)
        {
            this.n=n;
        }
        /// <summary>
        /// Заполнение массива рандомными значениями и их возврат.
        /// </summary>
        /// <param name="n">Длина массива.</param>
        /// <returns>Заполненный массив.</returns>
        public int[] GetArr(int n)
        {
            this.n=n;
            Random rand = new Random();
            int[] arr = new int[n];
            for(int i=0; i<n; i++)
            {
                arr[i]=rand.Next(1, 100);
            }
            return arr;
        }
        /// <summary>
        /// Свааап.
        /// </summary>
        /// <param name="e1">Ссылка на первую переменную.</param>
        /// <param name="e2">Ссылка на вторую переменную.</param>
        public void Swap(ref int e1, ref int e2)
        {
            int temp = e1;
            e1 = e2;
            e2 = temp;
        }
        /// <summary>
        /// Сортировка пузырьком.
        /// </summary>
        /// <param name="arr">Массивчик.</param>
        /// <returns>Отсортированный массив.</returns>
        public int[] BubbleSort(int[] arr)
        {
            var len = arr.Length;
            for (var i = 1; i < len; i++)
            {
                for (var j = 0; j < len - i; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        Swap(ref arr[j], ref arr[j + 1]);
                    }
                }
            }
            return arr;
        }
        /// <summary>
        /// Сортировка вставками.
        /// </summary>
        /// <param name="arr">Массивчик.</param>
        /// <returns>Отсортированный массив.</returns>
        public int[] InsertionSort(int[] arr)
        {
            for (var i = 1; i < arr.Length; i++)
            {
                var key = arr[i];
                var j = i;
                while ((j > 1) && (arr[j - 1] > key))
                {
                    Swap(ref arr[j - 1], ref arr[j]);
                    j--;
                }

                arr[j] = key;
            }

            return arr;
        }
        /// <summary>
        /// Копирка массива.
        /// </summary>
        /// <param name="arr">Массивчик.</param>
        /// <returns>Скопированный массив.</returns>
        public int[] CopyingArr(int[] arr)
        {
            int[] arr_temp = new int[arr.Length];
            arr.CopyTo(arr_temp);
            return arr;
        }
    }
}