namespace lab1;
using static Int32;

public static class MatrixMethods
{
    public static void MultiplyMatrix(List<List<int>> matrix, List<List<int>> anotherMatrix, ref int[] to, int count,
        ref List<List<int>> matrixToSum, ref int something)
    {
        var matrixDimension = matrix.Count;
        
        var newMatrix = new List<List<int>>();

        for (var i = 0; i < matrixDimension; i++)
        {
            if (anotherMatrix.Sum(line => line[i]) == 0 && to[i] == -1)
            {
                to[i] = count;
            }
            var line = new List<int>();
            for (var j = 0; j < matrixDimension; j++)
            {
                var sum = 0;
                var firstElements = matrix[i];
                var secondElements = anotherMatrix.Select(l => l[j]).ToList();
                for (var k = 0; k < matrixDimension; k++)
                {
                    sum += firstElements[k] * secondElements[k];
                }
                line.Add(sum);
            }
            
            newMatrix.Add(line);
        }
        
        for (var i = 0; i < matrixDimension; i++)
        {
            for (var j = 0; j < matrixDimension; j++)
            {
                matrixToSum[i][j] += newMatrix[i][j];
            }
        }

        count++;
        something = count;
        WriteMatrix(newMatrix);

        if (newMatrix.Any(l => l.Any(value => value != 0)))
        {
            Console.WriteLine();
            MultiplyMatrix(matrix, newMatrix, ref to, count, ref matrixToSum, ref something);
        }
        else
        {
            for (var i = 0; i < to.Length; i++)
            {
                if (to[i] == -1)
                {
                    to[i] = count;
                }
            }
        }
    }

    public static void WriteMatrix(List<List<int>> matrix)
    {
        var matrixDimension = matrix.Count;
        var sums = new List<int>();
        for (var i = 0; i < matrixDimension; i++)
        {
            sums.Add(matrix.Sum(line => line[i]));
            
            Console.Write("|");
            for (var j = 0; j < matrixDimension; j++)
            {
                Console.Write($" {matrix[i][j]} |");
            }

            Console.BackgroundColor = ConsoleColor.Yellow;
            var sum = matrix[i].Sum();
            Console.Write($" {sum}");
            Console.ResetColor();
            Console.WriteLine();
        }
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.Write("|");
        foreach (var sum in sums)
        {
            Console.Write($" {sum} |");
        }
        Console.ResetColor();
        Console.WriteLine();
    }
    
    public static void CheckMatrix(List<List<int>> matrix)
    {
        var matrixDimension = matrix.Count;

        var ways = new List<int>();

        for (var i = 0; i < matrixDimension; i++)
        {
            for (var j = 0; j < matrixDimension; j++)
            {
                if (matrix[i][j] == 1)
                {
                    ways.Add(j);
                    CheckElement(j, matrixDimension, matrix, ref ways);
                    ways.Clear();
                }
            }
        }
        
        Console.WriteLine("Исходная матрица корректна");
    }

    public static List<List<int>> FillMatrix(int matrixDimension)
    {
        var matrix = new List<List<int>>();
        for (var i = 0; i < matrixDimension; i++)
        {
            var line = new List<int>();
            int? value = null;
    
            for (var j = 0; j < matrixDimension; j++)
            {
                Console.Write($"Введите значение элемента [{i};{j}]: ");
                if (TryParse(Console.ReadLine(), out var convertedValue))
                {
                    value = convertedValue;
                }
        
                ArgumentNullException.ThrowIfNull(value);

                if (value != 0 && value != 1)
                {
                    throw new Exception("Значения в матрице могут быть только 0 или 1");
                }
        
                line.Add(value.Value);
        
                Console.WriteLine();
            }
    
            matrix.Add(line);
        }

        return matrix;
    }

    public static void AddLine(string? text = null)
    {
        if (text is null)
        {
            Console.WriteLine();
        }
        
        Console.WriteLine(text);
    }
    
    private static void CheckElement(int rowNumber, int matrixDimension, List<List<int>> matrix, ref List<int> ways)
    {
        for (var i = 0; i < matrixDimension; i++)
        {
            if (matrix[rowNumber][i] == 1)
            {
                if (ways.Contains(i))
                {
                    throw new Exception("Граф, построенный по исходной матрице закольцован");
                }
                
                ways.Add(i);
                CheckElement(i, matrixDimension, matrix, ref ways);
            }
        }
    }
}