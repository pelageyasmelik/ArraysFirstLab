using System;

class Program
{
    static void Main()
    {
        // Часть 1
        // Заданный массив вещественных чисел
        double[] array = { 0.5, -2.3, 1.1, 3.7, 0.8, 2.4, -1.5, 4.2, 1.9, -0.6 };

        // Исходный массив
        Console.WriteLine("Исходный массив:");
        PrintOneDArray(array);
        
        // Найти номер минимального элемента
        var minIndex = FindMinIndex(array);
        Console.WriteLine($"Индекс минимального элемента: {minIndex}");

        // Найти сумму элементов между первым и вторым отрицательными элементами
        var sumBetweenNegatives = SumBetweenNegatives(array);
        Console.WriteLine($"Сумма элементов между первым и вторым отрицательными: {sumBetweenNegatives}");

        // Преобразовать массив
        TransformArray(array);

        // Вывести преобразованный массив
        Console.WriteLine("Преобразованный массив:");
        PrintOneDArray(array);
        
        // Часть 2
        int[,] matrix = {
            { 1, -2, 3, 4 },
            { 5, 6, -7, 8 },
            { 9, 10, 11, -12 }
        };

        // Вывод исходной матрицы
        Console.WriteLine("Исходная матрица:");
        PrintMatrix(matrix);

        // Перестановка столбцов в соответствии с ростом характеристик
        SortColumnsByCharacteristics(matrix);

        // Вывод матрицы после перестановки
        Console.WriteLine("\nМатрица после перестановки столбцов:");
        PrintMatrix(matrix);

        // Нахождение суммы элементов в столбцах с отрицательными элементами
        Console.WriteLine("\nСумма элементов в столбцах с отрицательными элементами: ");
        SumOfColumnsWithNegatives(matrix);

    }

    private static void PrintOneDArray(double[] array)
    {
        foreach (var element in array)
            Console.Write($"{element}    ");
        Console.WriteLine();
    }

    static int FindMinIndex(double[] arr)
    {
        var minIndex = 0;
        for (var i = 1; i < arr.Length; i++)
        {
            if (arr[i] < arr[minIndex])
            {
                minIndex = i;
            }
        }
        return minIndex;
    }

    static double SumBetweenNegatives(double[] arr)
    {
        var firstNegativeIndex = -1;
        var secondNegativeIndex = -1;

        for (var i = 0; i < arr.Length; i++)
        {
            if (arr[i] < 0)
            {
                if (firstNegativeIndex == -1)
                {
                    firstNegativeIndex = i;
                }
                else
                {
                    secondNegativeIndex = i;
                    break;
                }
            }
        }

        if (firstNegativeIndex == -1 || secondNegativeIndex == -1)
        {
            // Если нет двух отрицательных элементов
            return 0;
        }

        double sum = 0;
        for (var i = firstNegativeIndex + 1; i < secondNegativeIndex; i++)
        {
            sum += arr[i];
        }

        return sum;
    }

    static void TransformArray(double[] arr)
    {
        Array.Sort(arr, (a, b) => Math.Abs(a) <= 1 ? -1 : 1);
    }
    
    // Вывод матрицы в консоль
    static void PrintMatrix(int[,] matrix)
    {
        var rows = matrix.GetLength(0);
        var columns = matrix.GetLength(1);

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < columns; j++)
            {
                Console.Write($"{matrix[i, j],4} "); // Вывод элемента матрицы с дополнительным форматированием ширины
            }
            Console.WriteLine(); // Переход на новую строку после каждой строки матрицы
        }
    }

    // Сортировка столбцов матрицы по их характеристикам (сумме модулей отрицательных нечетных элементов).
    static void SortColumnsByCharacteristics(int[,] matrix)
    {
        var columns = matrix.GetLength(1);
        int[] characteristics = new int[columns];

        // Рассчет характеристик для каждого столбца
        for (var j = 0; j < columns; j++)
        {
            characteristics[j] = CalculateColumnCharacteristic(matrix, j);
        }

        // Сортировка столбцов по характеристикам (пузырьковая сортировка)
        for (var i = 0; i < columns - 1; i++)
        {
            for (var j = 0; j < columns - 1 - i; j++)
            {
                if (characteristics[j] > characteristics[j + 1])
                {
                    SwapColumns(matrix, j, j + 1); // Обмен местами столбцов
                    Swap(ref characteristics[j], ref characteristics[j + 1]); // Обмен местами характеристик
                }
            }
        }
    }

    // Рассчет характеристики столбца
    static int CalculateColumnCharacteristic(int[,] matrix, int columnIndex)
    {
        var rows = matrix.GetLength(0);
        var characteristic = 0;

        for (int i = 0; i < rows; i++)
        {
            var element = matrix[i, columnIndex];
            if (element < 0 && element % 2 != 0)
            {
                characteristic += Math.Abs(element); // Увеличение характеристики при нахождении отрицательного нечетного элемента
            }
        }

        return characteristic; // Возвращение рассчитанной характеристики
    }

    // Обмен местами двух столбцов матрицы
    static void SwapColumns(int[,] matrix, int columnIndex1, int columnIndex2)
    {
        var rows = matrix.GetLength(0);

        for (var i = 0; i < rows; i++)
        {
            var temp = matrix[i, columnIndex1];
            matrix[i, columnIndex1] = matrix[i, columnIndex2];
            matrix[i, columnIndex2] = temp; // Обмен значениями элементов столбцов
        }
    }

    // Общий метод обмена значениями переменных (дженерик)
    static void Swap<T>(ref T a, ref T b)
    {
        T temp = a;
        a = b;
        b = temp; // Обмен значениями переменных
    }

    // Рассчет и вывод суммы элементов в столбцах, содержащих хотя бы один отрицательный элемент
    static void SumOfColumnsWithNegatives(int[,] matrix)
    {
        var columns = matrix.GetLength(1);

        for (var j = 0; j < columns; j++)
        {
            var hasNegative = false; // Флаг, указывающий на наличие отрицательного элемента в столбце
            var sum = 0;

            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[i, j] < 0)
                {
                    hasNegative = true; // Установка флага при обнаружении отрицательного элемента
                }
                sum += matrix[i, j]; // Суммирование всех элементов столбца
            }

            // Если в столбце есть хотя бы один отрицательный элемент, выводим сумму
            if (hasNegative)
            {
                Console.WriteLine($"Сумма в столбце {j + 1}: {sum}");
            }
        }
    }
}
