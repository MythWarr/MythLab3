using System;
namespace sharp
{
    /// <summary>
    /// Класс по игре в Тетрис (переменные и методы основной логики).
    /// </summary>
    public class Tetrisik
    {
        /// <summary>
        /// Константа ребра поля.
        /// </summary>
        public const int fieldSize = 10;
        /// <summary>
        /// Поле.
        /// </summary>
        public int[,] field {get; private set;}
        /// <summary>
        /// Падающая фигура.
        /// </summary>
        public int[,] currentShape{get; private set;}
        /// <summary>
        /// Значение по Х падающей фигуры.
        /// </summary>
        public int currentX{get; private set;}
        /// <summary>
        /// Значение по У падающей фигуры.
        /// </summary>
        public int currentY{get; private set;}
        /// <summary>
        /// Набранные очки.
        /// </summary>
        public int score{get; private set;}
        /// <summary>
        /// Статус игры.
        /// </summary>
        public bool gameOver{get; private set;}
        /// <summary>
        /// Переменная для выбора рандомной следующей фигуры.
        /// </summary>
        public Random rand {get; private set;} = new Random();
        /// <summary>
        /// Фигуры на выбор.
        /// </summary>
        public int[][,] Shapes {get; private set;} = new int[][,]
        {
            new int[,] {{1, 1, 1, 1}},                           
            new int[,] {{1, 1}, {1, 1}},                         
            new int[,] {{0, 1, 0}, {1, 1, 1}},                 
            new int[,] {{1, 0, 0}, {1, 1, 1}},                 
            new int[,] {{0, 0, 1}, {1, 1, 1}},                  
            new int[,] {{0, 1, 1}, {1, 1, 0}},                  
            new int[,] {{1, 1, 0}, {0, 1, 1}}                   
        };
        /// <summary>
        /// Метод инициализации поля и статуса игры.
        /// </summary>
        public void InitializeField()
        {
            field=new int[fieldSize, fieldSize];
            score = 0;
            gameOver = false;
        }
        /// <summary>
        /// Метод, чтобы кнопошки жались.
        /// </summary>
        public void HandleInput(ConsoleKey key)
        {
            if (key == ConsoleKey.LeftArrow)
            {
                if (!CheckCollision(currentShape, currentX - 1, currentY)) currentX--;
            }
            else if (key == ConsoleKey.RightArrow)
            {
                if (!CheckCollision(currentShape, currentX + 1, currentY)) currentX++;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                if (!CheckCollision(currentShape, currentX, currentY + 1)) currentY++;
            }
            else if (key == ConsoleKey.UpArrow)
            {
                int[,] rotated = RotateShape(currentShape);
                if (!CheckCollision(rotated, currentX, currentY))
                {
                    currentShape = rotated;
                }
            }
        }
        /// <summary>
        /// Обновление статуса всей игры.
        /// </summary>
        public void UpdateState()
        {
            currentY++;
            if (CheckCollision(currentShape, currentX, currentY))
            {
                currentY--;
                FixatePiece();
                ClearLines();
                SpawnPiece();
                CheckGameOver();
            }
        }
        /// <summary>
        /// Обновления статуса поражения.
        /// </summary>
        public void CheckGameOver()
        {
            if (CheckCollision(currentShape, currentX, currentY))
            {
                gameOver = true;
            }
        }
        /// <summary>
        /// Создание фигуры.
        /// </summary>
        public void SpawnPiece()
        {
            currentShape = Shapes[rand.Next(Shapes.Length)];
            currentY = 0;
            currentX = (10 - currentShape.GetLength(1)) / 2;
        }
        /// <summary>
        /// Проверка на биение фигуры об край поля в конвульсиях.
        /// </summary>
        /// <param name="shape">Массив фигур на выбор.</param>
        /// <param name="x">Позиция по Х.</param>
        /// /// <param name="y">Позиция по У.</param>
        /// <returns>Выводит да/нет на проверку коллизии.</returns>
        public bool CheckCollision(int[,] shape, int x, int y)
        {
            for (int i = 0; i < shape.GetLength(0); i++)
            {
                for (int j = 0; j < shape.GetLength(1); j++)
                {
                    if (shape[i, j] == 1)
                    {
                        int boardX = x + j;
                        int boardY = y + i;
                        if (boardX < 0 || boardX >= 10 || boardY >= 10) return true;
                        if (boardY >= 0 && field[boardY, boardX] == 1) return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Фиксация фигуры.
        /// </summary>
        public void FixatePiece()
        {
            for (int i = 0; i < currentShape.GetLength(0); i++)
            {
                for (int j = 0; j < currentShape.GetLength(1); j++)
                {
                    if (currentShape[i, j] == 1)
                    {
                        int boardX = currentX + j;
                        int boardY = currentY + i;
                        if (boardY >= 0 && boardY < 10 && boardX >= 0 && boardX < 10)
                        {
                            field[boardY, boardX] = 1;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Очищение линий после заполнения.
        /// </summary>
        public void ClearLines()
        {
            for (int i = 9; i >= 0; i--)
            {
                bool isFull = true;
                for (int j = 0; j < 10; j++)
                {
                    if (field[i, j] == 0)
                    {
                        isFull = false;
                        break;
                    }
                }

                if (isFull)
                {
                    for (int k = i; k > 0; k--)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            field[k, j] = field[k - 1, j];
                        }
                    }
                    for (int j = 0; j < 10; j++)
                    {
                        field[0, j] = 0;
                    }

                    score += 100;
                    i++;
                }
            }
        }
        /// <summary>
        /// Перевертыш на фигуре (транспонирование матрицы фигуры).
        /// <param name="shape">Фигурка.</param>
        /// <returns>Возвращает перевертыша.</returns>
        public int[,] RotateShape(int[,] shape)
        {
            int rows = shape.GetLength(0);
            int cols = shape.GetLength(1);
            int[,] rotated = new int[cols, rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    rotated[j, rows - 1 - i] = shape[i, j];
                }
            }
            return rotated;
        }
        /// <summary>
        /// Отрисовка поля.
        /// </summary>
        public void DrawField()
        {
            Console.SetCursorPosition(0, 0);
            
            Console.WriteLine($"Очки: {score}");
            Console.WriteLine("Управление: стрелками (вниз - ускорение и вверх - переворот)");
            Console.WriteLine("======================");

            for (int i = 0; i < 10; i++)
            {
                Console.Write("|");
                for (int j = 0; j < 10; j++)
                {
                    bool isFallingPiece = false;
                    if (currentShape != null && i >= currentY && i < currentY + currentShape.GetLength(0) 
                        && j >= currentX && j < currentX + currentShape.GetLength(1))
                    {
                        if (currentShape[i - currentY, j - currentX] == 1)
                        {
                            isFallingPiece = true;
                        }
                    }

                    if (isFallingPiece || field[i, j] == 1)
                        Console.Write("[]");
                    else
                        Console.Write(" .");
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("======================");
        }
    }
}