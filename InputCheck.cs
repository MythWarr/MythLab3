using System;
namespace sharp
{
    /// <summary>
    /// Статический класс, содержащий логику проверки вводимых значений.
    /// </summary>
    public static class InputCheck
    {
        /// <summary>
        /// Забирает у пользователя его заветное числишко.
        /// </summary>
        /// <returns>Возвращает число, введенное пользователем.</returns>
        public static int TakeNumber()
        {
            int a;
            while (!int.TryParse(Console.ReadLine(), out a))
            {
                Console.WriteLine("Укажите валидное число. ");
            }
            return a;
        }
        /// <summary>
        /// Сравнение результата с преполагаемым числом.
        /// </summary>
        /// <param name="result">Результат выполнения функции.</param>
        /// <returns>Вычисленное значение функции.</returns>
        public static bool IsCorrectNumber(double result)
        {
            Console.Write("Итак, Ваше число: ");
            double desiredResult = TakeNumber();
            if (desiredResult == result) {return true;}
            else {return false;}
        }
    }
}