using System;
namespace sharp
{
    /// <summary>
    /// Статический класс, содержащий логику игры по отгадыванию значения математической функции.
    /// </summary>
    public static class Function
    {
        /// <summary>
        /// Нуу, константа числа Пи.
        /// </summary>
        public const double Pi = 3.141592653589793;
        /// <summary>
        /// Вычисляет значение математической функции по двум аргументам и округляет до 2 знаков.
        /// </summary>
        /// <param name="a">Первое число (аргумент a).</param>
        /// <param name="b">Второе число (аргумент b).</param>
        /// <returns>Вычисленное значение функции.</returns>
        public static double Answer(int a, int b)
        {
            return Pi * (Math.Log(Math.Pow(b, 5))/(Math.Sin(a)+1));;
        }
    }
}